using System;
namespace Project_Vacation_Manager.Models
{
    public class ErrorViewModel
    {
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string RequestId { get; set; }
    }
}
