create procedure [dbo].[UpsertPassport]
  @id int,
  @DriverId int,
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
  if (@id = 0)
  begin
    insert into Passport values(@DriverId, @firstName, @lastName, @secondName, @number, @giveOrg, @giveDate, @address, @file)

    set @id = scope_identity()
  end
  else
  begin
    update Passport
    set FirstName = @firstName,
        LastName = @lastName,
        SecondName = @secondName,
        Number = @number,
        GiveOrg = @giveOrg,
        GiveDate = @giveDate,
        [Address] = @address,
        [File] = @file
    from Passport
    where Id = @id
  end

  select @id
end
