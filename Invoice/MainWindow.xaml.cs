using Invoice.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Invoice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Invoice_ invoice;
        print printpage;
        public MainWindow()
        {
            InitializeComponent();
            invoice = new Invoice_();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int q;
            int.TryParse(ItemQuantity.Text, out q);
            int p;
            int.TryParse(Price.Text, out p);
            var newitem = new Item
            {
                ItemName = ItemName.Text,
                ItemQuantity = q,
                price = p,
                total = p * q,
                ItemId = invoice.Items.Count + 1

            };
            if (!Validate(newitem))
                return;
            invoice.All += newitem.ItemQuantity * newitem.price;
            invoice.Items.Add(newitem);
            InvoiceGrid.Items.Add(newitem);

            ClearTextBlocks();

        }
        private bool Validate(Item item)
        {
            if (string.IsNullOrWhiteSpace(item.ItemName))
            {
                MessageBox.Show("أملئ خانه اسم المنتج من فضلك");
                return false;
            }

            if (item.ItemQuantity == 0)
            {
                MessageBox.Show("أملئ خانه العدد بقيمه صحيحه من فضلك");
                return false;
            }

            if (item.price == 0)
            {
                MessageBox.Show("أملئ خانه السعر بقيمه صحيحه من فضلك");
                return false;
            }
            return true;



        }
        private void ClearTextBlocks()
        {
            ItemQuantity.Clear();
            Price.Clear();
            ItemName.Clear();
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            printpage = new print();
            if (!getDataFromMainPage())
                return;
            InithializePrintPage();

            printpage.Show();


            clearinvoice();
        }
        private bool getDataFromMainPage()
        {
            if (!string.IsNullOrWhiteSpace(CustommerName.Text.ToString()))
                invoice.client.ClientName = CustommerName.Text.ToString();
            else
            {
                MessageBox.Show("أملئ خانه الاسم بقيمه صحيحه من فضلك");
                return false;
            }
            if (!string.IsNullOrWhiteSpace(CustommerPhone.Text.ToString()))
                invoice.client.ClientNumber = CustommerPhone.Text.ToString();
            else
            {
                MessageBox.Show("أملئ خانه رقم الهاتف بقيمه صحيحه من فضلك");
                return false;
            }
            if (!string.IsNullOrWhiteSpace(CustommerAddress.Text.ToString()))
                invoice.client.Address = CustommerAddress.Text.ToString();
            else
            {
                MessageBox.Show("أملئ خانه العنوان بقيمه صحيحه من فضلك");
                return false;
            }
            if(invoice.Items.Count == 0)
            {
                MessageBox.Show("ليس هناك اي منتجات لعمل فاتوره");
                return false;
            }

            //TODO: Some validation for the phone number but it depends on the kind 

            return true;
            
        }
        private void InithializePrintPage()
        {
            printpage.InvoiceNumber.Text = Invoice_.Number.ToString();
            printpage.CustommerName.Text = invoice.client.ClientName;
            printpage.Address.Text = invoice.client.Address;
            printpage.AllMoney.Text = "جنيه" + invoice.All.ToString() ;
            printpage.PhoneNumber.Text = invoice.client.ClientNumber;
            printpage.Date.Text = DateTime.Now.ToString();
            foreach (var i in invoice.Items)
                printpage.InvoiceGrid.Items.Add(i);

        }
        public void clearinvoice()
        {
            CustommerName.Clear();
            CustommerPhone.Clear();
            CustommerAddress.Clear();
            InvoiceGrid.Items.Clear();
            id.Clear();

            clearInvoiceData();
        }

        public void clearInvoiceData()
        {
            invoice.InvoideId++;
            invoice.Items.Clear();
            invoiceid.Text = invoice.InvoideId.ToString();
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            int i;
            int.TryParse(id.Text.ToString(), out i);
            if (i == 0 || invoice.Items.Count < i)
            {
                MessageBox.Show("أملئ خانه رقم المنتج بقيمه صحيحه من فضلك");
                return ;
            }
            
            var item = invoice.Items[i-1];
            invoice.All -= item.ItemQuantity * item.price;
            invoice.Items.Remove(item);
            InvoiceGrid.Items.Clear();
            foreach (var it in invoice.Items)
                InvoiceGrid.Items.Add(it);
            id.Clear();
        }
    }
}
