CREATE PROCEDURE [dbo].[Mark_Select_ByGradeID]
@idGrade int
AS
BEGIN
	SELECT Mark.mark_id, mark_name FROM Grade g
		join Model m on m.model_id=g.model_id
		join Mark on Mark.mark_id=m.mark_id
	WHERE grade_id=@idGrade
END
GO
