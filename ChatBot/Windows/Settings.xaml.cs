using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChatBot.Windows
{
    /// <summary>
    /// Lógica de interacción para Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {

        public Settings()
        {
            InitializeComponent();
            PropertyInfo[] colorItemsSource = typeof(Colors).GetProperties();

            BackgroundColorComboBox.ItemsSource = colorItemsSource;
            UserColorComboBox.ItemsSource = colorItemsSource;
            BotColorComboBox.ItemsSource = colorItemsSource;
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            string s;

            s = BackgroundColorComboBox.SelectedItem.ToString().Split(' ').Last();
            Properties.Settings.Default.BackgroundColor = s;

            s = UserColorComboBox.SelectedItem.ToString().Split(' ').Last();
            Properties.Settings.Default.UserMessagesColor = s;

            s = BotColorComboBox.SelectedItem.ToString().Split(' ').Last();
            Properties.Settings.Default.BotMessagesColor = s;

            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
