create procedure [dbo].[DeleteModel]
  @id int
as
begin
  declare @count int

  select
    @count = count(Id)
  from
    Grade
  where
    ModelId = @id

  if (@count = 0)
  begin
    delete from Model
    where Id = @id
    select 'Удален'
  end
  else
  begin
    select
      'Удаление невозможно. Надено зависимых записей: ' + cast(@count as nvarchar(50)) + ' шт'
  end
end
