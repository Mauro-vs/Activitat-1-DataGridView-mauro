using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Activitat_1_DataGridView_mauro
{
    public partial class FrmPaciente : Form
    {
        private List<Paciente> listPacientes;
        private Paciente pacienteActual;
        private IngresosHospitalarios frmPrincipal;
        public FrmPaciente(List<Paciente> pacientes, Paciente pacienteSelec, IngresosHospitalarios frmPrincipal)
        {
            InitializeComponent();

            listPacientes = pacientes;

            this.frmPrincipal = frmPrincipal;

            if (pacienteSelec != null)
            {
                pacienteActual = pacienteSelec; // Asigna el paciente seleccionado a pacienteActuals

                txtNombre.Text = pacienteActual.Nombre; // Muestra en el TextBox el nombre del paciente seleccionado
                txtApellido.Text = pacienteActual.Apellidos;
                txtEdad.Text = pacienteActual.Edad.ToString();

                btnAgregar.Visible = false; // Oculta el botón de agregar
                btnAgregar.Enabled = false; // Deshabilita el botón de agregar
                btnEditar.Visible = true; // Muestra el botón de editar
                btnEditar.Enabled = true; // Habilita el botón de editar

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = listPacientes;

            }
            else
            {
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = listPacientes;
            }


        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarPaciente();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            BorrarPaciente();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripAGREGAR_Click(object sender, EventArgs e)
        {
            // Limpiar los campos de texto antes de la validación
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEdad.Text = "";

            // Habilitar botones
            btnAgregar.Visible = true; // Muestra el botón de agregar
            btnAgregar.Enabled = true; // Habilita el botón de agregar
            btnEditar.Visible = false; // Oculta el botón de editar
            btnEditar.Enabled = false; // Deshabilita el botón de editar
        }

        private void toolStripEDITAR_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.DataBoundItem != null)
            {
                var pacienteSelec = (Paciente)dataGridView1.CurrentRow.DataBoundItem; // Obtiene el paciente seleccionado
                if (pacienteSelec != null)
                { 
                    txtNombre.Text = pacienteSelec.Nombre; // Muestra en el TextBox el nombre del paciente seleccionado
                    txtApellido.Text = pacienteSelec.Apellidos;
                    txtEdad.Text = pacienteSelec.Edad.ToString();

                    pacienteActual = pacienteSelec; // Asigna el paciente seleccionado a pacienteActual

                    btnAgregar.Visible = false; // Oculta el botón de agregar
                    btnAgregar.Enabled = false; // Deshabilita el botón de agregar
                    btnEditar.Visible = true; // Muestra el botón de editar
                    btnEditar.Enabled = true; // Habilita el botón de editar

                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = listPacientes;
                }
                frmPrincipal.Actualizar(listPacientes);
            }
        }

        private void ingresosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirIngresos();
            frmPrincipal.Actualizar(listPacientes);
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VolverAlMenu();
            frmPrincipal.Actualizar(listPacientes);
        }

        private void AbrirIngresos()
        {
            if (dataGridView1.CurrentRow.DataBoundItem != null)
            {
                var pacienteSelec = (Paciente)dataGridView1.CurrentRow.DataBoundItem; // Obtiene el paciente seleccionado
                if (pacienteSelec != null)
                {
                    var frm = new FrmIngresos(pacienteSelec, null);
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Seleccione un paciente válido para ver sus ingresos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un paciente para ver sus ingresos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VolverAlMenu()
        {
            this.Close();
        }

        private void AgregarPaciente()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtEdad.Text))

            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {
                string nombre = txtNombre.Text;
                string apellido = txtApellido.Text;
                int edad;
                if (!int.TryParse(txtEdad.Text, out edad) || edad < 0)
                {
                    MessageBox.Show("Por favor, ingrese una edad válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                listPacientes.Add(new Paciente
                {
                    Nombre = nombre,
                    Apellidos = apellido,
                    Edad = edad
                });

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = listPacientes;

                txtNombre.Text = "";
                txtApellido.Text = "";
                txtEdad.Text = "";

                if (this.frmPrincipal != null)
                {
                    this.frmPrincipal.Actualizar(listPacientes);
                }
            }
        }

        private void BorrarPaciente()
        {
            if (dataGridView1.CurrentRow != null)
            {
                // "CurrentRow" representa la fila seleccionada por el usuario.
                // "DataBoundItem" es el objeto original (en este caso, un Paciente) que está enlazado a esa fila.
                var pacienteSelec = (Paciente)dataGridView1.CurrentRow.DataBoundItem;
                if (pacienteSelec != null)
                {
                    var confirmResult = MessageBox.Show($"¿Estás seguro de que deseas borrar al paciente {pacienteSelec.Nombre} {pacienteSelec.Apellidos}?",
                                                         "Confirmar borrado",
                                                         MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.Yes)
                    {
                        listPacientes.Remove(pacienteSelec);
                        dataGridView1.DataSource = null;
                        dataGridView1.DataSource = listPacientes;
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un paciente para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            EditarPaciente();
        }

        private void EditarPaciente()
        {
            if (dataGridView1.CurrentRow != null)
            {
                //var pacienteSelec = (Paciente)dataGridView1.CurrentRow.DataBoundItem;
                pacienteActual.Nombre = txtNombre.Text;
                pacienteActual.Apellidos = txtApellido.Text;
                int edad;
                if (!int.TryParse(txtEdad.Text, out edad) || edad < 0)
                {
                    MessageBox.Show("Por favor, ingrese una edad válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                pacienteActual.Edad = edad;

                pacienteActual.Nombre = txtNombre.Text;
                pacienteActual.Apellidos = txtApellido.Text;
                pacienteActual.Edad = edad;

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = listPacientes;

                txtNombre.Clear();
                txtApellido.Clear();
                txtEdad.Clear();

                btnAgregar.Visible = true;
                btnAgregar.Enabled = true;
                btnEditar.Visible = false;
                btnEditar.Enabled = false;
            }
            else
            {
                MessageBox.Show("Seleccione un paciente para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
