using FinLib.Models.Base;
using FinLib.Models.Base.View;
using FinLib.Models.Enums;

namespace FinLib.Models.Views.SEC
{
    public class UserView : UpdatableView
    {
        [ViewColumn("Username")]
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [ViewColumn("Fullname")]
        public string FullName { get; set; }

        [ViewColumn("Mobile")]
        public string Mobile { get; set; }

        [ViewColumn("Is Active?")]
        public bool IsActive { get; set; }

        public DateTime? LockoutEnd { get; set; }
        public string Email { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? LastLoggedInTime { get; set; }
    }
}
