using System.Collections.Generic;
using System.Threading.Tasks;

namespace App4
{
	public interface ISoapService
	{
        //Task<List<TodoItem>> RefreshDataAsync ();

        Resultado getPrecoprazo(string nCdEmpresa, string sDsSenha, string nCdServico, string sCepOrigem, string sCepDestino, string nVlPeso, int nCdFormato, decimal nVlComprimento, decimal nVlAltura, decimal nVlLargura, decimal nVlDiametro, string sCdMaoPropria, decimal nVlValorDeclarado, string sCdAvisoRecebimento);
        //Task SaveTodoItemAsync (TodoItem item, bool isNewItem);

        //Task DeleteTodoItemAsync (string id);
    }
}
