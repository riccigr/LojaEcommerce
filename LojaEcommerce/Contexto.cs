using LojaEcommerce.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaEcommerce
{
    public class Contexto : DbContext
    {
        public DbSet<Produto> Produtos { get; private set; }
        public DbSet<Pedido> Pedidos { get; private set; }
        public DbSet<ItemPedido> ItensPedido { get; private set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {             
        }
    }
}
