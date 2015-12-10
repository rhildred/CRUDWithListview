USE [master]
GO
DROP DATABASE cds
GO
/****** Object:  Database [cds]    Script Date: 09/12/2015 2:40:14 PM ******/
CREATE DATABASE [cds]
GO
USE cds
GO
CREATE TABLE [dbo].[cds](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](50) NULL,
	[artist] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[cds] ON 

INSERT [dbo].[cds] ([id], [title], [artist]) VALUES (1, N'troubadoor', N'knaan')
SET IDENTITY_INSERT [dbo].[cds] OFF
USE [master]
GO
ALTER DATABASE [cds] SET  READ_WRITE 
GO
