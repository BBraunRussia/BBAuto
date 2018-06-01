CREATE PROCEDURE [dbo].[Passport_Insert]
@idPassport int,
@idDriver int,
@firstName varchar(50),
@lastName varchar(50),
@secondName varchar(50),
@number varchar(12),
@giveOrg varchar(200),
@giveDate datetime,
@address varchar(200),
@file varchar(100)
AS
BEGIN
	if (@idPassport = 0)
	begin
		INSERT INTO Passport VALUES(@idDriver, @firstName, @lastName, @secondName, @number,
			@giveOrg, @giveDate, @address, @file)
			
		SET @idPassport=SCOPE_IDENTITY()
	end
	else
	begin
		UPDATE Passport SET passport_firstName = @firstName, passport_lastName = @lastName,
			passport_secondName = @secondName, passport_number = @number,
			passport_GiveOrg = @giveOrg, passport_GiveDate = @giveDate,
			passport_address = @address, passport_file=@file
		FROM Passport
		WHERE passport_id=@idPassport
	end
	
	SELECT @idPassport
END
GO
