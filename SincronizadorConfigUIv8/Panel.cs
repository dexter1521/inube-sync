using System;
using System.ServiceProcess;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SincronizadorConfigUIv8;
using SincronizadorCore.Models;

namespace SincronizadorConfigUIv8
{
	public partial class Panel : Form
	{
		private AppSettings _settings;
		private RootConfig _config;
		const string SERVICE_NAME = "InubeSync";
		string _configPath = "";
		RootConfig _cfg = new RootConfig();
		public Panel()
		{
			_settings = new AppSettings();
			_config = new RootConfig();
			InitializeComponent();
			try
			{
				_configPath = ConfigHelper.GetAppSettingsPath(SERVICE_NAME);
			}
			catch
			{
				_configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "appsettings.json");
			}
		}

		// Botón para iniciar sincronización
		private void btnIniciar_Click(object sender, EventArgs e)
		{
			try
			{
				using var sc = GetController(SERVICE_NAME);
				if (sc.Status == ServiceControllerStatus.Running)
				{
					MessageBox.Show("El servicio ya está iniciado."); return;
				}
				sc.Start();
				sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
				RefreshServiceStatus();
			}
			catch (System.Exception ex)
			{

				MessageBox.Show("No se pudo iniciar: " + ex.Message);
			}
		}

		// Botón para detener sincronización
		private void btnDetener_Click(object sender, EventArgs e)
		{
			try
			{
				using var sc = GetController(SERVICE_NAME);
				if (sc.CanStop && sc.Status == ServiceControllerStatus.Running)
				{
					sc.Stop();
					sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
					RefreshServiceStatus();
				}
				else
				{
					MessageBox.Show("El servicio no admite detención o no está corriendo.");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("No se pudo detener: " + ex.Message);
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		}

		private void Timer1_Tick(object sender, EventArgs e)
		{
			// Método generado para evitar error CS0103. Implementa la lógica si es necesario.
		}

		private void btnCargar_Click(object sender, EventArgs e)
		{
			try
			{
				_config = ConfigHelper.LoadConfig();
				_settings = _config.AppSettings;
				nudIntervalo.Value = _settings.IntervaloMinutos > 0 ? _settings.IntervaloMinutos : 1;
				txtApiUrl.Text = _settings.ApiUrl;
				txtApiUser.Text = _settings.ApiUser;
				txtDeviceToken.Text = _settings.DeviceToken;
				txtConnectionString.Text = _settings.SqlServer;
				chkBoxProductos.Checked = _settings.DescargarProductos;
				chkBoxVentas.Checked = _settings.SubirVentas;
				chkBoxCortes.Checked = _settings.SubirCortes;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error al cargar configuración:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnGuardar_Click(object sender, EventArgs e)
		{
			try
			{
				if (!_ValidarCampos())
					return;

				_settings.IntervaloMinutos = (int)nudIntervalo.Value;
				_settings.ApiUrl = txtApiUrl.Text.Trim();
				_settings.ApiUser = txtApiUser.Text.Trim();
				_settings.DeviceToken = txtDeviceToken.Text.Trim();
				_settings.SqlServer = txtConnectionString.Text.Trim();
				_settings.DescargarProductos = chkBoxProductos.Checked;
				_settings.SubirVentas = chkBoxVentas.Checked;
				_settings.SubirCortes = chkBoxCortes.Checked;
				_config.AppSettings = _settings;
				ConfigHelper.SaveConfig(_config);

				MessageBox.Show("Configuración guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
				if (Panel.ActiveForm != null)
					Panel.ActiveForm.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error al guardar configuración:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private bool _ValidarCampos()
		{
			if (nudIntervalo.Value <= 0)
			{
				MessageBox.Show("El intervalo debe ser mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (string.IsNullOrWhiteSpace(txtApiUrl.Text))
			{
				MessageBox.Show("La URL de la API no puede estar vacía.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (string.IsNullOrWhiteSpace(txtApiUser.Text))
			{
				MessageBox.Show("El usuario de la API no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (string.IsNullOrWhiteSpace(txtConnectionString.Text))
			{
				MessageBox.Show("La cadena de conexión no puede estar vacía.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (string.IsNullOrWhiteSpace(txtDeviceToken.Text))
			{
				MessageBox.Show("El DeviceToken no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}



			// Puedes agregar más validaciones según tus necesidades
			return true;
		}


		// Auxiliar para obtener el controlador del servicio
		private ServiceController GetController(string serviceName)
		{
			return new ServiceController(serviceName);
		}

		// Refresca el estado del servicio en la UI
		private void RefreshServiceStatus()
		{
			try
			{
				using var sc = GetController(SERVICE_NAME);
				lblEstado.Text = $"Estado: {sc.Status}";
			}
			catch
			{
				lblEstado.Text = "Estado: desconocido";
			}
		}

		private async void btnReiniciar_Click(object sender, EventArgs e)
		{
			try
			{
				using var sc = GetController(SERVICE_NAME);
				if (sc.Status == ServiceControllerStatus.Running)
				{
					if (!sc.CanStop) { MessageBox.Show("El servicio no puede detenerse."); return; }
					sc.Stop();
					sc.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
				}

				// Pequeña espera para liberar handles de archivos
				await Task.Delay(1000);

				sc.Start();
				sc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
				RefreshServiceStatus();
			}
			catch (System.Exception ex)
			{

				MessageBox.Show("No se pudo reiniciar: " + ex.Message);
			}
		}
	}
}

