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

using System.IO;
using Microsoft.Win32;



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
            cmbTaskType.ItemsSource = new List<string> { "Supply", "Process" };
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
            txtDistanceMatrix.Clear();
        }

        private void BtnAddPosition_Click(object sender, RoutedEventArgs e)
        {
            StandardPosition position1 = new StandardPosition();
            StandardPosition position2 = new StandardPosition();
            StandardPosition position3 = new StandardPosition();
            StandardPosition position4 = new StandardPosition();
            StandardPosition position5 = new StandardPosition();
            StandardPosition position6 = new StandardPosition();
            StandardPosition position7 = new StandardPosition();
            StandardPosition position8 = new StandardPosition();
            StandardPosition position9 = new StandardPosition();
            StandardPosition position10 = new StandardPosition();
            StandardPosition position11 = new StandardPosition();
            StandardPosition position12 = new StandardPosition();

            position1.PositionID = "10";
            position2.PositionID = "11";
            position3.PositionID = "12";
            position4.PositionID = "13";
            position5.PositionID = "14";
            position6.PositionID = "15";
            position7.PositionID = "16";
            position8.PositionID = "17";
            position9.PositionID = "18";
            position10.PositionID = "19";
            position11.PositionID = "20";
            position12.PositionID = "21";

            position1.PositionName = "W1";
            position2.PositionName = "A";
            position3.PositionName = "C";
            position4.PositionName = "B";
            position5.PositionName = "W3";
            position6.PositionName = "W2";
            position7.PositionName = "FH1";
            position8.PositionName = "E";
            position9.PositionName = "D";
            position10.PositionName = "FH2";
            position11.PositionName = "F";
            position12.PositionName = "FH3";

            position1.CoordinateX = 1290.8;
            position2.CoordinateX = 1662.8;
            position3.CoordinateX = 1466;
            position4.CoordinateX = 1698.6;
            position5.CoordinateX = 1400.2;
            position6.CoordinateX = 1300.8;
            position7.CoordinateX = 3036.4;
            position8.CoordinateX = 2822.2;
            position9.CoordinateX = 3112.2;
            position10.CoordinateX = 2824.8;
            position11.CoordinateX = 2958;
            position12.CoordinateX = 2711;

            position1.CoordinateY = 530.2;
            position2.CoordinateY = 260.6;
            position3.CoordinateY = 67.4;
            position4.CoordinateY = -345.4;
            position5.CoordinateY = -201.2;
            position6.CoordinateY = 228.8;
            position7.CoordinateY = -917.2;
            position8.CoordinateY = -1154.8;
            position9.CoordinateY = -1405.8;
            position10.CoordinateY = -1529.8;
            position11.CoordinateY = -1747.6;
            position12.CoordinateY = -1819;

            standardPositions.Add(position1);
            standardPositions.Add(position2);
            standardPositions.Add(position3);
            standardPositions.Add(position4);
            standardPositions.Add(position5);
            standardPositions.Add(position6);
            standardPositions.Add(position7);
            standardPositions.Add(position8);
            standardPositions.Add(position9);
            standardPositions.Add(position10);
            standardPositions.Add(position11);
            standardPositions.Add(position12);

            positionNameList.Add(position1.PositionName);
            positionNameList.Add(position2.PositionName);
            positionNameList.Add(position3.PositionName);
            positionNameList.Add(position4.PositionName);
            positionNameList.Add(position5.PositionName);
            positionNameList.Add(position6.PositionName);
            positionNameList.Add(position7.PositionName);
            positionNameList.Add(position8.PositionName);
            positionNameList.Add(position9.PositionName);
            positionNameList.Add(position10.PositionName);
            positionNameList.Add(position11.PositionName);
            positionNameList.Add(position12.PositionName);

            string temp = "PositionID, PositionName, CoordinateX, CoordinateY" + "\r\n";

            txtStandardPositions.Text = temp;

            for (int i = 0; i < 12; i++)
            {
                string positionlist =
                    standardPositions[i].PositionID.ToString() + ", " +
                    standardPositions[i].PositionName.ToString() + ", " +
                    standardPositions[i].CoordinateX + ", " +
                    standardPositions[i].CoordinateY;

                txtStandardPositions.Text = txtStandardPositions.Text + positionlist + "\r\n";
                txtStandardPositions.ScrollToEnd();
            }

            /*
            StandardPosition standardPosition = new StandardPosition();

            standardPosition.PositionID = Guid.NewGuid().ToString();
            standardPosition.PositionName = txtPositionName.Text;
            standardPosition.PositionX = Convert.ToDouble(txtPositionX.Text);
            standardPosition.PositionY = Convert.ToDouble(txtPositionY.Text);

            standardPositions.Add(standardPosition);
            positionNameList.Add(standardPosition.PositionName);//should change to ID later

            string tempResult = "PositionID: " + standardPosition.PositionID.ToString() + ", " +
                "PositionName: " + standardPosition.PositionName.ToString() + ", " +
                "PositionX: " + standardPosition.PositionX + ", " +
                "PositionY: " + standardPosition.PositionY;

            txtStandardPositions.Text = txtStandardPositions.Text + tempResult + "\r\n";
            txtStandardPositions.ScrollToEnd();
            */
        }

        private void BtnAddTask_Click(object sender, RoutedEventArgs e)
        {
            StandardTask standardTask1 = new StandardTask();
            StandardTask standardTask2 = new StandardTask();
            StandardTask standardTask3 = new StandardTask();
            StandardTask standardTask4 = new StandardTask();
            StandardTask standardTask5 = new StandardTask();
            StandardTask standardTask6 = new StandardTask();
            StandardTask standardTask7 = new StandardTask();
            StandardTask standardTask8 = new StandardTask();
            StandardTask standardTask9 = new StandardTask();
            StandardTask standardTask10 = new StandardTask();
            StandardTask standardTask11 = new StandardTask();

            standardTask1.TaskID = "Task1";
            standardTask2.TaskID = "Task2";
            standardTask3.TaskID = "Task3";
            standardTask4.TaskID = "Task4";
            standardTask5.TaskID = "Task5";
            standardTask6.TaskID = "Task6";
            standardTask7.TaskID = "Task7";
            standardTask8.TaskID = "Task8";
            standardTask9.TaskID = "Task9";
            standardTask10.TaskID = "Task10";
            standardTask11.TaskID = "Task11";

            standardTask1.TaskName = "Task1";
            standardTask2.TaskName = "Task2";
            standardTask3.TaskName = "Task3";
            standardTask4.TaskName = "Task4";
            standardTask5.TaskName = "Task5";
            standardTask6.TaskName = "Task6";
            standardTask7.TaskName = "Task7";
            standardTask8.TaskName = "Task8";
            standardTask9.TaskName = "Task9";
            standardTask10.TaskName = "Task10";
            standardTask11.TaskName = "Task11";

            standardTask1.DeliveryTime = 55.2;
            standardTask2.DeliveryTime = 81.8;
            standardTask3.DeliveryTime = 26.6;
            standardTask4.DeliveryTime = 272.42;
            standardTask5.DeliveryTime = 279.66;
            standardTask6.DeliveryTime = 29.8;
            standardTask7.DeliveryTime = 38.8;
            standardTask8.DeliveryTime = 279.6;
            standardTask9.DeliveryTime = 288.2;
            standardTask10.DeliveryTime = 45.6;
            standardTask11.DeliveryTime = 62.06;

            standardTask1.FromLocation = "W1";
            standardTask2.FromLocation = "W1";
            standardTask3.FromLocation = "W2";
            standardTask4.FromLocation = "W3";
            standardTask5.FromLocation = "W3";
            standardTask6.FromLocation = "A";
            standardTask7.FromLocation = "B";
            standardTask8.FromLocation = "C";
            standardTask9.FromLocation = "C";
            standardTask10.FromLocation = "D";
            standardTask11.FromLocation = "E";

            standardTask1.ToLocation = "A";
            standardTask2.ToLocation = "B";
            standardTask3.ToLocation = "C";
            standardTask4.ToLocation = "D";
            standardTask5.ToLocation = "E";
            standardTask6.ToLocation = "C";
            standardTask7.ToLocation = "C";
            standardTask8.ToLocation = "D";
            standardTask9.ToLocation = "E";
            standardTask10.ToLocation = "F";
            standardTask11.ToLocation = "F";

            standardTask1.Deviation = 0.0;
            standardTask2.Deviation = 0.0;
            standardTask3.Deviation = 0.0;
            standardTask4.Deviation = 0.0;
            standardTask5.Deviation = 0.0;
            standardTask6.Deviation = 0.0;
            standardTask7.Deviation = 0.0;
            standardTask8.Deviation = 0.0;
            standardTask9.Deviation = 0.0;
            standardTask10.Deviation = 0.0;
            standardTask11.Deviation = 0.0;

            standardTask1.TaskType = "Supply";
            standardTask2.TaskType = "Supply";
            standardTask3.TaskType = "Supply";
            standardTask4.TaskType = "Supply";
            standardTask5.TaskType = "Supply";
            standardTask6.TaskType = "Process";
            standardTask7.TaskType = "Process";
            standardTask8.TaskType = "Process";
            standardTask9.TaskType = "Process";
            standardTask10.TaskType = "Process";
            standardTask11.TaskType = "Process";

            standardTasks.Add(standardTask1);
            standardTasks.Add(standardTask2);
            standardTasks.Add(standardTask3);
            standardTasks.Add(standardTask4);
            standardTasks.Add(standardTask5);
            standardTasks.Add(standardTask6);
            standardTasks.Add(standardTask7);
            standardTasks.Add(standardTask8);
            standardTasks.Add(standardTask9);
            standardTasks.Add(standardTask10);
            standardTasks.Add(standardTask11);

            string temp = "TaskName, TaskType, DeliveryTime, FromLocation, ToLocation, Deviation" + "\r\n";

            txtStandardTasks.Text = temp;

            for (int i = 0; i < 11; i++)
            {
                string tasklist = 
                    standardTasks[i].TaskName.ToString() + ", " +
                    standardTasks[i].TaskType + ", " +
                    standardTasks[i].DeliveryTime.ToString() + ", " +
                    standardTasks[i].FromLocation + ", " +
                    standardTasks[i].ToLocation + ", " +
                    standardTasks[i].Deviation;

                txtStandardTasks.Text = txtStandardTasks.Text + tasklist + "\r\n";
                txtStandardTasks.ScrollToEnd();
            }

            /*
            StandardTask standardTask = new StandardTask();

            standardTask.TaskID = Guid.NewGuid().ToString();
            standardTask.TaskName = "Task" + standardTasks.Count.ToString();
            standardTask.DeliveryTime = Convert.ToDouble(txtDeliveryTime.Text);
            standardTask.FromLocation = cmbFromLocation.SelectedItem.ToString();
            standardTask.ToLocation = cmbToLocation.SelectedItem.ToString();
            standardTask.TaskType = cmbTaskType.SelectedItem.ToString();

            standardTask.Deviation = Convert.ToDouble(txtDeviation.Text);//0.0

            standardTasks.Add(standardTask);
           
            string tempResult = "TaskName: " + standardTask.TaskName.ToString() + ", " +
                "TaskType: " + standardTask.TaskType + ", " +
                "DeliveryTime: " + standardTask.DeliveryTime.ToString() + ", " +
                "FromLocation: " + standardTask.FromLocation + ", " +
                "ToLocation: " + standardTask.ToLocation + ", " +
                "Deviation: " + standardTask.Deviation;

            txtStandardTasks.Text = txtStandardTasks.Text + tempResult + "\r\n";
            txtStandardTasks.ScrollToEnd();
            */
            
            /*
            txtDeliveryTime.Clear();
            txtFromLocation.Clear();
            txtToLocation.Clear();
            txtDeviation.Clear();
            */
        }

        // The necessary task for upgrade
        // 1. how to judge that "txtProbOfSupply.Text" is int or double type
        // 2. how to set ratio to generate num of each task
        private void BtnCreateSchedule_Click(object sender, RoutedEventArgs e)
        {
            int numofstandardtasks = standardTasks.Count();
            int totalnumoftasks = Convert.ToInt32(txtTotalNumTasks.Text);
            int timelength = Convert.ToInt32(txtTimeLength.Text);
            double probofsupply = Convert.ToDouble(txtProbOfSupply.Text);

            List<int> numofeachtask = controller.CalculateNumberofEachTask(standardTasks, totalnumoftasks, probofsupply, 0); //overload 
            taskSchedules = controller.GenerateSchedules(standardTasks, numofeachtask, timelength);

            string temp = "OrderNum, TaskID, TaskType, PickTime, Duration, ConsTime, FromLocation, ToLocation" + "\r\n";

            txtTaskSchedule.Text = temp;

            for (int i = 0; i < totalnumoftasks; i++)
            {
                string orderlist = (i + 1).ToString() + ", " +
                    taskSchedules[i].TaskName.ToString() + ", " +
                    taskSchedules[i].TaskType.ToString() + ", " +
                    taskSchedules[i].PickTime.ToString() + ", " +
                    taskSchedules[i].Duration.ToString() + ", " +
                    taskSchedules[i].ConsTime.ToString() + ", " +
                    taskSchedules[i].FromLocation.ToString() + ", " +
                    taskSchedules[i].ToLocation.ToString();

                txtTaskSchedule.Text = txtTaskSchedule.Text + orderlist + "\r\n";
                txtTaskSchedule.ScrollToEnd();
            }
        }

        private void BtnCalculateDistMatrix_Click(object sender, RoutedEventArgs e)
        {
            double[,] distanceArray = controller.CalculateDistance(standardPositions);
            string distanceMatrix = controller.WriteDistArray(distanceArray, positionNameList);
            
            txtDistanceMatrix.Text = distanceMatrix;
            txtDistanceMatrix.ScrollToEnd();
        }

        List<string> positionList = new List<string>();
        double[,] distanceMatrix;//unit: cm

        private void BtnReadCSVDistance_Click(object sender, RoutedEventArgs e)
        {            
            //Read DistanceMatrix
            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            string tempString = "";

            if (openFileDialog.ShowDialog() == true)
            {
                string[] lines = File.ReadAllLines(System.IO.Path.ChangeExtension(openFileDialog.FileName, ".csv"));

                string[] station = lines[0].Split(',');
                //for (int i = 1; i < station.Length; i++) positionList.Add(station[i]);
                distanceMatrix = new double[station.Length - 1, station.Length - 1];

                tempString = tempString + lines[0] + "\r\n";

                for (int i = 1; i < lines.Length; i++)
                {
                    tempString = tempString + lines[i] + "\r\n";

                    string[] data = lines[i].Split(',');
                    positionList.Add(data[0]);
                    for (int j = i-1; j < distanceMatrix.GetLength(0); j++)
                    {
                        distanceMatrix[i - 1, j] = Convert.ToDouble(data[j+1]);
                    }
                }
            }

            txtDistanceMatrix_.Text = tempString;


            MessageBox.Show("Distance matrix is imported");

        }

        List<TaskSchedule> inputTaskList = new List<TaskSchedule>();

        private void BtnReadCSV_Click(object sender, RoutedEventArgs e)
        {
            //Read CSV file
            OpenFileDialog openFileDialog = new OpenFileDialog();

            string tempString = "";

            if (openFileDialog.ShowDialog() == true)
            {
                string[] lines = File.ReadAllLines(System.IO.Path.ChangeExtension(openFileDialog.FileName, ".csv"));

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] data = lines[i].Split(',');
                    TaskSchedule tempTask = new TaskSchedule();
                    tempTask.ScheduleID = data[0];
                    tempTask.TaskName = data[1];
                    tempTask.TaskType= data[2];

                    //unit = second
                    tempTask.PickTime = ConvertMMSStoSecond(data[3]);
                    tempTask.Duration = ConvertMMSStoSecond(data[4]);
                    tempTask.ConsTime = ConvertMMSStoSecond(data[5]);

                    tempTask.FromLocation = data[6];
                    tempTask.ToLocation = data[7];

                    tempString = tempString + tempTask.ScheduleID + "," +
                    tempTask.TaskName + "," +
                    tempTask.TaskType + "," +
                    tempTask.PickTime + "," +
                    tempTask.Duration + "," +
                    tempTask.ConsTime + "," +
                    tempTask.FromLocation + "," + tempTask.ToLocation + "\r\n";


                    inputTaskList.Add(tempTask);                    
                }
            }

            txtTaskListInput.Text = tempString;

            MessageBox.Show("Task list is imported");
        }
        private void BtnCalculation_Click(object sender, RoutedEventArgs e)
        {
            double vehicleSpeed = 140.0; //cm/s

            //Sort by pick time
            List<TaskSchedule> sortedTaskList = inputTaskList.OrderBy(o => o.PickTime).ToList();

            //Create actual delivery tasks
            List<TaskSchedule> actualDeliveryList = CreateActualDeliveryTask(sortedTaskList);

            //Calc analytic solution
            List<double> actualPickTimeList = CalcAcutalPickTime(actualDeliveryList, vehicleSpeed, distanceMatrix, positionList);
            
            //Print results
            string tempString = "";
            for (int i = 0; i < actualDeliveryList.Count; i++)
            {
                double actualTravelTime = FindDistanceBetweenPositions(actualDeliveryList[i].FromLocation, actualDeliveryList[i].ToLocation, distanceMatrix, positionList) / vehicleSpeed;

                /*tempString = tempString + actualDeliveryList[i].ScheduleID + "," +
                    actualDeliveryList[i].TaskName + "," +
                    actualDeliveryList[i].TaskType + "," +
                    ConvertSecondtoMMSS(actualPickTimeList[i]) + "," +
                    ConvertSecondtoMMSS(actualTravelTime) + "," +
                    ConvertSecondtoMMSS(actualPickTimeList[i] + actualTravelTime) + "," +
                    actualDeliveryList[i].FromLocation + "," + actualDeliveryList[i].ToLocation + "\r\n";*/

                tempString = tempString + actualDeliveryList[i].ScheduleID + "," +
                        actualDeliveryList[i].TaskName + "," +
                        actualDeliveryList[i].TaskType + "," +
                        (actualPickTimeList[i]) + "," +
                        (actualTravelTime) + "," +
                        (actualPickTimeList[i] + actualTravelTime) + "," +
                        actualDeliveryList[i].FromLocation + "," + actualDeliveryList[i].ToLocation + "\r\n";
            }

            txtAnalyticSolution.Text = tempString;

        }

        List<double> CalcAcutalPickTime(List<TaskSchedule> tasks, double speed, double[,] distance, List<string> positions)
        {
            List<double> returnList = new List<double>();
            double currentTime = 0.0;

            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].TaskType != "Virtual")
                {
                    if (currentTime <= tasks[i].PickTime)
                    {
                        //save actual pick time
                        currentTime = tasks[i].PickTime;
                        returnList.Add(currentTime);

                        double tempDistance = FindDistanceBetweenPositions(tasks[i].FromLocation, tasks[i].ToLocation, distance, positions);
                        double actualTravelTime = tempDistance / speed;//second
                        currentTime = currentTime + actualTravelTime;
                    }
                    else
                    {
                        //save actual pick time
                        returnList.Add(currentTime);

                        double tempDistance = FindDistanceBetweenPositions(tasks[i].FromLocation, tasks[i].ToLocation, distance, positions);
                        double actualTravelTime = tempDistance / speed;//second
                        currentTime = currentTime + actualTravelTime;
                    }
                }
                //Virtual type
                else
                {
                    //save actual pick time
                    returnList.Add(currentTime);
                    double tempDistance = FindDistanceBetweenPositions(tasks[i].FromLocation, tasks[i].ToLocation, distance, positions);
                    double actualTravelTime = tempDistance / speed;//second

                    currentTime = currentTime + actualTravelTime;
                }
            }
            
            return returnList;
        }

        double FindDistanceBetweenPositions(string fromPosition, string toPosition, double[,] distance, List<string> positions)
        {
            double returnValue = 0.0;

            int fromIndex = 0, toIndex = 0;

            for (int i = 0; i < positions.Count; i++)
            {
                if (fromPosition == positions[i]) fromIndex = i;
                if (toPosition == positions[i]) toIndex = i;
            }

            returnValue = distance[fromIndex, toIndex];
            if (returnValue == 0.0) returnValue = distance[toIndex, fromIndex];

            return returnValue;
        }


        List<TaskSchedule> CreateActualDeliveryTask(List<TaskSchedule> tasks)
        {
            List<TaskSchedule> returnList = new List<TaskSchedule>();

            //Create temp delivery between locations
            for (int i = 0; i < tasks.Count; i++)
            {
                returnList.Add(tasks[i]);

                if (i != tasks.Count - 1)
                {
                    if (tasks[i].ToLocation != tasks[i + 1].FromLocation)
                    {

                        TaskSchedule temp = new TaskSchedule();
                        temp.ScheduleID =  i.ToString() + "_V";
                        temp.TaskName = "Virtual";
                        temp.TaskType = "Virtual";

                        temp.PickTime = 0.0;
                        temp.Duration = 0.0;
                        temp.ConsTime = 0.0;

                        temp.FromLocation = tasks[i].ToLocation;
                        temp.ToLocation = tasks[i + 1].FromLocation;

                        returnList.Add(temp);
                    }
                }
            }

            return returnList;
        }


        double ConvertMMSStoSecond(string time)
        {
            double returnValue = 0.0;

            string[] data = time.Split(':');
            double minute = Convert.ToDouble(data[0]);
            double second = Convert.ToDouble(data[1]);

            returnValue = minute * 60.0 + second;

            return returnValue;
        }

        string ConvertSecondtoMMSS(double seconds)
        {
            string returnValue = "";

            int minute = Convert.ToInt32(Math.Floor(seconds / 60.0));
            int second = Convert.ToInt32(seconds - minute * 60.0);

            returnValue = minute.ToString("D2") + ":" + second.ToString("D2");

            return returnValue;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConvertSecondtoMMSS(60.0);
        }
    }
}
