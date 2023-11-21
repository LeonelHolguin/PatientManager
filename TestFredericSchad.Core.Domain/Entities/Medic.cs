using PatientManager.Core.Domain.Common;


namespace PatientManager.Core.Domain.Entities
{
    public class Medic : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string IdentityCard { get; set; }
        public string Photo { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
    }
}
