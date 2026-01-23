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
using WindowsFormsApp1.Clases; // libreria para sql server

namespace WindowsFormsApp1
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        string password;

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true; //activamos la tecla de funciones
            this.Text = "Login";

            timer1.Enabled = true; // activamos el timer
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape) // si presionamos la tecla ESCAPE entonces va ejecutar la salida de la aplicacion
            {
                Application.Exit(); // cierra la aplicacion
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToShortDateString();
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((int)e.KeyChar == (int)Keys.Enter)
            {
                e.Handled = true;
                if(txtUsuario.Text.Trim() != string.Empty)
                {
                    txtPassword.Focus();
                }
                else
                {
                    MessageBox.Show("Debe ingresar un nombre de usuario", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUsuario.Focus();
                }
            }
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if(txtUsuario.Text.Trim() != string.Empty)
            {
                BuscarUsuario(txtUsuario.Text.Trim());
            }

        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                e.Handled = true;
                if (txtPassword.Text.Trim() != string.Empty)
                {
                    btnEntrar.Focus();
                }
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim() != string.Empty)
            {
                btnEntrar.PerformClick();
            }

        }

        //EVENTOS DE LOS BOTONES

        private void btnEntrar_Click(object sender, EventArgs e)
        
        {
            if(txtUsuario.Text.Trim() != string.Empty)
            {
                if(txtPassword.Text.Trim() != string.Empty)
                {
                    if(txtPassword.Text.Trim() == password)
                    {
                        this.Hide();
                        frmMenu frm = new frmMenu();
                        frm.ShowDialog();

                        txtUsuario.Text = string.Empty;
                        txtPassword.Text = string.Empty;
                        txtUsuario.Focus();
                        this.Show();
                    }
                }
            }

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit(); // cierra la aplicacion
        }

        //METODOS PERSONALIZADOS

        private void BuscarUsuario(string Usurio)
        {
            string query = "SELECT CLAVE , SUCURSAL FROM USUARIO WHERE USERS = @A1";

            using (SqlConnection cxn = new SqlConnection(cnn.db))
            {
                cxn.Open();
                SqlCommand cmd = new SqlCommand(query, cxn);
                cmd.Parameters.AddWithValue("@A1", Usurio);
                SqlDataReader rdr = cmd.ExecuteReader();

                if(rdr.Read())
                {
                    password = rdr["CLAVE"].ToString();
                    cnn.miSucursal = rdr["SUCURSAL"].ToString();
                }
            }
        }
    }
}
