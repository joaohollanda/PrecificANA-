using PrecificAna.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrecificAna.Services;

public class CalculadoraService
{
    public decimal CalcularPrecoFinal(Peca peca, Configuracao config)
    {
        // 1. Custo da Argila (baseado no peso seco inicial)
        decimal custoArgila = (decimal)(peca.PesoSecoGramas / 1000) * config.PrecoArgilaKg;

        // 2. Custo da 1ª Queima (Biscoito - peso seco)
        decimal custoBiscoito = (decimal)(peca.PesoSecoGramas / 1000) * config.PrecoQueimaBiscoitoKg;

        // 3. Custo do Material do Esmalte (O pote vs. gramas usadas)
        decimal precoPorGramaEsmalte = config.PrecoPoteEsmalte / (decimal)config.PesoPoteEsmalteGramas;
        decimal custoMaterialEsmalte = precoPorGramaEsmalte * (decimal)peca.QuantidadeEsmalteUtilizado;

        // 4. Custo da 2ª Queima (Alta - peso já com esmalte, informado por ela)
        // Se ela não informou o peso esmaltado, usamos o seco como base (margem de segurança)
        double pesoParaAlta = peca.PesoEsmaltadoGramas ?? peca.PesoSecoGramas;
        decimal custoQueimaAlta = (decimal)(pesoParaAlta / 1000) * config.PrecoQueimaEsmalteKg;

        // 5. Custo da Mão de Obra (Minutos convertidos em valor)
        decimal custoMaoDeObra = (peca.TempoProducaoMinutos / 60m) * config.ValorHoraTrabalho;

        // SOMA DE TUDO
        decimal custoTotalProducao = custoArgila + custoBiscoito + custoMaterialEsmalte + custoQueimaAlta + custoMaoDeObra;

        // 6. Preço Final com Margem de Lucro
        decimal precoComLucro = custoTotalProducao * (1 + (decimal)config.MargemLucroPorcentagem);

        return precoComLucro;
    }
}