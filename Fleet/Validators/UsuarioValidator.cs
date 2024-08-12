using Fleet.Interfaces.Repository;
using Fleet.Models;
using FluentValidation;
using Fleet.Resources;
using Fleet.Enums;


namespace Fleet.Validators
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator(IUsuarioRepository usuarioRepository, UsuarioRequestEnum request)
        {   
            if(request == UsuarioRequestEnum.Criar)
                RuleFor(x => x.Email).MustAsync(async (email, cancellationToken) => !await usuarioRepository.ExisteEmail(email))
                                     .WithMessage(Resource.usuario_emailduplicado);
            if(request != UsuarioRequestEnum.Criar)
                RuleFor(x => x.Id).MustAsync(async (id, cancellationToken) => await usuarioRepository.Existe(id))
                                .WithMessage("Usuario nao existe");
                if(request == UsuarioRequestEnum.Atualizar)
                    RuleFor(x => x.Email).MustAsync(async (email, cancellationToken) => !await usuarioRepository.ExisteEmail(email))
                                        .WithMessage(Resource.usuario_emailduplicado);
            if(request != UsuarioRequestEnum.Deletar) {
                RuleFor(x => x.CPF).Must(IsValidCPF)
                                .WithMessage("CPF invalido");
            
                RuleFor(x => x.CPF).MustAsync(async (cpf,cancellationToken)  => await usuarioRepository.ExisteCpf(cpf))
                                .WithMessage("CPF ja existe");
            }               
        }
        private static bool IsValidCPF(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11 || !long.TryParse(cpf, out _))
            {
                return false;
            }

            int[] multipliers1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multipliers2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * multipliers1[i];
            }

            int remainder = sum % 11;
            if (remainder < 2)
            {
                remainder = 0;
            }
            else
            {
                remainder = 11 - remainder;
            }

            string digit = remainder.ToString();
            tempCpf += digit;
            sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * multipliers2[i];
            }

            remainder = sum % 11;
            if (remainder < 2)
            {
                remainder = 0;
            }
            else
            {
                remainder = 11 - remainder;
            }

            digit += remainder.ToString();

            return cpf.EndsWith(digit);
        }
    }
}