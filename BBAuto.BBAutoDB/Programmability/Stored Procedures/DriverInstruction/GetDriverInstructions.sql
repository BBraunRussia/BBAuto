create procedure [dbo].[GetDriverInstructions]
as
  select
    Id,
    DocumentId,
    DriverId,
    Date
  from
    DriverInstruction
  order by Date desc
