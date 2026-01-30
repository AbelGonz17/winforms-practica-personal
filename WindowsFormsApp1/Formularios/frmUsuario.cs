using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Clases;

namespace WindowsFormsApp1
{
    public partial class frmUsuario : Form
    {
        public frmUsuario()
        {
            InitializeComponent();
        }

        bool ExisteData;

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true; //activamos la tecla de funciones
            this.Text = "Usuario";

            picUsuario.Image = Image.FromFile(Ruta.imagenDefault);
            ExisteData = false;
        }

        private void frmUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        //txtUsuario

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {

            }
        }

        private void txtUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((int)e.KeyChar == (int) Keys.Enter)
            {
                e.Handled = true; 
                if(txtUsuario.Text.Trim() != string.Empty)
                {
                    txtPassword.Focus();
                }
            }          
        }

        private void txtUsuario_Leave(object sender, EventArgs e)
        {
            if (txtUsuario.Text.Trim() != string.Empty)
            {
                BuscarUsuario(txtUsuario.Text.Trim());
            }
        }

        //txtPasword

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                e.Handled = true;
                if (txtPassword.Text.Trim() != string.Empty)
                {
                    txtNombre.Focus();
                }
            }
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim() != string.Empty)
            {
                txtPassword.Focus();
            }

        }

        //txtNombre

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                e.Handled = true;
                if (txtNombre.Text.Trim() != string.Empty)
                {
                    txtCorreo.Focus();
                }
            }
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            if (txtNombre.Text.Trim() != string.Empty)
            {
                txtNombre.Focus();
            }
        }

        //txtCorreo

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                e.Handled = true;
                if (txtCorreo.Text.Trim() != string.Empty)
                {
                    txtPosicion.Focus();
                }
            }
        }

        private void txtCorreo_Leave(object sender, EventArgs e)
        {
           
            if (txtCorreo.Text.Trim() != string.Empty)
            {
                txtCorreo.Focus();
            }
        }

        //txtPosicion

        private void txtPosicion_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtPosicion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                e.Handled = true;
                if (txtPosicion.Text.Trim() != string.Empty)
                {
                    txtSucursal.Focus();
                }
            }
        }

        private void txtPosicion_Leave(object sender, EventArgs e)
        {
            if (txtPosicion.Text.Trim() != string.Empty)
            {
                lblPosicion.Text = Busco.BuscaPuestoDeTrabajo(txtPosicion.Text, out bool found);

                if (!found)
                {
                    MessageBox.Show("La posicion digitada no existe", "PVTA",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    txtPosicion.Clear();
                    lblPosicion.Text = "";
                    txtPosicion.Focus();
                }
            }
        }

        //txtSucursal

        private void txtSucursal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                e.Handled = true;
                if (txtSucursal.Text.Trim() != string.Empty)
                {
                    btnGuardar.Focus();
                }
            }
        }

        private void txtSucursal_Leave(object sender, EventArgs e)
        {
            if (txtSucursal.Text.Trim() != string.Empty)
            {
                lblSucursal.Text = Busco.BuscaSucursal(txtSucursal.Text, out bool found);

                if (!found)
                {
                    MessageBox.Show("La sucursal digitada no existe", "PVTA",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    txtSucursal.Clear();
                    lblSucursal.Text = "";
                    txtSucursal.Focus();
                }
            }
        }

        private void txtSucursal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {

            }
                   
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
            LimpiarFormulario();
            txtUsuario.Focus(); 
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        //--------------------------------------
        //Metodos
        //--------------------------------------

        private void LimpiarFormulario()
        {
            lblSecuencia.Text = "";
            txtUsuario.Clear();
            txtPassword.Clear();
            txtNombre.Clear();
            txtCorreo.Clear();
            txtPosicion.Clear();
            lblPosicion.Text = "";
            txtSucursal.Clear();
            lblSucursal.Text = "";
            txtDocumento.Clear();
            ExisteData = false;

            picUsuario.Image = null;
            picUsuario.Image = Image.FromFile(Ruta.imagenDefault);
        }

        private void BuscarUsuario(string _usuario)
        {
            LimpiarFormulario();

            using (SqlConnection cxn = new SqlConnection(cnn.db))
            {
                cxn.Open();
                string query = @"SELECT T1.ID,      T1.USERS,   T1.CLAVE,
                                        T1.NOMBRE,  T1.CORREO,  T1.POSICION,
                                        T1.SUCURSAL,T1.DOCUMENTO,
                                        T2.NOMBREDEPOSICION,
                                        T3.NOMBREDESUCURSAL
                                     FROM USUARIO T1
                                 INNER JOIN POSICIONES T2 ON T1.POSICION = T2.POSICIONES
                                 INNER JOIN SUCURSALES T3 ON T1.SUCURSAL = T3.SUCURSAL
                                     WHERE USERS = @A1";

                SqlCommand cmd = new SqlCommand(query, cxn);
                cmd.Parameters.AddWithValue("@A1", _usuario);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    ExisteData = true;

                    lblSecuencia.Text = dr["ID"].ToString();
                    txtUsuario.Text = dr["USERS"].ToString();
                    txtPassword.Text = dr["CLAVE"].ToString();
                    txtNombre.Text = dr["NOMBRE"].ToString();
                    txtCorreo.Text = dr["CORREO"].ToString();
                    txtPosicion.Text = dr["POSICION"].ToString();
                    txtSucursal.Text = dr["SUCURSAL"].ToString();
                    txtDocumento.Text = dr["DOCUMENTO"].ToString();

                    try
                    {
                        picUsuario.Image = ConvertImage.ByteArrayToImage((byte[])dr["IMAGEN"]);

                    }
                    catch
                    {
                        picUsuario.Image = Image.FromFile(Ruta.imagenDefault);
                    }
                }
            }
        }

        private void picUsuario_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string _imagen = openFileDialog1.FileName;
                picUsuario.Image = Image.FromFile(_imagen);
            }
        }
    }
}
