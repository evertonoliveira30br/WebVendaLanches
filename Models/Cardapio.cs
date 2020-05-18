using Antlr.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using WebVendaLanches.Models.Interfaces;

namespace WebVendaLanches.Models
{
    public class Cardapio
    {
        public int LancheId { get; set; }
        public string Lanche { get; set; }
        public string Ingredientes { get; set; }
        public decimal PrecoLanche { get; set; } 
        
        public bool LanchePersonalizado { get; set; }
        
    }

    public class CardapioRepository : IRepository<Cardapio>
    {
       protected string StringConnection { get; } = WebConfigurationManager.ConnectionStrings["LanchesContext"].ConnectionString;

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cardapio> GetAll()
        {
            string sql = "SP_RECUPERA_CARDAPIO";
            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                List<Cardapio> list = new List<Cardapio>();
                Cardapio p = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            p = new Cardapio();
                            p.Ingredientes = reader["Ingredientes"].ToString();
                            p.Lanche = reader["Lanche"].ToString();
                            p.LancheId = (int)reader["LancheId"];
                            p.PrecoLanche = (decimal)reader["PrecoLanche"];
                            p.LanchePersonalizado = (bool)reader["LanchePersonalizado"];

                            list.Add(p);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                return list;
            }
        }

        public Cardapio GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Cardapio entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Cardapio entity)
        {
            throw new NotImplementedException();
        }      

    }
}