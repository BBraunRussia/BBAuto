create procedure [dbo].[UpsertMailText]
  @id int,
  @name nvarchar(50),
  @text nvarchar(500)
as
begin
  if (@id = 0)
  begin
    insert into MailText values(@name, @text)

    set @id = scope_identity()
  end
  else
  begin
    update MailText
    set [Name] = @name,
        [Text] = @text
    where Id = @id
  end

  select @id
end
