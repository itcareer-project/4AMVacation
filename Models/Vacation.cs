using System;
using System.ComponentModel.DataAnnotations;
namespace Project_Vacation_Manager.Models
{
    public class Vacation    {
        public int Id { get; set; }
        public string User { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartVac { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndVac { get; set; }
        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }
        public Boolean Accepted { get; set; }
    }
}
