using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFDemoB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Mi primer comentario Mallcco
        //Segundo comentario Mallcco
        //Tercer comentario Mallcco
        
        // Credenciales de prueba
        private const string USUARIO_CORRECTO = "andy.mallcco";
        private const string PASSWORD_CORRECTO = "123456";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Obtener los valores de los campos
            string usuario = txtUsuario.Text;
            string password = txtPassword.Password;

            // Validar credenciales
            if (usuario == USUARIO_CORRECTO && password == PASSWORD_CORRECTO)
            {
                // Login exitoso
                MessageBox.Show("¡Bienvenido " + usuario + "!", "Login Exitoso", 
                               MessageBoxButton.OK, MessageBoxImage.Information);
                
                // Abrir la ventana Home
                Home homeWindow = new Home();
                homeWindow.Show();
                
                // Cerrar la ventana de login
                this.Close();
            }
            else
            {
                // Login fallido
                MessageBox.Show("Usuario o contraseña incorrectos.", 
                               "Error de Autenticación", 
                               MessageBoxButton.OK, MessageBoxImage.Error);
                
                // Limpiar los campos
                txtUsuario.Clear();
                txtPassword.Clear();
                txtUsuario.Focus();
            }
        }
    }
}