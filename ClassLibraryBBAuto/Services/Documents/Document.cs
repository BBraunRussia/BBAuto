using System;
using System.ComponentModel;
using System.IO;
using BBAuto.Domain.Common;

namespace BBAuto.Domain.Services.Documents
{
  public class Document
  {
    private string _path;

    [Browsable(false)]
    public int Id { get; set; }
    [DisplayName("Название")]
    public string Name { get; set; }

    [DisplayName("Файл")]
    public string Path
    {
      get => _path;
      set
      {
        if (!string.IsNullOrEmpty(_path) && _path != value)
          DeleteFile(_path);

        _path = WorkWithFiles.fileCopy(value, "Documents", System.IO.Path.GetFileNameWithoutExtension(value));
      }
    }
    [DisplayName("Тип")]
    public bool Instruction { get; set; }

    private void DeleteFile(string filePath)
    {
      try
      {
        WorkWithFiles.Delete(filePath);
      }
      catch (Exception)
      {
        // ignored
      }
    }
  }
}
