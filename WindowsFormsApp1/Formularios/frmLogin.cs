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

        string passwordBD;

        private void frmLogin_Load(object sender, EventArgs e)
        {
            InicializarFormulario("Login");
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            SalirConEscape(e);
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
            if (txtUsuario.Text.Trim() != string.Empty)
            {
                bool usuarioExiste = BuscarUsuario(txtUsuario.Text.Trim());

                if (!usuarioExiste)
                {
                    MessageBox.Show("El usuario no existe en el sistema",
                                  "Aviso del sistema",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
                    txtUsuario.Focus();
                    txtUsuario.Select();
                }
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
            if (txtUsuario.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Debe ingresar un nombre de usuario",
                               "Aviso del sistema",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Exclamation);
                txtUsuario.Focus();
                return;
            }

            if (txtPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Debe ingresar una contraseña",
                               "Aviso del sistema",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Exclamation);
                txtPassword.Focus();
                return;
            }

            if (string.IsNullOrEmpty(passwordBD))
            {
                MessageBox.Show("El usuario ingresado no existe",
                               "Aviso del sistema",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Warning);
                txtUsuario.Focus();
                return;
            }

            if (txtPassword.Text.Trim() == passwordBD)
            {
                this.Hide();
                frmMenu frm = new frmMenu();
                frm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta",
                               "Aviso del sistema",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                txtPassword.Focus();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit(); // cierra la aplicacion
        }

        //METODOS PERSONALIZADOS

        private bool BuscarUsuario(string Usuario)
        {
            string query = "SELECT CLAVE, SUCURSAL FROM USUARIO WHERE USERS = @A1";

            using (SqlConnection cxn = new SqlConnection(cnn.db))
            {
                cxn.Open();
                SqlCommand cmd = new SqlCommand(query, cxn);
                cmd.Parameters.AddWithValue("@A1", Usuario);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    passwordBD = rdr["CLAVE"].ToString();
                    cnn.miSucursal = rdr["SUCURSAL"].ToString();
                    return true; // Usuario encontrado
                }
                else
                {
                    return false; // Usuario NO encontrado
                }
            }
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
