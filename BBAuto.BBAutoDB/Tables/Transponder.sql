create table [dbo].[Transponder] (
  [Id] int not null primary key,
  [Number] nvarchar(50) not null,
  [RegionId] int not null,
  [Comment] nvarchar(500) null
)
