create procedure [dbo].[DeleteViolationById]
  @id int
as
  declare @filePay nvarchar(max)

  select
    @filePay = filePay
  from
    Violation
  where
    Id = @id

  delete from Violation where Id = @id

  exec InsertHistory 'Violation', @id, 'Delete', @filePay