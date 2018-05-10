create procedure [dbo].[GetDiagCardById]
  @id int
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
    Id = @id
