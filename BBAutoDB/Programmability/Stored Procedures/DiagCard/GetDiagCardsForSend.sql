create procedure dbo.GetDiagCardsForSend
  @dateEnd datetime
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
    Year(dc.DateEnd) = Year(@dateEnd) and
    Month(dc.DateEnd) = Month(@dateEnd) and
    dc.NotificationSent = 0 and
    sc.CarId is null
