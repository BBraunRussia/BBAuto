CREATE PROCEDURE [dbo].[PTS_Select]
AS
BEGIN
	SELECT car_id, pts_number, pts_date, pts_giveOrg, pts_file FROM PTS
END
GO
