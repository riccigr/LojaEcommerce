using LojaEcommerce.Models;
using LojaEcommerce.Models.ViewModels;
using LojaEcommerce.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaEcommerce
{
    public class DataService : IDataService
    {
        private readonly Contexto _contexto;

        public DataService(Contexto contexto)
        {
            this._contexto = contexto;
        }

        public void InicializaDB()
        {
            this._contexto.Database.EnsureCreated();
            if(this._contexto.Produto.Count() == 0)
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
                    this._contexto.Produto.Add(produto);
                    this._contexto.ItensPedido.Add(new ItemPedido(produto, 1));

                    this._contexto.SaveChanges();
                }
            }
        }

        public void AddItemPedido(int produtoId)
        {
            var produtoSelecionado = _contexto.Produto.Where(p => p.Id == produtoId).SingleOrDefault();

            if(produtoSelecionado != null)
            {
                if (_contexto.ItensPedido.Where(i => i.Produto.Id == produtoSelecionado.Id).Any())
                {
                    var itemPedidoAtual = _contexto.ItensPedido.Where(i => i.Produto.Id == produtoSelecionado.Id).SingleOrDefault();
                    itemPedidoAtual.AtualizaQuantidade();
                }
                else
                {
                    _contexto.ItensPedido.Add(new ItemPedido(produtoSelecionado, 1));
                }

                _contexto.SaveChanges();
            }
        }

        public List<Produto> GetProdutos()
        {
            return _contexto.Produto.ToList();
        }

        public List<ItemPedido> GetItensPedido()
        {
            return _contexto.ItensPedido.ToList();
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
    }
}
