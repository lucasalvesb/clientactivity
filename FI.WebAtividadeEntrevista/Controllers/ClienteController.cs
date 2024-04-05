using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using System.Text;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel clienteModel, BeneficiarioModel beneficiarioModel, string[] CPFBeneficiario, string[] NomeBeneficiario)
        {
            BoCliente boCliente = new BoCliente();
            BoBeneficiario boBeneficiario = new BoBeneficiario();

            // Validate client CPF
            string cleanCPF = RemoverCaracteresNaoNumericos(clienteModel.CPF);
            var validarCPFAttribute = new ValidarCPFAttribute();
    
            if (!validarCPFAttribute.IsValid(clienteModel.CPF))
            {
                Response.StatusCode = 400;
                return Json(new { success = false, message = "CPF inválido" });
            }

            if (!ModelState.IsValid)
            {
                // If client model is not valid, return error messages
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(new { success = false, message = string.Join(Environment.NewLine, erros) });
            }
            else
            {
                // Insert client data
                long clientId = boCliente.Incluir(new Cliente()
                {
                    CEP = clienteModel.CEP,
                    CPF = clienteModel.CPF,
                    Cidade = clienteModel.Cidade,
                    Email = clienteModel.Email,
                    Estado = clienteModel.Estado,
                    Logradouro = clienteModel.Logradouro,
                    Nacionalidade = clienteModel.Nacionalidade,
                    Nome = clienteModel.Nome,
                    Sobrenome = clienteModel.Sobrenome,
                    Telefone = clienteModel.Telefone
                });

                if (clientId == -1)
                {
                    // If client already exists, return error
                    return Json(new { success = false, message = "CPF já cadastrado" });
                }

                // Insert beneficiary data
                if (beneficiarioModel != null)
                {
                    string cleanBeneficiaryCPF = RemoverCaracteresNaoNumericos(beneficiarioModel.CPF);

                    if (!validarCPFAttribute.IsValid(beneficiarioModel.CPF))
                    {
                        // If beneficiary CPF is invalid, return error
                        Response.StatusCode = 400;
                        return Json(new { success = false, message = "CPF de beneficiário inválido" });
                    }

                    for (int i = 0; i < clienteModel.CPFBeneficiario.Length; i++)
                    {
                        boBeneficiario.Incluir(new Beneficiario()
                        {
                            CPF = clienteModel.CPFBeneficiario[i],
                            Nome = clienteModel.NomeBeneficiario[i],
                            IdCliente = clientId
                        });
                    }
                }

                // Return success message along with client ID
                return Json(new { success = true, message = "Cadastro efetuado com sucesso", clientId = clientId });
            }
        }

        private string RemoverCaracteresNaoNumericos(string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                if (char.IsDigit(c))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente boCliente = new BoCliente();
            BoBeneficiario boBeneficiario = new BoBeneficiario();

            var validarCPFAttribute = new ValidarCPFAttribute();

            if (!validarCPFAttribute.IsValid(model.CPF))
            {
                Response.StatusCode = 400;
                return Json("CPF inválido");
            }

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                // Update client data
                boCliente.Alterar(new Cliente()
                {
                    Id = model.Id,
                    CPF = model.CPF,
                    CEP = model.CEP,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone
                });

                // Update beneficiary data
                if (model.CPFBeneficiario != null && model.NomeBeneficiario != null)
                {
                    for (int i = 0; i < model.CPFBeneficiario.Length; i++)
                    {
                        boBeneficiario.Alterar(new Beneficiario()
                        {
                            CPF = model.CPFBeneficiario[i],
                            Nome = model.NomeBeneficiario[i],
                            IdCliente = model.Id
                        });
                    }
                }

                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente boCliente = new BoCliente();
            BoBeneficiario boBeneficiario = new BoBeneficiario();

            Cliente cliente = boCliente.Consultar(id);
            Models.ClienteModel model = null;

            if (cliente != null)
            {
                string formattedCPF = FormataCPF(cliente.CPF);

                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CPF = formattedCPF,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone
                };

                // Retrieve beneficiary data
                List<Beneficiario> beneficiarios = boBeneficiario.Consultar(cliente.Id);
                if (beneficiarios != null && beneficiarios.Any())
                {
                    model.CPFBeneficiario = beneficiarios.Select(b => b.CPF).ToArray();
                    model.NomeBeneficiario = beneficiarios.Select(b => b.Nome).ToArray();
                }
            }

            return View(model);
        }


        private string FormataCPF(string cpf)
        {
            if (cpf == null || cpf.Length != 11)
                return cpf;

            return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        //[HttpPost]
        //public JsonResult AddBeneficiary(BeneficiarioModel model)
        //{
        //    string cleanCPF = RemoverCaracteresNaoNumericos(model.CPF);

        //    var validarCPFAttribute = new ValidarCPFAttribute();

        //    if (!validarCPFAttribute.IsValid(model.CPF))
        //    {
        //        Response.StatusCode = 400;
        //        return Json(new { success = false, message = "CPF inválido" });
        //    }

        //    BoBeneficiario bo = new BoBeneficiario();
        //    long beneficiaryId = bo.Incluir(new Beneficiario()
        //    {
        //        CPF = model.CPF,
        //        Nome = model.Nome,
        //        IdCliente = model.IdCliente,
        //        Id = model.Id,

        //    });

        //    return Json(new { success = true, message = "Beneficiário adicionado com sucesso", beneficiaryId = beneficiaryId });
        //}

    }
}