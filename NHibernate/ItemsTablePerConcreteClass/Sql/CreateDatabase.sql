

use master

declare @dttm varchar(55)
select  @dttm=convert(varchar,getdate(),113)
raiserror('Beginning CreateNHibernate3ItemsTablePerConcreteClassMapping at %s ....',1,1,@dttm) with nowait

GO

if exists (select * from sysdatabases where name='NHibernate3ItemsTablePerConcreteClassMapping')
begin
  raiserror('Dropping existing NHibernate3ItemsTablePerConcreteClassMapping database ....',0,1)
  DROP database NHibernate3ItemsTablePerConcreteClassMapping
end
GO

CHECKPOINT
go

raiserror('Creating NHibernate3ItemsTablePerConcreteClassMapping database....',0,1)
go

create database NHibernate3ItemsTablePerConcreteClassMapping
go

checkpoint

go

USE [NHibernate3ItemsTablePerConcreteClassMapping]
GO
/****** Object:  Table [dbo].[Pages]    Script Date: 04/10/2011 06:05:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pages](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [varchar](255) NOT NULL,
	[Description] [text] NOT NULL,
	[Url] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Notes]    Script Date: 04/10/2011 06:05:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Notes](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [varchar](255) NOT NULL,
	[Description] [text] NOT NULL,
	[Content] [text] NOT NULL,
 CONSTRAINT [PK_Notes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
