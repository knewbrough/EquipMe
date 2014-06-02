using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EquipMe.Models
{
    public class ArmorItem
    {
        public int ID { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Slot { get; set; }
        public int UpgradeMax { get; set; }
        public decimal Weight { get; set; }
        public int Physical { get; set; }
        public int Strike { get; set; }
        public int Slash { get; set; }
        public int Pierce { get; set; }                        
        public int Magic { get; set; }
        public int Fire { get; set; }
        public int Lightning { get; set; }
        public int Dark { get; set; }
        public decimal Poise { get; set; }
        public int Poison { get; set; }
        public int Bleed { get; set; }
        public int Petrify { get; set; }
        public int Curse { get; set; }        
        public string SetName { get; set; }

    }
}