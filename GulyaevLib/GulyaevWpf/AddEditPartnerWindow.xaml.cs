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
    /// Логика взаимодействия для AddEditPartnerWindow.xaml
    /// </summary>
    public partial class AddEditPartnerWindow : Window
    {
        private readonly ApplicationContext _context;
        private Service _service;
        private bool is_add;
        private PartnerView _selectedpartner;
        public AddEditPartnerWindow(bool add_true, PartnerView selectedpartner)
        {
            InitializeComponent();
            _service = new Service();
            _context = new ApplicationContext();
            is_add = add_true;
            _selectedpartner = selectedpartner;
            if (is_add == true)
            {
                Title = "Окно добавления";
                Add_b.Content = "Добавить";
            }
            else
            {
                Title = "Окно редактирования";
                Add_b.Content = "Обновить";
                typebox.Text = _selectedpartner.PartnerType;
                namebox.Text = _selectedpartner.PartnerName;
                dirbox.Text = _selectedpartner.PartnerDirector;
                mailbox.Text = _selectedpartner.PartnerEmail;
                innbox.Text = _selectedpartner.PartnerInn;
                addressbox.Text = _selectedpartner.PartnerAddress;
                numberbox.Text = _selectedpartner.PartnerNumber;
                ratingbox.Text = Convert.ToString(_selectedpartner.PartnerRating);

            }
        }

        private bool ValidatePartnerInput(out short rating)
        {
            rating = 0;

            if (typebox.SelectedItem == null && string.IsNullOrWhiteSpace(typebox.Text))
            {
                MessageBox.Show("Выберите тип партнёра.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(namebox.Text))
            {
                MessageBox.Show("Заполните наименование партнёра.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(dirbox.Text))
            {
                MessageBox.Show("Укажите директора.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(mailbox.Text) || !mailbox.Text.Contains("@") || !mailbox.Text.Contains("."))
            {
                MessageBox.Show("Введите корректный e‑mail (должен содержать '@' и домен).", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            var phone = numberbox.Text;
            if (string.IsNullOrWhiteSpace(phone) ||
                phone.Length != 12 ||
                !phone.StartsWith("+79") ||
                !phone.Skip(1).All(char.IsDigit))
            {
                MessageBox.Show("Введите номер телефона в формате +79XXXXXXXXX (например, +79123456789).", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(innbox.Text) || !(innbox.Text.Length == 10 || innbox.Text.Length == 12) || !innbox.Text.All(char.IsDigit))
            {
                MessageBox.Show("Введите корректный ИНН (10 или 12 цифр).", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(addressbox.Text))
            {
                MessageBox.Show("Заполните адрес.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(ratingbox.Text) || !short.TryParse(ratingbox.Text, out rating) || rating < 0 || rating > 100)
            {
                MessageBox.Show("Введите корректный рейтинг (число от 0 до 100).", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void Add_b_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidatePartnerInput(out var rating))
            {
                return;
            }

            var partnerTypeValue = typebox.Text;

            if (is_add == true)
            {
                var data = new Partner
                {
                    PartnerType = partnerTypeValue,
                    PartnerName = namebox.Text,
                    PartnerDirector = dirbox.Text,
                    PartnerEmail = mailbox.Text,
                    PartnerInn = innbox.Text,
                    PartnerAddress = addressbox.Text,
                    PartnerNumber = numberbox.Text,
                    PartnerRating = rating
                };
                _service.AddPartner(_context, data);
                this.DialogResult = true;
                Close();
            }
            else
            {
                var data = new Partner
                {
                    Id = _selectedpartner.Id,
                    PartnerType = partnerTypeValue,
                    PartnerName = namebox.Text,
                    PartnerDirector = dirbox.Text,
                    PartnerEmail = mailbox.Text,
                    PartnerInn = innbox.Text,
                    PartnerAddress = addressbox.Text,
                    PartnerNumber = numberbox.Text,
                    PartnerRating = rating
                };
                _service.UpdatePartner(_context, data);
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


