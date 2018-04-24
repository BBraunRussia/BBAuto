create procedure [dbo].[InsertStatusAfterDtps]
as
  insert into dbo.StatusAfterDTP values(N'на ходу')
  insert into dbo.StatusAfterDTP values(N'НЕ на ходу')
