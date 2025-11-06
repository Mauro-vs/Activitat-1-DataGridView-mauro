using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Activitat_1_DataGridView_mauro
{
    public partial class FrmIngresos : Form
    {
        private Paciente pacienteActual;
        private List<Ingreso> Ingresos { get; set; } = new List<Ingreso>();

        public FrmIngresos(Paciente paciente, Ingreso ingresos)
        {
            InitializeComponent();

            pacienteActual = paciente;

            dtpAlta.Enabled = false;

            lblTitulo.Text = $"Ingresos del Paciente {pacienteActual.Nombre} {pacienteActual.Apellidos}";
        }
        
        private void FrmIngresos_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = pacienteActual.Ingresos;
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarIngreso();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            BorrarIngreso();
        }

        private void toolStripAGREGAR_Click(object sender, EventArgs e)
        {
            AgregarIngreso();
        }

        private void toolStripBORRAR_Click(object sender, EventArgs e)
        {
            BorrarIngreso();
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VolverMenuPrincipal();
        }


        private void VolverMenuPrincipal()
        {
            this.Close();
        }

        private void BorrarIngreso()
        {
            if (dataGridView1.CurrentRow.DataBoundItem != null)
            {
                var ingresoSelec = (Ingreso)dataGridView1.CurrentRow.DataBoundItem;
                // Confirmar el borrado
                var resultado = MessageBox.Show($"¿Está seguro que desea borrar el ingreso por {ingresoSelec.Motivo}?", "Confirmar borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultado == DialogResult.Yes)
                {
                    pacienteActual.Ingresos.Remove(ingresoSelec);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = pacienteActual.Ingresos;
                }
            }
        }

        private void AgregarIngreso()
        {
            if (string.IsNullOrEmpty(txtMotivo.Text) ||
                string.IsNullOrEmpty(txtEspecialidad.Text) ||
                string.IsNullOrEmpty(txtHabitacion.Text)) // Validar campos obligatorios
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (checkBoxAlta.Checked)
            {
                Ingreso nuevoIngreso = new Ingreso // Crear un nuevo ingreso con los datos del formulario
                {
                    FechaIngreso = dtpIngreso.Value,
                    FechaAlta = dtpAlta.Value,
                    Motivo = txtMotivo.Text,
                    Especialidad = txtEspecialidad.Text,
                    Habitacion = txtHabitacion.Text
                };

                pacienteActual.Ingresos.Add(nuevoIngreso); // Agregar el nuevo ingreso a la lista del paciente actual
            }
            else
            {
                Ingreso nuevoIngreso = new Ingreso
                {
                    FechaIngreso = dtpIngreso.Value,
                    Motivo = txtMotivo.Text,
                    Especialidad = txtEspecialidad.Text,
                    Habitacion = txtHabitacion.Text
                };

                pacienteActual.Ingresos.Add(nuevoIngreso); 
            }

            

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = pacienteActual.Ingresos;

            // Limpiar los campos del formulario
            dtpIngreso.Value = DateTime.Now;
            dtpAlta.Value = DateTime.Now;
            txtMotivo.Text = "";
            txtEspecialidad.Text = "";
            txtHabitacion.Text = "";
        }

        private void EditarIngreso()
        {
            if (dataGridView1.CurrentRow.DataBoundItem != null)
            {
                var ingresoSelec = (Ingreso)dataGridView1.CurrentRow.DataBoundItem;

                if (checkBoxAlta.Checked)
                {
                    // Actualizar los datos del ingreso seleccionado con los nuevos valores del formulario
                    ingresoSelec.Motivo = txtMotivo.Text;
                    ingresoSelec.Especialidad = txtEspecialidad.Text;
                    ingresoSelec.Habitacion = txtHabitacion.Text;
                    ingresoSelec.FechaIngreso = dtpIngreso.Value;
                    ingresoSelec.FechaAlta = dtpAlta.Value;
                }
                else
                {
                    ingresoSelec.Motivo = txtMotivo.Text;
                    ingresoSelec.Especialidad = txtEspecialidad.Text;
                    ingresoSelec.Habitacion = txtHabitacion.Text;
                    ingresoSelec.FechaIngreso = dtpIngreso.Value;
                }

                btnAgregar.Visible = true; // Oculta el botón de agregar
                btnAgregar.Enabled = true; // Deshabilita el botón de agregar
                btnEditar.Visible = false; // Muestra el botón de editar
                btnEditar.Enabled = false; // Habilita el botón de editar

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = pacienteActual.Ingresos;

                // Limpiar los campos del formulario
                dtpIngreso.Value = DateTime.Now;
                dtpAlta.Value = DateTime.Now;
                txtMotivo.Text = "";    
                txtEspecialidad.Text = "";
                txtHabitacion.Text = "";

            }
            else
            {
                MessageBox.Show("Seleccione un ingreso para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripEDITAR_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.DataBoundItem != null)
            {
                var ingresoSelec = (Ingreso)dataGridView1.CurrentRow.DataBoundItem; // Obtener el ingreso seleccionado

                // Rellenar los campos del formulario con los datos del ingreso seleccionado
                txtMotivo.Text = ingresoSelec.Motivo;
                txtEspecialidad.Text = ingresoSelec.Especialidad;
                txtHabitacion.Text = ingresoSelec.Habitacion;
                dtpIngreso.Value = ingresoSelec.FechaIngreso;
                dtpAlta.Value = ingresoSelec.FechaAlta ?? DateTime.Now;


                btnAgregar.Visible = false; // Oculta el botón de agregar
                btnAgregar.Enabled = false; // Deshabilita el botón de agregar
                btnEditar.Visible = true; // Muestra el botón de editar
                btnEditar.Enabled = true; // Habilita el botón de editar

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = pacienteActual.Ingresos;


            }
            else
            {
                MessageBox.Show("Seleccione un ingreso para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarIngreso();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = pacienteActual.Ingresos;
        }

        private void pacientesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dtpAlta_ValueChanged(object sender, EventArgs e)
        {
            if (checkBoxAlta.Checked)
            {
                dtpAlta.Enabled = true;
            }
            else
            {
                dtpAlta.Enabled = false;    
            }
        }

        private void checkBoxAlta_CheckedChanged(object sender, EventArgs e)
        {
            dtpAlta.Enabled = checkBoxAlta.Checked;
        }
    }
}
