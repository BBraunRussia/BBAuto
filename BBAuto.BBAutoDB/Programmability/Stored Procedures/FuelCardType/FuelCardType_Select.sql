CREATE PROCEDURE [dbo].[FuelCardType_Select]
AS
BEGIN
	SELECT FuelCardType_id, FuelCardType_name 'Название' FROM FuelCardType
END
GO
