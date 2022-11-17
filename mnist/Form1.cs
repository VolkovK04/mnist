using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using mnist.VectorMath;

namespace mnist
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadDataFromFile("C:\\Users\\volkov\\Downloads\\mnist_train.csv");
            myNeurons = new Perceptron();
            myNeurons.RandomFill();
        }
        Perceptron myNeurons;

        Bitmap img;
        Graphics g;
        bool IsDraw = false;
        const int IMAGE_SIZE = 28;
        const int OUTPUT_SIZE = 10;
        List<Test> Tests = new List<Test>();


        private void Form1_Load(object sender, EventArgs e)
        {
            img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(img);
        }
                
        private void LoadDataFromFile(string filename)
        {
            StreamReader sr = new StreamReader(filename, Encoding.Default);
            while (!sr.EndOfStream)
            {
                string[] s = sr.ReadLine().Split(',');
                double[] input = new double[IMAGE_SIZE * IMAGE_SIZE];
                for (int i = 0; i < IMAGE_SIZE; i++)
                    for (int j = 0; j < IMAGE_SIZE; j++)
                        input[IMAGE_SIZE * j + i] = Convert.ToByte(s[IMAGE_SIZE * i + j + 1]) / 255.0;
                double[] expected = new double[OUTPUT_SIZE];
                expected[Convert.ToByte(s[0])] = 1;
                Tests.Add(new Test(new Vector(input), new Vector(expected)));
            }
            sr.Close();
        }
        private void DrawNumber(Test test)
        {
            double[] input = test.Input.ToArray();
            for (int i = 0; i < IMAGE_SIZE; i++)
            {
                for (int j = 0; j < IMAGE_SIZE; j++)
                {
                    byte buff = (byte)(input[IMAGE_SIZE * i + j] * 255);
                    Color color = Color.FromArgb(buff, buff, buff);
                    g.FillRectangle(new SolidBrush(color), 10 * i, 10 * j, 10, 10);
                }
            }
            int result = 0;
            double[] expected = test.Expected.ToArray();
            for (int i = 0; i < OUTPUT_SIZE; i++)
            {
                if (expected[i] == 1)
                {
                    result= i;
                    break;
                }
            }
            label1.Text = result.ToString();
            pictureBox1.Image = img;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter("log.txt", false, Encoding.Default);
            for (int i=0; i<Tests.Count; i++)
            {
                Vector output = myNeurons.Forward(Tests[i].Input);
                myNeurons.Backward(Tests[i].Expected);
                double error = (output - Tests[i].Expected).GetEuclideanLength();
                sw.WriteLine($"{i};{error}");
            }
            sw.Close();
        }
        int k = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            DrawNumber(Tests[k]);
            Vector output = myNeurons.Forward(Tests[k].Input);
            double error = (output - Tests[k].Expected).GetEuclideanLength();

            label2.Text = error.ToString();
            k++;
        }
    }
}
