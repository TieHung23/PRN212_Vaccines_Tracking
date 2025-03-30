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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WPF
{
    /// <summary>
    /// Interaction logic for CustomerAddUpdateForm.xaml
    /// </summary>
    public partial class CustomerAddUpdateForm : Window
    {
        private readonly CustomerServices _customerServices;
        private Customer _customer;
        private bool _isNew;
        private readonly IValidate _validate;
        public Customer Customer { get { return _customer; } }
        public CustomerAddUpdateForm(Customer customer = null)
        {
            InitializeComponent();
            _customerServices = new CustomerServices();
            _validate = new Validate.Validate();
            _customer = customer ?? new Customer();
            _isNew = customer == null;


            cbStatus.ItemsSource = new int[] { 0, 1 };
            if (!_isNew)
            {
                txtUsername.Text = _customer.Username;
                txtPassword.Password = _customer.Password;
                dpBirthday.SelectedDate = _customer.DateOfBirth;
                txtEmail.Text = _customer.Email;
                txtTelephone.Text = _customer.Phone;
                cbStatus.SelectedItem = _customer.Status;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = txtUsername.Text;
                string password = txtPassword.Password;
                if (!dpBirthday.SelectedDate.HasValue)
                {
                    MessageBox.Show("Vui lòng chọn ngày sinh");
                    return;
                }
                DateTime DOB = dpBirthday.SelectedDate.Value;
                string email = txtEmail.Text;
                string phone = txtTelephone.Text;
                if (cbStatus.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn trạng thái");
                    return;
                }
                int status = (int)cbStatus.SelectedItem;
                if (!_validate.isString(username) ||
                   !_validate.isString(password) ||
                   !_validate.isString(DOB.ToString()) ||
                   !_validate.isString(email) ||
                   !_validate.isString(phone))
                {
                    MessageBox.Show("Please fill all the blank");
                    return;
                }

                if (!Regex.IsMatch(txtTelephone.Text, @"^(0|\+84)(3[2-9]|5[6|8|9]|7[06-9]|8[1-9]|9[0-9])[0-9]{7}$"))
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
                if(existEmail != null)
                {
                    MessageBox.Show("Email is existed. Please choose another");
                    return;
                }

                var existPhone = _customerServices.GetAllForAdmin().FirstOrDefault(x => x.Phone == txtTelephone.Text);
                if (existPhone != null)
                {
                    MessageBox.Show("Phone is existed. Please choose another");
                    return;
                }
                _customer.Username = username;
                _customer.Password = password;
                _customer.DateOfBirth = DOB;
                _customer.Email = email;
                _customer.Phone = phone;
                _customer.Status = status;
                if (_isNew)
                {
                    _customer.CreatedAt = DateTime.Now;
                    MessageBox.Show("New customer is added successfully");
                }
                else
                {
                    _customer.CreatedAt = _customer.CreatedAt;
                    MessageBox.Show("Customer is updated successfully");
                }

                DialogResult = true;
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
