using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Clases;

namespace WindowsFormsApp1
{
    public class Busco
    {
        public static string BuscaPuestoDeTrabajo(string _posicion, out bool found)
        {
            found = false;

            using (SqlConnection cxn = new SqlConnection(cnn.db))
            {
                cxn.Open();
                string query = "SELECT POSICION FROM USUARIO WHERE POSICION = @A1";

                SqlCommand cmd = new SqlCommand(query, cxn);
                cmd.Parameters.AddWithValue("@A1", _posicion);
                SqlDataReader dr = cmd.ExecuteReader();

                if(dr.Read())
                {
                    found = true;
                    return dr["POSICION"].ToString();
                }
            }
            return null;
        }

        public static string BuscaSucursal(string _sucursal, out bool found)
        {
            found = false;

            using (SqlConnection cxn = new SqlConnection(cnn.db))
            {
                cxn.Open();
                string query = "SELECT SUCURSAL FROM USUARIO WHERE SUCURSAL = @A1";

                SqlCommand cmd = new SqlCommand(query, cxn);
                cmd.Parameters.AddWithValue("@A1", _sucursal);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    found = true;
                    return dr["SUCURSAL"].ToString();
                }
            }
            return null;
        }
     
    }
}
