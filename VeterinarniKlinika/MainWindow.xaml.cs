using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VeterinarniKlinika.Command;
using VeterinarniKlinika.Model;

namespace VeterinarniKlinika
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
  

        private ObservableCollection<Animal> _animals;
        private Animal _selectedAnimal;

        public MainWindow()
        {
            
            _animals = new ObservableCollection<Animal>();

            // Prikazy
            AddAnimalCommand = new RelayCommand(AddAnimal);
            DeleteAnimalCommand = new RelayCommand(DeleteAnimal, CanDeleteAnimal);
            UpdateAnimalCommand = new RelayCommand(UpdateAnimal, CanUpdateAnimal);
            InitializeComponent();
        }

        public ObservableCollection<Animal> Animals
        {
            get { return _animals; }
            set
            {
                _animals = value;
                OnPropertyChanged();
            }
        }

        public Animal SelectedAnimal
        {
            get { return _selectedAnimal; }
            set
            {
                _selectedAnimal = value;
                OnPropertyChanged();
                DeleteAnimalCommand.RaiseCanExecuteChanged();
                UpdateAnimalCommand.RaiseCanExecuteChanged();
            }
        }

        public ICommand AddAnimalCommand { get; private set; }
        public ICommand DeleteAnimalCommand { get; private set; }
        public ICommand UpdateAnimalCommand { get; private set; }

        private void AddAnimal(object parameter)
        {
            
            Animal newAnimal = new Animal();
            _animals.Add(newAnimal);
            SelectedAnimal = newAnimal;
        }

        private void DeleteAnimal(object parameter)
        {
            
            _animals.Remove(SelectedAnimal);
            SelectedAnimal = null;
        }

        private bool CanDeleteAnimal(object parameter)
        {
            
            return SelectedAnimal != null;
        }

        private void UpdateAnimal(object parameter)
        {
            
        }

        private bool CanUpdateAnimal(object parameter)
        {
            
            return SelectedAnimal != null;
        }


        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
