using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogopakSimAsyn
{
    internal class Program
    { 
        private static Random random = new Random();
        static async Task Main(string[] args)
        {
            Console.WriteLine("Метод Main начал работу в потоке "+Thread.CurrentThread.ManagedThreadId);
            while (true)
            {
                await MyAsync();
            }


        }

        static string GenerateData()
        {
            Console.WriteLine("Метод GenerateData начал работу в потоке " + Thread.CurrentThread.ManagedThreadId);
            StringBuilder stringBuilder = new StringBuilder();
            string date = DateTime.Now.ToString("dd/MM/yyyy;HH:mm:ss;", new CultureInfo("en-EN"));
            stringBuilder.Append(date);
            stringBuilder.Append("00");
            stringBuilder.Append("05941234567");
            int randomNumber = random.Next(1000000, 9999999);
            stringBuilder.Append(randomNumber.ToString());
            stringBuilder.Append('\n');
            Console.WriteLine("Метод GenerateData закончил работу в потоке " + Thread.CurrentThread.ManagedThreadId);
            return stringBuilder.ToString();

        }
        static async Task WriteToFile(List<string> list)
        {
            Console.WriteLine("Метод WriteToFile начал работу в потоке " + Thread.CurrentThread.ManagedThreadId);
            string pathToSave = "C:/folderToDel/leap.log";
            using (FileStream stream = new FileStream(pathToSave, FileMode.OpenOrCreate))
                foreach (string item in list)
                {
                    await stream.WriteAsync(Encoding.Default.GetBytes(item), 0, item.Length);
                }
            Console.WriteLine("Метод WriteToFile закончил работу в потоке " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Файл создан успешно!");
        }

        static async Task MyAsync() 
        {
            Console.WriteLine("Метод MyAssync начал работу в потоке " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Итерация начата");
            List<string> list = new List<string>();
            int j = random.Next(10);
            for (int i = 0; i <= j; i++)
            {
                list.Add(GenerateData());
                await Task.Delay(random.Next(100, 1000));
            }
            await WriteToFile(list);
            Console.WriteLine("Итерация завершена успешно");
            await Task.Delay(10000);
            Console.WriteLine("Метод MyAssync закончил работу в потоке " + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
