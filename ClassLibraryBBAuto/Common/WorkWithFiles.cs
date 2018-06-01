using System.Diagnostics;
using System.IO;

namespace BBAuto.Domain.Common
{
  public static class WorkWithFiles
  {
    private const string BaseFolder = @"\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\";

    internal static string fileCopy(string file, string folderName, string newFileName)
    {
      if (string.IsNullOrEmpty(file))
        return string.Empty;

      return runCopy(file, folderName, newFileName);
    }

    internal static string fileCopyByID(string file, string idType, int id, string subFolderName, string newFileName)
    {
      if (string.IsNullOrEmpty(file))
        return string.Empty;

      string folderName = getFolderName(idType, id, subFolderName);

      return runCopy(file, folderName, newFileName);
    }

    private static string runCopy(string file, string folderName, string newFileName)
    {
      string distPath = getDistPath(file, folderName, newFileName);

      if (!Directory.Exists(Path.GetDirectoryName(distPath)))
        Directory.CreateDirectory(Path.GetDirectoryName(distPath));

      if (!File.Exists(distPath))
        File.Copy(file, distPath, true);

      return distPath;
    }

    private static string getFolderName(string idType, int id, string subFolderName)
    {
      if (id == 0)
        return string.Empty;

      string idString = @"\" + id.ToString();

      if (subFolderName != string.Empty)
        subFolderName = @"\" + subFolderName;

      return idType + idString + subFolderName;
    }

    private static string getDistPath(string file, string folderName, string newFileName)
    {
      string fileExt = WorkWithFiles.getFileExt(file);

      return BaseFolder + folderName + @"\" + newFileName + fileExt;
    }

    private static string getFileExt(string fileName)
    {
      return Path.GetExtension(fileName);
    }

    public static void Delete(string file)
    {
      if (!string.IsNullOrEmpty(file))
        File.Delete(file);
    }

    public static void openFile(string file)
    {
      if (!string.IsNullOrEmpty(file))
        Process.Start(file);
    }
  }
}
