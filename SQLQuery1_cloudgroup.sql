create database cloudgroup;

USE cloudgroup
GO



create table users(
U_Id int primary key identity(1,1),
username varchar(100),
password varchar(100) NOT NULL,
CreatedAt datetime);



create table folders(
F_Id int primary key identity(1,1),
F_name varchar(100),
CreatedBy int foreign key references users(U_Id),
CreatedAt datetime,
IsDeleted bit );



create table documents(
D_Id int primary key identity(1,1),
D_name varchar(100),
Content_Type varchar(100),
size int,
CreatedBy int foreign key references users(U_Id),
CreatedAt datetime,
FolderId int foreign key references folders(F_Id),
IsDeleted bit);

ALTER table	folders
Add isFavourite bit default 0 

ALTER table documents
Add isFavourite bit default 0