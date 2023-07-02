using System.Collections;

namespace Library.Data
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Barow>? Barows { get; set; }
    }
}