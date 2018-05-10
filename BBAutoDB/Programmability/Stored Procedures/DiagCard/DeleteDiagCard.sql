create procedure [dbo].[DeleteDiagCard]
  @id int
as
  delete from DiagCard where Id = @id
