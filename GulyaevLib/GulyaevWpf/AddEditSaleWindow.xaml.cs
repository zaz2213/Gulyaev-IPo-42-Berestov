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
using GulyaevLib;

namespace GulyaevWpf
{
    /// <summary>
    /// Логика взаимодействия для AddEditSaleWindow.xaml
    /// </summary>
    public partial class AddEditSaleWindow : Window
    {
        private readonly ApplicationContext _context;
        private Service _service;
        private PartnerView _selectedpartner;
        private SalesView _selectedsales;

        private bool is_add;
        public AddEditSaleWindow(bool add_true, PartnerView selectedpartner, SalesView sale)
        {
            InitializeComponent();
            _service = new Service();
            _context = new ApplicationContext();
            is_add = add_true;
            _selectedpartner = selectedpartner;
            _selectedsales = sale;
            productbox.ItemsSource = _service.GetProductInfo(_context);
            if (is_add == true)
            {
                Title = "Окно добавления";
                Add_b.Content = "Добавить";
            }
            else
            {
                Title = "Окно редактирования";
                Add_b.Content = "Обновить";
                coubox.Text = _selectedsales.PartnerCount.ToString();
                if (_selectedsales.SaleDate != null)
                {
                    datebox.SelectedDate = _selectedsales.SaleDate.Value.ToDateTime(TimeOnly.MinValue);
                }
                productbox.Text = _selectedsales.Product_name;

            }
        }

        private bool ValidateInput(out int count, out DateOnly saleDate)
        {
            count = 0;
            saleDate = default;

            if (string.IsNullOrWhiteSpace(productbox.Text))
            {
                MessageBox.Show("Выберите продукт.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(coubox.Text) || !int.TryParse(coubox.Text, out count) || count <= 0)
            {
                MessageBox.Show("Введите корректное количество (целое положительное число).", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!datebox.SelectedDate.HasValue)
            {
                MessageBox.Show("Выберите дату продажи.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            saleDate = DateOnly.FromDateTime(datebox.SelectedDate.Value);
            return true;
        }

        private void Add_b_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput(out var count, out var saleDate))
            {
                return;
            }

            if (is_add == true)
            {
                var data = new PartnerProduct
                {
                    Partner = _selectedpartner.Id,
                    Product = _context.Products.Where(p => p.ProductName == productbox.Text).Select(p => p.Id).FirstOrDefault(),
                    PartnerCount = count,
                    SaleDate = saleDate
                };
                _service.AddSale(_context, data);
                this.DialogResult = true;
                Close();
            }
            else
            {
                var data = new PartnerProduct
                {
                    Id = _selectedsales.Id,
                    Partner = _selectedpartner.Id,
                    Product = _context.Products.Where(p => p.ProductName == productbox.Text).Select(p => p.Id).FirstOrDefault(),
                    PartnerCount = count,
                    SaleDate = saleDate
                };
                _service.UpdateSale(_context, data);
                this.DialogResult = true;
                Close();
            }
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }
    }
}
