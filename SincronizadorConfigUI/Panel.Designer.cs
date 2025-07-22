namespace SincronizadorConfigUI
{
	partial class Panel
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
			this.chkBoxProductos.Location = new System.Drawing.Point(7, 17);
			this.chkBoxProductos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.chkBoxProductos.Name = "chkBoxProductos";
			this.chkBoxProductos.Size = new System.Drawing.Size(83, 17);
			this.chkBoxProductos.TabIndex = 1;
			this.chkBoxProductos.Text = "Productos";
			this.chkBoxProductos.UseVisualStyleBackColor = true;
			// 
			// BtnAceptar
			// 
			this.BtnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnAceptar.Location = new System.Drawing.Point(473, 44);
			this.BtnAceptar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.BtnAceptar.Name = "BtnAceptar";
			this.BtnAceptar.Size = new System.Drawing.Size(78, 28);
			this.BtnAceptar.TabIndex = 2;
			this.BtnAceptar.Text = "Sincronizar";
			this.BtnAceptar.UseVisualStyleBackColor = true;
			this.BtnAceptar.Click += new System.EventHandler(this.BtnAceptar_Click);
			// 
			// txtTiempo
			// 
			this.txtTiempo.Enabled = false;
			this.txtTiempo.Location = new System.Drawing.Point(98, 32);
			this.txtTiempo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.txtTiempo.MaxLength = 3;
			this.txtTiempo.Name = "txtTiempo";
			this.txtTiempo.Size = new System.Drawing.Size(110, 20);
			this.txtTiempo.TabIndex = 6;
			this.txtTiempo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// txtSucursal
			// 
			this.txtSucursal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			this.txtSucursal.Enabled = false;
			this.txtSucursal.Location = new System.Drawing.Point(98, 63);
			this.txtSucursal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.txtSucursal.MaxLength = 30;
			this.txtSucursal.Name = "txtSucursal";
			this.txtSucursal.Size = new System.Drawing.Size(110, 20);
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
			this.groupBox1.Location = new System.Drawing.Point(9, 5);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.groupBox1.Size = new System.Drawing.Size(670, 113);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Datos Generales";
			// 
			// btnRemoto
			// 
			this.btnRemoto.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Bold);
			this.btnRemoto.Location = new System.Drawing.Point(556, 44);
			this.btnRemoto.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnRemoto.Name = "btnRemoto";
			this.btnRemoto.Size = new System.Drawing.Size(86, 28);
			this.btnRemoto.TabIndex = 15;
			this.btnRemoto.Text = "Test Remoto";
			this.btnRemoto.UseVisualStyleBackColor = true;
			this.btnRemoto.Click += new System.EventHandler(this.btnRemoto_Click);
			// 
			// btnLocal
			// 
			this.btnLocal.Location = new System.Drawing.Point(555, 15);
			this.btnLocal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.btnLocal.Name = "btnLocal";
			this.btnLocal.Size = new System.Drawing.Size(86, 25);
			this.btnLocal.TabIndex = 14;
			this.btnLocal.Text = "Test Local";
			this.btnLocal.UseVisualStyleBackColor = true;
			this.btnLocal.Click += new System.EventHandler(this.btnLocal_Click);
			// 
			// BtnEditar
			// 
			this.BtnEditar.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnEditar.Location = new System.Drawing.Point(473, 15);
			this.BtnEditar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.BtnEditar.Name = "BtnEditar";
			this.BtnEditar.Size = new System.Drawing.Size(77, 25);
			this.BtnEditar.TabIndex = 13;
			this.BtnEditar.Text = "Editar";
			this.BtnEditar.UseVisualStyleBackColor = true;
			this.BtnEditar.Click += new System.EventHandler(this.BtnEditar_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 32);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 13);
			this.label2.TabIndex = 11;
			this.label2.Text = "Sincronización";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 66);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Sucursal";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.chkBoxProductos);
			this.groupBox2.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox2.Location = new System.Drawing.Point(9, 121);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.groupBox2.Size = new System.Drawing.Size(360, 81);
			this.groupBox2.TabIndex = 12;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Descargar Datos";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.chkBoxVentas);
			this.groupBox3.Controls.Add(this.chkBoxCortes);
			this.groupBox3.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox3.Location = new System.Drawing.Point(374, 121);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.groupBox3.Size = new System.Drawing.Size(304, 81);
			this.groupBox3.TabIndex = 13;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Cargar Datos";
			// 
			// chkBoxVentas
			// 
			this.chkBoxVentas.AutoSize = true;
			this.chkBoxVentas.Location = new System.Drawing.Point(4, 17);
			this.chkBoxVentas.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.chkBoxVentas.Name = "chkBoxVentas";
			this.chkBoxVentas.Size = new System.Drawing.Size(65, 17);
			this.chkBoxVentas.TabIndex = 3;
			this.chkBoxVentas.Text = "Ventas";
			this.chkBoxVentas.UseVisualStyleBackColor = true;
			// 
			// chkBoxCortes
			// 
			this.chkBoxCortes.AutoSize = true;
			this.chkBoxCortes.Location = new System.Drawing.Point(4, 37);
			this.chkBoxCortes.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.chkBoxCortes.Name = "chkBoxCortes";
			this.chkBoxCortes.Size = new System.Drawing.Size(63, 17);
			this.chkBoxCortes.TabIndex = 4;
			this.chkBoxCortes.Text = "Cortes";
			this.chkBoxCortes.UseVisualStyleBackColor = true;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Location = new System.Drawing.Point(9, 206);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(675, 328);
			this.tabControl1.TabIndex = 14;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.dataGridView1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.tabPage1.Size = new System.Drawing.Size(667, 302);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Tareas";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(2, 2);
			this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersWidth = 51;
			this.dataGridView1.RowTemplate.Height = 24;
			this.dataGridView1.Size = new System.Drawing.Size(664, 301);
			this.dataGridView1.TabIndex = 12;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.logDataGridView);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.tabPage2.Size = new System.Drawing.Size(667, 302);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Mensajes";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// logDataGridView
			// 
			this.logDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.logDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.logDataGridView.Location = new System.Drawing.Point(2, 2);
			this.logDataGridView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.logDataGridView.Name = "logDataGridView";
			this.logDataGridView.RowHeadersWidth = 51;
			this.logDataGridView.RowTemplate.Height = 24;
			this.logDataGridView.Size = new System.Drawing.Size(663, 298);
			this.logDataGridView.TabIndex = 0;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(16, 539);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(664, 23);
			this.progressBar1.TabIndex = 15;
			// 
			// Panel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(694, 579);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.MaximizeBox = false;
			this.Name = "Panel";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Distribuciones Zuñiga";
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