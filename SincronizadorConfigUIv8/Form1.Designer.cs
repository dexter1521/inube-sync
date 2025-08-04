namespace SincronizadorConfigUIv8
{
	partial class Form1
	{
		/// <summary>
		/// Variable del diseñador necesaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpiar los recursos que se estén usando.
		/// </summary>
		/// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Código generado por el Diseñador de Windows Forms

		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido de este método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			btnCargar = new Button();
			label1 = new Label();
			txtApiUrl = new TextBox();
			txtApiUser = new TextBox();
			txtApiPassword = new TextBox();
			txtLogDirectory = new TextBox();
			txtConnectionString = new TextBox();
			btnGuardar = new Button();
			nudIntervalo = new NumericUpDown();
			label2 = new Label();
			label3 = new Label();
			label4 = new Label();
			label5 = new Label();
			label6 = new Label();
			((System.ComponentModel.ISupportInitialize)nudIntervalo).BeginInit();
			SuspendLayout();
			// 
			// btnCargar
			// 
			btnCargar.Location = new Point(123, 390);
			btnCargar.Margin = new Padding(4, 4, 4, 4);
			btnCargar.Name = "btnCargar";
			btnCargar.Size = new Size(88, 26);
			btnCargar.TabIndex = 0;
			btnCargar.Text = "Cargar";
			btnCargar.UseVisualStyleBackColor = true;
			btnCargar.Click += btnCargar_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(41, 41);
			label1.Margin = new Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new Size(49, 15);
			label1.TabIndex = 1;
			label1.Text = "URL API";
			// 
			// txtApiUrl
			// 
			txtApiUrl.Location = new Point(123, 41);
			txtApiUrl.Margin = new Padding(4, 4, 4, 4);
			txtApiUrl.Name = "txtApiUrl";
			txtApiUrl.Size = new Size(413, 23);
			txtApiUrl.TabIndex = 2;
			// 
			// txtApiUser
			// 
			txtApiUser.Location = new Point(123, 83);
			txtApiUser.Margin = new Padding(4, 4, 4, 4);
			txtApiUser.Name = "txtApiUser";
			txtApiUser.Size = new Size(413, 23);
			txtApiUser.TabIndex = 3;
			// 
			// txtApiPassword
			// 
			txtApiPassword.Location = new Point(123, 115);
			txtApiPassword.Margin = new Padding(4, 4, 4, 4);
			txtApiPassword.MaxLength = 255;
			txtApiPassword.Multiline = true;
			txtApiPassword.Name = "txtApiPassword";
			txtApiPassword.Size = new Size(413, 53);
			txtApiPassword.TabIndex = 4;
			// 
			// txtLogDirectory
			// 
			txtLogDirectory.Location = new Point(123, 176);
			txtLogDirectory.Margin = new Padding(4, 4, 4, 4);
			txtLogDirectory.Name = "txtLogDirectory";
			txtLogDirectory.Size = new Size(413, 23);
			txtLogDirectory.TabIndex = 5;
			// 
			// txtConnectionString
			// 
			txtConnectionString.Location = new Point(123, 224);
			txtConnectionString.Margin = new Padding(4, 4, 4, 4);
			txtConnectionString.Name = "txtConnectionString";
			txtConnectionString.Size = new Size(413, 23);
			txtConnectionString.TabIndex = 6;
			// 
			// btnGuardar
			// 
			btnGuardar.Location = new Point(449, 390);
			btnGuardar.Margin = new Padding(4, 4, 4, 4);
			btnGuardar.Name = "btnGuardar";
			btnGuardar.Size = new Size(88, 26);
			btnGuardar.TabIndex = 8;
			btnGuardar.Text = "Guardar";
			btnGuardar.UseVisualStyleBackColor = true;
			btnGuardar.Click += btnGuardar_Click;
			// 
			// nudIntervalo
			// 
			nudIntervalo.Location = new Point(123, 274);
			nudIntervalo.Margin = new Padding(4, 4, 4, 4);
			nudIntervalo.Name = "nudIntervalo";
			nudIntervalo.Size = new Size(413, 23);
			nudIntervalo.TabIndex = 9;
			nudIntervalo.TextAlign = HorizontalAlignment.Right;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(41, 83);
			label2.Margin = new Padding(4, 0, 4, 0);
			label2.Name = "label2";
			label2.Size = new Size(47, 15);
			label2.TabIndex = 10;
			label2.Text = "Usuario";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(41, 115);
			label3.Margin = new Padding(4, 0, 4, 0);
			label3.Name = "label3";
			label3.Size = new Size(57, 15);
			label3.TabIndex = 11;
			label3.Text = "Password";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(41, 176);
			label4.Margin = new Padding(4, 0, 4, 0);
			label4.Name = "label4";
			label4.Size = new Size(53, 15);
			label4.TabIndex = 12;
			label4.Text = "Dir. Logs";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new Point(41, 224);
			label5.Margin = new Padding(4, 0, 4, 0);
			label5.Name = "label5";
			label5.Size = new Size(58, 15);
			label5.TabIndex = 13;
			label5.Text = "Conexión";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new Point(41, 274);
			label6.Margin = new Padding(4, 0, 4, 0);
			label6.Name = "label6";
			label6.Size = new Size(73, 15);
			label6.TabIndex = 14;
			label6.Text = "Tiem de Act.";
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(616, 456);
			Controls.Add(label6);
			Controls.Add(label5);
			Controls.Add(label4);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(nudIntervalo);
			Controls.Add(btnGuardar);
			Controls.Add(txtConnectionString);
			Controls.Add(txtLogDirectory);
			Controls.Add(txtApiPassword);
			Controls.Add(txtApiUser);
			Controls.Add(txtApiUrl);
			Controls.Add(label1);
			Controls.Add(btnCargar);
			Margin = new Padding(4, 4, 4, 4);
			Name = "Form1";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Inube v1";
			((System.ComponentModel.ISupportInitialize)nudIntervalo).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Button btnCargar;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtApiUrl;
		private System.Windows.Forms.TextBox txtApiUser;
		private System.Windows.Forms.TextBox txtApiPassword;
		private System.Windows.Forms.TextBox txtLogDirectory;
		private System.Windows.Forms.TextBox txtConnectionString;
		private System.Windows.Forms.Button btnGuardar;
		private System.Windows.Forms.NumericUpDown nudIntervalo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
	}
}

