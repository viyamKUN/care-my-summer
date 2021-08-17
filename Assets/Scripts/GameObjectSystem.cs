namespace GameObjectSystem
{

    public class EnvObject
    {
        public string Name;
        public int Price;
        public int MiningAmount;

        public EnvObject(string name, int price, int mining)
        {
            this.Name = name;
            this.Price = price;
            this.MiningAmount = mining;
        }
    }
    public class Item
    {
        public string Name;
        public string Info;
        public int Price;

        public Item(string name, string info, int price)
        {
            this.Name = name;
            this.Info = info;
            this.Price = price;
        }
    }
}
