create procedure [dbo].[UpsertServiceStantion]
  @id int,
  @name nvarchar(200)
as
begin
  if (@id = 0)
  begin
    insert into ServiceStantion values(@name)
  end
  else
  begin
    update ServiceStantion
    set [Name] = @name
    where Id = @id
  end
end
