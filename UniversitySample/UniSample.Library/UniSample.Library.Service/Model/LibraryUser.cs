namespace UniSample.Library.Service.Model
{
    public class LibraryUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid? StudentId { get; set; }
        public List<Lending> Lendings { get; set; }
    }
}
