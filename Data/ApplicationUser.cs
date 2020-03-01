using Microsoft.AspNetCore.Identity;
namespace Project_Vacation_Manager.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Team { get; set; }
    }
}
