create procedure [dbo].[DeleteDTPFile]
  @id int
as
begin
  delete from dtpFile
  where Id = @id
end
