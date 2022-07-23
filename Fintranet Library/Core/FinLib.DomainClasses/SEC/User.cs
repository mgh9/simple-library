using FinLib.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinLib.DomainClasses.SEC
{
    public class User : IdentityUser<int>, Base.IUpdatableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        
        public string Mobile { get; set; }
        public string ImageUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoggedInTime { get; set; }
        public string LockoutDescription { get; set; }

        [JsonIgnore]
        public override string PasswordHash { get => base.PasswordHash; set => base.PasswordHash = value; }

        [ForeignKey("UpdatedByUserRole")]
        public int? UpdatedByUserRoleId { get; set; }
        public SEC.UserRole UpdatedByUserRole { get; set; }
        public DateTime? UpdateDate { get; set; }

        [ForeignKey("CreateByUserRole")]
        public int? CreatedByUserRoleId { get; set; }
        public SEC.UserRole CreateByUserRole { get; set; }
        public DateTime CreateDate { get; set; }

        public override string ToString()
        {
            return UserName;
        }
    }
}