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
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        int poz_pociagu_x = 1044;
        int poz_pociagu_y = 0;
        int poz_car1_x = 12;
        int poz_car2_x = 12;
        int poz_car4_x = 1166;
        int poz_car5_x = 1166;
        int poz_y_1 = 204;
        int poz_y_2 = 204;
        int poz_y_4 = 519;
        int poz_y_5 = 519;

        public int Car_1_Speed;
        public int Car_2_Speed;
        public int Car_3_Speed;
        public int Car_4_Speed;
        public int Car_5_Speed;
        public int Car_6_Speed;

        public bool PrzestawKierunekRuchu1 = false; // true = w lewo    false = w prawo
        public bool PrzestawKierunekRuchu2 = false; // true = w lewo    false = w prawo
        public bool PrzestawKierunekRuchu3 = true; // true = w lewo    false = w prawo
        public bool PrzestawKierunekRuchu4 = true; // true = w lewo    false = w prawo

        public bool Car_1_Top = true;
        public bool Car_1_Middle = false;
        public bool Car_1_Bottom = false;

        public bool Car_2_Top = true;
        public bool Car_2_Middle = false;
        public bool Car_2_Bottom = false;

        public bool Car_3_Top = false;
        public bool Car_3_Middle = false;
        public bool Car_3_Bottom = true;

        public bool Car_4_Top = false;
        public bool Car_4_Middle = false;
        public bool Car_4_Bottom = true;

        public int Train_Gora_Dol;
        bool czy_pojazd_przejechal = true;

        Bitmap bitmap1 = (Bitmap) Bitmap.FromFile(@"C:\Users\Alconite\source\repos\SO_Projekt\materials\Samochod.jpg");
        Bitmap bitmap2 = (Bitmap)Bitmap.FromFile(@"C:\Users\Alconite\source\repos\SO_Projekt\materials\Samochod.jpg");
        Bitmap bitmap4 = (Bitmap)Bitmap.FromFile(@"C:\Users\Alconite\source\repos\SO_Projekt\materials\Samochod.jpg");
        Bitmap bitmap5 = (Bitmap)Bitmap.FromFile(@"C:\Users\Alconite\source\repos\SO_Projekt\materials\Samochod.jpg");


        public Form1()
        {
            InitializeComponent();
            Thread trainMove = new Thread(FromWhereTrain);
            trainMove.Start(); // wyjaśnić dlaczego ten wątek nic nie robi póki jedzie auto
            WatekStart();
        }

        void WatekStart()
        {
            Thread LeftCar_1_Moving = new Thread(First_Car);
            Thread LeftCar_2_Moving = new Thread(Second_Car);
            Thread RightCar_1_Moving = new Thread(Fourth_Car);
            Thread RightCar_2_Moving = new Thread(Fifth_Car);
            LeftCar_1_Moving.Start();
            LeftCar_2_Moving.Start();
            RightCar_1_Moving.Start();
            RightCar_2_Moving.Start();
        }

        private void TrainMove()
        {
            if (Train_Gora_Dol == 1)
            {
                while (true)
                {
                    if (InvokeRequired)
                    {
                        train.Invoke(new Action(delegate ()
                        {
                            train.Refresh();
                            //this.Refresh();
                            train.Location = new Point(poz_pociagu_x, poz_pociagu_y);
                            poz_pociagu_y++;

                            Szlaban();
                            if (poz_pociagu_y > 680)
                            {
                                poz_pociagu_y = 0;
                                train.Location = new Point(poz_pociagu_x, poz_pociagu_y);
                                czy_pojazd_przejechal = true;
                            }
                        }));
                        Thread.Sleep(5);
                    }
                    if (czy_pojazd_przejechal == true)
                    {
                        FromWhereTrain();
                        break;
                    }
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
                            train.Refresh();
                            //this.Refresh();
                            train.Location = new Point(poz_pociagu_x, poz_pociagu_y);
                            poz_pociagu_y--;

                            Szlaban();
                            if (poz_pociagu_y < 15)
                            {
                                poz_pociagu_y = 690;
                                train.Location = new Point(poz_pociagu_x, poz_pociagu_y);
                                //czy_pojazd_przejechal = true;
                            }
                        }));
                        Thread.Sleep(5);
                    }
                    if (czy_pojazd_przejechal == true)
                    {
                        FromWhereTrain();
                        break;
                    }
                }
            }
        }

        void FromWhereTrain()
        {
            Random rnd = new Random();
            Train_Gora_Dol = rnd.Next(1, 3);
            czy_pojazd_przejechal = false;
            TrainMove();
        }

        void Szlaban()
        {
            szlaban_1.Image = null;
            szlaban_2.Image = null;
            if (poz_pociagu_y > 360)
            {
                szlaban_1.Visible = true;
                szlaban_2.Visible = true;
            }
            else
            {
                szlaban_1.Visible = false;
                szlaban_2.Visible = false;
            }
            //szlaban_1.Refresh();
            //szlaban_2.Refresh();
        }

        private void First_Car()
        {
            PredkoscPojazdu();
            while (true)
            {
                if (InvokeRequired)
                {
                    LeftCar_1.Invoke(new Action(delegate ()
                    {
                        LeftCar_1.Refresh();
                        if (!czy_pojazd_przejechal)
                        {
                            ZatrzymajAuto();
                        }
                        else
                        {
                            LeftCar_1.Location = new Point(poz_car1_x, poz_y_1);
                            ZwolnijPredkosc();
                            if (PrzestawKierunekRuchu1 == false)
                                poz_car1_x++;
                            else
                                poz_car1_x--;
                            if (poz_car1_x == 800 && poz_y_1 == 204)
                            {
                                Thread povorot_1 = new Thread(KiedySkret1_1);
                                povorot_1.Start();
                                bitmap1.RotateFlip(RotateFlipType.Rotate180FlipY);
                                LeftCar_1.Image = bitmap1;
                            }
                            if (poz_car1_x == 260 && poz_y_1 == 342)
                            {
                                Thread povorot_1_1 = new Thread(KiedySkret1_1);
                                povorot_1_1.Start();
                                bitmap1.RotateFlip(RotateFlipType.Rotate180FlipY);
                                LeftCar_1.Image = bitmap1;
                            }
                        }
                    }));
                    Thread.Sleep(Car_1_Speed);
                }
            }
        }

        private void Second_Car()
        {
            PredkoscPojazdu();
            while (true)
            {
                if (InvokeRequired)
                {
                    LeftCar_2.Invoke(new Action(delegate ()
                    {
                        LeftCar_2.Refresh();
                        if (!czy_pojazd_przejechal)
                        {
                            ZatrzymajAuto();
                        }
                        else
                        {
                            LeftCar_2.Location = new Point(poz_car2_x, poz_y_2);
                            ZwolnijPredkosc();
                            if (PrzestawKierunekRuchu2 == false)
                                poz_car2_x++;
                            else
                                poz_car2_x--;
                            if (poz_car2_x == 800 && poz_y_2 == 204)
                            {
                                Thread povorot_1 = new Thread(KiedySkret1_2);
                                povorot_1.Start();
                                bitmap2.RotateFlip(RotateFlipType.Rotate180FlipY);
                                LeftCar_2.Image = bitmap2;
                            }
                            if (poz_car2_x == 260 && poz_y_2 == 342)
                            {
                                Thread povorot_1_1 = new Thread(KiedySkret1_1);
                                povorot_1_1.Start();
                                bitmap2.RotateFlip(RotateFlipType.Rotate180FlipY);
                                LeftCar_2.Image = bitmap2;
                            }
                        }
                    }));
                    Thread.Sleep(Car_2_Speed);
                }
            }
        }

        private void Fourth_Car()
        {
            PredkoscPojazdu();
            while (true)
            {
                if (InvokeRequired)
                {
                    Right_Car_1.Invoke(new Action(delegate ()
                    {
                        Right_Car_1.Refresh();
                        if (!czy_pojazd_przejechal)
                        {
                            ZatrzymajAuto();
                        }
                        else
                        {
                            Right_Car_1.Location = new Point(poz_car4_x, poz_y_4);
                            ZwolnijPredkosc();
                            if (PrzestawKierunekRuchu3 == false)
                                poz_car4_x++;
                            else
                                poz_car4_x--;
                            if (poz_car4_x == 255 && poz_y_4 == 519) ////////
                            {
                                Thread povorot_2 = new Thread(KiedySkret2_1);
                                povorot_2.Start();
                                bitmap4.RotateFlip(RotateFlipType.Rotate180FlipY);
                                Right_Car_1.Image = bitmap4;
                            }
                            if (poz_car2_x == 812 && poz_y_2 == 379)
                            {
                                Thread povorot_2_1 = new Thread(KiedySkret2_2);
                                povorot_2_1.Start();
                                bitmap4.RotateFlip(RotateFlipType.Rotate180FlipY);
                                Right_Car_1.Image = bitmap4;
                            }
                        }
                    }));
                    Thread.Sleep(Car_4_Speed);
                }
            }
        }

        private void Fifth_Car()
        {
            PredkoscPojazdu();
            while (true)
            {
                if (InvokeRequired)
                {
                    Right_Car_2.Invoke(new Action(delegate ()
                    {
                        Right_Car_2.Refresh();
                        if (!czy_pojazd_przejechal)
                        {
                            ZatrzymajAuto();
                        }
                        else
                        {
                            Right_Car_2.Location = new Point(poz_car5_x, poz_y_5);
                            ZwolnijPredkosc();
                            if (PrzestawKierunekRuchu4 == false)
                                poz_car4_x++;
                            else
                                poz_car4_x--;
                            if (poz_car4_x == 255 && poz_y_4 == 519)
                            {
                                Thread povorot_5 = new Thread(KiedySkret2_1);
                                povorot_5.Start();
                                bitmap5.RotateFlip(RotateFlipType.Rotate180FlipY);
                                Right_Car_2.Image = bitmap5;
                            }
                            if (poz_car5_x == 812 && poz_y_5 == 379)
                            {
                                Thread povorot_5_1 = new Thread(KiedySkret2_2);
                                povorot_5_1.Start();
                                bitmap5.RotateFlip(RotateFlipType.Rotate180FlipY);
                                Right_Car_2.Image = bitmap5;
                            }
                        }
                    }));
                    Thread.Sleep(Car_5_Speed);
                }
            }
        }

        void ZatrzymajAuto()
        {
            return;
        }

        void PredkoscPojazdu()
        {
            Random rd = new Random();
            Car_1_Speed = rd.Next(5, 10);
            Car_2_Speed = rd.Next(5, 10);
            Car_3_Speed = rd.Next(5, 10);
            Car_4_Speed = rd.Next(5, 10);
            Car_5_Speed = rd.Next(5, 10);
            Car_6_Speed = rd.Next(5, 10);
        }

        void ZwolnijPredkosc()
        {
            if ((poz_car1_x - poz_car2_x < 70) || (poz_car1_x - poz_car2_x < -70))
                Car_2_Speed = Car_1_Speed;
            if ((poz_car4_x - poz_car5_x < 70) || (poz_car4_x - poz_car5_x < -70))
                Car_5_Speed = Car_4_Speed;
        }

        void KiedySkret1_1()
        {
            for (int i = 0; i < 138; i++)
            {
                Thread.Sleep(5);
                poz_y_1++;
            }
            if (PrzestawKierunekRuchu1 == false)
                PrzestawKierunekRuchu1 = true;
            else
                PrzestawKierunekRuchu1 = false;
        }

        void KiedySkret1_2()
        {
            for (int i = 0; i < 213; i++)
            {
                Thread.Sleep(5);
                poz_y_2++;
            }
            if (PrzestawKierunekRuchu2 == false)
                PrzestawKierunekRuchu2 = true;
            else
                PrzestawKierunekRuchu2 = false;
        }

        void KiedySkret2_1()
        {
            for (int i = 0; i < 140; i--)
            {
                Thread.Sleep(5);
                poz_y_4--;
            }
            if (PrzestawKierunekRuchu3 == true)
                PrzestawKierunekRuchu3 = false;
            else
                PrzestawKierunekRuchu3 = true;
        }

        void KiedySkret2_2()
        {
            for (int i = 0; i < 211; i++)
            {
                Thread.Sleep(5);
                poz_y_5--;
            }
            if (PrzestawKierunekRuchu4 == true)
                PrzestawKierunekRuchu4 = false;
            else
                PrzestawKierunekRuchu4 = true;
        }


        /*private void First_Car()
        {
            Random rnd = new Random();
            Car_1_Speed = rnd.Next(4, 8);
            //(804,204), (856,211), (905, 230), (911, 258), (908, 301), (887, 327), (855, 339), (804, 342) - górny skręt
            //bitmap1 = (Bitmap)Bitmap.FromFile(@"C:\Users\Alconite\source\repos\SO_Projekt\materials\Samochod.jpg");
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
        }*/

        /*private void Second_Car()
        {
            Thread.Sleep(3000);
            Random rnd = new Random();
            Car_2_Speed = rnd.Next(4, 8);
            //(804,204), (856,211), (905, 230), (911, 258), (908, 301), (887, 327), (855, 339), (804, 342) - górny skręt
            bitmap2 = (Bitmap)Bitmap.FromFile(@"C:\Users\Alconite\source\repos\SO_Projekt\materials\Samochod.jpg");
            LeftCar_2.SizeMode = PictureBoxSizeMode.Zoom;
            LeftCar_2.Image = bitmap2;
            if (bitmap2 != null)
            {
                bitmap2.RotateFlip(RotateFlipType.Rotate180FlipY);
                LeftCar_2.Image = bitmap2;
            }
            while (true)
            {
                if (InvokeRequired)
                {
                    LeftCar_2.Invoke(new Action(delegate ()
                    {
                        if (LeftCar_2.Location == new Point(12, 204))
                        {// пока едет по верхней полосе
                            while (true)
                            {
                                this.Refresh();
                                LeftCar_2.Location = new Point(poz_car2_x, 204);
                                poz_car2_x += Car_2_Speed;
                                if (LeftCar_2.Location == new Point(804, 204) || LeftCar_2.Location == new Point(805, 204) || LeftCar_2.Location == new Point(806, 204) ||
                                LeftCar_2.Location == new Point(807, 204) || LeftCar_2.Location == new Point(808, 204) || LeftCar_2.Location == new Point(809, 204) || LeftCar_2.Location == new Point(810, 204))
                                {
                                    LeftCar_2.Location = new Point(856, 211);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_2.Location = new Point(905, 230);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_2.Location = new Point(911, 258);
                                    bitmap2.RotateFlip(RotateFlipType.Rotate180FlipY);
                                    LeftCar_2.Image = bitmap1;
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_2.Location = new Point(908, 301);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_2.Location = new Point(887, 327);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_2.Location = new Point(855, 339);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_2.Location = new Point(804, 342);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    break;
                                }
                            }
                        }
                        if (LeftCar_2.Location == new Point(804, 342))
                        {
                            while (true)
                            {
                                this.Refresh();
                                LeftCar_2.Location = new Point(poz_car2_x, 342);
                                poz_car2_x -= Car_2_Speed;
                                if (LeftCar_2.Location == new Point(257, 342) || LeftCar_2.Location == new Point(258, 342) || LeftCar_2.Location == new Point(259, 342) ||
                                LeftCar_2.Location == new Point(260, 342) || LeftCar_2.Location == new Point(261, 342) || LeftCar_2.Location == new Point(262, 342) ||
                                LeftCar_2.Location == new Point(263, 342) || LeftCar_2.Location == new Point(264, 342))
                                {
                                    LeftCar_2.Location = new Point(211, 349);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_2.Location = new Point(168, 362);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_2.Location = new Point(129, 385);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_2.Location = new Point(108, 417);
                                    bitmap2.RotateFlip(RotateFlipType.Rotate180FlipY);
                                    LeftCar_2.Image = bitmap2;
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_2.Location = new Point(102, 459);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_2.Location = new Point(117, 491);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_2.Location = new Point(134, 514);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_2.Location = new Point(163, 535);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    LeftCar_2.Location = new Point(247, 555);
                                    this.Refresh();
                                    Thread.Sleep(300);
                                    break;
                                }
                            }
                        }
                        if (LeftCar_2.Location == new Point(247, 555))
                        {
                            while (true)
                            {
                                this.Refresh();
                                LeftCar_2.Location = new Point(poz_car2_x, 555);
                                poz_car2_x += Car_2_Speed;
                                if (LeftCar_2.Location == new Point(1150, 555) || LeftCar_2.Location == new Point(1151, 555) || LeftCar_2.Location == new Point(1152, 555) || LeftCar_2.Location == new Point(1153, 555) ||
                                    LeftCar_2.Location == new Point(1154, 555) || LeftCar_2.Location == new Point(1155, 555) || LeftCar_2.Location == new Point(1156, 555) || LeftCar_2.Location == new Point(1157, 555) ||
                                    LeftCar_2.Location == new Point(1158, 555))
                                    break;
                            }
                        }
                    }));
                }
            }
        }*/
        /*private void Third_Car()
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
                                if (LeftCar_1.Location == new Point(257, 342) || LeftCar_1.Location == new Point(258, 342) || LeftCar_1.Location == new Point(259, 342) ||
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
        }*/
    }
}
