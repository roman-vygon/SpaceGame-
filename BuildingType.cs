namespace SpaceGame
{
    public class BuildingType
    {
        public Resources cost { get; protected set; }
        public string Name { get; protected set; }
        public int maxLevel { get; protected set; }
    }
}