namespace GroupProject
{
    using System.Text.Json.Serialization;
    public class Shoe
    {
        public int shoeID {  get; set; }
        public double price { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }
        [JsonConverter(typeof(BoolFromIntConverter))]
        public bool isBookmark {  get; set; }

        private List<Stock> inventory = new List<Stock>();

        public List<(int,int,int)> GetInventory()
        {
            List<(int, int, int)> data = new List<(int, int, int)>();

            foreach(var item in inventory)
            {
                data.Add((item.colorID, item.sizeID, item.quantity));
            }

            return data;
        }

        public void AddStock(Stock stock)
        {
            inventory.Add(stock);
        }
    }
}
