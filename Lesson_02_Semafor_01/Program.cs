using System.Xml.Serialization;

namespace Lesson_02_Semafor_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "Symbols.xml";
            Functs functs = new Functs();

            functs.InitFile(path);

            Thread readFileThread = new Thread(() => functs.ReadFile(path));
            readFileThread.Start();

            Thread updateFileThread = new Thread(() => functs.UpdateFile(path));
            updateFileThread.Start();

            Console.WriteLine("Main thread is ended.");
        }
    }
}
