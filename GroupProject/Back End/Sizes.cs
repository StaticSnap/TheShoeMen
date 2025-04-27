namespace GroupProject
{
    public static class Sizes
    {
        private static Dictionary<int, string> sizes = new Dictionary<int, string>();

        public async static Task Init(HttpClient Http)
        {
            var query = $"select * from size";

            var response = await Http.PostAsync("http://localhost:8080", new StringContent(query));

            List<Size> data = await response.Content.ReadFromJsonAsync<List<Size>>();

            foreach (var item in data)
            {
                sizes[item.SizeID] = item.sizeValue;
            }

            return;
        }

        public static string GetSizeFromID(int id)
        {
            if (sizes.ContainsKey(id))
            {
                return sizes[id];
            }
            else
            {
                return "NULL";
            }
        }
    }
}
