create procedure [dbo].[GetMailTexts]
as
begin
  select mailText_id, mailText_name, mailText_text from MailText
end
