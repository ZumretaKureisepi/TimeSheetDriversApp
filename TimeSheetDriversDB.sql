USE [master]
GO
/****** Object:  Database [TimeSheetDriversDB]    Script Date: 4. 3. 2021. 14:30:02 ******/
CREATE DATABASE [TimeSheetDriversDB]
GO
ALTER DATABASE [TimeSheetDriversDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TimeSheetDriversDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TimeSheetDriversDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TimeSheetDriversDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TimeSheetDriversDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TimeSheetDriversDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TimeSheetDriversDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TimeSheetDriversDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TimeSheetDriversDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TimeSheetDriversDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TimeSheetDriversDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TimeSheetDriversDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TimeSheetDriversDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TimeSheetDriversDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TimeSheetDriversDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TimeSheetDriversDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TimeSheetDriversDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [TimeSheetDriversDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TimeSheetDriversDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TimeSheetDriversDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TimeSheetDriversDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TimeSheetDriversDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TimeSheetDriversDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [TimeSheetDriversDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TimeSheetDriversDB] SET RECOVERY FULL 
GO
ALTER DATABASE [TimeSheetDriversDB] SET  MULTI_USER 
GO
ALTER DATABASE [TimeSheetDriversDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TimeSheetDriversDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TimeSheetDriversDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TimeSheetDriversDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TimeSheetDriversDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'TimeSheetDriversDB', N'ON'
GO
ALTER DATABASE [TimeSheetDriversDB] SET QUERY_STORE = OFF
GO
USE [TimeSheetDriversDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 4. 3. 2021. 14:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuditFiles]    Script Date: 4. 3. 2021. 14:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditFiles](
	[AuditFileId] [int] IDENTITY(1,1) NOT NULL,
	[Field] [int] NOT NULL,
	[OldValue] [datetime2](7) NULL,
	[NewValue] [datetime2](7) NULL,
	[DateChanged] [datetime2](7) NOT NULL,
	[ChangedByUserId] [int] NOT NULL,
	[TimeSheetId] [int] NOT NULL,
 CONSTRAINT [PK_AuditFiles] PRIMARY KEY CLUSTERED 
(
	[AuditFileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Requests]    Script Date: 4. 3. 2021. 14:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Requests](
	[RequestId] [int] IDENTITY(1,1) NOT NULL,
	[DateOfRequest] [datetime2](7) NOT NULL,
	[RequestType] [int] NOT NULL,
	[StartDateTime] [datetime2](7) NOT NULL,
	[EndDateTime] [datetime2](7) NOT NULL,
	[Status] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Requests] PRIMARY KEY CLUSTERED 
(
	[RequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 4. 3. 2021. 14:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeSheets]    Script Date: 4. 3. 2021. 14:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeSheets](
	[TimeSheetId] [int] IDENTITY(1,1) NOT NULL,
	[Status] [int] NOT NULL,
	[EntryDate] [datetime2](7) NOT NULL,
	[DayType] [int] NOT NULL,
	[StartTime] [datetime2](7) NULL,
	[EndTime] [datetime2](7) NULL,
	[BreakTime] [datetime2](7) NULL,
	[MetersSquared] [int] NULL,
	[KmStand] [int] NULL,
	[PrivatTanken] [int] NULL,
	[Fuel] [int] NULL,
	[AdBlue] [int] NULL,
	[Notes] [nvarchar](max) NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_TimeSheets] PRIMARY KEY CLUSTERED 
(
	[TimeSheetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 4. 3. 2021. 14:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Password] [nvarchar](max) NULL,
	[Username] [nvarchar](max) NULL,
	[PersonalNo] [int] NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[FMNumber] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[Email] [nvarchar](max) NULL,
	[EntryDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WebAuthTokens]    Script Date: 4. 3. 2021. 14:30:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WebAuthTokens](
	[Value] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_WebAuthTokens] PRIMARY KEY CLUSTERED 
(
	[Value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210304132204_Initial', N'3.1.12')
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (1, N'Super User')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (2, N'Admin')
INSERT [dbo].[Roles] ([RoleId], [RoleName]) VALUES (3, N'User')
SET IDENTITY_INSERT [dbo].[Roles] OFF
SET IDENTITY_INSERT [dbo].[TimeSheets] ON 

INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (1, 0, CAST(N'2021-03-04T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (2, 0, CAST(N'2021-03-05T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (3, 0, CAST(N'2021-03-06T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (4, 0, CAST(N'2021-03-07T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (5, 0, CAST(N'2021-03-08T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (6, 0, CAST(N'2021-03-09T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (7, 0, CAST(N'2021-03-10T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (8, 0, CAST(N'2021-03-11T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (9, 0, CAST(N'2021-03-12T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (10, 0, CAST(N'2021-03-13T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (11, 0, CAST(N'2021-03-14T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (12, 0, CAST(N'2021-03-15T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (13, 0, CAST(N'2021-03-16T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (14, 0, CAST(N'2021-03-17T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (15, 0, CAST(N'2021-03-18T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (16, 0, CAST(N'2021-03-19T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (17, 0, CAST(N'2021-03-20T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (18, 0, CAST(N'2021-03-21T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (19, 0, CAST(N'2021-03-22T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (20, 0, CAST(N'2021-03-23T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (21, 0, CAST(N'2021-03-24T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (22, 0, CAST(N'2021-03-25T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (23, 0, CAST(N'2021-03-26T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (24, 0, CAST(N'2021-03-27T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (25, 0, CAST(N'2021-03-28T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (26, 0, CAST(N'2021-03-29T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (27, 0, CAST(N'2021-03-30T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
INSERT [dbo].[TimeSheets] ([TimeSheetId], [Status], [EntryDate], [DayType], [StartTime], [EndTime], [BreakTime], [MetersSquared], [KmStand], [PrivatTanken], [Fuel], [AdBlue], [Notes], [UserId]) VALUES (28, 0, CAST(N'2021-03-31T00:00:00.0000000' AS DateTime2), 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2)
SET IDENTITY_INSERT [dbo].[TimeSheets] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [Password], [Username], [PersonalNo], [FirstName], [LastName], [FMNumber], [RoleId], [Email], [EntryDate]) VALUES (1, N'$2y$12$VgzoyMZuq/NHYBENXcYgqeOrWMstxZu6Pkdc2/Oy1a3FPEqExH8RO', N'Dakn', 2001, N'Dario', N'Knezevic', 1, 2, N'dario@gmail.com', CAST(N'2021-03-04T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Users] ([UserId], [Password], [Username], [PersonalNo], [FirstName], [LastName], [FMNumber], [RoleId], [Email], [EntryDate]) VALUES (2, N'$2a$11$llI15Fn1hcocEDTm.3A/8.FQ4.QtR1hPQZv8Q9pHFcSUfF/Ct0J7m', N'Elta', 3002, N'Elvin', N'Taric', 2, 3, N'elvin@gmail.com', CAST(N'2021-03-04T14:29:19.2233111' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Users] OFF
INSERT [dbo].[WebAuthTokens] ([Value], [UserId], [DateCreated]) VALUES (N'ec9e7848-10d0-487d-8c1e-aecb448d4bd1', 1, CAST(N'2021-03-04T14:27:52.0469084' AS DateTime2))
/****** Object:  Index [IX_AuditFiles_ChangedByUserId]    Script Date: 4. 3. 2021. 14:30:03 ******/
CREATE NONCLUSTERED INDEX [IX_AuditFiles_ChangedByUserId] ON [dbo].[AuditFiles]
(
	[ChangedByUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AuditFiles_TimeSheetId]    Script Date: 4. 3. 2021. 14:30:03 ******/
CREATE NONCLUSTERED INDEX [IX_AuditFiles_TimeSheetId] ON [dbo].[AuditFiles]
(
	[TimeSheetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Requests_UserId]    Script Date: 4. 3. 2021. 14:30:03 ******/
CREATE NONCLUSTERED INDEX [IX_Requests_UserId] ON [dbo].[Requests]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_TimeSheets_UserId]    Script Date: 4. 3. 2021. 14:30:03 ******/
CREATE NONCLUSTERED INDEX [IX_TimeSheets_UserId] ON [dbo].[TimeSheets]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_RoleId]    Script Date: 4. 3. 2021. 14:30:03 ******/
CREATE NONCLUSTERED INDEX [IX_Users_RoleId] ON [dbo].[Users]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_WebAuthTokens_UserId]    Script Date: 4. 3. 2021. 14:30:03 ******/
CREATE NONCLUSTERED INDEX [IX_WebAuthTokens_UserId] ON [dbo].[WebAuthTokens]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AuditFiles]  WITH CHECK ADD  CONSTRAINT [FK_AuditFiles_TimeSheets_TimeSheetId] FOREIGN KEY([TimeSheetId])
REFERENCES [dbo].[TimeSheets] ([TimeSheetId])
GO
ALTER TABLE [dbo].[AuditFiles] CHECK CONSTRAINT [FK_AuditFiles_TimeSheets_TimeSheetId]
GO
ALTER TABLE [dbo].[AuditFiles]  WITH CHECK ADD  CONSTRAINT [FK_AuditFiles_Users_ChangedByUserId] FOREIGN KEY([ChangedByUserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AuditFiles] CHECK CONSTRAINT [FK_AuditFiles_Users_ChangedByUserId]
GO
ALTER TABLE [dbo].[Requests]  WITH CHECK ADD  CONSTRAINT [FK_Requests_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Requests] CHECK CONSTRAINT [FK_Requests_Users_UserId]
GO
ALTER TABLE [dbo].[TimeSheets]  WITH CHECK ADD  CONSTRAINT [FK_TimeSheets_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TimeSheets] CHECK CONSTRAINT [FK_TimeSheets_Users_UserId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles_RoleId]
GO
ALTER TABLE [dbo].[WebAuthTokens]  WITH CHECK ADD  CONSTRAINT [FK_WebAuthTokens_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WebAuthTokens] CHECK CONSTRAINT [FK_WebAuthTokens_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [TimeSheetDriversDB] SET  READ_WRITE 
GO
