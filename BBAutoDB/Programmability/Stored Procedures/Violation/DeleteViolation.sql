create procedure [dbo].[DeleteViolation]
  @idViolation int
as
begin
  declare @filePay nvarchar(max)

  select
    @filePay = violation_filePay
  from
    Violation
  where
    violation_id = @idViolation

  delete from Violation
  where violation_id = @idViolation

  exec InsertHistory 'Violation', @idViolation, 'Delete', @filePay
end
