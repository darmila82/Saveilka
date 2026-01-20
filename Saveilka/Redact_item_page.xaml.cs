using Saveilka.Data;
using Saveilka.Models;
using System.Collections.ObjectModel;
using Microsoft.Maui.Storage;
using System.Text.RegularExpressions;
namespace Saveilka;

public partial class Redact_item_page : ContentPage
{
    private readonly Database _database;
    private readonly Groups _group;
    private Items _item;
    public Redact_item_page(Database database, Groups group,Items item)
	{
		InitializeComponent();
        _database = database;
        _group = group;
        _item = item;
        Title = $"Редагувати: {item.name}";
    }

    private async void name_redact_clicked(object sender, EventArgs e) //редактіровать імя
    {
        string name = await DisplayPromptAsync("Нова назва", "Введіть нову назву");
        if (!string.IsNullOrWhiteSpace(name))
        {
            _item.name = name;

            await _database.update_item(_item);
            await Navigation.PopAsync();
        }

    }

    private async void image_redact_clicked(object sender, EventArgs e) //редактіровать фото
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
        _item.image = ms.ToArray();

        await _database.update_item(_item);
        await Navigation.PopAsync();
    }
    private async void rate_redact_clicked(object sender, EventArgs e) //редактіровать оцінку
    {
        string name = await DisplayPromptAsync("Нова оцінка", "Введіть нову оцінку");
        if (!string.IsNullOrWhiteSpace(name))
        {
            _item.rate = float.Parse(name);

            await _database.update_item(_item);
            await Navigation.PopAsync();
        }
    }
    private async void desc_redact_clicked(object sender, EventArgs e) //редактіровать опис
    {
        string name = await DisplayPromptAsync("Новий опис", "Введіть новий опис");
        if (!string.IsNullOrWhiteSpace(name))
        {
            _item.description = name;

            await _database.update_item(_item);
            await Navigation.PopAsync();
        }
    }

    private async void dzied_redact_clicked(object sender, EventArgs e) //редактіровать жанр
    {
        string name = await DisplayPromptAsync("Новий опис", "Введіть новий жанр");
        if (!string.IsNullOrWhiteSpace(name))
        {
            _item.dziedzina = name;

            await _database.update_item(_item);
            await Navigation.PopAsync();
        }
    }

    private async void dop_image_redact_clicked(object sender, EventArgs e) //редактіровать фото
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
        _item.images1 = ms1.ToArray();

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
        _item.images2 = ms2.ToArray();

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
        _item.images3 = ms3.ToArray();

        await _database.update_item(_item);
        await Navigation.PopAsync();
    }
}