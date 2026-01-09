using System;
using MauiApp1.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;

namespace MauiApp1
{
    public partial class MauiProgram
    {

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            string dbPath = Path.Combine( // тут тіпа привязуєм БД
                FileSystem.AppDataDirectory,
                "app.db3");
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton( // це шоб на всю прогу тіки 1 БД юзалась
           new Database(dbPath));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    

    } 
}
