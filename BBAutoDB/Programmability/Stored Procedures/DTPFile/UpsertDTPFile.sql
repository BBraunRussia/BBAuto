create procedure [dbo].[UpsertDTPFile]
  @idDtpFile int,
  @idDtp int,
  @name nvarchar(100),
  @file nvarchar(300)
as
begin
  if (@idDtpFile = 0)
  begin
    insert into dtpFile values(@idDtp, @name, @file)
    select @idDtpFile = @@identity
  end
  else
    update dtpFile
    set dtpFile_name = @name,
        dtpFile_file = @file
    where dtpFile_id = @idDtpFile

  select @idDtpFile
end
