/*
To create this custom NAnt task I used this blog posting:
http://objecthead.blogspot.com/2006/03/invoke-custom-nant-tasks-with.html

Regarding the recrusion methods I used this article:
http://msdn.microsoft.com/en-us/library/bb513869.aspx
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace Fisharoo.BuildTasks
{
    [TaskName("ParseCodeDebt")]
    public class ParseCodeDebt : Task
    {
        private string CurrentFolder { get; set; }

        public void PerformTest()
        {
            ExecuteTask();    
        }

        [TaskAttribute("PathToParse", Required = true)]
        [StringValidator (AllowEmpty = false)]
        public string PathToParse { get; set; }
        protected override void ExecuteTask()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Log(Level.Warning,"---------------------------------------------------------------------------");
            Log(Level.Warning, "CODE DEBT CHECK for " + PathToParse);

            //iterate through all the files in a directory recursively
            System.IO.DirectoryInfo root = new DirectoryInfo(PathToParse);
            WalkDirectoryTree(root);

            Log(Level.Warning, "");
            Log(Level.Warning, "Completed in " + sw.ElapsedMilliseconds + "ms");
            Log(Level.Warning, "---------------------------------------------------------------------------");
            Log(Level.Warning, "");
        }

        private void WalkDirectoryTree(System.IO.DirectoryInfo root)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            // First, process all the files directly under this folder
            try
            {
                files = root.GetFiles("*.*");
            }
                // This is thrown if even one of the files requires permissions greater
                // than the application provides.
            catch (UnauthorizedAccessException e)
            {
                // This code just writes out the message and continues to recurse.
                Log(Level.Warning, e.Message);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                Log(Level.Warning, e.Message);
            }

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    if (fi.Name.ToLower().EndsWith(".cs") && fi.Name.ToLower() != "parsecodedebt.cs")
                    {
                        try
                        {
                            //look for code debt in this file
                            ParseFile(fi);
                        }
                        catch (FileNotFoundException e)
                        {
                            Log(Level.Warning, e.Message);
                        }
                    }
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    if (dirInfo.Name != ".svn")
                    {
                        // Resursive call for each subdirectory.
                        WalkDirectoryTree(dirInfo);
                    }
                }
            }
        }

        private void ParseFile(FileInfo file)
        {
            StreamReader sr = file.OpenText();
            int lineNumber = 1;
            string line = "";
            line = sr.ReadLine();
            while(line!=null)
            {
                if(line.ToLower().Contains("//codedebt"))
                {
                    if (CurrentFolder != file.DirectoryName)
                    {
                        CurrentFolder = file.DirectoryName;
                        Log(Level.Warning, "");
                        Log(Level.Warning, CurrentFolder);
                    }
                    Log(Level.Warning, "\t" + file.Name);
                    string[] attributes = line.ToLower().Replace("//codedebt","").Trim().Split('|');
                    Log(Level.Warning, "\tLine " + lineNumber.ToString());
                    foreach (string s in attributes)
                    {
                        Log(Level.Warning, "\t" + s.Trim());
                    }
                }
                lineNumber++;
                line = sr.ReadLine();
            }
            sr.Close();
        }
    }
}