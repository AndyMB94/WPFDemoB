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
    /// Lógica de interacción para Reportes.xaml
    /// </summary>
    public partial class Reportes : Window
    {
        // Lista completa de ingresos
        private List<IngresoReporte> todosLosIngresos;

        // Lista filtrada que se muestra en el DataGrid
        private ObservableCollection<IngresoReporte> ingresosFiltrados;

        public Reportes()
        {
            InitializeComponent();

            // Inicializar listas
            todosLosIngresos = new List<IngresoReporte>();
            ingresosFiltrados = new ObservableCollection<IngresoReporte>();

            // Asignar la lista filtrada al DataGrid
            dgIngresos.ItemsSource = ingresosFiltrados;

            // Cargar datos de ejemplo
            CargarDatosEjemplo();

            // Mostrar todos los registros inicialmente
            MostrarTodosLosRegistros();

            // Establecer fechas por defecto (último mes)
            dpFechaInicio.SelectedDate = DateTime.Now.AddDays(-30);
            dpFechaFin.SelectedDate = DateTime.Now;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            AplicarFiltros();
        }

        private void btnLimpiarFiltros_Click(object sender, RoutedEventArgs e)
        {
            // Limpiar todos los filtros
            dpFechaInicio.SelectedDate = null;
            dpFechaFin.SelectedDate = null;
            txtFiltroPlaca.Clear();
            txtFiltroConductor.Clear();
            txtFiltroProducto.Clear();

            // Mostrar todos los registros
            MostrarTodosLosRegistros();
        }

        private void btnMostrarTodos_Click(object sender, RoutedEventArgs e)
        {
            MostrarTodosLosRegistros();
        }

        private void btnVolverHome_Click(object sender, RoutedEventArgs e)
        {
            // Volver al menú principal
            Home homeWindow = new Home();
            homeWindow.Show();
            this.Close();
        }

        private void AplicarFiltros()
        {
            var resultado = todosLosIngresos.AsEnumerable();

            // Filtro por fecha inicio
            if (dpFechaInicio.SelectedDate.HasValue)
            {
                DateTime fechaInicio = dpFechaInicio.SelectedDate.Value.Date;
                resultado = resultado.Where(x => x.Fecha.Date >= fechaInicio);
            }

            // Filtro por fecha fin
            if (dpFechaFin.SelectedDate.HasValue)
            {
                DateTime fechaFin = dpFechaFin.SelectedDate.Value.Date;
                resultado = resultado.Where(x => x.Fecha.Date <= fechaFin);
            }

            // Filtro por placa
            if (!string.IsNullOrWhiteSpace(txtFiltroPlaca.Text))
            {
                string placa = txtFiltroPlaca.Text.Trim().ToLower();
                resultado = resultado.Where(x => x.Placa.ToLower().Contains(placa));
            }

            // Filtro por conductor
            if (!string.IsNullOrWhiteSpace(txtFiltroConductor.Text))
            {
                string conductor = txtFiltroConductor.Text.Trim().ToLower();
                resultado = resultado.Where(x => x.Conductor.ToLower().Contains(conductor));
            }

            // Filtro por producto
            if (!string.IsNullOrWhiteSpace(txtFiltroProducto.Text))
            {
                string producto = txtFiltroProducto.Text.Trim().ToLower();
                resultado = resultado.Where(x => x.Producto.ToLower().Contains(producto));
            }

            // Actualizar la lista filtrada
            ingresosFiltrados.Clear();
            var registrosFiltrados = resultado.ToList();

            foreach (var ingreso in registrosFiltrados)
            {
                ingresosFiltrados.Add(ingreso);
            }

            // Actualizar estadísticas
            ActualizarEstadisticas();

            // Mostrar mensaje si no hay resultados
            if (!ingresosFiltrados.Any())
            {
                MessageBox.Show("No se encontraron registros con los filtros aplicados.",
                               "Sin Resultados", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void MostrarTodosLosRegistros()
        {
            ingresosFiltrados.Clear();
            foreach (var ingreso in todosLosIngresos)
            {
                ingresosFiltrados.Add(ingreso);
            }
            ActualizarEstadisticas();
        }

        private void ActualizarEstadisticas()
        {
            lblTotalRegistros.Text = ingresosFiltrados.Count.ToString();

            double pesoTotal = ingresosFiltrados.Sum(x => x.Peso);
            lblPesoTotal.Text = $"{pesoTotal:N2} Kg";
        }

        private void CargarDatosEjemplo()
        {
            // Crear datos de ejemplo para demostración
            var productos = new[] { "Cemento", "Arena", "Grava", "Hierro", "Madera", "Ladrillos", "Cal", "Yeso" };
            var conductores = new[] { "Juan Pérez", "María López", "Carlos Rodríguez", "Ana García", "Luis Martín", "Carmen Silva" };
            var placas = new[] { "ABC-123", "DEF-456", "GHI-789", "JKL-012", "MNO-345", "PQR-678" };
            var turnos = new[] { "Mañana", "Tarde", "Noche" };
            var transportes = new[] { "Camión", "Tráiler", "Furgón", "Volquete", "Cisterna" };

            Random random = new Random();

            // Generar 50 registros de ejemplo
            for (int i = 1; i <= 50; i++)
            {
                todosLosIngresos.Add(new IngresoReporte
                {
                    Id = i,
                    Fecha = DateTime.Now.AddDays(-random.Next(0, 60)), // Últimos 60 días
                    Placa = placas[random.Next(placas.Length)],
                    Turno = turnos[random.Next(turnos.Length)],
                    Conductor = conductores[random.Next(conductores.Length)],
                    Producto = productos[random.Next(productos.Length)],
                    Peso = Math.Round(random.NextDouble() * 40 + 5, 2), // Entre 5 y 45 toneladas
                    Transporte = transportes[random.Next(transportes.Length)]
                });
            }

            // Ordenar por fecha descendente (más recientes primero)
            todosLosIngresos = todosLosIngresos.OrderByDescending(x => x.Fecha).ToList();
        }
    }

    // Clase para representar un ingreso en el reporte
    public class IngresoReporte
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Placa { get; set; }
        public string Turno { get; set; }
        public string Conductor { get; set; }
        public string Producto { get; set; }
        public double Peso { get; set; }
        public string Transporte { get; set; }
    }
}