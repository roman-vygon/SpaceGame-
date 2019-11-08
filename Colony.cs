using System.Collections.Generic;
using System;
namespace SpaceGame
{
    public class Colony
    {
        private string name;
        private Resources planetFertility;
        public Dictionary<BuildingType, List<Building>> buildings = new Dictionary<BuildingType, List<Building>>();        
        
        public int buildingCount { get; private set; } = 0;
        public int maxBuildings { get; private set; } = 5;
        
        public Colony(string colonyName, Resources fertility)
        {
            this.name = colonyName;
            this.planetFertility = fertility;
        }

        public string getName()
        {
            return this.name;
        }
        public int getBuildingCount(BuildingType t)
        {
            if (this.buildings.ContainsKey(t))
                return this.buildings[t].Count;
            else
                return 0;
        }        
        public void upgradeBuilding(string name, Status status)
        {
            foreach (BuildingType key in buildings.Keys)
                foreach (Building b in buildings[key])
                    if (b.name == name)
                        b.upgrade(status);
        }
        public bool createBuilding(BuildingType t, string name)
        {            
            if (buildingCount < maxBuildings)
            {
                if (t is StatueType)
                {

                    if (buildings.ContainsKey(t))
                        this.buildings[t].Add(new Statue(name, t));
                    else
                        buildings[t] = new List<Building> { new Statue(name, t) };
                    buildingCount++;
                    return true;
                }
                else if (t is MineType)
                {
                    if (buildings.ContainsKey(t))
                        this.buildings[t].Add(new MineBuilding(name, t));
                    else
                        buildings[t] = new List<Building> { new MineBuilding(name, t) };
                    buildingCount++;
                    return true;
                }
                return false;
            }
            else
                return false;
        }

        public bool destroyBuilding(BuildingType t, string name)
        {
            foreach (Building building in buildings[t])
            {
                if (building.name == name)
                { 
                    buildings[t].Remove(building);
                    return true;
                }
            }
            return false;            
        }
        public void updateBuildings(Game game)
        {
            foreach (MineType mine in game.mineTypes)
            {
                foreach (MineBuilding factory in this.buildings[mine])
                {
                    factory.mineResources(game.status, planetFertility);
                }
            }            
        }
    }
}