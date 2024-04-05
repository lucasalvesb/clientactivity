using System.ComponentModel.DataAnnotations;

namespace WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo de Beneficiário
    /// </summary>
    public class BeneficiarioModel
    {
        public long Id { get; set; }

        /// <summary>
        /// CPF do Beneficiário
        /// </summary>
        [ValidarCPF(ErrorMessage = "O CPF é inválido.")]
        public string CPF { get; set; }

        /// <summary>
        /// Nome do Beneficiário
        /// </summary>
        [Required(ErrorMessage = "O nome do beneficiário é obrigatório.")]
        [StringLength(50, ErrorMessage = "O nome do beneficiário não pode ter mais de 50 caracteres.")]
        public string Nome { get; set; }

        /// <summary>
        /// ID do Cliente ao qual o beneficiário está associado
        /// </summary>
        public long IdCliente { get; set; }
    }
}