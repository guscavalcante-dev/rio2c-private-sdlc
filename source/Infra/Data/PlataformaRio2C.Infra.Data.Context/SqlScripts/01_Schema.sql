/****** Object:  Table [dbo].[Activity]    Script Date: 11/07/2019 11:56:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Activity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Activity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Address]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Address](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ZipCode] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[AddressValue] [varchar](1000) NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CountryId] [int] NULL,
	[StateId] [int] NULL,
	[CityId] [int] NULL,
 CONSTRAINT [PK_dbo.Address] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AppAesEncryptionInfo]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AppAesEncryptionInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Password] [varchar](150) NULL,
	[Salt] [varchar](150) NULL,
	[PasswordIterations] [int] NOT NULL,
	[InitialVector] [varchar](150) NULL,
	[KeySize] [int] NOT NULL,
	[Code] [int] NOT NULL,
 CONSTRAINT [PK_dbo.AppAesEncryptionInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ClaimType] [varchar](8000) NULL,
	[ClaimValue] [varchar](8000) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [varchar](128) NOT NULL,
	[ProviderKey] [varchar](128) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Email] [varchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [varchar](8000) NULL,
	[SecurityStamp] [varchar](8000) NULL,
	[PhoneNumber] [varchar](8000) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [varchar](256) NOT NULL,
	[PasswordNew] [varchar](50) NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[City]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[City](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[StateId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Address_Id] [int] NULL,
 CONSTRAINT [PK_dbo.City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Collaborator]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Collaborator](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[AddressId] [int] NULL,
	[PlayerId] [int] NULL,
	[UserId] [int] NOT NULL,
	[ImageId] [int] NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[PhoneNumber] [varchar](50) NULL,
	[CellPhone] [varchar](50) NULL,
	[Badge] [varchar](50) NULL,
	[SpeakerId] [int] NULL,
 CONSTRAINT [PK_dbo.Collaborator] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CollaboratorJobTitle]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CollaboratorJobTitle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](256) NULL,
	[LanguageId] [int] NOT NULL,
	[CollaboratoId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.CollaboratorJobTitle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CollaboratorMiniBio]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CollaboratorMiniBio](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](8000) NULL,
	[LanguageId] [int] NOT NULL,
	[CollaboratoId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.CollaboratorMiniBio] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CollaboratorPlayer]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CollaboratorPlayer](
	[CollaboratorId] [int] NOT NULL,
	[PlayerId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.CollaboratorPlayer] PRIMARY KEY CLUSTERED 
(
	[CollaboratorId] ASC,
	[PlayerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CollaboratorProducer]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CollaboratorProducer](
	[CollaboratorId] [int] NOT NULL,
	[ProducerId] [int] NOT NULL,
	[EventId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.CollaboratorProducer] PRIMARY KEY CLUSTERED 
(
	[CollaboratorId] ASC,
	[ProducerId] ASC,
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Conference]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Conference](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[Info] [varchar](3000) NULL,
	[RoomId] [int] NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Conference] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ConferenceLecturer]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConferenceLecturer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsPreRegistered] [bit] NOT NULL,
	[ConferenceId] [int] NOT NULL,
	[CollaboratorId] [int] NULL,
	[LecturerId] [int] NULL,
	[RoleLecturerId] [int] NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ConferenceLecturer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ConferenceSynopsis]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ConferenceSynopsis](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](8000) NULL,
	[LanguageId] [int] NOT NULL,
	[ConferenceId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ConferenceSynopsis] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ConferenceTitle]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ConferenceTitle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](8000) NULL,
	[LanguageId] [int] NOT NULL,
	[ConferenceId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ConferenceTitle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Country]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [varchar](100) NULL,
	[CountryCode] [varchar](3) NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Address_Id] [int] NULL,
 CONSTRAINT [PK_dbo.Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Event]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Event](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Event] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Holding]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Holding](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[ImageId] [int] NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Holding] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[HoldingDescription]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[HoldingDescription](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](8000) NULL,
	[LanguageId] [int] NOT NULL,
	[HoldingId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.HoldingDescription] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ImageFile]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ImageFile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [varchar](500) NOT NULL,
	[File] [varbinary](max) NOT NULL,
	[ContentType] [varchar](200) NOT NULL,
	[ContentLength] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ImageFile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Interest]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Interest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InterestGroupId] [int] NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Interest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[InterestGroup]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InterestGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Type] [varchar](100) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.InterestGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Language]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Language](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Language] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Lecturer]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Lecturer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](1000) NULL,
	[ImageId] [int] NULL,
	[Email] [varchar](50) NULL,
	[CompanyName] [varchar](50) NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Lecturer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LecturerJobTitle]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LecturerJobTitle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](50) NULL,
	[LanguageId] [int] NOT NULL,
	[LanguageCode] [varchar](50) NULL,
	[LecturerId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.LecturerJobTitle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Logistics]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Logistics](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ArrivalTime] [time](7) NOT NULL,
	[DepartureTime] [time](7) NOT NULL,
	[CollaboratorId] [int] NOT NULL,
	[EventId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ArrivalDate] [datetime] NOT NULL,
	[DepartureDate] [datetime] NOT NULL,
	[OriginalName] [varchar](100) NULL,
	[ServerName] [varchar](100) NULL,
 CONSTRAINT [PK_dbo.Logistics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Mail]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Mail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[Subject] [varchar](50) NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Mail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MailCollaborator]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailCollaborator](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[IdMailCollaborator] [int] NOT NULL,
	[IdMail] [int] NOT NULL,
	[IdCollaborator] [int] NOT NULL,
	[SendDate] [datetime] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Collaborator_Id] [int] NULL,
	[Mail_Id] [int] NULL,
 CONSTRAINT [PK_dbo.MailCollaborator] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Message]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](max) NULL,
	[IsRead] [bit] NOT NULL,
	[SenderId] [int] NOT NULL,
	[RecipientId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Message] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Negotiation]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Negotiation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[PlayerId] [int] NOT NULL,
	[RoomId] [int] NOT NULL,
	[EvaluationId] [int] NULL,
	[Date] [datetime] NOT NULL,
	[StarTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[TableNumber] [int] NOT NULL,
	[RoundNumber] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Negotiation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NegotiationConfig]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NegotiationConfig](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[RoudsFirstTurn] [int] NOT NULL,
	[RoundsSecondTurn] [int] NOT NULL,
	[TimeIntervalBetweenTurn] [time](7) NOT NULL,
	[TimeOfEachRound] [time](7) NOT NULL,
	[TimeIntervalBetweenRound] [time](7) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.NegotiationConfig] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NegotiationRoomConfig]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NegotiationRoomConfig](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoomId] [int] NOT NULL,
	[CountAutomaticTables] [int] NOT NULL,
	[NegotiationConfigId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[CountManualTables] [int] NOT NULL,
 CONSTRAINT [PK_dbo.NegotiationRoomConfig] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Player]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Player](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[CNPJ] [varchar](50) NULL,
	[Website] [varchar](100) NULL,
	[PhoneNumber] [varchar](50) NULL,
	[ImageId] [int] NULL,
	[AddressId] [int] NULL,
	[HoldingId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[TradeName] [varchar](100) NULL,
	[SocialMedia] [varchar](256) NULL,
	[CompanyName] [varchar](100) NULL,
 CONSTRAINT [PK_dbo.Player] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PlayerActivity]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayerActivity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PlayerId] [int] NOT NULL,
	[ActivityId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.PlayerActivity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PlayerDescription]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PlayerDescription](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](8000) NULL,
	[LanguageId] [int] NOT NULL,
	[PlayerId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.PlayerDescription] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PlayerInterest]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayerInterest](
	[PlayerId] [int] NOT NULL,
	[InterestId] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.PlayerInterest] PRIMARY KEY CLUSTERED 
(
	[PlayerId] ASC,
	[InterestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PlayerRestrictionsSpecifics]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PlayerRestrictionsSpecifics](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](8000) NULL,
	[LanguageId] [int] NOT NULL,
	[PlayerId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.PlayerRestrictionsSpecifics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PlayerTargetAudience]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayerTargetAudience](
	[PlayerId] [int] NOT NULL,
	[TargetAudienceId] [int] NOT NULL,
	[Id] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.PlayerTargetAudience] PRIMARY KEY CLUSTERED 
(
	[PlayerId] ASC,
	[TargetAudienceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Producer]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Producer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[TradeName] [varchar](100) NULL,
	[CNPJ] [varchar](50) NULL,
	[Website] [varchar](100) NULL,
	[SocialMedia] [varchar](256) NULL,
	[PhoneNumber] [varchar](50) NULL,
	[ImageId] [int] NULL,
	[AddressId] [int] NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Producer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProducerActivity]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProducerActivity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProducerId] [int] NOT NULL,
	[ActivityId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ProducerActivity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProducerDescription]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProducerDescription](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](8000) NULL,
	[LanguageId] [int] NOT NULL,
	[ProducerId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ProducerDescription] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProducerEvent]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProducerEvent](
	[ProducerId] [int] NOT NULL,
	[EventId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ProducerEvent] PRIMARY KEY CLUSTERED 
(
	[ProducerId] ASC,
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProducerTargetAudience]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProducerTargetAudience](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProducerId] [int] NOT NULL,
	[TargetAudienceId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ProducerTargetAudience] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Project]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumberOfEpisodes] [int] NOT NULL,
	[EachEpisodePlayingTime] [varchar](50) NULL,
	[ValuePerEpisode] [varchar](50) NULL,
	[TotalValueOfProject] [varchar](50) NULL,
	[ValueAlreadyRaised] [varchar](50) NULL,
	[ValueStillNeeded] [varchar](50) NULL,
	[Pitching] [bit] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ProducerId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProjectAdditionalInformation]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectAdditionalInformation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[LanguageId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ProjectAdditionalInformation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectInterest]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectInterest](
	[ProjectId] [int] NOT NULL,
	[InterestId] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ProjectInterest] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC,
	[InterestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectLinkImage]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectLinkImage](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](3000) NULL,
	[ProjectId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ProjectLinkImage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProjectLinkTeaser]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectLinkTeaser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](3000) NULL,
	[ProjectId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ProjectLinkTeaser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProjectLogLine]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectLogLine](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](8000) NULL,
	[LanguageId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ProjectLogLine] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProjectPlayer]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectPlayer](
	[PlayerId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Sent] [bit] NOT NULL,
	[SavedUserId] [int] NULL,
	[SendingUserId] [int] NULL,
	[DateSaved] [datetime] NULL,
	[DateSending] [datetime] NULL,
	[CreationDate] [datetime] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EvaluationId] [int] NULL,
 CONSTRAINT [PK_dbo.ProjectPlayer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectPlayerEvaluation]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectPlayerEvaluation](
	[Id] [int] NOT NULL,
	[ProjectPlayerId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[EvaluationUserId] [int] NOT NULL,
	[Reason] [varchar](1500) NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ProjectPlayerEvaluation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProjectProductionPlan]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectProductionPlan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[LanguageId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ProjectProductionPlan] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectStatus]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](150) NOT NULL,
	[Name] [varchar](50) NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ProjectStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProjectSummary]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectSummary](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[LanguageId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ProjectSummary] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectTitle]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectTitle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](256) NULL,
	[LanguageId] [int] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.ProjectTitle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Quiz]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Quiz](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EventId] [int] NOT NULL,
	[Name] [varchar](50) NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Quiz] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuizAnswer]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QuizAnswer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[OptionId] [int] NOT NULL,
	[Value] [varchar](50) NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.QuizAnswer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuizOption]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QuizOption](
	[Id] [int] NOT NULL,
	[QuestionId] [int] NOT NULL,
	[Text] [bit] NOT NULL,
	[Value] [varchar](200) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.QuizOption] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuizQuestion]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QuizQuestion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuizId] [int] NOT NULL,
	[Question] [varchar](200) NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.QuizQuestion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoleLecturer]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleLecturer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.RoleLecturer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoleLecturerTitle]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RoleLecturerTitle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](256) NULL,
	[LanguageId] [int] NOT NULL,
	[RoleLecturerId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.RoleLecturerTitle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Room]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Room](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Room] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoomName]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RoomName](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [varchar](256) NULL,
	[LanguageId] [int] NOT NULL,
	[RoomId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.RoomName] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Speaker]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Speaker](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CollaboratorId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[State]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[State](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [varchar](100) NULL,
	[StateCode] [varchar](2) NULL,
	[CountryId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Address_Id] [int] NULL,
 CONSTRAINT [PK_dbo.State] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SystemParameter]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SystemParameter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Code] [int] NOT NULL,
	[LanguageCode] [int] NOT NULL,
	[GroupCode] [int] NOT NULL,
	[TypeName] [varchar](150) NULL,
	[Description] [varchar](256) NULL,
	[Value] [varchar](1000) NULL,
	[SubCode] [varchar](150) NULL,
	[DateChanges] [datetime] NULL,
 CONSTRAINT [PK_dbo.SystemParameter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TargetAudience]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TargetAudience](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.TargetAudience] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserUseTerm]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserUseTerm](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[EventId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.UserUseTerm] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Event] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [StartDate]
GO
ALTER TABLE [dbo].[Event] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [EndDate]
GO
ALTER TABLE [dbo].[Logistics] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [ArrivalDate]
GO
ALTER TABLE [dbo].[Logistics] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [DepartureDate]
GO
ALTER TABLE [dbo].[NegotiationRoomConfig] ADD  DEFAULT ((0)) FOR [CountManualTables]
GO
ALTER TABLE [dbo].[Project] ADD  DEFAULT ((0)) FOR [ProducerId]
GO
ALTER TABLE [dbo].[Speaker] ADD  CONSTRAINT [DF_Speaker_Uid]  DEFAULT (newid()) FOR [Uid]
GO
ALTER TABLE [dbo].[UserUseTerm] ADD  DEFAULT ((2)) FOR [RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[City]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.City_dbo.Address_Address_Id] FOREIGN KEY([Address_Id])
REFERENCES [dbo].[Address] ([Id])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_dbo.City_dbo.Address_Address_Id]
GO
ALTER TABLE [dbo].[City]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.City_dbo.State_StateId] FOREIGN KEY([StateId])
REFERENCES [dbo].[State] ([Id])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_dbo.City_dbo.State_StateId]
GO
ALTER TABLE [dbo].[Collaborator]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Collaborator_dbo.Address_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([Id])
GO
ALTER TABLE [dbo].[Collaborator] CHECK CONSTRAINT [FK_dbo.Collaborator_dbo.Address_AddressId]
GO
ALTER TABLE [dbo].[Collaborator]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Collaborator_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Collaborator] CHECK CONSTRAINT [FK_dbo.Collaborator_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Collaborator]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Collaborator_dbo.ImageFile_ImageId] FOREIGN KEY([ImageId])
REFERENCES [dbo].[ImageFile] ([Id])
GO
ALTER TABLE [dbo].[Collaborator] CHECK CONSTRAINT [FK_dbo.Collaborator_dbo.ImageFile_ImageId]
GO
ALTER TABLE [dbo].[Collaborator]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Collaborator_dbo.Player_PlayerId] FOREIGN KEY([PlayerId])
REFERENCES [dbo].[Player] ([Id])
GO
ALTER TABLE [dbo].[Collaborator] CHECK CONSTRAINT [FK_dbo.Collaborator_dbo.Player_PlayerId]
GO
ALTER TABLE [dbo].[CollaboratorJobTitle]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.CollaboratorJobTitle_dbo.Collaborator_CollaboratoId] FOREIGN KEY([CollaboratoId])
REFERENCES [dbo].[Collaborator] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorJobTitle] CHECK CONSTRAINT [FK_dbo.CollaboratorJobTitle_dbo.Collaborator_CollaboratoId]
GO
ALTER TABLE [dbo].[CollaboratorJobTitle]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.CollaboratorJobTitle_dbo.Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorJobTitle] CHECK CONSTRAINT [FK_dbo.CollaboratorJobTitle_dbo.Language_LanguageId]
GO
ALTER TABLE [dbo].[CollaboratorMiniBio]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.CollaboratorMiniBio_dbo.Collaborator_CollaboratoId] FOREIGN KEY([CollaboratoId])
REFERENCES [dbo].[Collaborator] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorMiniBio] CHECK CONSTRAINT [FK_dbo.CollaboratorMiniBio_dbo.Collaborator_CollaboratoId]
GO
ALTER TABLE [dbo].[CollaboratorMiniBio]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.CollaboratorMiniBio_dbo.Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorMiniBio] CHECK CONSTRAINT [FK_dbo.CollaboratorMiniBio_dbo.Language_LanguageId]
GO
ALTER TABLE [dbo].[CollaboratorPlayer]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.CollaboratorPlayer_dbo.Collaborator_CollaboratorId] FOREIGN KEY([CollaboratorId])
REFERENCES [dbo].[Collaborator] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorPlayer] CHECK CONSTRAINT [FK_dbo.CollaboratorPlayer_dbo.Collaborator_CollaboratorId]
GO
ALTER TABLE [dbo].[CollaboratorPlayer]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.CollaboratorPlayer_dbo.Player_PlayerId] FOREIGN KEY([PlayerId])
REFERENCES [dbo].[Player] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorPlayer] CHECK CONSTRAINT [FK_dbo.CollaboratorPlayer_dbo.Player_PlayerId]
GO
ALTER TABLE [dbo].[CollaboratorProducer]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.CollaboratorProducer_dbo.Collaborator_CollaboratorId] FOREIGN KEY([CollaboratorId])
REFERENCES [dbo].[Collaborator] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorProducer] CHECK CONSTRAINT [FK_dbo.CollaboratorProducer_dbo.Collaborator_CollaboratorId]
GO
ALTER TABLE [dbo].[CollaboratorProducer]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.CollaboratorProducer_dbo.Event_EventId] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorProducer] CHECK CONSTRAINT [FK_dbo.CollaboratorProducer_dbo.Event_EventId]
GO
ALTER TABLE [dbo].[CollaboratorProducer]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.CollaboratorProducer_dbo.Producer_ProducerId] FOREIGN KEY([ProducerId])
REFERENCES [dbo].[Producer] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorProducer] CHECK CONSTRAINT [FK_dbo.CollaboratorProducer_dbo.Producer_ProducerId]
GO
ALTER TABLE [dbo].[Conference]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Conference_dbo.Room_RoomId] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
ALTER TABLE [dbo].[Conference] CHECK CONSTRAINT [FK_dbo.Conference_dbo.Room_RoomId]
GO
ALTER TABLE [dbo].[ConferenceLecturer]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ConferenceLecturer_dbo.Collaborator_CollaboratorId] FOREIGN KEY([CollaboratorId])
REFERENCES [dbo].[Collaborator] ([Id])
GO
ALTER TABLE [dbo].[ConferenceLecturer] CHECK CONSTRAINT [FK_dbo.ConferenceLecturer_dbo.Collaborator_CollaboratorId]
GO
ALTER TABLE [dbo].[ConferenceLecturer]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ConferenceLecturer_dbo.Conference_ConferenceId] FOREIGN KEY([ConferenceId])
REFERENCES [dbo].[Conference] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ConferenceLecturer] CHECK CONSTRAINT [FK_dbo.ConferenceLecturer_dbo.Conference_ConferenceId]
GO
ALTER TABLE [dbo].[ConferenceLecturer]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ConferenceLecturer_dbo.Lecturer_LecturerId] FOREIGN KEY([LecturerId])
REFERENCES [dbo].[Lecturer] ([Id])
GO
ALTER TABLE [dbo].[ConferenceLecturer] CHECK CONSTRAINT [FK_dbo.ConferenceLecturer_dbo.Lecturer_LecturerId]
GO
ALTER TABLE [dbo].[ConferenceLecturer]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ConferenceLecturer_dbo.RoleLecturer_RoleLecturerId] FOREIGN KEY([RoleLecturerId])
REFERENCES [dbo].[RoleLecturer] ([Id])
GO
ALTER TABLE [dbo].[ConferenceLecturer] CHECK CONSTRAINT [FK_dbo.ConferenceLecturer_dbo.RoleLecturer_RoleLecturerId]
GO
ALTER TABLE [dbo].[ConferenceSynopsis]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ConferenceSynopsis_dbo.Conference_ConferenceId] FOREIGN KEY([ConferenceId])
REFERENCES [dbo].[Conference] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ConferenceSynopsis] CHECK CONSTRAINT [FK_dbo.ConferenceSynopsis_dbo.Conference_ConferenceId]
GO
ALTER TABLE [dbo].[ConferenceSynopsis]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ConferenceSynopsis_dbo.Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[ConferenceSynopsis] CHECK CONSTRAINT [FK_dbo.ConferenceSynopsis_dbo.Language_LanguageId]
GO
ALTER TABLE [dbo].[ConferenceTitle]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ConferenceTitle_dbo.Conference_ConferenceId] FOREIGN KEY([ConferenceId])
REFERENCES [dbo].[Conference] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ConferenceTitle] CHECK CONSTRAINT [FK_dbo.ConferenceTitle_dbo.Conference_ConferenceId]
GO
ALTER TABLE [dbo].[ConferenceTitle]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ConferenceTitle_dbo.Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[ConferenceTitle] CHECK CONSTRAINT [FK_dbo.ConferenceTitle_dbo.Language_LanguageId]
GO
ALTER TABLE [dbo].[Country]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Country_dbo.Address_Address_Id] FOREIGN KEY([Address_Id])
REFERENCES [dbo].[Address] ([Id])
GO
ALTER TABLE [dbo].[Country] CHECK CONSTRAINT [FK_dbo.Country_dbo.Address_Address_Id]
GO
ALTER TABLE [dbo].[Holding]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Holding_dbo.ImageFile_ImageId] FOREIGN KEY([ImageId])
REFERENCES [dbo].[ImageFile] ([Id])
GO
ALTER TABLE [dbo].[Holding] CHECK CONSTRAINT [FK_dbo.Holding_dbo.ImageFile_ImageId]
GO
ALTER TABLE [dbo].[HoldingDescription]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.HoldingDescription_dbo.Holding_HoldingId] FOREIGN KEY([HoldingId])
REFERENCES [dbo].[Holding] ([Id])
GO
ALTER TABLE [dbo].[HoldingDescription] CHECK CONSTRAINT [FK_dbo.HoldingDescription_dbo.Holding_HoldingId]
GO
ALTER TABLE [dbo].[HoldingDescription]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.HoldingDescription_dbo.Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[HoldingDescription] CHECK CONSTRAINT [FK_dbo.HoldingDescription_dbo.Language_LanguageId]
GO
ALTER TABLE [dbo].[Interest]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Interest_dbo.InterestGroup_InterestGroupId] FOREIGN KEY([InterestGroupId])
REFERENCES [dbo].[InterestGroup] ([Id])
GO
ALTER TABLE [dbo].[Interest] CHECK CONSTRAINT [FK_dbo.Interest_dbo.InterestGroup_InterestGroupId]
GO
ALTER TABLE [dbo].[Lecturer]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Lecturer_dbo.ImageFile_ImageId] FOREIGN KEY([ImageId])
REFERENCES [dbo].[ImageFile] ([Id])
GO
ALTER TABLE [dbo].[Lecturer] CHECK CONSTRAINT [FK_dbo.Lecturer_dbo.ImageFile_ImageId]
GO
ALTER TABLE [dbo].[LecturerJobTitle]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.LecturerJobTitle_dbo.Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[LecturerJobTitle] CHECK CONSTRAINT [FK_dbo.LecturerJobTitle_dbo.Language_LanguageId]
GO
ALTER TABLE [dbo].[LecturerJobTitle]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.LecturerJobTitle_dbo.Lecturer_LecturerId] FOREIGN KEY([LecturerId])
REFERENCES [dbo].[Lecturer] ([Id])
GO
ALTER TABLE [dbo].[LecturerJobTitle] CHECK CONSTRAINT [FK_dbo.LecturerJobTitle_dbo.Lecturer_LecturerId]
GO
ALTER TABLE [dbo].[Logistics]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Logistics_dbo.Collaborator_CollaboratorId] FOREIGN KEY([CollaboratorId])
REFERENCES [dbo].[Collaborator] ([Id])
GO
ALTER TABLE [dbo].[Logistics] CHECK CONSTRAINT [FK_dbo.Logistics_dbo.Collaborator_CollaboratorId]
GO
ALTER TABLE [dbo].[Logistics]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Logistics_dbo.Event_EventId] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([Id])
GO
ALTER TABLE [dbo].[Logistics] CHECK CONSTRAINT [FK_dbo.Logistics_dbo.Event_EventId]
GO
ALTER TABLE [dbo].[MailCollaborator]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MailCollaborator_dbo.Collaborator_Collaborator_Id] FOREIGN KEY([Collaborator_Id])
REFERENCES [dbo].[Collaborator] ([Id])
GO
ALTER TABLE [dbo].[MailCollaborator] CHECK CONSTRAINT [FK_dbo.MailCollaborator_dbo.Collaborator_Collaborator_Id]
GO
ALTER TABLE [dbo].[MailCollaborator]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MailCollaborator_dbo.Mail_Mail_Id] FOREIGN KEY([Mail_Id])
REFERENCES [dbo].[Mail] ([Id])
GO
ALTER TABLE [dbo].[MailCollaborator] CHECK CONSTRAINT [FK_dbo.MailCollaborator_dbo.Mail_Mail_Id]
GO
ALTER TABLE [dbo].[Message]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Message_dbo.AspNetUsers_RecipientId] FOREIGN KEY([RecipientId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_dbo.Message_dbo.AspNetUsers_RecipientId]
GO
ALTER TABLE [dbo].[Message]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Message_dbo.AspNetUsers_SenderId] FOREIGN KEY([SenderId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_dbo.Message_dbo.AspNetUsers_SenderId]
GO
ALTER TABLE [dbo].[Negotiation]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Negotiation_dbo.Player_PlayerId] FOREIGN KEY([PlayerId])
REFERENCES [dbo].[Player] ([Id])
GO
ALTER TABLE [dbo].[Negotiation] CHECK CONSTRAINT [FK_dbo.Negotiation_dbo.Player_PlayerId]
GO
ALTER TABLE [dbo].[Negotiation]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Negotiation_dbo.Project_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Negotiation] CHECK CONSTRAINT [FK_dbo.Negotiation_dbo.Project_ProjectId]
GO
ALTER TABLE [dbo].[Negotiation]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Negotiation_dbo.ProjectPlayerEvaluation_EvaluationId] FOREIGN KEY([EvaluationId])
REFERENCES [dbo].[ProjectPlayerEvaluation] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Negotiation] CHECK CONSTRAINT [FK_dbo.Negotiation_dbo.ProjectPlayerEvaluation_EvaluationId]
GO
ALTER TABLE [dbo].[Negotiation]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Negotiation_dbo.Room_RoomId] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
ALTER TABLE [dbo].[Negotiation] CHECK CONSTRAINT [FK_dbo.Negotiation_dbo.Room_RoomId]
GO
ALTER TABLE [dbo].[NegotiationRoomConfig]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.NegotiationRoomConfig_dbo.NegotiationConfig_NegotiationConfigId] FOREIGN KEY([NegotiationConfigId])
REFERENCES [dbo].[NegotiationConfig] ([Id])
GO
ALTER TABLE [dbo].[NegotiationRoomConfig] CHECK CONSTRAINT [FK_dbo.NegotiationRoomConfig_dbo.NegotiationConfig_NegotiationConfigId]
GO
ALTER TABLE [dbo].[NegotiationRoomConfig]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.NegotiationRoomConfig_dbo.Room_RoomId] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
ALTER TABLE [dbo].[NegotiationRoomConfig] CHECK CONSTRAINT [FK_dbo.NegotiationRoomConfig_dbo.Room_RoomId]
GO
ALTER TABLE [dbo].[Player]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Player_dbo.Address_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([Id])
GO
ALTER TABLE [dbo].[Player] CHECK CONSTRAINT [FK_dbo.Player_dbo.Address_AddressId]
GO
ALTER TABLE [dbo].[Player]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Player_dbo.Holding_HoldingId] FOREIGN KEY([HoldingId])
REFERENCES [dbo].[Holding] ([Id])
GO
ALTER TABLE [dbo].[Player] CHECK CONSTRAINT [FK_dbo.Player_dbo.Holding_HoldingId]
GO
ALTER TABLE [dbo].[Player]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Player_dbo.ImageFile_ImageId] FOREIGN KEY([ImageId])
REFERENCES [dbo].[ImageFile] ([Id])
GO
ALTER TABLE [dbo].[Player] CHECK CONSTRAINT [FK_dbo.Player_dbo.ImageFile_ImageId]
GO
ALTER TABLE [dbo].[PlayerActivity]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.PlayerActivity_dbo.Activity_ActivityId] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity] ([Id])
GO
ALTER TABLE [dbo].[PlayerActivity] CHECK CONSTRAINT [FK_dbo.PlayerActivity_dbo.Activity_ActivityId]
GO
ALTER TABLE [dbo].[PlayerActivity]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.PlayerActivity_dbo.Player_PlayerId] FOREIGN KEY([PlayerId])
REFERENCES [dbo].[Player] ([Id])
GO
ALTER TABLE [dbo].[PlayerActivity] CHECK CONSTRAINT [FK_dbo.PlayerActivity_dbo.Player_PlayerId]
GO
ALTER TABLE [dbo].[PlayerDescription]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.PlayerDescription_dbo.Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[PlayerDescription] CHECK CONSTRAINT [FK_dbo.PlayerDescription_dbo.Language_LanguageId]
GO
ALTER TABLE [dbo].[PlayerDescription]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.PlayerDescription_dbo.Player_PlayerId] FOREIGN KEY([PlayerId])
REFERENCES [dbo].[Player] ([Id])
GO
ALTER TABLE [dbo].[PlayerDescription] CHECK CONSTRAINT [FK_dbo.PlayerDescription_dbo.Player_PlayerId]
GO
ALTER TABLE [dbo].[PlayerInterest]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.PlayerInterest_dbo.Interest_InterestId] FOREIGN KEY([InterestId])
REFERENCES [dbo].[Interest] ([Id])
GO
ALTER TABLE [dbo].[PlayerInterest] CHECK CONSTRAINT [FK_dbo.PlayerInterest_dbo.Interest_InterestId]
GO
ALTER TABLE [dbo].[PlayerInterest]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.PlayerInterest_dbo.Player_PlayerId] FOREIGN KEY([PlayerId])
REFERENCES [dbo].[Player] ([Id])
GO
ALTER TABLE [dbo].[PlayerInterest] CHECK CONSTRAINT [FK_dbo.PlayerInterest_dbo.Player_PlayerId]
GO
ALTER TABLE [dbo].[PlayerRestrictionsSpecifics]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.PlayerRestrictionsSpecifics_dbo.Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[PlayerRestrictionsSpecifics] CHECK CONSTRAINT [FK_dbo.PlayerRestrictionsSpecifics_dbo.Language_LanguageId]
GO
ALTER TABLE [dbo].[PlayerRestrictionsSpecifics]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.PlayerRestrictionsSpecifics_dbo.Player_PlayerId] FOREIGN KEY([PlayerId])
REFERENCES [dbo].[Player] ([Id])
GO
ALTER TABLE [dbo].[PlayerRestrictionsSpecifics] CHECK CONSTRAINT [FK_dbo.PlayerRestrictionsSpecifics_dbo.Player_PlayerId]
GO
ALTER TABLE [dbo].[PlayerTargetAudience]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.PlayerTargetAudience_dbo.Player_PlayerId] FOREIGN KEY([PlayerId])
REFERENCES [dbo].[Player] ([Id])
GO
ALTER TABLE [dbo].[PlayerTargetAudience] CHECK CONSTRAINT [FK_dbo.PlayerTargetAudience_dbo.Player_PlayerId]
GO
ALTER TABLE [dbo].[PlayerTargetAudience]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.PlayerTargetAudience_dbo.TargetAudience_TargetAudienceId] FOREIGN KEY([TargetAudienceId])
REFERENCES [dbo].[TargetAudience] ([Id])
GO
ALTER TABLE [dbo].[PlayerTargetAudience] CHECK CONSTRAINT [FK_dbo.PlayerTargetAudience_dbo.TargetAudience_TargetAudienceId]
GO
ALTER TABLE [dbo].[Producer]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Producer_dbo.Address_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([Id])
GO
ALTER TABLE [dbo].[Producer] CHECK CONSTRAINT [FK_dbo.Producer_dbo.Address_AddressId]
GO
ALTER TABLE [dbo].[Producer]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Producer_dbo.ImageFile_ImageId] FOREIGN KEY([ImageId])
REFERENCES [dbo].[ImageFile] ([Id])
GO
ALTER TABLE [dbo].[Producer] CHECK CONSTRAINT [FK_dbo.Producer_dbo.ImageFile_ImageId]
GO
ALTER TABLE [dbo].[ProducerActivity]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProducerActivity_dbo.Activity_ActivityId] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity] ([Id])
GO
ALTER TABLE [dbo].[ProducerActivity] CHECK CONSTRAINT [FK_dbo.ProducerActivity_dbo.Activity_ActivityId]
GO
ALTER TABLE [dbo].[ProducerActivity]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProducerActivity_dbo.Producer_ProducerId] FOREIGN KEY([ProducerId])
REFERENCES [dbo].[Producer] ([Id])
GO
ALTER TABLE [dbo].[ProducerActivity] CHECK CONSTRAINT [FK_dbo.ProducerActivity_dbo.Producer_ProducerId]
GO
ALTER TABLE [dbo].[ProducerDescription]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProducerDescription_dbo.Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[ProducerDescription] CHECK CONSTRAINT [FK_dbo.ProducerDescription_dbo.Language_LanguageId]
GO
ALTER TABLE [dbo].[ProducerDescription]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProducerDescription_dbo.Producer_ProducerId] FOREIGN KEY([ProducerId])
REFERENCES [dbo].[Producer] ([Id])
GO
ALTER TABLE [dbo].[ProducerDescription] CHECK CONSTRAINT [FK_dbo.ProducerDescription_dbo.Producer_ProducerId]
GO
ALTER TABLE [dbo].[ProducerEvent]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProducerEvent_dbo.Event_EventId] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([Id])
GO
ALTER TABLE [dbo].[ProducerEvent] CHECK CONSTRAINT [FK_dbo.ProducerEvent_dbo.Event_EventId]
GO
ALTER TABLE [dbo].[ProducerEvent]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProducerEvent_dbo.Producer_ProducerId] FOREIGN KEY([ProducerId])
REFERENCES [dbo].[Producer] ([Id])
GO
ALTER TABLE [dbo].[ProducerEvent] CHECK CONSTRAINT [FK_dbo.ProducerEvent_dbo.Producer_ProducerId]
GO
ALTER TABLE [dbo].[ProducerTargetAudience]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProducerTargetAudience_dbo.Producer_ProducerId] FOREIGN KEY([ProducerId])
REFERENCES [dbo].[Producer] ([Id])
GO
ALTER TABLE [dbo].[ProducerTargetAudience] CHECK CONSTRAINT [FK_dbo.ProducerTargetAudience_dbo.Producer_ProducerId]
GO
ALTER TABLE [dbo].[ProducerTargetAudience]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProducerTargetAudience_dbo.TargetAudience_TargetAudienceId] FOREIGN KEY([TargetAudienceId])
REFERENCES [dbo].[TargetAudience] ([Id])
GO
ALTER TABLE [dbo].[ProducerTargetAudience] CHECK CONSTRAINT [FK_dbo.ProducerTargetAudience_dbo.TargetAudience_TargetAudienceId]
GO
ALTER TABLE [dbo].[Project]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Project_dbo.Producer_ProducerId] FOREIGN KEY([ProducerId])
REFERENCES [dbo].[Producer] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_dbo.Project_dbo.Producer_ProducerId]
GO
ALTER TABLE [dbo].[ProjectAdditionalInformation]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProjectAdditionalInformation_dbo.Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[ProjectAdditionalInformation] CHECK CONSTRAINT [FK_dbo.ProjectAdditionalInformation_dbo.Language_LanguageId]
GO
ALTER TABLE [dbo].[ProjectAdditionalInformation]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProjectAdditionalInformation_dbo.Project_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectAdditionalInformation] CHECK CONSTRAINT [FK_dbo.ProjectAdditionalInformation_dbo.Project_ProjectId]
GO
ALTER TABLE [dbo].[ProjectInterest]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProjectInterest_dbo.Interest_InterestId] FOREIGN KEY([InterestId])
REFERENCES [dbo].[Interest] ([Id])
GO
ALTER TABLE [dbo].[ProjectInterest] CHECK CONSTRAINT [FK_dbo.ProjectInterest_dbo.Interest_InterestId]
GO
ALTER TABLE [dbo].[ProjectInterest]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProjectInterest_dbo.Project_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectInterest] CHECK CONSTRAINT [FK_dbo.ProjectInterest_dbo.Project_ProjectId]
GO
ALTER TABLE [dbo].[ProjectLinkImage]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProjectLinkImage_dbo.Project_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectLinkImage] CHECK CONSTRAINT [FK_dbo.ProjectLinkImage_dbo.Project_ProjectId]
GO
ALTER TABLE [dbo].[ProjectLinkTeaser]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProjectLinkTeaser_dbo.Project_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectLinkTeaser] CHECK CONSTRAINT [FK_dbo.ProjectLinkTeaser_dbo.Project_ProjectId]
GO
ALTER TABLE [dbo].[ProjectLogLine]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProjectLogLine_dbo.Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[ProjectLogLine] CHECK CONSTRAINT [FK_dbo.ProjectLogLine_dbo.Language_LanguageId]
GO
ALTER TABLE [dbo].[ProjectLogLine]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProjectLogLine_dbo.Project_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectLogLine] CHECK CONSTRAINT [FK_dbo.ProjectLogLine_dbo.Project_ProjectId]
GO
ALTER TABLE [dbo].[ProjectPlayer]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProjectPlayer_dbo.AspNetUsers_SavedUserId] FOREIGN KEY([SavedUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ProjectPlayer] CHECK CONSTRAINT [FK_dbo.ProjectPlayer_dbo.AspNetUsers_SavedUserId]
GO
ALTER TABLE [dbo].[ProjectPlayer]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProjectPlayer_dbo.AspNetUsers_SendingUserId] FOREIGN KEY([SendingUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ProjectPlayer] CHECK CONSTRAINT [FK_dbo.ProjectPlayer_dbo.AspNetUsers_SendingUserId]
GO
ALTER TABLE [dbo].[ProjectPlayer]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProjectPlayer_dbo.Player_PlayerId] FOREIGN KEY([PlayerId])
REFERENCES [dbo].[Player] ([Id])
GO
ALTER TABLE [dbo].[ProjectPlayer] CHECK CONSTRAINT [FK_dbo.ProjectPlayer_dbo.Player_PlayerId]
GO
ALTER TABLE [dbo].[ProjectPlayerEvaluation]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProjectPlayerEvaluation_dbo.AspNetUsers_EvaluationUserId] FOREIGN KEY([EvaluationUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ProjectPlayerEvaluation] CHECK CONSTRAINT [FK_dbo.ProjectPlayerEvaluation_dbo.AspNetUsers_EvaluationUserId]
GO
ALTER TABLE [dbo].[ProjectPlayerEvaluation]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProjectPlayerEvaluation_dbo.ProjectPlayer_Id] FOREIGN KEY([Id])
REFERENCES [dbo].[ProjectPlayer] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectPlayerEvaluation] CHECK CONSTRAINT [FK_dbo.ProjectPlayerEvaluation_dbo.ProjectPlayer_Id]
GO
ALTER TABLE [dbo].[ProjectPlayerEvaluation]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProjectPlayerEvaluation_dbo.ProjectStatus_StatusId] FOREIGN KEY([StatusId])
REFERENCES [dbo].[ProjectStatus] ([Id])
GO
ALTER TABLE [dbo].[ProjectPlayerEvaluation] CHECK CONSTRAINT [FK_dbo.ProjectPlayerEvaluation_dbo.ProjectStatus_StatusId]
GO
ALTER TABLE [dbo].[ProjectProductionPlan]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProjectProductionPlan_dbo.Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[ProjectProductionPlan] CHECK CONSTRAINT [FK_dbo.ProjectProductionPlan_dbo.Language_LanguageId]
GO
ALTER TABLE [dbo].[ProjectProductionPlan]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProjectProductionPlan_dbo.Project_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectProductionPlan] CHECK CONSTRAINT [FK_dbo.ProjectProductionPlan_dbo.Project_ProjectId]
GO
ALTER TABLE [dbo].[ProjectSummary]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProjectSummary_dbo.Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[ProjectSummary] CHECK CONSTRAINT [FK_dbo.ProjectSummary_dbo.Language_LanguageId]
GO
ALTER TABLE [dbo].[ProjectSummary]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProjectSummary_dbo.Project_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectSummary] CHECK CONSTRAINT [FK_dbo.ProjectSummary_dbo.Project_ProjectId]
GO
ALTER TABLE [dbo].[ProjectTitle]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProjectTitle_dbo.Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[ProjectTitle] CHECK CONSTRAINT [FK_dbo.ProjectTitle_dbo.Language_LanguageId]
GO
ALTER TABLE [dbo].[ProjectTitle]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.ProjectTitle_dbo.Project_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectTitle] CHECK CONSTRAINT [FK_dbo.ProjectTitle_dbo.Project_ProjectId]
GO
ALTER TABLE [dbo].[QuizAnswer]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.QuizAnswer_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[QuizAnswer] CHECK CONSTRAINT [FK_dbo.QuizAnswer_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[QuizOption]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.QuizOption_dbo.QuizAnswer_Id] FOREIGN KEY([Id])
REFERENCES [dbo].[QuizAnswer] ([Id])
GO
ALTER TABLE [dbo].[QuizOption] NOCHECK CONSTRAINT [FK_dbo.QuizOption_dbo.QuizAnswer_Id]
GO
ALTER TABLE [dbo].[QuizOption]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.QuizOption_dbo.QuizQuestion_QuestionId] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[QuizQuestion] ([Id])
GO
ALTER TABLE [dbo].[QuizOption] CHECK CONSTRAINT [FK_dbo.QuizOption_dbo.QuizQuestion_QuestionId]
GO
ALTER TABLE [dbo].[QuizQuestion]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.QuizQuestion_dbo.Quiz_QuizId] FOREIGN KEY([QuizId])
REFERENCES [dbo].[Quiz] ([Id])
GO
ALTER TABLE [dbo].[QuizQuestion] CHECK CONSTRAINT [FK_dbo.QuizQuestion_dbo.Quiz_QuizId]
GO
ALTER TABLE [dbo].[RoleLecturerTitle]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.RoleLecturerTitle_dbo.Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[RoleLecturerTitle] CHECK CONSTRAINT [FK_dbo.RoleLecturerTitle_dbo.Language_LanguageId]
GO
ALTER TABLE [dbo].[RoleLecturerTitle]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.RoleLecturerTitle_dbo.RoleLecturer_RoleLecturerId] FOREIGN KEY([RoleLecturerId])
REFERENCES [dbo].[RoleLecturer] ([Id])
GO
ALTER TABLE [dbo].[RoleLecturerTitle] CHECK CONSTRAINT [FK_dbo.RoleLecturerTitle_dbo.RoleLecturer_RoleLecturerId]
GO
ALTER TABLE [dbo].[RoomName]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.RoomName_dbo.Language_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Language] ([Id])
GO
ALTER TABLE [dbo].[RoomName] CHECK CONSTRAINT [FK_dbo.RoomName_dbo.Language_LanguageId]
GO
ALTER TABLE [dbo].[RoomName]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.RoomName_dbo.Room_RoomId] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Room] ([Id])
GO
ALTER TABLE [dbo].[RoomName] CHECK CONSTRAINT [FK_dbo.RoomName_dbo.Room_RoomId]
GO
ALTER TABLE [dbo].[Speaker]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Speaker_dbo.Collaborator_CollaboratorId] FOREIGN KEY([CollaboratorId])
REFERENCES [dbo].[Collaborator] ([Id])
GO
ALTER TABLE [dbo].[Speaker] NOCHECK CONSTRAINT [FK_dbo.Speaker_dbo.Collaborator_CollaboratorId]
GO
ALTER TABLE [dbo].[State]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.State_dbo.Address_Address_Id] FOREIGN KEY([Address_Id])
REFERENCES [dbo].[Address] ([Id])
GO
ALTER TABLE [dbo].[State] CHECK CONSTRAINT [FK_dbo.State_dbo.Address_Address_Id]
GO
ALTER TABLE [dbo].[State]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.State_dbo.Country_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[State] CHECK CONSTRAINT [FK_dbo.State_dbo.Country_CountryId]
GO
ALTER TABLE [dbo].[UserUseTerm]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.UserUseTerm_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
GO
ALTER TABLE [dbo].[UserUseTerm] CHECK CONSTRAINT [FK_dbo.UserUseTerm_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[UserUseTerm]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.UserUseTerm_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[UserUseTerm] CHECK CONSTRAINT [FK_dbo.UserUseTerm_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[UserUseTerm]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.UserUseTerm_dbo.Event_EventId] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([Id])
GO
ALTER TABLE [dbo].[UserUseTerm] CHECK CONSTRAINT [FK_dbo.UserUseTerm_dbo.Event_EventId]
GO
/****** Object:  StoredProcedure [dbo].[GetCryptInfoByCode]    Script Date: 11/07/2019 11:56:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
                                                                                          
                CREATE PROCEDURE [dbo].[GetCryptInfoByCode] 
                (
                    @code int
                )
                AS
                BEGIN
                -- SET NOCOUNT ON added to prevent extra result sets from
                -- interfering with SELECT statements.
                SET NOCOUNT ON;
               OPEN SYMMETRIC KEY [PlataformaRio2CEDSymmetricKey] 
                    DECRYPTION BY CERTIFICATE [PlataformaRio2CCertificate];
                  
                       SELECT top 1
                                    [Id] As 'Id'
                             ,CONVERT(varchar, DecryptByKey([Password])) AS 'Password'
                             ,CONVERT(varchar, DecryptByKey([Salt])) AS 'Salt'
                             ,[PasswordIterations] AS 'PasswordIterations'
                             ,CONVERT(varchar, DecryptByKey([InitialVector])) AS 'InitialVector'
                             ,[KeySize] AS 'KeySize'
                                    ,[Code] As Code
                             FROM [dbo].[AppAesEncryptionInfo] 
                             WHERE [Code] = @code
                 
                       END                  

GO
