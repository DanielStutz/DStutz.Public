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
            : base("pdflatex") { }
        #endregion

        #region Methods working
        /***********************************************************/
        public void Pdflatex(
            DirectoryInfo workingDir,
            DirectoryInfo outputDir,
            string texFile,
            string pdfName)
        {
            Pdflatex(workingDir, outputDir, texFile, pdfName, "aux", "log");
        }

        // TODO Add mode?!
        //public void pdflatex(
        //    DirectoryInfo workingDir,
        //    DirectoryInfo outputDir,
        //    string texFile,
        //    String pdfName,
        //    String mode,
        //    params string[] extToBeRemoved)
        //{
        //}

        public void Pdflatex(
            DirectoryInfo workingDir,
            DirectoryInfo outputDir,
            string texFile,
            string pdfName,
            params string[] extToBeRemoved)
        {
            List<string> arguments = new()
            {
                //"-shell-escape",
                //"-interaction=" + mode,
                "-output-directory=" + outputDir.FullName,
                "-jobname=" + pdfName,
                texFile
            };

            Handler.Execute(
                Program,
                arguments,
                workingDir.FullName);

            LogFileAction(pdfName + ".pdf", "latexed");

            try
            {
                if (extToBeRemoved != null)
                {
                    foreach (var ext in extToBeRemoved)
                    {
                        File.Delete(
                            Path.Combine(
                                outputDir.FullName,
                                pdfName + "." + ext));

                        LogFileAction(
                            pdfName + "." + ext,
                            "deleted");
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception(
                    "Unable to delete files in " + outputDir);
            }
        }
        #endregion

        #region Methods testing
        /***********************************************************/
        public override void Test()
        {
            Pdflatex(
                TestspaceDir,
                TestspaceDir,
                "CommandPDFLatex.tex",
                "CommandPDFLatex");
        }
        #endregion
    }
}
