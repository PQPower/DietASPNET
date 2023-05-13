using DataAccessLayer.Enums;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public LifeStyle LifeStyle { get; set; }
        public int Height{ get; set; }
        public ICollection<UserWeightHistory> UserWeightHistorys { get; set; }
        public User()
        {
            UserWeightHistorys = new List<UserWeightHistory>();
        }

    }
}
