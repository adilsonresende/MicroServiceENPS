using System;
using MicroserviceENPS.UserServices.Entities;

namespace MicroservicesENPS.CompanyServices.Entities
{
    public class Company
    {
        public Company()
        {

        }

        public Company(Guid id, Guid idUser, string fantasyName, string name, string cnpj, string ie)
        {
            DomainValidadorException.Whem(id == Guid.Empty, IdIsNotValid);
            Id = Id;

            DomainValidadorException.Whem(idUser == Guid.Empty, IdUserIsNotValid);
            IdUser = idUser;

            DomainValidadorException.Whem(string.IsNullOrWhiteSpace(fantasyName), FantasyNameIsNotValid);
            FantasyName = fantasyName;

            DomainValidadorException.Whem(string.IsNullOrWhiteSpace(name), NameIsNotValid);
            Name = name;

            bool IsCNPJValid = ValidadeCNPJ(cnpj);
            DomainValidadorException.Whem(!IsCNPJValid, CNPJIsNotValid);
            Name = name;
        }

        private string IdIsNotValid = "Id inválido!";
        private string IdUserIsNotValid = "IdUser inválido!";
        private string FantasyNameIsNotValid = "Fantasia inválido!";
        private string NameIsNotValid = "Razão social inválida";
        private string CNPJIsNotValid = "CNPJ inválido!";

        public Guid Id { get; private set; }
        public Guid IdUser { get; private set; }
        public bool IsActive { get; private set; } = false;
        public string FantasyName { get; private set; }
        public string Name { get; private set; }
        public string CNPJ { get; private set; }
        public string IE { get; private set; }

        public void ChangeIsActiveState(bool isActive)
        {
            IsActive = isActive;
        }

        public bool ValidadeCNPJ(string CNPJ)
        {
            string _cNPJ = CNPJ.Replace(".", "");
            _cNPJ = _cNPJ.Replace("/", "");
            _cNPJ = _cNPJ.Replace("-", "");
            int[] digits, sum, result;
            int numeberDigit;
            string ftmt;
            bool[] CNPJOk;
            ftmt = "6543298765432";
            digits = new int[14];
            sum = new int[2];
            sum[0] = 0;
            sum[1] = 0;
            result = new int[2];
            result[0] = 0;
            result[1] = 0;
            CNPJOk = new bool[2];
            CNPJOk[0] = false;
            CNPJOk[1] = false;
            try
            {
                for (numeberDigit = 0; numeberDigit < 14; numeberDigit++)
                {
                    digits[numeberDigit] = int.Parse(
                        _cNPJ.Substring(numeberDigit, 1));
                    if (numeberDigit <= 11)
                        sum[0] += (digits[numeberDigit] *
                          int.Parse(ftmt.Substring(
                          numeberDigit + 1, 1)));
                    if (numeberDigit <= 12)
                        sum[1] += (digits[numeberDigit] *
                          int.Parse(ftmt.Substring(
                          numeberDigit, 1)));
                }
                for (numeberDigit = 0; numeberDigit < 2; numeberDigit++)
                {
                    result[numeberDigit] = (sum[numeberDigit] % 11);
                    if ((result[numeberDigit] == 0) || (
                         result[numeberDigit] == 1))
                        CNPJOk[numeberDigit] = (
                        digits[12 + numeberDigit] == 0);
                    else
                        CNPJOk[numeberDigit] = (
                        digits[12 + numeberDigit] == (
                        11 - result[numeberDigit]));
                }
                return (CNPJOk[0] && CNPJOk[1]);
            }
            catch
            {
                return false;
            }
        }
    }
}