using momkitchen.Models;

namespace momkitchen.Mapper
{
    public class AuthenticationResult
    {
        public bool IsAuthenticated { get; set; }
        public Account User { get; set; }
    }
}
