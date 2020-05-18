using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebVendaLanches.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public DateTime DataHoraPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public IEnumerable<Lanche> Lanches { get; set; }       

        public decimal CalcularValorTotalPedido(Pedido pedido)
        {
            try
            {
                return pedido.Lanches.Sum(l => l.ValorLanche);
            }
            catch (Exception)
            {

                throw;
            }           
        }

        public IEnumerable<Ingrediente> Ingredientes(int LancheId)
        {
            var ingredienteRepository = new IngredienteRepository();

            return ingredienteRepository.GetAll().Where(l => l.LancheId == LancheId);


        }
    }
    
 }