use[QLBX]
go
update dbo.Users set pass='66326cebf8bba6f468c4b106795b2683' where 1=1
go

--Pass='qlbx123'

truncate table dbo.CheckInOut
go

SET IDENTITY_INSERT [dbo].[CheckInOut] ON
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (1, N'29A_15390', N'02817', CAST(0x0000AB4800734CDA AS DateTime), CAST(0x0000AB4800ED26C4 AS DateTime), 2, N'Hoàng Giáp', N'Hoàng Minh')
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (2, N'29A_87537', N'71032', CAST(0x0000AB4800735E85 AS DateTime), NULL, 1, N'Hoàng Giáp', NULL)
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (3, N'66A_00545', N'73602', CAST(0x0000AB4800A4E049 AS DateTime), CAST(0x0000AB4800ED6702 AS DateTime), 1, N'Hoàng Minh', N'Hoàng Minh')
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (4, N'18N_6038', N'75025', CAST(0x0000AB4800B576B4 AS DateTime), CAST(0x0000AB4800EDFA23 AS DateTime), 2, N'Hoàng Minh', N'Hoàng Minh')
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (5, N'30Y_9999', N'30373', CAST(0x0000AB4800D70320 AS DateTime), CAST(0x0000AB4801096990 AS DateTime), 1, N'Hoàng Giáp', N'Minh Hoàng')
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (6, N'70F1_66666', N'27253', CAST(0x0000AB4800A86F9E AS DateTime), CAST(0x0000AB4801311AB0 AS DateTime), 1, N'user4', N'user5')
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (7, N'29A_89234', N'72840', CAST(0x0000AB4800A88850 AS DateTime), NULL, 2, N'user4', NULL)
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (8, N'29A_46880', N'77055', CAST(0x0000AB4800FB41E7 AS DateTime), NULL, 1, N'Hoàng Minh', NULL)
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (9, N'29A_04518', N'14143', CAST(0x0000AB4800A8F313 AS DateTime), CAST(0x0000AB4800EF2FB0 AS DateTime), 2, N'user4', N'Hoàng Minh')
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (10, N'29Y_7765', N'13861', CAST(0x0000AB4800A903FF AS DateTime), NULL, 1, N'user4', NULL)
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (11, N'29C_29037', N'83511', CAST(0x0000AB4800DA8807 AS DateTime), NULL, 1, N'Hoàng Giáp', NULL)
INSERT [dbo].[CheckInOut] ([id], [license], [cardId], [checkInTime], [checkOutTime], [pricesId], [checkInUserName], [checkOutUserName]) VALUES (12, N'29A_57289', N'03455', CAST(0x0000AB48011CC252 AS DateTime), NULL, 2, N'user5', NULL)
SET IDENTITY_INSERT [dbo].[CheckInOut] OFF

declare @TargetDate datetime
set @TargetDate = '20200307' --'yyyymmdd'

update dbo.CheckInOut set checkInTime = DATEADD(day,DATEDIFF(day,checkInTime,@TargetDate),checkInTime)
where checkInTime is not null

update dbo.CheckInOut set checkOutTime = DATEADD(day,DATEDIFF(day,checkOutTime,@TargetDate),checkOutTime)
where checkOutTime is not null