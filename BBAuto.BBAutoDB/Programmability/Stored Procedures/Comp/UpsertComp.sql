create procedure [dbo].[UpsertComp]
  @id int,
  @Name varchar(50),
  @KaskoPaymentCount int
as
  if (@id = 0)
  begin
    insert into Comp(comp_name, KaskoPaymentCount) values(@Name, @KaskoPaymentCount)

    set @id = scope_identity()
  end
  else
    update
      Comp
    set
      Comp_name = @Name,
      KaskoPaymentCount = @KaskoPaymentCount
    where
      Comp_id = @id

  exec dbo.GetCompById @id
