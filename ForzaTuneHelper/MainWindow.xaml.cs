using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ForzaTuneHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private double FrontWeight = 0;
        private double BackWeight = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TxtFrontWeight_LostFocus(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(TxtFrontWeight.Text, out double result))
            {
                if (result < 0 || result > 100)
                {
                    TxtFrontWeight.BorderBrush = Brushes.Red;
                }
                else
                {
                    TxtFrontWeight.BorderBrush = Brushes.Green;
                    FrontWeight = result / 100;
                    BackWeight = 1 - FrontWeight;
                }
            }
            else if (!string.IsNullOrEmpty(TxtFrontWeight.Text))
            {
                TxtFrontWeight.BorderBrush = Brushes.Red;
            }
        }

        private bool CheckForErrors()
        {
            if(TxtFrontWeight.BorderBrush == Brushes.Red || 
                TxtArbSoftest.BorderBrush == Brushes.Red ||
                TxtArbStiffest.BorderBrush == Brushes.Red ||
                TxtDmpSoftest.BorderBrush == Brushes.Red ||
                TxtDmpStiffest.BorderBrush == Brushes.Red ||
                TxtSprSoftest.BorderBrush == Brushes.Red ||
                TxtSprStiffest.BorderBrush == Brushes.Red)
            {
                return false;
            }
            else if (TxtFrontWeight.BorderBrush == Brushes.Green ||
                TxtArbSoftest.BorderBrush == Brushes.Green ||
                TxtArbStiffest.BorderBrush == Brushes.Green ||
                TxtDmpSoftest.BorderBrush == Brushes.Green ||
                TxtDmpStiffest.BorderBrush == Brushes.Green ||
                TxtSprSoftest.BorderBrush == Brushes.Green ||
                TxtSprStiffest.BorderBrush == Brushes.Green)
            {
                return true;
            }
            return false;

        }

        private async void BtnTune_ClickAsync(object sender, RoutedEventArgs e)
        {
            if(CheckForErrors())
            {
                TxtArbFront.Text = Calculate(FrontWeight, double.Parse(TxtArbSoftest.Text), double.Parse(TxtArbStiffest.Text)).ToString();
                TxtArbBack.Text = Calculate(BackWeight, double.Parse(TxtArbSoftest.Text), double.Parse(TxtArbStiffest.Text)).ToString();

                TxtSprFront.Text = Calculate(FrontWeight, double.Parse(TxtSprSoftest.Text), double.Parse(TxtSprStiffest.Text)).ToString();
                TxtSprBack.Text = Calculate(BackWeight, double.Parse(TxtSprSoftest.Text), double.Parse(TxtSprStiffest.Text)).ToString();

                TxtDmpFront.Text = Calculate(FrontWeight, double.Parse(TxtDmpSoftest.Text), double.Parse(TxtDmpStiffest.Text)).ToString();
                TxtDmpBack.Text = Calculate(BackWeight, double.Parse(TxtDmpSoftest.Text), double.Parse(TxtDmpStiffest.Text)).ToString();
            }
            else
            {
                await this.ShowMessageAsync("Error", "Please correct the values and try again.");
            }
        }

        private double Calculate(double weight,double min,double max)
        {
            return ((max - min) * weight) + min;
        }

        private void Txt_LostFocus(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(((TextBox)sender).Text, out double result))
            {
                if (result < 0)
                {
                    ((TextBox)sender).BorderBrush = Brushes.Red;
                }
                else
                {
                    ((TextBox)sender).BorderBrush = Brushes.Green;
                }
            }
            else if (!string.IsNullOrEmpty(((TextBox)sender).Text))
            {
                TxtArbStiffest.BorderBrush = Brushes.Red;
            }
        }
    }
}
