create procedure [dbo].[GetDiagCardsByDate]
  @dateFrom datetime
as
  select
    dc.Id,
    dc.CarId,
    dc.Number,
    dc.DateEnd,
    dc.[File],
    dc.NotificationSent
  from
    dbo.DiagCard dc
  left join
    dbo.SaleCar sc on sc.CarId = dc.CarId
  where
    dc.DateEnd >= @dateFrom and sc.CarId is null
