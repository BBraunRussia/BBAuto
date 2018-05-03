create procedure [dbo].[DeleteEngineType]
  @id int
as
begin
  delete from EngineType
  where EngineType_id = @id
end
