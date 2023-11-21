using PatientManager.Core.Domain.Common;

namespace PatientManager.Core.Domain.Entities
{
    public class LaboratoryTestResult : AuditableBaseEntity
    {
        public string? Description { get; set; }
        public string State { get; set; }
        public int PatientId { get; set; }
        public int LaboratoryTestId { get; set; }
        public int? AppointmentId { get; set; }

        public LaboratoryTest? LaboratoryTest { get; set; }
        public Patient? Patient { get; set; }
        public Appointment? Appointment { get; set; }
    }
}
