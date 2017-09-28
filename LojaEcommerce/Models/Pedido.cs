using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace LojaEcommerce.Models
{
    public class Pedido : BaseModel
    {
        public List<ItemPedido> Itens { get; private set; }

        #region DadosPessoais
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        #endregion

        #region DadosEndereco
        public string Endereco { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        #endregion

        public Pedido()
        {
            this.Itens = new List<ItemPedido>();
            this.Nome = "Teste";
        }
    }


}
