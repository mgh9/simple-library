using FinLib.Models.Attributes;

namespace FinLib.Models.Configs
{
    [IgnoreTypewriterMapping]
    public sealed class SeedUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
        
        public bool LockoutEnabled { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsLibrarian{ get; set; }

        public bool IsActive { get; set; }
    }
}
