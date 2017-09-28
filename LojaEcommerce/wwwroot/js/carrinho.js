class Carrinho {
    clickIncremento(btn) {
        var data = this.getData(btn);
        data.Quantidade++;
        this.postQuantidade(data);
    }

    clickDecremento(btn) {
        var data = this.getData(btn);
        data.Quantidade--;
        this.postQuantidade(data);
    }

    updateQuantidade(input) {
        var data = this.getData(input);
        this.postQuantidade(data);
    }

    getData(elemento) {
        var item = $(elemento).parents('[item-id]');
        var itemId = item.attr('item-id');
        var qtd = item.find('input').val();

        return {
            Id: itemId,
            Quantidade: qtd
        };
    }

    postQuantidade(data) {

        var header = {};
        header['RequestVerificationToken'] = $('input[name=__RequestVerificationToken]').val();

        $.ajax({
            url: '/pedido/PostQuantidade',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data),
            headers: header
        }).done(function (response) {
            this.setQuantidade(response.itemPedido);
            this.setSubtotal(response.itemPedido);
            this.setTotal(response.carrinhoViewModel);
            if (response.itemPedido.quantidade == 0) {
                this.removeItem(response.itemPedido);
            }
            this.setNumeroItens(response.carrinhoViewModel);
        }.bind(this));
    }

    setQuantidade(itemPedido) {
        this.getLinhaPedido(itemPedido).find('input').val(itemPedido.quantidade);
    }

    setSubtotal(itemPedido) {
        this.getLinhaPedido(itemPedido).find('[subtotal]').html(itemPedido.subtotal.setCentavos());
    }

    setTotal(carrinhoViewModel) {
        $('[total]').html(carrinhoViewModel.total.setCentavos());
    }

    setNumeroItens(carrinhoViewModel) {
        var txtTotal = carrinhoViewModel.itens.length + ' ' + (carrinhoViewModel.itens.length == 1 ? 'item' : 'itens')
        $('[numero-itens]').html(txtTotal);
    }

    removeItem(itemPedido) {
        this.getLinhaPedido(itemPedido).remove();
    }

    getLinhaPedido(itemPedido) {
        return $('[item-id=' + itemPedido.id + ']');
    }

}

var carrinho = new Carrinho();

Number.prototype.setCentavos = function () {
    return this.toFixed(2).replace('.', ',');
};
