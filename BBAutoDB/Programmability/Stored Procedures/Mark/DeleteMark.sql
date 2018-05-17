create procedure [dbo].[DeleteMark]
  @id int
as
begin
  declare @count int

  select
    @count = count(*)
  from
    Model
  where
    MarkId = @id

  if (@count = 0)
  begin
    delete from Mark
    where Id = @id
    select 'Удален'
  end
  else
  begin
    select
      'Удаление невозможно. Надено зависимых записей: ' + cast(@count as nvarchar(50)) + ' шт'
  end
end
