using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebAtividadeEntrevista.Models
{
    public class ValidarCPFAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var cpf = value as string;

            if (cpf == null)
                return false;

            // Remove non-numeric characters and check length
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            if (cpf.Length != 11)
                return false;

            // Calculate CPF validation
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(cpf[i].ToString()) * (10 - i);
            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(cpf[i].ToString()) * (11 - i);
            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            // Validate CPF digits
            return digito1 == int.Parse(cpf[9].ToString()) && digito2 == int.Parse(cpf[10].ToString());
        }
    }
}