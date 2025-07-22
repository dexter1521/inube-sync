namespace SincronizadorConfigUI
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
			this.btnCargar = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtApiUrl = new System.Windows.Forms.TextBox();
			this.txtApiUser = new System.Windows.Forms.TextBox();
			this.txtApiPassword = new System.Windows.Forms.TextBox();
			this.txtLogDirectory = new System.Windows.Forms.TextBox();
			this.txtConnectionString = new System.Windows.Forms.TextBox();
			this.btnGuardar = new System.Windows.Forms.Button();
			this.nudIntervalo = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.nudIntervalo)).BeginInit();
			this.SuspendLayout();
			// 
			// btnCargar
			// 
			this.btnCargar.Location = new System.Drawing.Point(106, 338);
			this.btnCargar.Name = "btnCargar";
			this.btnCargar.Size = new System.Drawing.Size(75, 23);
			this.btnCargar.TabIndex = 0;
			this.btnCargar.Text = "Cargar";
			this.btnCargar.UseVisualStyleBackColor = true;
			this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(35, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "URL API";
			// 
			// txtApiUrl
			// 
			this.txtApiUrl.Location = new System.Drawing.Point(106, 36);
			this.txtApiUrl.Name = "txtApiUrl";
			this.txtApiUrl.Size = new System.Drawing.Size(354, 20);
			this.txtApiUrl.TabIndex = 2;
			// 
			// txtApiUser
			// 
			this.txtApiUser.Location = new System.Drawing.Point(106, 72);
			this.txtApiUser.Name = "txtApiUser";
			this.txtApiUser.Size = new System.Drawing.Size(354, 20);
			this.txtApiUser.TabIndex = 3;
			// 
			// txtApiPassword
			// 
			this.txtApiPassword.Location = new System.Drawing.Point(106, 116);
			this.txtApiPassword.MaxLength = 16;
			this.txtApiPassword.Name = "txtApiPassword";
			this.txtApiPassword.Size = new System.Drawing.Size(354, 20);
			this.txtApiPassword.TabIndex = 4;
			// 
			// txtLogDirectory
			// 
			this.txtLogDirectory.Location = new System.Drawing.Point(106, 152);
			this.txtLogDirectory.Name = "txtLogDirectory";
			this.txtLogDirectory.Size = new System.Drawing.Size(354, 20);
			this.txtLogDirectory.TabIndex = 5;
			// 
			// txtConnectionString
			// 
			this.txtConnectionString.Location = new System.Drawing.Point(106, 194);
			this.txtConnectionString.Name = "txtConnectionString";
			this.txtConnectionString.Size = new System.Drawing.Size(354, 20);
			this.txtConnectionString.TabIndex = 6;
			// 
			// btnGuardar
			// 
			this.btnGuardar.Location = new System.Drawing.Point(385, 338);
			this.btnGuardar.Name = "btnGuardar";
			this.btnGuardar.Size = new System.Drawing.Size(75, 23);
			this.btnGuardar.TabIndex = 8;
			this.btnGuardar.Text = "Guardar";
			this.btnGuardar.UseVisualStyleBackColor = true;
			this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
			// 
			// nudIntervalo
			// 
			this.nudIntervalo.Location = new System.Drawing.Point(106, 237);
			this.nudIntervalo.Name = "nudIntervalo";
			this.nudIntervalo.Size = new System.Drawing.Size(354, 20);
			this.nudIntervalo.TabIndex = 9;
			this.nudIntervalo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(35, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(43, 13);
			this.label2.TabIndex = 10;
			this.label2.Text = "Usuario";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(35, 116);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 13);
			this.label3.TabIndex = 11;
			this.label3.Text = "Password";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(35, 152);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(49, 13);
			this.label4.TabIndex = 12;
			this.label4.Text = "Dir. Logs";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(35, 194);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(51, 13);
			this.label5.TabIndex = 13;
			this.label5.Text = "Conexión";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(17, 237);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(67, 13);
			this.label6.TabIndex = 14;
			this.label6.Text = "Tiem de Act.";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(528, 395);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.nudIntervalo);
			this.Controls.Add(this.btnGuardar);
			this.Controls.Add(this.txtConnectionString);
			this.Controls.Add(this.txtLogDirectory);
			this.Controls.Add(this.txtApiPassword);
			this.Controls.Add(this.txtApiUser);
			this.Controls.Add(this.txtApiUrl);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnCargar);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Inube v1";
			((System.ComponentModel.ISupportInitialize)(this.nudIntervalo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

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

