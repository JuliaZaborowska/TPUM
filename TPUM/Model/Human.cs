using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Data
{
    class Human : CharacterBaseClass
    {
        public Human(int stamina, int endurance, int strength)
        {
            CharacterName = "Human";
            this.Stamina = stamina;
            this.Endurance = endurance;
            this.Strength = strength;
        }

    }
}
