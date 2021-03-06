USE [master]
GO
/****** Object:  Database [QLBX]    Script Date: 01/20/2020 17:23:43 ******/
CREATE DATABASE [QLBX]
GO
ALTER DATABASE [QLBX] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLBX].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLBX] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [QLBX] SET ANSI_NULLS OFF
GO
ALTER DATABASE [QLBX] SET ANSI_PADDING OFF
GO
ALTER DATABASE [QLBX] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [QLBX] SET ARITHABORT OFF
GO
ALTER DATABASE [QLBX] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [QLBX] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [QLBX] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [QLBX] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [QLBX] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [QLBX] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [QLBX] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [QLBX] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [QLBX] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [QLBX] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [QLBX] SET  DISABLE_BROKER
GO
ALTER DATABASE [QLBX] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [QLBX] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [QLBX] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [QLBX] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [QLBX] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [QLBX] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [QLBX] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [QLBX] SET  READ_WRITE
GO
ALTER DATABASE [QLBX] SET RECOVERY SIMPLE
GO
ALTER DATABASE [QLBX] SET  MULTI_USER
GO
ALTER DATABASE [QLBX] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [QLBX] SET DB_CHAINING OFF
GO
USE [QLBX]
GO
/****** Object:  ForeignKey [fk_duty_spell]    Script Date: 01/20/2020 17:23:46 ******/
ALTER TABLE [dbo].[Spellduty_User] DROP CONSTRAINT [fk_duty_spell]
GO
/****** Object:  ForeignKey [fk_user_spell]    Script Date: 01/20/2020 17:23:46 ******/
ALTER TABLE [dbo].[Spellduty_User] DROP CONSTRAINT [fk_user_spell]
GO
/****** Object:  ForeignKey [FK_CheckInOut_Price]    Script Date: 01/20/2020 17:23:46 ******/
ALTER TABLE [dbo].[CheckInOut] DROP CONSTRAINT [FK_CheckInOut_Price]
GO
/****** Object:  Table [dbo].[CheckInOut]    Script Date: 01/20/2020 17:23:46 ******/
ALTER TABLE [dbo].[CheckInOut] DROP CONSTRAINT [FK_CheckInOut_Price]
GO
DROP TABLE [dbo].[CheckInOut]
GO
/****** Object:  Table [dbo].[Spellduty_User]    Script Date: 01/20/2020 17:23:46 ******/
ALTER TABLE [dbo].[Spellduty_User] DROP CONSTRAINT [fk_duty_spell]
GO
ALTER TABLE [dbo].[Spellduty_User] DROP CONSTRAINT [fk_user_spell]
GO
DROP TABLE [dbo].[Spellduty_User]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 01/20/2020 17:23:46 ******/
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[Price]    Script Date: 01/20/2020 17:23:46 ******/
DROP TABLE [dbo].[Price]
GO
/****** Object:  Table [dbo].[Spellduty]    Script Date: 01/20/2020 17:23:46 ******/
DROP TABLE [dbo].[Spellduty]
GO
/****** Object:  Table [dbo].[Spellduty]    Script Date: 01/20/2020 17:23:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spellduty](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[startTime] [datetime] NULL,
	[endTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Spellduty] ON
INSERT [dbo].[Spellduty] ([id], [startTime], [endTime]) VALUES (6, CAST(0x0000AB430062E080 AS DateTime), CAST(0x0000AB4300C5C100 AS DateTime))
INSERT [dbo].[Spellduty] ([id], [startTime], [endTime]) VALUES (7, CAST(0x0000AB4300D63BC0 AS DateTime), CAST(0x0000AB430128A180 AS DateTime))
INSERT [dbo].[Spellduty] ([id], [startTime], [endTime]) VALUES (12, CAST(0x0000AB4400D63BC0 AS DateTime), CAST(0x0000AB440128A180 AS DateTime))
INSERT [dbo].[Spellduty] ([id], [startTime], [endTime]) VALUES (13, CAST(0x0000AB4500000000 AS DateTime), CAST(0x0000AB45018344A0 AS DateTime))
INSERT [dbo].[Spellduty] ([id], [startTime], [endTime]) VALUES (14, CAST(0x0000AB4700107AC0 AS DateTime), CAST(0x0000AB47017B0740 AS DateTime))
INSERT [dbo].[Spellduty] ([id], [startTime], [endTime]) VALUES (15, CAST(0x0000AB480062E080 AS DateTime), CAST(0x0000AB480083D600 AS DateTime))
INSERT [dbo].[Spellduty] ([id], [startTime], [endTime]) VALUES (17, CAST(0x0000AB48008C1360 AS DateTime), CAST(0x0000AB4800AD08E0 AS DateTime))
INSERT [dbo].[Spellduty] ([id], [startTime], [endTime]) VALUES (18, CAST(0x0000AB4800B54640 AS DateTime), CAST(0x0000AB4800F73140 AS DateTime))
INSERT [dbo].[Spellduty] ([id], [startTime], [endTime]) VALUES (19, CAST(0x0000AB4800FF6EA0 AS DateTime), CAST(0x0000AB49017B0740 AS DateTime))
SET IDENTITY_INSERT [dbo].[Spellduty] OFF
/****** Object:  Table [dbo].[Price]    Script Date: 01/20/2020 17:23:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Price](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[veh_type] [nvarchar](100) NULL,
	[price] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Price] ON
INSERT [dbo].[Price] ([id], [veh_type], [price]) VALUES (1, N'Xe máy', 3000)
INSERT [dbo].[Price] ([id], [veh_type], [price]) VALUES (2, N'Ô tô', 5000)
SET IDENTITY_INSERT [dbo].[Price] OFF
/****** Object:  Table [dbo].[Users]    Script Date: 01/20/2020 17:23:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[users_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](500) NULL,
	[born] [date] NULL,
	[user_address] [nvarchar](500) NULL,
	[phone] [varchar](15) NULL,
	[acc] [varchar](50) NULL,
	[user_role] [int] NULL,
	[pass] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[users_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON
INSERT [dbo].[Users] ([users_id], [name], [born], [user_address], [phone], [acc], [user_role], [pass]) VALUES (8, N'Minh Hoàng', CAST(0x9F240B00 AS Date), N'Hà Nội', N'0949669275', N'user3', 0, N'92877af70a45fd6a2ed7fe81e1236b78')
INSERT [dbo].[Users] ([users_id], [name], [born], [user_address], [phone], [acc], [user_role], [pass]) VALUES (9, N'Hoàng Minh', CAST(0x95230B00 AS Date), N'Hà Nội', N'0989669888', N'user2', 0, N'7e58d63b60197ceb55a1c487989a3720')
INSERT [dbo].[Users] ([users_id], [name], [born], [user_address], [phone], [acc], [user_role], [pass]) VALUES (10, N'Hoàng Giáp', CAST(0x2D210B00 AS Date), N'Lào Cai', N'0878787878', N'user1', 0, N'24c9e15e52afc47c225b757e7bee1f9d')
INSERT [dbo].[Users] ([users_id], [name], [born], [user_address], [phone], [acc], [user_role], [pass]) VALUES (13, N'Hoàng Giáp', CAST(0x2D210B00 AS Date), N'Bắc Giang', N'911', N'admin1', 1, N'e00cf25ad42683b3df678c61f42c6bda')
INSERT [dbo].[Users] ([users_id], [name], [born], [user_address], [phone], [acc], [user_role], [pass]) VALUES (14, N'Hoàng Giáp Minh', CAST(0x0E220B00 AS Date), N'Bắc Cạn', N'911', N'admin2', 1, N'c84258e9c39059a89ab77d846ddab909')
INSERT [dbo].[Users] ([users_id], [name], [born], [user_address], [phone], [acc], [user_role], [pass]) VALUES (21, N'user4', CAST(0x30170B00 AS Date), N'Hà Nội', N'0990909090', N'user4', 0, N'3f02ebe3d7929b091e3d8ccfde2f3bc6')
INSERT [dbo].[Users] ([users_id], [name], [born], [user_address], [phone], [acc], [user_role], [pass]) VALUES (22, N'user5', CAST(0xF5170B00 AS Date), N'Hải Phòng', N'090908761', N'user5', 0, N'0a791842f52a0acfbb3a783378c066b8')
INSERT [dbo].[Users] ([users_id], [name], [born], [user_address], [phone], [acc], [user_role], [pass]) VALUES (23, N'admin3', CAST(0xF0170B00 AS Date), N'Hà Giang', N'911', N'admin3', 1, N'32cacb2f994f6b42183a1300d9a3e8d6')
SET IDENTITY_INSERT [dbo].[Users] OFF
/****** Object:  Table [dbo].[Spellduty_User]    Script Date: 01/20/2020 17:23:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Spellduty_User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[spelldutyId] [int] NULL,
	[usersId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Spellduty_User] ON
INSERT [dbo].[Spellduty_User] ([id], [spelldutyId], [usersId]) VALUES (1, 6, 10)
INSERT [dbo].[Spellduty_User] ([id], [spelldutyId], [usersId]) VALUES (2, 6, 8)
INSERT [dbo].[Spellduty_User] ([id], [spelldutyId], [usersId]) VALUES (9, 7, 8)
INSERT [dbo].[Spellduty_User] ([id], [spelldutyId], [usersId]) VALUES (11, 7, 14)
INSERT [dbo].[Spellduty_User] ([id], [spelldutyId], [usersId]) VALUES (14, 12, 9)
INSERT [dbo].[Spellduty_User] ([id], [spelldutyId], [usersId]) VALUES (15, 12, 8)
INSERT [dbo].[Spellduty_User] ([id], [spelldutyId], [usersId]) VALUES (17, 13, 8)
INSERT [dbo].[Spellduty_User] ([id], [spelldutyId], [usersId]) VALUES (19, 13, 10)
INSERT [dbo].[Spellduty_User] ([id], [spelldutyId], [usersId]) VALUES (22, 13, 9)
INSERT [dbo].[Spellduty_User] ([id], [spelldutyId], [usersId]) VALUES (23, 14, 8)
INSERT [dbo].[Spellduty_User] ([id], [spelldutyId], [usersId]) VALUES (24, 14, 9)
INSERT [dbo].[Spellduty_User] ([id], [spelldutyId], [usersId]) VALUES (25, 14, 10)
INSERT [dbo].[Spellduty_User] ([id], [spelldutyId], [usersId]) VALUES (26, 15, 10)
INSERT [dbo].[Spellduty_User] ([id], [spelldutyId], [usersId]) VALUES (27, 17, 9)
INSERT [dbo].[Spellduty_User] ([id], [spelldutyId], [usersId]) VALUES (28, 18, 10)
INSERT [dbo].[Spellduty_User] ([id], [spelldutyId], [usersId]) VALUES (29, 18, 9)
INSERT [dbo].[Spellduty_User] ([id], [spelldutyId], [usersId]) VALUES (30, 17, 21)
INSERT [dbo].[Spellduty_User] ([id], [spelldutyId], [usersId]) VALUES (31, 15, 22)
INSERT [dbo].[Spellduty_User] ([id], [spelldutyId], [usersId]) VALUES (32, 19, 8)
INSERT [dbo].[Spellduty_User] ([id], [spelldutyId], [usersId]) VALUES (33, 19, 22)
SET IDENTITY_INSERT [dbo].[Spellduty_User] OFF
/****** Object:  Table [dbo].[CheckInOut]    Script Date: 01/20/2020 17:23:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CheckInOut](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[license] [varchar](50) NOT NULL,
	[cardId] [varchar](50) NOT NULL,
	[checkInTime] [datetime] NULL,
	[checkOutTime] [datetime] NULL,
	[pricesId] [int] NULL,
	[checkInUserName] [nvarchar](100) NULL,
	[checkOutUserName] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[CheckInOut] ON
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (35, N'29A_15390', N'02817', CAST(0x0000AB4800734CDA AS DateTime), CAST(0x0000AB4800ED26C4 AS DateTime), 2, N'Hoàng Giáp', N'Hoàng Minh')
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (36, N'29A_87537', N'71032', CAST(0x0000AB4800735E85 AS DateTime), NULL, 1, N'Hoàng Giáp', NULL)
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (37, N'66A_00545', N'73602', CAST(0x0000AB4800A4E049 AS DateTime), CAST(0x0000AB4800ED6702 AS DateTime), 1, N'Hoàng Minh', N'Hoàng Minh')
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (38, N'18N_6038', N'75025', CAST(0x0000AB4800B576B4 AS DateTime), CAST(0x0000AB4800EDFA23 AS DateTime), 2, N'Hoàng Minh', N'Hoàng Minh')
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (39, N'30Y_9999', N'30373', CAST(0x0000AB4800D70320 AS DateTime), CAST(0x0000AB4801096990 AS DateTime), 1, N'Hoàng Giáp', N'Minh Hoàng')
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (40, N'70F1_66666', N'27253', CAST(0x0000AB4800A86F9E AS DateTime), CAST(0x0000AB4801311AB0 AS DateTime), 1, N'user4', N'user5')
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (41, N'29A_89234', N'72840', CAST(0x0000AB4800A88850 AS DateTime), NULL, 2, N'user4', NULL)
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (42, N'29A_46880', N'77055', CAST(0x0000AB4800FB41E7 AS DateTime), NULL, 1, N'Hoàng Minh', NULL)
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (43, N'29A_04518', N'14143', CAST(0x0000AB4800A8F313 AS DateTime), CAST(0x0000AB4800EF2FB0 AS DateTime), 2, N'user4', N'Hoàng Minh')
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (44, N'29Y_7765', N'13861', CAST(0x0000AB4800A903FF AS DateTime), NULL, 1, N'user4', NULL)
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (45, N'29C_29037', N'83511', CAST(0x0000AB4800DA8807 AS DateTime), NULL, 1, N'Hoàng Giáp', NULL)
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (46, N'29A_57289', N'03455', CAST(0x0000AB48011CC252 AS DateTime), NULL, 2, N'user5', NULL)
SET IDENTITY_INSERT [dbo].[CheckInOut] OFF
/****** Object:  ForeignKey [fk_duty_spell]    Script Date: 01/20/2020 17:23:46 ******/
ALTER TABLE [dbo].[Spellduty_User]  WITH CHECK ADD  CONSTRAINT [fk_duty_spell] FOREIGN KEY([spelldutyId])
REFERENCES [dbo].[Spellduty] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Spellduty_User] CHECK CONSTRAINT [fk_duty_spell]
GO
/****** Object:  ForeignKey [fk_user_spell]    Script Date: 01/20/2020 17:23:46 ******/
ALTER TABLE [dbo].[Spellduty_User]  WITH CHECK ADD  CONSTRAINT [fk_user_spell] FOREIGN KEY([usersId])
REFERENCES [dbo].[Users] ([users_id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Spellduty_User] CHECK CONSTRAINT [fk_user_spell]
GO
/****** Object:  ForeignKey [FK_CheckInOut_Price]    Script Date: 01/20/2020 17:23:46 ******/
ALTER TABLE [dbo].[CheckInOut]  WITH CHECK ADD  CONSTRAINT [FK_CheckInOut_Price] FOREIGN KEY([pricesId])
REFERENCES [dbo].[Price] ([id])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[CheckInOut] CHECK CONSTRAINT [FK_CheckInOut_Price]
GO
