create procedure [dbo].[GetViolationList]
as
  select
    violation_id,
    car_id,
    violation_date,
    violation_number,
    violation_file,
    violation_datePay,
    violation_filePay,
    violationType_id,
    violation_sum,
    violation_sent,
    violation_noDeduction,
    violation_agreed,
    violation_dateCreate
  from
    Violation
