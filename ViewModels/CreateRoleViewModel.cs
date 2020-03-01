using System.ComponentModel.DataAnnotations;
namespace Project_Vacation_Manager.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}
