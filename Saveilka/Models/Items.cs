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
        public float rate { get; set; }
        public string description { get; set; }
        public string type { get; set; }

        [Ignore]
        public Color RateColor =>
    rate < 5 ? Colors.Red :
    rate < 8 ? Colors.DarkGoldenrod :
                Colors.DarkGreen;
    }
}
