﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Project_Vacation_Manager.ViewModels
{
    public class EditUserViewModel
    {        public EditUserViewModel()
        {
            Roles = new List<string>();
        }
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Team { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<string> Roles { get; set; }
    }
}
