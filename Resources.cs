namespace SpaceGame
{
    public struct Resources
    {
        public int iron;
        public int gold;
        public int spice;

        public Resources(int _iron, int _gold, int _spice)
        {
            this.iron = _iron;
            this.gold = _gold;
            this.spice = _spice;
        }
        public override string ToString()
        {
            return "iron: " + iron.ToString() + " gold: " + gold.ToString() + " spice: " + spice.ToString();
        }
        public static Resources operator -(Resources res1,
                                         Resources res2)
        {
            Resources res3 = new Resources();
            res3.gold = res1.gold - res2.gold;
            res3.iron = res1.iron - res2.iron;
            res3.spice = res1.spice - res2.spice;            
            return res3;
        }
        public static Resources operator *(Resources res1,
                                         Resources res2)
        {
            Resources res3 = new Resources();
            res3.gold = res1.gold * res2.gold;
            res3.iron = res1.iron * res2.iron;
            res3.spice = res1.spice * res2.spice;
            return res3;
        }
    }
}