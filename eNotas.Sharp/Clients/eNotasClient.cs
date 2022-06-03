using eNotas.Sharp.Models;
using eNotas.Sharp.Services;
using System;
using System.Collections.Generic;
using System.Text;
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

        #region NFe
        public async Task<ApiResponse> EmitirNfe(Nota nota, string empresaId)
        {
            string path = $"/v2/empresas/{empresaId}/nf-e";

            return await _client.Post(path, nota);
        }

        public async Task<ApiResponse<Consulta>> ConsultaNfe(string notaId, string empresaId)
        {
            string path = $"/v2/empresas/{empresaId}/nf-e/{notaId}";

            return await _client.Get<Consulta>(path);
        }

        public async Task<ApiResponse<Models.Xml.NfeProc>> ConsultaNfeXML(string notaId, string empresaId)
        {
            string path = $"/v2/empresas/{empresaId}/nf-e/{notaId}/xml";

            return await _client.Get<Models.Xml.NfeProc>(path, "xml");
        }

        public async Task<ApiResponse<Models.XmlCancelamento.ProcEventoNFe>> ConsultaNfeXMLCancelamento(string notaId, string empresaId)
        {
            string path = $"/v2/empresas/{empresaId}/nf-e/{notaId}/xmlCancelamento";

            return await _client.Get<Models.XmlCancelamento.ProcEventoNFe>(path, "xml");
        }

        public async Task<ApiResponse> CancelaNfe(string notaId, string empresaId)
        {
            string path = $"/v2/empresas/{empresaId}/nf-e/{notaId}";

            return await _client.Delete(path);
        }

        public async Task<ApiResponse> CancelaNfe(Inutilizacao inutilizacao, string empresaId)
        {
            string path = $"/v2/empresas/{empresaId}/nf-e/inutilizacao";

            return await _client.Post(path, inutilizacao);
        }
        #endregion

        #region NFCe
        public async Task<ApiResponse> EmitirNfce(Nota nota, string empresaId)
        {
            string path = $"/v2/empresas/{empresaId}/nfc-e";

            return await _client.Post(path, nota);
        }

        public async Task<ApiResponse<Consulta>> ConsultaNfce(string notaId, string empresaId)
        {
            string path = $"/v2/empresas/{empresaId}/nfc-e/{notaId}";

            return await _client.Get<Consulta>(path);
        }

        public async Task<ApiResponse<Models.Xml.NfeProc>> ConsultaNfceXML(string notaId, string empresaId)
        {
            string path = $"/v2/empresas/{empresaId}/nfc-e/{notaId}/xml";

            return await _client.Get<Models.Xml.NfeProc>(path, "xml");
        }

        public async Task<ApiResponse<Models.XmlCancelamento.ProcEventoNFe>> ConsultaNfceXMLCancelamento(string notaId, string empresaId)
        {
            string path = $"/v2/empresas/{empresaId}/nfc-e/{notaId}/xmlCancelamento";

            return await _client.Get<Models.XmlCancelamento.ProcEventoNFe>(path, "xml");
        }

        public async Task<ApiResponse> CancelaNfce(string notaId, string empresaId)
        {
            string path = $"/v2/empresas/{empresaId}/nfc-e/{notaId}";

            return await _client.Delete(path);
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
