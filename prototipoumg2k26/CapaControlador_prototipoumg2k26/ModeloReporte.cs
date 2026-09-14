using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo_prototipoumg2k26.Contratos;
using CapaModelo_prototipoumg2k26.Entidades;
using CapaModelo_prototipoumg2k26.Repositorios;

namespace CapaControlador_prototipoumg2k26
{
    public class ModeloReporte
    {
        private int _idPK;
        private string _nombreReporte;
        private string _tipoReporte;
        private DateTime _fechaGeneracion; // solo lectura: la asigna la BD
        private string _descripcionReporte;
        private int _idUsuarioReporte;
        private IRepositorioReporte RepositorioReporte;

        public EstadoEntidad Estado { private get; set; }
        private List<ModeloReporte> ListaReportes;

        public int IdPK { get => _idPK; set => _idPK = value; }
        public string NombreReporte { get => _nombreReporte; set => _nombreReporte = value; }
        public string TipoReporte { get => _tipoReporte; set => _tipoReporte = value; }
        public DateTime FechaGeneracion { get => _fechaGeneracion; private set => _fechaGeneracion = value; }
        public string DescripcionReporte { get => _descripcionReporte; set => _descripcionReporte = value; }
        public int IdUsuarioReporte { get => _idUsuarioReporte; set => _idUsuarioReporte = value; }

        public ModeloReporte()
        {
            RepositorioReporte = new RepositorioReporte();
        }

        public string GrabarCambios()
        {
            string mensaje = null;
            try
            {
                var modeloDatosReporte = new Reporte();
                modeloDatosReporte.IdReporte = _idPK;
                modeloDatosReporte.NombreReporte = _nombreReporte;
                modeloDatosReporte.TipoReporte = _tipoReporte;
                modeloDatosReporte.DescripcionReporte = _descripcionReporte;
                modeloDatosReporte.IdUsuarioReporte = _idUsuarioReporte;

                switch (Estado)
                {
                    case EstadoEntidad.Added:
                        RepositorioReporte.Agregar(modeloDatosReporte);
                        mensaje = "Grabacion exitosa";
                        break;
                    case EstadoEntidad.Modified:
                        RepositorioReporte.Editar(modeloDatosReporte);
                        mensaje = "Actualizacion exitosa";
                        break;
                    case EstadoEntidad.Deleted:
                        RepositorioReporte.Remover(modeloDatosReporte);
                        mensaje = "Eliminacion exitosa";
                        break;
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.ToString();
            }
            return mensaje;
        }

        public List<ModeloReporte> GetAll()
        {
            var modeloDatosReportes = RepositorioReporte.GetAll();
            ListaReportes = new List<ModeloReporte>();
            foreach (Reporte item in modeloDatosReportes)
            {
                ListaReportes.Add(new ModeloReporte
                {
                    _idPK = item.IdReporte,
                    _nombreReporte = item.NombreReporte,
                    _tipoReporte = item.TipoReporte,
                    _fechaGeneracion = item.FechaGeneracion,
                    _descripcionReporte = item.DescripcionReporte,
                    _idUsuarioReporte = item.IdUsuarioReporte
                });
            }
            return ListaReportes;
        }

        public IEnumerable<ModeloReporte> FindByNombre(string filter)
        {
            return ListaReportes.FindAll(r => r._nombreReporte.Contains(filter) || r._tipoReporte.Contains(filter));
        }


        public IEnumerable<ModeloReporte> FindByUsuario(int idUsuario)
        {
            return ListaReportes.FindAll(r => r._idUsuarioReporte == idUsuario);
        }
    }
}