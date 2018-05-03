create procedure [dbo].[UpsertPassport]
  @idPassport int,
  @idDriver int,
  @firstName nvarchar(50),
  @lastName nvarchar(50),
  @secondName nvarchar(50),
  @number nvarchar(12),
  @giveOrg nvarchar(200),
  @giveDate datetime,
  @address nvarchar(200),
  @file nvarchar(100)
as
begin
  if (@idPassport = 0)
  begin
    insert into Passport values(@idDriver, @firstName, @lastName, @secondName, @number, @giveOrg, @giveDate, @address, @file)

    set @idPassport = scope_identity()
  end
  else
  begin
    update Passport
    set passport_firstName = @firstName,
        passport_lastName = @lastName,
        passport_secondName = @secondName,
        passport_number = @number,
        passport_GiveOrg = @giveOrg,
        passport_GiveDate = @giveDate,
        passport_address = @address,
        passport_file = @file
    from Passport
    where passport_id = @idPassport
  end

  select @idPassport
end
