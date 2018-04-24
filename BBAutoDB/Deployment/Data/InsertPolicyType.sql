create procedure [dbo].[InsertPolicyType]
as
  insert into dbo.policyType values(N'ОСАГО')
  insert into dbo.policyType values(N'КАСКО')
  insert into dbo.policyType values(N'ДСАГО')
  insert into dbo.policyType values(N'GAP')
  insert into dbo.policyType values(N'Расширение КАСКО')
