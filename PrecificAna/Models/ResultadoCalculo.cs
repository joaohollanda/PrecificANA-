using System;
using System.Collections.Generic;
using System.Text;

namespace PrecificAna.Models;

public class ResultadoCalculo
{
    public decimal CustoArgila { get; set; }
    public decimal CustoQueimaBiscoito { get; set; }
    public decimal CustoMaterialEsmalte { get; set; }
    public decimal CustoQueimaAlta { get; set; }
    public decimal CustoMaoDeObra { get; set; }
    public decimal CustoTotalProducao { get; set; }
    public decimal PrecoComLucro { get; set; }
}
