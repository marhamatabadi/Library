namespace Library.Data
{
    public class Barow
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? WasDelivered { get; set; }
        public virtual Member? Member { get; set; }
        public virtual Book? Book { get; set; }
    }
}
