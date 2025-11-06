using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Activitat_1_DataGridView_mauro
{
    public partial class IngresosHospitalarios : Form
    {

        private void IngresosHospitalarios_Load(object sender, EventArgs e)
        {
        }

        // Lista con datos de ejemplo
        private List<Paciente> Pacientes { get; set; } = new List<Paciente> {
            new Paciente {
                Nombre = "Ana",
                Apellidos = "García López",
                Edad = 32,
                Ingresos = new List<Ingreso> {
                    new Ingreso {
                        FechaIngreso = new DateTime(2023, 1, 15, 9, 30, 0), // 15 de enero de 2023 a las 09:30:00
                        FechaAlta = new DateTime(2023, 1, 25, 17, 45, 0),   // 25 de enero de 2023 a las 17:45:00
                        Motivo = "Apendicitis",
                        Especialidad = "Cirugía",
                        Habitacion = "101A"
                    },
                    new Ingreso {
                        FechaIngreso = new DateTime(2024, 4, 10, 8, 15, 0),
                        FechaAlta = null,
                        Motivo = "Neumonía",
                        Especialidad = "Neumología",
                        Habitacion = "202B"
                    }
                }
            },
            new Paciente {
                Nombre = "Luis",
                Apellidos = "Martínez Pérez",
                Edad = 45,
                Ingresos = new List<Ingreso> {
                    new Ingreso {
                        FechaIngreso = new DateTime(2024, 5, 5, 10, 0, 0),
                        FechaAlta = new DateTime(2024, 5, 15, 18, 30, 0),
                        Motivo = "Fractura de pierna",
                        Especialidad = "Traumatología",
                        Habitacion = "303C"
                    }
                }
            },
            new Paciente {
                Nombre = "Marta",
                Apellidos = "Sánchez Ruiz",
                Edad = 27,
                Ingresos = new List<Ingreso> {
                    new Ingreso {
                        FechaIngreso = new DateTime(2024, 2, 20, 6, 45, 0),
                         FechaAlta = new DateTime(2024, 3, 1, 11, 20, 0),
                         Motivo = "Parto",
                         Especialidad = "Obstetricia",
                         Habitacion = "404D"
                    },
                    new Ingreso {
                        FechaIngreso = new DateTime(2024, 6, 12, 14, 10, 0),
                        FechaAlta = null,
                        Motivo = "Migraña severa",
                        Especialidad = "Neurología",
                        Habitacion = "505E"
                    },
                    new Ingreso {
                        FechaIngreso = new DateTime(2023, 11, 8, 16, 0, 0), 
                        FechaAlta = new DateTime(2023, 11, 15, 10, 30, 0),   
                        Motivo = "Alergia severa",
                        Especialidad = "Alergología",
                        Habitacion = "606F"
                    }
                }
            },
          };
        public IngresosHospitalarios()
        {
            InitializeComponent();
            
            RefrescarDatos();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pacientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirPaciente();
            Actualizar(Pacientes);
        }

        private void ingresosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirIngresos();
            Actualizar(Pacientes);
        }

        private void toolStripAGREGAR_Click(object sender, EventArgs e)
        {
            AbrirPaciente();
            Actualizar(Pacientes);
        }

        private void toolStripBORRAR_Click(object sender, EventArgs e)
        {
            BorrarPaciente();
            Actualizar(Pacientes);
        }

        private void toolStripEDITAR_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                // Recoge el paciente seleccionado en el DataGridView
                var pacienteSelec = dataGridView1.CurrentRow.DataBoundItem as Paciente;

                if (pacienteSelec != null)
                {
                    AbrirPacienteEditar(pacienteSelec.Id);
                    Actualizar(Pacientes);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un paciente para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripLISTAR_Click(object sender, EventArgs e)
        {
            AbrirIngresos();
            Actualizar(Pacientes);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AbrirPaciente();
            Actualizar(Pacientes);
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            BorrarPaciente();
            Actualizar(Pacientes);
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            AbrirIngresos();
            Actualizar(Pacientes);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                // Recoge el paciente seleccionado en el DataGridView
                var pacienteSelec = dataGridView1.CurrentRow.DataBoundItem as Paciente;

                if (pacienteSelec != null)
                {
                    AbrirPacienteEditar(pacienteSelec.Id);
                    Actualizar(Pacientes);
                }
            }
            else
            {
                MessageBox.Show("Seleccione un paciente para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AbrirPaciente()
        {
            var frm = new FrmPaciente(Pacientes, null, this);
            frm.ShowDialog();
        }

        private void AbrirPacienteEditar(int id)
        {
            if (dataGridView1.CurrentRow.DataBoundItem != null)
            {
                // Crea la variable pacienteSelec y se lo pasa a FrmPaciente para editarlo
                Paciente pacienteSelec = (Paciente)dataGridView1.CurrentRow.DataBoundItem;
                if (pacienteSelec != null)
                {
                    var frm = new FrmPaciente(Pacientes, pacienteSelec, this);
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un paciente para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AbrirIngresos()
        {
            if (dataGridView1.CurrentRow.DataBoundItem != null)
            {
                Paciente pacienteSelec = (Paciente)dataGridView1.CurrentRow.DataBoundItem;
                if (pacienteSelec != null)
                {
                    var frm = new FrmIngresos(pacienteSelec, null);
                    frm.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un paciente para ver sus ingresos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void BorrarPaciente()
        {
            if (dataGridView1.CurrentRow != null)
            {
                // "CurrentRow" es la fila seleccionada por el usuario.
                // "DataBoundItem" es el objeto original que está enlazado a esa fila.
                var pacienteSelec = (Paciente)dataGridView1.CurrentRow.DataBoundItem;
                if (pacienteSelec != null)
                {
                    var confirmResult = MessageBox.Show($"¿Estás seguro de que deseas borrar al paciente {pacienteSelec.Nombre} {pacienteSelec.Apellidos}?",
                                                         "Confirmar borrado",
                                                         MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirmResult == DialogResult.Yes)
                    {
                        Pacientes.Remove(pacienteSelec);

                        RefrescarDatos();
                        Actualizar(Pacientes);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un paciente para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void RefrescarDatos()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Pacientes;
        }
        public void Actualizar(List<Paciente> pacientes)
        {
            pacientes = Pacientes;
            RefrescarDatos();
        }
    }
}
