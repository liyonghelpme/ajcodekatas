

use master

declare @dttm varchar(55)
select  @dttm=convert(varchar,getdate(),113)
raiserror('Beginning Create NHibernate3ItemsTablePerClassMapping at %s ....',1,1,@dttm) with nowait

GO

if exists (select * from sysdatabases where name='NHibernate3ItemsTablePerClassMapping')
begin
  raiserror('Dropping existing NHibernate3ItemsTablePerClassMapping database ....',0,1)
  DROP database NHibernate3ItemsTablePerClassMapping
end
GO

CHECKPOINT
go

raiserror('Creating NHibernate3ItemsTablePerClassMapping database....',0,1)
go

create database NHibernate3ItemsTablePerClassMapping
go

checkpoint

go

USE [NHibernate3ItemsTablePerClassMapping]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 04/10/2011 18:17:20 ******/
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
 CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Pages]    Script Date: 04/10/2011 18:17:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pages](
	[Id] [uniqueidentifier] NOT NULL,
	[Url] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Notes]    Script Date: 04/10/2011 18:17:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notes](
	[Id] [uniqueidentifier] NOT NULL,
	[Content] [text] NOT NULL,
 CONSTRAINT [PK_Notes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Notes_Items]    Script Date: 04/10/2011 18:17:20 ******/
ALTER TABLE [dbo].[Notes]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Items] FOREIGN KEY([Id])
REFERENCES [dbo].[Items] ([Id])
GO
ALTER TABLE [dbo].[Notes] CHECK CONSTRAINT [FK_Notes_Items]
GO
/****** Object:  ForeignKey [FK_Pages_Items]    Script Date: 04/10/2011 18:17:20 ******/
ALTER TABLE [dbo].[Pages]  WITH CHECK ADD  CONSTRAINT [FK_Pages_Items] FOREIGN KEY([Id])
REFERENCES [dbo].[Items] ([Id])
GO
ALTER TABLE [dbo].[Pages] CHECK CONSTRAINT [FK_Pages_Items]
GO
