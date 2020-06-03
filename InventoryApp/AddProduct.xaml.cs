using InventoryApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InventoryApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProduct : ContentPage
    {
        private Entry _nameEntry;
        private Entry _quantity;
        private Button _saveButton;

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db1");

        public AddProduct()
        {
            InitializeComponent();

            StackLayout stackLayout = new StackLayout();

            _nameEntry = new Entry();
            _nameEntry.Keyboard = Keyboard.Text;
            _nameEntry.Placeholder = "Product Name";
            stackLayout.Children.Add(_nameEntry);

            _quantity = new Entry();
            _quantity.Keyboard = Keyboard.Text;
            _quantity.Placeholder = "Quantity";
            stackLayout.Children.Add(_quantity);

            _saveButton = new Button();
            _saveButton.Text = "Add";
            _saveButton.Clicked += _saveButton_Clicked;
            stackLayout.Children.Add(_saveButton);

            Content = stackLayout;
        }

        private async void _saveButton_Clicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            db.CreateTable<Product>();

            var maxPk = db.Table<Product>().OrderByDescending(c => c.Id).FirstOrDefault();

            Product product = new Product()
            {
                Id = (maxPk == null ? 1 : maxPk.Id + 1),
                Name = _nameEntry.Text,
                Quantity = _quantity.Text
            };
            db.Insert(product);
            await DisplayAlert(null, product.Name + " Added", "Ok");
            await Navigation.PopAsync();
        }
    }
}