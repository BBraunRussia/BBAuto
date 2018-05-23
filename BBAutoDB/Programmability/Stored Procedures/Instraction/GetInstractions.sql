create procedure [dbo].[GetInstractions]
as
begin
  select
    Id,
    Number,
    [Date],
    DriverId,
    [File]
  from
    Instraction
end
