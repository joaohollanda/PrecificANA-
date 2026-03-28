using System;
using System.Collections.Generic;
using System.Text;

namespace PrecificAna.Models;

public class Peca
{
    public string Nome { get; set; } = string.Empty;
    public double PesoSecoGramas { get; set; }
    public double? PesoEsmaltadoGramas { get; set; }
    public int TempoProducaoMinutos { get; set; }
    public double QuantidadeEsmalteUtilizado { get; set; }

}
