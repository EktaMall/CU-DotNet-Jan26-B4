using System.ComponentModel.DataAnnotations;

namespace CarManagementMVCApp.Models
{
    public class ManageRoleViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }

        [Required]
        public string SelectedRole { get; set; }

        public List<string> Roles { get; set; } = new List<string>();
    }
}
