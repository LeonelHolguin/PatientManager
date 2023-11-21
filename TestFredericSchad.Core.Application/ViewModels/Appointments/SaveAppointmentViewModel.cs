using PatientManager.Core.Application.ViewModels.Medics;
using PatientManager.Core.Application.ViewModels.Patients;
using System.ComponentModel.DataAnnotations;

namespace PatientManager.Core.Application.ViewModels.Appointments
{
    public class SaveAppointmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar una fecha")]
        [DataType(DataType.Date)]
        public DateOnly? AppointmentDate { get; set; }

        [Required(ErrorMessage = "Debe colocar una hora")]
        [DataType(DataType.Time)]
        public TimeOnly? AppointmentTime { get; set; }

        [Required(ErrorMessage = "Debe colocar la causa")]
        [DataType(DataType.MultilineText)]
        public string Reason { get; set; }

        public string State { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un paciente")]
        public int PatientId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un médico")]
        public int MedicId { get; set; }


        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }


        public IEnumerable<PatientViewModel>? Patients { get; set; }
        public IEnumerable<MedicViewModel>? Medics { get; set; }
    }
}
