USE [master]
GO
/****** Object:  Database [handymanworkapp]    Script Date: 6/14/2019 12:44:07 PM ******/
CREATE DATABASE [handymanworkapp]
GO
ALTER DATABASE [handymanworkapp] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [handymanworkapp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [handymanworkapp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [handymanworkapp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [handymanworkapp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [handymanworkapp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [handymanworkapp] SET ARITHABORT OFF 
GO
ALTER DATABASE [handymanworkapp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [handymanworkapp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [handymanworkapp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [handymanworkapp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [handymanworkapp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [handymanworkapp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [handymanworkapp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [handymanworkapp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [handymanworkapp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [handymanworkapp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [handymanworkapp] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [handymanworkapp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [handymanworkapp] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [handymanworkapp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [handymanworkapp] SET  MULTI_USER 
GO
ALTER DATABASE [handymanworkapp] SET DB_CHAINING OFF 
GO
ALTER DATABASE [handymanworkapp] SET ENCRYPTION ON
GO
ALTER DATABASE [handymanworkapp] SET QUERY_STORE = ON
GO
ALTER DATABASE [handymanworkapp] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
USE [handymanworkapp]
GO
ALTER DATABASE SCOPED CONFIGURATION SET BATCH_MODE_ADAPTIVE_JOINS = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET BATCH_MODE_MEMORY_GRANT_FEEDBACK = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET BATCH_MODE_ON_ROWSTORE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET DEFERRED_COMPILATION_TV = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET ELEVATE_ONLINE = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET ELEVATE_RESUMABLE = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET GLOBAL_TEMPORARY_TABLE_AUTO_DROP = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET INTERLEAVED_EXECUTION_TVF = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET ISOLATE_SECURITY_POLICY_CARDINALITY = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LIGHTWEIGHT_QUERY_PROFILING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET OPTIMIZE_FOR_AD_HOC_WORKLOADS = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET ROW_MODE_MEMORY_GRANT_FEEDBACK = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET VERBOSE_TRUNCATION_WARNINGS = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET XTP_PROCEDURE_EXECUTION_STATISTICS = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET XTP_QUERY_EXECUTION_STATISTICS = OFF;
GO
USE [handymanworkapp]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 6/14/2019 12:44:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[NickName] [nvarchar](50) NULL,
	[Mobile] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Active] [bit] NOT NULL,
	[RoleId] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[DefaultMenuId] [int] NOT NULL,
	[CreatedOnUtc] [datetime] NOT NULL,
	[UpdatedOnUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeSchedule]    Script Date: 6/14/2019 12:44:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeSchedule](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[CreatedOnUtc] [datetime] NOT NULL,
	[UpdatedOnUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_EmployeeSchedule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeScheduleWeek]    Script Date: 6/14/2019 12:44:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeScheduleWeek](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeScheduleId] [int] NOT NULL,
	[MondayIn1] [time](4) NULL,
	[MondayOut1] [time](4) NULL,
	[MondayIn2] [time](4) NULL,
	[MondayOut2] [time](4) NULL,
	[TuesdayIn1] [time](4) NULL,
	[TuesdayOut1] [time](4) NULL,
	[TuesdayIn2] [time](4) NULL,
	[TuesdayOut2] [time](4) NULL,
	[WednesdayIn1] [time](4) NULL,
	[WednesdayOut1] [time](4) NULL,
	[WednesdayIn2] [time](4) NULL,
	[WednesdayOut2] [time](4) NULL,
	[ThursdayIn1] [time](4) NULL,
	[ThursdayOut1] [time](4) NULL,
	[ThursdayIn2] [time](4) NULL,
	[ThursdayOut2] [time](4) NULL,
	[FridayIn1] [time](4) NULL,
	[FridayOut1] [time](4) NULL,
	[FridayIn2] [time](4) NULL,
	[FridayOut2] [time](4) NULL,
	[SaturdayIn1] [time](4) NULL,
	[SaturdayOut1] [time](4) NULL,
	[SaturdayIn2] [time](4) NULL,
	[SaturdayOut2] [time](4) NULL,
	[SundayIn1] [time](4) NULL,
	[SundayOut1] [time](4) NULL,
	[SundayIn2] [time](4) NULL,
	[SundayOut2] [time](4) NULL,
	[CreatedOnUtc] [datetime] NOT NULL,
	[UpdatedOnUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_EmployeeScheduleWeek] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InventoryItem]    Script Date: 6/14/2019 12:44:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NULL,
	[InventoryTypeId] [int] NOT NULL,
	[Price] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreatedOnUtc] [datetime] NOT NULL,
	[UpdatedOnUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_InventoryItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InventoryType]    Script Date: 6/14/2019 12:44:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreatedOnUtc] [datetime] NOT NULL,
	[UpdatedOnUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_InventoryType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 6/14/2019 12:44:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Street1] [nvarchar](50) NOT NULL,
	[Street2] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[Zipcode] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[CreatedOnUtc] [datetime] NOT NULL,
	[UpdatedOnUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaintenanceIssueStatus]    Script Date: 6/14/2019 12:44:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaintenanceIssueStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreatedOnUtc] [datetime] NOT NULL,
	[UpdatedOnUtc] [datetime] NULL,
 CONSTRAINT [PK_MaintenanceIssueStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaintenancePriority]    Script Date: 6/14/2019 12:44:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaintenancePriority](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreatedOnUtc] [datetime] NULL,
	[UpdatedOnUtc] [datetime] NULL,
 CONSTRAINT [PK_MaintenancePriority] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaintenanceService]    Script Date: 6/14/2019 12:44:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaintenanceService](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LocationId] [int] NOT NULL,
	[AssignedEmployeeId] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[MaintenanceIssueStatusId] [int] NOT NULL,
	[MaintenancePriorityId] [int] NOT NULL,
	[DaysToFinish] [int] NULL,
	[RoomId] [int] NULL,
	[Comment] [nvarchar](200) NULL,
	[Description] [nvarchar](200) NULL,
	[CreatedOnUtc] [datetime] NOT NULL,
	[UpdatedOnUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_MaintenanceService] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaintenanceServiceImages]    Script Date: 6/14/2019 12:44:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaintenanceServiceImages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaintenanceServiceId] [int] NOT NULL,
	[Image] [varbinary](max) NOT NULL,
	[CreatedOnUtc] [datetime] NOT NULL,
	[UpdatedOnUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_MaintenanceServiceImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaintenanceServiceStatus]    Script Date: 6/14/2019 12:44:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaintenanceServiceStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MaintenanceServiceId] [int] NOT NULL,
	[Comment] [nvarchar](200) NOT NULL,
	[CreatedById] [int] NOT NULL,
	[CreatedOnUtc] [datetime] NOT NULL,
	[UpdatedOnUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_MaintenanceServiceStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuOptions]    Script Date: 6/14/2019 12:44:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuOptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[RouteUrl] [nvarchar](50) NOT NULL,
	[Component] [nvarchar](50) NULL,
	[ComponentTitle] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOnUtc] [datetime] NOT NULL,
	[UpdatedOnUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_MenuOptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseOrderStatus]    Script Date: 6/14/2019 12:44:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrderStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreatedOnUtc] [datetime] NOT NULL,
	[UpdatedOnUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_PurchaseOrderStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Room]    Script Date: 6/14/2019 12:44:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LocationId] [int] NOT NULL,
	[CreatedOnUtc] [datetime] NOT NULL,
	[UpdatedOnUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 6/14/2019 12:44:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreatedOnUtc] [datetime] NOT NULL,
	[UpdatedOnUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRolesMenuOptionsMapping]    Script Date: 6/14/2019 12:44:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRolesMenuOptionsMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserRoleId] [int] NOT NULL,
	[MenuOptionId] [int] NOT NULL,
	[CreateAccess] [bit] NOT NULL,
	[UpdateAccess] [bit] NOT NULL,
	[ReadAccess] [bit] NOT NULL,
	[DeleteAccess] [bit] NOT NULL,
	[CreatedOnUtc] [datetime] NOT NULL,
	[UpdatedOnUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_UserRolesMenuOptionsMapping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vendor]    Script Date: 6/14/2019 12:44:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Street1] [nvarchar](50) NULL,
	[Street2] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Zipcode] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[Active] [bit] NOT NULL,
	[CreatedOnUtc] [datetime] NOT NULL,
	[UpdatedOnUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_Vendor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeLocation] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_EmployeeLocation]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeMenuOption] FOREIGN KEY([DefaultMenuId])
REFERENCES [dbo].[MenuOptions] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_EmployeeMenuOption]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[UserRoles] ([Id])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_EmployeeRole]
GO
ALTER TABLE [dbo].[EmployeeSchedule]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeScheduleEmployee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[EmployeeSchedule] CHECK CONSTRAINT [FK_EmployeeScheduleEmployee]
GO
ALTER TABLE [dbo].[EmployeeScheduleWeek]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeScheduleWeekEmployeeSchedule] FOREIGN KEY([EmployeeScheduleId])
REFERENCES [dbo].[EmployeeSchedule] ([Id])
GO
ALTER TABLE [dbo].[EmployeeScheduleWeek] CHECK CONSTRAINT [FK_EmployeeScheduleWeekEmployeeSchedule]
GO
ALTER TABLE [dbo].[InventoryItem]  WITH CHECK ADD  CONSTRAINT [FK_InventoryInventoryType] FOREIGN KEY([InventoryTypeId])
REFERENCES [dbo].[InventoryType] ([Id])
GO
ALTER TABLE [dbo].[InventoryItem] CHECK CONSTRAINT [FK_InventoryInventoryType]
GO
ALTER TABLE [dbo].[MaintenanceService]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceServiceEmployee] FOREIGN KEY([AssignedEmployeeId])
REFERENCES [dbo].[Employee] ([Id])
GO
ALTER TABLE [dbo].[MaintenanceService] CHECK CONSTRAINT [FK_MaintenanceServiceEmployee]
GO
ALTER TABLE [dbo].[MaintenanceService]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceServiceLocation] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([Id])
GO
ALTER TABLE [dbo].[MaintenanceService] CHECK CONSTRAINT [FK_MaintenanceServiceLocation]
GO
ALTER TABLE [dbo].[MaintenanceService]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceServiceMaintenanceIssueStatus] FOREIGN KEY([MaintenanceIssueStatusId])
REFERENCES [dbo].[MaintenanceIssueStatus] ([Id])
GO
ALTER TABLE [dbo].[MaintenanceService] CHECK CONSTRAINT [FK_MaintenanceServiceMaintenanceIssueStatus]
GO
ALTER TABLE [dbo].[MaintenanceService]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceServiceMaintenancePriority] FOREIGN KEY([MaintenancePriorityId])
REFERENCES [dbo].[MaintenancePriority] ([Id])
GO
ALTER TABLE [dbo].[MaintenanceService] CHECK CONSTRAINT [FK_MaintenanceServiceMaintenancePriority]
GO
ALTER TABLE [dbo].[MaintenanceService]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceServiceRoom] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
ALTER TABLE [dbo].[MaintenanceService] CHECK CONSTRAINT [FK_MaintenanceServiceRoom]
GO
ALTER TABLE [dbo].[MaintenanceServiceImages]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceServiceImagesMaintenanceService] FOREIGN KEY([MaintenanceServiceId])
REFERENCES [dbo].[MaintenanceService] ([Id])
GO
ALTER TABLE [dbo].[MaintenanceServiceImages] CHECK CONSTRAINT [FK_MaintenanceServiceImagesMaintenanceService]
GO
ALTER TABLE [dbo].[MaintenanceServiceStatus]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceServiceStatusMaintenanceService] FOREIGN KEY([MaintenanceServiceId])
REFERENCES [dbo].[MaintenanceService] ([Id])
GO
ALTER TABLE [dbo].[MaintenanceServiceStatus] CHECK CONSTRAINT [FK_MaintenanceServiceStatusMaintenanceService]
GO
ALTER TABLE [dbo].[Room]  WITH CHECK ADD  CONSTRAINT [FK_RoomLocation] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([Id])
GO
ALTER TABLE [dbo].[Room] CHECK CONSTRAINT [FK_RoomLocation]
GO
ALTER TABLE [dbo].[UserRolesMenuOptionsMapping]  WITH CHECK ADD  CONSTRAINT [FK_UserRolesMenuOptionsMapping] FOREIGN KEY([UserRoleId])
REFERENCES [dbo].[UserRoles] ([Id])
GO
ALTER TABLE [dbo].[UserRolesMenuOptionsMapping] CHECK CONSTRAINT [FK_UserRolesMenuOptionsMapping]
GO
ALTER TABLE [dbo].[UserRolesMenuOptionsMapping]  WITH CHECK ADD  CONSTRAINT [FK_UserRolesMenuOptionsMappingMenuOptions] FOREIGN KEY([MenuOptionId])
REFERENCES [dbo].[MenuOptions] ([Id])
GO
ALTER TABLE [dbo].[UserRolesMenuOptionsMapping] CHECK CONSTRAINT [FK_UserRolesMenuOptionsMappingMenuOptions]
GO
USE [master]
GO
ALTER DATABASE [handymanworkapp] SET  READ_WRITE 
GO
