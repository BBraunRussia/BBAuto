create procedure [dbo].[DeleteMailText]
  @id int
as
begin
  delete from MailText
  where mailText_id = @id
end
