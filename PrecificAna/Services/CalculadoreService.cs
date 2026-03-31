using PrecificAna.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrecificAna.Services;

public class CalculadoraService
{
    public ResultadoCalculo CalcularPrecoFinal(Peca peca, Configuracao config)
    {
        var resultado = new ResultadoCalculo();
        const decimal FATOR_CORRECAO = 1.05m;


        resultado.CustoArgila = ((decimal)peca.PesoSecoGramas / 1000m) * FATOR_CORRECAO * config.PrecoArgilaKg;

        resultado.CustoQueimaBiscoito = ((decimal)peca.PesoSecoGramas / 1000m) * config.PrecoQueimaBiscoitoKg;

        decimal precoPorGramaEsmalte = config.PrecoPoteEsmalte / (decimal)config.PesoPoteEsmalteGramas;
        resultado.CustoMaterialEsmalte = precoPorGramaEsmalte * (decimal)peca.QuantidadeEsmalteUtilizado;

        decimal pesoParaAlta = (decimal)(peca.PesoEsmaltadoGramas ?? peca.PesoSecoGramas);
        resultado.CustoQueimaAlta = (pesoParaAlta / 1000m) * config.PrecoQueimaEsmalteKg;

        resultado.CustoMaoDeObra = (peca.TempoProducaoMinutos / 60m) * config.ValorHoraTrabalho;

        resultado.CustoTotalProducao = resultado.CustoArgila +
                                       resultado.CustoQueimaBiscoito +
                                       resultado.CustoMaterialEsmalte +
                                       resultado.CustoQueimaAlta +
                                       resultado.CustoMaoDeObra;

        resultado.PrecoComLucro = resultado.CustoTotalProducao * (1 + (decimal)config.MargemLucroPorcentagem);

        return resultado;
    }
}