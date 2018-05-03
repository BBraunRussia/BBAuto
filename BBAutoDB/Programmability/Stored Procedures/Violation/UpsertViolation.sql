create procedure [dbo].[UpsertViolation]
  @idViolation int,
  @idCar int,
  @date datetime,
  @number nvarchar(50),
  @file nvarchar(200),
  @datePay nvarchar(50),
  @filePay nvarchar(200),
  @idViolationType int,
  @sum int,
  @sent int,
  @noDeduction int,
  @agreed nvarchar(5) = 'False'
as
begin
  declare @event nvarchar(50)

  if (@datePay = '')
    set @datePay = null
  else
    set @datePay = cast(@datePay as datetime)

  if (@idViolation = 0)
  begin
    declare @count int
    select
      @count = count(violation_id)
    from
      Violation
    where
      violation_date = @date
      and violation_number = @number

    insert into Violation values(@idCar, @date, @number, @file, @datePay, @filePay, @idViolationType, @sum, 0, @noDeduction, @agreed, current_timestamp)

    set @event = 'insert'

    set @idViolation = scope_identity()
  end
  else
  begin
    update Violation
    set violation_date = @date,
        violation_number = @number,
        violation_file = @file,
        violation_datePay = @datePay,
        violation_filePay = @filePay,
        violationType_id = @idViolationType,
        violation_sum = @sum,
        violation_sent = @sent,
        violation_noDeduction = @noDeduction,
        violation_agreed = @agreed
    where violation_id = @idViolation

    set @event = 'update'
  end

  exec InsertHistory 'Violation', @idViolation, @event, @filePay

  select @idViolation
end
