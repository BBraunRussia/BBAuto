create procedure [dbo].[GetColumnSizes]
as
begin
  select
    DriverId,
    StatusId,
    column0,
    column1,
    column2,
    column3,
    column4,
    column5,
    column6,
    column7,
    column8,
    column9,
    column10,
    column11,
    column12,
    column13,
    column14,
    column15,
    column16
  from
    ColumnSize
end
