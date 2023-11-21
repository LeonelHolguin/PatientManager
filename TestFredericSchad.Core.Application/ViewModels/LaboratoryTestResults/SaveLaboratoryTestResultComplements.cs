using PatientManager.Core.Application.ViewModels.LaboratoryTest;


namespace PatientManager.Core.Application.ViewModels.LaboratoryTestResults
{
    public class SaveLaboratoryTestResultComplements
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public List<int> LaboratoryTestsId { get; set; }
        public List<LaboratoryTestViewModel> ListLaboratoryTest { get; set; }   
    }
}
