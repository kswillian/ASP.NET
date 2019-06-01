using System;
using System.Data.Entity;

namespace ASP.NET_Web_Application.Models
{
    public class Filial
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string bairro { get; set; }
        public char uf { get; set; }
    }

    public class FilialDBContext : DbContext
    {
        public DbSet<Filial> Filiais { get; set; }
    }
}