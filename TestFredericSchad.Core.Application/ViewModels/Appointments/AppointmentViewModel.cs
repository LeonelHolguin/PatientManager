using PatientManager.Core.Application.ViewModels.Medics;
using PatientManager.Core.Application.ViewModels.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManager.Core.Application.ViewModels.Appointments
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public string Reason { get; set; }
        public string State { get; set; }
        public PatientViewModel Patient { get; set; }
        public MedicViewModel Medic { get; set; }
    }
}
