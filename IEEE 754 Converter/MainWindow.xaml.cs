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
        float f;

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

        private void floatTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (floatTxtBox.Text == "")
            {
                bigEndTxtBox.Text = "";
                litEndTxtBox.Text = "";
                return;
            }
            try
            {
                f = (float)Convert.ToDouble(floatTxtBox.Text.Replace(".", ","));
            }
            catch
            {
                invalidLbl.Visibility = Visibility.Visible;
                return;
            }
            invalidLbl.Visibility = Visibility.Hidden;
            uint ui = BitConverter.ToUInt32(BitConverter.GetBytes(f), 0);

            bigEndTxtBox.Text = String.Format("{0:X}", ui);
            litEndTxtBox.Text = String.Format("{0:X}", LittleEndian(ui));
            }

            private void floatTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
            {
                char ch = e.Text[0];

                if ((Char.IsDigit(ch) || ch == '.'))
                {
                    if (ch == '.' && floatTxtBox.Text.Contains('.'))
                        e.Handled = true;
                }
                else
                    e.Handled = true;
            }
        }
    }
