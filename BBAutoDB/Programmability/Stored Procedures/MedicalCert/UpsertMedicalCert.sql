create procedure [dbo].[UpsertMedicalCert]
  @id int,
  @DriverId int,
  @number nvarchar(50),
  @dateBegin datetime,
  @dateEnd datetime,
  @file nvarchar(500),
  @notificationSent bit
as
begin
  if (@id = 0)
  begin
    insert into MedicalCert values(@number, @dateBegin, @dateEnd, @DriverId, @file, 0)

    set @id = scope_identity()
  end
  else
    update MedicalCert
    set Number = @number,
        DateBegin = @dateBegin,
        DateEnd = @dateEnd,
        DriverId = @DriverId,
        [File] = @file,
        NotificationSent = @notificationSent
    where Id = @id

  select @id
end
