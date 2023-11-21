using System.ComponentModel.DataAnnotations;

namespace PatientManager.Core.Application.ViewModels.LaboratoryTest
{
    public class SaveLaboratoryTestViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
    }
}
