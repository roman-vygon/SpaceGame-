using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    class MineBuilding : Building
    {
        public MineBuilding(string _name, BuildingType t)
        {
            this.name = _name;
            this.type = t;
        }
        public void mineResources(Status status, Resources fertility)
        {
            MineType type = this.type as MineType;
            status.addResources(type.levels[this.level]*fertility);
        }
        public override string ToString()
        {
            return "Name: " + name + " Level: " + level + " Type : " + type.Name;
        }
    }
}
