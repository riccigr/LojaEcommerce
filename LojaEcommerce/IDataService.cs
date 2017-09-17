using LojaEcommerce.Models;
using System.Collections.Generic;

namespace LojaEcommerce
{
    public interface IDataService
    {
        void InicializaDB();
        List<Produto> GetProdutos();
        List<ItemPedido> GetItensPedido();
    }
}