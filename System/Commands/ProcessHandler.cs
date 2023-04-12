using DStutz.Apps;

using System.Diagnostics;

namespace DStutz.System.Commands
{
    // https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.process?view=netcore-3.1
    public class ProcessHandler
    {
        #region Properties
        /***********************************************************/
        public ILogger Logger { get; } = AppLogger.CreateLogger<ProcessHandler>();
        public bool LogAnyway { get; set; } = true;
        public bool AppendToFile { get; set; } = false;
        public bool WaitForExit { get; set; } = true;
        public int WaitForExitMilliSeconds { get; set; } = -1;

        // Use the operating system shell to start the process
        // default is true
        private bool UseShellExecute = false;

        // The application redirects its streams to the process
        // default is false, false and false
        private bool RedirectStandardError = true;
        private bool RedirectStandardInput = false;
        private bool RedirectStandardOutput = true;

        private Process? Process;
        private string? ProcessOutput;
        private string? ProcessError;
        #endregion

        #region Constructors
        /***********************************************************/
        public ProcessHandler()
        {
            // This settings are used by the following commands
            //
            // java............  -->.1 (because of missing arguments)
            // java -help        --> 0
            // java -version     --> 0
            // magick            --> 0 (error message)
            // magick -help      --> 0
            // magick -version   --> 0
            // pdflatex -help    --> 0
            // pdflatex -version --> 0
            UseShellExecute = false;
            RedirectStandardError = false;
            RedirectStandardInput = false;
            RedirectStandardOutput = false;
        }

        public ProcessHandler(
            bool useShellExecute)
        {
            UseShellExecute = useShellExecute;

            if (useShellExecute)
            {
                RedirectStandardError = false;
                RedirectStandardInput = false;
                RedirectStandardOutput = false;
            }
        }

        public ProcessHandler(
            bool redirectError,
            bool redirectInput,
            bool redirectOutput)
        {
            RedirectStandardError = redirectError;
            RedirectStandardInput = redirectInput;
            RedirectStandardOutput = redirectOutput;

            if (redirectError ||
                redirectInput ||
                redirectOutput)
            {
                UseShellExecute = false;
            }
        }
        #endregion

        #region Methods processing
        /***********************************************************/
        public string? Execute(
            string command)
        {
            return Execute(command, null, null, null, null);
        }

        public string? Execute(
            string command,
            string argument,
            string? workingDir = null,
            string? outputFile = null)
        {
            return Execute(command, argument, null, workingDir, outputFile);
        }

        public string? Execute(
            string command,
            IEnumerable<string> arguments,
            string? workingDir = null,
            string? outputFile = null)
        {
            return Execute(command, null, arguments, workingDir, outputFile);
        }

        private string? Execute(
            string command,
            string? argument,
            IEnumerable<string>? arguments,
            string? workingDir,
            string? outputFile)
        {
            //Console.WriteLine(command);
            //Console.WriteLine(argument);
            //Console.WriteLine(workingDir);
            //Console.WriteLine(outputFile);

            var psi = new ProcessStartInfo(command);

            // The application uses the streams of the shell
            psi.UseShellExecute = UseShellExecute;

            // The application can use the streams of the process
            if (!UseShellExecute)
            {
                psi.RedirectStandardError = RedirectStandardError;
                psi.RedirectStandardInput = RedirectStandardInput;
                psi.RedirectStandardOutput = RedirectStandardOutput;
            }

            //psi.WindowStyle = ProcessWindowStyle.Minimized;

            if (!string.IsNullOrWhiteSpace(argument))
                psi.Arguments = argument;

            if (arguments != null)
                foreach (var a in arguments)
                    psi.ArgumentList.Add(a);

            if (!string.IsNullOrWhiteSpace(workingDir))
                psi.WorkingDirectory = workingDir;

            try
            {
                Process = Process.Start(psi);

                if (LogAnyway)
                {
                    Logger.LogInformation(
                        "Process started");
                    Logger.LogInformation(
                        "--> Command file: {0}",
                        psi.FileName);

                    if (!string.IsNullOrEmpty(psi.Arguments))
                        Logger.LogInformation(
                            "--> Command line: {0}",
                            psi.Arguments);

                    if (psi.ArgumentList?.Count > 0)
                        Logger.LogInformation(
                            "--> Command line: {0}",
                            string.Join(" ", psi.ArgumentList));

                    if (!string.IsNullOrEmpty(workingDir))
                        Logger.LogInformation(
                            "--> Working dir:  {0}",
                            workingDir);

                    if (!string.IsNullOrEmpty(outputFile))
                        Logger.LogInformation(
                            "--> Output file:  {0}",
                            outputFile);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Unable to execute command " +
                    psi.FileName + " " +
                    psi.Arguments, ex);
            }

            try
            {
                if (Process != null)
                {
                    if (RedirectStandardOutput &&
                        Process.StandardOutput != null)
                    {
                        // Read the textual output of the application
                        ProcessOutput = Process.StandardOutput.ReadToEnd();

                        if (string.IsNullOrEmpty(outputFile))
                        {
                            Logger.LogInformation(
                                "--> Output:");
                            Logger.LogInformation(
                                ProcessOutput);
                        }
                        else
                        {
                            Logger.LogInformation(
                                "--> Output: {0}",
                                outputFile);

                            if (AppendToFile)
                            {
                                File.AppendAllText(
                                    outputFile,
                                    ProcessOutput);
                            }
                            else
                            {
                                File.WriteAllText(
                                    outputFile,
                                    ProcessOutput);
                            }
                        }
                    }

                    if (RedirectStandardError &&
                        Process.StandardError != null)
                    {
                        // Read the error output of the application
                        ProcessError = Process.StandardError.ReadToEnd();
                    }

                    if (WaitForExit)
                    {
                        if (WaitForExitMilliSeconds < 0)
                        {
                            Logger.LogInformation(
                                "--> Waiting for exit");
                            Process.WaitForExit();

                            if (LogAnyway)
                            {
                                Logger.LogInformation(
                                    "Process exit");
                                Logger.LogInformation(
                                    "--> Exit value: {0}",
                                    Process.ExitCode);
                            }
                        }
                        else
                        {
                            Logger.LogInformation(
                                "--> Waiting {0} ms for exit",
                                WaitForExitMilliSeconds);
                            Process.WaitForExit(WaitForExitMilliSeconds);
                        }
                    }
                }

                return ProcessOutput;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Unable to wait for process to execute command " +
                    psi.FileName + " " +
                    psi.Arguments, ex);
            }
        }

        public void Kill()
        {
            if (Process != null)
                Process.Kill();
        }

        public int GetExitCode()
        {
            if (Process != null)
                return Process.ExitCode;

            return 1;
        }
        #endregion

        #region Miscellaneous
        /***********************************************************/
        public string? GetOutput()
        {
            return ProcessOutput;
        }

        public string? GetError()
        {
            return ProcessError;
        }
        #endregion
    }
}

