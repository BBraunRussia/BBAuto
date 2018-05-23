create procedure [dbo].[GetSTSes]
as
begin
  select
    CarId,
    Number,
    [Date],
    GiveOrg,
    [File]
  from
    STS
end
