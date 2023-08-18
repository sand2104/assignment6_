create database ProductInventoryDb

use ProductInventoryDb

create table Products
( ProductId int primary key identity(101,1),
ProductName nvarchar(50) not null,
Price float,
Quantity int,
MFDate date,
ExpDate date
)

insert into Products values ('Mobile', 14000.00, 6,'01/01/2020','01/01/2025')
insert into Products values ('Earphones',1200.50,15,'12/12/2021','10/10/2022')
insert into Products values ('PenDrive',4000.70,9,'09/11/2024','09/11/2022')

select * from Products