namespace SistemaControle.Domain.Shared;

public class CatalogoDeErros
{
    public static string FalhaObterSistemaPorId => "CON0001";

    public static string ObterMensagem(string codigoErro) => Resources.ResourceManager.GetString(codigoErro) ?? "Erro Interno";
}
