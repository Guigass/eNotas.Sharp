using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace eNotas.Sharp.Models.XmlCancelamento
{
	[XmlRoot(ElementName = "detEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class DetEvento
	{
		[XmlElement(ElementName = "descEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string DescEvento { get; set; }
		[XmlElement(ElementName = "nProt", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string NProt { get; set; }
		[XmlElement(ElementName = "xJust", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XJust { get; set; }
		[XmlAttribute(AttributeName = "versao")]
		public string Versao { get; set; }
	}

	[XmlRoot(ElementName = "infEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class InfEvento
	{
		[XmlElement(ElementName = "cOrgao", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string COrgao { get; set; }
		[XmlElement(ElementName = "tpAmb", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string TpAmb { get; set; }
		[XmlElement(ElementName = "CNPJ", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CNPJ { get; set; }
		[XmlElement(ElementName = "chNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string ChNFe { get; set; }
		[XmlElement(ElementName = "dhEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string DhEvento { get; set; }
		[XmlElement(ElementName = "tpEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string TpEvento { get; set; }
		[XmlElement(ElementName = "nSeqEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string NSeqEvento { get; set; }
		[XmlElement(ElementName = "verEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VerEvento { get; set; }
		[XmlElement(ElementName = "detEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public DetEvento DetEvento { get; set; }
		[XmlAttribute(AttributeName = "Id")]
		public string Id { get; set; }
		[XmlElement(ElementName = "verAplic", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VerAplic { get; set; }
		[XmlElement(ElementName = "cStat", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CStat { get; set; }
		[XmlElement(ElementName = "xMotivo", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XMotivo { get; set; }
		[XmlElement(ElementName = "xEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XEvento { get; set; }
		[XmlElement(ElementName = "CNPJDest", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CNPJDest { get; set; }
		[XmlElement(ElementName = "dhRegEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string DhRegEvento { get; set; }
		[XmlElement(ElementName = "nProt", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string NProt { get; set; }
	}

	[XmlRoot(ElementName = "CanonicalizationMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
	public partial class CanonicalizationMethod
	{
		[XmlAttribute(AttributeName = "Algorithm")]
		public string Algorithm { get; set; }
	}

	[XmlRoot(ElementName = "SignatureMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
	public partial class SignatureMethod
	{
		[XmlAttribute(AttributeName = "Algorithm")]
		public string Algorithm { get; set; }
	}

	[XmlRoot(ElementName = "Transform", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
	public partial class Transform
	{
		[XmlAttribute(AttributeName = "Algorithm")]
		public string Algorithm { get; set; }
	}

	[XmlRoot(ElementName = "Transforms", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
	public partial class Transforms
	{
		[XmlElement(ElementName = "Transform", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public List<Transform> Transform { get; set; }
	}

	[XmlRoot(ElementName = "DigestMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
	public partial class DigestMethod
	{
		[XmlAttribute(AttributeName = "Algorithm")]
		public string Algorithm { get; set; }
	}

	[XmlRoot(ElementName = "Reference", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
	public partial class Reference
	{
		[XmlElement(ElementName = "Transforms", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public Transforms Transforms { get; set; }
		[XmlElement(ElementName = "DigestMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public DigestMethod DigestMethod { get; set; }
		[XmlElement(ElementName = "DigestValue", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public string DigestValue { get; set; }
		[XmlAttribute(AttributeName = "URI")]
		public string URI { get; set; }
	}

	[XmlRoot(ElementName = "SignedInfo", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
	public partial class SignedInfo
	{
		[XmlElement(ElementName = "CanonicalizationMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public CanonicalizationMethod CanonicalizationMethod { get; set; }
		[XmlElement(ElementName = "SignatureMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public SignatureMethod SignatureMethod { get; set; }
		[XmlElement(ElementName = "Reference", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public Reference Reference { get; set; }
	}

	[XmlRoot(ElementName = "X509Data", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
	public partial class X509Data
	{
		[XmlElement(ElementName = "X509Certificate", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public string X509Certificate { get; set; }
	}

	[XmlRoot(ElementName = "KeyInfo", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
	public partial class KeyInfo
	{
		[XmlElement(ElementName = "X509Data", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public X509Data X509Data { get; set; }
	}

	[XmlRoot(ElementName = "Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
	public partial class Signature
	{
		[XmlElement(ElementName = "SignedInfo", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public SignedInfo SignedInfo { get; set; }
		[XmlElement(ElementName = "SignatureValue", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public string SignatureValue { get; set; }
		[XmlElement(ElementName = "KeyInfo", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public KeyInfo KeyInfo { get; set; }
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
	}

	[XmlRoot(ElementName = "evento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class Evento
	{
		[XmlElement(ElementName = "infEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public InfEvento InfEvento { get; set; }
		[XmlElement(ElementName = "Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public Signature Signature { get; set; }
		[XmlAttribute(AttributeName = "versao")]
		public string Versao { get; set; }
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
	}

	[XmlRoot(ElementName = "retEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class RetEvento
	{
		[XmlElement(ElementName = "infEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public InfEvento InfEvento { get; set; }
		[XmlAttribute(AttributeName = "versao")]
		public string Versao { get; set; }
	}

	[XmlRoot(ElementName = "procEventoNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class ProcEventoNFe
	{
		[XmlElement(ElementName = "evento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public Evento Evento { get; set; }
		[XmlElement(ElementName = "retEvento", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public RetEvento RetEvento { get; set; }
		[XmlAttribute(AttributeName = "versao")]
		public string Versao { get; set; }
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
	}
}
