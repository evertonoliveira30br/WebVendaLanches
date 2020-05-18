using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebVendaLanches.Models
{
    public class Lanche
    {
        public int LancheId { get; set; }
        public string  Descricao { get; set; }
        public IEnumerable<Ingrediente> Ingredientes { get; set; }
        public decimal ValorLanche { get { return this.CalcularPrecoLanche(); } }
        public bool TemMuitoHamburguer { get; }
        public bool TemMuitoQueijo { get; }
        public bool LancheLight { get; }

        private decimal CalcularPrecoLanche()
        {
            try
            {
                return this.Ingredientes.Sum(i => i.Valor * i.Quantidade);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private decimal CalcularDescontoPromocao()
        {
            return PromocaoMuitoHamburguer();
        }

        private decimal PromocaoMuitoHamburguer()
        {
            return this.Ingredientes.Count(i => i.Descricao.ToUpper() == "HAMBÚGUER CARNE") / 3M;
        }
    }
}