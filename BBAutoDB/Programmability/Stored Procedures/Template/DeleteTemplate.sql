create procedure [dbo].[DeleteTemplate]
  @idTemplate int
as
begin
  delete from Template
  where template_id = @idTemplate
end
