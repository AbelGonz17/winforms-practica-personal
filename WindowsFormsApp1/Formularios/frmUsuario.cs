using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class frmUsuario : Form
    {
        public frmUsuario()
        {
            InitializeComponent();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true; //activamos la tecla de funciones
            this.Text = "Usuario";
        }

        private void frmUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) // si presionamos la tecla ESCAPE entonces va ejecutar la salida de la aplicacion
            {
                Application.Exit();
            }
        }

        //txtUsuario

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            

        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {

        }

        //txtPasword

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {

        }

        //txtNombre

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {

        }

        //txtCorreo

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtCorreo_Leave(object sender, EventArgs e)
        {

        }

        //txtPosicion

        private void txtPosicion_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtPosicion_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtPosicion_Leave(object sender, EventArgs e)
        {

        }

        //txtSucursal

        private void txtSucursal_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtSucursal_Leave(object sender, EventArgs e)
        {

        }

        private void txtSucursal_KeyDown(object sender, KeyEventArgs e)
        {

        }

        //------------------------------------------
        //BOTONES DE VENTANAS CONSULTAS
        //------------------------------------------

        private void btnUsuario_Click(object sender, EventArgs e)
        {

        }

        private void btnPosicion_Click(object sender, EventArgs e)
        {

        }

        private void btnSucursal_Click(object sender, EventArgs e)
        {

        }

        //------------------------------------------
        //BOTONES DE EJECUCION
        //------------------------------------------

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        




    }
}
