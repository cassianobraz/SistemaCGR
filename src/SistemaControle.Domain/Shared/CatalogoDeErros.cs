namespace SistemaControle.Domain.Shared;

public class CatalogoDeErros
{
    public static string DescricaoCategoriaNull => "CGR0001";
    public static string FinalidadeCategoriaInvalida => "CGR0002";
    public static string PessoaNaoEncontrada => "CGR0003";
    public static string DescricaoTransacaoNull => "CGR0004";
    public static string TipoTransacaoInvalida => "CGR0005";
    public static string ValorTransacaoInvalida => "CGR0006";
    public static string CategoriaTransacaoNull => "CGR0007";
    public static string PessoaTransacaoNull => "CGR0008";

    public static string ObterMensagem(string codigoErro) => Resources.ResourceManager.GetString(codigoErro) ?? "Erro Interno";
}
