using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueWithMinStats
{
    class Program
    {
        static void Main(string[] args)
        {
            FirstTest();
            SecondTest();

            Console.ReadKey();
        }

        private static void FirstTest()
        {
            var queue = new QueueWithMinStats();
            var addedNumbers = new List<int>();
            var rand = new Random();

            var res = true;
            for(var i = 0; i < 1000; i++)
            {
                var numb = rand.Next();
                queue.Enqueue(numb);
                addedNumbers.Add(numb);
                if (queue.CurrentMiminum != addedNumbers.Min())
                    res = false;
            }

            for (var i = 0; i < 1000; i++)
            {
                try
                {
                    var numb = queue.Dequeue();
                    addedNumbers.Remove(numb);
                    if (queue.CurrentMiminum != addedNumbers.Min())
                        res = false;
                }
                catch(QueueIsEmptyException ex)
                {
                    Console.WriteLine(ex.Message);

                    if (res)
                        Console.WriteLine("Test successfull");
                    else
                        Console.WriteLine("Test fail");
                }
            }
        }

        private static void SecondTest()
        {
            var queue = new QueueWithMinStats();

            var res = true;
            for (var i = 0; i < 1000; i++)
            {
                queue.Enqueue(i);

                if (queue.CurrentMiminum != i)
                    res = false;

                queue.Dequeue();
                try
                {

                   var min =  queue.CurrentMiminum;
                }
                catch(QueueIsEmptyException)
                {
                }
            }

            if (res)
                Console.WriteLine("Test successfull");
            else
                Console.WriteLine("Test fail");
        }
    }
}
