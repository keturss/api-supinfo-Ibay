namespace ProjetWebAPI.Models.Inputs
{
    public class UserUpdateInput
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string RefreshToken { get; set; }
    }
}
