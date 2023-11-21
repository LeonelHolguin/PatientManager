using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManager.Core.Application.ViewModels.LaboratoryTestResults
{
    public class SaveLaboratoryTestResultViewModel
    {
        public int Id { get; set; }

        public string? Description { get; set; }
        public string State { get; set; }
        public int PatientId { get; set; }
        public int LaboratoryTestId { get; set; }
        public int? AppointmentId { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
    }
}
