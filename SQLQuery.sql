 
  select *from Categories
  select *from Menus
  select *from Permissons
  select *from Products
 select *from Orders
  select *from OrderDetails
  select *from Carts
  select *from CartDetails
   select *from Reviews
  select *from [eShopSolutionTest].[Identity].[Roles]
  select *from [eShopSolutionTest].[dbo].[AspNetRoles]
  select *from [eShopSolutionTest].[Identity].[UserRoles]	
    select *from [eShopSolutionTest].[Identity].[Users]
	select *from Images
	select *from Capacities
	select *from Colors

  update OrderDetails
  set OrderId=8
  where Id=5

  delete from [Orders]
  delete from [OrderDetails]

  TRUNCATE TABLE [OnionArchitecture].[dbo].[AspNetRoles]
  TRUNCATE TABLE [OnionArchitecture].[Identity].[Roles]
  TRUNCATE TABLE [OnionArchitecture].[Identity].[UserRoles]
  TRUNCATE TABLE [OnionArchitecture].[Identity].[Users]
  TRUNCATE TABLE Permissons
  TRUNCATE TABLE Capacities
  TRUNCATE TABLE Colors
  TRUNCATE TABLE Categories
  TRUNCATE TABLE Products
  TRUNCATE TABLE Permissons

  --sửa kiểu dữ liệu
  ALTER TABLE Products
	ALTER COLUMN Gender int

	ALTER TABLE Users
ADD GenderId int
