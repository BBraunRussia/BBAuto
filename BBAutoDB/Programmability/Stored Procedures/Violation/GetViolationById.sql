create procedure dbo.GetViolationById
  @id int
as
  select
    Id,
    CarId,
    [Date],
    Number,
    [File],
    DatePay,
    FilePay,
    ViolationTypeId,
    [Sum],
    [Sent],
    NoDeduction,
    Agreed,
    DateCreate
  from
    Violation
  where
    Id = @id