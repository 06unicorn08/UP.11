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
using System.Windows.Shapes;

namespace WPF_autoparking.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUser.Text;
            string password = txtPass.Password;

            var account = AutoParkEntities.GetContext().Accounts.FirstOrDefault(a => a.username == username && a.password == password);

            if (account != null)
            {
                // Авторизация прошла успешно, открываем новое окно
                MainView otherWindow = new MainView();
                otherWindow.Show();
                this.Close();
            }
            else
            {
                // Неверные учетные данные
                MessageBox.Show("Неверное имя пользователя или пароль");
            }
        }
    }
}
