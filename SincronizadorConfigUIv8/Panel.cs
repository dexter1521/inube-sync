using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SincronizadorCore;
using SincronizadorCore.Utils;

namespace SincronizadorConfigUIv8
{
	public partial class Panel : Form
	{
		// Campos para el control del proceso de sincronización
		private SincronizadorCore.Services.SyncService? _syncService;
		private System.Threading.CancellationTokenSource? _cts;
		private AppSettings _settings;
		public Panel()
		{
			_settings = new AppSettings();
			InitializeComponent();
		}

		// Botón para iniciar sincronización
		private async void btnIniciar_Click(object sender, EventArgs e)
		{
			var settings = ConfigHelper.LoadConfig();
			_cts = new System.Threading.CancellationTokenSource();
			_syncService = new SincronizadorCore.Services.SyncService(settings);

			btnIniciar.Enabled = false;
			btnDetener.Enabled = true;

			// Mostrar progressBar en modo indeterminado
			progressBar1.Style = ProgressBarStyle.Marquee;
			progressBar1.Visible = true;
			// Actualizar estado visual
			lblEstado.Text = "Sincronizando...";
			lblEstado.ForeColor = System.Drawing.Color.Blue;

			try
			{
				await System.Threading.Tasks.Task.Run(() => _syncService.SincronizarHaciaNubeAsync(), _cts.Token);
				MessageBox.Show("Sincronización finalizada.");
				lblEstado.Text = "Listo";
				lblEstado.ForeColor = System.Drawing.Color.Green;
			}
			catch (System.OperationCanceledException)
			{
				MessageBox.Show("Sincronización detenida por el usuario.");
				lblEstado.Text = "Cancelado";
				lblEstado.ForeColor = System.Drawing.Color.OrangeRed;
			}
			finally
			{
				btnIniciar.Enabled = true;
				btnDetener.Enabled = false;
				// Ocultar progressBar al finalizar
				progressBar1.Style = ProgressBarStyle.Blocks;
				progressBar1.Visible = false;
			}
		}

		// Botón para detener sincronización
		private void btnDetener_Click(object sender, EventArgs e)
		{
			_cts?.Cancel();
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
				_settings = ConfigHelper.LoadConfig();
				nudIntervalo.Value = _settings.IntervaloMinutos > 0 ? _settings.IntervaloMinutos : 1;
				txtApiUrl.Text = _settings.ApiUrl;
				txtApiUser.Text = _settings.ApiUser;
				txtDeviceToken.Text = _settings.DeviceToken;
				txtLogsPath.Text = _settings.LogsPath;
				txtConnectionString.Text = _settings.SqlServer;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error al cargar configuración:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
				_settings.LogsPath = txtLogsPath.Text.Trim();
				_settings.SqlServer = txtConnectionString.Text.Trim();
				ConfigHelper.SaveConfig(_settings);

				MessageBox.Show("Configuración guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
				if (Panel.ActiveForm != null)
					Panel.ActiveForm.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error al guardar configuración:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

			string logDir = txtLogsPath.Text.Trim();

			// Validar caracteres inválidos en la ruta de logs
			if (logDir.Any(c => Path.GetInvalidPathChars().Contains(c)))
			{
				MessageBox.Show("La ruta del directorio de logs contiene caracteres inválidos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			// Validar si hay caracteres prohibidos en carpetas
			char[] caracteresProhibidos = Path.GetInvalidFileNameChars();
			if (logDir.IndexOfAny(caracteresProhibidos) >= 0)
			{
				MessageBox.Show("La ruta del directorio de logs contiene caracteres no permitidos por el sistema.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (!Directory.Exists(logDir))
			{
				var resultado = MessageBox.Show(
					$"El directorio '{logDir}' no existe. ¿Deseas crearlo?",
					"Directorio no encontrado",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question);

				if (resultado == DialogResult.Yes)
				{
					try
					{
						Directory.CreateDirectory(logDir);
					}
					catch (Exception ex)
					{
						MessageBox.Show($"No se pudo crear el directorio:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return false;
					}
				}
				else
				{
					return false;
				}
			}



			// Puedes agregar más validaciones según tus necesidades
			return true;
		}











	}
}

