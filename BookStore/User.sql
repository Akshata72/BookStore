create database BookStore;
drop table BookStore;

Create Table BookStore(
UserId int identity Primary Key,
FullName varchar(255) NOT NULL,
EmailId varchar(255) UNIQUE,
MobileNumber bigint NOT NULL,
Password varchar(255) NOT NULL);



create Procedure sp_UserRegistration @FullName varchar(255),@EmailId varchar(255),@MobileNumber bigint,@Password varchar(255)
As
Begin
insert into BookStore (FullName,EmailId,MobileNumber,Password)values(@FullName,@EmailId,@MobileNumber,@Password)
End;

select * from BookStore;

create PROCEDURE SP_User_Login @EmailId varchar(255)
AS
BEGIN
select * from BookStore where EmailId = @EmailId ;
End;

Execute SP_User_Login sableak880@gmailcom,Sable@123;

Delete From BookStore
where UserId = 1;


Create Procedure Sp_ForgatePassword @EmailId varchar(255)
AS
BEGIN
select * from BookStore where EmailId = @EmailId ;
End;


create procedure Sp_ResetPassword @EmailId varchar(255),@Password varchar(255)
AS
BEGIN
Update BookStore
set Password = @Password where EmailId =@EmailId;
End;