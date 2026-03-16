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
using GulyaevLib;

namespace GulyaevWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApplicationContext _context;
        private Service _service;
        public MainWindow()
        {
            InitializeComponent();
            _service = new Service();
            _context = new ApplicationContext();
            RefreshPartners();
            PartnerListBox.SelectionChanged += (s, e) => RefreshSales();
        }
        private void RefreshPartners()
        {
            PartnerListBox.ItemsSource = _service.GetAllPartnerInfo(_context);
        }
        private void RefreshSales()
        {
            var selectedpartner = PartnerListBox.SelectedItem as PartnerView;
            if (selectedpartner != null)
            {
                var partnerdata = _service.GetAllSalesInfo(_context, selectedpartner.Id);
                if (partnerdata != null)
                {
                    ParnterGridData.ItemsSource = partnerdata;
                }
            }
            else
            {
                ParnterGridData.ItemsSource = null;
            }
        }

        private void RestorePartnerSelection(int partnerId)
        {
            if (PartnerListBox.Items == null)
            {
                return;
            }

            foreach (var item in PartnerListBox.Items)
            {
                if (item is PartnerView view && view.Id == partnerId)
                {
                    PartnerListBox.SelectedItem = view;
                    break;
                }
            }
        }

        private void Partner_Add(object sender, RoutedEventArgs e)
        {
            var _partnerWindow = new AddEditPartnerWindow(true, null);
            _partnerWindow.Owner = this;
            if (_partnerWindow.ShowDialog() == true)
            {
                RefreshPartners();
            }
        }
        private void Partner_Edit(object sender, RoutedEventArgs e)
        {
            var selectedpartner = PartnerListBox.SelectedItem as PartnerView;
            if (selectedpartner != null)
            {
                var selectedPartnerId = selectedpartner.Id;
                var _partnerWindow = new AddEditPartnerWindow(false, selectedpartner);
                _partnerWindow.Owner = this;
                if (_partnerWindow.ShowDialog() == true)
                {
                    RefreshPartners();
                    RestorePartnerSelection(selectedPartnerId);
                    RefreshSales();
                }
            }
        }
        private void Partner_Del(object sender, RoutedEventArgs e)
        {
            var selectedpartner = PartnerListBox.SelectedItem as PartnerView;
            if (selectedpartner != null)
            {
                var result = MessageBox.Show(
                    $"Вы уверены, что хотите удалить партнёра?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result != MessageBoxResult.Yes)
                {
                    return;
                }

                var deletepartner = new Partner
                {
                    Id = selectedpartner.Id,
                    PartnerType = selectedpartner.PartnerType,
                    PartnerName = selectedpartner.PartnerName,
                    PartnerDirector = selectedpartner.PartnerDirector,
                    PartnerEmail = selectedpartner.PartnerEmail,
                    PartnerInn = selectedpartner.PartnerInn,
                    PartnerAddress = selectedpartner.PartnerAddress,
                    PartnerNumber = selectedpartner.PartnerNumber,
                    PartnerRating = selectedpartner.PartnerRating
                };
                _service.DeletePartner(_context, deletepartner);
                RefreshPartners();
                RefreshSales();


            }
        }
        private void Sales_Add(object sender, RoutedEventArgs e)
        {
            var selectedpartner = PartnerListBox.SelectedItem as PartnerView;
            if (selectedpartner != null)
            {
                var selectedPartnerId = selectedpartner.Id;
                var _saleWindow = new AddEditSaleWindow(true, selectedpartner, null);
                _saleWindow.Owner = this;
                if (_saleWindow.ShowDialog() == true)
                {
                    RefreshPartners();
                    RestorePartnerSelection(selectedPartnerId);
                    RefreshSales();
                }
            }
        }
        private void Sales_Edit(object sender, RoutedEventArgs e)
        {
            var selectedsale = ParnterGridData.SelectedItem as SalesView;
            var selectedpartner = PartnerListBox.SelectedItem as PartnerView;
            if (selectedsale != null && selectedpartner != null)
            {
                var selectedPartnerId = selectedpartner.Id;
                var _partnerWindow = new AddEditSaleWindow(false, selectedpartner, selectedsale);
                _partnerWindow.Owner = this;
                if (_partnerWindow.ShowDialog() == true)
                {
                    RefreshPartners();
                    RestorePartnerSelection(selectedPartnerId);
                    RefreshSales();
                }
            }
        }
        private void Sales_Del(object sender, RoutedEventArgs e)
        {
            var selectedsale = ParnterGridData.SelectedItem as SalesView;
            var selectedpartner = PartnerListBox.SelectedItem as PartnerView;
            if (selectedsale != null && selectedpartner != null)
            {
                var confirm = MessageBox.Show(
                    $"Вы уверены, что хотите удалить продажу?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (confirm != MessageBoxResult.Yes)
                {
                    return;
                }

                var selectedPartnerId = selectedpartner.Id;
                var deletesale = new PartnerProduct
                {
                    Id = selectedsale.Id,
                    PartnerCount = selectedsale.PartnerCount,
                    SaleDate = selectedsale.SaleDate,
                    Product = _context.Products.Where(p => p.ProductName == selectedsale.Product_name).Select(p => p.Id).FirstOrDefault(),
                    Partner = selectedpartner.Id
                };
                _service.DeleteSale(_context, deletesale);
                RefreshPartners();
                RestorePartnerSelection(selectedPartnerId);
                RefreshSales();
            }
        }
        private void Exit_click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}