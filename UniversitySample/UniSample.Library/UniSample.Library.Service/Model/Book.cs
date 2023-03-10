using System.ComponentModel.DataAnnotations;

namespace UniSample.Library.Service.Model
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public bool Available { get; set; }
        public List<Lending> Lendings { get; set; }
    }
}
