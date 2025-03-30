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

namespace WPF
{
    /// <summary>
    /// Interaction logic for VaccineDetailAddUpdateForm.xaml
    /// </summary>
    public partial class VaccineDetailAddUpdateForm : Window
    {
        private VaccineDetail _vaccineDetail;
        private readonly IValidate _validate;
        private bool _isNew;
        private VaccineServices _services = new();
        public VaccineDetail vaccineDetail { get { return _vaccineDetail; } }

        public VaccineDetailAddUpdateForm(VaccineDetail vaccineDetail = null)
        {
            InitializeComponent();
            _validate = new Validate.Validate();

            _vaccineDetail = vaccineDetail ?? new VaccineDetail();
            _isNew = vaccineDetail == null;

            txtVaccine.ItemsSource = _services.GetAll();
            txtVaccine.DisplayMemberPath = "Name";
            txtVaccine.SelectedValuePath = "Id";

            txtStatus.ItemsSource = new int[] { 0, 1 };

            if (!_isNew)
            {
                txtEntryDate.SelectedDate = _vaccineDetail.EntryDate;
                txtQuantity.Text = _vaccineDetail.Quantity.ToString();
                txtStatus.SelectedValue = _vaccineDetail.Status;
                txtVaccine.SelectedValue = _vaccineDetail.VaccineId;
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string Quantity = txtQuantity.Text;
                string Status = txtStatus.SelectedValue.ToString();
                string Vaccine = txtVaccine.SelectedValue.ToString();
                if (!txtEntryDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Vui lòng nhập entry date");
                    return;
                }
                if (String.IsNullOrWhiteSpace(Quantity))
                {
                    MessageBox.Show("Vui lòng số lượng");
                    return;
                }
                if (String.IsNullOrWhiteSpace(Status))
                {
                    MessageBox.Show("Vui lòng nhập trạng thái");
                    return;
                }
                if (String.IsNullOrWhiteSpace(Vaccine))
                {
                    MessageBox.Show("Vui lòng chọn tên vaccine");
                    return;
                }
                _vaccineDetail.EntryDate = txtEntryDate.SelectedDate.Value;
                _vaccineDetail.Quantity = int.Parse(Quantity);
                _vaccineDetail.Status = int.Parse(Status); ;
                _vaccineDetail.VaccineId = int.Parse(Vaccine);

                DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    
}
