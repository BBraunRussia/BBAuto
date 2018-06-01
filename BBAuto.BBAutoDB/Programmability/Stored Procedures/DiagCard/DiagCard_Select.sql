CREATE PROCEDURE [dbo].[DiagCard_Select]
AS
BEGIN
	SELECT diagCard_id, car_id, diagCard_number, diagCard_date, diagCard_file,
		diagCard_notificationSent
	FROM DiagCard
END
GO
