namespace DStutz.System.Commands
{
    public class CommandMagick : Command
    {
        /**********************************************************************
         * Constructors
         **********************************************************************/
        public CommandMagick()
            : base("magick") { }

        /**********************************************************************
         * Methods - http://www.imagemagick.org/Usage/crop/#extent
         **********************************************************************/
        public void ConvertExtent(
            string image,
            string size,
            string imageConverted,
            string? workingDir = null)
        {
            Handler.Execute(
                Program,
                new[]
                {
                    image,
                    "-gravity", "center",
                    "-extent", size,
                    imageConverted
                },
                workingDir);
        }

        public void ConvertExtent(
            string image,
            string size,
            string backgroundColor,
            string imageConverted,
            string? workingDir = null)
        {
            Handler.Execute(
                Program,
                new[]
                {
                    image,
                    "-gravity", "center",
                    "-extent", size,
                    "-background", backgroundColor,
                    imageConverted
                },
                workingDir);
        }

        /**********************************************************************
         * Methods - http://www.imagemagick.org/Usage/resize/#resize
         **********************************************************************/
        public void ConvertResize(
            string image,
            string size,
            string imageConverted,
            string? workingDir = null)
        {
            Handler.Execute(
                Program,
                new[]
                {
                    image,
                    "-resize ", size,
                    imageConverted
                },
                workingDir);
        }
    }
}
