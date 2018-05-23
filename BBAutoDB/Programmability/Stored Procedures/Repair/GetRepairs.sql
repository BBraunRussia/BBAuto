create procedure [dbo].[GetRepairs]
as
begin
  select
    Id,
    CarId,
    RepairTypeId,
    ServiceStantionId,
    [Date],
    Cost,
    [File]
  from
    Repair
end
