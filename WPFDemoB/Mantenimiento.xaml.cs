using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFDemoB
{
    /// <summary>
    /// Lógica de interacción para Mantenimiento.xaml
    /// </summary>
    public partial class Mantenimiento : Window
    {
        // Lista para almacenar los conductores
        private ObservableCollection<Conductor> conductores;
        private int contadorId = 1;

        public Mantenimiento()
        {
            InitializeComponent();

            // Inicializar la lista de conductores
            conductores = new ObservableCollection<Conductor>();

            // Asignar la lista al DataGrid
            dgConductores.ItemsSource = conductores;

            // Establecer valores por defecto
            cmbTransporte.SelectedIndex = 0; // Camión por defecto

            // Agregar algunos conductores de ejemplo
            AgregarConductoresEjemplo();

            // Actualizar contador
            ActualizarContador();
        }

        private void btnAgregarConductor_Click(object sender, RoutedEventArgs e)
        {
            // Validar que todos los campos estén llenos
            if (ValidarCampos())
            {
                // Crear nuevo conductor
                Conductor nuevoConductor = new Conductor
                {
                    Id = contadorId++,
                    Nombre = txtNombreConductor.Text.Trim(),
                    Licencia = txtLicencia.Text.Trim(),
                    Transporte = cmbTransporte.Text,
                    FechaRegistro = DateTime.Now
                };

                // Agregar a la lista
                conductores.Add(nuevoConductor);

                // Mostrar mensaje de éxito
                MessageBox.Show($"CONDUCTOR REGISTRADO EXITOSAMENTE\n\n" +
                               $"Nombre: {nuevoConductor.Nombre}\n" +
                               $"Licencia: {nuevoConductor.Licencia}\n" +
                               $"Transporte: {nuevoConductor.Transporte}\n" +
                               $"Fecha: {nuevoConductor.FechaRegistro:dd/MM/yyyy}",
                               "Registro Exitoso", MessageBoxButton.OK, MessageBoxImage.Information);

                // Limpiar campos
                LimpiarCampos();

                // Actualizar contador
                ActualizarContador();
            }
        }

        private void btnLimpiarFormulario_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void btnVolverHome_Click(object sender, RoutedEventArgs e)
        {
            // Volver al menú principal
            Home homeWindow = new Home();
            homeWindow.Show();
            this.Close();
        }

        private bool ValidarCampos()
        {
            // Validar Nombre Conductor
            if (string.IsNullOrWhiteSpace(txtNombreConductor.Text))
            {
                MessageBox.Show("Por favor ingrese el nombre del conductor", "Campo Requerido",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNombreConductor.Focus();
                return false;
            }

            // Validar Licencia
            if (string.IsNullOrWhiteSpace(txtLicencia.Text))
            {
                MessageBox.Show("Por favor ingrese el número de licencia", "Campo Requerido",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                txtLicencia.Focus();
                return false;
            }

            // Validar Transporte
            if (cmbTransporte.SelectedItem == null)
            {
                MessageBox.Show("Por favor seleccione un tipo de transporte", "Campo Requerido",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbTransporte.Focus();
                return false;
            }

            // Validar que la licencia no esté duplicada
            if (conductores.Any(c => c.Licencia.Equals(txtLicencia.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Ya existe un conductor con este número de licencia", "Licencia Duplicada",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                txtLicencia.Focus();
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            txtNombreConductor.Clear();
            txtLicencia.Clear();
            cmbTransporte.SelectedIndex = 0;
            txtNombreConductor.Focus();
        }

        private void ActualizarContador()
        {
            lblTotalConductores.Text = $"Total de conductores registrados: {conductores.Count}";
        }

        private void AgregarConductoresEjemplo()
        {
            // Agregar algunos conductores de ejemplo para demostración
            conductores.Add(new Conductor
            {
                Id = contadorId++,
                Nombre = "Juan Pérez García",
                Licencia = "L12345678",
                Transporte = "Camión",
                FechaRegistro = DateTime.Now.AddDays(-5)
            });

            conductores.Add(new Conductor
            {
                Id = contadorId++,
                Nombre = "María López Silva",
                Licencia = "L87654321",
                Transporte = "Tráiler",
                FechaRegistro = DateTime.Now.AddDays(-3)
            });

            conductores.Add(new Conductor
            {
                Id = contadorId++,
                Nombre = "Carlos Rodríguez Mendoza",
                Licencia = "L11223344",
                Transporte = "Furgón",
                FechaRegistro = DateTime.Now.AddDays(-1)
            });
        }
    }

    // Clase para representar un conductor
    public class Conductor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Licencia { get; set; }
        public string Transporte { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}