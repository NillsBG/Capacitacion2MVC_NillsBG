using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaControlador_prototipoumg2k26;

namespace CapaVista_prototipoumg2k26.Formas
{
    public partial class FrmReportes : Form
    {
        private ModeloReporte reporte = new ModeloReporte();

        public FrmReportes()
        {
            InitializeComponent();
            panIngresoDatos.Enabled = false;
        }

        private void FrmReportes_Load(object sender, EventArgs e)
        {
            listaReportes();
        }

        private void listaReportes()
        {
            try
            {
                dgvReportes.DataSource = reporte.GetAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            dgvReportes.DataSource = reporte.FindByNombre(txtSearch.Text);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            panIngresoDatos.Enabled = true;
            reporte.Estado = EstadoEntidad.Added;
            LimpiarCampos();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvReportes.SelectedRows.Count > 0)
            {
                panIngresoDatos.Enabled = true;
                reporte.Estado = EstadoEntidad.Modified;
                // Orden de columnas autogeneradas:
                // 0=IdPK, 1=NombreReporte, 2=TipoReporte, 3=FechaGeneracion, 4=DescripcionReporte, 5=IdUsuarioReporte
                reporte.IdPK = Convert.ToInt32(dgvReportes.CurrentRow.Cells[0].Value);
                txtNombre.Text = dgvReportes.CurrentRow.Cells[1].Value.ToString();
                txtTipo.Text = dgvReportes.CurrentRow.Cells[2].Value.ToString();
                txtDescripcion.Text = dgvReportes.CurrentRow.Cells[4].Value?.ToString() ?? string.Empty;
                txtIdUsuario.Text = dgvReportes.CurrentRow.Cells[5].Value.ToString();
            }
            else MessageBox.Show("Seleccione una fila");
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtIdUsuario.Text, out int idUsuario))
            {
                MessageBox.Show("El campo 'Id Usuario' debe ser numérico.");
                return;
            }

            reporte.NombreReporte = txtNombre.Text;
            reporte.TipoReporte = txtTipo.Text;
            reporte.DescripcionReporte = txtDescripcion.Text;
            reporte.IdUsuarioReporte = idUsuario;

            string resultado = reporte.GrabarCambios();
            MessageBox.Show(resultado);
            listaReportes();
            Reinicio();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (dgvReportes.SelectedRows.Count > 0)
            {
                reporte.Estado = EstadoEntidad.Deleted;
                reporte.IdPK = Convert.ToInt32(dgvReportes.CurrentRow.Cells[0].Value);
                string resultado = reporte.GrabarCambios();
                MessageBox.Show(resultado);
                listaReportes();
            }
            else MessageBox.Show("Seleccione una fila");
        }

        private void Reinicio()
        {
            panIngresoDatos.Enabled = false;
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtTipo.Clear();
            txtDescripcion.Clear();
            txtIdUsuario.Text = "1"; // valor por defecto: usuario "admin" de tus datos de prueba
        }
    }
}
