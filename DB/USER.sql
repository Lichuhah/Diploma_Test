create table [USER]
(
    ID       int identity,
    LOGIN    nvarchar(255),
    PASSWORD nvarchar(255),
    constraint USER_pk
        primary key (ID)
)
go

