create procedure [dbo].[UpsertModel]
  @id int,
  @Name nvarchar(50),
  @MarkId int
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(Id)
    from
      Model
    where
      [Name] = @Name
      and MarkId = @MarkId

    if (@count = 0)
    begin
      insert into Model values(@Name, @MarkId)
      exec dbo.GetModelById @id
    end
    else
      select 'Модель автомобиля с таким названием уже существует'
  end
  else
  begin
    update Model
    set [Name] = @Name,
        MarkId = @MarkId
    where Id = @id
    exec dbo.GetModelById @id
  end
end
