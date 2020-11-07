using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Potor_Raul_Lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }

    public partial class MainWindow : Window
    {

        ActionState action = ActionState.Nothing;
        PhoneNumbersDataset phoneNumbersDataSet = new PhoneNumbersDataset();
        PhoneNumbersDatasetTableAdapters.PhoneNumbers1TableAdapter
       tblPhoneNumbersAdapter = new PhoneNumbersDatasetTableAdapters.PhoneNumbers1TableAdapter();
        Binding txtPhoneNumberBinding = new Binding();
        Binding txtSubscriberBinding = new Binding();
        Binding txtContractDateBinding = new Binding();
        Binding txtContractValueBingind = new Binding();


        public MainWindow()
        {
            InitializeComponent();

            grdMain.DataContext = phoneNumbersDataSet.PhoneNumbers1;
            txtPhoneNumberBinding.Path = new PropertyPath("Phonenum");
            txtSubscriberBinding.Path = new PropertyPath("Subscriber");
            txtPhoneNumber.SetBinding(TextBox.TextProperty, txtPhoneNumberBinding);
            txtSubscriber.SetBinding(TextBox.TextProperty, txtSubscriberBinding);
        }

        private void lstPhonesLoad()
        {
            tblPhoneNumbersAdapter.Fill(phoneNumbersDataSet.PhoneNumbers1);
        }

        private void grdMain_Loaded(object sender, RoutedEventArgs e)
        {
            lstPhonesLoad();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Close Application?", "Question", MessageBoxButton.YesNo,
MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {
            PhoneNumbersDataset phoneNumbersDataSet =
((PhoneNumbersDataset)(this.FindResource("phoneNumbersDataset")));
            System.Windows.Data.CollectionViewSource phoneNumbersViewSource =
            ((System.Windows.Data.CollectionViewSource)(this.FindResource("phoneNumbersViewSource")));
           // phoneNumbersViewSource.View.MoveCurrentToFirst();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;

            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            lstPhones.IsEnabled = false;
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            txtPhoneNumber.IsEnabled = true;
            txtSubscriber.IsEnabled = true;
            txtContractValue.IsEnabled = true;
            txtContractDate.IsEnabled = true;
            BindingOperations.ClearBinding(txtPhoneNumber, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtSubscriber, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtContractValue, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtContractDate, TextBox.TextProperty);
            txtPhoneNumber.Text = "";
            txtSubscriber.Text = "";
            Keyboard.Focus(txtPhoneNumber);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            string tempPhonenum = txtPhoneNumber.Text.ToString();
            string tempSubscriber = txtSubscriber.Text.ToString();
            string tempContractValue = txtContractValue.Text.ToString();
            string tempContractDate = txtContractDate.Text.ToString();
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            lstPhones.IsEnabled = false;
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            txtPhoneNumber.IsEnabled = true;
            txtSubscriber.IsEnabled = true;
            txtContractDate.IsEnabled = true;
            txtContractValue.IsEnabled = true;
            BindingOperations.ClearBinding(txtPhoneNumber, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtSubscriber, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtContractValue, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtContractDate, TextBox.TextProperty);
            txtPhoneNumber.Text = tempPhonenum;
            txtSubscriber.Text = tempSubscriber;
            txtContractValue.Text = tempContractValue;
            txtContractDate.Text = tempContractDate;
            Keyboard.Focus(txtPhoneNumber);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
            string tempPhonenum = txtPhoneNumber.Text.ToString();
            string tempSubscriber = txtSubscriber.Text.ToString();
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            lstPhones.IsEnabled = false;
            btnPrevious.IsEnabled = false;
            btnNext.IsEnabled = false;
            BindingOperations.ClearBinding(txtPhoneNumber, TextBox.TextProperty);
            BindingOperations.ClearBinding(txtSubscriber, TextBox.TextProperty);
            txtPhoneNumber.Text = tempPhonenum;
            txtSubscriber.Text = tempSubscriber;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;
            btnNew.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;
            lstPhones.IsEnabled = true;
            btnPrevious.IsEnabled = true;
            btnNext.IsEnabled = true;
            txtPhoneNumber.IsEnabled = false;
            txtSubscriber.IsEnabled = false;
            txtContractValue.IsEnabled = false;
            txtContractDate.IsEnabled = false;
            txtPhoneNumber.SetBinding(TextBox.TextProperty, txtPhoneNumberBinding);
            txtSubscriber.SetBinding(TextBox.TextProperty, txtSubscriberBinding);
           


        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (action == ActionState.New)
            {
                try
                {
                    DataRow newRow = phoneNumbersDataSet.PhoneNumbers1.NewRow();
                    newRow.BeginEdit();
                    newRow["Phonenum"] = txtPhoneNumber.Text.Trim();
                    newRow["Subscriber"] = txtSubscriber.Text.Trim();
                    newRow["Contract_value"] = txtContractValue.Text.Trim();
                    newRow["Contract_date"] = txtContractDate.Text.Trim();
                    newRow.EndEdit();
                    phoneNumbersDataSet.PhoneNumbers1.Rows.Add(newRow);
                    tblPhoneNumbersAdapter.Update(phoneNumbersDataSet.PhoneNumbers1);
                    phoneNumbersDataSet.AcceptChanges();
                }
                catch (DataException ex)
                {
                    phoneNumbersDataSet.RejectChanges();
                    MessageBox.Show(ex.Message);
                }
                btnNew.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnCancel.IsEnabled = false;
                lstPhones.IsEnabled = true;
                btnPrevious.IsEnabled = true;
                btnNext.IsEnabled = true;
                txtPhoneNumber.IsEnabled = false;
                txtSubscriber.IsEnabled = false;
                txtContractDate.IsEnabled = false;
                txtContractValue.IsEnabled = false;
            }
            else
 if (action == ActionState.Edit)
            {
                try
                {
                    DataRow editRow = phoneNumbersDataSet.PhoneNumbers1.Rows[lstPhones.SelectedIndex];
                    editRow.BeginEdit();
                    editRow["Phonenum"] = txtPhoneNumber.Text.Trim();
                    editRow["Subscriber"] = txtSubscriber.Text.Trim();
                    editRow["Contract_value"] = txtContractValue.Text.Trim();
                    editRow["Contract_date"] = txtContractDate.Text.Trim();
                    editRow.EndEdit();
                    tblPhoneNumbersAdapter.Update(phoneNumbersDataSet.PhoneNumbers1);
                    phoneNumbersDataSet.AcceptChanges();
                }
                catch (DataException ex)
                {
                    phoneNumbersDataSet.RejectChanges();
                    MessageBox.Show(ex.Message);
                }
                btnNew.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnCancel.IsEnabled = false;
                lstPhones.IsEnabled = true;
                btnPrevious.IsEnabled = true;
                btnNext.IsEnabled = true;
                txtPhoneNumber.IsEnabled = false;
                txtSubscriber.IsEnabled = false;
                txtContractDate.IsEnabled = false;
                txtContractValue.IsEnabled = false;
                txtPhoneNumber.SetBinding(TextBox.TextProperty, txtPhoneNumberBinding);
                txtSubscriber.SetBinding(TextBox.TextProperty, txtSubscriberBinding);
                txtContractValue.SetBinding(TextBox.TextProperty, txtContractValueBingind);
                txtContractDate.SetBinding(TextBox.TextProperty, txtContractDateBinding);
            }
            else
 if (action == ActionState.Delete)
            {
                try
                {
                    DataRow deleterow = phoneNumbersDataSet.PhoneNumbers1.Rows[lstPhones.SelectedIndex];
                    deleterow.Delete();

                    tblPhoneNumbersAdapter.Update(phoneNumbersDataSet.PhoneNumbers1);
                    phoneNumbersDataSet.AcceptChanges();
                }
                catch (DataException ex)
                {
                    phoneNumbersDataSet.RejectChanges(); MessageBox.Show(ex.Message);
                    MessageBox.Show(ex.Message);
                }
                btnNew.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnCancel.IsEnabled = false;
                lstPhones.IsEnabled = true;
                btnPrevious.IsEnabled = true;
                btnNext.IsEnabled = true;
                txtPhoneNumber.IsEnabled = false;
                txtSubscriber.IsEnabled = false;
                txtContractValue.IsEnabled = false;
                txtContractDate.IsEnabled = true;
                txtPhoneNumber.SetBinding(TextBox.TextProperty, txtPhoneNumberBinding);
                txtSubscriber.SetBinding(TextBox.TextProperty, txtSubscriberBinding);
                txtContractDate.SetBinding(TextBox.TextProperty, txtContractDateBinding);
                txtContractValue.SetBinding(TextBox.TextProperty, txtContractValueBingind);
            }
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView navigationView =
CollectionViewSource.GetDefaultView(phoneNumbersDataSet.PhoneNumbers1);
            navigationView.MoveCurrentToPrevious();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            ICollectionView navigationView =
CollectionViewSource.GetDefaultView(phoneNumbersDataSet.PhoneNumbers1);
            navigationView.MoveCurrentToNext();
        }
    }
}
