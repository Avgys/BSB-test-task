using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        [NotMapped]
        private int roleId = 0;
        public int RoleId { 
            get {
                if (Role == null)
                {
                    return roleId;
                }
                else 
                {
                    return Role.Id;
                }
            }
            set => roleId = value ; }
        public Role Role { get; set; }
        [NotMapped]
        public string RoleName { get => Role.Name; }
    }
}