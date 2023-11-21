using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace PatientManager.Core.Application.ViewModels.Patients
{
    public class SavePatientViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debe colocar un apellido")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe colocar un número telefonico")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Debe colocar una dirección")]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Required(ErrorMessage = "Debe colocar una cédula")]
        [DataType(DataType.Text)]
        public string IdentityCard { get; set; }

        [Required(ErrorMessage = "Debe colocar una fecha")]
        [DataType(DataType.Date)]
        public DateOnly? BirthDate { get; set; }

        public bool IsSmoker { get; set; }

        public bool HasAllergy { get; set; }

        public string Photo { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }
    }
}
