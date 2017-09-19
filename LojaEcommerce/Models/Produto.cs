using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LojaEcommerce.Models
{
    public class Produto
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        [Column(TypeName = "decima(18,2)")]
        public decimal Preco { get; private set; }

        public Produto() {}
        public Produto(int id, string nome, decimal preco) : this(nome, preco)
        {
            this.Id = id;
        }

        public Produto(string nome, decimal preco)
        {
            this.Nome = nome;
            this.Preco = preco;
        }
    }
}
