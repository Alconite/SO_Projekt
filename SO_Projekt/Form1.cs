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
        public Form1()
        {
            InitializeComponent();
            Thread trainMove = new Thread(TrainMove);
            trainMove.Start();
        }

        private void TrainMove()
        {
            Thread.Sleep(1000);
            while (true)
            {
                if (InvokeRequired)
                {
                    train.Invoke(new Action(delegate ()
                    {
                        train.Image = null;
                        train.Refresh();

                        train.Location = new Point(poz_pociagu_x, poz_pociagu_y);
                        Thread.Sleep(10);
                        poz_pociagu_y++;

                        Szlaban();
                        if (poz_pociagu_y == 680)
                        {
                            poz_pociagu_y = 0;
                            train.Location = new Point(poz_pociagu_x, poz_pociagu_y);
                        }
                    }));
                    Thread.Sleep(5);
                }
            }
        }

        void Szlaban()
        {
            szlaban_1.Image = null;
            szlaban_2.Image = null;
            if (poz_pociagu_y >= 300)
            {
                szlaban_1.Visible = true;
                szlaban_2.Visible = true;
            }
            else
            {
                szlaban_1.Visible = false;
                szlaban_2.Visible = false;
            }
        }

      

    }
}
