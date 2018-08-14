using System;
using System.ComponentModel.DataAnnotations;

namespace Request.Models
{
    public class Requests
    {
        [Required]
        [Key]
        public int RequestId { get; set; }
        public string ResquestName { get; set; }
        public string ContentName { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        [Required]
        public int StateId { get; set; }
        public bool Deleted { get; set; }
        [Required]
        public string UserId { get; set; }

           
    }
}
