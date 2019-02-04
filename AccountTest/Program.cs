using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press [ENTER] to start");
            Console.ReadLine();

            string AccountFilePath = @"d:\a4.txt";

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string result = ReadAccountFile(AccountFilePath);
            stopwatch.Stop();

            Console.WriteLine("\r\n\r\n\r\n\r\n" + result + "\r\n\r\nTime elapsed (ms): {0}:", stopwatch.ElapsedMilliseconds);

            Console.ReadLine();
        }

        public static string ReadAccountFile(string AccountFilePath)
        {
            List<ModelAccountInfo> modelAccountInfos = new List<ModelAccountInfo>();
            ModelAccountInfo modelAccountInfo = new ModelAccountInfo();

            string dosya_yolu = AccountFilePath;
            string[] result = File.ReadAllLines(dosya_yolu);

            int accountGroupCount = Convert.ToInt32(result[0]);
            if (accountGroupCount > 5) return "Error: [the number of tests > 5]";

            int nextQuantityCount = 1;
            int LoopFileLine = 0;

            for (int i = 0; i < accountGroupCount; i++)
            {
                modelAccountInfos = new List<ModelAccountInfo>();
                int accountQuantityCount = Convert.ToInt32(result[nextQuantityCount]);
                if (accountQuantityCount > 100000) return "Error: [the number of accounts > 100 000]";
                LoopFileLine = 1 + nextQuantityCount;
                for (int j = 0; j < accountQuantityCount; j++)
                {
                    modelAccountInfo = new ModelAccountInfo();
                    modelAccountInfo = modelAccountInfos.Where(t => t.Account == result[LoopFileLine + j]).FirstOrDefault();
                    if (modelAccountInfo != null)
                    {
                        modelAccountInfo.Count = modelAccountInfo.Count + 1;
                    }
                    else
                    {
                        modelAccountInfo = new ModelAccountInfo();
                        modelAccountInfo.Account = result[LoopFileLine + j];
                        modelAccountInfo.Count = 1;
                        modelAccountInfos.Add(modelAccountInfo);
                    }

                }

                nextQuantityCount = LoopFileLine + accountQuantityCount;
                Console.Write(string.Join("\r\n", modelAccountInfos.OrderBy(t => t.Account).Select(t => t.Account + " " + t.Count).ToArray()) + "\r\n\r\n");
            }
            return "Success!!";
        }


        public class ModelAccountInfo
        {
            public string Account { get; set; }
            public int Count { get; set; }
        }


    }
}
