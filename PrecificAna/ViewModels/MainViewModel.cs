using CommunityToolkit.Mvvm.ComponentModel;
using PrecificAna.Models;
using PrecificAna.Services;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace PrecificAna.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private bool _isLoading = true;

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

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PrecoFinal))]
    private decimal _precoArgilaKg;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PrecoFinal))]
    private decimal _precoQueimaBiscoitoKg;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PrecoFinal))]
    private decimal _precoQueimaEsmalteKg;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PrecoFinal))]
    private decimal _precoPoteEsmalte;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PrecoFinal))]
    private decimal _pesoPoteEsmalteGramas;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PrecoFinal))]
    private decimal _valorHoraTrabalho;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PrecoFinal))]
    private decimal _margemLucroPorcentagem;

    [ObservableProperty]
    private ResultadoCalculo _resultado;

    private readonly CalculadoraService _calculadora = new();
    private readonly ConfiguracaoService _configuracaoService = new();

    public MainViewModel()
    {
        _isLoading = true;

        Resultado = new ResultadoCalculo();
        Configuracao configSalva = _configuracaoService.Carregar();

        if (configSalva != null)
        {
            PrecoArgilaKg = configSalva.PrecoArgilaKg;
            PrecoQueimaBiscoitoKg = configSalva.PrecoQueimaBiscoitoKg;
            PrecoQueimaEsmalteKg = configSalva.PrecoQueimaEsmalteKg;
            PrecoPoteEsmalte = configSalva.PrecoPoteEsmalte;
            PesoPoteEsmalteGramas = (decimal)configSalva.PesoPoteEsmalteGramas;
            ValorHoraTrabalho = configSalva.ValorHoraTrabalho;
            MargemLucroPorcentagem = (decimal)configSalva.MargemLucroPorcentagem;
        }
        else
        {
            PrecoArgilaKg = 14m;
            PrecoQueimaBiscoitoKg = 19.50m;
            PrecoQueimaEsmalteKg = 35m;
            PrecoPoteEsmalte = 16m;
            PesoPoteEsmalteGramas = 150;
            ValorHoraTrabalho = 25m;
            MargemLucroPorcentagem = 0.30m;
        }

            _isLoading = false;
    }


    public string PrecoFinal
    {
        get
        {
            var peca = new Peca
            {
                PesoSecoGramas = PesoSeco,
                PesoEsmaltadoGramas = PesoEsmaltado,
                QuantidadeEsmalteUtilizado = GramasEsmalte,
                TempoProducaoMinutos = TempoMinutos
            };

            var configDaTela = new Configuracao
            {
                PrecoArgilaKg = this.PrecoArgilaKg,
                PrecoQueimaBiscoitoKg = this.PrecoQueimaBiscoitoKg,
                PrecoQueimaEsmalteKg = this.PrecoQueimaEsmalteKg,
                PrecoPoteEsmalte = this.PrecoPoteEsmalte,
                PesoPoteEsmalteGramas = (double)this.PesoPoteEsmalteGramas, 
                ValorHoraTrabalho = this.ValorHoraTrabalho,
                MargemLucroPorcentagem = (double)this.MargemLucroPorcentagem
            };
            Resultado = _calculadora.CalcularPrecoFinal(peca, configDaTela);

            return Resultado.PrecoComLucro.ToString("C2");
        }
    }


    protected override void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (_isLoading) return;

        bool isCampoDaPeca = e.PropertyName == nameof(PesoSeco) ||
                             e.PropertyName == nameof(TempoMinutos) ||
                             e.PropertyName == nameof(GramasEsmalte) ||
                             e.PropertyName == nameof(PesoEsmaltado) ||
                             e.PropertyName == nameof(PrecoFinal) ||
                             e.PropertyName == nameof(Resultado);
        if (!isCampoDaPeca)
        {
            var configAtual = new Configuracao
            {
                PrecoArgilaKg = this.PrecoArgilaKg,
                PrecoQueimaBiscoitoKg = this.PrecoQueimaBiscoitoKg,
                PrecoQueimaEsmalteKg = this.PrecoQueimaEsmalteKg,
                PrecoPoteEsmalte = this.PrecoPoteEsmalte,
                PesoPoteEsmalteGramas = (double)this.PesoPoteEsmalteGramas,
                ValorHoraTrabalho = this.ValorHoraTrabalho,
                MargemLucroPorcentagem = (double)this.MargemLucroPorcentagem
            };

            _configuracaoService.SalvarConfiguracao(configAtual);
        }
    }



}
