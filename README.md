# PrecificANA- (PrecificAna)

Calculadora de custos/preço de peças de cerâmica (aplicativo **Windows / WPF**).

O app permite informar dados da peça (peso, tempo e esmalte) e configurar custos (argila, queimas, esmalte, mão de obra e margem), retornando um **preço final estimado com lucro**.

## Tecnologias

- **C# / .NET**: `net10.0-windows`
- **WPF** (UI)
- **MVVM** com [`CommunityToolkit.Mvvm`]
- Persistência simples de configurações em **JSON** (`settings.json`)

## Requisitos

- Windows (WPF)
- **.NET SDK** compatível com `net10.0-windows`
- (Opcional) Visual Studio com workload “Desktop development with .NET”

## Como rodar

### Via Visual Studio
1. Abra o arquivo `PrecificAna.slnx`
2. Selecione o projeto `PrecificAna`
3. Pressione **F5** (Run)

### Via CLI (.NET)
Na raiz do repositório:

```bash
dotnet restore
dotnet build
dotnet run --project PrecificAna/PrecificAna.csproj
```

## Como usar

O app possui 2 abas:

### 1) Calculadora
Preencha os campos:
1. **Peso da Peça Seca (gramas)** (`PesoSeco`)
2. **Tempo de produção (minutos)** (`TempoMinutos`)
3. **Esmalte líquido usado (gramas)** (`GramasEsmalte`)
4. **Peso esmaltado final (gramas)** (`PesoEsmaltado`)  
   - Opcional: se não for informado, o cálculo usa o peso seco como base para a queima alta (margem de segurança).

O campo **“Preço Final Estimado (com lucro)”** é atualizado automaticamente conforme os valores mudam.

### 2) Configurações
Você configura:
- **Preço da argila (R$/kg)** (`PrecoArgilaKg`)
- **Preço queima biscoito (R$/kg)** (`PrecoQueimaBiscoitoKg`)
- **Preço queima alta/esmalte (R$/kg)** (`PrecoQueimaEsmalteKg`)
- **Preço do pote de esmalte (R$)** (`PrecoPoteEsmalte`)
- **Peso do pote de esmalte (gramas)** (`PesoPoteEsmalteGramas`)
- **Valor da hora de trabalho (R$)** (`ValorHoraTrabalho`)
- **Margem de lucro** (`MargemLucroPorcentagem`)

As configurações são salvas automaticamente em disco.

## Onde ficam salvas as configurações?

O serviço `ConfiguracaoService` grava/consulta um arquivo:

- `settings.json` (no diretório de execução do app)

Se o arquivo não existir (primeira execução), o app usa valores padrão no `MainViewModel`.

## Lógica do cálculo (resumo)

O cálculo do preço final está em `PrecificAna/Services/CalculadoraService.cs` e soma:

1. **Argila**: `(pesoSecoKg * precoArgilaKg)`
2. **Queima biscoito**: `(pesoSecoKg * precoQueimaBiscoitoKg)`
3. **Material do esmalte**:  
   - `precoPorGrama = precoPoteEsmalte / pesoPoteEsmalteGramas`
   - `custo = precoPorGrama * gramasEsmalteUsadas`
4. **Queima alta**: `(pesoParaAltaKg * precoQueimaEsmalteKg)`  
   - `pesoParaAlta = pesoEsmaltado ?? pesoSeco`
5. **Mão de obra**: `(tempoMinutos/60 * valorHoraTrabalho)`

Depois aplica lucro:

- `precoFinal = custoTotal * (1 + margemLucro)`

## Estrutura do projeto

```
PrecificAna/
  App.xaml
  MainWindow.xaml
  Models/
    Peca.cs
    Configuracao.cs
  Services/
    CalculadoreService.cs
    ConfiguracaoService.cs
  ViewModels/
    MainViewModel.cs
```

- `MainWindow.xaml`: UI com bindings para o `MainViewModel`
- `MainViewModel.cs`: propriedades observáveis + cálculo do `PrecoFinal` + auto-save de configurações
- `CalculadoraService`: regra de negócio do cálculo
- `ConfiguracaoService`: salva/carrega `settings.json`

## Observações / Melhorias futuras (opcional)

- Criar uma `ResultadoCalculo` real para exibir também os custos individuais calculados (a UI hoje mostra textos fixos em “Custos individuais”).
- Validações de entrada (ex.: impedir valores negativos, lidar com campos vazios e separador decimal).
- Ajustar tipagem/consistência da margem (`MargemLucroPorcentagem`) e unidades (ex.: aceitar 30 vs 0.30).
- Definir um local de gravação mais “padrão” (ex.: `%AppData%`) ao invés do diretório atual.

---

[`CommunityToolkit.Mvvm`]: https://www.nuget.org/packages/CommunityToolkit.Mvvm