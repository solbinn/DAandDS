using PriorityQueueLib;
using MyListLib;
using System.Diagnostics;

namespace MyPriorityQueueRealisation
{
    public class PriorityQueueItem : IComparable<PriorityQueueItem>
    {
        int Priority { get; set; }
        int OrderIndex { get; set; }
        int StepCount { get; set; }

        public PriorityQueueItem(int priority, int orderIndex, int stepCount)
        {
            Priority = priority;
            OrderIndex = orderIndex;
            StepCount = stepCount;
        }

        public int CompareTo(PriorityQueueItem other)
        {
            return Priority.CompareTo(other.Priority);
        }

        static void Main(string[] args)
        {
            string logFilePath = "log.txt";
            MyPriorityQueue<PriorityQueueItem> priorityQueue = new MyPriorityQueue<PriorityQueueItem>();
            int itemCount = Convert.ToInt32(Console.ReadLine());
            int totalAddedCount = 0;
            Stopwatch stopwatch = Stopwatch.StartNew();
            StreamWriter logWriter = new StreamWriter(logFilePath);

            for (int i = 0; i < itemCount; i++)
            {
                Random randomGenerator = new Random();
                int numberOfItems = randomGenerator.Next(1, 11);
                for (int j = 0; j < numberOfItems; j++)
                {
                    int priorityValue = randomGenerator.Next(1, 6);
                    PriorityQueueItem newItem = new PriorityQueueItem(priorityValue, j, i);
                    priorityQueue.Add(newItem);
                    logWriter.WriteLine("ADD:" + " " + newItem.OrderIndex + " " + newItem.Priority + " " + newItem.StepCount);
                    ++totalAddedCount;
                }
            }

            for (int i = 0; i < totalAddedCount; i++)
            {
                PriorityQueueItem currentItem = priorityQueue.Peek();
                logWriter.WriteLine("REMOVE:" + " " + currentItem.OrderIndex + " " + currentItem.Priority + " " + currentItem.StepCount);
                priorityQueue.Remove(currentItem);
            }

            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;
            logWriter.WriteLine($"Время выполнения: {elapsedTime.TotalSeconds} секунд");
            logWriter.Close();
        }
    }
}
