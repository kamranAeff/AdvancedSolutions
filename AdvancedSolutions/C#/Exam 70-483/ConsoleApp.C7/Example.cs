using System;
using System.Threading;
using System.Windows.Forms;
namespace ConsoleApp.C7
{
    class Example
    {
        static int length = 10;
        public static void Sample1()
        {
            int length = 20;

            var t1 = new Thread(()=> {

                for (int i = 0; i < length; i++)
                    Console.WriteLine(">> t[1] : {0}", i);

            });

            var t2 = new Thread(()=>
            {

                for (int i = 0; i < length; i++)
                    Console.WriteLine(">> t[2] : {0}", i);

            });

            // 1)
            //t1.Start();// thread 1 ise dussun
            //t2.Start();// thread 2 ise dussun
            /*bu halda emeliyyat xaotik veziyyet alir threadlar paralel isleyir,
            ve yaxud eksine, hec bir qanunauygunluq yoxdur */

            // 2)
            t1.Start();
            t1.Join();// thread 1-in biteyini gozleyir deye 2ci thread 1-ci isini tam bitirdikden sonra islemeye baslayir
            t2.Start();
        }

        public static void Sample2()
        {
            
            //ThreadPool vasitesi ile (QueueUserWorkItem-methodu) emeliyyatlar thread novbesine salinir ve prosessor imkan qeder her threadi paralel isletmeye calisir
            // adi threaddan ferqi odur ki, ThreadPoolda eger hazir bos thread varsa 
            //emeliyyat onun ohdeliyine verilir,ve yeni thread yaradilmir
            //fikirlesende ki bir programda 20 thread isleyir ve her thread 1 mb yer tutdugunu nezere alsaq bu ramda 20 mb yer demekdir,ustelik her defe 
            //yeni instance yaratmaq (var t1=new Thread(()=>{})) xosagelmez neticelere getirib cixara biler

            ThreadPool.QueueUserWorkItem(delegate
            {
                for (int i = 0; i < length; i++)
                    Console.WriteLine(">> t[2] : {0}", i);
            });


            ThreadPool.QueueUserWorkItem(methodEx1);
        }

        private static void methodEx1(object state)
        {
            for (int i = 0; i < length; i++)
                Console.WriteLine(">> t[2] : {0}", i);
        }


        public static void Sample3()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BackgroundWorkerForm());
        }
    }
}
