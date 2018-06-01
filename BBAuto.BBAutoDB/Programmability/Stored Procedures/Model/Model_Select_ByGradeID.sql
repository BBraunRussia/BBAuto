CREATE PROCEDURE [dbo].[Model_Select_ByGradeID]
@idGrade int
AS
BEGIN
	SELECT m.model_id, model_name FROM Grade g
		join Model m on m.model_id=g.model_id
	WHERE grade_id=@idGrade
END
GO
