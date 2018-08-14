using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QLC.Models.RepuestViewModel
{
    public class RequestViewModel
    {
        public int RequestId { get; set; }
        public string ResquestName { get; set; }
        public string ContentName { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public string Note { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int StateId { get; set; }
        public bool Deleted { get; set; }
        public string UserId { get; set; }
        public string StatusName { get; set; }
        public decimal Money { get; set; }
    }
}
