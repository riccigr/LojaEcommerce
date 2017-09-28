using LojaEcommerce.Models;
using LojaEcommerce.Models.ViewModels;
using LojaEcommerce.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaEcommerce.Controllers
{
    public class PedidoController : Controller
    {

        private readonly IDataService _dataService;
        public PedidoController(IDataService dataService)
        {
            this._dataService = dataService;
        }

        public IActionResult Carrossel()
        {
            List<Produto> produtos = _dataService.GetProdutos();
            return View(produtos);
        }

        public IActionResult Carrinho(int? produtoId)
        {
            if (produtoId.HasValue) {
                _dataService.AddItemPedido(produtoId.Value);
            }

            CarrinhoViewModel viewModel = GetCarrinhoViewModel();

            return View(viewModel);
        }

        public IActionResult Cadastro()
        {
            Pedido viewModel = _dataService.GetPedido();

            if(ViewData == null)
            {
                return RedirectToAction("Carrossel");
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Resumo(Pedido cadastro)
        {
            if (ModelState.IsValid)
            {
                Pedido viewModel = _dataService.UpdateCadastro(cadastro);

                return View(viewModel);
            }

            return RedirectToAction("Cadastro");
        }

        private CarrinhoViewModel GetCarrinhoViewModel()
        {
            List<Produto> produtos = _dataService.GetProdutos();
            var itensCarrinho = _dataService.GetItensPedido();

            CarrinhoViewModel viewModel = new CarrinhoViewModel(itensCarrinho);
            return viewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public UpdateItemPedidoResponse PostQuantidade([FromBody]ItemPedido input)
        {
            return _dataService.UpdateQuantidade(input);
        }


    }
}
