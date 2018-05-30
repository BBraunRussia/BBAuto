create procedure [dbo].[GetViolationsByDate]
  @date datetime
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
    DateCreate = @date
