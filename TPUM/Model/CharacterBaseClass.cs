using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Data
{
    abstract class CharacterBaseClass 
    {
        public string CharacterName { get; set; }
        public int Stamina { get; set; }
        public int Endurance { get; set; }
        public int Strength { get; set; }

    }
}
