 
  select *from Categories
  select *from Menus
  select *from Permissons
  select *from Products
 select *from Orders
  select *from OrderDetails
  select *from Carts
  select *from CartDetails
   select *from Reviews
  select *from [eShopSolution].[Identity].[Roles]
  select *from [eShopSolution].[dbo].[AspNetRoles]
  select *from [eShopSolution].[Identity].[UserRoles]	
    select *from [eShopSolution].[Identity].[Users]
	select *from Images
	select *from Capacities
	select *from Colors

  update OrderDetails
  set OrderId=8
  where Id=5

  delete from [Orders]
  delete from [OrderDetails]

  TRUNCATE TABLE [eShopSolution].[dbo].[AspNetRoles]
  TRUNCATE TABLE [eShopSolution].[Identity].[Roles]
  TRUNCATE TABLE [eShopSolution].[Identity].[UserRoles]
  TRUNCATE TABLE [eShopSolution].[Identity].[Users]
  TRUNCATE TABLE Permissons
  TRUNCATE TABLE Menus
  TRUNCATE TABLE Products
  TRUNCATE TABLE Categories
  TRUNCATE TABLE Colors
  TRUNCATE TABLE Capacities

  --sửa kiểu dữ liệu
  ALTER TABLE Products
	ALTER COLUMN Gender int

	ALTER TABLE Users
ADD GenderId int
