using CRUDCORE.Models;
using System.Data.SqlClient;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CRUDCORE.Datos
{
    public class ContactoDatos
    {
        // Método para listar todos los contactos desde la base de datos
        public List<ContactoModel> ListarContactos()
        {
            // Inicializa una lista vacía para almacenar los contactos que se obtendrán de la base de datos
            var oLista = new List<ContactoModel>();

            // Crea una instancia de la clase Conexion, que obtiene la cadena de conexión a la base de datos
            var cn = new Conexion();

            // Bloque 'using' para asegurar que la conexión a la base de datos se cierre automáticamente después de su uso
            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                // Abre la conexión con la base de datos
                conexion.Open();

                // Crea un comando SQL para ejecutar el procedimiento almacenado "sp_Listar"
                SqlCommand cmd = new SqlCommand("sp_Listar", conexion);
                cmd.CommandType = CommandType.StoredProcedure;  // Indica que es un procedimiento almacenado

                // Ejecuta el comando y lee los resultados con un SqlDataReader
                using (var dr = cmd.ExecuteReader())
                {
                    // Recorre cada fila de los resultados devueltos por el procedimiento almacenado
                    while (dr.Read())
                    {
                        // Añade cada contacto leído a la lista 'oLista'
                        oLista.Add(new ContactoModel()
                        {
                            // Asigna los valores obtenidos de la base de datos a las propiedades del objeto ContactoModel
                            IdContacto = Convert.ToInt32(dr["IdContacto"]),  // Convierte el valor a entero
                            Nombre = dr["Nombre"].ToString(),                // Convierte el valor a string
                            Telefono = dr["Telefono"].ToString(),            // Convierte el valor a string
                            Correo = dr["Correo"].ToString()                 // Convierte el valor a string
                        });
                    }
                }
            }

            // Devuelve la lista completa de contactos obtenidos de la base de datos
            return oLista;
        }


        public ContactoModel GetContacto(int IdContacto)
        {
            var oContacto = new ContactoModel();

            var cn = new Conexion();

            using(var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener", conexion);
                cmd.Parameters.AddWithValue("Idcontacto",IdContacto);
                cmd.CommandType = CommandType.StoredProcedure;

                using(var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oContacto.IdContacto = Convert.ToInt32(dr["IdContacto"]);  // Convierte el valor a entero
                        oContacto.Nombre = dr["Nombre"].ToString();                // Convierte el valor a string
                        oContacto.Telefono = dr["Telefono"].ToString();            // Convierte el valor a string
                        oContacto.Correo = dr["Correo"].ToString();
                    }
                }
            }
            return oContacto;
        }


        public bool setGuardar(ContactoModel contactoModel)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Guardar", conexion);
                    cmd.Parameters.AddWithValue("Nombre", contactoModel.Nombre);
                    cmd.Parameters.AddWithValue("Telefono", contactoModel.Telefono);
                    cmd.Parameters.AddWithValue("Correo", contactoModel.Correo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                respuesta = false;
            }
            return respuesta;
        }


        public bool setEditar(ContactoModel contactoModel)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Editar", conexion);
                    cmd.Parameters.AddWithValue("IdContacto", contactoModel.IdContacto);
                    cmd.Parameters.AddWithValue("Nombre", contactoModel.Nombre);
                    cmd.Parameters.AddWithValue("Telefono", contactoModel.Telefono);
                    cmd.Parameters.AddWithValue("Correo", contactoModel.Correo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                respuesta = false;
            }
            return respuesta;
        }

        public bool Eliminar(int IdContacto)
        {
            bool respuesta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Eliminar", conexion);
                    cmd.Parameters.AddWithValue("IdContacto", IdContacto);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                respuesta = false;
            }
            return respuesta;
        }

    }
}
