create procedure [dbo].[DeleteDTPFile]
  @idDtpFile int
as
begin
  delete from dtpFile
  where dtpFile_id = @idDtpFile
end
