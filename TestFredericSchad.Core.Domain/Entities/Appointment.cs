using PatientManager.Core.Domain.Common;

namespace PatientManager.Core.Domain.Entities
{
    public class Appointment : AuditableBaseEntity
    {
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public string Reason {  get; set; }
        public string State { get; set; }
        public int PatientId { get; set; }
        public int MedicId { get; set; }


        public Patient? Patient { get; set; }
        public Medic? Medic { get; set; }
        public ICollection<LaboratoryTestResult>? LaboratoryTestResults { get; set; }
    }
}
