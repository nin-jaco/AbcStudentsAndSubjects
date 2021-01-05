using ABCSchool.Domain.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABCSchool.Domain.Entities
{
    [Table("Students")]
    public partial class Student : IEntity
    {
        public Student()
        {
            StudentSubjects = new HashSet<StudentSubject>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 1)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 1)]
        public string LastName { get; set; }
        [StringLength(150)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}
