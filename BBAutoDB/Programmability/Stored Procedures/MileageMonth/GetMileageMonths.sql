create procedure [dbo].[GetMileageMonths]
  @car int,
  @date datetime
as
begin
  select
    [Count],
    psn_count,
    psk_count,
    gas_count,
    gas_begin,
    gas_end,
    gas_norm
  from
    MileageMonth
  where
    CarId = @car
    and [Date] = @date
end
