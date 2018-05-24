create procedure [dbo].[GetRepairTypeById]
  @id int
as
  select
    Id,
    [Name]
  from
    RepairType
  where
    Id = @id
