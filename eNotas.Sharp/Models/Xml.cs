using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace eNotas.Sharp.Models.Xml
{
	[XmlRoot(ElementName = "NFref", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class NFref
	{
		[XmlElement(ElementName = "refNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string RefNFe { get; set; }
	}

	[XmlRoot(ElementName = "ide", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class Ide
	{
		[XmlElement(ElementName = "cUF", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CUF { get; set; }
		[XmlElement(ElementName = "cNF", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CNF { get; set; }
		[XmlElement(ElementName = "natOp", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string NatOp { get; set; }
		[XmlElement(ElementName = "mod", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string Mod { get; set; }
		[XmlElement(ElementName = "serie", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string Serie { get; set; }
		[XmlElement(ElementName = "nNF", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string NNF { get; set; }
		[XmlElement(ElementName = "dhEmi", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string DhEmi { get; set; }
		[XmlElement(ElementName = "dhSaiEnt", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string DhSaiEnt { get; set; }
		[XmlElement(ElementName = "tpNF", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string TpNF { get; set; }
		[XmlElement(ElementName = "idDest", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string IdDest { get; set; }
		[XmlElement(ElementName = "cMunFG", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CMunFG { get; set; }
		[XmlElement(ElementName = "tpImp", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string TpImp { get; set; }
		[XmlElement(ElementName = "tpEmis", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string TpEmis { get; set; }
		[XmlElement(ElementName = "cDV", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CDV { get; set; }
		[XmlElement(ElementName = "tpAmb", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string TpAmb { get; set; }
		[XmlElement(ElementName = "finNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string FinNFe { get; set; }
		[XmlElement(ElementName = "indFinal", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string IndFinal { get; set; }
		[XmlElement(ElementName = "indPres", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string IndPres { get; set; }
		[XmlElement(ElementName = "indIntermed", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string IndIntermed { get; set; }
		[XmlElement(ElementName = "procEmi", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string ProcEmi { get; set; }
		[XmlElement(ElementName = "verProc", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VerProc { get; set; }
		[XmlElement(ElementName = "NFref", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public NFref NFref { get; set; }
	}

	[XmlRoot(ElementName = "enderEmit", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class EnderEmit
	{
		[XmlElement(ElementName = "xLgr", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XLgr { get; set; }
		[XmlElement(ElementName = "nro", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string Nro { get; set; }
		[XmlElement(ElementName = "xBairro", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XBairro { get; set; }
		[XmlElement(ElementName = "cMun", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CMun { get; set; }
		[XmlElement(ElementName = "xMun", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XMun { get; set; }
		[XmlElement(ElementName = "UF", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string UF { get; set; }
		[XmlElement(ElementName = "CEP", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CEP { get; set; }
		[XmlElement(ElementName = "cPais", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CPais { get; set; }
		[XmlElement(ElementName = "xPais", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XPais { get; set; }
		[XmlElement(ElementName = "fone", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string Fone { get; set; }
	}

	[XmlRoot(ElementName = "emit", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class Emit
	{
		[XmlElement(ElementName = "CNPJ", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CNPJ { get; set; }
		[XmlElement(ElementName = "xNome", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XNome { get; set; }
		[XmlElement(ElementName = "xFant", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XFant { get; set; }
		[XmlElement(ElementName = "enderEmit", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public EnderEmit EnderEmit { get; set; }
		[XmlElement(ElementName = "IE", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string IE { get; set; }
		[XmlElement(ElementName = "IM", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string IM { get; set; }
		[XmlElement(ElementName = "CRT", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CRT { get; set; }
	}

	[XmlRoot(ElementName = "enderDest", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class EnderDest
	{
		[XmlElement(ElementName = "xLgr", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XLgr { get; set; }
		[XmlElement(ElementName = "nro", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string Nro { get; set; }
		[XmlElement(ElementName = "xBairro", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XBairro { get; set; }
		[XmlElement(ElementName = "cMun", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CMun { get; set; }
		[XmlElement(ElementName = "xMun", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XMun { get; set; }
		[XmlElement(ElementName = "UF", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string UF { get; set; }
		[XmlElement(ElementName = "CEP", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CEP { get; set; }
		[XmlElement(ElementName = "cPais", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CPais { get; set; }
		[XmlElement(ElementName = "xPais", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XPais { get; set; }
	}

	[XmlRoot(ElementName = "dest", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class Dest
	{
		[XmlElement(ElementName = "CNPJ", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CNPJ { get; set; }
		[XmlElement(ElementName = "xNome", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XNome { get; set; }
		[XmlElement(ElementName = "enderDest", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public EnderDest EnderDest { get; set; }
		[XmlElement(ElementName = "indIEDest", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string IndIEDest { get; set; }
		[XmlElement(ElementName = "IE", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string IE { get; set; }
	}

	[XmlRoot(ElementName = "entrega", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class Entrega
	{
		[XmlElement(ElementName = "CNPJ", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CNPJ { get; set; }
		[XmlElement(ElementName = "xNome", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XNome { get; set; }
		[XmlElement(ElementName = "xLgr", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XLgr { get; set; }
		[XmlElement(ElementName = "nro", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string Nro { get; set; }
		[XmlElement(ElementName = "xBairro", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XBairro { get; set; }
		[XmlElement(ElementName = "cMun", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CMun { get; set; }
		[XmlElement(ElementName = "xMun", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XMun { get; set; }
		[XmlElement(ElementName = "UF", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string UF { get; set; }
		[XmlElement(ElementName = "CEP", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CEP { get; set; }
	}

	[XmlRoot(ElementName = "prod", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class Prod
	{
		[XmlElement(ElementName = "cProd", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CProd { get; set; }
		[XmlElement(ElementName = "cEAN", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CEAN { get; set; }
		[XmlElement(ElementName = "xProd", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XProd { get; set; }
		[XmlElement(ElementName = "NCM", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string NCM { get; set; }
		[XmlElement(ElementName = "CEST", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CEST { get; set; }
		[XmlElement(ElementName = "CFOP", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CFOP { get; set; }
		[XmlElement(ElementName = "uCom", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string UCom { get; set; }
		[XmlElement(ElementName = "qCom", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string QCom { get; set; }
		[XmlElement(ElementName = "vUnCom", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VUnCom { get; set; }
		[XmlElement(ElementName = "vProd", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VProd { get; set; }
		[XmlElement(ElementName = "cEANTrib", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CEANTrib { get; set; }
		[XmlElement(ElementName = "uTrib", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string UTrib { get; set; }
		[XmlElement(ElementName = "qTrib", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string QTrib { get; set; }
		[XmlElement(ElementName = "vUnTrib", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VUnTrib { get; set; }
		[XmlElement(ElementName = "indTot", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string IndTot { get; set; }
	}

	[XmlRoot(ElementName = "ICMSSN101", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class ICMSSN101
	{
		[XmlElement(ElementName = "orig", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string Orig { get; set; }
		[XmlElement(ElementName = "CSOSN", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CSOSN { get; set; }
		[XmlElement(ElementName = "pCredSN", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string PCredSN { get; set; }
		[XmlElement(ElementName = "vCredICMSSN", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VCredICMSSN { get; set; }
	}

	[XmlRoot(ElementName = "ICMS", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class ICMS
	{
		[XmlElement(ElementName = "ICMSSN101", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public ICMSSN101 ICMSSN101 { get; set; }
	}

	[XmlRoot(ElementName = "PISOutr", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class PISOutr
	{
		[XmlElement(ElementName = "CST", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CST { get; set; }
		[XmlElement(ElementName = "vBC", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VBC { get; set; }
		[XmlElement(ElementName = "pPIS", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string PPIS { get; set; }
		[XmlElement(ElementName = "vPIS", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VPIS { get; set; }
	}

	[XmlRoot(ElementName = "PIS", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class PIS
	{
		[XmlElement(ElementName = "PISOutr", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public PISOutr PISOutr { get; set; }
	}

	[XmlRoot(ElementName = "COFINSOutr", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class COFINSOutr
	{
		[XmlElement(ElementName = "CST", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CST { get; set; }
		[XmlElement(ElementName = "vBC", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VBC { get; set; }
		[XmlElement(ElementName = "pCOFINS", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string PCOFINS { get; set; }
		[XmlElement(ElementName = "vCOFINS", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VCOFINS { get; set; }
	}

	[XmlRoot(ElementName = "COFINS", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class COFINS
	{
		[XmlElement(ElementName = "COFINSOutr", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public COFINSOutr COFINSOutr { get; set; }
	}

	[XmlRoot(ElementName = "imposto", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class Imposto
	{
		[XmlElement(ElementName = "ICMS", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public ICMS ICMS { get; set; }
		[XmlElement(ElementName = "PIS", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public PIS PIS { get; set; }
		[XmlElement(ElementName = "COFINS", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public COFINS COFINS { get; set; }
	}

	[XmlRoot(ElementName = "det", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class Det
	{
		[XmlElement(ElementName = "prod", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public Prod Prod { get; set; }
		[XmlElement(ElementName = "imposto", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public Imposto Imposto { get; set; }
		[XmlAttribute(AttributeName = "nItem")]
		public string NItem { get; set; }
	}

	[XmlRoot(ElementName = "ICMSTot", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class ICMSTot
	{
		[XmlElement(ElementName = "vBC", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VBC { get; set; }
		[XmlElement(ElementName = "vICMS", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VICMS { get; set; }
		[XmlElement(ElementName = "vICMSDeson", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VICMSDeson { get; set; }
		[XmlElement(ElementName = "vFCPUFDest", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VFCPUFDest { get; set; }
		[XmlElement(ElementName = "vICMSUFDest", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VICMSUFDest { get; set; }
		[XmlElement(ElementName = "vICMSUFRemet", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VICMSUFRemet { get; set; }
		[XmlElement(ElementName = "vFCP", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VFCP { get; set; }
		[XmlElement(ElementName = "vBCST", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VBCST { get; set; }
		[XmlElement(ElementName = "vST", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VST { get; set; }
		[XmlElement(ElementName = "vFCPST", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VFCPST { get; set; }
		[XmlElement(ElementName = "vFCPSTRet", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VFCPSTRet { get; set; }
		[XmlElement(ElementName = "vProd", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VProd { get; set; }
		[XmlElement(ElementName = "vFrete", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VFrete { get; set; }
		[XmlElement(ElementName = "vSeg", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VSeg { get; set; }
		[XmlElement(ElementName = "vDesc", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VDesc { get; set; }
		[XmlElement(ElementName = "vII", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VII { get; set; }
		[XmlElement(ElementName = "vIPI", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VIPI { get; set; }
		[XmlElement(ElementName = "vIPIDevol", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VIPIDevol { get; set; }
		[XmlElement(ElementName = "vPIS", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VPIS { get; set; }
		[XmlElement(ElementName = "vCOFINS", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VCOFINS { get; set; }
		[XmlElement(ElementName = "vOutro", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VOutro { get; set; }
		[XmlElement(ElementName = "vNF", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VNF { get; set; }
	}

	[XmlRoot(ElementName = "total", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class Total
	{
		[XmlElement(ElementName = "ICMSTot", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public ICMSTot ICMSTot { get; set; }
	}

	[XmlRoot(ElementName = "transp", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class Transp
	{
		[XmlElement(ElementName = "modFrete", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string ModFrete { get; set; }
	}

	[XmlRoot(ElementName = "detPag", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class DetPag
	{
		[XmlElement(ElementName = "indPag", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string IndPag { get; set; }
		[XmlElement(ElementName = "tPag", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string TPag { get; set; }
		[XmlElement(ElementName = "vPag", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VPag { get; set; }
	}

	[XmlRoot(ElementName = "pag", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class Pag
	{
		[XmlElement(ElementName = "detPag", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public DetPag DetPag { get; set; }
	}

	[XmlRoot(ElementName = "infAdic", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class InfAdic
	{
		[XmlElement(ElementName = "infCpl", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string InfCpl { get; set; }
	}

	[XmlRoot(ElementName = "infRespTec", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class InfRespTec
	{
		[XmlElement(ElementName = "CNPJ", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CNPJ { get; set; }
		[XmlElement(ElementName = "xContato", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XContato { get; set; }
		[XmlElement(ElementName = "email", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string Email { get; set; }
		[XmlElement(ElementName = "fone", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string Fone { get; set; }
	}

	[XmlRoot(ElementName = "infNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class InfNFe
	{
		[XmlElement(ElementName = "ide", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public Ide Ide { get; set; }
		[XmlElement(ElementName = "emit", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public Emit Emit { get; set; }
		[XmlElement(ElementName = "dest", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public Dest Dest { get; set; }
		[XmlElement(ElementName = "entrega", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public Entrega Entrega { get; set; }
		[XmlElement(ElementName = "det", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public Det Det { get; set; }
		[XmlElement(ElementName = "total", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public Total Total { get; set; }
		[XmlElement(ElementName = "transp", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public Transp Transp { get; set; }
		[XmlElement(ElementName = "pag", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public Pag Pag { get; set; }
		[XmlElement(ElementName = "infAdic", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public InfAdic InfAdic { get; set; }
		[XmlElement(ElementName = "infRespTec", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public InfRespTec InfRespTec { get; set; }
		[XmlAttribute(AttributeName = "versao")]
		public string Versao { get; set; }
		[XmlAttribute(AttributeName = "Id")]
		public string Id { get; set; }
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

	[XmlRoot(ElementName = "NFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class NFe
	{
		[XmlElement(ElementName = "infNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public InfNFe InfNFe { get; set; }
		[XmlElement(ElementName = "Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public Signature Signature { get; set; }
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
	}

	[XmlRoot(ElementName = "infProt", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class InfProt
	{
		[XmlElement(ElementName = "tpAmb", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string TpAmb { get; set; }
		[XmlElement(ElementName = "verAplic", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string VerAplic { get; set; }
		[XmlElement(ElementName = "chNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string ChNFe { get; set; }
		[XmlElement(ElementName = "dhRecbto", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string DhRecbto { get; set; }
		[XmlElement(ElementName = "nProt", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string NProt { get; set; }
		[XmlElement(ElementName = "digVal", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string DigVal { get; set; }
		[XmlElement(ElementName = "cStat", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string CStat { get; set; }
		[XmlElement(ElementName = "xMotivo", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public string XMotivo { get; set; }
	}

	[XmlRoot(ElementName = "protNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class ProtNFe
	{
		[XmlElement(ElementName = "infProt", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public InfProt InfProt { get; set; }
		[XmlAttribute(AttributeName = "versao")]
		public string Versao { get; set; }
	}

	[XmlRoot(ElementName = "nfeProc", Namespace = "http://www.portalfiscal.inf.br/nfe")]
	public partial class NfeProc
	{
		[XmlElement(ElementName = "NFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public NFe NFe { get; set; }
		[XmlElement(ElementName = "protNFe", Namespace = "http://www.portalfiscal.inf.br/nfe")]
		public ProtNFe ProtNFe { get; set; }
		[XmlAttribute(AttributeName = "versao")]
		public string Versao { get; set; }
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
	}

}
