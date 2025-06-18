 
  select *from Categories
  select *from Menus
  select *from Permissons
  select *from Products
 select *from Orders
  select *from OrderDetails
  select *from Carts
  select *from CartDetails
   select *from Reviews
	select *from Images
	select *from Capacities
	select *from Colors
	select *from Sizes

	SELECT * FROM INFORMATION_SCHEMA.TABLES;

	
  select *from [eShopSolution].[Identity].[Roles]
  select *from [eShopSolution].[dbo].[AspNetRoles]
  select *from [eShopSolution].[Identity].[UserRoles]	
    select *from [eShopSolution].[Identity].[Users]

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
  TRUNCATE TABLE Menus

  --sửa kiểu dữ liệu
  ALTER TABLE Products
	ALTER COLUMN Gender int

	ALTER TABLE Users
ADD GenderId int


select  [eShopSolution].[Identity].[Users].FullName, Orders.TotalPrice
from Orders
inner join  [eShopSolution].[Identity].[Users] on Orders.UserId =  [eShopSolution].[Identity].[Users].Id
where  [eShopSolution].[Identity].[Users].FullName='string'