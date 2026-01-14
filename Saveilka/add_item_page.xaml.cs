using Saveilka.Data;
using Saveilka.Models;

namespace Saveilka;

public partial class add_item_page : ContentPage
{
    private readonly Database _database;
    private readonly Groups _group;

    private byte[]? _imageBytes;


    public add_item_page(Database database,Groups group)
    {
        InitializeComponent();
        _database = database;
        _group = group;


        Title = $"Додати до: {group.name}";

    }

    private async void save_button_clicked(object sender, EventArgs e) 
    {
        //шоб було ім
        if (string.IsNullOrWhiteSpace(name_entry.Text)) 
        {
            await DisplayAlert("Помилка", "Ім’я не може бути порожнім", "OK");
            return;
        }

        //шоб була оцінка
        if (!float.TryParse(rate_entry.Text, out float rate))
        {
            await DisplayAlert("Помилка", "Оцінка повинна бути числом", "OK");
            return;
        }


        //записуєм item в БД
        var item = new Items
        {
            name = name_entry.Text,
            rate = rate,
            description = description_entry.Text,
            type = _group.name,
            image = _imageBytes
        };


        //зберігаєм в БД
        await _database.save_items(item);

        //закриваєм модальне вікно
        await Navigation.PopAsync();

        
    }

    private async void cancel_button_clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void pick_image_clicked(object sender, EventArgs e)
    {
        var result = await FilePicker.Default.PickAsync(
            new PickOptions
            {
                PickerTitle = "Оберіть фото",
                FileTypes = FilePickerFileType.Images
            });

        if (result == null)
            return;

        using var stream = await result.OpenReadAsync();
        using var ms = new MemoryStream();

        await stream.CopyToAsync(ms);
        _imageBytes = ms.ToArray();


        preview_image.Source = ImageSource.FromStream(() => new MemoryStream(_imageBytes));
        preview_image.IsVisible = true;
    }
}
