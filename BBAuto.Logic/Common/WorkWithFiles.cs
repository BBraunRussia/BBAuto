using System.Diagnostics;
using System.IO;

namespace BBAuto.Logic.Common
{
  public static class WorkWithFiles
  {
    internal static string FileCopy(string file, string folderName, string newFileName)
    {
      if (string.IsNullOrEmpty(file))
        return string.Empty;

      return RunCopy(file, folderName, newFileName);
    }

    internal static string FileCopyById(string file, string idType, int id, string subFolderName, string newFileName)
    {
      if (string.IsNullOrEmpty(file))
        return string.Empty;

      string folderName = GetFolderName(idType, id, subFolderName);

      return RunCopy(file, folderName, newFileName);
    }

    private static string RunCopy(string file, string folderName, string newFileName)
    {
      string distPath = GetDistPath(file, folderName, newFileName);

      if (!Directory.Exists(Path.GetDirectoryName(distPath)))
        Directory.CreateDirectory(Path.GetDirectoryName(distPath));

      if (!File.Exists(distPath))
        File.Copy(file, distPath, true);

      return distPath;
    }

    private static string GetFolderName(string idType, int id, string subFolderName)
    {
      if (id == 0)
        return string.Empty;

      var idString = @"\" + id;

      if (subFolderName != string.Empty)
        subFolderName = @"\" + subFolderName;

      return idType + idString + subFolderName;
    }

    private static string GetDistPath(string file, string folderName, string newFileName)
    {
      string fileExt = GetFileExt(file);

      return @"\\bbmru08.bbmag.bbraun.com\programs\Utility\BBAuto\files\" + folderName + @"\" + newFileName + fileExt;
    }

    private static string GetFileExt(string fileName)
    {
      return Path.GetExtension(fileName);
    }

    public static void Delete(string file)
    {
      if (!string.IsNullOrEmpty(file))
        File.Delete(file);
    }

    public static void OpenFile(string file)
    {
      if (!string.IsNullOrEmpty(file))
        Process.Start(file);
    }
  }
}
