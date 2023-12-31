using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using VeterinarniKlinika.Model;
using VeterinarniKlinika.ViewModel.Repository;
using System;
using System.Linq;

namespace VeterinarniKlinika.ViewModel
{
    public class AppointmentViewModel : INotifyPropertyChanged
    {
        private readonly AppointmentRepository _appointmentRepository;
        private ObservableCollection<Appointment> _appointments;
        private Appointment _selectedAppointment;
        private string _searchAnimalID;

        private int _veterinarianID;
        private int _animalID;
        private DateTime? _appointmentDate; 
        private string _serviceType;
        private string _comments;
        private int _status;

        public AppointmentViewModel()
        {
            _appointmentRepository = new AppointmentRepository(DatabaseHelper.GetConnectionString());
            LoadAppointments();
            CreateAppointmentCommand = new RelayCommand(CreateAppointment);
            UpdateAppointmentCommand = new RelayCommand(UpdateAppointment, CanExecuteUpdateOrDelete);
            DeleteAppointmentCommand = new RelayCommand(DeleteAppointment, CanExecuteUpdateOrDelete);
            SearchByAnimalIDCommand = new RelayCommand(SearchByAnimalID);
        }

        public ObservableCollection<Appointment> Appointments
        {
            get => _appointments;
            set
            {
                _appointments = value;
                OnPropertyChanged(nameof(Appointments));
            }
        }

        public Appointment SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                _selectedAppointment = value;
                OnPropertyChanged(nameof(SelectedAppointment));
                (UpdateAppointmentCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (DeleteAppointmentCommand as RelayCommand)?.RaiseCanExecuteChanged();
                if (_selectedAppointment != null)
                {
                    VeterinarianID = _selectedAppointment.VeterinarianID;
                    AnimalID = _selectedAppointment.AnimalID;
                    AppointmentDate = _selectedAppointment.AppointmentDate;
                    ServiceType = _selectedAppointment.ServiceType;
                    Comments = _selectedAppointment.Comments;
                    Status = _selectedAppointment.Status;
                }
            }
        }

        public int VeterinarianID
        {
            get => _veterinarianID;
            set
            {
                _veterinarianID = value;
                OnPropertyChanged(nameof(VeterinarianID));
            }
        }

        public int AnimalID
        {
            get => _animalID;
            set
            {
                _animalID = value;
                OnPropertyChanged(nameof(AnimalID));
            }
        }

        public DateTime? AppointmentDate 
        {
            get => _appointmentDate;
            set
            {
                _appointmentDate = value;
                OnPropertyChanged(nameof(AppointmentDate));
            }
        }

        public string ServiceType
        {
            get => _serviceType;
            set
            {
                _serviceType = value;
                OnPropertyChanged(nameof(ServiceType));
            }
        }

        public string Comments
        {
            get => _comments;
            set
            {
                _comments = value;
                OnPropertyChanged(nameof(Comments));
            }
        }

        public int Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public string SearchAnimalID
        {
            get => _searchAnimalID;
            set
            {
                _searchAnimalID = value;
                OnPropertyChanged(nameof(SearchAnimalID));
            }
        }

        public ICommand CreateAppointmentCommand { get; private set; }
        public ICommand UpdateAppointmentCommand { get; private set; }
        public ICommand DeleteAppointmentCommand { get; private set; }
        public ICommand SearchByAnimalIDCommand { get; private set; }

        private void LoadAppointments()
        {
            var appointmentList = _appointmentRepository.GetAllAppointments();
            Appointments = new ObservableCollection<Appointment>(appointmentList);
        }

        private bool CanExecuteUpdateOrDelete(object parameter)
        {
            return SelectedAppointment != null;
        }

        private void CreateAppointment(object parameter)
        {
            var newAppointment = new Appointment
            {
                VeterinarianID = this.VeterinarianID,
                AnimalID = this.AnimalID,
                AppointmentDate = this.AppointmentDate ?? DateTime.Now, 
                ServiceType = this.ServiceType,
                Comments = this.Comments,
                Status = this.Status
            };

            _appointmentRepository.CreateAppointment(newAppointment);
            LoadAppointments();
            ResetAppointmentFields();
        }

        private void UpdateAppointment(object parameter)
        {
            if (SelectedAppointment != null)
            {
                SelectedAppointment.VeterinarianID = this.VeterinarianID;
                SelectedAppointment.AnimalID = this.AnimalID;
                SelectedAppointment.AppointmentDate = this.AppointmentDate ?? DateTime.Now; 
                SelectedAppointment.ServiceType = this.ServiceType;
                SelectedAppointment.Comments = this.Comments;
                SelectedAppointment.Status = this.Status;

                _appointmentRepository.UpdateAppointment(SelectedAppointment);
                LoadAppointments();
            }
        }

        private void DeleteAppointment(object parameter)
        {
            if (SelectedAppointment != null)
            {
                _appointmentRepository.DeleteAppointment(SelectedAppointment.AppointmentID);
                LoadAppointments();
            }
        }

        private void ResetAppointmentFields()
        {
            VeterinarianID = 0;
            AnimalID = 0;
            AppointmentDate = null; 
            ServiceType = null;
            Comments = null;
            Status = 0;
        }

        private void SearchByAnimalID(object parameter)
        {
            if (int.TryParse(_searchAnimalID, out int animalIdParsed))
            {
                var filteredAppointments = _appointmentRepository.GetAllAppointments()
                    .Where(appointment => appointment.AnimalID == animalIdParsed)
                    .ToList();

                Appointments = new ObservableCollection<Appointment>(filteredAppointments);
            }
            else
            {
                Appointments = new ObservableCollection<Appointment>(_appointmentRepository.GetAllAppointments());
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
