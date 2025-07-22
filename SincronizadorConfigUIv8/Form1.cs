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
	public partial class Form1 : Form
	{
		private AppSettings _settings;

		public Form1()
		{
			_settings = new AppSettings();
			InitializeComponent();
		}

		private void btnCargar_Click(object sender, EventArgs e)
		{
			try
			{
				_settings = ConfigHelper.LoadConfig();
				nudIntervalo.Value = _settings.IntervaloMinutos > 0 ? _settings.IntervaloMinutos : 1;
				txtApiUrl.Text = _settings.ApiUrl;
				txtApiUser.Text = _settings.ApiUser;
				txtApiPassword.Text = _settings.ApiPassword;
				txtLogDirectory.Text = _settings.LogDirectory;
				txtConnectionString.Text = _settings.ConnectionStrings;
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
				_settings.ApiPassword = txtApiPassword.Text.Trim();
				_settings.LogDirectory = txtLogDirectory.Text.Trim();
				_settings.ConnectionStrings = txtConnectionString.Text.Trim();
				ConfigHelper.SaveConfig(_settings);

				MessageBox.Show("Configuración guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
				if (Form1.ActiveForm != null)
					Form1.ActiveForm.Close();
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

			string logDir = txtLogDirectory.Text.Trim();

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
