using System;
using System.Collections.Generic;
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

        //What for?
        double GetPickTime(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
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

                    taskSchedule.TaskID = standardTasks[i].TaskID;
                    taskSchedule.PickTime = Math.Round(rand.NextDouble() * timelength * 1000) / 1000;
                    taskSchedule.ConsTime = Math.Round(NormalDistribution.Random(standardTasks[i].DeliveryTime, standardTasks[i].Deviation) * 1000) / 1000 + taskSchedule.PickTime;                    

                    while (taskSchedule.ConsTime > timelength)
                    {
                        taskSchedule.PickTime = Math.Round(rand.NextDouble() * timelength * 1000) / 1000;
                        taskSchedule.ConsTime = Math.Round(NormalDistribution.Random(standardTasks[i].DeliveryTime, standardTasks[i].Deviation) * 1000) / 1000 + taskSchedule.PickTime;
                    }

                    taskSchedule.Duration = taskSchedule.ConsTime - taskSchedule.PickTime;

                    returnList.Add(taskSchedule);
                }
            }

            return returnList;
        }
    }
}
