namespace ReviewYourMovie.Server.Models
{
    public class UserHashPassword
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        //public byte[] PasswordHash { get; set; }
        //public byte[] PasswordSalt { get; set; }
    }
}
