USE [master]
GO
/****** Object:  Database [PHS]    Script Date: 30/11/2017 1:38:22 AM ******/
CREATE DATABASE [PHS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PHS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\PHS.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PHS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\PHS_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [PHS] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PHS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PHS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PHS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PHS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PHS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PHS] SET ARITHABORT OFF 
GO
ALTER DATABASE [PHS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PHS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PHS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PHS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PHS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PHS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PHS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PHS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PHS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PHS] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PHS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PHS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PHS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PHS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PHS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PHS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PHS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PHS] SET RECOVERY FULL 
GO
ALTER DATABASE [PHS] SET  MULTI_USER 
GO
ALTER DATABASE [PHS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PHS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PHS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PHS] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [PHS] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'PHS', N'ON'
GO
USE [PHS]
GO
/****** Object:  Table [dbo].[agent]    Script Date: 30/11/2017 1:38:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[agent](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[addr] [nvarchar](150) NULL,
	[phone] [nvarchar](11) NULL,
 CONSTRAINT [PK_daily] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[agent_payment]    Script Date: 30/11/2017 1:38:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[agent_payment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_agent] [int] NOT NULL,
	[amount] [float] NOT NULL,
	[date] [datetime] NOT NULL,
 CONSTRAINT [PK_agent_payment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[agent_payment_detail]    Script Date: 30/11/2017 1:38:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[agent_payment_detail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_payment] [int] NOT NULL,
	[id_book] [int] NOT NULL,
	[quantity] [int] NOT NULL,
 CONSTRAINT [PK_agent_payment_detail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[agent_storage]    Script Date: 30/11/2017 1:38:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[agent_storage](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_agent] [int] NOT NULL,
	[id_book] [int] NOT NULL,
	[quantity] [int] NOT NULL,
 CONSTRAINT [PK_agent_storage] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bill_in]    Script Date: 30/11/2017 1:38:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bill_in](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_publisher] [int] NOT NULL,
	[total] [float] NOT NULL,
	[date] [datetime] NOT NULL,
	[rc] [nvarchar](50) NOT NULL,
	[status] [int] NOT NULL CONSTRAINT [DF_bill_in_status]  DEFAULT ((0)),
 CONSTRAINT [PK_bill_in_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bill_in_detail]    Script Date: 30/11/2017 1:38:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bill_in_detail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_bill_in] [int] NULL,
	[id_book] [int] NULL,
	[quantity] [int] NULL,
 CONSTRAINT [PK_bill_in_detail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bill_out]    Script Date: 30/11/2017 1:38:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bill_out](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_agent] [int] NOT NULL,
	[total] [float] NULL,
	[date] [datetime] NOT NULL,
	[rc] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_bill_out] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[bill_out_detail]    Script Date: 30/11/2017 1:38:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bill_out_detail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_bill_out] [int] NOT NULL,
	[id_book] [int] NOT NULL,
	[quantity] [int] NOT NULL,
 CONSTRAINT [PK_bill_out_detail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[book]    Script Date: 30/11/2017 1:38:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[book](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[id_cat] [int] NOT NULL,
	[id_nxb] [int] NOT NULL,
	[author] [nvarchar](50) NOT NULL,
	[price] [float] NOT NULL,
	[price_in] [float] NOT NULL,
	[stock] [int] NOT NULL,
 CONSTRAINT [PK_book] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[cat]    Script Date: 30/11/2017 1:38:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cat](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
 CONSTRAINT [PK_cat] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[publisher]    Script Date: 30/11/2017 1:38:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[publisher](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[addr] [nvarchar](150) NULL,
	[phone] [nvarchar](11) NULL,
	[bank] [nvarchar](50) NULL,
 CONSTRAINT [PK_nxb] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Index [IX_book]    Script Date: 30/11/2017 1:38:24 AM ******/
CREATE NONCLUSTERED INDEX [IX_book] ON [dbo].[book]
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[agent_payment]  WITH CHECK ADD  CONSTRAINT [agent_relations] FOREIGN KEY([id_agent])
REFERENCES [dbo].[agent] ([id])
GO
ALTER TABLE [dbo].[agent_payment] CHECK CONSTRAINT [agent_relations]
GO
ALTER TABLE [dbo].[agent_payment_detail]  WITH CHECK ADD  CONSTRAINT [payment_detail_books] FOREIGN KEY([id_book])
REFERENCES [dbo].[book] ([id])
GO
ALTER TABLE [dbo].[agent_payment_detail] CHECK CONSTRAINT [payment_detail_books]
GO
ALTER TABLE [dbo].[agent_payment_detail]  WITH CHECK ADD  CONSTRAINT [payment_detail_payid] FOREIGN KEY([id_payment])
REFERENCES [dbo].[agent_payment] ([id])
GO
ALTER TABLE [dbo].[agent_payment_detail] CHECK CONSTRAINT [payment_detail_payid]
GO
ALTER TABLE [dbo].[agent_storage]  WITH CHECK ADD  CONSTRAINT [storeagent] FOREIGN KEY([id_agent])
REFERENCES [dbo].[agent] ([id])
GO
ALTER TABLE [dbo].[agent_storage] CHECK CONSTRAINT [storeagent]
GO
ALTER TABLE [dbo].[agent_storage]  WITH CHECK ADD  CONSTRAINT [storebook] FOREIGN KEY([id_book])
REFERENCES [dbo].[book] ([id])
GO
ALTER TABLE [dbo].[agent_storage] CHECK CONSTRAINT [storebook]
GO
ALTER TABLE [dbo].[bill_in]  WITH CHECK ADD  CONSTRAINT [id_publisher] FOREIGN KEY([id_publisher])
REFERENCES [dbo].[publisher] ([id])
GO
ALTER TABLE [dbo].[bill_in] CHECK CONSTRAINT [id_publisher]
GO
ALTER TABLE [dbo].[bill_in_detail]  WITH CHECK ADD  CONSTRAINT [bdt_book] FOREIGN KEY([id_book])
REFERENCES [dbo].[book] ([id])
GO
ALTER TABLE [dbo].[bill_in_detail] CHECK CONSTRAINT [bdt_book]
GO
ALTER TABLE [dbo].[bill_in_detail]  WITH CHECK ADD  CONSTRAINT [bill_in_relation] FOREIGN KEY([id_bill_in])
REFERENCES [dbo].[bill_in] ([id])
GO
ALTER TABLE [dbo].[bill_in_detail] CHECK CONSTRAINT [bill_in_relation]
GO
ALTER TABLE [dbo].[bill_out]  WITH CHECK ADD  CONSTRAINT [agent_relation] FOREIGN KEY([id_agent])
REFERENCES [dbo].[agent] ([id])
GO
ALTER TABLE [dbo].[bill_out] CHECK CONSTRAINT [agent_relation]
GO
ALTER TABLE [dbo].[bill_out_detail]  WITH CHECK ADD  CONSTRAINT [bill_out_relation] FOREIGN KEY([id_bill_out])
REFERENCES [dbo].[bill_out] ([id])
GO
ALTER TABLE [dbo].[bill_out_detail] CHECK CONSTRAINT [bill_out_relation]
GO
ALTER TABLE [dbo].[bill_out_detail]  WITH CHECK ADD  CONSTRAINT [book_relation] FOREIGN KEY([id_book])
REFERENCES [dbo].[book] ([id])
GO
ALTER TABLE [dbo].[bill_out_detail] CHECK CONSTRAINT [book_relation]
GO
ALTER TABLE [dbo].[book]  WITH CHECK ADD  CONSTRAINT [Cat_relation] FOREIGN KEY([id_cat])
REFERENCES [dbo].[cat] ([id])
GO
ALTER TABLE [dbo].[book] CHECK CONSTRAINT [Cat_relation]
GO
ALTER TABLE [dbo].[book]  WITH CHECK ADD  CONSTRAINT [Pub_relation] FOREIGN KEY([id_nxb])
REFERENCES [dbo].[publisher] ([id])
GO
ALTER TABLE [dbo].[book] CHECK CONSTRAINT [Pub_relation]
GO
USE [master]
GO
ALTER DATABASE [PHS] SET  READ_WRITE 
GO
