using System;
using System.IO;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;


namespace CarGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        const int CAR_HEIGHT = 120;
        const int CAR_WIDTH = 60;
        const int SPEED = 10;
        const int NUM_OF_COINS = 8;
        const int SIZE_COIN = 15;
        int score = 0;

        public DispatcherTimer dt = new DispatcherTimer();
        bool gameOver = false;
        Random rnd = new Random();
        List<Rectangle> coinsList;

        public MainWindow()
        {
            InitializeComponent();
            coinsList = initializeCoins();
            dt.Tick += work;
            dt.Interval = TimeSpan.FromMilliseconds(SPEED);
            dt.Start();
        }

        private List<Rectangle> initializeCoins()
        {
            Image coinIm = new Image();
            coinIm.Source = new BitmapImage(new Uri("pack://application:,,,/images/coin.png"));
            List<Rectangle> coinsList = new List<Rectangle>();
            for (int i = 0; i < NUM_OF_COINS; i++)
            {
                double x = rnd.Next(15, 560);
                double y = rnd.Next(20, 640);

                Rectangle rec = new Rectangle();
                rec.Width = SIZE_COIN;
                rec.Height = SIZE_COIN;
                rec.Fill = new ImageBrush(coinIm.Source);
                coinsList.Add(rec);
                myCanvas.Children.Add(rec);
                Canvas.SetTop(rec, y);
                Canvas.SetLeft(rec, x);
            }

            return coinsList;
        }

        private void work(object sender, EventArgs e)
        {

            if(isCrashingCar())
            {
                gameover();
            }
                       
            moveElements();            
        }



        private void moveElements()
        {
            Canvas.SetTop(en1, Canvas.GetTop(en1) + 10);
            Canvas.SetTop(en2, Canvas.GetTop(en2) + 10);
            Canvas.SetTop(en3, Canvas.GetTop(en3) + 10);

            //moving road
            if (Canvas.GetTop(r1) > 850)
            {
                Canvas.SetTop(r1, -100);
            }
            else
            {
                Canvas.SetTop(r1, Canvas.GetTop(r1) + 10);
            }

            if (Canvas.GetTop(r2) > 850)
            {
                Canvas.SetTop(r2, -100);
            }
            else
            {
                Canvas.SetTop(r2, Canvas.GetTop(r2) + 10);
            }

            if (Canvas.GetTop(r3) > 850)
            {
                Canvas.SetTop(r3, -100);
            }
            else
            {
                Canvas.SetTop(r3, Canvas.GetTop(r3) + 10);
            }

            if (Canvas.GetTop(r4) > 850)
            {
                Canvas.SetTop(r4, -100);
            }
            else
            {
                Canvas.SetTop(r4, Canvas.GetTop(r4) + 10);
            }
          
            //moving cars
            if (Canvas.GetTop(en1) > 850)
            {
                int p = rnd.Next(35, 180);
                Canvas.SetLeft(en1, p);
                Canvas.SetTop(en1, -100);
            }

            if (Canvas.GetTop(en2) > 850)
            {
                int p = rnd.Next(220, 365);
                Canvas.SetLeft(en2, p);
                Canvas.SetTop(en2, -100);
            }

            if (Canvas.GetTop(en3) > 850)
            {
                int p = rnd.Next(405, 550);
                Canvas.SetLeft(en3, p);
                Canvas.SetTop(en3, -100);
            }


            //moving coins
            foreach(var it in coinsList)
            {
                Rect coin = new Rect(Canvas.GetLeft(it), Canvas.GetTop(it), SIZE_COIN, SIZE_COIN);
                Rect player = new Rect(Canvas.GetLeft(car), Canvas.GetTop(car), CAR_WIDTH, CAR_HEIGHT);
                if(player.IntersectsWith(coin))
                {
                    score ++;
                    scoreLabel.Content = "SCORE: " + score;
                    Canvas.SetTop(it, 0);
                    Canvas.SetLeft(it, rnd.Next(15, 560));
                }

                if(Canvas.GetTop(it)>800)
                {
                    Canvas.SetTop(it, 0);
                    Canvas.SetLeft(it, rnd.Next(15, 560));
                }
                else
                {
                    Canvas.SetTop(it, Canvas.GetTop(it) + 10);
                }
            }
            
        }

        private bool isCrashingCar()
        {
            
            Rect c1 = new Rect(Canvas.GetLeft(en1),Canvas.GetTop(en1), CAR_WIDTH-15,CAR_HEIGHT-15);
            Rect c2 = new Rect(Canvas.GetLeft(en2), Canvas.GetTop(en2), CAR_WIDTH - 15, CAR_HEIGHT - 15);
            Rect c3 = new Rect(Canvas.GetLeft(en3), Canvas.GetTop(en3), CAR_WIDTH - 15, CAR_HEIGHT - 15);
            

            Rect player= new Rect(Canvas.GetLeft(car), Canvas.GetTop(car), CAR_WIDTH - 15, CAR_HEIGHT - 15);

            if (player.IntersectsWith(c1) || player.IntersectsWith(c2) || player.IntersectsWith(c3))
                return true;

            return false;
        }

        private void gameover()
        {
            gameOver = true;
            dt.Stop();
        }

  

        private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!gameOver)
            {

                Point pos = e.GetPosition(myCanvas);
                if(pos.Y<780&&pos.Y>20)
                    Canvas.SetTop(car, pos.Y);
                if(pos.X>10&&pos.X<570)
                    Canvas.SetLeft(car, pos.X);
            }
          
        }
    }
}
