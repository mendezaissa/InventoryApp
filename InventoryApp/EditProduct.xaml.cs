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
    public partial class EditProduct : ContentPage
    {
        private ListView _listView;
        private Entry _idEntry;
        private Entry _nameEntry;
        private Entry _quantity;
        private Button _button;

        Product _product = new Product();

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db1");

        public EditProduct()
        {
            InitializeComponent();

            this.Title = "Edit Product";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();

            _listView = new ListView();
            _listView.ItemsSource = db.Table<Product>().OrderBy(x => x.Name).ToList();
            _listView.ItemSelected += _listView_ItemSelected;
            stackLayout.Children.Add(_listView);

            _idEntry = new Entry();
            _idEntry.Placeholder = "ID";
            _idEntry.IsVisible = false;
            stackLayout.Children.Add(_idEntry);

            _nameEntry = new Entry();
            _nameEntry.Keyboard = Keyboard.Text;
            _nameEntry.Placeholder = "Product Name";
            stackLayout.Children.Add(_nameEntry);

            _quantity = new Entry();
            _quantity.Keyboard = Keyboard.Text;
            _quantity.Placeholder = "Quantity";
            stackLayout.Children.Add(_quantity);

            _button = new Button();
            _button.Text = "Update";
            _button.Clicked += _buttonClicked;
            stackLayout.Children.Add(_button);

            Content = stackLayout;
        }

        private async void _buttonClicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            Product product = new Product()
            {
                Id = Convert.ToInt32(_idEntry.Text),
                Name = _nameEntry.Text,
                Quantity = _quantity.Text
            };
            db.Update(product);
            await Navigation.PopAsync();
        }

        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _product = (Product)e.SelectedItem;
            _idEntry.Text = _product.Id.ToString();
            _nameEntry.Text = _product.Name;
            _quantity.Text = _product.Quantity;
        }
    }
}