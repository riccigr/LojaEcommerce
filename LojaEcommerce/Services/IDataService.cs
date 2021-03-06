﻿using LojaEcommerce.Models;
using LojaEcommerce.Response;
using System.Collections.Generic;

namespace LojaEcommerce
{
    public interface IDataService
    {
        void InicializaDB();
        List<Produto> GetProdutos();
        List<ItemPedido> GetItensPedido();
        UpdateItemPedidoResponse UpdateQuantidade(ItemPedido item);
        void AddItemPedido(int produtoId);
        Pedido GetPedido();
        Pedido UpdateCadastro(Pedido cadastro);
    }
}