using System.Xml.Serialization;
using eNotas.Sharp.Models.Xml;
using eNotas.Sharp.Tests.Helpers;

namespace eNotas.Sharp.Tests;

public class NfeProcXmlTests
{
    [Fact]
    public void Deserialize_MinimalFixture_PopulatesNfeProc()
    {
        var xml = FixtureLoader.Read("nfe-proc-minimo.xml");
        var serializer = new XmlSerializer(typeof(NfeProc));

        using var reader = new StringReader(xml);
        var nfeProc = (NfeProc?)serializer.Deserialize(reader);

        Assert.NotNull(nfeProc);
        Assert.Equal("4.00", nfeProc!.Versao);
        Assert.NotNull(nfeProc.NFe);
        Assert.NotNull(nfeProc.NFe!.InfNFe);
        Assert.Equal("NFe00000000000000000000000000000000000000000000", nfeProc.NFe.InfNFe!.Id);
        Assert.NotNull(nfeProc.NFe.InfNFe.Ide);
        Assert.Equal("31", nfeProc.NFe.InfNFe.Ide!.CUF);
        Assert.Equal("1", nfeProc.NFe.InfNFe.Ide.NNF);
        Assert.NotNull(nfeProc.ProtNFe);
        Assert.Equal("100", nfeProc.ProtNFe!.InfProt!.CStat);
        Assert.Equal("00000000000000000000000000000000000000000000", nfeProc.ProtNFe.InfProt.ChNFe);
    }
}
