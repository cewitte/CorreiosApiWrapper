namespace CorreiosApiWrapper.Business
{
    public class CorreiosServices
    {
        public string Codigo { get; set; }
        public decimal Valor { get; set; }
        public int PrazoEntrega { get; set; }
        public decimal ValorSemAdicionais { get; set; }
        public decimal ValorMaoPropria { get; set; }
        public decimal ValorAvisoRecebimento { get; set; }
        public decimal ValorValorDeclarado { get; set; }
        public string EntregaDomiciliar { get; set; }
        public string EntregaSabado { get; set; }
        public string Erro { get; set; }
        public string MsgErro { get; set; }
        public string ObsFim { get; set; }
    }
}