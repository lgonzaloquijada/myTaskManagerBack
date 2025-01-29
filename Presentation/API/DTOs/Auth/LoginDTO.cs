namespace API.DTOs.Auth
{
    public class LoginDto
    {
        public string email { get; set; }
        public string password { get; set; }

        public LoginDto(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public LoginDto()
        {
            email = string.Empty;
            password = string.Empty;
        }
    }
}