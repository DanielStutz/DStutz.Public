namespace DStutz.System.Commands
{
    public class CommandPDFLatex : Command
    {
        #region Properties (unused yet)
        /***********************************************************/
        public static readonly string BATCH_MODE = "batchmode";
        public static readonly string ERRORSTOP_MODE = "errorstopmode";
        public static readonly string NONSTOP_MODE = "nonstopmode";
        public static readonly string SCROLL_MODE = "scrollmode";
        #endregion

        #region Constructors
        /***********************************************************/
        public CommandPDFLatex()
            : base("pdflatex.exe", "texlive") { }
        #endregion

        #region Methods working
        /***********************************************************/
        public void Pdflatex(
            DirectoryInfo workingDir,
            DirectoryInfo outputDir,
            string fileNameTEX,
            string fileNamePDF)
        {
            Pdflatex(
                workingDir, outputDir,
                fileNameTEX, fileNamePDF,
                "aux", "log");
        }

        // TODO Add mode?!
        //public void pdflatex(
        //    DirectoryInfo workingDir,
        //    DirectoryInfo outputDir,
        //    string fileNameTEX,
        //    String fileNamePDF,
        //    String mode,
        //    params string[] extToBeRemoved)
        //{
        //}

        public void Pdflatex(
            DirectoryInfo workingDir,
            DirectoryInfo outputDir,
            string fileNameTEX,
            string fileNamePDF,
            params string[] extToBeRemoved)
        {
            var jobname = fileNamePDF.Replace(".pdf", "");

            List<string> arguments = new()
            {
                //"-shell-escape",
                //"-interaction=" + mode,
                "-output-directory=" + outputDir.FullName,
                "-jobname=" + jobname,
                fileNameTEX
            };

            Handler.Execute(
                Program,
                arguments,
                workingDir.FullName);

            LogFileAction(
                jobname + ".pdf",
                "latexed");

            try
            {
                if (extToBeRemoved != null)
                {
                    foreach (var ext in extToBeRemoved)
                    {
                        File.Delete(
                            Path.Combine(
                                outputDir.FullName,
                                jobname + "." + ext));

                        LogFileAction(
                            jobname + "." + ext,
                            "deleted");
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception(
                    "Unable to delete files in " +
                    outputDir.FullName);
            }
        }
        #endregion

        #region Methods testing
        /***********************************************************/
        public override void Test()
        {
            base.Test();

            Pdflatex(
                TestDir,          // Working directory
                TestDir,          // Output directory
                "Important.tex",  // Template
                "Important.pdf"); // Output
        }
        #endregion
    }
}
