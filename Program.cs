using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Media;

namespace SpaceGame
{
    public class Game
    {
        public Status status = new Status();
        private List<Planet> planets = new List<Planet>();
        public List<MineType> mineTypes;
        public StatueType statue = new StatueType("Religious statue", new Resources(1000,1000,1000), 1);
        
        static void Main(string[] args)
        {
            SoundPlayer sp = new SoundPlayer("soundtrack.wav");            
            sp.PlayLooping();            
            Game game = new Game();
            game.setTimer();          
            
            game.generateGamePreset();
            string readMeText;
            using (StreamReader readtext = new StreamReader("logo.txt"))
            {
                readMeText = readtext.ReadToEnd();
                Console.Write(readMeText);
                Console.WriteLine("");
            }
            int cnt = 0;
            while (true)
            {
                String cmd;
                cmd = Console.ReadLine();

                if (cmd == "exit")
                    break;
                if (cnt == 2)
                {
                    Console.Clear();
                    Console.Write(readMeText);
                    Console.WriteLine("");
                    cnt = 0;
                }
                cnt++;
                switch (cmd)
                {
                    case "back":
                        game.status.goBack();
                        break;

                    case "create planet":
                        Console.WriteLine("enter planet name:");
                        string createName = Console.ReadLine();

                        Console.WriteLine("enter planet coordinates 'x y z' - float):");
                        string coords = Console.ReadLine();
                        string[] numbers = coords.Split(' ');
                        double x = Convert.ToDouble(numbers[0]);
                        double y = Convert.ToDouble(numbers[1]);
                        double z = Convert.ToDouble(numbers[2]);

                        Console.WriteLine("enter planet fertility 'iron gold spice' - integer):");
                        string fert = Console.ReadLine();
                        string[] fertility = fert.Split(' ');
                        int iron = Convert.ToInt32(fertility[0]);
                        int gold = Convert.ToInt32(fertility[1]);
                        int spice = Convert.ToInt32(fertility[2]);

                        game.addPlanet(createName, x, y, z, iron, gold, spice);

                        Console.WriteLine("planet created");
                        break;

                    case "add colony":
                        Console.WriteLine("enter colony name:");
                        string newColonyName = Console.ReadLine();

                        game.addColony(newColonyName);

                        break;

                    case "move to planet":
                        Console.WriteLine("enter planet name:");
                        string moveName = Console.ReadLine();
                        if (game.status.setDestination(moveName, game.planets))
                            Console.WriteLine("Set destination to " + moveName);
                        else
                            Console.WriteLine("No such planet");
                        break;

                    case "move to colony":
                        Console.WriteLine("enter colony name:");
                        string colonyName = Console.ReadLine();
                        game.status.moveToColony(colonyName);
                        break;
                    case "upgrade building":
                        Console.WriteLine("enter building name");
                        if (game.status.currentColony == null)
                            Console.WriteLine("Enter a colony first");
                        else
                        {
                            string name = Console.ReadLine();
                            game.status.currentColony.upgradeBuilding(name, game.status);
                        }
                        break;

                    case "info":
                        game.status.getCurrentInfo(game.planets, game.mineTypes, new List<StatueType> { game.statue });
                        break;

                    case "destroy planet":
                        Console.WriteLine("enter planet name:");
                        string destroyingPlanetName = Console.ReadLine();
                        game.destroyPlanet(destroyingPlanetName);
                        break;

                    case "destroy colony":
                        Console.WriteLine("enter colony name:");
                        string destroyingColonyName = Console.ReadLine();
                        game.destroyColony(destroyingColonyName);
                        break;
                    case "build mine":                        
                        Colony colony = game.status.currentColony;
                        if (colony == null)
                            Console.WriteLine("Not in a colony");
                        else
                        {
                            if (colony.buildingCount < colony.maxBuildings)
                            {
                                foreach (MineType t in game.mineTypes)
                                    Console.WriteLine(t.Name + " Cost: " + t.cost.ToString());
                                Console.WriteLine("Enter mine type");
                                string name = Console.ReadLine();
                                foreach (MineType t in game.mineTypes)
                                {
                                    if (t.Name == name)
                                    {
                                        if (game.status.payResources(t.cost))
                                        {
                                            Console.WriteLine("Enter mine name");
                                            name = Console.ReadLine();
                                            colony.createBuilding(t, name);
                                            Console.WriteLine("Factory built");
                                            break;
                                        }                                        
                                    }
                                }
                            }
                            else
                                Console.WriteLine("Max buildings capacity");
                        }

                        break;

                    case "build statue":                        
                        Colony cur_colony = game.status.currentColony;
                        if (cur_colony == null)
                            Console.WriteLine("Not in a colony");
                        else
                        {
                            Console.WriteLine("enter statue name:");
                            string Name = Console.ReadLine();                            
                            if (game.status.payResources(game.statue.cost))
                            {
                                cur_colony.createBuilding(game.statue, Name);
                                Console.WriteLine("Statue built");
                            }
                            else                            
                                Console.WriteLine("Insufficient resources");                            
                        }
                        break;

                    default:
                        Console.WriteLine("unknown command");
                        break;
                }
            }
        }

        public void addPlanet(string name, double x, double y, double z, int iron, int gold, int spice)
        {
            Coordinates coord1 = new Coordinates(x,y,z);
            Resources res1 = new Resources(iron,gold, spice);
            Planet planet1 = new Planet(name, coord1, res1);
            planets.Add(planet1);
        }

        public void addColony(string name)
        {
            Planet curPlanet = status.currentPlanet;

            if (curPlanet != null)
                curPlanet.setColony(name);
            else
                Console.WriteLine("you must be on planet");
        }

        public void destroyPlanet(string name)
        {
            if (status.currentPlanet != null)
            {
                Console.WriteLine("you must be in space. use command 'back'");
                return;
            }

            foreach (Planet planet in planets)
            {
                if (planet.getName() == name)
                {
                    planets.Remove(planet);
                    Console.WriteLine("destroyed");
                    return;
                }
            }

            Console.WriteLine("planet " + name + " doesnt exist");
        }

        public void destroyColony(string name)
        {
            if (status.currentColony != null || status.currentPlanet == null)
            {
                Console.WriteLine("you must be on planet");
                return;
            }

            status.currentPlanet.destroyColony(name);
        }

        private void generateGamePreset()
        {
            Coordinates coord1 = new Coordinates(-12.3463, 457.534, -9042.0);
            Resources res1 = new Resources(8, 1, 0);
            Planet planet1 = new Planet("Tuchanka", coord1, res1);
            planet1.setColony("Krogans_shit");

            Coordinates coord2 = new Coordinates(63.1111, 684.425, 562.4);
            Resources res2 = new Resources(1, 4, 12);
            Planet planet2 = new Planet("Earth", coord2, res2);
            planet2.setColony("Tomsk");
            planet2.setColony("Moscow");

            Coordinates coord3 = new Coordinates(-257.89, 78.9235, 245.206);
            Resources res3 = new Resources(2, 2, 8);
            Planet planet3 = new Planet("Alpha Centauri 4", coord3, res3);
            planet3.setColony("no");

            this.planets = new List<Planet> { planet1, planet2, planet3 };

            Dictionary<int, Resources> levels1 = new Dictionary<int, Resources> {

                [1] = new Resources(1, 1, 1),
                [2] = new Resources(2, 2, 2),
                [3] = new Resources(3, 3, 3)
            };

            Dictionary<int, Resources> levels2 = new Dictionary<int, Resources>
            {

                [1] = new Resources(2, 2, 2),
                [2] = new Resources(4, 4, 4),
                [3] = new Resources(6, 6, 6)
            };

            Dictionary<int, Resources> levels3 = new Dictionary<int, Resources>
            {

                [1] = new Resources(4, 4, 4),
                [2] = new Resources(8, 8, 8),
                [3] = new Resources(12, 12, 12)
            };

            MineType type1 = new MineType(levels1, "Small mine", new Resources(10,10,10), 3);
            MineType type2 = new MineType(levels1, "Medium mine", new Resources(50, 50, 50), 3);
            MineType type3 = new MineType(levels1, "Large mine", new Resources(100, 100, 100), 3);

            this.mineTypes = new List<MineType> { type1, type2, type3 };            
        }

        private void setTimer()
        {
            TimerCallback tm = new TimerCallback(tick);
            Timer timer = new Timer(tm, null, 1000, 10000); // mine 1 iteration per 10 sec            
        }

        public void tick(object obj)
        {
            foreach (Planet planet in planets)
                foreach (Colony colony in planet.getColonies())
                    foreach (MineType t in mineTypes)
                        if (colony.getBuildingCount(t) > 0)
                            foreach (MineBuilding b in colony.buildings[t])
                                b.mineResources(this.status, planet.getFertility());
            status.Move();

        }
    }
}
