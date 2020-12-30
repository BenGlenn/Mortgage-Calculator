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

namespace MorgageCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Variables 
        static public double amountBorrowed { get; set; }
        static public double interestRate { get; set; }
        static public int mortgagePeriod { get; set; }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Get & Parse values
            amountBorrowed = (double)Int32.Parse(lbl_Amount.Text);

            // Get Interest rate
            decimal result;
            if (Decimal.TryParse(lbl_Interest.Text, out result))
                interestRate = (double)result;

            // get mortgage period

            mortgagePeriod = Int32.Parse(lbl_Period.Text);

            // Calculate mortage

            lbl_Payments.Text =
                CalcMortage(amountBorrowed, interestRate, mortgagePeriod);
        }

        private string CalcMortage(double lbl_Amount, double lbl_Interest, int lbl_Period)
        {
            double p = lbl_Amount;
            double r = ConvertToMonthlyInterest(lbl_Interest);
            double n = YearsToMonths(lbl_Period);

            var c = (decimal)(((r * p) * Math.Pow((1 + r), n)) / (Math.Pow((1 + r), n) - 1));

            return ($"${Math.Round(c, MidpointRounding.AwayFromZero)}");
        }

        private int YearsToMonths(int years)
        {
            return (12 * years);
        }

        private double ConvertToMonthlyInterest(double percent)
        {
            return (percent / 12) / 100;
        }
    }
}
