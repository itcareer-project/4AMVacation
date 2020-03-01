using System.Collections.Generic;
namespace Project_Vacation_Manager.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public List<Team> Teams { get; set; }        
    }
}
