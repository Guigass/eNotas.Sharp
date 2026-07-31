using eNotas.Sharp.Clients;
using eNotas.Sharp.Models;

// Credenciais via ambiente — nunca hardcode API Key neste repositório.
var apiKey = Environment.GetEnvironmentVariable("ENOTAS_API_KEY");
var empresaId = Environment.GetEnvironmentVariable("ENOTAS_EMPRESA_ID");

if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(empresaId))
{
    Console.WriteLine("Defina ENOTAS_API_KEY e ENOTAS_EMPRESA_ID para emitir uma NF-e de homologação.");
    Console.WriteLine();
    Console.WriteLine("Exemplo (PowerShell):");
    Console.WriteLine("  $env:ENOTAS_API_KEY = \"sua-api-key-de-homologacao\"");
    Console.WriteLine("  $env:ENOTAS_EMPRESA_ID = \"id-da-empresa\"");
    Console.WriteLine("  dotnet run --project Exemplos/EmissaoNfeHomologacao");
    return 0;
}

var nota = CriarNotaHomologacaoMinima();

using var enotas = new eNotasClient(apiKey);
var response = await enotas.EmitirNfe(nota, empresaId);

Console.WriteLine($"IsSuccess: {response.IsSuccess}");
Console.WriteLine($"Status: {response.Status}");
Console.WriteLine($"Message: {response.Message}");
if (response.Exception != null)
    Console.WriteLine($"Exception: {response.Exception.Message}");

return response.IsSuccess ? 0 : 1;

static Nota CriarNotaHomologacaoMinima()
{
    // Payload mínimo alinhado ao fixture/tests e ao sample Postman V2 (dados fictícios).
    return new Nota
    {
        Id = $"exemplo-{DateTime.UtcNow:yyyyMMddHHmmss}",
        Tipo = "NF-e",
        AmbienteEmissao = "Homologacao",
        EnviarPorEmail = false,
        ValorTotal = 1.00m,
        Pedido = new Pedido
        {
            PresencaConsumidor = "NaoSeAplica",
            Pagamento = new Pagamento
            {
                Tipo = "PagamentoAVista",
                Formas = new List<Forma>
                {
                    new Forma { Tipo = "Dinheiro", Valor = 1.00m }
                }
            }
        },
        Cliente = new Cliente
        {
            IndicadorContribuinteIcms = "NaoContribuinte",
            TipoPessoa = "F",
            Nome = "Cliente Teste",
            Email = "teste@example.com",
            CpfCnpj = "00000000191",
            Telefone = "3132223333",
            Endereco = new Endereco
            {
                Uf = "MG",
                Cidade = "Belo Horizonte",
                Logradouro = "Rua Teste",
                Numero = "100",
                Complemento = "Sala 1",
                Bairro = "Centro",
                Cep = "30130174"
            }
        },
        Transporte = new Transporte
        {
            Frete = new Frete
            {
                Modalidade = "SemFrete",
                Valor = 0.0m
            }
        },
        Itens = new List<Iten>
        {
            new Iten
            {
                Cfop = "5101",
                Codigo = "1",
                Descricao = "Produto Teste",
                Ncm = "49019900",
                Quantidade = 1.0m,
                UnidadeMedida = "un",
                ValorUnitario = 1.00m,
                ValorTotal = 1.00m,
                Impostos = new Impostos
                {
                    Icms = new Icms
                    {
                        Origem = 0,
                        SituacaoTributaria = "041",
                        ModalidadeBaseCalculo = 0,
                        BaseCalculo = 1.00m
                    },
                    Pis = new Imposto { SituacaoTributaria = "01" },
                    Cofins = new Imposto { SituacaoTributaria = "01" }
                }
            }
        }
    };
}
