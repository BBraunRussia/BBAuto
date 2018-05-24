create procedure [dbo].[GetRepairTypes]
as
  select
    Id,
    [Name]
  from
    RepairType
  order by [Name]
