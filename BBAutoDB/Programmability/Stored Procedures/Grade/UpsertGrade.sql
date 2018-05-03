create procedure [dbo].[UpsertGrade]
  @id int,
  @Name nvarchar(50),
  @ePower int,
  @eVol int,
  @maxLoad int,
  @noLoad int,
  @idEngineType int,
  @idModel int
as
begin
  if (@id = 0)
  begin
    insert into Grade values(@Name, @ePower, @eVol, @maxLoad, @noLoad, @idEngineType, @idModel)
    set @id = scope_identity()
  end
  else
  begin
    update Grade
    set grade_name = @Name,
        grade_epower = @ePower,
        grade_evol = @eVol,
        grade_maxLoad = @maxLoad,
        grade_noLoad = @noLoad,
        engineType_id = @idEngineType
    where grade_Id = @id
    and model_id = @idModel
  end

  select @id
end
