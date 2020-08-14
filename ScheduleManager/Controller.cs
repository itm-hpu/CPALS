using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Statistics.Distributions.Univariate;

namespace ScheduleManager
{
    class Controller
    {
        Random rand;

        public Controller()
        {
            rand = new Random();
        }
        
        public List<int> CalculateNumberofEachTask(List<StandardTask> standardtasks, int totalnumoftasks, double probofsupply, int a)
        {
            List<int> returnValue = new List<int>();

            returnValue.Add(1);
            returnValue.Add(1);
            returnValue.Add(1);
            returnValue.Add(1);
            returnValue.Add(1);

            returnValue.Add(3);
            returnValue.Add(3);
            returnValue.Add(3);
            returnValue.Add(3);
            returnValue.Add(4);
            returnValue.Add(4);

            return returnValue;
        }

        public List<int> CalculateNumberofEachTask(List<StandardTask> standardtasks, int totalnumoftasks, double probofsupply)
        {
            List<int> returnValue = new List<int>();

            int totalnumofsupply = Convert.ToInt32(Math.Ceiling(totalnumoftasks * probofsupply)); // total num of supply task
            int totalnumofprocess = totalnumoftasks -totalnumofsupply; // total num of process task

            int numofstandardsupply = 0;
            int numofstandardprocess = 0;

            for (int i = 0; i < standardtasks.Count; i++)
            {
                if (standardtasks[i].TaskType == "Supply")
                {
                    numofstandardsupply = numofstandardsupply + 1;
                }
                else if (standardtasks[i].TaskType == "Process")
                {
                    numofstandardprocess = numofstandardprocess + 1;
                }
            }

            int numofsupplytask = Convert.ToInt32(Math.Ceiling(totalnumofsupply * 1 / (Convert.ToDouble(numofstandardsupply))));
            int numofprocesstask = Convert.ToInt32(Math.Ceiling(totalnumofprocess * 1 / (Convert.ToDouble(numofstandardprocess))));

            int countoftotalsupply = 0;
            int countoftotalprocess = 0;
            int sumoftotalsupply = 0;
            int sumoftotalprocess = 0;
            for (int i = 0; i < standardtasks.Count; i++)
            {
                if (standardtasks[i].TaskType == "Supply")
                {
                    if (countoftotalsupply != numofstandardsupply - 1)
                    {
                        returnValue.Add(numofsupplytask);
                    }
                    else
                    {
                        returnValue.Add(totalnumofsupply - sumoftotalsupply);
                    }
                    countoftotalsupply++;
                    sumoftotalsupply = sumoftotalsupply + numofsupplytask;
                }
                else if (standardtasks[i].TaskType == "Process")
                {
                    if (countoftotalprocess != numofstandardprocess - 1)
                    {
                        returnValue.Add(numofprocesstask);
                    }
                    else
                    {
                        returnValue.Add(totalnumofprocess - sumoftotalprocess);
                    }
                    countoftotalprocess++;
                    sumoftotalprocess = sumoftotalprocess + numofprocesstask;
                }
            }

            return returnValue;
        }

        public List<TaskSchedule> GenerateSchedules(List<StandardTask> standardTasks, List<int> numofeachtask, int timelength)
        {
            List<TaskSchedule> returnList = new List<TaskSchedule>();

            for (int i = 0; i < standardTasks.Count; i++)
            {
                for (int j = 0; j < numofeachtask[i]; j++)
                {
                    TaskSchedule taskSchedule = new TaskSchedule();

                    taskSchedule.TaskName = standardTasks[i].TaskName;
                    taskSchedule.TaskType = standardTasks[i].TaskType;
                    taskSchedule.PickTime = Math.Round(rand.NextDouble() * timelength);
                    taskSchedule.ConsTime = Math.Round(NormalDistribution.Random(standardTasks[i].DeliveryTime, standardTasks[i].Deviation)) + taskSchedule.PickTime;
                    taskSchedule.FromLocation = standardTasks[i].FromLocation;
                    taskSchedule.ToLocation = standardTasks[i].ToLocation;

                    while (taskSchedule.ConsTime > timelength)
                    {
                        taskSchedule.PickTime = Math.Round(rand.NextDouble() * timelength);
                        taskSchedule.ConsTime = Math.Round(NormalDistribution.Random(standardTasks[i].DeliveryTime, standardTasks[i].Deviation)) + taskSchedule.PickTime;
                    }

                    taskSchedule.Duration = taskSchedule.ConsTime - taskSchedule.PickTime;

                    returnList.Add(taskSchedule);
                }
            }

            return returnList;
        }

        public double[,] CalculateDistance(List<StandardPosition> standardPositions)
        {
            double[,] returnArray = new double[standardPositions.Count, standardPositions.Count];

            for (int i = 0; i < standardPositions.Count; i++)
            {
                for (int j = 0; j < standardPositions.Count; j++)
                {
                    if (i == j)
                    {
                        returnArray[i, j] = 0;
                    }
                    else
                    {
                        double distX = standardPositions[i].CoordinateX - standardPositions[j].CoordinateX;
                        double distY = standardPositions[i].CoordinateY - standardPositions[j].CoordinateY;
                        returnArray[i, j] = Math.Round(Math.Sqrt(distX * distX + distY * distY) * 100) / 100;
                    }
                }
            }
            return returnArray;
        }

        public string WriteDistArray(double[,] distanceArray, List<string> positionNameList)
        {
            string tempResult = string.Empty;

            for (int i = 0; i < distanceArray.GetLength(0); i++)
            {
                tempResult = tempResult + positionNameList[i] + ", ";
            }
            tempResult = "PositionName, " + tempResult;
            tempResult = tempResult.Substring(0, tempResult.Length - 2);
            tempResult = tempResult + "\r\n";

            for (int i = 0; i < distanceArray.GetLength(0); i++)
            {
                tempResult = tempResult + positionNameList[i] + ", ";

                for (int j = 0; j < distanceArray.GetLength(1); j++)
                {
                    tempResult = tempResult + distanceArray[i, j].ToString("F2") + ", ";
                }
                tempResult = tempResult.Substring(0, tempResult.Length - 2);
                tempResult = tempResult + "\r\n";
            }
            return tempResult;
        }
    }
}
