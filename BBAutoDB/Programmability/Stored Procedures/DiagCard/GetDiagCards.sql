create procedure [dbo].[GetDiagCards]
as
  select
    Id,
    CarId,
    Number,
    [Date],
    [File],
    NotificationSent
  from
    DiagCard
