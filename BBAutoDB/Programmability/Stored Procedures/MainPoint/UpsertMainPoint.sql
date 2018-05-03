create procedure [dbo].[UpsertMainPoint]
  @id int
as
begin
  insert into MainPoint values(@id)
end
