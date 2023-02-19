using DStutz.System.Commands;
using DStutz.System.IO;

namespace DStutz.Coder
{
    public abstract class FileBase : CodeBlock
    {
        #region Properties
        /***********************************************************/
        public string FileName { get; }
        public string Warning { get; } = "";
        public string Version { get; } = "";
        #endregion

        #region Constructors
        /***********************************************************/
        public FileBase(
            string codeName,
            string codeType,
            string codeTemplate,
            string? warning = null,
            string? version = null)
            : base(codeTemplate)
        {
            FileName = $"{codeType}_{codeName}.cs";

            if (warning != null)
                Warning = $"!!! {warning} !!!";

            if (version != null)
                Version = version;
        }
        #endregion

        #region Methods
        /***********************************************************/
        protected virtual void PostProcessing()
        {
            Replace("VERSION", Version);
            Replace("WARNING", Warning);
        }
        #endregion

        #region Methods handling file
        /***********************************************************/
        private static CommandTextPad TextPad { get; } = new CommandTextPad();

        public FileInfo Safe()
        {
            return Safe("C:/Workspace");
        }

        public FileInfo Safe(
            string dir)
        {
            var info = new FileInfo(dir + "/" + FileName);

            FileWriter.WriteAllText(GetCode(true), info);

            return info;
        }

        public void SafeAndOpenWithTextPad()
        {
            TextPad.Open(Safe().FullName);
        }

        public void SafeAndOpenWithTextPad(
            string dir)
        {
            TextPad.Open(Safe(dir).FullName);
        }
        #endregion
    }
}
