create procedure [dbo].[GetDiagCardById]
  @idCar int
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
  where
    CarId = @idCar
