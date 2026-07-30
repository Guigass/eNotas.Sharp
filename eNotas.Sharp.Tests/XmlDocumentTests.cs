using System.Xml.Serialization;
using eNotas.Sharp.Tests.Helpers;

namespace eNotas.Sharp.Tests;

public class XmlDocumentTests
{
    [Fact]
    public void Deserialize_CancelamentoFixture()
    {
        var xml = FixtureLoader.Read("proc-evento-cancelamento-minimo.xml");
        var serializer = new XmlSerializer(typeof(Models.XmlCancelamento.ProcEventoNFe));

        using var reader = new StringReader(xml);
        var doc = (Models.XmlCancelamento.ProcEventoNFe?)serializer.Deserialize(reader);

        Assert.NotNull(doc);
        Assert.Equal("1.00", doc!.Versao);
        Assert.Equal("110111", doc.Evento!.InfEvento!.TpEvento);
        Assert.Equal("Cancelamento", doc.Evento.InfEvento.DetEvento!.DescEvento);
        Assert.Equal("135", doc.RetEvento!.InfEvento!.CStat);
    }

    [Fact]
    public void Deserialize_CorrecaoFixture()
    {
        var xml = FixtureLoader.Read("proc-evento-correcao-minimo.xml");
        var serializer = new XmlSerializer(typeof(Models.XmlCorrecao.ProcEventoNFe));

        using var reader = new StringReader(xml);
        var doc = (Models.XmlCorrecao.ProcEventoNFe?)serializer.Deserialize(reader);

        Assert.NotNull(doc);
        Assert.Equal("110110", doc!.Evento!.InfEvento!.TpEvento);
        Assert.Equal("Correcao de teste", doc.Evento.InfEvento.DetEvento!.XCorrecao);
        Assert.Equal("135", doc.RetEvento!.InfEvento!.CStat);
    }

    [Fact]
    public void Deserialize_InutilizacaoFixture()
    {
        var xml = FixtureLoader.Read("proc-inut-minimo.xml");
        var serializer = new XmlSerializer(typeof(Models.XmlInutilizacao.ProcInutNFe));

        using var reader = new StringReader(xml);
        var doc = (Models.XmlInutilizacao.ProcInutNFe?)serializer.Deserialize(reader);

        Assert.NotNull(doc);
        Assert.Equal("4.00", doc!.Versao);
        Assert.Equal("100", doc.InutNFe!.InfInut!.NNFIni);
        Assert.Equal("105", doc.InutNFe.InfInut.NNFFin);
        Assert.Equal("102", doc.RetInutNFe!.InfInut!.CStat);
    }
}
