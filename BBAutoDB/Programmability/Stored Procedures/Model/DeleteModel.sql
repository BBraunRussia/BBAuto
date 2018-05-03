create procedure [dbo].[DeleteModel]
  @id int
as
begin
  declare @count int

  select
    @count = count(model_id)
  from
    Grade
  where
    model_id = @id

  if (@count = 0)
  begin
    delete from Model
    where model_id = @id
    select 'Удален'
  end
  else
  begin
    select
      'Удаление невозможно. Надено зависимых записей: ' + cast(@count as nvarchar(50)) + ' шт'
  end
end
