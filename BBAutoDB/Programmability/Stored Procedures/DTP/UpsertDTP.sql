create procedure [dbo].[UpsertDTP]
  @idDTP int,
  @idCar int,
  @date datetime,
  @idRegion int,
  @dateCallInsure datetime,
  @idCulprit int,
  @idStatusAfterDTP nvarchar(50),
  @numberLoss nvarchar(50),
  @sum float,
  @damage nvarchar(300),
  @facts nvarchar(500),
  @comm nvarchar(100),
  @idCurrentStatusAfterDTP int
as
begin
  if (@idDTP = 0)
  begin
    declare @number int
    select
      @number = max(dtp_number) + 1
    from
      DTP
    if (@number is null)
      set @number = 1

    insert into DTP values(@idCar, @number, @date, @idRegion, @dateCallInsure, @idCulprit, @idStatusAfterDTP, @numberLoss, @sum, @damage, @facts, @comm, @idCurrentStatusAfterDTP)

    set @idDTP = scope_identity()
  end
  else
  begin
    update DTP
    set dtp_date = @date,
        region_id = @idRegion,
        dtp_dateCallInsure = @dateCallInsure,
        culprit_id = @idCulprit,
        StatusAfterDTP_id = @idStatusAfterDTP,
        dtp_numberLoss = @numberLoss,
        dtp_sum = @sum,
        dtp_damage = @damage,
        dtp_facts = @facts,
        dtp_comm = @comm,
        CurrentStatusAfterDTP_id = @idCurrentStatusAfterDTP
    where dtp_id = @idDTP
  end

  select @idDTP
end
