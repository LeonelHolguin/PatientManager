using PatientManager.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManager.Core.Domain.Entities
{
    public class Patient : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; } 
        public string Address { get; set; }
        public string IdentityCard { get; set; }
        public DateOnly BirthDate { get; set; }
        public bool IsSmoker { get; set; }
        public bool HasAllergy { get; set; }
        public string Photo { get; set; }


        public ICollection<LaboratoryTestResult>? LaboratoryTestResults { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
