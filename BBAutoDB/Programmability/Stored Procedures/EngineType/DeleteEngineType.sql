create procedure [dbo].[DeleteEngineType]
  @id int
as
begin
  delete from EngineType
  where Id = @id
end
