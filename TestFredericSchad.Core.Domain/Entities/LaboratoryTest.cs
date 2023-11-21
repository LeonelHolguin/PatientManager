using PatientManager.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManager.Core.Domain.Entities
{
    public class LaboratoryTest : AuditableBaseEntity
    {
        public string Name { get; set; }


        public ICollection<LaboratoryTestResult>? LaboratoryTestResults { get; set; }
    }
}
