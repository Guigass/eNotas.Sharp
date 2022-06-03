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

        /// <summary>
        /// Esse metodo traz a nota em json deserializado na classe Consulta, caso queria consultar o xml ou o xmlCancelamento é só passar o parametros GET como xml ou xmlCancelamento
        /// </summary>
        /// <param name="notaId"></param>
        /// <param name="empresaId"></param>
        /// <param name="get">xml ou xmlCancelamento</param>
        /// <returns></returns>
        private async Task<dynamic> ConsultaNfe(string notaId, string empresaId, string get = "")
        {
            string path = $"/v2/empresas/{empresaId}/nf-e/{notaId}/{get}";

            if (get == "xml")
                return await _client.Get<Models.Xml.NfeProc>(path, "xml");

            else if (get == "xmlCancelamento")
                return await _client.Get<Models.XmlCancelamento.ProcEventoNFe>(path, "xml");


            return await _client.Get<Consulta>(path);
        }

        public async Task<ApiResponse> CancelaNfe(string notaId, string empresaId)
        {
            string path = $"/v2/empresas/{empresaId}/nf-e/{notaId}";

            return await _client.Delete(path);
        }
        #endregion

        #region NFCe
        public async Task<ApiResponse> EmitirNfce(Nota nota, string empresaId)
        {
            string path = $"/v2/empresas/{empresaId}/nfc-e";

            return await _client.Post(path, nota);
        }

        /// <summary>
        /// Esse metodo traz a nota em json deserializado na classe Consulta, caso queria consultar o xml ou o xmlCancelamento é só passar o parametros GET como xml ou xmlCancelamento
        /// </summary>
        /// <param name="notaId"></param>
        /// <param name="empresaId"></param>
        /// <param name="get">xml ou xmlCancelamento</param>
        /// <returns></returns>
        public async Task<dynamic> ConsultaNfce(string notaId, string empresaId, string get = "")
        {
            string path = $"/v2/empresas/{empresaId}/nfc-e/{notaId}/{get}";

            if (get == "xml")
                return await _client.Get<Models.Xml.NfeProc>(path, "xml");

            else if (get == "xmlCancelamento")
                return await _client.Get<Models.XmlCancelamento.ProcEventoNFe>(path, "xml");


            return await _client.Get<Consulta>(path);
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
