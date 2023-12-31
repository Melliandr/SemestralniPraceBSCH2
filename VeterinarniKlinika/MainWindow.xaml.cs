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
using VeterinarniKlinika.View;

namespace VeterinarniKlinika
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenPacientRegisterView_Click(object sender, RoutedEventArgs e)
        {
            var pacientRegisterView = new PacientRegisterView();
            pacientRegisterView.ShowDialog(); // Use ShowDialog for a modal window or Show for a non-modal
        }

        private void OpenVeterinarianRegisterView_Click(object sender, RoutedEventArgs e)
        {
            var veterinarianRegisterView = new VeterinarianRegisterView();
            veterinarianRegisterView.Show();
        }

        private void OpenAppointmentView_Click(object sender, RoutedEventArgs e)
        {
            var appointmentView = new AppointmentView();
            appointmentView.Show();
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void ListViewItem_Selected_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
