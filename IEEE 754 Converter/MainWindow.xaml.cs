using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace IEEE_754_Converter
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

        static string LittleEndian(uint num)
        {
            byte[] bytes = BitConverter.GetBytes(num);
            string retval = "";
            foreach (byte b in bytes)
                retval += b.ToString("X2");
            return retval;
        }

        static string LittleEndian(ulong num)
        {
            byte[] bytes = BitConverter.GetBytes(num);
            string retval = "";
            foreach (byte b in bytes)
                retval += b.ToString("X2");
            return retval;
        }

        private void floatTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (floatTxtBox.Text == "")
            {
                bigEndTxtBox.Text = "";
                litEndTxtBox.Text = "";
                bigEndTxtBox64.Text = "";
                litEndTxtBox64.Text = "";
                return;
            }
            try
            {
                float f = (float)Convert.ToDouble(floatTxtBox.Text.Replace(".", ","));
                double d = (float)Convert.ToDouble(floatTxtBox.Text.Replace(".", ","));

                uint hex32 = BitConverter.ToUInt32(BitConverter.GetBytes(f), 0);

                bigEndTxtBox.Text = String.Format("{0:X}", hex32);
                litEndTxtBox.Text = String.Format("{0:X}", LittleEndian(hex32));

                ulong hex64 = BitConverter.ToUInt64(BitConverter.GetBytes(d), 0);

                bigEndTxtBox64.Text = String.Format("{0:X}", hex64);
                litEndTxtBox64.Text = String.Format("{0:X}", LittleEndian(hex64));
            }
            catch
            {
                bigEndTxtBox.Text = "Invalid decimal";
                litEndTxtBox.Text = "Invalid decimal";
                bigEndTxtBox64.Text = "Invalid decimal";
                litEndTxtBox64.Text = "Invalid decimal";
                return;
            }
        }

        private void floatTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char ch = e.Text[0];

            if (Char.IsDigit(ch) || ch == '.' || ch == ',')
            {
                if (ch == '.' && floatTxtBox.Text.Contains('.'))
                    e.Handled = true;
                if (ch == ',' && floatTxtBox.Text.Contains(','))
                    e.Handled = true;
            }
            else
                e.Handled = true;
        }
    }
}
