namespace LinuxLudo.Web.Models
{
    public class AuthenticatedUserModel
    {
        // Token retrieved from API when signed in to identify the user
        public string AccessToken { get; set; }
    }
}