using ValidaCPF_CNPJ.Services;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("api/valida-cpf", (CpfRequest request) =>
{
    if (ValidadorCPF.IsValid(request.cpf))
    {
        return Results.Ok(new { cpf = request.cpf, valido = true, mensagem = "CPF válido." });
    }

    return Results.BadRequest(new { cpf = request.cpf, valido = false, mensagem = "CPF inválido." });
});
app.Run();

record CpfRequest(string cpf);