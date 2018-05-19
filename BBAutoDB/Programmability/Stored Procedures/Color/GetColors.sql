create procedure [dbo].[GetColors]
as
  select
    Id,
    [Name]
  from
    Color
  order by
    [Name]
