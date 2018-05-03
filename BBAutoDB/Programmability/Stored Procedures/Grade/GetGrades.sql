create procedure [dbo].[GetGrades]
as
begin
  select
    grade_Id,
    grade_name,
    grade_epower,
    grade_evol,
    grade_maxLoad,
    grade_noLoad,
    engineType_id,
    model_id
  from
    Grade
end
