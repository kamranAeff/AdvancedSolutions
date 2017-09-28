using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ConsoleApp.C7
{
    class TPL
    {
        /// <summary>
        /// Delay - bu statik metod vasitesi ile mueyyen qeder gozlemeni temin etmek olar
        /// Laskin TPL-de her isi bir task ustune goturduyune gore cari programa tesir etmesini temin etmek ucun 
        /// .Wait() methodunu istifade etmek lazimdir
        /// </summary>
        public static void Sample1()
        {
            Console.WriteLine("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
            Task.Delay(TimeSpan.FromSeconds(10)).Wait();
            Console.WriteLine("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
        }

        /// <summary>
        /// ContinueWith-methodu bir taskin ardinca diger taski icra etmeni temin edir, 
        /// bele ki bir onceki taskin sehvsiz isleyib islemediyini yoxlayaraq (.IsFaulted-propertisi ile) her hansi ferqli emeliyyatlari ede bilerik
        /// </summary>
        public static void Sample2()
        {
            Task.Run(delegate
            {

                Console.WriteLine("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
                Task.Delay(TimeSpan.FromSeconds(10)).Wait();

            })
            .ContinueWith(pTask => {
                if (!pTask.IsFaulted)
                    Console.WriteLine("previousTask Ok");
                
                throw new Exception("Custom error");
            })
            .ContinueWith(r => nextTask(r, 1));
        }

        private static Task nextTask(Task previousTask, object arg2)
        {
            return Task.Run(delegate {

                if (previousTask.IsFaulted)
                {
                    Console.WriteLine("occured error on previousTask");
                    return;
                }
                Console.WriteLine("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);

            });
        }

        /// <summary>
        /// Yaradilmis taskin icra edilmesi ucun .Start() Methodunu cagirmaq mutleqdir
        /// </summary>
        static public void Sample3()
        {
            var t1 = new Task(() => {
                Console.WriteLine("Wait please...");
                Task.Delay(TimeSpan.FromSeconds(10)).Wait();
                Console.WriteLine("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
                
            });// Anonymous methods with lambda

            t1.Start();// taskin icra edilmesi ucun mutleq vacibdir
        }

        /// <summary>
        /// .Run()-methodu qeyd olunmus emeliyyati yerine yetirmek ucun Thread poola-yeni task elave edir, ve geriye hemin taski object kimi qaytarir
        /// .Run()-methodunu icra etdikde taski .Start etmete ehtiyyac yoxdur
        /// </summary>
        static public void Sample4()
        {
            var tResult=Task.Run(() => {
                Console.WriteLine("Wait please...");
                Task.Delay(TimeSpan.FromSeconds(10)).Wait();
                Console.WriteLine("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
            });

            Console.WriteLine(tResult.Status);
        }

        /// <summary>
        /// .Run<TResult>()-methodu .Run()-methodundan (return Task; )ferqli olaraq  geriye netice qaytarir "Task<TResult>" tipinde
        /// .neticeni almaq ucun await acar sozunden istifade olunur
        /// await acar sozu taninmaq ucun async acar sozu ile isarelenmis methodda yazilmalidir
        /// await ile isarelenen task emeliyyatin sona catmasini gozleyir mutleq
        /// </summary>
        async static public void Sample5()
        {
            var tResult = await Task.Run<int>(() =>
            {
                Console.WriteLine("Wait please...");
                Task.Delay(TimeSpan.FromSeconds(10)).Wait();
                return 5;
            });

            Console.WriteLine(">> Result: {0}", tResult);
        }

        /// <summary>
        /// .Wait() Methodu cari taskin bitmesini gozlemek ucun istifade olunur
        /// ferqi gozle gormek ucun Sample4 ve sample 6 methodlarini birge istifade ede bilersiz
        /// </summary>
        static public void Sample6()
        {
            var tResult = Task.Run(() =>
            {
                Console.WriteLine("Wait please...");
                Task.Delay(TimeSpan.FromSeconds(10)).Wait();
                Console.WriteLine("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
            });

            tResult.Wait();

            Console.WriteLine(tResult.Status);
        }

        /// <summary>
        /// .WaitAll() Methodu teyin edilmis bir nece taskin bitmesini gozlemek ucun istifade olunur
        /// .Wait methodunun sekildeyismesidir
        ///  emeliyyatlar paralel icra olundugu ucun(ideal veziyyetde) umumilikde kecen vaxt en cox icra muddetine malik olan taskin muddetine beraber olacaq
        /// </summary>
        static public void Sample7()
        {
            Stopwatch stopWatch = new Stopwatch();

            var t1 = Task.Run(() =>
            {
                Console.WriteLine("Task-1  Wait please...");
                Task.Delay(TimeSpan.FromSeconds(10)).Wait();
                Console.WriteLine("Task-1  {0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
            });

            var t2 = Task.Run(() =>
            {
                Console.WriteLine("Task-2  Wait please...");
                Task.Delay(TimeSpan.FromSeconds(11)).Wait();
                Console.WriteLine("Task-2  {0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
            });

            var t3 = Task.Run(() =>
            {
                Console.WriteLine("Task-3  Wait please...");
                Task.Delay(TimeSpan.FromSeconds(12)).Wait();
                Console.WriteLine("Task-3  {0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
            });

            stopWatch.Start();
            Task.WaitAll(t1, t2, t3);
            stopWatch.Stop();

            Console.WriteLine("Completed All of Tasks , Ellapsed time: {0:00} ",stopWatch.Elapsed.Seconds);
        }

        /// <summary>
        /// WaitAny methodunun WaitAll methodundan ferqi parameter olaraq aldigi Task Massivinden her hansi bir taskin bitdiyi anda hemin taskin massivdeki indexini qaytarir
        /// Progressbar ile icra edilen tasklarin icra %-ni gostermek ucun en yaxsi usuldur
        /// </summary>
        static public void Sample8()
        {
            List<Task> tasks = new List<Task>
            {
                Task.Run(() =>
            {
                Console.WriteLine("Task-1  Wait please...");
                Task.Delay(TimeSpan.FromSeconds(10)).Wait();
                Console.WriteLine("Task-1  {0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
            }),
            Task.Run(() =>
            {
                Console.WriteLine("Task-2  Wait please...");
                Task.Delay(TimeSpan.FromSeconds(11)).Wait();
                Console.WriteLine("Task-2  {0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
            }),
            Task.Run(() =>
            {
                Console.WriteLine("Task-3  Wait please...");
                Task.Delay(TimeSpan.FromSeconds(12)).Wait();
                Console.WriteLine("Task-3  {0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
            })
            };

            int index;
            while (tasks.Count>0)
            {
                index = Task.WaitAny(tasks.ToArray());
                Console.WriteLine("Completed TaskID: {0} , Status: {1}", tasks[index].Id,tasks[index].Status);
                tasks.RemoveAt(index);
            }
        }

        /// <summary>
        /// WhenAll methodu WaitAny Methodundan ferqli olaraq icraya gonderilen tasklarin neticesini massiv kimi qaytarir
        /// Butun Tasklarin bitmesini gozleyir
        /// </summary>
        async static public void Sample9()
        {
            var t1 = Task.Run<int>(() =>
            {
                Task.Delay(TimeSpan.FromSeconds(10)).Wait();
                return 1;
            });

            var t2 = Task.Run(() =>
            {
                Task.Delay(TimeSpan.FromSeconds(11)).Wait();
                return 2;
            });

            var t3 = Task.Run(() =>
            {
                Task.Delay(TimeSpan.FromSeconds(12)).Wait();
                return 3;
            });

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var results=await Task.WhenAll(t1,t2,t3);
            stopWatch.Stop();

            Console.WriteLine(">> Completed all tasks. Ellapsed time: {0:00} \nShowResults",stopWatch.Elapsed.Seconds);

            foreach (int result in results)
                Console.WriteLine(">> Value: {0}", result);
        }
    }
}
