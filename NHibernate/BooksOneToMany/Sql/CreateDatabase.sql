

use master

declare @dttm varchar(55)
select  @dttm=convert(varchar,getdate(),113)
raiserror('Beginning Create NHibernate3BooksOneToMany at %s ....',1,1,@dttm) with nowait

GO

if exists (select * from sysdatabases where name='NHibernate3BooksOneToMany')
begin
  raiserror('Dropping existing NHibernate3BooksOneToMany database ....',0,1)
  DROP database NHibernate3BooksOneToMany
end
GO

CHECKPOINT
go

raiserror('Creating NHibernate3BooksOneToMany database....',0,1)
go

create database NHibernate3BooksOneToMany
go

checkpoint

go

USE [NHibernate3BooksOneToMany]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 05/08/2011 17:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Author] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Chapters]    Script Date: 05/08/2011 17:17:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chapters](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Notes] [text] NULL,
	[ChapterIndex] [int] NULL,
	[BookId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Chapters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Chapters_Books]    Script Date: 05/08/2011 17:17:25 ******/
ALTER TABLE [dbo].[Chapters]  WITH CHECK ADD  CONSTRAINT [FK_Chapters_Books] FOREIGN KEY([BookId])
REFERENCES [dbo].[Books] ([Id])
GO
ALTER TABLE [dbo].[Chapters] CHECK CONSTRAINT [FK_Chapters_Books]
GO
