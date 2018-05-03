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
  declare @carID int

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
    @carID = id
  from
    Car
  where
    grz = @carNumber

  select
    @count = count(MileageMonth_id)
  from
    MileageMonth
  where
    car_id = @carID
    and MileageMonth_date = @date


  if (@count = 0)
    insert into MileageMonth(car_id, MileageMonth_date, MileageMonth_count, psn_count, psk_count, gas_count, gas_begin, gas_end, gas_norm) values(@carID, @date, @mileageCount, @psn, @psk, @gasCount, @gasBegin, @gasEnd, @gasNorm)
  else
    update MileageMonth
    set MileageMonth_count = @mileageCount,
        psn_count = @psn,
        psk_count = @psk,
        gas_count = @gasCount,
        gas_begin = @gasBegin,
        gas_end = @gasEnd,
        gas_norm = @gasNorm
    where car_id = @carID
    and MileageMonth_date = @date

  select '1'
end
