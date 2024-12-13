using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistroDeEstudiantes
{
    public partial class Form1 : Form
    {
        private BindingSource bindingSource = new BindingSource();
        private List<Estdiante> estudiantes = new List<Estdiante>();


        public Form1()
        {
            InitializeComponent();
            
            estudiantes.Add(new Estdiante { Nombre = "Ana Pérez", Edad = 7, Grado = "Primero", Estado = "Becado" });
            estudiantes.Add(new Estdiante { Nombre = "Luis Gómez", Edad = 12, Grado = "Segundo", Estado = "Regular" });
            estudiantes.Add(new Estdiante { Nombre = "Carla López", Edad = 15, Grado = "Tercero", Estado = "Becado" });

            InicializarDataGridView();
        }


        
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void InicializarDataGridView()
        {

            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();

            bindingSource.DataSource = estudiantes;
            dataGridView1.DataSource = bindingSource;



            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AllowUserToAddRows = false;
        }


        private void ActualizarTabla()
        {
            bindingSource.ResetBindings(false);
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {

            var form = new FormEstudiante();
            if (form.ShowDialog() == DialogResult.OK)
            {
                estudiantes.Add(form.Estudiante);
                ActualizarTabla();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                var estudianteSeleccionado = (Estdiante)dataGridView1.CurrentRow.DataBoundItem;
                var form = new FormEstudiante(estudianteSeleccionado);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    ActualizarTabla();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un estudiante para editar.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentRow != null)
            {
                var estudianteSeleccionado = (Estdiante)dataGridView1.CurrentRow.DataBoundItem;
                estudiantes.Remove(estudianteSeleccionado);
                ActualizarTabla();
            }
            else
            {
                MessageBox.Show("Seleccione un estudiante para eliminar.");
            }
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            var promedioEdad = estudiantes.Average(est => est.Edad);
            var estudiantesPorGrado = estudiantes.GroupBy(est => est.Grado)
                                                  .Select(g => new { Grado = g.Key, Total = g.Count() });
            var porcentajeBecados = (double)estudiantes.Count(est => est.Estado == "Becado") / estudiantes.Count * 100;

            MessageBox.Show($"Promedio de Edad: {promedioEdad}\n" +
                            $"Estudiantes por Grado:\n" +
                            string.Join("\n", estudiantesPorGrado.Select(g => $"{g.Grado}: {g.Total}")) +
                            $"\nPorcentaje de Becados: {porcentajeBecados:0.00}%");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
    
}
