using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Data
{
    class Mob : CharacterBaseClass
    {
        public Mob(int stamina, int endurance, int strength)
        {
            CharacterName = "Mob";
            this.Stamina = stamina;
            this.Endurance = endurance;
            this.Strength = strength;
        }

    }
}
