using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace RetailMVCWebEF.Models
{
    public class ApplicationUser : IdentityUser
    {
        //public AspNetUsers LocalUser { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            //LocalUser = UserRepository.Find(userIdentity.GetUserId());
            return userIdentity;
        }
    }
    public  class GestionHosteleriaGenNHibernateEntities1 : IdentityDbContext<ApplicationUser>
    {
        public GestionHosteleriaGenNHibernateEntities1()
            : base("DefaultConnection2")
        {
        }

        public static GestionHosteleriaGenNHibernateEntities1 Create()
        {
            return new GestionHosteleriaGenNHibernateEntities1();
        }

    }
}