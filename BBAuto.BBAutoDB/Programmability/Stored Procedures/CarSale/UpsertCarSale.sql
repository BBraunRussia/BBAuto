create procedure [dbo].[UpsertCarSale]
  @carId int,
  @comment varchar(100),
  @date datetime,
  @customerId int
as
  declare @id int
  select @id = cs.car_id from CarSale cs where cs.car_id = @carId

  if (@id is null)
  begin
    insert into CarSale(car_id, carSale_comm, carSale_date, CustomerId)
      values(@carId, @comment, @date, @customerId)
  end
  else
  begin
    update
      CarSale
    set
      carSale_date = @date,
      carSale_comm = @comment,
      CustomerId = @customerId
    where
      car_id = @carId
  end

  exec dbo.GetCarSaleByCarId @carId
