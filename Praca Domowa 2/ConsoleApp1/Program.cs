using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Bitmap bt = new Bitmap(@"..\\..\\shrek.jpg");
            int wdt = 160;

            for (int y = 0; y < bt.Height; y++)
            {
                for (int x = 0; x < bt.Width; x++)
                {
                    int tem = x%wdt;
                    if (tem >0 && tem <wdt/2)
                    {
                        
                        Color c = bt.GetPixel(x, y);

                        int r = c.R;
                        int g = c.G;
                        int b = c.B;
                        int avg = (r + g + b) / 3;
                        bt.SetPixel(x, y, Color.FromArgb(avg, avg, avg));
                    }

                }
            }
            bt.Save(@"..\\..\\shrek-out.bmp");

        }

        


    }
}
