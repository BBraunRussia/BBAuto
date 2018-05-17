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
    set [Name] = @Name,
        Epower = @ePower,
        Evol = @eVol,
        MaxLoad = @maxLoad,
        NoLoad = @noLoad,
        EngineTypeId = @idEngineType
    where
      Id = @id and
      ModelId = @idModel
  end

  select @id
end
