Create Table tblMain
(
MainID int primary key identity,
aDate Date,
aTime varchar(15),
TableName varchar(10),
WaiterName varchar(15),
status varchar(15),
orderType varchar(15),
total float,
recieved float,
change float



)
Create table tblDetails(

DetailID int primary key identity, 
MainID int,
proID int,
qty int,
price float,
amount float,


)
truncate table tblDetails;
truncate table tblMain;
select * from tblMain m inner join  tblDetails d on m.MainID=d.MainID;