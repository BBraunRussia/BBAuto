CREATE PROCEDURE [dbo].[PTS_Insert]
@idCar int,
@number varchar(50),
@date datetime,
@giveOrg varchar(100),
@file varchar(200)
AS
BEGIN
	if (@idCar = 0)
		return

	Declare @count int
	SELECT @count=COUNT(car_id) FROM PTS WHERE car_id=@idCar
	
	if (@count = 0)
	begin
		INSERT INTO PTS VALUES(@idCar, @number, @date, @giveOrg, @file)
	end
	else
	begin
		UPDATE PTS
		SET pts_number=@number, pts_date=@date, pts_giveOrg=@giveOrg, pts_file=@file
		WHERE car_id=@idCar
	end
END
GO
