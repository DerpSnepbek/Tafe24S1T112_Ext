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
	public sealed partial class CurrencyConvertor : Page
	{
		public CurrencyConvertor()
		{
			this.InitializeComponent();
		}
		double[] conversionRate = new double[] { 0.85189982, 0.72872436, 74.257327, 1.1739732, 0.8556672, 87.00755, 1.371907, 1.1686692, 101.68635, 0.011492628, 0.013492774, 0.00983393997 };

		private async void ConversionButton_Click(object sender, RoutedEventArgs e)
		{
			int fromSelection = fromCurrencyComboBox.SelectedIndex;
			int toSelection = toCurrencyComboBox.SelectedIndex;

			if (fromSelection < 0 || toSelection < 0)
			{
				var dialog = new MessageDialog("Please select currencies for conversion.");
				await dialog.ShowAsync();
				if (fromSelection < 0)
					fromCurrencyComboBox.Focus(FocusState.Programmatic);
				else
					toCurrencyComboBox.Focus(FocusState.Programmatic);
				return;
			}

			if (fromSelection == toSelection)
			{
				var dialog = new MessageDialog("You cannot convert to the same currency. Please select another currency to convert.");
				await dialog.ShowAsync();
				fromCurrencyComboBox.Focus(FocusState.Programmatic);
				return;
			}

			double toConversionRate = conversionRate[toSelection];

			double amount;
			if (!double.TryParse(amountTextBox.Text, out amount))
			{
				var dialog = new MessageDialog("Please enter a valid numeric value for the amount.");
				await dialog.ShowAsync();
				amountTextBox.Focus(FocusState.Programmatic);
				return;
			}
			double toAmount = amount * toConversionRate;

			string[] currencyNames = { "US Dollars", "Euro", "British Pounds", "Indian Rupees" };
			string fromCurrency = currencyNames[fromSelection];
			string toCurrency = currencyNames[toSelection];

			outputAmountFromTextBlock.Text = $"{amount} {fromCurrency} = ";
			outputAmountToTextBlock.Text = $"{toAmount:C8} {toCurrency}";

			fromConversionRateTextBlock.Text = $"1 {fromCurrency} = {toConversionRate} {toCurrency}";
			toConversionRateTextBlock.Text = $"1 {toCurrency} = {1 / toConversionRate} {fromCurrency}";
		}

		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Frame.Navigate(typeof(MainMenu));
		}
	}
}