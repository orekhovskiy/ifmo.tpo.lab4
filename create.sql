create table [User]
(
	id int primary key identity(1,1),
	login nvarchar(15) not null unique,
	password nvarchar(32) not null,
	firstname nvarchar(30) not null,
	lastname nvarchar(30) not null
);

create table [Messages]
(
    id int primary key identity(1,1),
    login nvarchar(15) not null,
    content text not null, 
    timeSend date not null
);