using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Models;
using Services;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CustomerServices _customerService;
        private readonly ChildrenServices _childrenService;
        private readonly BookingServices _bookingServices;
        private readonly VaccineServices _vacineServices;
        private readonly VaccineDetailService _vaccineDetailService;
        public MainWindow()
        {
            InitializeComponent();
            _customerService = new CustomerServices();
            _childrenService = new ChildrenServices();
            _bookingServices = new BookingServices();
            _vacineServices = new VaccineServices();
            _vaccineDetailService = new VaccineDetailService();


            LoadCustomer(_customerService.GetAllForAdmin());
            LoadChildren(_childrenService.GetAllForAdmin());
            LoadVaccine(_vacineServices.GetAll());
            LoadVaccineDetail(_vaccineDetailService.GetAll());


            cbParent.ItemsSource = _customerService.GetAllForAdmin();
            LoadBookings();
        }

        private void LoadCustomer(List<Customer> customers)
        {
            DGCustomer.ItemsSource = customers;
        }


        private void LoadChildren(List<Child> children)
        {
            DGChildren.ItemsSource = children;
        }
        private void LoadVaccine(List<Vaccine> vaccine)
        {
            DGVaccine.ItemsSource = vaccine;
        }
        private void LoadVaccineDetail(List<VaccineDetail> vaccineDetail)
        {
            DGVaccineDetail.ItemsSource = vaccineDetail;
        }


        private void LoadBookings()
        {
            var bookings = _bookingServices.GetBookingForAdmin();
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

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerAddUpdateForm customerAddUpdateForm = new CustomerAddUpdateForm();
            if(customerAddUpdateForm.ShowDialog() == true)
            {
                try
                {
                    _customerService.AddCustomer(customerAddUpdateForm.Customer);
                    LoadCustomer(_customerService.GetAllForAdmin());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            if(DGCustomer.SelectedItem is Customer customer)
            {
                CustomerAddUpdateForm customerAddUpdateForm = new CustomerAddUpdateForm(customer);
                if(customerAddUpdateForm.ShowDialog() == true)
                {
                    try
                    {
                        _customerService.UpdateCustomer(customerAddUpdateForm.Customer);
                        LoadCustomer(_customerService.GetAllForAdmin());
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please choose a customer to update");
            }
        }

        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (DGCustomer.SelectedItem is Customer customer)
            {
                if (MessageBox.Show($"Delete {customer.Username}?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _customerService.DeleteCustomer(customer.Id);
                        LoadCustomer(_customerService.GetAllForAdmin());
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

            //if(DGCustomer.SelectedItem is Customer customer)
            //{
            //    if (MessageBox.Show($"Delete {customer.Username}?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //    {
            //        try
            //        {
            //            tabControl.SelectionChanged -= tabControl_SelectionChanged;
            //            _customerService.DeleteCustomer(customer.Id);
            //            LoadCustomer(_customerService.GetAllForAdmin());
            //        }
            //        catch (ArgumentException ex)
            //        {
            //            MessageBox.Show(ex.Message);
            //        }
            //        catch(Exception ex)
            //        {
            //            MessageBox.Show(ex.Message);
            //        }
            //        finally
            //        {
            //            tabControl.SelectionChanged += tabControl_SelectionChanged;
            //        }
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Please select a customer to delete");
            //}

        }

        private void txtCustomerSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = txtCustomerSearch.Text;
            LoadCustomer(_customerService.SearchCustomer(search));
        }

        private void btnAddChild_Click(object sender, RoutedEventArgs e)
        {
            ChildAddUpdateWindow childAddUpdateWindow = new ChildAddUpdateWindow(true);
            if(childAddUpdateWindow.ShowDialog() == true)
            {
                try
                {
                    _childrenService.AddChild(childAddUpdateWindow.Child);
                    LoadChildren(_childrenService.GetAllForAdmin());
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnUpdateChild_Click(object sender, RoutedEventArgs e)
        {
            if(DGChildren.SelectedItem is Child child)
            {
                ChildAddUpdateWindow childAddUpdateWindow = new ChildAddUpdateWindow(true,null,child);
                if(childAddUpdateWindow.ShowDialog() == true)
                {
                    try
                    {
                        _childrenService.UpdateChild(childAddUpdateWindow.Child);
                        LoadChildren(_childrenService.GetAllForAdmin());
                    }
                    catch(ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    catch(Exception ex)
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
                        _childrenService.DeleteChild(child.Id);
                        LoadChildren(_childrenService.GetAllForAdmin());
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

            //if (DGChildren.SelectedItem is Child child)
            //{
            //    if (MessageBox.Show($"Delete {child.Name}?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //    {
            //        try
            //        {
            //            tabControl.SelectionChanged -= tabControl_SelectionChanged;
            //            _childrenService.DeleteChild(child.Id);
            //            LoadChildren(_childrenService.GetAllForAdmin());
            //        }
            //        catch (ArgumentException ex)
            //        {
            //            MessageBox.Show(ex.Message);
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message);
            //        }
            //        finally
            //        {
            //            tabControl.SelectionChanged += tabControl_SelectionChanged;
            //        }
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Please select a child to delete");
            //}
        }

        private void SortAll(object sender, RoutedEventArgs e)
        {
            LoadChildren(_childrenService.GetAll());
        }

        private void cbParent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var customerId = (int)cbParent.SelectedValue;
            var children = _childrenService.GetByCustomerId(customerId);
            LoadChildren(children);
        }

        private void txtChildSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = txtChildSearch.Text;
            LoadChildren(_childrenService.SearchChild(search));
        }

        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            BookingWindow bookingWindow = new BookingWindow();
            bookingWindow.Show();
            this.Close();
        }

        private void AddVaccineButton_Click(object sender, RoutedEventArgs e)
        {
            VaccineAddUpdateWindow vaccineAddUpdateWindow = new VaccineAddUpdateWindow();
            if (vaccineAddUpdateWindow.ShowDialog() == true)
            {
                try
                {
                    _vacineServices.AddVaccine(vaccineAddUpdateWindow.vaccine);
                    LoadVaccine(_vacineServices.GetAll());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DGVaccine.SelectedItem is Customer customer)
            {
                CustomerAddUpdateForm customerAddUpdateForm = new CustomerAddUpdateForm(customer);
                if (customerAddUpdateForm.ShowDialog() == true)
                {
                    try
                    {
                        _customerService.UpdateCustomer(customerAddUpdateForm.Customer);
                        LoadCustomer(_customerService.GetAllForAdmin());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please choose a customer to update");
            }
        }

        private void UpdateVaccineButton_Click(object sender, RoutedEventArgs e)
        {
            if (DGVaccine.SelectedItem is Vaccine vaccine)
            {
                VaccineAddUpdateWindow vaccineAddUpdate = new(vaccine);
                if (vaccineAddUpdate.ShowDialog() == true)
                {
                    try
                    {
                        _vacineServices.UpdateVaccine(vaccineAddUpdate.vaccine);
                        LoadVaccine(_vacineServices.GetAll());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please choose a vaccine to update");
            }
        }

        private void DeleteVccineButton_Click(object sender, RoutedEventArgs e)
        {
            var vaccine = DGVaccine.SelectedItem as Vaccine;
            if (vaccine != null)
            {
                if (MessageBox.Show($"Delete {vaccine.Name}?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _vacineServices.DeleteVaccine(vaccine);
                        LoadVaccine(_vacineServices.GetAll());
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
                MessageBox.Show("Please select a vaccine to delete");
            }
        }

        private void txtVaccineSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = txtVaccineSearch.Text;
            LoadVaccine(_vacineServices.SearchVaccine(search)); 
        }

        private void txtVaccineDetaiSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = txtVaccineDetailSearch.Text;
            LoadVaccineDetail(_vaccineDetailService.SearchVaccineDetail(search));
        }

        private void AddVaccineDetailButton_Click(object sender, RoutedEventArgs e)
        {
            VaccineDetailAddUpdateForm vaccineDetailAddUpdateForm = new VaccineDetailAddUpdateForm();
            if (vaccineDetailAddUpdateForm.ShowDialog() == true)
            {
                try
                {
                    _vaccineDetailService.AddCVaccineDetail(vaccineDetailAddUpdateForm.vaccineDetail);
                    LoadVaccineDetail(_vaccineDetailService.GetAll());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void UpdateVaccineDetailButton_Click(object sender, RoutedEventArgs e)
        {
            if (DGVaccineDetail.SelectedItem is VaccineDetail vaccineDetail)
            {
                VaccineDetailAddUpdateForm vaccineDetailAddUpdate = new(vaccineDetail);
                if (vaccineDetailAddUpdate.ShowDialog() == true)
                {
                    try
                    {
                        _vaccineDetailService.UpdateVaccineDetail(vaccineDetail);
                        LoadVaccineDetail(_vaccineDetailService.GetAll());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please choose a vaccine to update");
            }
        }

        private void DeleteVccineDetailButton_Click(object sender, RoutedEventArgs e)
        {
            var vaccineDetail = DGVaccineDetail.SelectedItem as VaccineDetail;
            if (vaccineDetail != null)
            {
                if (MessageBox.Show($"Delete {vaccineDetail.VaccineDetailsId}?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _vaccineDetailService.DeleteVaccineDetail(vaccineDetail);
                        LoadVaccineDetail(_vaccineDetailService.GetAll());
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
                MessageBox.Show("Please select a vaccine to delete");
            }
        }


        //private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (tabCustomer.IsSelected)
        //    {
        //        LoadCustomer(_customerService.GetAllForAdmin());
        //    }

        //    if (tabChild.IsSelected)
        //    {
        //        LoadChildren(_childrenService.GetAllForAdmin());
        //    }
        //}
    }
}