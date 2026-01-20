using Saveilka.Data;
using Saveilka.Models;

namespace Saveilka;

public partial class add_item_page : ContentPage
{
    private readonly Database _database;
    private readonly Groups _group;

    private byte[]? _imageBytes;
    private byte[]? _imageBytes1;
    private byte[]? _imageBytes2;
    private byte[]? _imageBytes3;


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
            dziedzina = dziedzina_entry.Text,
            image = _imageBytes,
            images1 = _imageBytes1,
            images2 = _imageBytes2,
            images3 = _imageBytes3,
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

    private async void pick_dop_image_clicked(object sender, EventArgs e)
    {
        var result1 = await FilePicker.Default.PickAsync(
    new PickOptions
    {
        PickerTitle = "Оберіть фото",
        FileTypes = FilePickerFileType.Images
    });

        if (result1 == null)
            return;

        using var stream1 = await result1.OpenReadAsync();
        using var ms1 = new MemoryStream();

        await stream1.CopyToAsync(ms1);
        _imageBytes1 = ms1.ToArray();

        var result2 = await FilePicker.Default.PickAsync(
    new PickOptions
    {
        PickerTitle = "Оберіть фото",
        FileTypes = FilePickerFileType.Images
    });

        if (result2 == null)
            return;

        using var stream2 = await result2.OpenReadAsync();
        using var ms2 = new MemoryStream();

        await stream2.CopyToAsync(ms2);
        _imageBytes2 = ms2.ToArray();

        var result3 = await FilePicker.Default.PickAsync(
    new PickOptions
    {
        PickerTitle = "Оберіть фото",
        FileTypes = FilePickerFileType.Images
    });

        if (result3 == null)
            return;

        using var stream3 = await result3.OpenReadAsync();
        using var ms3 = new MemoryStream();

        await stream3.CopyToAsync(ms3);
        _imageBytes3 = ms3.ToArray();
    }
}
