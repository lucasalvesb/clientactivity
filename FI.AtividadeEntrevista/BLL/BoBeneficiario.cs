using FI.AtividadeEntrevista.DAL;
using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiário</param>
        public long Incluir(DML.Beneficiario beneficiario)
        {
            DaoBeneficiario dao = new DaoBeneficiario();
            return dao.Incluir(beneficiario);
        }

        /// <summary>
        /// Altera um beneficiário
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiário</param>
        public void Alterar(Beneficiario beneficiario)
        {
            DaoBeneficiario dao = new DaoBeneficiario();
            dao.Alterar(beneficiario);
        }

        /// <summary>
        /// Consulta os beneficiários pelo id
        /// </summary>
        /// <param name="id">id do beneficiário</param>
        /// <returns>Lista de beneficiários</returns>
        public List<Beneficiario> Consultar(long id)
        {
            DaoBeneficiario dao = new DaoBeneficiario();
            return dao.Consultar(id);
        }

        /// <summary>
        /// Exclui o beneficiário pelo id
        /// </summary>
        /// <param name="id">id do beneficiário</param>
        public void Excluir(long id)
        {
            DaoBeneficiario dao = new DaoBeneficiario();
            dao.Excluir(id);
        }

        /// <summary>
        /// Lista os beneficiários
        /// </summary>
        /// <returns>Lista de beneficiários</returns>
        public List<Beneficiario> Listar()
        {
            DaoBeneficiario dao = new DaoBeneficiario();
            return dao.Listar();
        }
    }
}