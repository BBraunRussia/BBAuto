create procedure [dbo].[GetDiagCardById]
  @idCar int
as
  select
    Id,
    CarId,
    Number,
    DateEnd,
    [File],
    NotificationSent
  from
    DiagCard
  where
    CarId = @idCar
