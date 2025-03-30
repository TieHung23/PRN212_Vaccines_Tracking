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
using Validate;

namespace WPF
{
    /// <summary>
    /// Interaction logic for VaccineAddUpdateWindow.xaml
    /// </summary>
    public partial class VaccineAddUpdateWindow : Window
    {
        private Vaccine _vaccine;
        private readonly IValidate _validate;
        private bool _isNew;
        public Vaccine vaccine { get { return _vaccine; } }

        public VaccineAddUpdateWindow(Vaccine vaccine = null)
        {
            InitializeComponent();
            _validate = new Validate.Validate();

            _vaccine = vaccine ?? new Vaccine();
            _isNew = vaccine == null;

            if (!_isNew)
            {
                txtVaccineName.Text = _vaccine.Name;
                txtPrice.Text = _vaccine.Price.ToString();
                txtDescription.Text = _vaccine.Description;
                txtDosesTimes.Text = _vaccine.DosesTimes.ToString();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string vaccineName = txtVaccineName.Text;
                string price = txtPrice.Text;
                string discription = txtDescription.Text;
                string dosesTimes = txtDosesTimes.Text;
                if (String.IsNullOrWhiteSpace(vaccineName))
                {
                    MessageBox.Show("Vui lòng nhập tên vaccine");
                    return;
                }
                if (String.IsNullOrWhiteSpace(price))
                {
                    MessageBox.Show("Vui lòng nhập giá");
                    return;
                }
                if (String.IsNullOrWhiteSpace(discription))
                {
                    MessageBox.Show("Vui lòng nhập miêu tả");
                    return;
                }
                if (String.IsNullOrWhiteSpace(dosesTimes))
                {
                    MessageBox.Show("Vui lòng nhập số lần tiêm");
                    return;
                }

                if (!_validate.isString(vaccineName) ||
                   !_validate.isString(discription) )       
                {
                    MessageBox.Show("Please fill all the blank");
                    return;
                }


                _vaccine.Name = vaccineName;
                _vaccine.Price = decimal.Parse(price);
                _vaccine.Description = discription;
                _vaccine.DosesTimes = int.Parse(dosesTimes);

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
