using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CorreiosApiWrapper.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Text;
using CorreiosApiWrapper.ViewModels;

namespace CorreiosApiWrapper.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IOptions<CorreiosServicesData> _data;

        public ValuesController(IOptions<CorreiosServicesData> data)
        {
            _data = data;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "RUNNING", "EXECUTANDO" };
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var allCodes = "1 - Formato caixa/pacote\n"
                        +   "2 - Formato rolo/prisma\n"
                        +   "3 - Envelope\n"
                        +   "40010 - SEDEX Varejo\n"
                        +   "40045 - SEDEX a Cobrar Varejo\n"
                        +   "41106 - PAC Varejo\n"
                        +   "04014 - SEDEX à vista\n"
                        +   "04065 - SEDEX à vista pagamanento na entrega\n"
                        +   "04510 - PAC à vista\n"
                        +   "04707 - PAC à vista pagamento na entrega\n"
                        +   "40169 - SEDEX 12 (à vista e a faturar)\n"
                        +   "40215 - SEDEX 10 (à vista e a faturar)\n"
                        +   "40290 - SEDEX Hoje Varejo\n";

            switch (id)
            {
                case 1:
                    return "Formato caixa/pacote";
                case 2:
                    return "Formato rolo/prisma";
                case 3:
                    return "Envelope";
                case 40010:
                    return "SEDEX Varejo";
                case 40045:
                    return "SEDEX a Cobrar Varejo";
                case 41106:
                    return "PAC Varejo";
                case 04014:
                    return "SEDEX à vista";
                case 04065:
                    return "SEDEX à vista pagamento na entrega ";
                case 04510:
                    return "PAC à vista";
                case 04707:
                    return "PAC à vista pagamento na entrega";
                case 40169:
                    return "SEDEX 12 (à vista e a faturar)*";
                case 40215:
                    return "SEDEX 10 (à vista e a faturar)*";
                case 40290:
                    return "SEDEX Hoje Varejo*";
                default:
                    return allCodes;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CorreiosWebServiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            var result = await CorreiosApiWrapperResult.GetCorreiosPricesAsync(_data.Value,
                                                                               _data.Value.Empresa,
                                                                               _data.Value.Senha,
                                                                               model.NCdServico,
                                                                               string.Concat(model.SCepOrigem.Where(char.IsDigit)),
                                                                               string.Concat(model.SCepDestino.Where(char.IsDigit)),
                                                                               model.NVlPeso,
                                                                               model.NCdFormato,
                                                                               model.NVlComprimento,
                                                                               model.NVlAltura,
                                                                               model.NVlLargura,
                                                                               model.NVlDiametro,
                                                                               model.SCdMaoPropria,
                                                                               model.NVlValorDeclarado,
                                                                               model.SCdAvisoRecebimento);

            return Json(result);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
