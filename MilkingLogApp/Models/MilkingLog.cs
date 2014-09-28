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

        internal bool IsSubsetOf(MilkingLog milkingLog)
        {
            if (this.cycle6 && !milkingLog.cycle6) return false;
            if (this.cycle12 && !milkingLog.cycle12) return false;
            if (this.cycle16 && !milkingLog.cycle16) return false;
            if (this.cycle21 && !milkingLog.cycle21) return false;
            return true;
        }

        internal bool IsOverlapingWith(MilkingLog milkingLog)
        {
            if (this.cycle6 && milkingLog.cycle6) return true;
            if (this.cycle12 && milkingLog.cycle12) return true;
            if (this.cycle16 && milkingLog.cycle16) return true;
            if (this.cycle21 && milkingLog.cycle21) return true;
            return false;
        }
    }
}