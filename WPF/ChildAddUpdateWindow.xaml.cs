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
using Services;
using Validate;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WPF
{
    /// <summary>
    /// Interaction logic for ChildAddUpdateWindow.xaml
    /// </summary>
    public partial class ChildAddUpdateWindow : Window
    {
        private readonly CustomerServices _customerServices;
        private readonly ChildrenServices _childrenServices;
        private Child _child;
        private bool _isNew;
        private bool _isAdmin;
        private int? _userId;
        private readonly IValidate _validate;
        public Child Child { get { return _child; } }
        public ChildAddUpdateWindow(bool isAdmin, int? userid = null, Child child = null)
        {
            InitializeComponent();
            
            _customerServices = new CustomerServices();
            _childrenServices = new ChildrenServices();
            _validate = new Validate.Validate();
            _child = child ?? new Child();
            _isAdmin = isAdmin;
            _isNew = child == null;
            _userId = userid;
            //update admin
            //if (isAdmin && child != null)
            //{
            //    cbParent.IsEnabled = false;
            //}

            ////add admin
            //if (isAdmin && child == null)
            //{
            //    cbParent.IsEnabled = true;
            //    cbParent.ItemsSource = _customerServices.GetAllForAdmin();
            //}
            //if (!isAdmin && _userId.HasValue)
            //{
            //    cbParent.IsEnabled = false;
            //    //cbParent.IsEnabled = false;
            //    var customer = _customerServices.GetById(_userId.Value); // Lấy trực tiếp từ database
            //    if (customer != null)
            //    {
            //        cbParent.ItemsSource = new List<Customer> { customer };
            //        cbParent.SelectedIndex = 0; // Chọn mặc định customer này
            //    }
            //}

            //cbGender.ItemsSource = new string[] { "F", "M", "O" };
            //cbStatus.ItemsSource = new int[] { 0, 1 };
            //if (!_isNew)
            //{
            //    //cbParent.SelectedItem = _child.ParentId;
            //    cbParent.SelectedItem = cbParent.Items.Cast<Customer>()
            //                          .FirstOrDefault(c => c.Id == _child.ParentId);
            //    txtName.Text = _child.Name;
            //    dpBirthday.SelectedDate = _child.DateOfBirth;
            //    cbGender.SelectedItem = _child.Gender;
            //    cbStatus.SelectedItem = _child.Status;
            //}

            SetupUI();
        }

        private void SetupUI()
        {
            cbGender.ItemsSource = new string[] { "F", "M", "O" };
            cbStatus.ItemsSource = new int[] { 0, 1 };

            if (_isAdmin)
            {
                // Admin có quyền chọn parent khi thêm mới
                if (_isNew)
                {
                    cbParent.IsEnabled = true;
                    cbParent.ItemsSource = _customerServices.GetAllForAdmin();
                }
                else
                {
                    // Khi update, Admin không thể đổi parent
                    cbParent.IsEnabled = false;
                    cbParent.ItemsSource = _customerServices.GetAllForAdmin();
                    cbParent.SelectedValue = _child.ParentId;
                }
            }
            else if (_userId.HasValue)
            {
                // Customer chỉ có thể xem parent của họ
                cbParent.IsEnabled = false;
                var customer = _customerServices.GetById(_userId.Value);
                if (customer != null)
                {
                    cbParent.ItemsSource = new List<Customer> { customer };
                    cbParent.SelectedValue = customer.Id;
                }
            }

            if (!_isNew)
            {
                // Gán dữ liệu khi update
                txtName.Text = _child.Name;
                dpBirthday.SelectedDate = _child.DateOfBirth;
                cbGender.SelectedItem = _child.Gender;
                cbStatus.SelectedItem = _child.Status;
            }
        }



        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int parentId = (int)cbParent.SelectedValue;
            string name = txtName.Text;
            DateTime birthday = dpBirthday.SelectedDate.Value;
            string gender = (string)cbGender.SelectedItem;
            int status = (int)cbStatus.SelectedItem;
            if (string.IsNullOrWhiteSpace(parentId.ToString()) ||
                string.IsNullOrWhiteSpace(name.ToString()) ||
                string.IsNullOrWhiteSpace(birthday.ToString()) ||
                string.IsNullOrWhiteSpace(gender.ToString()) ||
                string.IsNullOrWhiteSpace(status.ToString()))
            {
                MessageBox.Show("Please fill all the blank");
                return;
            }

            _child.ParentId = parentId;
            _child.Name = name;
            _child.DateOfBirth = birthday;
            _child.Gender = gender;
            _child.Status = status;
            if (_isNew)
            {
                MessageBox.Show("New child is added successfully");
            }
            else
            {
                MessageBox.Show("Child is updated successfully");
            }

            DialogResult = true;
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
