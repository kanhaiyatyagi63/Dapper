--create database
create database EmployeeManagement

use EmployeeManagement
--create table
Create table Employees
(
id int not null Identity(1,1) Primary Key,
name varchar(50),
dob datetime,
email varchar(50),
gender varchar(10)
)

--stored procedure
create procedure Sp_FilterEmployee @value varchar(50)
AS
select * from Employees where email like '%'+@value+'%' or name like '%'+@value+'%'
