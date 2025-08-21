namespace SincronizadorConfigUIv8
{
	partial class Panel
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			chkBoxProductos = new CheckBox();
			Timer1 = new System.Windows.Forms.Timer(components);
			groupBox1 = new GroupBox();
			btnReiniciar = new Button();
			btnGuardar = new Button();
			btnCargar = new Button();
			txtConnectionString = new TextBox();
			nudIntervalo = new NumericUpDown();
			label6 = new Label();
			label5 = new Label();
			label3 = new Label();
			txtDeviceToken = new TextBox();
			txtApiUser = new TextBox();
			label2 = new Label();
			label1 = new Label();
			txtApiUrl = new TextBox();
			btnDetener = new Button();
			btnIniciar = new Button();
			lblEstado = new Label();
			groupBox2 = new GroupBox();
			groupBox3 = new GroupBox();
			chkBoxVentas = new CheckBox();
			chkBoxCortes = new CheckBox();
			tabControl1 = new TabControl();
			tabPage1 = new TabPage();
			dataGridView1 = new DataGridView();
			tabPage2 = new TabPage();
			logDataGridView = new DataGridView();
			backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			progressBar1 = new ProgressBar();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)nudIntervalo).BeginInit();
			groupBox2.SuspendLayout();
			groupBox3.SuspendLayout();
			tabControl1.SuspendLayout();
			tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
			tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)logDataGridView).BeginInit();
			SuspendLayout();
			// 
			// chkBoxProductos
			// 
			chkBoxProductos.AutoSize = true;
			chkBoxProductos.Location = new Point(8, 20);
			chkBoxProductos.Margin = new Padding(3, 2, 3, 2);
			chkBoxProductos.Name = "chkBoxProductos";
			chkBoxProductos.Size = new Size(83, 17);
			chkBoxProductos.TabIndex = 1;
			chkBoxProductos.Text = "Productos";
			chkBoxProductos.UseVisualStyleBackColor = true;
			// 
			// Timer1
			// 
			Timer1.Tick += Timer1_Tick;
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(btnReiniciar);
			groupBox1.Controls.Add(btnGuardar);
			groupBox1.Controls.Add(btnCargar);
			groupBox1.Controls.Add(txtConnectionString);
			groupBox1.Controls.Add(nudIntervalo);
			groupBox1.Controls.Add(label6);
			groupBox1.Controls.Add(label5);
			groupBox1.Controls.Add(label3);
			groupBox1.Controls.Add(txtDeviceToken);
			groupBox1.Controls.Add(txtApiUser);
			groupBox1.Controls.Add(label2);
			groupBox1.Controls.Add(label1);
			groupBox1.Controls.Add(txtApiUrl);
			groupBox1.Controls.Add(btnDetener);
			groupBox1.Controls.Add(btnIniciar);
			groupBox1.Controls.Add(lblEstado);
			groupBox1.Font = new Font("Tahoma", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
			groupBox1.Location = new Point(10, 6);
			groupBox1.Margin = new Padding(3, 2, 3, 2);
			groupBox1.Name = "groupBox1";
			groupBox1.Padding = new Padding(3, 2, 3, 2);
			groupBox1.Size = new Size(781, 130);
			groupBox1.TabIndex = 10;
			groupBox1.TabStop = false;
			groupBox1.Text = "Datos Generales";
			// 
			// btnReiniciar
			// 
			btnReiniciar.Location = new Point(674, 45);
			btnReiniciar.Name = "btnReiniciar";
			btnReiniciar.Size = new Size(101, 29);
			btnReiniciar.TabIndex = 30;
			btnReiniciar.Text = "Recargar";
			btnReiniciar.UseVisualStyleBackColor = true;
			btnReiniciar.Click += btnReiniciar_Click;
			// 
			// btnGuardar
			// 
			btnGuardar.Location = new Point(509, 89);
			btnGuardar.Margin = new Padding(4);
			btnGuardar.Name = "btnGuardar";
			btnGuardar.Size = new Size(88, 26);
			btnGuardar.TabIndex = 29;
			btnGuardar.Text = "Guardar";
			btnGuardar.UseVisualStyleBackColor = true;
			btnGuardar.Click += btnGuardar_Click;
			// 
			// btnCargar
			// 
			btnCargar.Location = new Point(421, 89);
			btnCargar.Margin = new Padding(4);
			btnCargar.Name = "btnCargar";
			btnCargar.Size = new Size(88, 26);
			btnCargar.TabIndex = 28;
			btnCargar.Text = "Cargar";
			btnCargar.UseVisualStyleBackColor = true;
			btnCargar.Click += btnCargar_Click;
			// 
			// txtConnectionString
			// 
			txtConnectionString.Location = new Point(421, 13);
			txtConnectionString.Margin = new Padding(4);
			txtConnectionString.Name = "txtConnectionString";
			txtConnectionString.Size = new Size(176, 20);
			txtConnectionString.TabIndex = 26;
			// 
			// nudIntervalo
			// 
			nudIntervalo.Location = new Point(421, 45);
			nudIntervalo.Margin = new Padding(4);
			nudIntervalo.Name = "nudIntervalo";
			nudIntervalo.Size = new Size(176, 20);
			nudIntervalo.TabIndex = 25;
			nudIntervalo.TextAlign = HorizontalAlignment.Right;
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new Point(336, 46);
			label6.Margin = new Padding(4, 0, 4, 0);
			label6.Name = "label6";
			label6.Size = new Size(77, 13);
			label6.TabIndex = 24;
			label6.Text = "Tiem de Act.";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new Point(336, 19);
			label5.Margin = new Padding(4, 0, 4, 0);
			label5.Name = "label5";
			label5.Size = new Size(59, 13);
			label5.TabIndex = 23;
			label5.Text = "Conexión";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(8, 71);
			label3.Margin = new Padding(4, 0, 4, 0);
			label3.Name = "label3";
			label3.Size = new Size(61, 13);
			label3.TabIndex = 21;
			label3.Text = "Password";
			// 
			// txtDeviceToken
			// 
			txtDeviceToken.Location = new Point(77, 71);
			txtDeviceToken.Margin = new Padding(4);
			txtDeviceToken.MaxLength = 255;
			txtDeviceToken.Multiline = true;
			txtDeviceToken.Name = "txtDeviceToken";
			txtDeviceToken.Size = new Size(251, 53);
			txtDeviceToken.TabIndex = 20;
			// 
			// txtApiUser
			// 
			txtApiUser.Location = new Point(77, 46);
			txtApiUser.Margin = new Padding(4);
			txtApiUser.Name = "txtApiUser";
			txtApiUser.Size = new Size(251, 20);
			txtApiUser.TabIndex = 19;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(8, 46);
			label2.Margin = new Padding(4, 0, 4, 0);
			label2.Name = "label2";
			label2.Size = new Size(50, 13);
			label2.TabIndex = 18;
			label2.Text = "Usuario";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(8, 19);
			label1.Margin = new Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new Size(52, 13);
			label1.TabIndex = 17;
			label1.Text = "URL API";
			// 
			// txtApiUrl
			// 
			txtApiUrl.Location = new Point(77, 12);
			txtApiUrl.Margin = new Padding(4);
			txtApiUrl.Name = "txtApiUrl";
			txtApiUrl.Size = new Size(251, 20);
			txtApiUrl.TabIndex = 16;
			// 
			// btnDetener
			// 
			btnDetener.Font = new Font("Tahoma", 7.5F, FontStyle.Bold);
			btnDetener.Location = new Point(675, 80);
			btnDetener.Name = "btnDetener";
			btnDetener.Size = new Size(100, 32);
			btnDetener.TabIndex = 15;
			btnDetener.Text = "Detener";
			btnDetener.UseVisualStyleBackColor = true;
			btnDetener.Click += btnDetener_Click;
			// 
			// btnIniciar
			// 
			btnIniciar.Location = new Point(674, 13);
			btnIniciar.Name = "btnIniciar";
			btnIniciar.Size = new Size(101, 29);
			btnIniciar.TabIndex = 14;
			btnIniciar.Text = "Iniciar";
			btnIniciar.UseVisualStyleBackColor = true;
			btnIniciar.Click += btnIniciar_Click;
			// 
			// lblEstado
			// 
			lblEstado.AutoSize = true;
			lblEstado.Location = new Point(619, 96);
			lblEstado.Name = "lblEstado";
			lblEstado.Size = new Size(28, 13);
			lblEstado.TabIndex = 11;
			lblEstado.Text = "..:::..";
			// 
			// groupBox2
			// 
			groupBox2.Controls.Add(chkBoxProductos);
			groupBox2.Font = new Font("Tahoma", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
			groupBox2.Location = new Point(10, 140);
			groupBox2.Margin = new Padding(3, 2, 3, 2);
			groupBox2.Name = "groupBox2";
			groupBox2.Padding = new Padding(3, 2, 3, 2);
			groupBox2.Size = new Size(420, 94);
			groupBox2.TabIndex = 12;
			groupBox2.TabStop = false;
			groupBox2.Text = "Descargar Datos";
			// 
			// groupBox3
			// 
			groupBox3.Controls.Add(chkBoxVentas);
			groupBox3.Controls.Add(chkBoxCortes);
			groupBox3.Font = new Font("Tahoma", 7.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
			groupBox3.Location = new Point(437, 140);
			groupBox3.Margin = new Padding(3, 2, 3, 2);
			groupBox3.Name = "groupBox3";
			groupBox3.Padding = new Padding(3, 2, 3, 2);
			groupBox3.Size = new Size(354, 94);
			groupBox3.TabIndex = 13;
			groupBox3.TabStop = false;
			groupBox3.Text = "Subir Datos";
			// 
			// chkBoxVentas
			// 
			chkBoxVentas.AutoSize = true;
			chkBoxVentas.Location = new Point(4, 20);
			chkBoxVentas.Margin = new Padding(3, 2, 3, 2);
			chkBoxVentas.Name = "chkBoxVentas";
			chkBoxVentas.Size = new Size(65, 17);
			chkBoxVentas.TabIndex = 3;
			chkBoxVentas.Text = "Ventas";
			chkBoxVentas.UseVisualStyleBackColor = true;
			// 
			// chkBoxCortes
			// 
			chkBoxCortes.AutoSize = true;
			chkBoxCortes.Location = new Point(4, 43);
			chkBoxCortes.Margin = new Padding(3, 2, 3, 2);
			chkBoxCortes.Name = "chkBoxCortes";
			chkBoxCortes.Size = new Size(63, 17);
			chkBoxCortes.TabIndex = 4;
			chkBoxCortes.Text = "Cortes";
			chkBoxCortes.UseVisualStyleBackColor = true;
			// 
			// tabControl1
			// 
			tabControl1.Controls.Add(tabPage1);
			tabControl1.Controls.Add(tabPage2);
			tabControl1.Location = new Point(10, 238);
			tabControl1.Margin = new Padding(3, 2, 3, 2);
			tabControl1.Name = "tabControl1";
			tabControl1.SelectedIndex = 0;
			tabControl1.Size = new Size(788, 379);
			tabControl1.TabIndex = 14;
			// 
			// tabPage1
			// 
			tabPage1.Controls.Add(dataGridView1);
			tabPage1.Location = new Point(4, 24);
			tabPage1.Margin = new Padding(3, 2, 3, 2);
			tabPage1.Name = "tabPage1";
			tabPage1.Padding = new Padding(3, 2, 3, 2);
			tabPage1.Size = new Size(780, 351);
			tabPage1.TabIndex = 0;
			tabPage1.Text = "Tareas";
			tabPage1.UseVisualStyleBackColor = true;
			// 
			// dataGridView1
			// 
			dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridView1.Location = new Point(3, 2);
			dataGridView1.Margin = new Padding(3, 2, 3, 2);
			dataGridView1.Name = "dataGridView1";
			dataGridView1.RowHeadersWidth = 51;
			dataGridView1.RowTemplate.Height = 24;
			dataGridView1.Size = new Size(774, 347);
			dataGridView1.TabIndex = 12;
			// 
			// tabPage2
			// 
			tabPage2.Controls.Add(logDataGridView);
			tabPage2.Location = new Point(4, 24);
			tabPage2.Margin = new Padding(3, 2, 3, 2);
			tabPage2.Name = "tabPage2";
			tabPage2.Padding = new Padding(3, 2, 3, 2);
			tabPage2.Size = new Size(780, 351);
			tabPage2.TabIndex = 1;
			tabPage2.Text = "Mensajes";
			tabPage2.UseVisualStyleBackColor = true;
			// 
			// logDataGridView
			// 
			logDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			logDataGridView.Dock = DockStyle.Fill;
			logDataGridView.Location = new Point(3, 2);
			logDataGridView.Margin = new Padding(3, 2, 3, 2);
			logDataGridView.Name = "logDataGridView";
			logDataGridView.RowHeadersWidth = 51;
			logDataGridView.RowTemplate.Height = 24;
			logDataGridView.Size = new Size(774, 347);
			logDataGridView.TabIndex = 0;
			// 
			// progressBar1
			// 
			progressBar1.Location = new Point(18, 622);
			progressBar1.Margin = new Padding(4);
			progressBar1.Name = "progressBar1";
			progressBar1.Size = new Size(774, 26);
			progressBar1.TabIndex = 15;
			// 
			// Panel
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(809, 668);
			Controls.Add(progressBar1);
			Controls.Add(tabControl1);
			Controls.Add(groupBox3);
			Controls.Add(groupBox2);
			Controls.Add(groupBox1);
			Margin = new Padding(3, 2, 3, 2);
			MaximizeBox = false;
			Name = "Panel";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "Sincroniador";
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)nudIntervalo).EndInit();
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			groupBox3.ResumeLayout(false);
			groupBox3.PerformLayout();
			tabControl1.ResumeLayout(false);
			tabPage1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
			tabPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)logDataGridView).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.CheckBox chkBoxProductos;
		private System.Windows.Forms.Timer Timer1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblEstado;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox chkBoxVentas;
		private System.Windows.Forms.CheckBox chkBoxCortes;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.DataGridView logDataGridView;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Button btnIniciar;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.Button btnDetener;
		private ProgressBar progressBar1;
		private TextBox txtApiUrl;
		private Label label1;
		private Label label2;
		private TextBox txtApiUser;
		private TextBox txtDeviceToken;
		private Label label3;
		private Label label5;
		private Label label6;
		private NumericUpDown nudIntervalo;
		private TextBox txtConnectionString;
		private Button btnCargar;
		private Button btnGuardar;
		private Button btnReiniciar;
	}
}