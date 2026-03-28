namespace PrecificAna.Models;

public class Configuracao
{
    public decimal PrecoArgilaKg { get; set; }
    public decimal PrecoQueimaBiscoitoKg { get; set; }
    public decimal PrecoQueimaEsmalteKg { get; set; }
    public double TaxaPerdaPeso { get; set; } = 0.10;
    public decimal ValorHoraTrabalho { get; set; }
    public double MargemLucroPorcentagem { get; set; }
    public decimal PrecoPoteEsmalte { get; set; }
    public double PesoPoteEsmalteGramas { get; set; }
}
