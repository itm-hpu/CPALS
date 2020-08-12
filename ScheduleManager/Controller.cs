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

        public int[] CalculateNumberofEachTask(int numofstandardtasks, int totalnumoftasks)
        {

            int[] returnValue = new int[numofstandardtasks];

            double[] portion = new double[numofstandardtasks];
            for (int i = 0; i < numofstandardtasks; i++)
            {
                portion[i] = rand.NextDouble();
            }

            int sum = 0;
            for (int i = 0; i < numofstandardtasks; i++)
            {
                if (i == numofstandardtasks)
                {
                    returnValue[i] = numofstandardtasks - sum;
                }
                else
                {
                    returnValue[i] = (int)Math.Ceiling(totalnumoftasks * (portion[i] / portion.Sum()));
                }

                sum = sum + returnValue[i];
            }

            return returnValue;
        }

        public List<TaskSchedule> GenerateSchedules(List<StandardTask> standardTasks, int numofstandardtasks, int[] numofeachtask, int timelength)
        {
            List<TaskSchedule> returnList = new List<TaskSchedule>();

            for (int i = 0; i < numofstandardtasks; i++)
            {
                for (int j = 0; j < numofeachtask[i]; j++)
                {
                    TaskSchedule taskSchedule = new TaskSchedule();

                    taskSchedule.TaskName = standardTasks[i].TaskName;
                    taskSchedule.PickTime = Math.Round(rand.NextDouble() * timelength);
                    taskSchedule.ConsTime = Math.Round(NormalDistribution.Random(standardTasks[i].DeliveryTime, standardTasks[i].Deviation)) + taskSchedule.PickTime;                    

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
                        double distX = standardPositions[i].PositionX - standardPositions[j].PositionX;
                        double distY = standardPositions[i].PositionY - standardPositions[j].PositionY;
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
