using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MilkingLogApp.Models
{
    public class MilkingLog
    {
        public int id { get; set; }
        [Required]
        public DateTime date { get; set; }
        public Boolean cycle6 { get; set; }
        public Boolean cycle12 { get; set; }
        public Boolean cycle16 { get; set; }
        public Boolean cycle21 { get; set; }
        public Decimal amount { get; set; }
    }
}