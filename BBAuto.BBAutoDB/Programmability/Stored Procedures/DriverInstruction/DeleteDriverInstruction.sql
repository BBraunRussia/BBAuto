create procedure [dbo].[DeleteDriverInstruction]
  @id int
as
  delete from DriverInstruction where Id = @id
