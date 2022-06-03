using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace eNotas.Sharp.Models.XmlInutilizacao
{
	[XmlRoot(ElementName = "infInut", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public class InfInut
	{
		[XmlElement(ElementName = "tpAmb", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string TpAmb { get; set; }
		[XmlElement(ElementName = "xServ", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XServ { get; set; }
		[XmlElement(ElementName = "cUF", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CUF { get; set; }
		[XmlElement(ElementName = "ano", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string Ano { get; set; }
		[XmlElement(ElementName = "CNPJ", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CNPJ { get; set; }
		[XmlElement(ElementName = "mod", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string Mod { get; set; }
		[XmlElement(ElementName = "serie", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string Serie { get; set; }
		[XmlElement(ElementName = "nNFIni", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string NNFIni { get; set; }
		[XmlElement(ElementName = "nNFFin", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string NNFFin { get; set; }
		[XmlElement(ElementName = "xJust", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XJust { get; set; }
		[XmlAttribute(AttributeName = "Id")]
		public string Id { get; set; }
		[XmlElement(ElementName = "verAplic", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VerAplic { get; set; }
		[XmlElement(ElementName = "cStat", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CStat { get; set; }
		[XmlElement(ElementName = "xMotivo", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XMotivo { get; set; }
		[XmlElement(ElementName = "dhRecbto", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string DhRecbto { get; set; }
		[XmlElement(ElementName = "nProt", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string NProt { get; set; }
	}

	[XmlRoot(ElementName = "CanonicalizationMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
	public class CanonicalizationMethod
	{
		[XmlAttribute(AttributeName = "Algorithm")]
		public string Algorithm { get; set; }
	}

	[XmlRoot(ElementName = "SignatureMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
	public class SignatureMethod
	{
		[XmlAttribute(AttributeName = "Algorithm")]
		public string Algorithm { get; set; }
	}

	[XmlRoot(ElementName = "Transform", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
	public class Transform
	{
		[XmlAttribute(AttributeName = "Algorithm")]
		public string Algorithm { get; set; }
	}

	[XmlRoot(ElementName = "Transforms", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
	public class Transforms
	{
		[XmlElement(ElementName = "Transform", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public List<Transform> Transform { get; set; }
	}

	[XmlRoot(ElementName = "DigestMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
	public class DigestMethod
	{
		[XmlAttribute(AttributeName = "Algorithm")]
		public string Algorithm { get; set; }
	}

	[XmlRoot(ElementName = "Reference", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
	public class Reference
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
	public class SignedInfo
	{
		[XmlElement(ElementName = "CanonicalizationMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public CanonicalizationMethod CanonicalizationMethod { get; set; }
		[XmlElement(ElementName = "SignatureMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public SignatureMethod SignatureMethod { get; set; }
		[XmlElement(ElementName = "Reference", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public Reference Reference { get; set; }
	}

	[XmlRoot(ElementName = "X509Data", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
	public class X509Data
	{
		[XmlElement(ElementName = "X509Certificate", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public string X509Certificate { get; set; }
	}

	[XmlRoot(ElementName = "KeyInfo", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
	public class KeyInfo
	{
		[XmlElement(ElementName = "X509Data", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public X509Data X509Data { get; set; }
	}

	[XmlRoot(ElementName = "Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
	public class Signature
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

	[XmlRoot(ElementName = "inutNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public class InutNFe
	{
		[XmlElement(ElementName = "infInut", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public InfInut InfInut { get; set; }
		[XmlElement(ElementName = "Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public Signature Signature { get; set; }
		[XmlAttribute(AttributeName = "versao")]
		public string Versao { get; set; }
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
	}

	[XmlRoot(ElementName = "retInutNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public class RetInutNFe
	{
		[XmlElement(ElementName = "infInut", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public InfInut InfInut { get; set; }
		[XmlAttribute(AttributeName = "versao")]
		public string Versao { get; set; }
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
	}

	[XmlRoot(ElementName = "ProcInutNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public class ProcInutNFe
	{
		[XmlElement(ElementName = "inutNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public InutNFe InutNFe { get; set; }
		[XmlElement(ElementName = "retInutNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public RetInutNFe RetInutNFe { get; set; }
		[XmlAttribute(AttributeName = "versao")]
		public string Versao { get; set; }
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
	}
}
