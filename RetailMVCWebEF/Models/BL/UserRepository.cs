using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RetailMVCWebEF.Models.ML;
using RetailMVCWebEF.Models.VL;

namespace RetailMVCWebEF.Models.BL
{
    public class UserRepository
    {
        public static UserViewModel ViewModelFind(String id)
    {
            ML.GestionHosteleriaGenNHibernateEntities1 db = new ML.GestionHosteleriaGenNHibernateEntities1();
        AspNetUser Usd = db.AspNetUsers.Find(id);
        if (Usd != null)
            return new UserViewModel().View(Usd);
        else
            return null;
    }

    public async static Task<UserViewModel> ViewModelFindAsync(String id)
    {
            ML.GestionHosteleriaGenNHibernateEntities1 db = new ML.GestionHosteleriaGenNHibernateEntities1();
        return new UserViewModel().View(await db.AspNetUsers.FindAsync(id), false);
    }

    public static IQueryable<UserViewModel> ViewModelListSet(Guid? EntidadId)
    {
        ML.GestionHosteleriaGenNHibernateEntities1 DB = new ML.GestionHosteleriaGenNHibernateEntities1();
        return DB.AspNetUsers.Select(c => new UserViewModel()
        {
            Id = c.Id,      
            LockoutEnabled = c.LockoutEnabled,
            UserName = c.UserName
        });
    }

    public static List<UserViewModel> ViewModelList(Guid? EntidadId)
    {
        return ViewModelListSet(EntidadId).ToList();
    }

   /* public static List<AspNetUser> List(String[] Roles)
    {
            GestionHosteleriaGenNHibernateEntities1 db = new GestionHosteleriaGenNHibernateEntities1();
        return db.AspNetUsers.Where(u => !Roles.Any() || u.AspNetRole.Where(r => Roles.Contains(r.Id)).Any()).ToList();
    }*/
    public static List<AspNetUser> ListEnabled(Guid[] Entidades, Guid? EntidadId)
    {
            ML.GestionHosteleriaGenNHibernateEntities1 DB = new ML.GestionHosteleriaGenNHibernateEntities1();
        return DB.AspNetUsers.Where(u => u.LockoutEnabled == false).ToList();
    }
   /* public static List<AspNetUsers> ListClienteRole(Guid? EntidadId)
    {
        Entities DB = new Entities();
        return DB.AspNetUsers.Where(u => (EntidadId == null || u.EntidadId.Value.Equals(EntidadId.Value)) && u.LockoutEnabled == false && u.UserTypeClient == true).ToList();
    }*/
    public static AspNetUser Find(String id)
    {
            ML.GestionHosteleriaGenNHibernateEntities1 db = new ML.GestionHosteleriaGenNHibernateEntities1();
        return db.AspNetUsers.Find(id);
    }

    }
  

}