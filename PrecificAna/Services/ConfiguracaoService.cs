using PrecificAna.Models;
using System.Text.Json;
using System.IO;

namespace PrecificAna.Services;

public class ConfiguracaoService
{
    private readonly string _caminhoArquivo = "settings.json";

    public void SalvarConfiguracao(Configuracao config)
    {
        var opcoes = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(config, opcoes);
        System.IO.File.WriteAllText(_caminhoArquivo, json);
    }

    public Configuracao Carregar()
    {
        // Se o arquivo não existir (primeira vez abrindo o app), retorna nulo
        if (!File.Exists(_caminhoArquivo))
            return null;

        // Lê o arquivo do HD e transforma de volta em objeto C#
        string json = File.ReadAllText(_caminhoArquivo);
        return JsonSerializer.Deserialize<Configuracao>(json);
    }
}
