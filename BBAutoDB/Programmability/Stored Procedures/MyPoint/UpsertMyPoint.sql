create procedure [dbo].[UpsertMyPoint]
  @id int,
  @idRegion int,
  @name nvarchar(100)
as
begin
  if (@id = 0)
  begin
    declare @count int
    select
      @count = count(*)
    from
      MyPoint
    where
      mypoint_name = @name
      and region_id = @idRegion

    if (@count = 0)
    begin
      insert into MyPoint values(@idRegion, @name)
      set @id = scope_identity()
    end
  end
  else
  begin
    update MyPoint
    set region_id = @idRegion,
        mypoint_name = @name
    where mypoint_id = @id
  end

  select @id
end
