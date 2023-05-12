using RetailMVCWebEF.Models.BL;
using System.Web.Mvc;

namespace RetailMVCWebEF.Controllers
{
    public class BaseController : Controller
    {
        protected Application Application => this.HttpContext.ApplicationInstance as Application;
    }
}