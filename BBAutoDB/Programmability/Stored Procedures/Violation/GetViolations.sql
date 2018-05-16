create procedure [dbo].[GetViolations]
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
