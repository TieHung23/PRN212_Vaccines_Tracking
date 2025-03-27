using System.Data.SqlTypes;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Models;
using Services;
using Validate;

namespace WPF
{
    /// <summary>
    /// Interaction logic for BookingWindow.xaml
    /// </summary>
    public partial class BookingWindow : Window
    {
        CustomerServices cusServices;
        VaccineServices vacServices;
        ChildrenServices childrenServices;
        BookingServices bookingServices;
        IValidate validate;
        Customer _cus = null;
        private List<Child> _children = new List<Child>();
        private List<Vaccine> _vaccines = new List<Vaccine>();
        public BookingWindow()
        {
            InitializeComponent();
        }
        public BookingWindow( Customer cus )
        {
            InitializeComponent();
            _cus = cus;

            btnSearchUser.IsEnabled = false;
            txtUserSearch.IsEnabled = false;
            btnResetUser.IsEnabled = false;
            dgUsers.IsEnabled = false;


        }
        public void LoadCustomer()
        {
            dgUsers.ItemsSource = null;
            if( _cus == null )
            {
                cusServices = new CustomerServices();
                dgUsers.ItemsSource = cusServices.GetAll();
            }
            else
            {
                dgUsers.ItemsSource = ( System.Collections.IEnumerable? ) _cus;
                ParentNameTextBlock.Text = $"   • {_cus.Username}";
            }
        }
        public void LoadVaccine()
        {
            dgVaccines.ItemsSource = null;
            vacServices = new VaccineServices();
            dgVaccines.ItemsSource = vacServices.GetAll();
        }
        public void LoadChildren()
        {
            dgChildren.ItemsSource = null;
            childrenServices = new ChildrenServices();
            if( _cus != null )
            {
                dgChildren.ItemsSource = childrenServices.GetByCustomerId( _cus.Id );
            }
            else
                dgChildren.ItemsSource = childrenServices.GetAll();
        }

        public void Calculate( List<Child> childs, List<Vaccine> vaccines )
        {
            if( childs.Count == 0 || vaccines.Count == 0 )
            {
                txtTotalPrice.Text = "$0.00";
                return;
            }
            SqlMoney total = vaccines.Sum( x => x.Price ) * childs.Count;
            txtTotalPrice.Text = "$" + total.ToString();
        }

        private void btnSearchUser_Click( object sender, RoutedEventArgs e )
        {
            var search = txtUserSearch.Text;
            validate = new Validate.Validate();
            if( validate.isString( search ) )
            {
                cusServices = new CustomerServices();
                dgUsers.ItemsSource = cusServices.GetAllByMail( search );
            }
            else
            {
                MessageBox.Show( "Please enter a valid value" );
            }
        }

        private void btnResetUser_Click( object sender, RoutedEventArgs e )
        {
            _cus = null!;
            txtUserSearch.Text = string.Empty;
            LoadCustomer();
            ParentNameTextBlock.Text = string.Empty;
        }

        private void btnSearchChild_Click( object sender, RoutedEventArgs e )
        {
            var result = dgChildren.ItemsSource as List<Child>;

            if( result != null )
            {
                var search = txtChildSearch.Text;
                validate = new Validate.Validate();
                if( validate.isString( search ) )
                {
                    dgChildren.ItemsSource = result.Where( x => x.Name.Contains( search ) ).ToList();
                }
                else
                {
                    MessageBox.Show( "Please enter a valid value" );
                }
            }
        }

        private void btnResetChild_Click( object sender, RoutedEventArgs e )
        {
            txtChildSearch.Text = string.Empty;
            LoadChildren();
            _children.Clear();
            Calculate( _children, _vaccines );
        }

        private void btnSearchVaccine_Click( object sender, RoutedEventArgs e )
        {
            var search = txtVaccineSearch.Text;
            validate = new Validate.Validate();
            if( validate.isString( search ) )
            {
                vacServices = new VaccineServices();
                dgVaccines.ItemsSource = vacServices.GetByName( search );
            }
            else
            {
                MessageBox.Show( "Please enter a valid value" );
            }
        }

        private void btnResetVaccine_Click( object sender, RoutedEventArgs e )
        {
            txtVaccineSearch.Text = string.Empty;
            LoadVaccine();
            _vaccines.Clear();
            Calculate( _children, _vaccines );
        }

        private void btnBooking_Click( object sender, RoutedEventArgs e )
        {
            var DateVaccination = dpBookingDate.SelectedDate;
            validate = new Validate.Validate();

            if( !validate.isString( dpBookingDate.SelectedDate.ToString() ) )
            {
                MessageBox.Show( "Please enter a valid date" );
                return;
            }

            if( _cus == null )
            {
                MessageBox.Show( "Please select a parent" );
                return;
            }

            if( _children.Count == 0 )
            {
                MessageBox.Show( "Please select a child" );
                return;
            }

            if( _vaccines.Count == 0 )
            {
                MessageBox.Show( "Please select a vaccine" );
                return;
            }

            if( !validate.isDecimal( txtTotalPrice.Text.Replace( "$", "" ) ) )
            {
                MessageBox.Show( "Please enter a valid price" );
                return;
            }

            bookingServices = new BookingServices();
            Booking booking = new Booking
            {
                ParentId = _cus.Id,
                Children = _children,
                Vaccines = _vaccines,
                CreatedAt = DateTime.Now,
                Status = 1,
                FinalPrice = decimal.Parse( txtTotalPrice.Text, NumberStyles.Currency, CultureInfo.GetCultureInfo( "en-US" ) ),
            };

            bookingServices.AddBooking( booking, ( DateTime )  DateVaccination  );

            MessageBox.Show( "Booking added successfully" );
            return;
        }

        private void btnClose_Click( object sender, RoutedEventArgs e )
        {
            this.Close();
        }

        private void dgUsers_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if( dgUsers.SelectedItem != null )
            {
                _cus = ( Customer ) dgUsers.SelectedItem;
                LoadChildren();

                ParentNameTextBlock.Text = $"   • {_cus.Username}";

                dgChildren.IsEnabled = true;
            }
        }

        private void dgChildren_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if( dgChildren.SelectedItem != null )
            {
                ChildStackPanel.Children.Clear();
                _children.Clear();
                foreach( var item in dgChildren.SelectedItems )
                {
                    if( item is Child selectedChild ) // Ensure item is a valid Child
                    {
                        _children.Add( ( Child ) item );
                        TextBlock childText = new TextBlock
                        {
                            Text = $"• {selectedChild.Name}",
                            FontSize = 14,
                            Margin = new Thickness( 10, 0, 0, 2 )
                        };
                        Calculate( _children, _vaccines );
                        ChildStackPanel.Children.Add( childText );
                    }
                }
            }

        }

        private void dgVaccines_SelectionChanged( object sender, SelectionChangedEventArgs e )
        {
            if( dgVaccines.SelectedItem != null )
            {
                VaccineStackPanel.Children.Clear();
                _vaccines.Clear();
                foreach( var item in dgVaccines.SelectedItems )
                {
                    if( item is Vaccine selectedVaccine ) // Ensure item is a valid Vaccine
                    {
                        _vaccines.Add( ( Vaccine ) item );
                        TextBlock vaccineText = new TextBlock
                        {
                            Text = $"• {selectedVaccine.Name}",
                            FontSize = 14,
                            Margin = new Thickness( 10, 0, 0, 2 )
                        };
                        Calculate( _children, _vaccines );
                        VaccineStackPanel.Children.Add( vaccineText );
                    }
                }
            }
        }

        private void Window_Loaded( object sender, RoutedEventArgs e )
        {
            LoadVaccine();
            LoadCustomer();
            LoadChildren();
            dgChildren.IsEnabled = false;
            dpBookingDate.SelectedDate = DateTime.Now;
        }

        private void dpBookingDate_SelectedDateChanged( object sender, SelectionChangedEventArgs e )
        {
            if( dpBookingDate.SelectedDate < DateTime.Today )
            {
                MessageBox.Show( "You cannot select a past date.", "Invalid Date", MessageBoxButton.OK, MessageBoxImage.Warning );
                dpBookingDate.SelectedDate = DateTime.Today;
            }
        }

    }
}
