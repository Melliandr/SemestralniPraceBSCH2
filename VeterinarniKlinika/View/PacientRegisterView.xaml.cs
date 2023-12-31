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
using VeterinarniKlinika.ViewModel;

namespace VeterinarniKlinika.View
{
    /// <summary>
    /// Interaction logic for PacientRegisterView.xaml
    /// </summary>
    public partial class PacientRegisterView : Window
    {
        public PacientRegisterView()
        {
            InitializeComponent();
            DataContext = new AnimalViewModel();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
