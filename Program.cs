using ValidaCPF_CNPJ.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.MapPost("api/valida-cpf", (CpfRequest request) =>
{
    if (ValidadorCPF.IsValid(request.cpf))
    {
        return Results.Ok(new { cpf = request.cpf, valido = true, mensagem = "CPF válido." });
    }

    return Results.BadRequest(new { cpf = request.cpf, valido = false, mensagem = "CPF inválido." });
});

app.MapPost("api/valida-cnpj", (CnpjRequest request) =>
{
    if (ValidadorCNPJ.IsValid(request.cnpj))
    {
        return Results.Ok(new { cnpj = request.cnpj, valido = true, mensagem = "CNPJ válido." });
    }
    
    return Results.BadRequest(new { cnpj = request.cnpj, valido = false, mensagem = "CNPJ inválido" });
});

app.Run();

public record CpfRequest(string cpf);
public record CnpjRequest(string cnpj);