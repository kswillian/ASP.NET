using System;
using System.Data.Entity;

namespace ASP.NET_Web_Application.Models
{
    public class Prato
    {
        public int id { get; set; }
        public string url_img { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public double valor { get; set; }
    }

    public class PratoDBContext : DbContext
    {
        public DbSet<Prato> Pratos { get; set; }
    }
}