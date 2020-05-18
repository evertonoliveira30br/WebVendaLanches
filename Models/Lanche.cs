using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Configuration;
using WebVendaLanches.Models.Interfaces;

namespace WebVendaLanches.Models
{
    public class Lanche
    {
        public int LancheId { get; set; }
        public string  Descricao { get; set; }
        public IEnumerable<Ingrediente> Ingredientes { get; set; }
        public decimal ValorLanche { get { return this.CalcularPrecoLanche(); } }
        public bool TemMuitoHamburguer { get; private set; }
        public bool TemMuitoQueijo { get; private set; }
        public bool LancheLight { get; private set; }

        public bool LanchePersonalizado { get; set; }

        private const int promocaoPorcaoHamburguerCarne = 3;
        private const int promocaoPorcaoQueijo = 3;
        private const decimal percentualDescontoLancheLight = .1M;

        private decimal CalcularPrecoLanche()
        {
            try
            {
                decimal valor = this.Ingredientes.Sum(i => i.Valor * i.Quantidade) - this.CalcularPromocaoPorcoes();

                return valor - PromocaoLight();
             
            }
            catch (Exception)
            {

                throw;
            }
        }

        private decimal CalcularPromocaoPorcoes()
        {
            return PromocaoMuitoHamburguer() + PromocaoMuitoQueijo();            
        }

        private decimal PromocaoMuitoHamburguer()
        {
            Ingrediente hamburguer = Ingredientes.FirstOrDefault(i =>
            {
                return i.Descricao.ToUpper() == "HAMBÚRGUER DE CARNE";
            });
            
            int qtdePorcoesPromocao = 0;

            TemMuitoHamburguer = false;

            if (hamburguer != null)
            {
                for (int i = 1; i <= hamburguer.Quantidade; i++)
                {
                    var calculo1 = i % promocaoPorcaoHamburguerCarne;

                    if (calculo1 == 0)
                    {
                        qtdePorcoesPromocao++;
                        TemMuitoHamburguer = true;
                    }

                }

                var desconto = qtdePorcoesPromocao * hamburguer.Valor;

                return desconto;

            }
            else
                return 0;

            
        }

        private decimal PromocaoMuitoQueijo()
        {
            var queijo = Ingredientes.FirstOrDefault(i => i.Descricao.ToUpper() == "QUEIJO");

            int qtdePorcoesPromocao = 0;

            TemMuitoQueijo = false;

            if (queijo != null)
            {
                for (int i = 1; i <= queijo.Quantidade; i++)
                {
                    var calculo1 = i % promocaoPorcaoQueijo;

                    if (calculo1 == 0)
                    {
                        qtdePorcoesPromocao++;
                        TemMuitoQueijo = true;
                    }

                }

                var desconto = qtdePorcoesPromocao * queijo.Valor;

                return desconto;

            }
            else
                return 0;

        }

        private decimal PromocaoLight()
        {
            var alface = Ingredientes.FirstOrDefault(i => i.Descricao.ToUpper() == "ALFACE");
            var bacon = Ingredientes.FirstOrDefault(i => i.Descricao.ToUpper() == "BACON");

            int QuantidadeAlface = alface == null ? 0 : alface.Quantidade;
            int QuantidadeBacon = bacon == null ? 0 : bacon.Quantidade;

            if (QuantidadeAlface > 0 && QuantidadeBacon == 0)
                return this.ValorLanche * percentualDescontoLancheLight;
            else
                return 0;
        }
    }


    public class LancheRepository : IRepository<Lanche>
    {
        protected string StringConnection { get; } = WebConfigurationManager.ConnectionStrings["LanchesContext"].ConnectionString;

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lanche> GetAll()
        {
            throw new NotImplementedException();
        }

        public Lanche GetById(int id)
        {
            string sql = "SP_RECUPERA_LANCHE_POR_ID";
            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("LANCHEID", SqlDbType.Int) { Value=id });
                                
                Lanche p = new Lanche();
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {                            
                            p.LancheId = (int)reader["LancheId"];
                            p.Descricao = reader["Descricao"].ToString();
                            p.LanchePersonalizado = (bool)reader["LanchePersonalizado"];                            
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                
                return p;
            }
        }

        public void Save(Lanche entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Lanche entity)
        {
            throw new NotImplementedException();
        }

    }
}