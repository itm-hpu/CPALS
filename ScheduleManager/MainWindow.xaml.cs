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
        //Global variables
        List<StandardTask> standardTasks;
        List<TaskSchedule> taskSchedules;
        List<StandardPosition> standardPositions;
        List<string> positionNameList;

        Controller controller = new Controller();        

        public MainWindow()
        {
            InitializeComponent();
            standardTasks = new List<StandardTask>();
            taskSchedules = new List<TaskSchedule>();
            standardPositions = new List<StandardPosition>();
            positionNameList = new List<string>();

            cmbFromLocation.ItemsSource = positionNameList;
            cmbToLocation.ItemsSource = positionNameList;
        }        

        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            StandardTask standardTask = new StandardTask();

            standardTask.TaskID = Guid.NewGuid().ToString();
            standardTask.TaskName = "Task" + standardTasks.Count.ToString();
            standardTask.DeliveryTime = Convert.ToDouble(txtDeliveryTime.Text);
            standardTask.FromLocation = cmbFromLocation.SelectedItem.ToString();
            standardTask.ToLocation = cmbToLocation.SelectedItem.ToString();

            standardTask.Deviation = Convert.ToDouble(txtDeviation.Text);//0.0

            standardTasks.Add(standardTask);

            string tempResult = "TaskName: " + standardTask.TaskName.ToString() + ", " +
                "DeliveryTime: " + standardTask.DeliveryTime.ToString() + ", " +
                "FromLocation: " + standardTask.FromLocation + ", " +
                "ToLocation: " + standardTask.ToLocation + ", " +
                "Deviation: " + standardTask.Deviation;

            txtStandardTasks.Text = txtStandardTasks.Text + tempResult + "\r\n";
            txtStandardTasks.ScrollToEnd();

            /*
            txtDeliveryTime.Clear();
            txtFromLocation.Clear();
            txtToLocation.Clear();
            txtDeviation.Clear();*/
        }
        
        private void BtnCreateSchedule_Click(object sender, RoutedEventArgs e)
        {
            int numofstandardtasks = standardTasks.Count();
            int totalnumoftasks = Convert.ToInt32(txtTotalNumTasks.Text);
            int timelength = Convert.ToInt32(txtTimeLength.Text);

            int[] numofeachtask = controller.CalculateNumberofEachTask(numofstandardtasks, totalnumoftasks);
            taskSchedules = controller.GenerateSchedules(standardTasks, numofstandardtasks, numofeachtask, timelength);

            string temp = "OrderNum, TaskID, PickTime, Duration, ConsTime" + "\r\n";

            txtTaskSchedule.Text = temp;

            for (int i = 0; i < totalnumoftasks; i++)
            {
                string orderlist = (i + 1).ToString() + ", " +
                    taskSchedules[i].TaskName.ToString() + ", " +
                    taskSchedules[i].PickTime.ToString() + ", " +
                    taskSchedules[i].Duration.ToString() + ", " +
                    taskSchedules[i].ConsTime.ToString();

                txtTaskSchedule.Text = txtTaskSchedule.Text + orderlist + "\r\n";
                txtTaskSchedule.ScrollToEnd();
            }
            
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            standardTasks = new List<StandardTask>();
            taskSchedules = new List<TaskSchedule>();

            txtStandardTasks.Clear();
            txtTaskSchedule.Clear();
            txtTotalNumTasks.Clear();
            txtTimeLength.Clear();
            txtStandardPositions.Clear();
        }

        private void BtnAddPosition_Click(object sender, RoutedEventArgs e)
        {
            StandardPosition standardPosition = new StandardPosition();

            standardPosition.PositionID = Guid.NewGuid().ToString();
            standardPosition.PositionName = txtPositionName.Text;
            standardPosition.PositionX = Convert.ToDouble(txtPositionX.Text);
            standardPosition.PositionY = Convert.ToDouble(txtPositionY.Text);

            standardPositions.Add(standardPosition);
            positionNameList.Add(standardPosition.PositionName);//should change to ID later

            string tempResult = /*"PositionID: " + standardPosition.PositionID.ToString() + ", " +*/
                "PositionName: " + standardPosition.PositionName.ToString() + ", " +
                "PositionX: " + standardPosition.PositionX + ", " +
                "PositionY: " + standardPosition.PositionY;

            txtStandardPositions.Text = txtStandardPositions.Text + tempResult + "\r\n";
            txtStandardPositions.ScrollToEnd();
        }

        private void BtnCalculateDistMatrix_Click(object sender, RoutedEventArgs e)
        {

            double[,] distanceArray = controller.CalculateDistance(standardPositions);
            string distanceMatrix = controller.WriteDistArray(distanceArray, positionNameList);
            
            txtDistanceMatrix.Text = distanceMatrix;
            txtDistanceMatrix.ScrollToEnd();
        }
    }
}
