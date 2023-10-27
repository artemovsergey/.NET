using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace E_Exceptions
{
    //custom exception type
    public class CreditCardWithdrawException : Exception
    {

    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DirFileDemo();
            }
            catch(Exception ex)
            {

            }
        }

        static void DirFileDemo()
        {
            //File.Copy("test.txt", @"Q:\tmp\test_copy.txt", overwrite:true);
            File.Copy("test.txt", "test_copy.txt", overwrite: true);

            File.Move("test_copy.txt", "test_copy_renamed.txt");

            File.Delete("test_copy.txt");

            if (File.Exists("test.txt"))
            {
                File.AppendAllText("test.txt", "bla");
            }

            File.Replace("test_2.txt", "test_3.txt", "test_backup.txt");

            bool existsDir = Directory.Exists(@"C:\tmp");
            if (existsDir)
            {
                var files = Directory.EnumerateFiles(@"C:\tmp", "*.txt", SearchOption.AllDirectories);
                foreach(var file in files)
                {
                    Console.WriteLine(file);
                }
            }

            //Directory.Delete()

            //string fullPath = Path.Combine(@"C:\tmp", "\\bla", "file.txt");
        }

        static void FileDemo()
        {
            IEnumerable<string> lines = File.ReadLines("test.txt");

            File.WriteAllText("test_2.txt", "My name is John");
            File.WriteAllLines("test_3.txt", new string[] { "My name", "is John" });
            File.WriteAllBytes("test_4.txt", new byte[] { 72, 101, 108, 108, 111 });

            string alltext = File.ReadAllText("test_2.txt");
            Console.WriteLine(alltext);

            string[] allLines = File.ReadAllLines("test_3.txt");
            Console.WriteLine(allLines[0]);
            Console.WriteLine(allLines[1]);

            byte[] bytes = File.ReadAllBytes("test_4.txt");
            Console.WriteLine(Encoding.ASCII.GetString(bytes));

            Console.ReadLine();

            Stream fs = new FileStream("test.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            try
            {
                string str = "Hello, World";
                byte[] strInBytes = Encoding.ASCII.GetBytes(str);

                fs.Write(strInBytes, 0, strInBytes.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.ToString()}");
            }
            finally
            {
                fs.Close();
            }

            Console.WriteLine("Now reading:");

            using (Stream readingStream = new FileStream("test.txt", FileMode.Open, FileAccess.Read))
            {
                byte[] temp = new byte[readingStream.Length];

                int bytesToRead = (int)readingStream.Length;
                int bytesRead = 0;
                while (bytesToRead > 0)
                {
                    int n = readingStream.Read(temp, bytesRead, bytesToRead);

                    if (n == 0)
                        break;

                    bytesRead += n;
                    bytesToRead -= n;
                }
                string str = Encoding.ASCII.GetString(temp, 0, temp.Length);
                Console.WriteLine(str);

                Console.ReadLine();
            }
        }

        static void ExceptionsDemo()
        {
            FileStream file = null;
            try
            {
                file = File.Open("temp.txt", FileMode.Open);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (file != null)
                    file.Dispose();
            }

            Console.ReadLine();

            Console.WriteLine("Please input a number");

            string result = Console.ReadLine();

            int number = 0;
            try
            {
                number = int.Parse(result);
            }
            catch (OverflowException ex)
            {

            }
            catch (FormatException ex)
            {
                Console.WriteLine("A format exception has occured.");
                Console.WriteLine("Information is below:");
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine(number);
        }
    }
}
