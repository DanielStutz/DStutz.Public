using DStutz.System.Commands;
using DStutz.System.IO;

namespace DStutz.Coder
{
    public abstract class FileBase : CodeBlock
    {
        #region Properties
        /***********************************************************/
        public string FileName { get; }
        public string Version { get; } = "";
        public string Remarks { get; } = "";
        #endregion

        #region Constructors
        /***********************************************************/
        public FileBase(
            string codeName,
            string codeType,
            string codeTemplate,
            CodeInfo codeInfo,
            string version)
            : base(codeTemplate)
        {
            FileName = $"{codeType}_{codeName}.cs";
            Version = codeInfo.Version;

            if (codeInfo.Remarks != null &&
                codeInfo.Remarks.Count > 0)
                Remarks = $"!!! {string.Join(", ", codeInfo.Remarks)} !!!";

            if (!Version.Equals(version))
                throw new Exception(
                    $"Version {Version} of json file does not match " +
                    $"version {version} of class {GetType().Name}");
        }
        #endregion

        #region Methods
        /***********************************************************/
        protected virtual void PostProcessing()
        {
            Replace("VERSION", Version);
            Replace("REMARKS", Remarks);
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
