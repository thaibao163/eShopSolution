  select *from Orders
  select *from OrderDetails
  select *from Products
  select *from Categories
  select *from [OnionArchitecture].[Identity].[Users]
  select *from Menus
  select *from Permissons

  select *from [OnionArchitecture].[dbo].[AspNetRoles]
  select *from [OnionArchitecture].[Identity].[Roles]
  select *from [OnionArchitecture].[Identity].[UserRoles]
    select *from [OnionArchitecture].[Identity].[Users]

  update OrderDetails
  set OrderId=8
  where Id=5

  delete from [Orders]
  delete from [OrderDetails]

  TRUNCATE TABLE Products