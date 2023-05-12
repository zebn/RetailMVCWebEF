using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RetailMVCWebEF.Models.ML;

namespace RetailMVCWebEF.Models.VL
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            this.AspNetUserClaims = new HashSet<AspNetUserClaim>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public Nullable<System.DateTimeOffset> LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

      
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }

        public AspNetUser Model(AspNetUser Usd)
        {
            if (Usd != null)
            {
                Usd.AccessFailedCount = this.AccessFailedCount;
                Usd.Email = this.Email;
                Usd.EmailConfirmed = this.EmailConfirmed;
                Usd.Id = this.Id;
                Usd.LockoutEnabled = this.LockoutEnabled;
                Usd.PasswordHash = this.PasswordHash;
                Usd.PhoneNumber = this.PhoneNumber;
                Usd.PhoneNumberConfirmed = this.PhoneNumberConfirmed;
                Usd.SecurityStamp = this.SecurityStamp;
                Usd.TwoFactorEnabled = this.TwoFactorEnabled;
            }

            return Usd;
        }

        public UserViewModel View(AspNetUser Usd, Boolean GetInstanceIfIsNull = true)
        {
            if (Usd != null)
            {
                this.AccessFailedCount = Usd.AccessFailedCount;
                this.Email = Usd.Email;
                this.EmailConfirmed = Usd.EmailConfirmed;
                this.Id = Usd.Id;
                this.LockoutEnabled = Usd.LockoutEnabled;
                this.PasswordHash = Usd.PasswordHash;
                this.PhoneNumber = Usd.PhoneNumber;
                this.PhoneNumberConfirmed = Usd.PhoneNumberConfirmed;
                this.SecurityStamp = Usd.SecurityStamp;
                this.TwoFactorEnabled = Usd.TwoFactorEnabled;
                this.UserName = Usd.UserName;
                return this;
            }
            else if (GetInstanceIfIsNull)
                return this;
            else
                return null;
        }

        public AspNetUser Model()
        {
            return this.Model(new AspNetUser());
        }

        public UserViewModel View()
        {
            return this.View(new AspNetUser());
        }
    }
}
