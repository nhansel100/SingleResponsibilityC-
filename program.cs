using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using static SingleResponsibility2.Journal;

namespace SingleResponsibility2
{

    /// <summary>
    /// This is a demonstration of the single responsibility principle of OOP. 
    /// In this demonstration, each class has it's own role of managing the program.
    /// </summary>

    public class Journal
    {
        private readonly List<string> entries = new List<string>();

        private static int count = 0;

        public int AddEntry(string text)
        {
            entries.Add($"{++count}");
            return count; // memento
        }

        public void RemoveEntry(int index)
        {
            entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }
        
        //class to manage data persistence
        public class Persistence
        {
            public void SaveToFile(Journal j, string filename, bool overwrite = false)
            {
                if (overwrite || !File.Exists(filename))
                    File.WriteAllText(filename, j.ToString());
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("Today I posted to Github");
            j.AddEntry("Then I went for a run :)");
            Console.WriteLine(j);

            var p = new Persistence();
            var filename = @"C:\Users\Nate\Repos";
            p.SaveToFile(j, filename, true);
            Process.Start(filename);
        }
    }
}
