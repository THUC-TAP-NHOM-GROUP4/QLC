using System.ComponentModel.DataAnnotations;

namespace QLC.Models.UserViewModel
{
    public class ApplicationRoleViewModel
    {
        public string Id { get; set; }
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool IsAdmin { get; set; }
    }
}
