using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SincronizadorConfigUI
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void btnCargar_Click(object sender, EventArgs e)
		{
			try
			{
				var config = ConfigHelper.LoadConfig();
				nudIntervalo.Value = config.IntervaloMinutos > 0 ? config.IntervaloMinutos : 1;
				txtApiUrl.Text = config.ApiUrl;
				txtApiUser.Text = config.ApiUser;
				txtApiPassword.Text = config.ApiPassword;
				txtLogDirectory.Text = config.LogDirectory;
				txtConnectionString.Text = config.ConnectionStrings;
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
				// Ensure nudIntervalo is a NumericUpDown control  
				if (nudIntervalo.Value <= 0)
				{
					MessageBox.Show("El intervalo debe ser mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				var config = new AppSettings
				{
					IntervaloMinutos = (int)nudIntervalo.Value, // Correctly using NumericUpDown control  
					ApiUrl = txtApiUrl.Text.Trim(),
					ApiUser = txtApiUser.Text.Trim(),
					ApiPassword = txtApiPassword.Text.Trim(),
					LogDirectory = txtLogDirectory.Text.Trim(),
					ConnectionStrings = txtConnectionString.Text.Trim()
				};

				ConfigHelper.SaveConfig(config);
				MessageBox.Show("Configuración guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
				Form1.ActiveForm.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error al guardar configuración:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

	}
}
