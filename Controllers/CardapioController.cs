using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebVendaLanches.Models;

namespace WebVendaLanches.Controllers
{
    public class CardapioController : Controller
    {

        readonly CardapioRepository repository = new CardapioRepository();
        // GET: Cardapio
        public ActionResult Index()
        {
            return View(repository.GetAll());
        }
    }
}