using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace pract_4_Kovyazin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Calculator : ContentPage
    {
        public Calculator()
        {
            InitializeComponent();
        }
        private void SliderValueChange(object sender, ValueChangedEventArgs e)
        {
            SliderLabel.Text = $"{Slider.Value}%";

            if (LoanEntry.Text != "" && MonthEntry.Text != "")
            {
                Calculation(LoanEntry.Text, MonthEntry.Text, PaymentTypePicker.SelectedIndex, Slider.Value);
            }
            else
            {
                this.DisplayToastAsync("Введите все значения!!!!!",3000);
                MonthlyPaymentLabel.Text = "Ежемесячный платеж: ...";
                TotalLabel.Text = "Общая сумма: ...";
                OverpaymentLabel.Text = "Переплата: ...";
            }
        }

        private void Calculation(string creditAmount, string loanterminMonths, int PickerPayment, double interestR)
        {
            try
            {
                if (Convert.ToDouble(loanterminMonths) > 0 && Convert.ToDouble(creditAmount) > 0 && PickerPayment == 0)
                {

                    double monthlyInterestRate = interestR / 1200;
                    double annuityFactor = monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, int.Parse(loanterminMonths)) / (Math.Pow(1 + monthlyInterestRate, int.Parse(loanterminMonths)) - 1);
                    double annuityPayment = Math.Round(double.Parse(creditAmount) * annuityFactor, 2); 
                    MonthlyPaymentLabel.Text = $"Ежемесячный платеж: {annuityPayment}";
                    TotalLabel.Text = $"Общая сумма: {Math.Round(annuityPayment, 2) * int.Parse(loanterminMonths)}";
                    OverpaymentLabel.Text = $"Переплата: {Math.Round(Math.Round(annuityPayment, 2) * int.Parse(loanterminMonths) - Math.Round(double.Parse(creditAmount), 2), 2)}";


                }
                else
                {
                    this.DisplayToastAsync("Значения должны быть больше нуля", 3000);
                    MonthlyPaymentLabel.Text = "Ежемесячный платеж: 0";
                    TotalLabel.Text = "Общая сумма: 0";
                    OverpaymentLabel.Text = "Переплата: 0";
                }
            }
            catch
            {
                this.DisplayToastAsync("Вы должны вводить только числа");
            }
        }
    }
}