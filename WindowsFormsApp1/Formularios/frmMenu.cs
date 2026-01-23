using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            InicializarFormulario("Menu");

        }

        private void frmMenu_KeyDown(object sender, KeyEventArgs e)
        {
            SalirConEscape(e);
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToShortDateString();
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void InicializarFormulario(string titulo)
        {
            this.KeyPreview = true;
            this.Text = titulo;
            timer1.Enabled = true;
        }

        protected void SalirConEscape(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }

    }
}
