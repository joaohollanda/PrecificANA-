using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace PrecificAna
{
    public partial class App : Application
    {
        public App()
        {
            // Força o WPF a usar o padrão de moeda, data e números do seu Windows (pt-BR)
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        }
    }
}