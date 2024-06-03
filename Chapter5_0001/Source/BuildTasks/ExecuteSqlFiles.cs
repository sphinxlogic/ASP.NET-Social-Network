/*
To create this custom NAnt task I used this blog posting:
http://objecthead.blogspot.com/2006/03/invoke-custom-nant-tasks-with.html

My thanks to Matt Tuohy's!!!
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using NAnt.Core;
using NAnt.Core.Attributes;

namespace Fisharoo.BuildTasks
{
    [TaskName("ExecuteSqlFiles")]
    public class ExecuteSqlFiles : Task
    {
        private string _pathToSqlFiles;
        private string _databaseServer;

        [TaskAttribute("PathToSqlFiles", Required = true)]
        [StringValidator (AllowEmpty = false)]
        public string PathToSqlFiles
        {
            get { return _pathToSqlFiles; }
            set { _pathToSqlFiles = value; }
        }

        [TaskAttribute("DatabaseServer", Required = true)]
        [StringValidator (AllowEmpty = false)]
        public string DatabaseServer
        {
            get { return _databaseServer; }
            set { _databaseServer = value; }
        }

        protected override void ExecuteTask()
        {
            //Project.Log(Level.Error, PathToSqlFiles); //will fail the build
            //Project.Log(Level.Info, PathToSqlFiles); //will log in NAnt output
            //Project.Log(Level.Warn, PathToSqlFiles); //will display warning
            DirectoryInfo di = new DirectoryInfo(_pathToSqlFiles);
            FileInfo[] sqlFiles = di.GetFiles("*.sql");
            
            using (Process proc = new Process())
            {
                foreach (FileInfo file in sqlFiles)
                {
                    proc.StartInfo.CreateNoWindow = true;
                    proc.StartInfo.UseShellExecute = false;
                    proc.EnableRaisingEvents = false;
                    proc.StartInfo.RedirectStandardError = true;
                    proc.StartInfo.RedirectStandardOutput = true;

                    Console.Write("Attempting to execute: " + file.Name);

                    proc.StartInfo.FileName = "osql";
                    proc.StartInfo.Arguments = "-b -n -S " + _databaseServer + " -E -i\"" + _pathToSqlFiles + "\\" +
                                               file.Name + "\" -d Fisharoo";
                    proc.Start();
                    proc.WaitForExit();
                    if (proc.ExitCode > 0)
                    {
                        Console.Write(" - FAILED!!!");
                        Console.WriteLine("");
                        string so = proc.StandardOutput.ReadToEnd();
                        
                        Project.Log(Level.Error,
                          "#### " + file.Name + " failed");
                        throw (new Exception("OSQL failed to execute " + file.Name + ".\n\n" + so));
                    }
                    else
                    {
                        Console.Write(" - SUCCESS");
                        Console.WriteLine("");
                    }
                }
                proc.Close();
            }
        }
    }
}