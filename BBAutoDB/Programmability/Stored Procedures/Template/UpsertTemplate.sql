create procedure [dbo].[UpsertTemplate]
  @idTemplate int,
  @name nvarchar(50),
  @path nvarchar(200)
as
begin
  if (@idTemplate = 0)
    insert into Template values(@name, @path)
  else
    update Template
    set Template_name = @name,
        Template_path = @path
    where Template_id = @idTemplate
end
