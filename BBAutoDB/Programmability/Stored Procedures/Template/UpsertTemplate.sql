create procedure [dbo].[UpsertTemplate]
  @id int,
  @name nvarchar(50),
  @path nvarchar(200)
as
begin
  if (@id = 0)
    insert into Template values(@name, @path)
  else
    update Template
    set [Name] = @name,
        [Path] = @path
    where Id = @id
end
