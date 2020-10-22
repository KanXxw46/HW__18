using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace HW18
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public object MainFrame { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
        }
        private void InputClick(object sender, RoutedEventArgs e)
        {
            var user = new User();
            try
            {
                string connStr = "server=TheFarmersMarketApp;user=User;database=Autorisation;password=Pass;SSL Mode=mod;";
                MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();

                SqlCommand com = new SqlCommand("SELECT * FROM accounts WHERE login=@login and password=@password");
                com.Parameters.AddWithValue("@login", login.Text);
                com.Parameters.AddWithValue("@password", password);
                SqlDataReader Dr = com.ExecuteReader();
                if (Dr.HasRows == true)
                {
                    conn.Close();
                }
                else
                {
                    MessageBox.Show("Такого логина или пароля не существует: \n\n1) Проверьте правильность ввода\n\n2) Обратитесь к администартору", "Ошибка авторизации");

                }
                user.Login = login.Text;
                user.Password = password.Password;
            }
            catch (ArgumentException exception)
            {
                error.Content = exception.Message;
                error.Opacity = 100;
                return;
            }
            //Check from database existance and is password correct and fill with data user

            if (user.Role == "Buyer")
            {
                MainFrame.Navigate(new BuyerMainPage(MainFrame, user));
            }
            else if (user.Role == "Provider")
            {
                MainFrame.Navigate(new ProviderMainPage(MainFrame, user));
            }
        }


        private void ForgotYourPassword(object sender, RoutedEventArgs e)
        {
            //TODO
            //switch to email
        }

    }
}
