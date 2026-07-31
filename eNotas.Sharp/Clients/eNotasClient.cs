using eNotas.Sharp.Models;
using eNotas.Sharp.Services;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace eNotas.Sharp.Clients
{
    public class eNotasClient : IDisposable
    {
        private string _url = "https://api.enotasgw.com.br";
        private string _apiKey = "";
        private RestService _client;

        public eNotasClient(string apiKey)
        {
            _apiKey = apiKey;
            _client = new RestService(_url, _apiKey);
        }

        internal eNotasClient(string apiKey, HttpMessageHandler handler)
        {
            _apiKey = apiKey;
            _client = new RestService(_url, _apiKey, handler);
        }

        #region NFe

        public async Task<ApiResponse> EmitirNfe(Nota nota, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/nf-e";

            return await _client.Post(path, nota, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse<Consulta>> ConsultaNfe(string notaId, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/nf-e/{notaId}";

            return await _client.Get<Consulta>(path, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse<Models.Xml.NfeProc>> ConsultaNfeXML(string notaId, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/nf-e/{notaId}/xml";

            return await _client.Get<Models.Xml.NfeProc>(path, "xml", cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse<Models.XmlCancelamento.ProcEventoNFe>> ConsultaNfeXMLCancelamento(string notaId, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/nf-e/{notaId}/xmlCancelamento";

            return await _client.Get<Models.XmlCancelamento.ProcEventoNFe>(path, "xml", cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse> CancelaNfe(string notaId, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/nf-e/{notaId}";

            return await _client.Delete(path, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse> InutilizacaoNfe(Inutilizacao inutilizacao, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/nf-e/inutilizacao";

            return await _client.Post(path, inutilizacao, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse<ConsultaInutilizacao>> ConsultaInutilizacaoNfe(string inutilizacaoId, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/nf-e/inutilizacao/{inutilizacaoId}";

            return await _client.Get<ConsultaInutilizacao>(path, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse<Models.XmlInutilizacao.ProcInutNFe>> ConsultaInutilizacaoXMLNfe(string inutilizacaoId, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/nf-e/inutilizacao/{inutilizacaoId}/xml";

            return await _client.Get<Models.XmlInutilizacao.ProcInutNFe>(path, "xml", cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse> CartaDeCorrecao(CartaCorrecao cartaCorrecao, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/nf-e/cartaCorrecao";

            return await _client.Post(path, cartaCorrecao, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse<CorrecaoResponse>> ConsultaCartaDeCorrecao(string cartaCorrecaoId, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/nf-e/cartaCorrecao/{cartaCorrecaoId}";

            return await _client.Get<CorrecaoResponse>(path, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse<Models.XmlCorrecao.ProcEventoNFe>> ConsultaCartaDeCorrecaoXml(string cartaCorrecaoId, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/nf-e/cartaCorrecao/{cartaCorrecaoId}/xml";

            return await _client.Get<Models.XmlCorrecao.ProcEventoNFe>(path, "xml", cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse> ConsultaManifestacao(string chaveAcesso, string empresaId, CancellationToken cancellationToken = default)
        {
            // Host api2 / v3 (Postman); URL absoluta — não altera _url padrão (api.enotasgw.com.br).
            string path = $"https://api2.enotasgw.com.br/v3/empresas/{empresaId}/nf-e/manifestacao/{chaveAcesso}";

            return await _client.Get(path, cancellationToken).ConfigureAwait(false);
        }

        #endregion

        #region NFCe
        public async Task<ApiResponse> EmitirNfce(Nota nota, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/nfc-e";

            return await _client.Post(path, nota, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse<Consulta>> ConsultaNfce(string notaId, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/nfc-e/{notaId}";

            return await _client.Get<Consulta>(path, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse<Models.Xml.NfeProc>> ConsultaNfceXML(string notaId, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/nfc-e/{notaId}/xml";

            return await _client.Get<Models.Xml.NfeProc>(path, "xml", cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse<Models.XmlCancelamento.ProcEventoNFe>> ConsultaNfceXMLCancelamento(string notaId, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/nfc-e/{notaId}/xmlCancelamento";

            return await _client.Get<Models.XmlCancelamento.ProcEventoNFe>(path, "xml", cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse> CancelaNfce(string notaId, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/nfc-e/{notaId}";

            return await _client.Delete(path, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse> InutilizacaoNfce(Inutilizacao inutilizacao, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/nfc-e/inutilizacao";

            return await _client.Post(path, inutilizacao, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse<ConsultaInutilizacao>> ConsultaInutilizacaoNfce(string inutilizacaoId, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/nfc-e/inutilizacao/{inutilizacaoId}";

            return await _client.Get<ConsultaInutilizacao>(path, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse<Models.XmlInutilizacao.ProcInutNFe>> ConsultaInutilizacaoXMLNfce(string inutilizacaoId, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/nfc-e/inutilizacao/{inutilizacaoId}/xml";

            return await _client.Get<Models.XmlInutilizacao.ProcInutNFe>(path, "xml", cancellationToken).ConfigureAwait(false);
        }
        #endregion

        #region NFSe

        public async Task<ApiResponse> EmitirNfse(Nfse nfse, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v1/empresas/{empresaId}/nfes";

            return await _client.Post(path, nfse, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse<ConsultaNfse>> ConsultaNfse(string nfeId, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v1/empresas/{empresaId}/nfes/{nfeId}";

            return await _client.Get<ConsultaNfse>(path, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse<ConsultaNfse>> ConsultaNfsePorIdExterno(string idExterno, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v1/empresas/{empresaId}/nfes/porIdExterno/{idExterno}";

            return await _client.Get<ConsultaNfse>(path, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse<ListaNfse>> ListarNfse(
            string empresaId,
            int pageNumber,
            int pageSize,
            string sortBy = null,
            string sortDirection = null,
            string filter = null,
            CancellationToken cancellationToken = default)
        {
            var path = new StringBuilder($"/v1/empresas/{empresaId}/nfes?pageNumber={pageNumber}&pageSize={pageSize}");

            if (!string.IsNullOrEmpty(sortBy))
                path.Append("&sortBy=").Append(Uri.EscapeDataString(sortBy));

            if (!string.IsNullOrEmpty(sortDirection))
                path.Append("&sortDirection=").Append(Uri.EscapeDataString(sortDirection));

            if (!string.IsNullOrEmpty(filter))
                path.Append("&filter=").Append(Uri.EscapeDataString(filter));

            return await _client.Get<ListaNfse>(path.ToString(), cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse> CancelaNfse(string nfeId, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v1/empresas/{empresaId}/nfes/{nfeId}";

            return await _client.Delete(path, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse> CancelaNfsePorIdExterno(string idExterno, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v1/empresas/{empresaId}/nfes/porIdExterno/{idExterno}";

            return await _client.Delete(path, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse> ConsultaNfseXML(string nfeId, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v1/empresas/{empresaId}/nfes/{nfeId}/xml";

            return await _client.Get(path, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse> ConsultaNfseXMLPorIdExterno(string idExterno, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v1/empresas/{empresaId}/nfes/porIdExterno/{idExterno}/xml";

            return await _client.Get(path, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse<byte[]>> ConsultaNfsePDF(string nfeId, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v1/empresas/{empresaId}/nfes/{nfeId}/pdf";

            return await _client.GetBytes(path, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse<byte[]>> ConsultaNfsePDFPorIdExterno(string idExterno, string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v1/empresas/{empresaId}/nfes/porIdExterno/{idExterno}/pdf";

            return await _client.GetBytes(path, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse> ConsultaServicosMunicipais(
            string uf,
            string nomeCidade,
            int pageNumber,
            int pageSize,
            string filter = null,
            CancellationToken cancellationToken = default)
        {
            var path = new StringBuilder(
                $"/v1/estados/{Uri.EscapeDataString(uf)}/cidades/{Uri.EscapeDataString(nomeCidade)}/servicos?pageNumber={pageNumber}&pageSize={pageSize}");

            if (!string.IsNullOrEmpty(filter))
                path.Append("&filter=").Append(Uri.EscapeDataString(filter));

            return await _client.Get(path.ToString(), cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse> ConsultaServicosMunicipaisUnificados(
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            string path = $"/v1/servicos/cidades?pageNumber={pageNumber}&pageSize={pageSize}";

            return await _client.Get(path, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse> ConsultaProvedorCidade(string codigoIBGECidade, CancellationToken cancellationToken = default)
        {
            string path = $"/v1/estados/cidades/{Uri.EscapeDataString(codigoIBGECidade)}/provedor";

            return await _client.Get(path, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse> CriticarDadosObrigatorios(string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v1/empresas/{empresaId}/criticardadosobrigatorios";

            return await _client.Get(path, cancellationToken).ConfigureAwait(false);
        }

        #endregion

        #region Empresas

        public async Task<ApiResponse> IncluirAlterarEmpresa(Empresa empresa, CancellationToken cancellationToken = default)
        {
            string path = "/v2/empresas";

            return await _client.Post(path, empresa, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse<Empresa>> ConsultaEmpresa(string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}";

            return await _client.Get<Empresa>(path, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse<ListaEmpresas>> ListarEmpresas(
            int pageNumber,
            int pageSize,
            string searchBy = null,
            string searchTerm = null,
            string sortBy = null,
            string sortDirection = null,
            CancellationToken cancellationToken = default)
        {
            var path = new StringBuilder($"/v2/empresas?pageNumber={pageNumber}&pageSize={pageSize}");

            if (!string.IsNullOrEmpty(searchBy))
                path.Append("&searchBy=").Append(Uri.EscapeDataString(searchBy));

            if (!string.IsNullOrEmpty(searchTerm))
                path.Append("&searchTerm=").Append(Uri.EscapeDataString(searchTerm));

            if (!string.IsNullOrEmpty(sortBy))
                path.Append("&sortBy=").Append(Uri.EscapeDataString(sortBy));

            if (!string.IsNullOrEmpty(sortDirection))
                path.Append("&sortDirection=").Append(Uri.EscapeDataString(sortDirection));

            return await _client.Get<ListaEmpresas>(path.ToString(), cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse> VincularCertificadoDigital(
            string empresaId,
            byte[] arquivo,
            string senha,
            string nomeArquivo = "certificado.pfx",
            CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/certificadoDigital";

            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(senha), "senha");
                content.Add(new ByteArrayContent(arquivo), "arquivo", nomeArquivo);

                return await _client.PostMultipart(path, content, cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task<ApiResponse> VincularLogotipo(
            string empresaId,
            byte[] arquivo,
            string nomeArquivo = "logo.png",
            CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/logo";

            using (var content = new MultipartFormDataContent())
            {
                content.Add(new ByteArrayContent(arquivo), "logotipo", nomeArquivo);

                return await _client.PostMultipart(path, content, cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task<ApiResponse> DesabilitarEmpresa(string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v1/empresas/{empresaId}/desabilitar";

            return await _client.Post(path, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse> HabilitarEmpresa(string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v1/empresas/{empresaId}/habilitar";

            return await _client.Post(path, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse> SetupSat(string empresaId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/empresas/{empresaId}/sat/setup";

            return await _client.Get(path, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ApiResponse> ConsultaSat(string satId, CancellationToken cancellationToken = default)
        {
            string path = $"/v2/sat/{satId}/all";

            return await _client.Get(path, cancellationToken).ConfigureAwait(false);
        }

        #endregion

        public void Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            _client?.Dispose();
        }
    }
}
