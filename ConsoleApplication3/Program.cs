using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPicker
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> queue = new Queue<int>(Enumerable.Range(1, 59));
            var buckets = new List<Bucket>()
                {
                    new Bucket("A", .58),
                    new Bucket("B", .33),
                    new Bucket("C", .09),
                };

            Bucket furthest;
            int picks = 0;
            while (queue.Any())
            {
                int pick = queue.Dequeue();
                furthest = buckets.OrderBy(b => b.GetAllocation(picks) - b.DesiredAllocation).First();
                furthest.Items.Add(pick);

                picks++;

                Console.WriteLine("Round {0}: Picked bucket {1} - A:{2:P}, B:{3:P}, C:{4:P}", picks, furthest.Id, 
                    buckets[0].GetAllocation(picks), buckets[1].GetAllocation(picks), buckets[2].GetAllocation(picks));
            }
            foreach (var bucket in buckets)
            {
                Console.WriteLine("{0}: {1:P}, {2} items", bucket.Id, bucket.ItemCount/(double) picks, bucket.ItemCount);
            }
        }
        public class Bucket
        {
            public List<int> Items { get; private set; }
            public string Id { get; set; }
            public double DesiredAllocation { get; set; }

            public int ItemCount { get { return Items.Count; } }

            public double GetAllocation(int picks)
            {
                return ItemCount/(double) picks;
            }

            public Bucket(string id, double allocation)
            {
                Items = new List<int>();
                Id = id;
                DesiredAllocation = allocation;
            }
        }
    }
}
