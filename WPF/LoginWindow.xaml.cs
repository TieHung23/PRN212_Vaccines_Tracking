using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private readonly CustomerServices _customerServices;

        public LoginWindow()
        {
            InitializeComponent();
            validate = new Validate.Validate();
            _customerServices = new CustomerServices();
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

            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                MessageBox.Show( "Email is not valid!" );
                return;
            }

            if( email == ADMIN_MAIL && pass == ADMIN_PASS )
            {
                MessageBox.Show( "Login successful!" );
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                var customer = _customerServices.Login(email, pass);
                if( customer != null )
                {
                    CustomerWindow customerWindow = new CustomerWindow(customer);
                    customerWindow.Customer = customer;
                    customerWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid email or phone");
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
