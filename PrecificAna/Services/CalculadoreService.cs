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

        // Criamos uma constante para o fator de correção de 5% (1.05)
        // Isso garante que o custo da argila cubra a perda de massa
        const decimal FATOR_CORRECAO = 1.05m;

        // 1. Custo da Argila (Usando 1000m para forçar conta decimal e aplicando os 5%)
        resultado.CustoArgila = ((decimal)peca.PesoSecoGramas / 1000m) * FATOR_CORRECAO * config.PrecoArgilaKg;

        // 2. Custo da 1ª Queima (Biscoito - peso seco)
        resultado.CustoQueimaBiscoito = ((decimal)peca.PesoSecoGramas / 1000m) * config.PrecoQueimaBiscoitoKg;

        // 3. Custo do Material do Esmalte
        decimal precoPorGramaEsmalte = config.PrecoPoteEsmalte / (decimal)config.PesoPoteEsmalteGramas;
        resultado.CustoMaterialEsmalte = precoPorGramaEsmalte * (decimal)peca.QuantidadeEsmalteUtilizado;

        // 4. Custo da 2ª Queima (Alta)
        decimal pesoParaAlta = (decimal)(peca.PesoEsmaltadoGramas ?? peca.PesoSecoGramas);
        resultado.CustoQueimaAlta = (pesoParaAlta / 1000m) * config.PrecoQueimaEsmalteKg;

        // 5. Custo da Mão de Obra
        resultado.CustoMaoDeObra = (peca.TempoProducaoMinutos / 60m) * config.ValorHoraTrabalho;

        // SOMA DOS CUSTOS (Custo de Produção)
        resultado.CustoTotalProducao = resultado.CustoArgila +
                                       resultado.CustoQueimaBiscoito +
                                       resultado.CustoMaterialEsmalte +
                                       resultado.CustoQueimaAlta +
                                       resultado.CustoMaoDeObra;

        // 6. Preço Final (Custo + Margem de Lucro)
        // Se a margem é 0.30 (30%), a conta é Custo * 1.30
        resultado.PrecoComLucro = resultado.CustoTotalProducao * (1 + (decimal)config.MargemLucroPorcentagem);

        return resultado;
    }
}