using Microsoft.AspNetCore.Mvc;
using System;

namespace Buriti_Store.WebApp.MVC.Controllers
{
    public class BaseController : Controller
    {
        protected Guid ClientId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");
    }
}
