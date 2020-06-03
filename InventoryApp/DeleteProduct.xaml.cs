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
    public partial class DeleteProduct : ContentPage
    {
        private ListView _listView;
        private Button _button;

        Product _product = new Product();
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db1");

        public DeleteProduct()
        {
            InitializeComponent();

            this.Title = "Delete Product";

            var db = new SQLiteConnection(_dbPath);

            StackLayout stackLayout = new StackLayout();

            _listView = new ListView();
            _listView.ItemsSource = db.Table<Product>().OrderBy(x => x.Name).ToList();
            _listView.ItemSelected += _listView_ItemSelected;
            stackLayout.Children.Add(_listView);

            _button = new Button();
            _button.Text = "Delete Product";
            _button.Clicked += _buttonClicked;
            stackLayout.Children.Add(_button);

            Content = stackLayout;
        }

        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _product = (Product)e.SelectedItem;

        }

        private async void _buttonClicked(object sender, EventArgs e)
        {
            var db = new SQLiteConnection(_dbPath);
            await DisplayAlert(null, _product.Name + " Deleted", "Ok");
            db.Table<Product>().Delete(x => x.Id == _product.Id);
            await Navigation.PopAsync();
        }
    }
}