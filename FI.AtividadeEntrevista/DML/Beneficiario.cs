using System;

namespace FI.AtividadeEntrevista.DML
{
    /// <summary>
    /// Classe que representa um beneficiário
    /// </summary>
    public class Beneficiario
    {
        /// <summary>
        /// Id do beneficiário
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// CPF do beneficiário
        /// </summary>
        public string CPF { get; set; }

        /// <summary>
        /// Nome do beneficiário
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// ID do Cliente ao qual o beneficiário está associado
        /// </summary>
        public long IdCliente { get; set; }
    }
}