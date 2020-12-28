using ABCSchool.Domain.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABCSchool.Domain.Entities
{
    [Table("Subjects")]
    public partial class Subject : IEntity
    {
        public Subject()
        {
            Students = new HashSet<Student>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 1)]
        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
