using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo_prototipoumg2k26.Entidades
{
    public class Reporte
    {
        public int IdReporte { get; set; }
        public string NombreReporte { get; set; }
        public string TipoReporte { get; set; }
        public DateTime FechaGeneracion { get; set; } // la asigna la BD automáticamente (CURRENT_TIMESTAMP)
        public string DescripcionReporte { get; set; }
        public int IdUsuarioReporte { get; set; }
    }
}
