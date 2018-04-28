create procedure [dbo].[UpsertAccount]
  @idAccount int,
  @Number nvarchar(50),
  @Agreed int,
  @idPolicyType int,
  @idOwner int,
  @paymentNumber int,
  @businessTrip bit,
  @file nvarchar(500)
as
begin
  if (@idAccount = 0)
  begin
    insert into Account(Number, Agreed, PolicyTypeId, OwnerId, PaymentNumber, BusinessTrip, [File])
    values(@Number, 0, @idPolicyType, @idOwner, @paymentNumber, @businessTrip, @file)

    set @idAccount = scope_identity()
  end
  else
    update Account
    set number = @Number,
        Agreed = @Agreed,
        PolicyTypeId = @idPolicyType,
        OwnerId = @idOwner,
        PaymentNumber = @paymentNumber,
        BusinessTrip = @businessTrip,
        [File] = @file
    where Id = @idAccount

  select @idAccount
end
