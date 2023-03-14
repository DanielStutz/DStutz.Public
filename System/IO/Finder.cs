namespace DStutz.System.IO
{
    public abstract class Finder
    {
        public static DirectoryInfo FindSolutionDirectoryOrThrow(
            IHostEnvironment he,
            string solutionName)
        {
            var parent = new DirectoryInfo(he.ContentRootPath);

            while (parent.Exists)
            {
                if (parent.Parent == null)
                    break;

                // Find e.g. dir 'DStutz' with file 'DStutz.sln'
                foreach (var child in parent.EnumerateDirectories())
                    if (child.Name.Equals(solutionName))
                        foreach (var file in child.EnumerateFiles())
                            if (file.Name.Equals($"{solutionName}.sln"))
                                return child;

                parent = parent.Parent;
            }

            throw new Exception(
                $"Unable to find solution '{solutionName}.sln'");
        }

        public static DirectoryInfo FindDirectory(
            bool isMandatory,
            params string[] paths)
        {
            var dir = new DirectoryInfo(Path.Combine(paths));

            if (isMandatory && !dir.Exists)
                throw new Exception(
                    $"Unable to find dir '{dir.FullName}'");

            return dir;
        }

        public static FileInfo FindFile(
            bool isMandatory,
            params string[] paths)
        {
            var file = new FileInfo(Path.Combine(paths));

            if (isMandatory && !file.Exists)
                throw new Exception(
                    $"Unable to find file '{file.FullName}'");

            return file;
        }
    }
}
