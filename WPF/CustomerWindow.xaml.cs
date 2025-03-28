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

namespace WPF
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public Customer Customer { get; set; }
        private readonly CustomerServices _customerServices;
        private readonly ChildrenServices _childrenServices;
        private readonly BookingServices _bookingServices;
        public CustomerWindow(Customer customer)
        {
            InitializeComponent();
            _customerServices = new CustomerServices();
            _childrenServices = new ChildrenServices();
            _bookingServices = new BookingServices();
            Customer = customer;
            DataContext = this;
            LoadProfile();
            LoadChildren();
            LoadBooking();
        }

        private void LoadProfile()
        {
            //txtId.Text = customer.Id.ToString();
            txtUsername.Text = Customer.Username;
            //txtPassword.Password = customer.Password;
            dpDateOfBirth.SelectedDate = Customer.DateOfBirth;
            txtEmail.Text = Customer.Email;
            txtPhone.Text = Customer.Phone;
            txtCreatedAt.Text = Customer.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
            //txtStatus.Text = Customer.Status == 1 ? "Active" : "Inactive";
        }

        private void LoadChildren()
        {
            DGChildren.ItemsSource = _childrenServices.GetByCustomerId(Customer.Id);
        }

        private void LoadBooking()
        {
            var bookings = _bookingServices.GetBookingByParent(Customer.Id);

            var displayBookings = bookings.Select(b => new
            {
                Id = b.Id,
                ParentName = b.Parent?.Username,
                VaccineNames = b.Vaccines != null
                                    ? string.Join(", ", b.Vaccines.Select(v => v.Name))
                                    : string.Empty,
                ChildrenName = b.Children != null
                                    ? string.Join(", ", b.Children.Select(c => c.Name))
                                    : string.Empty,
                FinalPrice = b.FinalPrice,
                CreatedAt = b.CreatedAt,
                Status = b.Status,
            });

            DGBooking.ItemsSource = displayBookings;
        }

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            txtUsername.IsReadOnly = false;
            dpDateOfBirth.IsEnabled = true;
            txtEmail.IsReadOnly = false;
            txtPhone.IsReadOnly = false;
            btnEditProfile.Visibility = Visibility.Hidden;
            btnSaveProfile.Visibility = Visibility.Visible;
        }

        private void SaveProfile_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            if (!dpDateOfBirth.SelectedDate.HasValue)
            {
                MessageBox.Show("Vui lòng chọn ngày sinh");
                return;
            }
            DateTime DOB = dpDateOfBirth.SelectedDate.Value;
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
            Customer.Username = username;
            Customer.Email = email;
            Customer.Phone = phone;
            Customer.DateOfBirth = DOB;
            _customerServices.UpdateCustomer(Customer);
            LoadProfile();

            txtUsername.IsReadOnly = true;
            dpDateOfBirth.IsEnabled = false;
            txtEmail.IsReadOnly = true;
            txtPhone.IsReadOnly = true;
            btnEditProfile.Visibility = Visibility.Visible;
            btnSaveProfile.Visibility = Visibility.Hidden;
        }

        private void btnAddChild_Click(object sender, RoutedEventArgs e)
        {
            ChildAddUpdateWindow childAddUpdateWindow = new ChildAddUpdateWindow(false, Customer.Id);
            if(childAddUpdateWindow.ShowDialog() == true)
            {
                try
                {
                    _childrenServices.AddChild(childAddUpdateWindow.Child);
                    LoadChildren();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnUpdateChild_Click(object sender, RoutedEventArgs e)
        {
            if (DGChildren.SelectedItem is Child child)
            {
                ChildAddUpdateWindow childAddUpdateWindow = new ChildAddUpdateWindow(false, Customer.Id, child);
                if(childAddUpdateWindow.ShowDialog() == true)
                {
                    try
                    {
                        _childrenServices.UpdateChild(childAddUpdateWindow.Child);
                        LoadChildren();
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please choose one to update");
            }
        }

        private void btnDeleteChild_Click(object sender, RoutedEventArgs e)
        {
            if (DGChildren.SelectedItem is Child child)
            {
                if (MessageBox.Show($"Delete {child.Name}?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _childrenServices.DeleteChild(child.Id);
                        LoadChildren();
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to delete");
            }
        }

        

        private void txtChildSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = txtChildSearch.Text;
            _childrenServices.SearchChild(search);
            DGChildren.ItemsSource = _childrenServices.SearchChild(search).Where(x => x.ParentId == Customer.Id);
        }

        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            BookingWindow bookingWindow = new BookingWindow(Customer);
            bookingWindow.Show();
            this.Close();
        }
    }
}
