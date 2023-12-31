using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using VeterinarniKlinika.Model;
using VeterinarniKlinika.ViewModel.Repository;

namespace VeterinarniKlinika.ViewModel
{
    public class VeterinarianViewModel : INotifyPropertyChanged
    {
        private readonly VeterinarianRepository _veterinarianRepository;
        private ObservableCollection<Veterinarian> _veterinarians;
        private Veterinarian _selectedVeterinarian;
        private string _firstName;
        private string _lastName;
        private string _specialization;
        private string _phoneNumber;
        private DateTime? _birthDate; 
        private string _address;

        private string _searchPhoneNumber;

        public VeterinarianViewModel()
        {
            _veterinarianRepository = new VeterinarianRepository(DatabaseHelper.GetConnectionString());
            SearchVeterinariansByPhoneNumberCommand = new RelayCommand(SearchVeterinariansByPhoneNumber);
            LoadVeterinarians();
            CreateVeterinarianCommand = new RelayCommand(CreateVeterinarian);
            UpdateVeterinarianCommand = new RelayCommand(UpdateVeterinarian, CanExecuteUpdateOrDelete);
            DeleteVeterinarianCommand = new RelayCommand(DeleteVeterinarian, CanExecuteUpdateOrDelete);
        }

        public ObservableCollection<Veterinarian> Veterinarians
        {
            get => _veterinarians;
            set
            {
                _veterinarians = value;
                OnPropertyChanged(nameof(Veterinarians));
            }
        }

        public Veterinarian SelectedVeterinarian
        {
            get => _selectedVeterinarian;
            set
            {
                _selectedVeterinarian = value;
                OnPropertyChanged(nameof(SelectedVeterinarian));
                (UpdateVeterinarianCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (DeleteVeterinarianCommand as RelayCommand)?.RaiseCanExecuteChanged();
                if (_selectedVeterinarian != null)
                {
                    FirstName = _selectedVeterinarian.FirstName;
                    LastName = _selectedVeterinarian.LastName;
                    Specialization = _selectedVeterinarian.Specialization;
                    PhoneNumber = _selectedVeterinarian.PhoneNumber;
                    BirthDate = _selectedVeterinarian.BirthDate;
                    Address = _selectedVeterinarian.Address;
                }
            }
        }

        
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public string Specialization
        {
            get => _specialization;
            set
            {
                _specialization = value;
                OnPropertyChanged(nameof(Specialization));
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public DateTime? BirthDate 
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }

        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public string SearchPhoneNumber
        {
            get => _searchPhoneNumber;
            set
            {
                _searchPhoneNumber = value;
                OnPropertyChanged(nameof(SearchPhoneNumber));
            }
        }

        public ICommand CreateVeterinarianCommand { get; private set; }
        public ICommand UpdateVeterinarianCommand { get; private set; }
        public ICommand DeleteVeterinarianCommand { get; private set; }

        public ICommand SearchVeterinariansByPhoneNumberCommand { get; private set; }

        private void LoadVeterinarians()
        {
            var veterinarianList = _veterinarianRepository.GetAllVeterinarians();
            Veterinarians = new ObservableCollection<Veterinarian>(veterinarianList);
        }

        private bool CanExecuteUpdateOrDelete(object parameter)
        {
            return SelectedVeterinarian != null;
        }

        private void CreateVeterinarian(object parameter)
        {
            var newVeterinarian = new Veterinarian
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Specialization = this.Specialization,
                PhoneNumber = this.PhoneNumber,
                BirthDate = this.BirthDate,
                Address = this.Address
            };

            _veterinarianRepository.CreateVeterinarian(newVeterinarian);
            LoadVeterinarians();

            
            FirstName = null;
            LastName = null;
            Specialization = null;
            PhoneNumber = null;
            BirthDate = DateTime.Now;
            Address = null;
        }

        private void UpdateVeterinarian(object parameter)
        {
            if (SelectedVeterinarian != null)
            {
                SelectedVeterinarian.FirstName = this.FirstName;
                SelectedVeterinarian.LastName = this.LastName;
                SelectedVeterinarian.Specialization = this.Specialization;
                SelectedVeterinarian.PhoneNumber = this.PhoneNumber;
                SelectedVeterinarian.BirthDate = this.BirthDate;
                SelectedVeterinarian.Address = this.Address;

                _veterinarianRepository.UpdateVeterinarian(SelectedVeterinarian);
                LoadVeterinarians();
            }
        }

        private void DeleteVeterinarian(object parameter)
        {
            if (SelectedVeterinarian != null)
            {
                _veterinarianRepository.DeleteVeterinarian(SelectedVeterinarian.VeterinarianID);
                LoadVeterinarians();
            }
        }

        private void SearchVeterinariansByPhoneNumber(object parameter)
        {
            var filteredVeterinarians = _veterinarianRepository.GetAllVeterinarians()
                                           .Where(v => v.PhoneNumber.Contains(SearchPhoneNumber))
                                           .ToList();

            Veterinarians = new ObservableCollection<Veterinarian>(filteredVeterinarians);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
