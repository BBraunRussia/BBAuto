/*
Post-Deployment Script Template              
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.    
 Use SQLCMD syntax to include a file in the post-deployment script.      
 Example:      :r .\myfile.sql                
 Use SQLCMD syntax to reference a variable in the post-deployment script.    
 Example:      :setvar TableName MyTable              
               SELECT * FROM [$(TableName)]          
--------------------------------------------------------------------------------------
*/
print N'Begin post deployment process.'
declare @curver nvarchar(max)
select @curver = Id from dbo.DbVersion

declare @dbVer nvarchar(8)
select @dbVer = DbVersion from dbo.DbVersion

if @curver is null
	set @curver = ''

if @curver = 'x.x.xxxx.xxxx' --recreate DB
	set @curver = ''

print N'Current version is: ' + @curver

if @curver <= '' 
begin
  print N'Clear database...'
--  exec dbo.ClearDatabase
  
  print N'Fill database...'
--  exec dbo.FillDatabase
end
