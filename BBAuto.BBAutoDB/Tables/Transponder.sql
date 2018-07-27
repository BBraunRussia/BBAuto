create table [dbo].[Transponder] (
  Id int not null identity primary key,
  Number nvarchar(50) not null,
  RegionId int not null,
  Lost bit not null,
  Comment nvarchar(500) null
)
