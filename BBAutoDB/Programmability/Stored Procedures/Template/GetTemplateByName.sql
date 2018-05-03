create procedure [dbo].[GetTemplateByName]
  @name nvarchar(50)
as
begin
  select
    Template_id,
    Template_path
  from
    Template
  where
    Template_name = @name
end
