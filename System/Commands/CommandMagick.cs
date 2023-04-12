namespace DStutz.System.Commands
{
    public class CommandMagick : Command
    {
        #region Constructors
        /***********************************************************/
        public CommandMagick()
            : base("magick.exe") { }
        #endregion

        #region Methods extending
        /***********************************************************/
        // See https://legacy.imagemagick.org/Usage/crop/#extent
        public void ConvertExtent(
            string fileNameImage,
            string size,
            string fileNameConvertedImage)
        {
            ConvertExtent(
                WorkspaceDir,
                fileNameImage,
                size,
                fileNameConvertedImage);
        }

        public void ConvertExtent(
            DirectoryInfo workingDir,
            string fileNameImage,
            string size,
            string fileNameConvertedImage)
        {
            List<string> arguments = new()
            {
                "convert",
                // Gravity: center, north, south, east, west
                "-gravity", "center",
                "-extent", size,
                fileNameImage,
                fileNameConvertedImage,
            };

            Handler.Execute(
                Program,
                arguments,
                workingDir.FullName);
        }

        public void ConvertExtent(
            string fileNameImage,
            string size,
            string backgroundColor,
            string fileNameConvertedImage)
        {
            ConvertExtent(
                WorkspaceDir,
                fileNameImage,
                size,
                backgroundColor,
                fileNameConvertedImage);
        }

        public void ConvertExtent(
            DirectoryInfo workingDir,
            string fileNameImage,
            string size,
            string backgroundColor,
            string fileNameConvertedImage)
        {
            List<string> arguments = new()
            {
                "convert",
                // Gravity: center, north, south, east, west
                "-gravity", "center",
                "-extent", size,
                "-background", backgroundColor,
                fileNameImage,
                fileNameConvertedImage,
            };

            Handler.Execute(
                Program,
                arguments,
                workingDir.FullName);
        }
        #endregion

        #region Methods resizing
        /***********************************************************/
        // See https://legacy.imagemagick.org/Usage/resize/#resize
        public void ConvertResize(
            string fileNameImage,
            string size,
            string fileNameConvertedImage)
        {
            ConvertResize(
                WorkspaceDir,
                fileNameImage,
                size,
                fileNameConvertedImage);
        }
        public void ConvertResize(
            DirectoryInfo workingDir,
            string fileNameImage,
            string size,
            string fileNameConvertedImage)
        {
            List<string> arguments = new()
            {
                "convert",
                "-resize", size,
                fileNameImage,
                fileNameConvertedImage,
            };

            Handler.Execute(
                Program,
                arguments,
                workingDir.FullName);
        }
        #endregion

        #region Methods resizing
        /***********************************************************/
        // See https://imagemagick.org/script/identify.php
        public string? Identify(
            string fileNameImage)
        {
            return Identify(WorkspaceDir, fileNameImage);
        }
        public string? Identify(
            DirectoryInfo workingDir,
            string fileNameImage)
        {
            List<string> arguments = new()
            {
                "identify",
                fileNameImage,
            };

            return Handler.Execute(
                Program,
                arguments,
                workingDir.FullName);
        }
        #endregion

        #region Methods testing
        /***********************************************************/
        public override void Test()
        {
            base.Test();

            // Nick.png PNG 750x1000 750x1000+0+0 8-bit sRGB 811958B 0.000u 0:00.002
            Identify(
                TestDir,
                "Nick.png");

            ConvertResize(
                TestDir,
                "Nick.png",
                "375x500",
                "NickResized.jpg");

            ConvertResize(
                TestDir,
                "Nick.png",
                "375x500",
                "NickResized.png");

            ConvertExtent(
                TestDir,
                "Nick.png",
                "850x1100",
                "NickExtendedWhite.jpg");

            ConvertExtent(
                TestDir,
                "Nick.png",
                "850x1100",
                "skyblue",
                "NickExtendedSkyblue.png");
        }
        #endregion
    }
}
