using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MortgageCalculator : Page
	{
		public MortgageCalculator()
		{
			this.InitializeComponent();
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			int principle, years, months;
			double yearlyInterestRate, monthlyInterestRate, monthlyRepayment;

			try
			{
				principle = int.Parse(principleTextBox.Text);
			}
			catch (Exception ex)
			{
				var dialogMessage = new MessageDialog("Error! Please ensure all fields are filled. " + ex.Message);
				await dialogMessage.ShowAsync();
				principleTextBox.Focus(FocusState.Programmatic);
				return;
			}
			try
			{
				years = int.Parse(yearsTextBox.Text);
			}
			catch (Exception ex)
			{
				var dialogMessage = new MessageDialog("Error! Please ensure all fields are filled. " + ex.Message);
				await dialogMessage.ShowAsync();
				yearsTextBox.Focus(FocusState.Programmatic);
				return;
			}
			try
			{
				months = int.Parse(monthsTextBox.Text);
			}
			catch (Exception ex)
			{
				var dialogMessage = new MessageDialog("Error! Please ensure all fields are filled. " + ex.Message);
				await dialogMessage.ShowAsync();
				monthsTextBox.Focus(FocusState.Programmatic);
				return;
			}
			try
			{
				yearlyInterestRate = double.Parse(yearlyInterestTextBox.Text);
			}
			catch (Exception ex)
			{
				var dialogMessage = new MessageDialog("Error! Please ensure all fields are filled. " + ex.Message);
				await dialogMessage.ShowAsync();
				yearlyInterestTextBox.Focus(FocusState.Programmatic);
				return;
			}
			monthlyInterestRate = calcMonthlyInterest(yearlyInterestRate);
			monthlyRepayment = calcMortgagePayment(principle, years, months, monthlyInterestRate);
			monthlyInterestTextbox.Text = monthlyInterestRate.ToString();
			monthlyRepaymentTextbox.Text = monthlyRepayment.ToString();
		}

		private static double calcMonthlyInterest(double yearlyInterestRate)
		{
			double monthlyInterestRate = (yearlyInterestRate / 100) / 12;
			return monthlyInterestRate;
		}
		private static double calcMortgagePayment(int principle, int years, int months, double monthlyInterestRate)
		{
			months += (years * 12);
			double denominator = Math.Pow(1 + monthlyInterestRate, months) - 1;
			double monthlyRepayment = principle * (monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, months)) / denominator;
			return monthlyRepayment;
		}
	}
    
}
