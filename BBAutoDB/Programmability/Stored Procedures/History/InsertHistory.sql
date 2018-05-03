create procedure [dbo].[InsertHistory]
  @Comment nvarchar(50),
  @id int,
  @Event nvarchar(50),
  @file nvarchar(max)
as
begin
  if (@file = '')
    insert into History values(@Comment, @id, current_timestamp, @Event, null)
  else
    insert into History values(@Comment, @id, current_timestamp, @Event, @file)
end
