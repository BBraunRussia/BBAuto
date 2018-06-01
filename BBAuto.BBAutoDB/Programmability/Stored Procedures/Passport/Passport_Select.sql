CREATE PROCEDURE [dbo].[Passport_Select]
AS
BEGIN
	SELECT passport_id, driver_id, passport_firstName, passport_lastName, passport_secondName,
		passport_number, passport_GiveOrg, passport_GiveDate, passport_address, passport_file
	FROM Passport
END
GO
