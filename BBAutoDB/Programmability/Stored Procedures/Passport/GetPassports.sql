create procedure [dbo].[GetPassports]
as
begin
  select
    Id,
    DriverId,
    FirstName,
    LastName,
    SecondName,
    Number,
    GiveOrg,
    GiveDate,
    [Address],
    [File]
  from
    Passport
end
