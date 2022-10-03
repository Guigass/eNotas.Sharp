# eNotas.Sharp
Biblioteca em C# (.Net Standard) para uso dos Endpoints da eNotas.

- Exemplo de Uso
    ```
    //Uso da biblioteca é simples basta dar o using passando sua APIKEY, 
    //chamar o método passando o modelo nescessário e o id da empresa 
    //(APIKEY e empresaID fornecidos pela eNotas).
    using (var enotas = new eNotasClient("apiKey"))
    {
        var nota = new Nota
        {
            //......
        };
        var response = enotas.EmitirNfe(nota, "empresaID").Result;
        // Ou
        // var resp = await enotas.EmitirNfe(nota, "empresaID");
        // Para Metodos ASYNC
    }
    ```

--------------------------------------------------------------------------------------------------
- Instalação no Nuget PM
    ```
    Install-Package eNotas.Sharp
    ```
--------------------------------------------------------------------------------------------------

- Métodos Disponíveis:
    ```
    * Emitir NF-e
    * Consultar NF-e
    * Cancelar NF-e
    * Consultar XML de Cancelamento NF-e (Utilizar o metodo ConsultaNfce)
    * Emitir NFC-e
    * Consultar NFC-e
    * Cancelar NFC-e
    * Consultar XML de Cancelamento NFC-e
    * Inutilizar Numeração NF-e
    * Consultar Inutilização de Número da Nota Fiscal NF-e
    * Consultar XML de Inutilização NF-e
    * Inutilizar Numeração NFC-e
    * Consultar Inutilização de Número da Nota Fiscal NFC-e
    * Consultar XML de Inutilização NFC-e
    * Emitir Carta de Correção pela Chave da NF-e
    * Consultar Carta de Correção NF-e
    ```

- Métodos em Consutrução:
    ```
    * Manifestação de Destinatário NF-e
    ```

- Métodos Futuros:
    ```
    * Incluir/Alterar Empresa
    * Vincular Certificado
    * Vincular Logotipo
    * Consultar Empresa
    * Listar Empresas
    * Download EXE customizado do S@T
    ```
