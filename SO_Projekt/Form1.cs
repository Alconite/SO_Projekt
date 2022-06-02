using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SO_Projekt
{
    public partial class Form1 : Form
    {
        int poz_pociagu_x = 1044;
        int poz_pociagu_y = 0;
        int poz_car1_x = 12;
        int poz_car2_x = 12;
        int poz_car3_x = 12;
        int poz_car4_x = 1178;
        int poz_car5_x = 1178;
        int poz_car6_x = 1178;
        int poz_y_1 = 204;
        int poz_y_2 = 340;
        int poz_y_3 = 556;
        int poz_y_4 = 518;
        int poz_y_5 = 378;
        int poz_y_6 = 167;

        public int Car_1_Speed;
        public int Train_Gora_Dol;
        bool czy_pojazd_przejechal = false;
        Bitmap bitmap1;
        public Form1()
        {
            InitializeComponent();
            Thread trainMove = new Thread(TrainMove);
            trainMove.Start(); // wyjaśnić dlaczego ten wątek nic nie robi póki jedzie auto
            Thread LeftCar_1_Moving = new Thread(CarMoving);
            LeftCar_1_Moving.Start();
        }

        private void TrainMove()
        {
            Random rnd = new Random();
            while (true)
            {
                Train_Gora_Dol = rnd.Next(1, 3);
                czy_pojazd_przejechal = false;
                if (Train_Gora_Dol == 1)
                {
                    while (true)
                    {
                        if (InvokeRequired)
                        {
                            train.Invoke(new Action(delegate ()
                            {
                                //train.Refresh();
                                this.Refresh();
                                train.Location = new Point(poz_pociagu_x, poz_pociagu_y);
                                poz_pociagu_y += 5;

                                Szlaban();
                                if (poz_pociagu_y > 680)
                                {
                                    poz_pociagu_y = 0;
                                    train.Location = new Point(poz_pociagu_x, poz_pociagu_y);
                                    czy_pojazd_przejechal = true;
                                }
                            }));
                        }
                        if (czy_pojazd_przejechal == true)
                            break;
                    }
                }
                else
                {
                    poz_pociagu_y = 690;
                    while (true)
                    {
                        if (InvokeRequired)
                        {
                            train.Invoke(new Action(delegate ()
                            {
                                //train.Refresh();
                                this.Refresh();
                                train.Location = new Point(poz_pociagu_x, poz_pociagu_y);
                                poz_pociagu_y -= 5;

                                Szlaban();
                                if (poz_pociagu_y < 10)
                                {
                                    poz_pociagu_y = 690;
                                    train.Location = new Point(poz_pociagu_x, poz_pociagu_y);
                                    czy_pojazd_przejechal = true;
                                }
                            }));
                        }
                        if (czy_pojazd_przejechal == true)
                            break;
                    }
                }
            }
        }

        void Szlaban()
        {
            if (poz_pociagu_y > 360) // gdy z gory jedzie nie dziala i w ogole dziala 50/50
            {
                szlaban_1.Visible = true;
                szlaban_2.Visible = true;
            }
            else
            {
                szlaban_1.Visible = false;
                szlaban_2.Visible = false;
            }
            szlaban_1.Refresh();
            szlaban_2.Refresh();
        }

        private void CarMoving()
        {
            Random rnd = new Random();
            Car_1_Speed = rnd.Next(4, 8);
            //(804,204), (856,211), (905, 230), (911, 258), (908, 301), (887, 327), (855, 339), (804, 342) - górny skręt
            bitmap1 = (Bitmap)Bitmap.FromFile(@"C:\Users\Alconite\source\repos\SO_Projekt\materials\Samochod.jpg");
            LeftCar_1.SizeMode = PictureBoxSizeMode.Zoom;
            LeftCar_1.Image = bitmap1;
            if (bitmap1 != null)
            {
                bitmap1.RotateFlip(RotateFlipType.Rotate180FlipY);
                LeftCar_1.Image = bitmap1;
            }
            while (true)
            {
                if (InvokeRequired)
                {
                    LeftCar_1.Invoke(new Action(delegate ()
                    {
                        if (LeftCar_1.Location == new Point(12, 204))
                        {// пока едет по верхней полосе
                            while (true)
                            {
                                this.Refresh();
                                LeftCar_1.Location = new Point(poz_car1_x, 204);
                                poz_car1_x += Car_1_Speed;
                                if (LeftCar_1.Location == new Point(804, 204) || LeftCar_1.Location == new Point(805, 204) || LeftCar_1.Location == new Point(806, 204) || 
                                LeftCar_1.Location == new Point(807, 204) || LeftCar_1.Location == new Point(808, 204) || LeftCar_1.Location == new Point(809, 204) || LeftCar_1.Location == new Point(810, 204))
                                {
                                    LeftCar_1.Location = new Point(856, 211);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_1.Location = new Point(905, 230);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_1.Location = new Point(911, 258);
                                    bitmap1.RotateFlip(RotateFlipType.Rotate180FlipY);
                                    LeftCar_1.Image = bitmap1;
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_1.Location = new Point(908, 301);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_1.Location = new Point(887, 327);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_1.Location = new Point(855, 339);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_1.Location = new Point(804, 342);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    break;
                                }
                            }
                        }
                        if (LeftCar_1.Location == new Point(804, 342))
                        {
                            while (true)
                            {
                                this.Refresh();
                                LeftCar_1.Location = new Point(poz_car1_x, 342);
                                poz_car1_x -= Car_1_Speed;
                                if(LeftCar_1.Location == new Point(257, 342) || LeftCar_1.Location == new Point(258, 342) || LeftCar_1.Location == new Point(259, 342) ||
                                LeftCar_1.Location == new Point(260, 342) || LeftCar_1.Location == new Point(261, 342) || LeftCar_1.Location == new Point(262, 342) ||
                                LeftCar_1.Location == new Point(263, 342) || LeftCar_1.Location == new Point(264, 342))
                                {
                                    LeftCar_1.Location = new Point(211, 349);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_1.Location = new Point(168, 362);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_1.Location = new Point(129, 385);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_1.Location = new Point(108, 417);
                                    bitmap1.RotateFlip(RotateFlipType.Rotate180FlipY);
                                    LeftCar_1.Image = bitmap1;
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_1.Location = new Point(102, 459);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_1.Location = new Point(117, 491);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_1.Location = new Point(134, 514);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_1.Location = new Point(163, 535);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_1.Location = new Point(247, 555);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    break;
                                }
                            }
                        }
                        if (LeftCar_1.Location == new Point(247, 555))
                        {
                        while (true)
                        {
                            this.Refresh();
                            LeftCar_1.Location = new Point(poz_car1_x, 555);
                            poz_car1_x += Car_1_Speed;
                            if (LeftCar_1.Location == new Point(1150, 555) || LeftCar_1.Location == new Point(1151, 555) || LeftCar_1.Location == new Point(1152, 555) || LeftCar_1.Location == new Point(1153, 555) ||
                                LeftCar_1.Location == new Point(1154, 555) || LeftCar_1.Location == new Point(1155, 555) || LeftCar_1.Location == new Point(1156, 555) || LeftCar_1.Location == new Point(1157, 555) ||
                                LeftCar_1.Location == new Point(1158, 555))
                                    break;                                    
                            }
                        }
                    }));
                }
            }
        }


    }
}
