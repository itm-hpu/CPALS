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

        class LogistcisTask
        {
            public int TaskID { get; set; }
            public double DeliveryTime { get; set; }
            public string FromLocation { get; set; }
            public string ToLocation { get; set; }
            public double Deviation { get; set; }
        }

        List<LogistcisTask> logisticsTasks = new List<LogistcisTask>();
        
        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.LogistcisTask logisticstask = new MainWindow.LogistcisTask();
            
            logisticstask.TaskID = logisticsTasks.Count() + 1;
            logisticstask.DeliveryTime = Convert.ToDouble(txtDeliveryTime.Text);
            logisticstask.FromLocation = txtFromLocation.Text;
            logisticstask.ToLocation = txtToLocation.Text;
            logisticstask.Deviation = Convert.ToDouble(txtDeviation.Text);

            logisticsTasks.Add(logisticstask);

            string tempResult = "TaskID: " + logisticstask.TaskID.ToString() + ", " +
                "DeliveryTime: " + logisticstask.DeliveryTime.ToString() + ", " +
                "FromLocation: " + logisticstask.FromLocation + ", " +
                "ToLocation: " + logisticstask.ToLocation + ", " +
                "Deviation: " + logisticstask.Deviation;
           
            txtLogisticsTasks.Text = txtLogisticsTasks.Text + tempResult + "\r\n";

            txtDeliveryTime.Text = string.Empty;
            txtFromLocation.Text = string.Empty;
            txtToLocation.Text = string.Empty;
            txtDeviation.Text = string.Empty;

            txtLogisticsTasks.ScrollToEnd();
        }

        class TaskSchedule
        {
            public int TaskID { get; set; }
            public double PickTime { get; set; }
            public double ConsTime { get; set; }
        }

        List<TaskSchedule> taskSchedules = new List<TaskSchedule>();

        private void BtnCreateSchedule_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();

            double[] portion = new double[logisticsTasks.Count()];
            
            for (int i = 0; i < logisticsTasks.Count(); i++)
            {
                portion[i] = rand.NextDouble();
            }


            int iterNum = Convert.ToInt32(txtTotalNumTasks.Text);

            for (int i = 0; i < iterNum; i++)
            {
                MainWindow.TaskSchedule schedule = new MainWindow.TaskSchedule();
            }
        }
    }
}
