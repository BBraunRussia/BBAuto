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
    set mailText_name = @name,
        mailText_text = @text
    where mailText_id = @id
  end

  select @id
end
