using CorreiosApiWrapper.Business;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CorreiosApiWrapper.Services
{
    public static class CorreiosApiWrapperResult
    {
        public static async Task<CorreiosServicePrices> GetCorreiosPricesAsync(CorreiosServicesData data,
                                                                               string nCdEmpresa,
                                                                               string sDsSenha,
                                                                               string nCdServico,
                                                                               string sCepOrigem,
                                                                               string sCepDestino,
                                                                               string nVlPeso,
                                                                               int nCdFormato,
                                                                               decimal nVlComprimento,
                                                                               decimal nVlAltura,
                                                                               decimal nVlLargura,
                                                                               decimal nVlDiametro,
                                                                               string sCdMaoPropria,
                                                                               decimal nVlValorDeclarado,
                                                                               string sCdAvisoRecebimento,
                                                                               string strRetorno = "XML",
                                                                               string nIndicaCalculo = "3")
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(data.Uri);

                var request = $"?nCdEmpresa={nCdEmpresa ?? data.Empresa}&sDsSenha={sDsSenha ?? data.Senha}&nCdServico={nCdServico}&sCepOrigem={sCepOrigem}&sCepDestino={sCepDestino}&nVlPeso={nVlPeso}&nCdFormato={nCdFormato}&nVlComprimento={nVlComprimento}&nVlAltura={nVlAltura}&nVlLargura={nVlLargura}&nVlDiametro={nVlDiametro}&sCdMaoPropria={sCdMaoPropria}&nVlValorDeclarado={nVlValorDeclarado}&sCdAvisoRecebimento={sCdAvisoRecebimento}&StrRetorno={strRetorno}&nIndicaCalculo={nIndicaCalculo}";

                var correiosServicePrices = new CorreiosServicePrices
                {
                    CorreiosServices = new List<CorreiosServices>()
                };

                try
                {
                    var xDoc = XDocument.LoadAsync(await client.GetStreamAsync(request), LoadOptions.None, new System.Threading.CancellationToken(false)).Result;
                    var brazilian = new CultureInfo("pt-BR");

                    correiosServicePrices.CorreiosServices = (from e in xDoc.Root.Elements("cServico")
                                  select new CorreiosServices
                                  {
                                     Codigo = (string)e.Element("Codigo"),
                                     Valor = Convert.ToDecimal((string)e.Element("Valor"), brazilian),
                                     PrazoEntrega = Convert.ToInt32((string)e.Element("PrazoEntrega")),
                                     ValorSemAdicionais = Convert.ToDecimal((string)e.Element("ValorSemAdicionais"), brazilian),
                                     ValorMaoPropria = Convert.ToDecimal((string)e.Element("ValorMaoPropria"), brazilian),
                                     ValorAvisoRecebimento = Convert.ToDecimal((string)e.Element("ValorAvisoRecebimento"), brazilian),
                                     ValorValorDeclarado = Convert.ToDecimal((string)e.Element("ValorValorDeclarado"), brazilian),
                                     EntregaDomiciliar = (string)e.Element("EntregaDomiciliar"),
                                     EntregaSabado = (string)e.Element("EntregaSabado"),
                                     Erro = (string)e.Element("Erro"),
                                     MsgErro = (string)e.Element("MsgErro"),
                                     ObsFim = (string)e.Element("ObsFim")
                                  }).ToList();
                }
                catch (Exception e)
                {
                    correiosServicePrices.CorreiosServices.Add(new CorreiosServices
                    {
                        Codigo = null,
                        Valor = 0.00m,
                        PrazoEntrega = 0,
                        ValorSemAdicionais = 0.00m,
                        ValorMaoPropria = 0.00m,
                        ValorAvisoRecebimento = 0.00m,
                        ValorValorDeclarado = 0.00m,
                        EntregaDomiciliar = null,
                        EntregaSabado = null,
                        Erro = e.HResult.ToString(),
                        MsgErro = e.Message,
                        ObsFim = ($"Erro ao acessar {data.Uri.ToString()}")
                    });
                }

                return correiosServicePrices;

            }
        }
    }
}

