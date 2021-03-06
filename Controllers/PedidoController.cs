﻿using Antlr.Runtime.Tree;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebVendaLanches.Models;

namespace WebVendaLanches.Controllers
{
    public class PedidoController : Controller
    {
        private Pedido pedido = new Pedido();

        // GET: Pedido
        public ActionResult Index(int lancheId)
        {
            LancheRepository repositoryLanche = new LancheRepository();

            ViewBag.NomeLanche = repositoryLanche.GetById(lancheId).Descricao;

            return View(pedido.Ingredientes(lancheId));
        }

        [HttpPost]
        public JsonResult CalcularPrecoLanche(List<Ingrediente> ingredientes)
        {
            var lanche = new Lanche
            {
               LancheId = 0,
               Ingredientes=ingredientes
            };        
            
            return Json(lanche, JsonRequestBehavior.AllowGet);
        }
    }
}