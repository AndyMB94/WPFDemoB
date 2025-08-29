using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para Operaciones.xaml
    /// </summary>
    public partial class Operaciones : Window
    {
        public Operaciones()
        {
            InitializeComponent();

            // Establecer la fecha actual por defecto
            dpFechaHora.SelectedDate = DateTime.Now;

            // Establecer valores por defecto en los ComboBox
            cmbTipoDocumento.SelectedIndex = 0; // DNI por defecto
            cmbTurno.SelectedIndex = 0; // Mañana por defecto
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            // Validar que todos los campos estén llenos
            if (ValidarCampos())
            {
                // Simular guardado exitoso
                string mensaje = "INGRESO REGISTRADO EXITOSAMENTE\n\n";
                mensaje += $"Tipo Documento: {cmbTipoDocumento.Text}\n";
                mensaje += $"Número: {txtNumeroDocumento.Text}\n";
                mensaje += $"Placa: {txtPlaca.Text}\n";
                mensaje += $"Turno: {cmbTurno.Text}\n";
                mensaje += $"Conductor: {txtNombreConductor.Text}\n";
                mensaje += $"Cliente: {txtNombreCliente.Text}\n";
                mensaje += $"Fecha: {dpFechaHora.SelectedDate:dd/MM/yyyy}\n";
                mensaje += $"Peso: {txtPesoIngreso.Text} Kg";

                MessageBox.Show(mensaje, "Registro Exitoso",
                               MessageBoxButton.OK, MessageBoxImage.Information);

                // Limpiar campos después de guardar
                LimpiarCampos();
            }
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            // Volver al menú principal
            Home homeWindow = new Home();
            homeWindow.Show();
            this.Close();
        }

        private bool ValidarCampos()
        {
            // Validar Tipo Documento
            if (cmbTipoDocumento.SelectedItem == null)
            {
                MessageBox.Show("Por favor seleccione un tipo de documento", "Campo Requerido",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbTipoDocumento.Focus();
                return false;
            }

            // Validar Número Documento
            if (string.IsNullOrWhiteSpace(txtNumeroDocumento.Text))
            {
                MessageBox.Show("Por favor ingrese el número de documento", "Campo Requerido",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNumeroDocumento.Focus();
                return false;
            }

            // Validar Placa
            if (string.IsNullOrWhiteSpace(txtPlaca.Text))
            {
                MessageBox.Show("Por favor ingrese la placa del vehículo", "Campo Requerido",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPlaca.Focus();
                return false;
            }

            // Validar Turno
            if (cmbTurno.SelectedItem == null)
            {
                MessageBox.Show("Por favor seleccione un turno", "Campo Requerido",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbTurno.Focus();
                return false;
            }

            // Validar Nombre Conductor
            if (string.IsNullOrWhiteSpace(txtNombreConductor.Text))
            {
                MessageBox.Show("Por favor ingrese el nombre del conductor", "Campo Requerido",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNombreConductor.Focus();
                return false;
            }

            // Validar Nombre Cliente
            if (string.IsNullOrWhiteSpace(txtNombreCliente.Text))
            {
                MessageBox.Show("Por favor ingrese el nombre del cliente", "Campo Requerido",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNombreCliente.Focus();
                return false;
            }

            // Validar Fecha
            if (dpFechaHora.SelectedDate == null)
            {
                MessageBox.Show("Por favor seleccione una fecha", "Campo Requerido",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                dpFechaHora.Focus();
                return false;
            }

            // Validar Peso
            if (string.IsNullOrWhiteSpace(txtPesoIngreso.Text))
            {
                MessageBox.Show("Por favor ingrese el peso de ingreso", "Campo Requerido",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPesoIngreso.Focus();
                return false;
            }

            // Validar que el peso sea numérico
            if (!double.TryParse(txtPesoIngreso.Text, out double peso))
            {
                MessageBox.Show("El peso debe ser un número válido", "Formato Incorrecto",
                               MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPesoIngreso.Focus();
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            cmbTipoDocumento.SelectedIndex = 0;
            txtNumeroDocumento.Clear();
            txtPlaca.Clear();
            cmbTurno.SelectedIndex = 0;
            txtNombreConductor.Clear();
            txtNombreCliente.Clear();
            dpFechaHora.SelectedDate = DateTime.Now;
            txtPesoIngreso.Clear();

            // Poner el foco en el primer campo
            cmbTipoDocumento.Focus();
        }
    }
}