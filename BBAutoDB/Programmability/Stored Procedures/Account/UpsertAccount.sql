create procedure [dbo].[UpsertAccount]
  @id int,
  @Number nvarchar(50),
  @Agreed bit,
  @PolicyTypeId int,
  @OwnerId int,
  @paymentNumber int,
  @businessTrip bit,
  @file nvarchar(100)
as
  if (@id = 0)
  begin
    insert into Account values(@Number, 0, @PolicyTypeId, @OwnerId, @paymentNumber, @businessTrip, @file)

    set @id = scope_identity()
  end
  else
    update
      Account
    set
      Number = @Number,
      Agreed = @Agreed,
      PolicyTypeId = @PolicyTypeId,
      OwnerId = @OwnerId,
      PaymentNumber = @paymentNumber,
      BusinessTrip = @businessTrip,
      [File] = @file
    where
      Id = @id

  select @id
