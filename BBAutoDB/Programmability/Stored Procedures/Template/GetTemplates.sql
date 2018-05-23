create procedure [dbo].[GetTemplates]
as
begin
  select Id, [Name], [Path] from Template
end
