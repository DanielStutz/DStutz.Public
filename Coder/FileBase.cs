using DStutz.System.Commands;
using DStutz.System.IO;

namespace DStutz.Coder
{
    public abstract class FileBase : CodeBlock
    {
        #region Properties
        /***********************************************************/
        public string FileName { get; }
        #endregion

        #region Constructors
        /***********************************************************/
        public FileBase(
            string fileName,
            string fileTemplate)
            : base(fileTemplate)
        {
            FileName = fileName;
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
