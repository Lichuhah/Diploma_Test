create table CONTROLLER
(
    ID      int identity,
    User_ID int,
    Code    nvarchar(19),
    constraint CONTROLLER_pk
        primary key (ID),
    constraint CONTROLLER_USER_null_fk
        foreign key (User_ID) references [USER]
)
go

