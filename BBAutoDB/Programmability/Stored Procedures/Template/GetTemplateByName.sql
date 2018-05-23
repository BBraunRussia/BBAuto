create procedure [dbo].[GetTemplateByName]
  @name nvarchar(50)
as
begin
  select
    Id,
    [Path]
  from
    Template
  where
    [Name] = @name
end
