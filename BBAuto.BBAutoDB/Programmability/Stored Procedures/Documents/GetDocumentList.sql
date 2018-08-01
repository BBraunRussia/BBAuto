create procedure [dbo].[GetDocumentList]
as
  select
    Id,
    [Name],
    [Path],
    Instruction
  from
    Documents
  order by
    [Name]
