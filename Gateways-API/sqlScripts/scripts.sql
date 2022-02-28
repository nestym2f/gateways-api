USE [gateways]
GO
/****** Object:  Table [dbo].[Gateways]    Script Date: 2/27/2022 4:26:49 PM ******/
INSERT [dbo].[Gateways] ([serialNumber], [name], [IPv4Address]) VALUES (N'xxx123yyy456', N'musala', N'127.0.0.1')
GO
INSERT [dbo].[Gateways] ([serialNumber], [name], [IPv4Address]) VALUES (N'zzz123yyy456', N'musala2', N'127.0.0.2')
GO
INSERT [dbo].[Gateways] ([serialNumber], [name], [IPv4Address]) VALUES (N'yyy123yyy456', N'musala3', N'127.0.0.3')
GO
INSERT [dbo].[Gateways] ([serialNumber], [name], [IPv4Address]) VALUES (N'www123yyy456', N'musala4', N'127.0.0.4')
GO
INSERT [dbo].[Gateways] ([serialNumber], [name], [IPv4Address]) VALUES (N'www123yyy321', N'musala5', N'127.0.0.5')
GO
INSERT [dbo].[Peripherals] ([Id], [vendor], [dateCreated], [status], [gatewayId]) VALUES (1, N'aVendor', CAST(N'2022-02-28T16:40:05.3917086' AS DateTime2), 1, 1)
GO
INSERT [dbo].[Peripherals] ([Id], [vendor], [dateCreated], [status], [gatewayId]) VALUES (2, N'aVendor', CAST(N'2022-02-28T16:40:11.8714342' AS DateTime2), 0, 1)
GO
INSERT [dbo].[Peripherals] ([Id], [vendor], [dateCreated], [status], [gatewayId]) VALUES (3, N'Sell', CAST(N'2022-02-28T16:40:47.2424942' AS DateTime2), 1, 2)
GO
INSERT [dbo].[Peripherals] ([Id], [vendor], [dateCreated], [status], [gatewayId]) VALUES (4, N'Buy', CAST(N'2022-02-28T16:40:56.0557615' AS DateTime2), 1, 3)
GO
INSERT [dbo].[Peripherals] ([Id], [vendor], [dateCreated], [status], [gatewayId]) VALUES (5, N'Other', CAST(N'2022-02-28T16:41:30.3928294' AS DateTime2), 0, 4)