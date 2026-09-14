using CapaModelo_prototipoumg2k26.Contratos;
using CapaModelo_prototipoumg2k26.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo_prototipoumg2k26.Repositorios
{
    public class RepositorioReporte : RepositorioMaestro, IRepositorioReporte
    {
        private string selectAll;
        private string insert;
        private string update;
        private string delete;

        public RepositorioReporte()
        {
            selectAll = "SELECT * FROM Tbl_Reporte";

            insert = "INSERT INTO Tbl_Reporte (NombreReporte, TipoReporte, DescripcionReporte, IdUsuario_Reporte) VALUES (?, ?, ?, ?)";
            update = "UPDATE Tbl_Reporte SET NombreReporte=?, TipoReporte=?, DescripcionReporte=?, IdUsuario_Reporte=? WHERE IdReporte=?";
            delete = "DELETE FROM Tbl_Reporte WHERE IdReporte=?";
        }

        public int Agregar(Reporte entidad)
        {
            var _parametros = new List<OdbcParameter>();
            _parametros.Add(new OdbcParameter("p_Nombre", entidad.NombreReporte));
            _parametros.Add(new OdbcParameter("p_Tipo", entidad.TipoReporte));
            _parametros.Add(new OdbcParameter("p_Descripcion", (object)entidad.DescripcionReporte ?? DBNull.Value));
            _parametros.Add(new OdbcParameter("p_IdUsuario", entidad.IdUsuarioReporte));

            return EjecucionNonQuery(insert, _parametros, CommandType.Text);
        }

        public int Editar(Reporte entidad)
        {
            var _parametros = new List<OdbcParameter>();
            _parametros.Add(new OdbcParameter("p_Nombre", entidad.NombreReporte));
            _parametros.Add(new OdbcParameter("p_Tipo", entidad.TipoReporte));
            _parametros.Add(new OdbcParameter("p_Descripcion", (object)entidad.DescripcionReporte ?? DBNull.Value));
            _parametros.Add(new OdbcParameter("p_IdUsuario", entidad.IdUsuarioReporte));
            _parametros.Add(new OdbcParameter("p_IdReporte", entidad.IdReporte));

            return EjecucionNonQuery(update, _parametros, CommandType.Text);
        }

        public int Remover(Reporte entidad)
        {
            var _parametros = new List<OdbcParameter>();
            _parametros.Add(new OdbcParameter("p_IdReporte", entidad.IdReporte));

            return EjecucionNonQuery(delete, _parametros, CommandType.Text);
        }

        public IEnumerable<Reporte> GetAll()
        {
            var lstReporte = new List<Reporte>();
            var tblTabla = EjecucionConsulta(selectAll, CommandType.Text);
            foreach (DataRow row in tblTabla.Rows)
            {
                var reporte = new Reporte();
                reporte.IdReporte = Convert.ToInt32(row["IdReporte"]);
                reporte.NombreReporte = row["NombreReporte"].ToString();
                reporte.TipoReporte = row["TipoReporte"].ToString();
                reporte.FechaGeneracion = Convert.ToDateTime(row["FechaGeneracion"]);
                reporte.DescripcionReporte = row["DescripcionReporte"] == DBNull.Value ? string.Empty : row["DescripcionReporte"].ToString();
                reporte.IdUsuarioReporte = Convert.ToInt32(row["IdUsuario_Reporte"]);
                lstReporte.Add(reporte);
            }
            tblTabla.Clear();
            tblTabla = null;
            return lstReporte;
        }
    }
}
