create procedure [dbo].[GetMailTexts]
as
begin
  select Id, [Name], [Text] from MailText
end
