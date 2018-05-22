create procedure [dbo].[GetMarks]
as
  select
    Id,
    [Name]
  from
    dbo.Mark
  order by
    [Name]
