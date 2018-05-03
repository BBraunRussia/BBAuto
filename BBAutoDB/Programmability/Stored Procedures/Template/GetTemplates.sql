create procedure [dbo].[GetTemplates]
as
begin
  select Template_id, Template_name, Template_path from Template
end
