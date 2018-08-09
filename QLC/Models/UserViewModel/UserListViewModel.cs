using System.ComponentModel.DataAnnotations;

namespace QLC.Models.UserViewModel
{
    public class UserListViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
