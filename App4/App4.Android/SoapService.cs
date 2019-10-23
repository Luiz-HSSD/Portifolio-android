using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Services.Protocols;
using App4;
using App4.Droid.br.com.correios.ws;
namespace App4.Droid
{
    public class SoapService : ISoapService
    {
        CalcPrecoPrazoWS todoService;
        TaskCompletionSource<bool> getRequestComplete = null;
        TaskCompletionSource<bool> saveRequestComplete = null;
        TaskCompletionSource<bool> deleteRequestComplete = null;

        //public List<TodoItem> Items { get; private set; } = new List<TodoItem>();

        public SoapService()
        {
            todoService = new br.com.correios.ws.CalcPrecoPrazoWS();
            //todoService.Url = Constants.SoapUrl;

        }

        

        public Resultado FromASMXServiceResultado(cResultado item)
        {
            var b= new Resultado
            {
                Servicos = new Servico[1]
            };
            if(item.Servicos.Length>0)
            {
                b.Servicos[0] = new Servico()
                {
                    Codigo= item.Servicos[0].Codigo,
                    DataMaxEntrega= item.Servicos[0].DataMaxEntrega,
                    EntregaDomiciliar= item.Servicos[0].EntregaDomiciliar,
                    EntregaSabado= item.Servicos[0].EntregaSabado,
                    Erro= item.Servicos[0].Erro,
                    HoraMaxEntrega= item.Servicos[0].HoraMaxEntrega,
                    MsgErro= item.Servicos[0].MsgErro,
                    obsFim= item.Servicos[0].obsFim,
                    PrazoEntrega= item.Servicos[0].PrazoEntrega,
                    Valor= item.Servicos[0].Valor,
                    ValorAvisoRecebimento= item.Servicos[0].ValorAvisoRecebimento,
                    ValorMaoPropria= item.Servicos[0].ValorMaoPropria,
                    ValorSemAdicionais= item.Servicos[0].ValorSemAdicionais,
                    ValorValorDeclarado= item.Servicos[0].ValorValorDeclarado,
                    
                };
                
            }
            return b;
        }

        public Resultado getPrecoprazo(string nCdEmpresa, string sDsSenha, string nCdServico, string sCepOrigem, string sCepDestino, string nVlPeso, int nCdFormato, decimal nVlComprimento, decimal nVlAltura, decimal nVlLargura, decimal nVlDiametro, string sCdMaoPropria, decimal nVlValorDeclarado, string sCdAvisoRecebimento)
        {
            return FromASMXServiceResultado( todoService.CalcPrecoPrazo(nCdEmpresa, sDsSenha, nCdServico, sCepOrigem, sCepDestino, nVlPeso, nCdFormato, nVlComprimento, nVlAltura, nVlLargura, nVlDiametro, sCdMaoPropria, nVlValorDeclarado, sCdAvisoRecebimento));
        }



    }
}
