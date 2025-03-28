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
using Models;
using Services;
using Validate;

namespace WPF
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private readonly CustomerServices _customerService;
        private readonly IValidate _validate;
        public RegisterWindow()
        {
            InitializeComponent();
            _customerService = new CustomerServices();
            _validate = new Validate.Validate();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            DateTime DOB = dpDateOfBirth.SelectedDate.Value;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            
            if(!_validate.isString(username) || 
               !_validate.isString(password)  || 
               !_validate.isString(DOB.ToString()) ||
               !_validate.isString(email) ||
               !_validate.isString(phone))
            {
                MessageBox.Show("Please fill all the blank");
                return;
            }

            if (!Regex.IsMatch(txtPhone.Text, @"^(0|\+84)(3[2-9]|5[6|8|9]|7[06-9]|8[1-9]|9[0-9])[0-9]{7}$"))
            {
                MessageBox.Show("Wrong format phone number");
                return;
            }

            if (!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                MessageBox.Show("Wrong format email");
                return;
            }

            Customer customer = new Customer
            {
                Username = username,
                Password = password,
                DateOfBirth = DOB.ToString(),
                Email = email,
                Phone = phone,
                CreatedAt = DateTime.Now,
                Status = 1,
            };

            try
            {
                _customerService.Add(customer);
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
