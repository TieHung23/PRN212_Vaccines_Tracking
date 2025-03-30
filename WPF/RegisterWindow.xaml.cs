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
        private readonly IValidate _validate;
        private readonly CustomerServices _customerServices;
        public RegisterWindow()
        {
            InitializeComponent();
            _validate = new Validate.Validate();
            _customerServices = new CustomerServices();
        }

        private void btnRes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = txtUsername.Text;
                string password = txtPassword.Password;
                if (!dpDOB.SelectedDate.HasValue)
                {
                    MessageBox.Show("Vui lòng chọn ngày sinh");
                    return;
                }
                DateTime DOB = dpDOB.SelectedDate.Value;
                string email = txtEmail.Text;
                string phone = txtPhone.Text;
                //if (cbStatus.SelectedItem == null)
                //{
                //    MessageBox.Show("Vui lòng chọn trạng thái");
                //    return;
                //}
                //int status = (int)cbStatus.SelectedItem;
                if (!_validate.isString(username) ||
                   !_validate.isString(password) ||
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

                var existEmail = _customerServices.GetAllForAdmin().FirstOrDefault(x => x.Email == txtEmail.Text);
                if (existEmail != null)
                {
                    MessageBox.Show("Email is existed. Please choose another");
                    return;
                }

                var existPhone = _customerServices.GetAllForAdmin().FirstOrDefault(x => x.Phone == txtPhone.Text);
                if (existPhone != null)
                {
                    MessageBox.Show("Phone is existed. Please choose another");
                    return;
                }

                Customer _customer = new Customer();
                _customer.Username = username;
                _customer.Password = password;
                _customer.DateOfBirth = DOB;
                _customer.Email = email;
                _customer.Phone = phone;
                _customer.CreatedAt = DateTime.Now;
                _customer.Status = 1;

                _customerServices.AddCustomer(_customer);
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
