CREATE PROCEDURE [dbo].[STS_Select]
AS
BEGIN
	SELECT car_id, sts_number, sts_date, sts_giveOrg, sts_file FROM STS
END
GO
