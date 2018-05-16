create procedure [dbo].[UpsertViolation]
  @id int,
  @carId int,
  @date datetime,
  @number nvarchar(50),
  @file nvarchar(200),
  @datePay nvarchar(50),
  @filePay nvarchar(200),
  @violationTypeId int,
  @sum int,
  @sent bit,
  @noDeduction bit,
  @agreed bit
as
begin
  declare @event nvarchar(50)

  if (@datePay = '')
    set @datePay = null
  else
    set @datePay = cast(@datePay as datetime)

  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(*)
    from
      Violation
    where
      [Date] = @date
      and Number = @number

    insert into Violation values(@carId, @date, @number, @file, @datePay, @filePay, @violationTypeId, @sum, 0, @noDeduction, @agreed, getdate())

    set @event = 'insert'

    set @id = scope_identity()
  end
  else
  begin
    update Violation
    set [Date] = @date,
        Number = @number,
        [File] = @file,
        DatePay = @datePay,
        FilePay = @filePay,
        violationTypeId = @violationTypeId,
        [Sum] = @sum,
        [Sent] = @sent,
        NoDeduction = @noDeduction,
        Agreed = @agreed
    where Id = @id

    set @event = 'update'
  end

  exec InsertHistory 'Violation', @id, @event, @filePay

  select @id
end
