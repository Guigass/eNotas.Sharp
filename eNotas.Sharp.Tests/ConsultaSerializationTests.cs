using eNotas.Sharp.Models;
using eNotas.Sharp.Tests.Helpers;
using Newtonsoft.Json;

namespace eNotas.Sharp.Tests;

public class ConsultaSerializationTests
{
    [Fact]
    public void Deserialize_Fixture_PopulatesConsulta()
    {
        var json = FixtureLoader.Read("consulta-nfe.json");

        var consulta = JsonConvert.DeserializeObject<Consulta>(json);

        Assert.NotNull(consulta);
        Assert.Equal("teste-unitario-001", consulta!.Id);
        Assert.Equal("NF-e", consulta.Tipo);
        Assert.Equal("Autorizada", consulta.Status);
        Assert.Equal("Homologacao", consulta.AmbienteEmissao);
        Assert.Equal(1, consulta.Numero);
        Assert.Equal(1, consulta.Serie);
        Assert.Equal("00000000000000000000000000000000000000000000", consulta.ChaveAcesso);
        Assert.Equal(1.0, consulta.ValorTotal);
        Assert.False(consulta.EmitidaEmContingencia);
    }
}
