using LojaEcommerce.Models;
using LojaEcommerce.Models.ViewModels;
using LojaEcommerce.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LojaEcommerce
{
    public class DataService : IDataService
    {
        private readonly Contexto _contexto;
        private readonly IHttpContextAccessor _contextAcessor;

        public DataService(Contexto contexto, IHttpContextAccessor contextAcessor)
        {
            this._contexto = contexto;
            this._contextAcessor = contextAcessor;
        }

        public void InicializaDB()
        {
            this._contexto.Database.EnsureCreated();
            if(this._contexto.Produtos.Count() == 0)
            {
                List<Produto> produtos = new List<Produto>
                {
                    new Produto( "Sleep not found", 59.90m),
                    new Produto( "May the code be with you", 59.90m),
                    new Produto( "Rollback", 59.90m),
                    new Produto( "REST", 69.90m),
                    new Produto( "Design Patterns com Java", 69.90m),
                    new Produto( "Vire o jogo com Spring Framework", 69.90m),
                    new Produto( "Test-Driven Development", 69.90m),
                    new Produto( "iOS: Programe para iPhone e iPad", 69.90m),
                    new Produto( "Desenvolvimento de Jogos para Android", 69.90m)
                };
                foreach (var produto in produtos)
                {
                    this._contexto.Produtos.Add(produto);

                    this._contexto.SaveChanges();
                }
            }
        }

        public void AddItemPedido(int produtoId)
        {
            var produtoSelecionado = _contexto.Produtos.Where(p => p.Id == produtoId).SingleOrDefault();

            if(produtoSelecionado != null)
            {
                Pedido pedido = null;

                int? pedidoId = GetSessionPedidoId();
                if (pedidoId.HasValue)
                {
                    pedido = _contexto.Pedidos.Where(p => p.Id == pedidoId.Value).SingleOrDefault();
                }

                if (pedido == null)
                {
                    pedido = new Pedido();
                }

                if (!_contexto.ItensPedido.Where(i => i.Produto.Id == produtoSelecionado.Id && i.Pedido.Id == pedido.Id).Any())
                {
                    _contexto.ItensPedido.Add(new ItemPedido(pedido, produtoSelecionado, 1));
                    _contexto.SaveChanges();
                    SetSessionPedidoId(pedido);
                }


            }
        }


        public List<Produto> GetProdutos()
        {
            return _contexto.Produtos.ToList();
        }

        public List<ItemPedido> GetItensPedido()
        {
            int? pedidoId = GetSessionPedidoId();
            Pedido pedido = _contexto.Pedidos.Where(p => p.Id == pedidoId).Single();

            return _contexto.ItensPedido.Where(i => i.Pedido.Id == pedido.Id).ToList();
        }

        public UpdateItemPedidoResponse UpdateQuantidade(ItemPedido item)
        {
            var itemSelecionado = _contexto.ItensPedido.Where(i => i.Id == item.Id).SingleOrDefault();

            if(itemSelecionado != null)
            {
                itemSelecionado.AtualizaQuantidade(item.Quantidade);
                if(itemSelecionado.Quantidade == 0)
                {
                    _contexto.ItensPedido.Remove(itemSelecionado);
                }

                _contexto.SaveChanges();
            }

            var carrinhoViewModel = new CarrinhoViewModel(_contexto.ItensPedido.ToList());
            
            return new UpdateItemPedidoResponse(itemSelecionado, carrinhoViewModel);
        }

        public Pedido GetPedido()
        {
            int? pedidoId = GetSessionPedidoId();
            return _contexto.Pedidos.Where(p => p.Id == pedidoId).SingleOrDefault();
        }

        private void SetSessionPedidoId(Pedido pedido)
        {
            _contextAcessor.HttpContext.Session.SetInt32("pedidoId", pedido.Id);
        }

        private int? GetSessionPedidoId()
        {
            return _contextAcessor.HttpContext.Session.GetInt32("pedidoId");
        }

    }
}
