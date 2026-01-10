using Saveilka.Data;
using Saveilka.Models;
using System.Text.RegularExpressions;

namespace Saveilka // на всі ошибки похуй,вони чось самі дауни

{
    public partial class MainPage : ContentPage
    {
        private readonly Database _database;
        private List<Groups> _allGroups;
        private Groups _selectedGroup;
        public MainPage(Database database) // конструктор,шо непонятно
        {
            InitializeComponent();
            _database = database;
            load_groups();

            groups_list.SelectionChanged += (s, e) => // це шоб можна було "вибрать" групу
            {
                _selectedGroup = e.CurrentSelection.FirstOrDefault() as Groups;
            };
        }
        private async void load_groups() // прогружаєм і виводим список груп
        {
            _allGroups = await _database.get_groups();
            groups_list.ItemsSource = _allGroups;
        }
        private void search_group_changed(object sender, TextChangedEventArgs e) // це поіск
        {
            string filter = e.NewTextValue?.ToLower() ?? "";

            var filteredGroups = _allGroups
                .Where(g => g.name.ToLower().Contains(filter))
                .ToList();

            groups_list.ItemsSource = filteredGroups;
        }
        private async void add_button_clicked(object sender, EventArgs e) // додавання в БД групи
        {
            string name = await DisplayPromptAsync("Нова група", "Введіть назву групи");
            if (!string.IsNullOrWhiteSpace(name))
            {
                await _database.save_groups(new Groups { name = name });
                load_groups();
            }
        }

        private async void open_group_clicked(object sender, EventArgs e) // открить вибрану групу
        {
            if (_selectedGroup == null)
            {
                await DisplayAlert("Помилка", "Будь ласка, оберіть групу зі списку", "OK");
                return;
            }

            await Navigation.PushAsync(new ItemsPage(_database, _selectedGroup));
        }
        private async void delete_group_clicked(object sender, EventArgs e) // удалить вибрану групу
        {
            if (_selectedGroup == null)
            {
                await DisplayAlert("Помилка", "Будь ласка, оберіть групу зі списку", "OK");
                return;
            }

            bool confirm = await DisplayAlert("Підтвердження",
                $"Видалити групу '{_selectedGroup.name}'?", "Так", "Ні");

            if (confirm)
            {
                await _database.delete_group(_selectedGroup);
                _selectedGroup = null;
                load_groups();
            }

        }

    }
}

