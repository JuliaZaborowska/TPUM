using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Data
{
    public enum FieldType
    {
        GRASS,
        ROCK,
        START,
    }
    interface IMap
    {
        string Name { get; }
        bool CanWalk { get; }
        FieldType Type { get; }
    }
}
