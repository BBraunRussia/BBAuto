using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace BBAuto.Domain.Common
{
  public static class WorkWithFiles
  {
    private static string CurrentDirectory => Directory.GetCurrentDirectory() + @"\files\";

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
      var localPath = getDistPath(file, folderName, newFileName);
      var distPath = GetFullPath(localPath);

      if (!Directory.Exists(Path.GetDirectoryName(distPath)))
        Directory.CreateDirectory(Path.GetDirectoryName(distPath) ?? "");

      var fullFilePath = Path.IsPathRooted(file) ? file : GetFullPath(file);

      if (!File.Exists(distPath))
        File.Copy(fullFilePath, distPath, true);

      return localPath;
    }

    private static string getFolderName(string idType, int id, string subFolderName)
    {
      if (id == 0)
        return string.Empty;

      string idString = @"\" + id;

      if (subFolderName != string.Empty)
        subFolderName = @"\" + subFolderName;

      return idType + idString + subFolderName;
    }

    private static string getDistPath(string file, string folderName, string newFileName)
    {
      string fileExt = getFileExt(file);

      return folderName + @"\" + newFileName + fileExt;
    }

    private static string getFileExt(string fileName)
    {
      return Path.GetExtension(fileName);
    }

    public static void Delete(string file)
    {
      if (string.IsNullOrEmpty(file))
        return;

      var fullPath = GetFullPath(file);
      File.Delete(fullPath);
    }

    public static void openFile(string file)
    {
      var filePath = GetFullPath(file);

      try
      {
        if (!string.IsNullOrEmpty(file))
          Process.Start(filePath);
      }
      catch (Exception e)
      {
        MessageBox.Show($"{e.Message}. Файл {filePath}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    public static string GetFullPath(string localPath)
    {
      return CurrentDirectory + localPath;
    }
  }
}
