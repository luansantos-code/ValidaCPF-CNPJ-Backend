namespace ValidaCPF_CNPJ.Services;

public static class ValidadorCPF
{
    public static bool IsValid(string cpf)
    {
        if (string.IsNullOrEmpty(cpf)) return false;
        
        cpf = new string(cpf.Where(char.IsDigit).ToArray());
        
        if (cpf.Length != 11) return false;

        if (cpf.Distinct().Count() == 1) return false;

        int[] multiplicador1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplicador2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

        string tempCpf = cpf[..9];
        int soma = tempCpf.Zip(multiplicador1, (digit, mult) => (digit - '0') * mult).Sum();
        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        tempCpf += digito1;
        soma = tempCpf.Zip(multiplicador2, (digit, mult) => (digit - '0') * mult).Sum();
        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;
        
        return cpf.EndsWith($"{digito1}{digito2}");
    }
}