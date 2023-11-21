using PatientManager.Core.Application.ViewModels.LaboratoryTest;
using PatientManager.Core.Application.ViewModels.Patients;
using PatientManager.Core.Application.ViewModels.Appointments;


namespace PatientManager.Core.Application.ViewModels.LaboratoryTestResults
{
    public class LaboratoryTestResultViewModel
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string State { get; set; }
        public PatientViewModel Patient { get; set; }
        public LaboratoryTestViewModel LaboratoryTest { get; set; }
        public AppointmentViewModel Appointment { get; set; }
    }
}
