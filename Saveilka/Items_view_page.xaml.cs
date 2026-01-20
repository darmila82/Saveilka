using Saveilka.Data;
using Saveilka.Models;
using System.Collections.ObjectModel;
using Microsoft.Maui.Storage;
using System.Text.RegularExpressions;

namespace Saveilka;

public partial class Items_view_page : ContentPage
{

    public Items_view_page(Database database,Items item)
	{
		InitializeComponent();
        Title = $"Огляд {item.name}";
        load_images(item);
    }

    public async void load_images(Items item)
    {
        if (item.image == null || item.images1 == null || item.images2 == null || item.images3 == null)
        {
            await DisplayAlert("Помилка", "Немає,що розгортати,додайте фото", "OK");
            return;
        }
        selected_image.Source = ImageSource.FromStream(
    () => new MemoryStream(item.image)
);

        image1.Source = ImageSource.FromStream(
          () => new MemoryStream(item.images1)
      );

        image2.Source = ImageSource.FromStream(
    () => new MemoryStream(item.images2)
);

        image3.Source = ImageSource.FromStream(
          () => new MemoryStream(item.images3)
      );
    }
}