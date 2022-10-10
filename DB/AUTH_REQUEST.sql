create table AUTH_REQUEST
(
    ID      int not null,
    User_ID int,
    [Key]   nvarchar,
    constraint AUTH_REQUEST_pk
        primary key (ID)
)
go

