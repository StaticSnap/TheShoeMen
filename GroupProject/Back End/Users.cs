using static System.Net.WebRequestMethods;
using System.Net.Http.Json;
using System.Text.Json;

namespace GroupProject
{
    public static class Users
    {
        private static List<User> users = new List<User>();

        public async static Task Init(HttpClient Http)
        {
            // If the users have been loaded before. then don't bother loading them again.
            if (users.Count != 0)
            {
                return;
            }
            var query = $"select * from users";

            var response = await Http.PostAsync("http://localhost:8080", new StringContent(query));

            users = await response.Content.ReadFromJsonAsync<List<User>>();
        }

        public static List<User> GetUsers()
        {
            return users;
        }

        public static User GetUserFromName(string des_username)
        {
            if (users.Count > 0) {
                foreach (var user in users)
                {
                    if (user.username == des_username)
                    {
                        return user;
                    }
                }
                throw new Exception("User not found");
            }
            else {
                // Create and return a dummy user with default values
                return new User {
                    username = "guest",
                    password = "password",
                    user_role = "customer"
                };
            }
        }
    }
}
