create procedure [dbo].[UpsertCustomer]
  @id int,
  @firstName nvarchar(50),
  @lastName nvarchar(50),
  @secondName nvarchar(50),
  @passportNumber nvarchar(12),
  @passportGiveOrg nvarchar(200),
  @passportGiveDate datetime,
  @address nvarchar(200),
  @inn nvarchar(12)
as
  if (@id = 0)
  begin
    insert into Customer(FirstName, LastName, SecondName, PassportNumber, PassportGiveOrg, PassportGiveDate, Address, Inn)
      values(@firstName, @lastName, @secondName, @passportNumber, @passportGiveOrg, @passportGiveDate, @address, @inn)
  end
  begin
    update
      Customer
    set
      FirstName = @firstName,
      LastName = @lastName,
      SecondName = @secondName,
      PassportNumber = @passportNumber,
      PassportGiveOrg = @passportGiveOrg,
      PassportGiveDate = @passportGiveDate,
      Address = @address,
      Inn = @inn
    where
      Id = @id
  end
  
