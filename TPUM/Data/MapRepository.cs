using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Data
{
    class MapRepository
    {
        private List<IMap> MapList = new List<IMap>();
        private IMap AddField(FieldType type)
        {
            IMap map;
            switch (type)
            {
                case FieldType.GRASS: map = new Grass(); 
                    break;
                case FieldType.ROCK: map = new Rock(); 
                    break;
                case FieldType.START: map = new StartingPoint(); 
                    break;
                default: map = new Grass(); 
                    break;
            }
            MapList.Add(map); //add it to repository
            Console.WriteLine("Created new instance of {0}", map.Name);
            return map;
        }
        public IMap GetField(FieldType type)
        {
            IMap map = MapList.Find(x => x.Type == type);
            if (map != null) return map;
            else return AddField(type);
        }
    }
}
