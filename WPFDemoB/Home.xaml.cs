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
    /// Lógica de interacción para Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
        }

        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            // Confirmar si desea cerrar sesión
            MessageBoxResult resultado = MessageBox.Show(
                "¿Está seguro que desea cerrar la sesión?",
                "Confirmar Cierre de Sesión",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                // Crear nueva ventana de login
                MainWindow loginWindow = new MainWindow();

                // Mostrar la ventana de login
                loginWindow.Show();

                // Cerrar la ventana actual (Home)
                this.Close();
            }
        }

        private void btnIngresos_Click(object sender, RoutedEventArgs e)
        {
            // Abrir la ventana de Operaciones (Ingresos)
            Operaciones operacionesWindow = new Operaciones();
            operacionesWindow.Show();
            this.Close();
        }

        private void btnConductores_Click(object sender, RoutedEventArgs e)
        {
            // Abrir la ventana de Mantenimiento (Conductores)
            Mantenimiento mantenimientoWindow = new Mantenimiento();
            mantenimientoWindow.Show();
            this.Close();
        }

        private void btnReporteIngresos_Click(object sender, RoutedEventArgs e)
        {
            // Abrir la ventana de Reportes (Ingresos)
            Reportes reportesWindow = new Reportes();
            reportesWindow.Show();
            this.Close();
        }
    }
}