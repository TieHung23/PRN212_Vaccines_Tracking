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
using Services;
using Validate;

namespace WPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private const string ADMIN_MAIL = "admin123@gmail.com";
        private const string ADMIN_PASS = "a";
        private readonly IValidate validate;
        private readonly CustomerServices _customerSerivce;

        public LoginWindow()
        {
            InitializeComponent();
            validate = new Validate.Validate();
            _customerSerivce = new CustomerServices();
        }

        private void btnLogin_Click( object sender, RoutedEventArgs e )
        {
            var email = txtEmail.Text;
            var pass = txtPassword.Password;

            if( !validate.isString( email ) || !validate.isString( pass ) )
            {
                MessageBox.Show( "Email and password are required!" );
                return;
            }

            if( !validate.isMatchEmailRegex( email ) )
            {
                MessageBox.Show( "Email is not valid!" );
                return;
            }

            if( email == ADMIN_MAIL && pass == ADMIN_PASS )
            {
                MessageBox.Show( "Login successful!" );
                MainWindow main = new MainWindow();
                this.Close();
                main.Show();
            }
            else
            {
                var customer = _customerSerivce.Login( email, pass );
                if( customer != null )
                {
                    CustomerWindow customerWindow = new CustomerWindow();
                    customerWindow.Show();
                    this.Close();
                }
            }
        }

        private void btnRes_Click( object sender, RoutedEventArgs e )
        {
            RegisterWindow res = new RegisterWindow();
            this.Hide();
            res.ShowDialog();
            this.Show();
        }
    }
}
