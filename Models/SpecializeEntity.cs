namespace ProjectTest.Models
{
    public class SpecializeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DoctorEntity> Doctors { get; set; }
    }
}