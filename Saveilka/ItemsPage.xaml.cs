using Saveilka.Data;
using Saveilka.Models;
using System.Collections.ObjectModel;
using Microsoft.Maui.Storage;
using System.Text.RegularExpressions;

namespace Saveilka;

public partial class ItemsPage : ContentPage
{
    private readonly Database _database;
    private readonly Groups _group;
    private List<Items> _all_items = new();
    private ObservableCollection<Items> displayed_items = new();
    private Items _selected_item;

    public ItemsPage(Database database, Groups group) // конструктор,шо непонятно
    {
        InitializeComponent();
        _database = database;
        _group = group;
        Title = group.name;
        load_items();



        displayed_items = new ObservableCollection<Items>(_all_items ?? new List<Items>()); //це заповнить + отрісовать лист в залежності от групи
        items_list.ItemsSource = displayed_items;
    }

    public async void load_items() // прогружаєм і виводим список items
    {
        _all_items = await _database.get_items_by_groups(_group.name);
        items_list.ItemsSource = _all_items;
    }
    private void search_item_changed(object sender, TextChangedEventArgs e) // це поіск
    {
        string filter = e.NewTextValue?.ToLower() ?? "";

        var filteredGroups = _all_items
            .Where(g => g.name.ToLower().Contains(filter))
            .ToList();

        items_list.ItemsSource = filteredGroups;
    }

    private async void delete_item_clicked(object sender, EventArgs e) // удалить вибраний item
    {
        if (_selected_item == null)
        {
            await DisplayAlert("Помилка", "Будь ласка, оберіть групу зі списку", "OK");
            return;
        }

        bool confirm = await DisplayAlert("Підтвердження",
            $"Видалити '{_selected_item.name}'?", "Так", "Ні");

        if (confirm)
        {
            await _database.delete_items(_selected_item);
            _selected_item = null;
            load_items();
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        load_items();
    }
    private async void add_item_button_clicked(object sender, EventArgs e) //откриваєм окно для додавання item в цю групу
    {
        await Navigation.PushAsync(new add_item_page(_database, _group));
        load_items();
    }

    private void item_selected(object sender, SelectionChangedEventArgs e) //обработать,який щас вибраний item
    {
        _selected_item = e.CurrentSelection.FirstOrDefault() as Items;



        if (_selected_item?.image == null)
        {
            selected_image.Source = null;
            return;
        }

        selected_image.Source = ImageSource.FromStream(
            () => new MemoryStream(_selected_item.image)
        );


    }

    private async void redact_item_button_clicked(object sender, EventArgs e) //откриваєм окно для редактірованія
    {
        if (_selected_item == null)
        {
            await DisplayAlert("Помилка", "Будь ласка, оберіть,що редагувати", "OK");
            return;
        }
        await Navigation.PushAsync(new Redact_item_page(_database, _group,_selected_item));
    }

}
