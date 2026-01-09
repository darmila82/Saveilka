using MauiApp1.Data;
using MauiApp1.Models;
using System.Text.RegularExpressions;

namespace MauiApp1;

public partial class ItemsPage : ContentPage
{
    private readonly Database _database;
    private readonly Groups _group;

    public ItemsPage(Database database, Groups group)
    {
        InitializeComponent();
        _database = database;
        _group = group;
        Title = group.name;
        LoadItems();
    }

    private async void LoadItems()
    {
        ItemsView.ItemsSource = await _database.GetItemsByGroupAsync(_group.name);
    }

    private async void OnAddItemClicked(object sender, EventArgs e)
    {
        var name = await DisplayPromptAsync("Новий Item", "Введіть назву");
        if (string.IsNullOrWhiteSpace(name))
            return;

        var scoreStr = await DisplayPromptAsync("Оцінка", "Введіть оцінку");
        if (!int.TryParse(scoreStr, out int rate))
            return;

        var desc = await DisplayPromptAsync("Пояснення", "Введіть пояснення");

        var item = new Items
        {
            name = name,
            rate = rate,
            description = desc,
            type = _group.name
        };

        await _database.SaveItemAsync(item);
        LoadItems();
    }
}
