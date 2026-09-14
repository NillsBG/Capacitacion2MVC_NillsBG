using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Modelo_ComboI.Repositorios;

namespace Capa_Controlador_ComboI
{
    public class ModeloComboI
    {
        private RepositorioComboI RepositorioComboI;

        public ModeloComboI()
        {
            RepositorioComboI = new RepositorioComboI();
        }

     
        public DataTable ObtenerDatos(string tabla, string campoValor, string campoTexto)
        {
            return RepositorioComboI.obtenerDatos(tabla, campoValor, campoTexto);
        }


        public DataTable ObtenerDatos(string tabla, string campoValor, string campoTexto, string campoEstado)
        {
            return RepositorioComboI.obtenerDatos(tabla, campoValor, campoTexto, campoEstado);
        }


        public DataTable ObtenerTodos(string tabla, string campoValor, string campoTexto)
        {
            return RepositorioComboI.obtenerTodos(tabla, campoValor, campoTexto);
        }
    }
}