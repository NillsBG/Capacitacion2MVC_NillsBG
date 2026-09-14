using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Modelo_ComboI.Repositorios
{
    public class RepositorioComboI : Repositorio
    {

        public DataTable obtenerDatos(string _tabla, string _campo1, string _campo2)
        {
            return obtenerDatos(_tabla, _campo1, _campo2, "estado");
        }

        public DataTable obtenerDatos(string _tabla, string _campo1, string _campo2, string _campoEstado)
        {
            string sql = "SELECT " + _campo1 + "," + _campo2 + " FROM " + _tabla + " WHERE " + _campoEstado + " = 1;";

            using (var conexion = ObtenerConexion())
            {
                OdbcCommand command = new OdbcCommand(sql, conexion);
                OdbcDataAdapter adaptador = new OdbcDataAdapter(command);
                DataTable dtDatos = new DataTable();
                conexion.Open();
                adaptador.Fill(dtDatos);
                return dtDatos;
            }
        }

   
        public DataTable obtenerTodos(string _tabla, string _campo1, string _campo2)
        {
            string sql = "SELECT " + _campo1 + "," + _campo2 + " FROM " + _tabla + ";";

            using (var conexion = ObtenerConexion())
            {
                OdbcCommand command = new OdbcCommand(sql, conexion);
                OdbcDataAdapter adaptador = new OdbcDataAdapter(command);
                DataTable dtDatos = new DataTable();
                conexion.Open();
                adaptador.Fill(dtDatos);
                return dtDatos;
            }
        }
    }
}