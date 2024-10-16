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
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GeneticAlgorithmWF
{
    public partial class Form1 : Form
    {
        private Population population;
        private Func<int, double> func = x => { return 39 - 96 * x - 67 * (x * x) + 4 * (x * x * x); };
        private int genNum;
        private bool isRunning = false;
        private Thread thread;

        public Form1()
        {
            InitializeComponent();
            Start();
        }

        public void Start()
        {
            DrawChart(func);
            // Создание искомого решения для тестирования алгоритма
            FitnessCalc.setSolution(func);
        }

        private void MakeNextGen()
        {
            genNum++;
            Algorithm.evolvePopulation(population);
        }

        private void DrawChart(Func<int, double> func) 
        {
            chart1.ChartAreas.Add("Area");
            chart1.ChartAreas["Area"].AxisX.Minimum = -10;
            chart1.ChartAreas["Area"].AxisX.Maximum = 53;
            chart1.ChartAreas["Area"].AxisX.Interval = 1;
            chart1.Series.Add("Line");
            chart1.Series["Line"].Color = Color.Blue;
            //chart1.Series["Line"].
            //chart1.Series["Line"].Points.Add(new DataPoint(3, 3));
            chart1.Series["Line"].ChartType = SeriesChartType.Line;

            chart1.Series.Add("Point");
            chart1.Series["Point"].Color = Color.Red;
            chart1.Series["Point"].MarkerStyle = MarkerStyle.Circle;
            chart1.Series["Point"].MarkerSize = 12;
            chart1.Series["Point"].ChartType = SeriesChartType.FastPoint;

            for (int i = -10; i <= 53; i++)
            {
                chart1.Series["Line"].Points.Add(new DataPoint(i, func(i)));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                isRunning = true;
                button1.Text = "Закончить поиск";
                genNum = 1;
                richTextBox1.Text = "========= Начало поиска==========\n";
                population = new Population(4, true, radioButton1.Checked);

                thread = new Thread(FindSolution);
                thread.Start();
            }
            else
            {
                isRunning = false;
                button1.Text = "Начать поиск";
            }
        }

        private void FindSolution()
        {
            while (isRunning)
            {
                DrawPopulation();
                WriteLogs();
                MakeNextGen();
                Thread.Sleep(200);
            }
        }

        private void WriteLogs()
        {
            richTextBox1.Invoke(new Action<int>((genNum) => {
                richTextBox1.Text +=
                $"Поколение {genNum}\n";
            }), genNum);
            for (int i = 0; i < population.Size; i++)
            {
                var indiv = population.Individuals[i];
                richTextBox1.Invoke(new Action<Individual>((individual) => {
                    richTextBox1.Text +=
                    $"  Особь {i+1}: \n" +
                    $"      Адаптивность: {individual.getFitness()} \n" +
                    $"      Фенотип: {individual.Phenotype} \n";
                }), indiv);
            }
            richTextBox1.Invoke(new Action(() => {
                richTextBox1.Text += "\n";
            }));
        }

        private void DrawPopulation()
        {
            for (int i = 0; i < chart1.Series["Point"].Points.Count; i++)
            {
                chart1.Invoke(new Action(() => { chart1.Series["Point"].Points.RemoveAt(i); }));
            }


            for (int i = 0; i < population.Size; i++)
            {
                chart1.Invoke(new Action(() => {
                    chart1.Series["Point"].Points.Add(new DataPoint(
                        population.Individuals[i].Phenotype,
                        func(population.Individuals[i].Phenotype))); }
                ));
            }
        }

        private void ClosingHandler(object sender, FormClosingEventArgs e)
        {
            isRunning = false;
            thread.Join();
        }
    }
}
