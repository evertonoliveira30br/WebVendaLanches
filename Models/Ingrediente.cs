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
    public class Ingrediente
    {
        public int IngredienteId { get; set; }
        public string  Descricao { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
        public int LancheId { get; set; }
        public bool PodeAlterar { get; set; }
        
    }

    public class IngredienteRepository : IRepository<Ingrediente>
    {
        protected string StringConnection { get; } = WebConfigurationManager.ConnectionStrings["LanchesContext"].ConnectionString;

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Ingrediente> GetAll()
        {
            string sql = "SP_RECUPERA_INGREDIENTES_LANCHE";
            using (var conn = new SqlConnection(StringConnection))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                List<Ingrediente> list = new List<Ingrediente>();
                Ingrediente p = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            p = new Ingrediente();
                            p.LancheId = (int)reader["LancheId"];
                            p.IngredienteId = (int)reader["IngredienteId"];
                            p.Quantidade = (int)reader["QuantidadeIngrediente"];
                            p.Descricao = reader["Descricao"].ToString();
                            p.Valor = (decimal)reader["Valor"];
                            p.PodeAlterar = (bool)reader["PodeAlterar"];

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

        public Ingrediente GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Ingrediente entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Ingrediente entity)
        {
            throw new NotImplementedException();
        }

    }
}