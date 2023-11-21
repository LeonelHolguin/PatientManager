namespace PatientManager.Core.Application.ViewModels.Patients
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string IdentityCard { get; set; }
        public DateOnly BirthDate { get; set; }
        public bool IsSmoker { get; set; }
        public bool HasAllergy { get; set; }
        public string Photo { get; set; }
    }
}
