using MauiApp1.Data;
using MauiApp1.Models;
using System.Text.RegularExpressions;

namespace MauiApp1;

public partial class ItemsPage : ContentPage
{
    private readonly Database _database;
    private readonly Groups _group;
    private List<Items> _all_items;
    private Items _selected_item;

    public ItemsPage(Database database, Groups group)
    {
        InitializeComponent();
        _database = database;
        _group = group;
        Title = group.name;
        load_items();

        items_list.SelectionChanged += (s, e) =>
        {
            _selected_item = e.CurrentSelection.FirstOrDefault() as Items;
        };
    }

    private async void load_items()
    {
       _all_items = await _database.get_items_by_groups(_group.name);
        items_list.ItemsSource = _all_items;
    }
    private void search_text_changed(object sender, TextChangedEventArgs e)
    {
        string filter = e.NewTextValue?.ToLower() ?? "";

        var filteredGroups = _all_items
            .Where(g => g.name.ToLower().Contains(filter))
            .ToList();

        items_list.ItemsSource = filteredGroups;
    }

    private async void delete_item_clicked(object sender, EventArgs e)
    {
        if (_selected_item == null)
        {
            await DisplayAlert("Помилка", "Будь ласка, оберіть групу зі списку", "OK");
            return;
        }

        bool confirm = await DisplayAlert("Підтвердження",
            $"Видалити групу '{_selected_item.name}'?", "Так", "Ні");

        if (confirm)
        {
            await _database.delete_items(_selected_item);
            _selected_item = null;
            load_items();
        }
    }

    private async void add_item_button_clicked(object sender, EventArgs e)
    {
        var name = await DisplayPromptAsync("Новий Item", "Введіть назву");
        if (string.IsNullOrWhiteSpace(name))
            return;

        var rate_bar = await DisplayPromptAsync("Оцінка", "Введіть оцінку");
        if (!int.TryParse(rate_bar, out int rate))
            return;

        var desc = await DisplayPromptAsync("Пояснення", "Введіть пояснення");

        var item = new Items
        {
            name = name,
            rate = rate,
            description = desc,
            type = _group.name
        };

        await _database.save_items(item);
        load_items();
    }
}
