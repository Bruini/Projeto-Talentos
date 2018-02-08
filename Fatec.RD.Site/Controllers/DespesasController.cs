using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fatec.RD.Site.Controllers
{
    public class DespesasController : Controller
    {
        // GET: Despesas
        public ActionResult Index()
        {
            return View();
        }
    }
}