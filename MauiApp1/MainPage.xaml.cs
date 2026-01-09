using MauiApp1.Data;
using MauiApp1.Models;
using System.Text.RegularExpressions;

namespace MauiApp1

{
    public partial class MainPage : ContentPage
    {
        private readonly Database _database;

        public MainPage(Database database)
        {
            InitializeComponent();
            _database = database;
            load_groups();
        }
        private async void load_groups() // прогружаєм і виводим список груп
        {
            GroupsView.ItemsSource = await _database.GetGroupsAsync();
        }
        private async void add_button_clicked(object sender, EventArgs e) // додавання в БД item
        {
            string name = await DisplayPromptAsync("Нова група", "Введіть назву групи");
            if (!string.IsNullOrWhiteSpace(name))
            {
                await _database.SaveGroupAsync(new Groups { name = name });
                load_groups();
            }
        }
        private async void group_selected(object sender, SelectionChangedEventArgs e)
        {
            var group = e.CurrentSelection.FirstOrDefault() as Groups;
            if (group != null)
            {
                // Переходимо на сторінку з item цієї групи
                await Navigation.PushAsync(new ItemsPage(_database, group));
            }
        }
        private async void delete_button_clicked(object sender, EventArgs e) // видалення
        {
            var item = (sender as SwipeItem)?.CommandParameter as Items;
            if (item != null)
            {

                load_groups();
            }
        }

    }
}
