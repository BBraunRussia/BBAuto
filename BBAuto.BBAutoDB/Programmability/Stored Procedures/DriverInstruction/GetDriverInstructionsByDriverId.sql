create procedure [dbo].[GetDriverInstructionsByDriverId]
  @driverId int
as
  select
    Id,
    DocumentId,
    DriverId,
    Date
  from
    DriverInstruction
  where
    DriverId = @driverId
  order by Date desc

