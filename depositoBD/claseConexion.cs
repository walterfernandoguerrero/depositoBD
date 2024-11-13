using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace depositoBD
{
    public class claseConexion
    {
         public static SqlConnection conn { get; set; }
        public static void abrirConexion()
        {
            string proveedor = "Data Source=.\\SQLEXPRESS;Initial Catalog = deposito; Integrated Security = True";
        conn = new SqlConnection(proveedor);
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void cerrarConexion()
        {
            conn.Close();
        }
        public static DataTable leerDatos(string query)
        {
            try
            {
                abrirConexion();
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand(query, conn);
                // cmd.CommandType = CommandType.Text;
                SqlDataReader lector;
                lector = cmd.ExecuteReader();
                dt.Load(lector);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cerrarConexion();
            }
        }

        public static void ejecutarSQL(string query)
        {
            try
            {
                abrirConexion();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cerrarConexion();
            }
        }
    }

}
