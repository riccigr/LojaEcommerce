using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace LojaEcommerce.Models
{
    public class Pedido : BaseModel
    {
        public List<ItemPedido> Itens { get; private set; }

        #region DadosPessoais
        [Required(ErrorMessage = "O Nome é obrigatório!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage ="O nome deve conter entre 5 e 50 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório!")]
        [StringLength(200, MinimumLength = 11, ErrorMessage = "O e-mail deve conter entre 11 e 200 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O Telefone é obrigatório!")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "O Telefone deve conter entre 8 e 20 caracteres")]
        public string Telefone { get; set; }
        #endregion

        #region DadosEndereco
        [Required(ErrorMessage = "O Endereco é obrigatório!")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "O Endereco deve conter entre 6 e 50 caracteres")]
        public string Endereco { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "O Complemento deve conter entre 5 e 50 caracteres")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "O Bairro é obrigatório!")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "O Bairro deve conter entre 2 e 20 caracteres")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "A Cidade é obrigatória!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "A Cidade deve conter entre 5 e 50 caracteres")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "A Unidade Federativa é obrigatório!")]
        [StringLength(2, ErrorMessage = "A Unidade Federativa deve conter 2 caracteres")]
        public string UF { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório!")]
        [StringLength(9, ErrorMessage = "O CEP deve conter 9 caracteres")]
        public string CEP { get; set; }
        #endregion

        public Pedido()
        {
            this.Itens = new List<ItemPedido>();
        }

        internal void UpdateCadastro(Pedido cadastro)
        {
            this.Nome = cadastro.Nome;
            this.Email = cadastro.Email;
            this.Telefone = cadastro.Telefone;
            this.Endereco = cadastro.Endereco;
            this.Complemento = cadastro.Complemento;
            this.Bairro = cadastro.Bairro;
            this.Cidade = cadastro.Cidade;
            this.UF = cadastro.UF;
            this.CEP = cadastro.CEP;
        }
    }


}
