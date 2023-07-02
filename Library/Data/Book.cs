namespace Library.Data
{
    public class Book
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Address { get; set; }
        public DateTime PublishDate { get; set; }
        public int TotalNumber { get; set; }
        
        public virtual ICollection<Barow>? Barows { get; set; }
    }
}