using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorreiosApiWrapper.ViewModels
{
    public class CorreiosWebServiceViewModel
    {
        public string NCdEmpresa { get; set; }
        public string SDsSenha { get; set; }
        public string NCdServico { get; set; }
        public string SCepOrigem { get; set; }
        public string SCepDestino { get; set; }
        public string NVlPeso { get; set; }
        public int NCdFormato { get; set; }
        public decimal NVlComprimento { get; set; }
        public decimal NVlAltura { get; set; }
        public decimal NVlLargura { get; set; }
        public decimal NVlDiametro { get; set; }
        public string SCdMaoPropria { get; set; }
        public decimal NVlValorDeclarado { get; set; }
        public string SCdAvisoRecebimento { get; set; }

    }
}
