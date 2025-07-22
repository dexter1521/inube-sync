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
			this.components = new System.ComponentModel.Container();
			this.chkBoxProductos = new System.Windows.Forms.CheckBox();
			this.BtnAceptar = new System.Windows.Forms.Button();
			this.Timer1 = new System.Windows.Forms.Timer(this.components);
			this.txtTiempo = new System.Windows.Forms.TextBox();
			this.txtSucursal = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnRemoto = new System.Windows.Forms.Button();
			this.btnLocal = new System.Windows.Forms.Button();
			this.BtnEditar = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.chkBoxVentas = new System.Windows.Forms.CheckBox();
			this.chkBoxCortes = new System.Windows.Forms.CheckBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.logDataGridView = new System.Windows.Forms.DataGridView();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.tabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.logDataGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// chkBoxProductos
			// 
			this.chkBoxProductos.AutoSize = true;
			this.chkBoxProductos.Location = new System.Drawing.Point(9, 21);
			this.chkBoxProductos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.chkBoxProductos.Name = "chkBoxProductos";
			this.chkBoxProductos.Size = new System.Drawing.Size(101, 21);
			this.chkBoxProductos.TabIndex = 1;
			this.chkBoxProductos.Text = "Productos";
			this.chkBoxProductos.UseVisualStyleBackColor = true;
			// 
			// BtnAceptar
			// 
			this.BtnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnAceptar.Location = new System.Drawing.Point(631, 54);
			this.BtnAceptar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.BtnAceptar.Name = "BtnAceptar";
			this.BtnAceptar.Size = new System.Drawing.Size(104, 34);
			this.BtnAceptar.TabIndex = 2;
			this.BtnAceptar.Text = "Sincronizar";
			this.BtnAceptar.UseVisualStyleBackColor = true;
			this.BtnAceptar.Click += new System.EventHandler(this.BtnAceptar_Click);
			// 
			// Timer1
			// 
			this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
			// 
			// txtTiempo
			// 
			this.txtTiempo.Enabled = false;
			this.txtTiempo.Location = new System.Drawing.Point(131, 39);
			this.txtTiempo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtTiempo.MaxLength = 3;
			this.txtTiempo.Name = "txtTiempo";
			this.txtTiempo.Size = new System.Drawing.Size(145, 23);
			this.txtTiempo.TabIndex = 6;
			this.txtTiempo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtSucursal
			// 
			this.txtSucursal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtSucursal.Enabled = false;
			this.txtSucursal.Location = new System.Drawing.Point(131, 78);
			this.txtSucursal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.txtSucursal.MaxLength = 30;
			this.txtSucursal.Name = "txtSucursal";
			this.txtSucursal.Size = new System.Drawing.Size(145, 23);
			this.txtSucursal.TabIndex = 8;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnRemoto);
			this.groupBox1.Controls.Add(this.btnLocal);
			this.groupBox1.Controls.Add(this.BtnEditar);
			this.groupBox1.Controls.Add(this.txtSucursal);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.BtnAceptar);
			this.groupBox1.Controls.Add(this.txtTiempo);
			this.groupBox1.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(12, 6);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.groupBox1.Size = new System.Drawing.Size(893, 139);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Datos Generales";
			// 
			// btnRemoto
			// 
			this.btnRemoto.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Bold);
			this.btnRemoto.Location = new System.Drawing.Point(741, 54);
			this.btnRemoto.Name = "btnRemoto";
			this.btnRemoto.Size = new System.Drawing.Size(114, 34);
			this.btnRemoto.TabIndex = 15;
			this.btnRemoto.Text = "Test Remoto";
			this.btnRemoto.UseVisualStyleBackColor = true;
			this.btnRemoto.Click += new System.EventHandler(this.button2_Click);
			// 
			// btnLocal
			// 
			this.btnLocal.Location = new System.Drawing.Point(740, 19);
			this.btnLocal.Name = "btnLocal";
			this.btnLocal.Size = new System.Drawing.Size(115, 31);
			this.btnLocal.TabIndex = 14;
			this.btnLocal.Text = "Test Local";
			this.btnLocal.UseVisualStyleBackColor = true;
			this.btnLocal.Click += new System.EventHandler(this.button1_Click);
			// 
			// BtnEditar
			// 
			this.BtnEditar.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnEditar.Location = new System.Drawing.Point(631, 19);
			this.BtnEditar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.BtnEditar.Name = "BtnEditar";
			this.BtnEditar.Size = new System.Drawing.Size(103, 31);
			this.BtnEditar.TabIndex = 13;
			this.BtnEditar.Text = "Editar";
			this.BtnEditar.UseVisualStyleBackColor = true;
			this.BtnEditar.Click += new System.EventHandler(this.BtnEditar_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(106, 17);
			this.label2.TabIndex = 11;
			this.label2.Text = "Sincronización";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 81);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Sucursal";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.chkBoxProductos);
			this.groupBox2.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(12, 149);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.groupBox2.Size = new System.Drawing.Size(480, 100);
			this.groupBox2.TabIndex = 12;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Descargar Datos";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.chkBoxVentas);
			this.groupBox3.Controls.Add(this.chkBoxCortes);
			this.groupBox3.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox3.Location = new System.Drawing.Point(499, 149);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.groupBox3.Size = new System.Drawing.Size(405, 100);
			this.groupBox3.TabIndex = 13;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Cargar Datos";
			// 
			// chkBoxVentas
			// 
			this.chkBoxVentas.AutoSize = true;
			this.chkBoxVentas.Location = new System.Drawing.Point(5, 21);
			this.chkBoxVentas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.chkBoxVentas.Name = "chkBoxVentas";
			this.chkBoxVentas.Size = new System.Drawing.Size(78, 21);
			this.chkBoxVentas.TabIndex = 3;
			this.chkBoxVentas.Text = "Ventas";
			this.chkBoxVentas.UseVisualStyleBackColor = true;
			// 
			// chkBoxCortes
			// 
			this.chkBoxCortes.AutoSize = true;
			this.chkBoxCortes.Location = new System.Drawing.Point(5, 46);
			this.chkBoxCortes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.chkBoxCortes.Name = "chkBoxCortes";
			this.chkBoxCortes.Size = new System.Drawing.Size(76, 21);
			this.chkBoxCortes.TabIndex = 4;
			this.chkBoxCortes.Text = "Cortes";
			this.chkBoxCortes.UseVisualStyleBackColor = true;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(12, 254);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(900, 404);
			this.tabControl1.TabIndex = 14;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.dataGridView1);
			this.tabPage1.Location = new System.Drawing.Point(4, 25);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabPage1.Size = new System.Drawing.Size(892, 375);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Tareas";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(3, 2);
			this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersWidth = 51;
			this.dataGridView1.RowTemplate.Height = 24;
			this.dataGridView1.Size = new System.Drawing.Size(885, 370);
			this.dataGridView1.TabIndex = 12;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.logDataGridView);
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.tabPage2.Size = new System.Drawing.Size(892, 375);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Mensajes";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// logDataGridView
			// 
			this.logDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.logDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logDataGridView.Location = new System.Drawing.Point(3, 2);
			this.logDataGridView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.logDataGridView.Name = "logDataGridView";
			this.logDataGridView.RowHeadersWidth = 51;
			this.logDataGridView.RowTemplate.Height = 24;
			this.logDataGridView.Size = new System.Drawing.Size(886, 371);
			this.logDataGridView.TabIndex = 0;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(21, 663);
			this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(885, 28);
			this.progressBar1.TabIndex = 15;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(925, 713);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.MaximizeBox = false;
			this.Name = "Panel";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Sincroniador";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.tabPage2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.logDataGridView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.CheckBox chkBoxProductos;
		private System.Windows.Forms.Button BtnAceptar;
		private System.Windows.Forms.Timer Timer1;
		private System.Windows.Forms.TextBox txtTiempo;
		private System.Windows.Forms.TextBox txtSucursal;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.CheckBox chkBoxVentas;
		private System.Windows.Forms.CheckBox chkBoxCortes;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.DataGridView logDataGridView;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Button BtnEditar;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Button btnLocal;
		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.Button btnRemoto;
	}
}