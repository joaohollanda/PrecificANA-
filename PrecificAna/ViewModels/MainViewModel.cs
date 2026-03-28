using CommunityToolkit.Mvvm.ComponentModel;
using PrecificAna.Models;
using PrecificAna.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrecificAna.ViewModels;

public partial class MainViewModel : ObservableObject
{

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PrecoFinal))]
    private double _pesoSeco;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PrecoFinal))]
    private double? _pesoEsmaltado;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PrecoFinal))]
    private double _gramasEsmalte;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PrecoFinal))]
    private int _tempoMinutos;

    private readonly CalculadoraService _calculadora = new();

    private Configuracao _config = new()
    {
        // Custos de Fornecedores e Ateliê
        PrecoArgilaKg = 14m,
        PrecoQueimaBiscoitoKg = 19.50m,
        PrecoQueimaEsmalteKg= 35m,

        // Custos de Material (Esmalte em pote)
        PrecoPoteEsmalte = 16m,
        PesoPoteEsmalteGramas = 150,

        // Definições de Negócio
        ValorHoraTrabalho = 25m,
        MargemLucroPorcentagem = 0.30
    };

    public string PrecoFinal
    {
        get
        {
            // Criamos um objeto Peca com o que está digitado na tela agora
            var peca = new Peca
            {
                PesoSecoGramas = PesoSeco,
                PesoEsmaltadoGramas = PesoEsmaltado,
                QuantidadeEsmalteUtilizado = GramasEsmalte,
                TempoProducaoMinutos = TempoMinutos
            };

            // Chamamos o serviço para calcular
            decimal resultado = _calculadora.CalcularPrecoFinal(peca, _config);

            // Retornamos formatado como dinheiro (ex: R$ 55,00)
            return resultado.ToString("C2");
        }
    }

}
