using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Controlador_ComboI;

namespace Capa_Vista_ComboI
{
    public partial class ComboI : UserControl
    {
        private ModeloComboI modelo = new ModeloComboI();

        public ComboI()
        {
            InitializeComponent();
        }

        //==========================
        // COMBO "INTELIGENTE"
        //==========================
        // Versión original: asume que la columna de estado se llama "estado".
        // Ejemplo: comboI1.llenarCombo("tbl_empleadospuestos", "codigo_empleado", "puesto");
        public void llenarCombo(string tabla, string campoValor, string campoTexto)
        {
            LlenarInterno(() => modelo.ObtenerDatos(tabla, campoValor, campoTexto), tabla, campoValor, campoTexto);
        }

        // Nueva versión: recibe también el nombre real de la columna de estado,
        // necesaria cuando la tabla no usa literalmente "estado" (como en
        // sistema_polideportivo: EstadoUsuario, EstadoEquipo, etc.)
        // Ejemplo: comboI1.llenarCombo("Tbl_Usuario", "IdUsuario", "NombreUsuario", "EstadoUsuario");
        public void llenarCombo(string tabla, string campoValor, string campoTexto, string campoEstado)
        {
            LlenarInterno(() => modelo.ObtenerDatos(tabla, campoValor, campoTexto, campoEstado), tabla, campoValor, campoTexto);
        }

        // Para tablas sin columna de estado, como Tbl_Reporte.
        // Ejemplo: comboI1.llenarComboSinFiltro("Tbl_Reporte", "IdReporte", "NombreReporte");
        public void llenarComboSinFiltro(string tabla, string campoValor, string campoTexto)
        {
            LlenarInterno(() => modelo.ObtenerTodos(tabla, campoValor, campoTexto), tabla, campoValor, campoTexto);
        }

        private void LlenarInterno(Func<DataTable> obtenerDatos, string tabla, string campoValor, string campoTexto)
        {
            try
            {
                DataTable tablaDatos = obtenerDatos();

                cboPrueba.DataSource = tablaDatos;
                cboPrueba.ValueMember = campoValor;
                cboPrueba.DisplayMember = campoTexto;
                cboPrueba.SelectedIndex = -1; // que no quede nada preseleccionado
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el combo (" + tabla + "): " + ex.Message);
            }
        }

        //==========================
        // AYUDAS PARA LEER/ESTABLECER LA SELECCIÓN
        //==========================
        [Browsable(false)]
        public object ValorSeleccionado => cboPrueba.SelectedValue;

        [Browsable(false)]
        public string TextoSeleccionado => cboPrueba.Text;

        public void Seleccionar(object valor)
        {
            if (cboPrueba.DataSource != null)
            {
                cboPrueba.SelectedValue = valor;
            }
        }

        public void Limpiar()
        {
            cboPrueba.SelectedIndex = -1;
        }
    }
}