
--
--		Project:		AjTest
--		Description:	AjTest
--

use master

declare @dttm varchar(55)
select  @dttm=convert(varchar,getdate(),113)
raiserror('Beginning Create NHibernate3SimpleMapping at %s ....',1,1,@dttm) with nowait

GO

if exists (select * from sysdatabases where name='NHibernate3SimpleMapping')
begin
  raiserror('Dropping existing NHibernate3SimpleMapping database ....',0,1)
  DROP database NHibernate3SimpleMapping
end
GO

CHECKPOINT
go

raiserror('Creating NHibernate3SimpleMapping database....',0,1)
go

/*
   Use default size with autogrow
*/

create database NHibernate3SimpleMapping
go

checkpoint

go

use NHibernate3SimpleMapping

go

if db_name() <> 'NHibernate3SimpleMapping'
   raiserror('Error in NHibernate3SimpleMapping.SQL, ''USE NHibernate3SimpleMapping'' failed!  Killing the SPID now.'
            ,22,127) with log

go

use NHibernate3SimpleMapping
go
set ansi_nulls on
go
set quoted_identifier on
go


--
--		Entity:		Customer
--		Description:	Customer
--


if exists (select name from sysobjects 
         where name = 'Customers' and type = 'U')
	drop table Customers
go

create table Customers (
		[Id] [uniqueidentifier] primary key not null,	
		[Name] varchar(200),	
		[Address] varchar(200),
		[Notes] text
)
go


GO

