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
	public partial class Panel : Form
	{
		public Panel()
		{
			InitializeComponent();
		}

		private void BtnEditar_Click(object sender, EventArgs e)
		{
			Form1 Form1Form = new Form1();
			Form1Form.ShowDialog();
		}

		private void BtnAceptar_Click(object sender, EventArgs e)
		{

		}

		private void btnLocal_Click(object sender, EventArgs e)
		{

		}

		private void btnRemoto_Click(object sender, EventArgs e)
		{

		}
	}
}
