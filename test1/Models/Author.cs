using System.ComponentModel.DataAnnotations;

namespace test1.Models
{
    public class Author
    {

            [Key]
            public Guid Id { get; set; }
            public string? Name { get; set; }
            public DateTime? DateOfBirth { get; set; }

            // One-to-many relationship with books
            public List<book>? Books { get; set; }
        }
    }
