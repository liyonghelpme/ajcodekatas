

use master

declare @dttm varchar(55)
select  @dttm=convert(varchar,getdate(),113)
raiserror('Beginning Create NHibernate3ItemsTablePerHierarchyMapping at %s ....',1,1,@dttm) with nowait

GO

if exists (select * from sysdatabases where name='NHibernate3ItemsTablePerHierarchyMapping')
begin
  raiserror('Dropping existing NHibernate3ItemsTablePerHierarchyMapping database ....',0,1)
  DROP database NHibernate3ItemsTablePerHierarchyMapping
end
GO

CHECKPOINT
go

raiserror('Creating NHibernate3ItemsTablePerHierarchyMapping database....',0,1)
go

create database NHibernate3ItemsTablePerHierarchyMapping
go

checkpoint

go

USE [NHibernate3ItemsTablePerHierarchyMapping]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 04/03/2011 16:00:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Items](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [varchar](255) NOT NULL,
	[Description] [text] NOT NULL,
	[ItemType] [varchar](10) NOT NULL,
	[Url] [varchar](255) NULL,
	[Content] [text] NULL,
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
