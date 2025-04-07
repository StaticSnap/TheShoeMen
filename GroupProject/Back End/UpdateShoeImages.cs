using System.Net.Http.Json;

namespace GroupProject
{
    public static class UpdateShoeImages
    {
        public static async Task UpdateImages(HttpClient httpClient)
        {
            try
            {
                var response = await httpClient.PostAsync("http://localhost:8080/api/update-shoe-images", null);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Shoe images updated successfully!");
                }
                else
                {
                    Console.WriteLine($"Error updating shoe images: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while updating shoe images: {ex.Message}");
            }
        }
    }
} 