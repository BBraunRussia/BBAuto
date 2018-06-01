CREATE PROCEDURE [dbo].[ServiceStantion_Insert]
@idServiceStantion int,
@name varchar(200)
AS
BEGIN
	if (@idServiceStantion = 0)
	begin
		INSERT INTO ServiceStantion VALUES(@name)
	end
	else
	begin
		UPDATE ServiceStantion
		SET ServiceStantion_name=@name
		WHERE ServiceStantion_id=@idServiceStantion
	end
END
GO
