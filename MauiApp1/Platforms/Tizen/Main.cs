using System;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace MauiApp1
{
    internal class Program : MauiApplication
    {
        protected override MauiApp 
            () => MauiProgram.load_app();

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
