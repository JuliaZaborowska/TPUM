using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Data
{
    class Human : CharacterBaseClass
    {
        public Human(string characterName, int stamina, int endurance, int strength)
        {
            this.CharacterName = characterName;
            this.Stamina = stamina;
            this.Endurance = endurance;
            this.Strength = strength;
        }

    }
}
