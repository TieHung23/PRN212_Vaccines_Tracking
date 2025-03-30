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
using Models;
using Models.DTOs;
using Services;
using Validate;

namespace WPF
{
    /// <summary>
    /// Interaction logic for VaccinesTrackingWindow.xaml
    /// </summary>
    public partial class VaccinesTrackingWindow : Window
    {
        VaccinesTrackingServices _vaccinesTrackingServices;
        Customer _customer;
        CustomerServices _customerServices;
        ChildrenServices _childrenServices;
        IValidate validate;

        public VaccinesTrackingWindow()
        {
            InitializeComponent();
            _vaccinesTrackingServices = new VaccinesTrackingServices();
            _customerServices = new CustomerServices();
            _childrenServices = new ChildrenServices();
        }

        public VaccinesTrackingWindow( Customer cus )
        {
            InitializeComponent();
            _customer = cus;
            _vaccinesTrackingServices = new VaccinesTrackingServices();
            _customerServices = new CustomerServices();
            _childrenServices = new ChildrenServices();
            btnCancel.IsEnabled = false;
            btnComplete.IsEnabled = false;
            UserDataGrid.IsEnabled = false;
        }

        public void LoadVaccinesTrackingGrid()
        {
            VaccineTrackingGrid.ItemsSource = null;
            VaccineTrackingGrid.ItemsSource = _vaccinesTrackingServices.GetAll();

            if( _customer != null )
            {
                VaccineTrackingGrid.ItemsSource = null;
                VaccineTrackingGrid.ItemsSource = _vaccinesTrackingServices.GetByUserId( _customer.Id );
            }
        }

        public void LoadUserGrid()
        {
            UserDataGrid.ItemsSource = null;
            UserDataGrid.ItemsSource = _customerServices.GetAll();

            if( _customer != null )
            {
                UserDataGrid.ItemsSource = null;
                UserDataGrid.ItemsSource = _customerServices.GetAllByMail( _customer.Email );
            }
        }

        public void LoadChildrenGrid()
        {
            ChildDataGrid.ItemsSource = null;
            ChildDataGrid.ItemsSource = _childrenServices.GetAll();

            if ( _customer != null )
            {
                ChildDataGrid.ItemsSource = null;
                ChildDataGrid.ItemsSource = _childrenServices.GetByCustomerId( _customer.Id );
            }
        }

        private void btnComplete_Click( object sender, RoutedEventArgs e )
        {
            VaccinesTrackingResponse vaccinesTrackingResponse = VaccineTrackingGrid.SelectedItem as VaccinesTrackingResponse;
            if( vaccinesTrackingResponse != null )
            {
                if( vaccinesTrackingResponse.Status.ToLower() == "done" )
                {
                    MessageBox.Show( "This vaccine has been completed!" );
                    return;
                }
                if( vaccinesTrackingResponse.Status.ToLower() == "cancelled" )
                {
                    MessageBox.Show( "This vaccine has been cancelled!" );
                    return;
                }
                if( vaccinesTrackingResponse.Status.ToLower() == "waiting" )
                {
                    MessageBox.Show( "This vaccine is waiting!" );
                    return;
                }

                _vaccinesTrackingServices.MarkAsDone( vaccinesTrackingResponse.VaccinesTrackingId );

                MessageBox.Show( "Mark as done successfully!" );
                LoadVaccinesTrackingGrid();
                return;

            }
            else MessageBox.Show( "Please select a row!" );
        }

        private void btnCancel_Click( object sender, RoutedEventArgs e )
        {
            VaccinesTrackingResponse vaccinesTrackingResponse = VaccineTrackingGrid.SelectedItem as VaccinesTrackingResponse;
            if( vaccinesTrackingResponse != null )
            {
                if( vaccinesTrackingResponse.Status.ToLower() == "done" )
                {
                    MessageBox.Show( "This vaccine has been completed!" );
                    return;
                }
                if( vaccinesTrackingResponse.Status.ToLower() == "cancelled" )
                {
                    MessageBox.Show( "This vaccine has been cancelled!" );
                    return;
                }
                var result = MessageBox.Show( "Are you sure you want to cancel this vaccine it have effect to all your vaccination progress?", "Cancel", MessageBoxButton.YesNo );
                if( result == MessageBoxResult.No )
                    return;

                _vaccinesTrackingServices.Cancel( vaccinesTrackingResponse.VaccinesTrackingId );
                MessageBox.Show( "Cancel all successfully!" );
                LoadVaccinesTrackingGrid();
                return;

            }
            else
                MessageBox.Show( "Please select a row!" );
        }

        private void btnClose_Click( object sender, RoutedEventArgs e )
        {
            this.Close();
        }

        private void btnSearchParent_Click( object sender, RoutedEventArgs e )
        {
            var key = SearchUserBox.Text;
            validate = new Validate.Validate();
            if( !validate.isString(key) )
            {
                btnReset_Click( sender, e );
            }
            else
            {
                UserDataGrid.ItemsSource = null;
                UserDataGrid.ItemsSource = _customerServices.GetAllByMail( key );
            }
            return;
        }

        private void btnSearchChild_Click( object sender, RoutedEventArgs e )
        {
            var key = SearchChildBox.Text;
            validate = new Validate.Validate();
            if( !validate.isString( key ) )
            {
                btnReset_Click( sender, e );
            }
            else
            {
                ChildDataGrid.ItemsSource = null;
                ChildDataGrid.ItemsSource = _childrenServices.GetChildByName( key );
            }
            return;
        }

        private void Window_Loaded( object sender, RoutedEventArgs e )
        {
            LoadChildrenGrid();
            LoadUserGrid();
            LoadVaccinesTrackingGrid();
        }

        private void btnReset_Click( object sender, RoutedEventArgs e )
        {
            LoadChildrenGrid();
            LoadUserGrid();
            LoadVaccinesTrackingGrid();
            SearchChildBox.Text = "";
            SearchUserBox.Text = "";
            return;
        }

        private void UserDataGrid_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            Customer customer = UserDataGrid.SelectedItem as Customer;
            if( customer != null )
            {
                _customer = customer;
                LoadChildrenGrid();
                LoadVaccinesTrackingGrid();
            }
            _customer = null;
            return;
        }

        private void ChildDataGrid_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            Child child = ChildDataGrid.SelectedItem as Child;
            if( child != null )
            {
                VaccineTrackingGrid.ItemsSource = null;
                VaccineTrackingGrid.ItemsSource = _vaccinesTrackingServices.GetByChildId( child.Id );
            }
            return;
        }
    }
}
