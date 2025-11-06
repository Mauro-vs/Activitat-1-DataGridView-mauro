using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Activitat_1_DataGridView_mauro
{
 
        public class Paciente
        {
            private static int contadorId = 0;
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public int Edad { get; set; }
            public List<Ingreso> Ingresos { get; set; } = new List<Ingreso>();

            public Paciente() // Constructor
        {
                contadorId++; // Incrementa el contador estático cada vez que se crea un nuevo Paciente
            Id = contadorId;
            }
    }
    }

