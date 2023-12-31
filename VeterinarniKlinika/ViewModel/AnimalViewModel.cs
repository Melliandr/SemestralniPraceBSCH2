using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using VeterinarniKlinika.Model;
using VeterinarniKlinika.ViewModel.Repository;
using System;
using System.Linq;

namespace VeterinarniKlinika.ViewModel
{
    public class AnimalViewModel : INotifyPropertyChanged
    {
        private readonly AnimalRepository _animalRepository;
        private ObservableCollection<Animal> _animals;
        private Animal _selectedAnimal;
        private string _name;
        private string _species;
        private string _breed;
        private DateTime? _dateOfBirth;
        private DateTime? _searchDateOfBirth;

        public AnimalViewModel()
        {
            SearchByDateOfBirthCommand = new RelayCommand(SearchByDateOfBirth);
            _animalRepository = new AnimalRepository(DatabaseHelper.GetConnectionString());
            LoadAnimals();
            CreateAnimalCommand = new RelayCommand(CreateAnimal);
            UpdateAnimalCommand = new RelayCommand(UpdateAnimal, CanExecuteUpdateOrDelete);
            DeleteAnimalCommand = new RelayCommand(DeleteAnimal, CanExecuteUpdateOrDelete);
        }

        public ObservableCollection<Animal> Animals
        {
            get => _animals;
            set
            {
                _animals = value;
                OnPropertyChanged(nameof(Animals));
            }
        }

        public Animal SelectedAnimal
        {
            get => _selectedAnimal;
            set
            {
                _selectedAnimal = value;
                OnPropertyChanged(nameof(SelectedAnimal));
                UpdatePropertiesFromSelectedAnimal();
                (UpdateAnimalCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (DeleteAnimalCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Species
        {
            get => _species;
            set
            {
                _species = value;
                OnPropertyChanged(nameof(Species));
            }
        }

        public string Breed
        {
            get => _breed;
            set
            {
                _breed = value;
                OnPropertyChanged(nameof(Breed));
            }
        }

        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }

        public DateTime? SearchDateOfBirth
        {
            get => _searchDateOfBirth;
            set
            {
                _searchDateOfBirth = value;
                OnPropertyChanged(nameof(SearchDateOfBirth));
            }
        }

        public ICommand CreateAnimalCommand { get; private set; }
        public ICommand UpdateAnimalCommand { get; private set; }
        public ICommand DeleteAnimalCommand { get; private set; }
        public ICommand SearchByDateOfBirthCommand { get; private set; }

        private void LoadAnimals()
        {
            var animalList = _animalRepository.GetAllAnimals();
            Animals = new ObservableCollection<Animal>(animalList);
        }

        private bool CanExecuteUpdateOrDelete(object parameter)
        {
            return SelectedAnimal != null;
        }

        private void CreateAnimal(object parameter)
        {
            var newAnimal = new Animal
            {
                Name = this.Name,
                Species = this.Species,
                Breed = this.Breed,
                DateOfBirth = this.DateOfBirth
            };

            _animalRepository.CreateAnimal(newAnimal);
            LoadAnimals();
            ClearInputFields();
        }

        private void UpdateAnimal(object parameter)
        {
            if (SelectedAnimal != null)
            {
                SelectedAnimal.Name = this.Name;
                SelectedAnimal.Species = this.Species;
                SelectedAnimal.Breed = this.Breed;
                SelectedAnimal.DateOfBirth = this.DateOfBirth;

                _animalRepository.UpdateAnimal(SelectedAnimal);
                LoadAnimals();
            }
        }

        private void DeleteAnimal(object parameter)
        {
            if (SelectedAnimal != null)
            {
                _animalRepository.DeleteAnimal(SelectedAnimal.AnimalID);
                LoadAnimals();
                ClearInputFields();
            }
        }

        private void SearchByDateOfBirth(object parameter)
        {
            if (_searchDateOfBirth.HasValue)
            {
                var filteredAnimals = _animalRepository.GetAllAnimals()
                    .Where(animal => animal.DateOfBirth == _searchDateOfBirth.Value).ToList();

                Animals = new ObservableCollection<Animal>(filteredAnimals);
            }
        }

        private void UpdatePropertiesFromSelectedAnimal()
        {
            if (_selectedAnimal != null)
            {
                Name = _selectedAnimal.Name;
                Species = _selectedAnimal.Species;
                Breed = _selectedAnimal.Breed;
                DateOfBirth = _selectedAnimal.DateOfBirth;
            }
            else
            {
                ClearInputFields();
            }
        }

        private void ClearInputFields()
        {
            Name = null;
            Species = null;
            Breed = null;
            DateOfBirth = null;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
