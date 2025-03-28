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
        public MainWindow()
        {
            InitializeComponent();
            _customerService = new CustomerServices();
            _childrenService = new ChildrenServices();

            LoadCustomer();
            LoadChild(_childrenService.GetAllForAdmin());
            cbParent.ItemsSource = _customerService.GetAll();
        }

        private void LoadCustomer()
        {
            DGCustomer.ItemsSource = _customerService.GetAll();
        }

        private void LoadChild(List<Child> children)
        {
            DGChildren.ItemsSource = children;
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerAddUpdateForm customerAddUpdateForm = new CustomerAddUpdateForm();
            if(customerAddUpdateForm.ShowDialog() == true)
            {
                try
                {
                    _customerService.Add(customerAddUpdateForm.Customer);
                    LoadCustomer();
                }catch(Exception ex)
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
                    _customerService.Update(customerAddUpdateForm.Customer);
                    LoadCustomer();
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to update");
            }
        }

        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (DGCustomer.SelectedItem is Customer customer)
            {
                if (MessageBox.Show($"Delete {customer.Username}?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _customerService.Delete(customer.Id);
                    LoadCustomer();
                }
            }
            else
            {
                MessageBox.Show("Please select a customer to delete");
            }
        }

        private void txtCustomerSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = txtCustomerSearch.Text;
            DGCustomer.ItemsSource = _customerService.SearchCustomer(search);
        }

        private void btnAddChild_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdateChild_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteChild_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtChildSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SortAll(object sender, RoutedEventArgs e)
        {
            LoadChild(_childrenService.GetAllForAdmin());
        }

        private void cbParent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbParent.SelectedValue != null)
            {
                int selectedParentId = (int)cbParent.SelectedValue;
                LoadChild(_childrenService.GetByCustomerId(selectedParentId));
            }
        }
    }
}