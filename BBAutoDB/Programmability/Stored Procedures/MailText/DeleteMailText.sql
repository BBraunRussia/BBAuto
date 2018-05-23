create procedure [dbo].[DeleteMailText]
  @id int
as
begin
  delete from MailText
  where Id = @id
end
