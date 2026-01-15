using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Graphics;
using System.Text;
using System.Threading.Tasks;

namespace Saveilka.Models
{
    public class Items
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public string name { get; set; }
        public float rate { get; set; } //оцінка
        public string description { get; set; } //опис
        public string type { get; set; } //група
        public string dziedzina { get; set; } //жанр
        public byte[]? image { get; set; } //фото

    }
}
