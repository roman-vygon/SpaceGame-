using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public class MineType : BuildingType
    {
        public Dictionary<int, Resources> levels{ get; private set; }
        public MineType (Dictionary<int, Resources> _levels, string _name, Resources _cost, int _maxLevel)
        {
            this.levels = _levels;
            this.Name = _name;
            this.cost = _cost;
            this.maxLevel = _maxLevel;
        }
    }
}
