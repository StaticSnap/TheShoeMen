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

        public async Task Init(HttpClient Http)
        {
            var query = $"select colorID, sizeID, quantity from stock where shoeID = " + shoeID;

            var response = await Http.PostAsync("http://localhost:8080", new StringContent(query));

            inventory = await response.Content.ReadFromJsonAsync<List<Stock>>();
        }

        public List<(int,int,int)> GetInventory()
        {
            List<(int, int, int)> data = new List<(int, int, int)>();

            foreach(var item in inventory)
            {
                data.Add((item.colorID, item.sizeID, item.quantity));
            }

            return data;
        }
    }
}
