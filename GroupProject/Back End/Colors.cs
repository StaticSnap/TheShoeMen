using GroupProject.Components.Pages;

namespace GroupProject
{
    public static class Colors
    {
        private static Dictionary<int, string> colors = new Dictionary<int, string>();

        public async static Task Init(HttpClient Http)
        {
            var query = $"select * from color";

            var response = await Http.PostAsync("http://localhost:8080", new StringContent(query));

            List<Color> data = await response.Content.ReadFromJsonAsync<List<Color>>();

            foreach (var item in data)
            {
                colors[item.ColorID] = item.colorName;
            }

            return;
        }

        public static string GetColorFromID(int id)
        {
            if (colors.ContainsKey(id))
            {
                return colors[id];
            }
            else
            {
                return "NULL";
            }
        }
    }
}
