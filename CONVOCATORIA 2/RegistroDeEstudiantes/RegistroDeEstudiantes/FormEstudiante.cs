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
    public partial class FormEstudiante : Form
    {
        public Estdiante Estudiante { get; private set; }

        public FormEstudiante(Estdiante estudiante = null)
        {
            InitializeComponent();
            Estudiante = estudiante ?? new Estdiante();
            if (estudiante != null)
            {
                txtNombre.Text = estudiante.Nombre;
                numericEdad.Value = estudiante.Edad;
                cmbGrado.SelectedItem = estudiante.Grado;
                if (estudiante.Estado == "Becado")
                    radioBecado.Checked = true;
                else
                    radioRegular.Checked = true;
            }
        }

        private void FormEstudiante_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (Validar())
            {
                Estudiante.Nombre = txtNombre.Text;
                Estudiante.Edad = (int)numericEdad.Value;
                Estudiante.Grado = cmbGrado.SelectedItem.ToString();
                Estudiante.Estado = radioBecado.Checked ? "Becado" : "Regular";
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool Validar()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre no puede estar vacío.");
                return false;
            }
            if (cmbGrado.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar un grado.");
                return false;
            }
            if (numericEdad.Value < 6 || numericEdad.Value > 30)
            {
                MessageBox.Show("La edad debe estar entre 6 y 30 años.");
                return false;
            }
            return true;
        }


    }
}
