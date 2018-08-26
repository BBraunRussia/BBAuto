create procedure [dbo].[GetDocumentById]
  @id int
as
  select
    Id,
    [Name],
    [Path],
    Instruction
  from
    Documents
  where
    Id = @id
