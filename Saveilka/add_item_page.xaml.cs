using Saveilka.Data;
using Saveilka.Models;

namespace Saveilka;

public partial class add_item_page : ContentPage
{
    private readonly Database _database;

    public add_item_page(Database database)
    {
        InitializeComponent();
        _database = database;
    }

    private async void save_button_clicked(object sender, EventArgs e)
    {
        // Перевірка введених даних
        if (string.IsNullOrWhiteSpace(name_entry.Text))
        {
            await DisplayAlert("Помилка", "Ім’я не може бути порожнім", "OK");
            return;
        }


        if (!float.TryParse(rate_entry.Text, out float rate))
        {
            await DisplayAlert("Помилка", "Оцінка повинна бути числом", "OK");
            return;
        }

        // Створюємо новий запис
        var item = new Items
        {
            name = name_entry.Text,
            rate = rate,
            description = description_entry.Text
        };

        // Зберігаємо в БД
        await _database.save_items(item);

        // Закриваємо модальне вікно
        await Navigation.PopModalAsync();
    }

    private async void cancel_button_clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}
