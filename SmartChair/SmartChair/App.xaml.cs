using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace SmartChair
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");

            //NumberFormatInfo nfi = System.Threading.Thread.CurrentThread
            //                        .CurrentCulture.NumberFormat;
            //nfi.CurrencySymbol = "USD";
            //nfi.CurrencyDecimalSeparator = ".";
            //nfi.CurrencyDecimalDigits = 0;

            //Thread.CurrentThread.CurrentUICulture.NumberFormat = nfi;

            ////nfi = System.Threading.Thread.CurrentThread
            ////                        .CurrentUICulture.NumberFormat;
            ////nfi.CurrencySymbol = "USD";
            ////nfi.CurrencyDecimalSeparator = ".";
            ////nfi.CurrencyDecimalDigits = 0;

            //FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
            //    new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(
            //        CultureInfo.CurrentCulture.IetfLanguageTag)));

            base.OnStartup(e);

        }
    }
}
