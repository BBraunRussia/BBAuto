create procedure [dbo].[GetPTSes]
as
begin
  select
    CarId,
    Number,
    [Date],
    GiveOrg,
    [File]
  from
    PTS
end
