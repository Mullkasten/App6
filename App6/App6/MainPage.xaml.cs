using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using System.Collections.Generic;
using Emgu.Util;
using Emgu.CV;
using Emgu.CV.CvEnum;
using System.IO;
using Xamarin.Essentials;

namespace App6
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        public void Btn1(object sender, EventArgs e)
        {
            String win1 = "Test Window (Press any key to close)"; //The name of the window
            CvInvoke.NamedWindow(win1); //Create the window using the specific name
            Mat frame = new Mat();
            //Mat crope = new Mat();
            VideoCapture capture = new VideoCapture();
            int i = 0, k = 0, pulse = 0;
            int a = 0, b = 0, c = 0;
            using (StreamWriter w = new StreamWriter("test.txt"))
            {
                while ((CvInvoke.WaitKey(1) == -1))
                {
                    capture.Read(frame);
                    Mat crope = new Mat(frame, new Emgu.CV.Structure.Range(180, 400), new Emgu.CV.Structure.Range(300, 540));
                    CvInvoke.CvtColor(crope, crope, ColorConversion.Bgr2Gray);
                    //Console.WriteLine(Average(crope));
                    CvInvoke.Imshow(win1, crope);
                    a = b;
                    b = c;
                    c = Average(crope);
                    if ((a != c) && (a == b))
                        pulse++;
                    i++;
                    if (i == 30)
                    {
                        k = pulse * 60;
                        pulse = 0;
                        i = 0;
                    }
                }
            }
            Console.WriteLine(k);
            static int Average(Mat frame)
            {
                byte[,] array;
                array = (byte[,])frame.GetData();
                int sum = 0;
                foreach (byte num in array)
                {
                    sum += num;
                }
                return sum / array.Length;
            }
        }
    }
}
