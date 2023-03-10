namespace UniSample.Library.Service.Model
{
    public class Lending
    {
        public Guid Id { get; set; }
        public Book Book { get; set; }
        public LibraryUser LibraryUser { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? ReturnTime { get; set; }
    }
}
