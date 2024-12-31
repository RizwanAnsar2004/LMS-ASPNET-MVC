create database SDAProject2
use SDAProject2


create table students(
stu_id int primary key identity(1,1),
stu_fname varchar(255),
stu_lname varchar(255),
stu_username AS (stu_fname + ' ' + stu_lname) ,
stu_password varchar(255),
);
create table admin(
ad_id int primary key identity(1,1),
ad_fname varchar(255),
ad_lname varchar(255),
ad_username AS (ad_fname + ' ' + ad_lname),
ad_password varchar(255));
create table faculty(
f_id int primary key identity(1,1),
f_fname varchar(255),
f_lname varchar(255),
f_username AS (f_fname + ' ' + f_lname),

f_password varchar(255));

insert into students values('rizwan','ansar','rizwan');
insert into admin values('admin','1','admin');
insert into faculty values('faculty','1','faculty');
select * from students;
select * from admin;
select * from faculty;