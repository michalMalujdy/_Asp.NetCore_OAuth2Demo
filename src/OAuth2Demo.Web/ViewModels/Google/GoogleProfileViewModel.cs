namespace OAuth2Demo.Web.ViewModels.Google
{
    public class GoogleProfileViewModel
    {
        public string GoogleId { get; set; }

        public string PictureUrl { get; set; }

        public string Email { get; set; }

        public bool IsEmailVerified { get; set; }
    }
}