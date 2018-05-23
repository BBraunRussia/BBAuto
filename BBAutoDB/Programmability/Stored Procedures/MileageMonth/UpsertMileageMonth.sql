create procedure [dbo].[UpsertMileageMonth]
  @carNumber nvarchar(50),
  @date datetime,
  @gasCount float,
  @gasBegin float,
  @gasEnd float,
  @gasNorm float,
  @psn int,
  @psk int,
  @mileageCount int
as
begin

  declare @count int
  declare @carId int

  select
    @count = count(id)
  from
    Car
  where
    grz = @carNumber
  if (@count = 0)
  begin
    select 'Машины с таким номером нет в базе данных'
    return;
  end
  if (@count > 1)
  begin
    select 'Машин с таким номером больше 1'
    return;
  end

  select
    @carId = id
  from
    Car
  where
    grz = @carNumber

  select
    @count = count(*)
  from
    MileageMonth
  where
    CarId = @carId
    and [Date] = @date


  if (@count = 0)
    insert into MileageMonth(CarId, [Date], [Count], psn_count, psk_count, gas_count, gas_begin, gas_end, gas_norm)
      values(@carId, @date, @mileageCount, @psn, @psk, @gasCount, @gasBegin, @gasEnd, @gasNorm)
  else
    update MileageMonth
    set [Count] = @mileageCount,
        psn_count = @psn,
        psk_count = @psk,
        gas_count = @gasCount,
        gas_begin = @gasBegin,
        gas_end = @gasEnd,
        gas_norm = @gasNorm
    where CarId = @carId
    and [Date] = @date

  select '1'
end
