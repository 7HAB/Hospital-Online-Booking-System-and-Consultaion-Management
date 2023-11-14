namespace graduationProject.DAL
{
    public interface IDoctorRepo : IGenaricRepo<Doctor>
    {
        public Doctor? GetById(string? id);
        public List<Doctor> GetAll();
        public List<Specialization> GetDoctorsBySpecialization(int SpeializationId);
        public List<Specialization> GetAllSpecializations();
        void UploadDoctorImage(List<Doctor> doctors);
        //void UpdateDoctorImage(string doctorId, string fileName, string storedFileName, string contentType);


    }
}
