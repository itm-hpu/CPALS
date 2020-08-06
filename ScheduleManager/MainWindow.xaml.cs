using System;
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
using Accord.Statistics.Distributions.Univariate;

namespace ScheduleManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Create(Add) Standard Logistics Task
        /// </summary>
        class StandardTask
        {
            public int TaskID { get; set; }
            public double DeliveryTime { get; set; }
            public string FromLocation { get; set; }
            public string ToLocation { get; set; }
            public double Deviation { get; set; }
        }

        List<StandardTask> standardTasks = new List<StandardTask>();
        
        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.StandardTask standardTask = new MainWindow.StandardTask();

            standardTask.TaskID = standardTasks.Count() + 1;
            standardTask.DeliveryTime = Convert.ToDouble(txtDeliveryTime.Text);
            standardTask.FromLocation = txtFromLocation.Text;
            standardTask.ToLocation = txtToLocation.Text;
            standardTask.Deviation = Convert.ToDouble(txtDeviation.Text);

            standardTasks.Add(standardTask);

            string tempResult = "TaskID: " + standardTask.TaskID.ToString() + ", " +
                "DeliveryTime: " + standardTask.DeliveryTime.ToString() + ", " +
                "FromLocation: " + standardTask.FromLocation + ", " +
                "ToLocation: " + standardTask.ToLocation + ", " +
                "Deviation: " + standardTask.Deviation;

            txtStandardTasks.Text = txtStandardTasks.Text + tempResult + "\r\n";

            txtDeliveryTime.Text = string.Empty;
            txtFromLocation.Text = string.Empty;
            txtToLocation.Text = string.Empty;
            txtDeviation.Text = string.Empty;

            txtStandardTasks.ScrollToEnd();
        }

        /// <summary>
        /// Create Logistics Schedule
        /// </summary>
        class TaskSchedule
        {
            public int TaskID { get; set; }
            public double PickTime { get; set; }
            public double ConsTime { get; set; }
        }

        List<TaskSchedule> taskSchedules = new List<TaskSchedule>();

        public double GetPickTime(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        private void BtnCreateSchedule_Click(object sender, RoutedEventArgs e)
        {
            int numofstandtasks = standardTasks.Count();
            int totalnumoftasks = Convert.ToInt32(txtTotalNumTasks.Text);
            int timelength = Convert.ToInt32(txtTimeLength.Text);

            Random rand = new Random();

            double[] portion = new double[numofstandtasks];
            for (int i = 0; i < numofstandtasks; i++)
            {
                portion[i] = rand.NextDouble();
            }

            int[] eachnumofstandtasks = new int[numofstandtasks];
            int sum = 0;
            for (int i = 0; i < numofstandtasks; i++)
            {
                if (i == numofstandtasks)
                {
                    eachnumofstandtasks[i] = totalnumoftasks - sum;
                }
                else
                {
                    eachnumofstandtasks[i] = (int)Math.Ceiling(totalnumoftasks * (portion[i] / portion.Sum()));
                }

                sum = sum + eachnumofstandtasks[i];
            }

            for (int i = 0; i < numofstandtasks; i++)
            {
                for (int j = 0; j < eachnumofstandtasks[i]; j++)
                {
                    MainWindow.TaskSchedule taskSchedule = new MainWindow.TaskSchedule();

                    taskSchedule.TaskID = standardTasks[i].TaskID;
                    taskSchedule.PickTime = Math.Round(rand.NextDouble() * timelength * 1000) / 1000;
                    taskSchedule.ConsTime = Math.Round(NormalDistribution.Random(standardTasks[i].DeliveryTime, standardTasks[i].Deviation) * 1000) / 1000 + taskSchedule.PickTime;

                    while (taskSchedule.ConsTime > timelength)
                    {
                        taskSchedule.PickTime = Math.Round(rand.NextDouble() * timelength * 1000) / 1000;
                        taskSchedule.ConsTime = Math.Round(NormalDistribution.Random(standardTasks[i].DeliveryTime, standardTasks[i].Deviation) * 1000) / 1000 + taskSchedule.PickTime;
                    }

                    taskSchedules.Add(taskSchedule);
                }
            }

            for (int i = 0; i < totalnumoftasks; i++)
            {
                string tempResult = "Order: # " + (i + 1).ToString() + ", " +
                    "TaskID: " + taskSchedules[i].TaskID.ToString() + ", " +
                    "PickTime: " + taskSchedules[i].PickTime.ToString() + ", " +
                    "ConsTime: " + taskSchedules[i].ConsTime.ToString();

                txtTaskSchedule.Text = txtTaskSchedule.Text + tempResult + "\r\n";
                txtTaskSchedule.ScrollToEnd();
            }
            
        }
    }
}
