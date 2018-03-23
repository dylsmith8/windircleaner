using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace DirectoryHelper
{
    public static class MoveFilesEngine
    {
        public static void FindAndMoveFilesAsync(string directory, string fileWhiteList)
        {
            string[] files = Directory.GetFiles(directory);

            List<string> supportedFileTypes = ParseWhiteList(fileWhiteList);

            foreach (string file in files)
            {
                string fileType = Path.GetExtension(file);

                if (!supportedFileTypes.Contains(fileType))
                {
                    continue;
                }

                MoveFile(file, fileType);
            }         
        }

        private static void MoveFile(string file, string fileType)
        {
            string fileName = Path.GetFileName(file);

            switch (fileType.ToUpperInvariant())
            {
                case ".PDF":
                    File.Move(file, string.Format(CultureInfo.CurrentCulture, @"C:\Users\smith83\Documents\DocumentDownloads\{0}", fileName));
                    break;
                case ".EXE":
                    File.Move(file, string.Format(CultureInfo.CurrentCulture, @"C:\Users\smith83\Documents\ProgramDownloads\{0}", fileName));
                    break;
            }
        }

        private static List<string> ParseWhiteList(string fileWhiteList)
        {
            return fileWhiteList.Split('/').ToList();
        }
    }
}
