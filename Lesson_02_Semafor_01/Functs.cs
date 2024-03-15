using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lesson_02_Semafor_01
{
    internal class Functs
    {
        object locker = new object();
        public void InitFile(string path)
        {
            Random rnd = new Random();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < rnd.Next(1, 15); i++)
            {
                stringBuilder.Append('!');
            }

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(string));
                using (Stream stream = File.Open(path, FileMode.Create))
                {
                    xmlSerializer.Serialize(stream, stringBuilder.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ReadFile(string path)
        {
            int count = 0;
            string des = string.Empty;
            int hash = Thread.CurrentThread.GetHashCode();
            lock (locker)
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(string));
                    using (Stream stream = File.Open(path, FileMode.Open))
                    {
                        des = (string)xmlSerializer.Deserialize(stream);
                    }

                    for (int i = 0; i < des.Length; i++)
                    {
                        if (des[i] == '!')
                        {
                            Console.WriteLine($"Thread #{hash}: iterator {i} found symbol '!'");
                            Thread.Sleep(1000);
                            count++;
                        }
                    }
                    Console.WriteLine($"Number of '!' symbols is: {count}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public void UpdateFile(string path)
        {
            string des = string.Empty;
            lock (locker)
            {
                try
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(string));
                    using (Stream stream = File.Open(path, FileMode.Open))
                    {
                        des = (string)xmlSerializer.Deserialize(stream);
                    }

                    string res = des.Replace('!', '#');
                    using (Stream stream = File.OpenWrite(path))
                    {
                        xmlSerializer.Serialize(stream, res);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
