namespace GroupProject
{
    using System.Text.Json.Serialization;
    public class User
    {
        public required string username {  get; set; }
        public required string password { get; set; }
        public required string user_role { get; set; }
    }
}
