create procedure [dbo].[UpsertMedicalCert]
  @id int,
  @idDriver int,
  @number nvarchar(50),
  @dateBegin datetime,
  @dateEnd datetime,
  @file nvarchar(500),
  @notificationSent int
as
begin
  if (@id = 0)
  begin
    insert into MedicalCert values(@number, @dateBegin, @dateEnd, @idDriver, @file, 0)

    set @id = scope_identity()
  end
  else
    update MedicalCert
    set MedicalCert_number = @number,
        MedicalCert_dateBegin = @dateBegin,
        MedicalCert_dateEnd = @dateEnd,
        driver_id = @idDriver,
        MedicalCert_file = @file,
        MedicalCert_notificationSent = @notificationSent
    where MedicalCert_id = @id

  select @id
end
