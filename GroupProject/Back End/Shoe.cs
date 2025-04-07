namespace GroupProject
{
    using System.Text.Json.Serialization;
    public class Shoe
    {
        public int shoeID {  get; set; }
        public double price { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }
        public string imagePath { get; set; }
        [JsonConverter(typeof(BoolFromIntConverter))]
        public bool isBookmark {  get; set; }

    }
}
