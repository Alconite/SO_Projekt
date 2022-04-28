using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SO_Projekt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox.Image = Image.FromFile("D:\\Andrii Kobel\\Systemy Operacyjne\\SO_Projekt\\materials\\mapa_v3.png"); // УВАГА. Тут нужно менять под путь к файлу на своём компьютере
        }

    }
}
