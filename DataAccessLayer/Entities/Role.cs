using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class Role : BaseEntity
    {
        [MaxLength(20)]
        [Required]
        public string RoleName { get; set; }
        public ICollection<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }
}
