SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectTypeId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[HasAdditionalInfo] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_Activities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Activities_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CountryId] [int] NULL,
	[StateId] [int] NULL,
	[CityId] [int] NULL,
	[Address1] [varchar](200) NULL,
	[ZipCode] [varchar](10) NULL,
	[IsManual] [bit] NOT NULL,
	[Latitude] [decimal](9, 6) NULL,
	[Longitude] [decimal](9, 6) NULL,
	[IsGeoLocationUpdated] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendeeCollaborators](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[EditionId] [int] NOT NULL,
	[CollaboratorId] [int] NOT NULL,
	[WelcomeEmailSendDate] [datetimeoffset](7) NULL,
	[OnboardingStartDate] [datetimeoffset](7) NULL,
	[OnboardingFinishDate] [datetimeoffset](7) NULL,
	[OnboardingUserDate] [datetimeoffset](7) NULL,
	[OnboardingCollaboratorDate] [datetimeoffset](7) NULL,
	[OnboardingOrganizationDataSkippedDate] [datetimeoffset](7) NULL,
	[PlayerTermsAcceptanceDate] [datetimeoffset](7) NULL,
	[ProducerTermsAcceptanceDate] [datetimeoffset](7) NULL,
	[SpeakerTermsAcceptanceDate] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_AttendeeCollaborators] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeCollaborators_EditionId_CollaboratorId] UNIQUE NONCLUSTERED 
(
	[EditionId] ASC,
	[CollaboratorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeCollaborators_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendeeCollaboratorTickets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[AttendeeCollaboratorId] [int] NOT NULL,
	[AttendeeSalesPlatformTicketTypeId] [int] NOT NULL,
	[SalesPlatformAttendeeId] [varchar](40) NOT NULL,
	[SalesPlatformUpdateDate] [datetimeoffset](7) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastNames] [varchar](200) NULL,
	[CellPhone] [varchar](50) NULL,
	[JobTitle] [varchar](200) NULL,
	[Barcode] [varchar](40) NULL,
	[IsBarcodePrinted] [bit] NOT NULL,
	[IsBarcodeUsed] [bit] NOT NULL,
	[BarcodeUpdateDate] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_AttendeeCollaboratorTickets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeCollaboratorTickets_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendeeCollaboratorTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[AttendeeCollaboratorId] [int] NOT NULL,
	[CollaboratorTypeId] [int] NOT NULL,
	[IsApiDisplayEnabled] [bit] NOT NULL,
	[ApiHighlightPosition] [int] NULL,
	[TermsAcceptanceDate] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_AttendeeCollaboratorTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeCollaboratorTypes_AttendeeCollaboratorId_CollaboratorTypeId] UNIQUE NONCLUSTERED 
(
	[AttendeeCollaboratorId] ASC,
	[CollaboratorTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeCollaboratorTypes_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendeeInnovationOrganization](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[EditionId] [int] NOT NULL,
	[InnovationOrganizationId] [int] NOT NULL,
	[ProjectEvaluationStatusId] [int] NOT NULL,
	[ProjectEvaluationRefuseReasonId] [int] NULL,
	[Reason] [varchar](1500) NULL,
	[EvaluationUserId] [int] NULL,
	[EvaluationEmailSendDate] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_AttendeeInnovationOrganization] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeInnovationOrganization_EditionId_InnovationOrganizationId] UNIQUE NONCLUSTERED 
(
	[EditionId] ASC,
	[InnovationOrganizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeInnovationOrganization_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendeeInnovationOrganizationCollaborators](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[AttendeeInnovationOrganizationId] [int] NOT NULL,
	[AttendeeCollaboratorId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_AttendeeInnovationOrganizationCollaborators] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeInnovationOrganizationCollaborators_AttendeeInnovationOrganizationId_AttendeeCollaboratorId] UNIQUE NONCLUSTERED 
(
	[AttendeeInnovationOrganizationId] ASC,
	[AttendeeCollaboratorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeInnovationOrganizationCollaborators_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendeeLogisticSponsors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[EditionId] [int] NOT NULL,
	[LogisticSponsorId] [int] NOT NULL,
	[IsOther] [bit] NOT NULL,
	[IsLogisticListDisplayed] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_AttendeeLogisticSponsors_Uid] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendeeMusicBandCollaborators](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[AttendeeMusicBandId] [int] NOT NULL,
	[AttendeeCollaboratorId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_AttendeeMusicBandCollaborators] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeMusicBandCollaborators_AttendeeMusicBandId_AttendeeCollaboratorId] UNIQUE NONCLUSTERED 
(
	[AttendeeMusicBandId] ASC,
	[AttendeeCollaboratorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeMusicBandCollaborators_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendeeMusicBands](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[EditionId] [int] NOT NULL,
	[MusicBandId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_AttendeeMusicBands] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeMusicBands_EditionId_MusicBandId] UNIQUE NONCLUSTERED 
(
	[EditionId] ASC,
	[MusicBandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeMusicBands_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendeeOrganizationCollaborators](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[AttendeeOrganizationId] [int] NOT NULL,
	[AttendeeCollaboratorId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_AttendeeOrganizationCollaborators] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeOrganizationCollaborators_AttendeeOrganizationId_AttendeeCollaboratorId] UNIQUE NONCLUSTERED 
(
	[AttendeeOrganizationId] ASC,
	[AttendeeCollaboratorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeOrganizationCollaborators_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendeeOrganizations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[EditionId] [int] NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[OnboardingStartDate] [datetimeoffset](7) NULL,
	[OnboardingFinishDate] [datetimeoffset](7) NULL,
	[OnboardingOrganizationDate] [datetimeoffset](7) NULL,
	[OnboardingInterestsDate] [datetimeoffset](7) NULL,
	[ProjectSubmissionOrganizationDate] [datetimeoffset](7) NULL,
	[SellProjectsCount] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_AttendeeOrganizations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeOrganizations_EditionId_OrganizationId] UNIQUE NONCLUSTERED 
(
	[EditionId] ASC,
	[OrganizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeOrganizations_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendeeOrganizationTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[AttendeeOrganizationId] [int] NOT NULL,
	[OrganizationTypeId] [int] NOT NULL,
	[IsApiDisplayEnabled] [bit] NOT NULL,
	[ApiHighlightPosition] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_AttendeeOrganizationTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeOrganizationTypes_AttendeeOrganizationId_OrganizationTypeId] UNIQUE NONCLUSTERED 
(
	[AttendeeOrganizationId] ASC,
	[OrganizationTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeOrganizationTypes_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendeePlaces](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[EditionId] [int] NOT NULL,
	[PlaceId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_AttendeePlaces] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeePlaces] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendeeSalesPlatforms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[EditionId] [int] NOT NULL,
	[SalesPlatformId] [int] NOT NULL,
	[SalesPlatformEventId] [varchar](30) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_AttendeeSalesPlatforms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeSalesPlatforms_EditionId_SalesPlatformId] UNIQUE NONCLUSTERED 
(
	[EditionId] ASC,
	[SalesPlatformId] ASC,
	[CreateUserId] ASC,
	[UpdateUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeSalesPlatforms_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendeeSalesPlatformTicketTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[AttendeeSalesPlatformId] [int] NOT NULL,
	[TicketClassId] [varchar](30) NOT NULL,
	[TicketClassName] [varchar](200) NOT NULL,
	[CollaboratorTypeId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_AttendeeSalesPlatformTicketTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeSalesPlatformTicketTypes_AttendeeSalesPlatformId_TicketClassId] UNIQUE NONCLUSTERED 
(
	[AttendeeSalesPlatformId] ASC,
	[TicketClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_AttendeeSalesPlatformTicketTypes_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[StateId] [int] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[IsManual] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Cities_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CollaboratorEditionParticipations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CollaboratorId] [int] NOT NULL,
	[EditionId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_CollaboratorEditionParticipations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_CollaboratorEditionParticipations_CollaboratorId_EditionId] UNIQUE NONCLUSTERED 
(
	[CollaboratorId] ASC,
	[EditionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_CollaboratorEditionParticipations_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CollaboratorGenders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Name] [varchar](300) NOT NULL,
	[HasAdditionalInfo] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_CollaboratorGenders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_CollaboratorGenders_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CollaboratorIndustries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Name] [varchar](300) NOT NULL,
	[HasAdditionalInfo] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_CollaboratorIndustries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_CollaboratorIndustries_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CollaboratorJobTitles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CollaboratorId] [int] NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[LanguageId] [int] NULL,
	[Value] [varchar](256) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_CollaboratorJobTitles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_CollaboratorJobTitles_CollaboratorId_LanguageId] UNIQUE NONCLUSTERED 
(
	[CollaboratorId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_CollaboratorJobTitles_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CollaboratorMiniBios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CollaboratorId] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Value] [varchar](8000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_CollaboratorMiniBios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_CollaboratorMiniBios_CollaboratorId_LanguageId] UNIQUE NONCLUSTERED 
(
	[CollaboratorId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_CollaboratorMiniBios_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CollaboratorRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Name] [varchar](300) NOT NULL,
	[HasAdditionalInfo] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_CollaboratorRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_CollaboratorRoles_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Collaborators](
	[Id] [int] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastNames] [varchar](200) NULL,
	[Document] [varchar](100) NULL,
	[ImageUploadDate] [datetimeoffset](7) NULL,
	[PhoneNumber] [varchar](50) NULL,
	[CellPhone] [varchar](50) NULL,
	[Badge] [varchar](50) NULL,
	[PublicEmail] [varchar](256) NULL,
	[Website] [varchar](300) NULL,
	[Linkedin] [varchar](100) NULL,
	[Instagram] [varchar](100) NULL,
	[Twitter] [varchar](100) NULL,
	[Youtube] [varchar](300) NULL,
	[AddressId] [int] NULL,
	[BirthDate] [datetime] NULL,
	[CollaboratorGenderId] [int] NULL,
	[CollaboratorGenderAdditionalInfo] [varchar](300) NULL,
	[CollaboratorRoleId] [int] NULL,
	[CollaboratorRoleAdditionalInfo] [varchar](300) NULL,
	[CollaboratorIndustryId] [int] NULL,
	[CollaboratorIndustryAdditionalInfo] [varchar](300) NULL,
	[HasAnySpecialNeeds] [bit] NULL,
	[SpecialNeedsDescription] [varchar](300) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_Collaborators] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Collaborators_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CollaboratorTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Name] [varchar](256) NOT NULL,
	[RoleId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_CollaboratorTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_CollaboratorTypes_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConferenceParticipantRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Name] [varchar](256) NOT NULL,
	[IsLecturer] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PKConferenceParticipantRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ConferenceParticipantRoles_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConferenceParticipantRoleTitles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ConferenceParticipantRoleId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Value] [varchar](256) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PKConferenceParticipantRoleTitles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ConferenceParticipantRoleTitles_ConferenceParticipantRoleId_LanguageId] UNIQUE NONCLUSTERED 
(
	[ConferenceParticipantRoleId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ConferenceParticipantRoleTitles_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConferenceParticipants](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ConferenceId] [int] NOT NULL,
	[AttendeeCollaboratorId] [int] NOT NULL,
	[ConferenceParticipantRoleId] [int] NOT NULL,
	[IsPreRegistered] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ConferenceParticipants] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_ConferenceParticipants_ConferenceId_AttendeeCollaboratorId] UNIQUE NONCLUSTERED 
(
	[ConferenceId] ASC,
	[AttendeeCollaboratorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ConferenceParticipants_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConferencePillars](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ConferenceId] [int] NOT NULL,
	[PillarId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ConferencePillars] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ConferencePillars_ConferenceId_PillarId] UNIQUE NONCLUSTERED 
(
	[ConferenceId] ASC,
	[PillarId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ConferencePillars_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConferencePresentationFormats](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ConferenceId] [int] NOT NULL,
	[PresentationFormatId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ConferencePresentationFormats] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ConferencePresentationFormats_ConferenceId_PresentationFormatId] UNIQUE NONCLUSTERED 
(
	[ConferenceId] ASC,
	[PresentationFormatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ConferencePresentationFormats_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Conferences](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[EditionEventId] [int] NOT NULL,
	[RoomId] [int] NOT NULL,
	[StartDate] [datetimeoffset](7) NOT NULL,
	[EndDate] [datetimeoffset](7) NOT NULL,
	[Info] [varchar](3000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_Conferences] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConferenceSynopsis](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ConferenceId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Value] [varchar](8000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ConferenceSynopsis] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ConferenceSynopsis_ConferenceId_LanguageId] UNIQUE NONCLUSTERED 
(
	[ConferenceId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ConferenceSynopsis_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConferenceTitles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ConferenceId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Value] [varchar](8000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ConferenceTitles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ConferenceTitles_ConferenceId_LanguageId] UNIQUE NONCLUSTERED 
(
	[ConferenceId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ConferenceTitles_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConferenceTracks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ConferenceId] [int] NOT NULL,
	[TrackId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ConferenceTracks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ConferenceTracks_ConferenceId_TrackId] UNIQUE NONCLUSTERED 
(
	[ConferenceId] ASC,
	[TrackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ConferenceTracks_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Connections](
	[ConnectionId] [uniqueidentifier] NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
	[UserAgent] [varchar](500) NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_Connections] PRIMARY KEY CLUSTERED 
(
	[ConnectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Connections_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Code] [varchar](3) NULL,
	[IsManual] [bit] NOT NULL,
	[DefaultLanguageId] [int] NOT NULL,
	[CompanyNumberMask] [varchar](50) NULL,
	[ZipCodeMask] [varchar](50) NULL,
	[PhoneNumberMask] [varchar](50) NULL,
	[MobileMask] [varchar](50) NULL,
	[IsCompanyNumberRequired] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Countries_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EditionEvents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[EditionId] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[StartDate] [datetimeoffset](7) NOT NULL,
	[EndDate] [datetimeoffset](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_EditionEvents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_EditionEvents_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Editions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[UrlCode] [int] NOT NULL,
	[IsCurrent] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[StartDate] [datetimeoffset](7) NOT NULL,
	[EndDate] [datetimeoffset](7) NOT NULL,
	[SellStartDate] [datetimeoffset](7) NOT NULL,
	[SellEndDate] [datetimeoffset](7) NOT NULL,
	[ProjectSubmitStartDate] [datetimeoffset](7) NOT NULL,
	[ProjectSubmitEndDate] [datetimeoffset](7) NOT NULL,
	[ProjectEvaluationStartDate] [datetimeoffset](7) NOT NULL,
	[ProjectEvaluationEndDate] [datetimeoffset](7) NOT NULL,
	[OneToOneMeetingsScheduleDate] [datetimeoffset](7) NOT NULL,
	[NegotiationStartDate] [datetimeoffset](7) NOT NULL,
	[NegotiationEndDate] [datetimeoffset](7) NOT NULL,
	[AttendeeOrganizationMaxSellProjectsCount] [int] NOT NULL,
	[ProjectMaxBuyerEvaluationsCount] [int] NOT NULL,
	[MusicProjectSubmitStartDate] [datetimeoffset](7) NOT NULL,
	[MusicProjectSubmitEndDate] [datetimeoffset](7) NOT NULL,
	[MusicProjectEvaluationStartDate] [datetimeoffset](7) NOT NULL,
	[MusicProjectEvaluationEndDate] [datetimeoffset](7) NOT NULL,
	[InnovationProjectSubmitStartDate] [datetimeoffset](7) NOT NULL,
	[InnovationProjectSubmitEndDate] [datetimeoffset](7) NOT NULL,
	[InnovationProjectEvaluationStartDate] [datetimeoffset](7) NOT NULL,
	[InnovationProjectEvaluationEndDate] [datetimeoffset](7) NOT NULL,
	[AudiovisualNegotiationsCreateStartDate] [datetimeoffset](7) NULL,
	[AudiovisualNegotiationsCreateEndDate] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_Editions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Editions_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Editions_UrlCode] UNIQUE NONCLUSTERED 
(
	[UrlCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HoldingDescriptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[HoldingId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Value] [varchar](8000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_HoldingDescriptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_HoldingDescriptions_HoldingId_LanguageId] UNIQUE NONCLUSTERED 
(
	[HoldingId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_HoldingDescriptions_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Holdings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[ImageUploadDate] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_Holdings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Holdings_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InnovationOptionGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_InnovationOptionGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_InnovationOptionGroups_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InnovationOptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[InnovationOptionGroupId] [int] NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Description] [varchar](500) NULL,
	[DisplayOrder] [int] NOT NULL,
	[HasAdditionalInfo] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_InnovationOptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_InnovationOptions_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InnovationOrganizationOptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[InnovationOrganizationId] [int] NOT NULL,
	[InnovationOptionId] [int] NOT NULL,
	[AdditionalInfo] [varchar](200) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_InnovationOrganizationOptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_InnovationOrganizationOptions_InnovationOrganizationId_InnovationOptionId] UNIQUE NONCLUSTERED 
(
	[InnovationOrganizationId] ASC,
	[InnovationOptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_InnovationOrganizationOptions_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InnovationOrganizations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Document] [varchar](50) NULL,
	[ServiceName] [varchar](150) NOT NULL,
	[FoundersNames] [varchar](1000) NOT NULL,
	[FoundationDate] [datetime] NOT NULL,
	[AccumulatedRevenue] [decimal](12, 2) NOT NULL,
	[Description] [varchar](600) NOT NULL,
	[Curriculum] [varchar](600) NOT NULL,
	[WorkDedicationId] [int] NOT NULL,
	[BusinessDefinition] [varchar](300) NULL,
	[Website] [varchar](300) NULL,
	[BusinessFocus] [varchar](300) NULL,
	[MarketSize] [varchar](300) NULL,
	[BusinessEconomicModel] [varchar](300) NULL,
	[BusinessOperationalModel] [varchar](300) NULL,
	[BusinessDifferentials] [varchar](300) NULL,
	[CompetingCompanies] [varchar](300) NULL,
	[BusinessStage] [varchar](300) NULL,
	[PresentationUploadDate] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_InnovationOrganizations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_InnovationOrganizations_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InterestGroups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectTypeId] [int] NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[Type] [varchar](100) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_InterestGroups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_InterestGroups_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Interests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[InterestGroupId] [int] NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[HasAdditionalInfo] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_Interests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Interests_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Code] [varchar](10) NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Languages_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogisticAccommodations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[LogisticId] [int] NOT NULL,
	[AttendeePlaceId] [int] NOT NULL,
	[CheckInDate] [datetimeoffset](7) NOT NULL,
	[CheckOutDate] [datetimeoffset](7) NOT NULL,
	[AdditionalInfo] [varchar](1000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_LogisticAccommodations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_LogisticAccommodations_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogisticAirfares](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[LogisticId] [int] NOT NULL,
	[IsNational] [bit] NOT NULL,
	[IsArrival] [bit] NOT NULL,
	[From] [varchar](100) NOT NULL,
	[DepartureDate] [datetimeoffset](7) NOT NULL,
	[To] [varchar](100) NOT NULL,
	[ArrivalDate] [datetimeoffset](7) NOT NULL,
	[TicketNumber] [varchar](20) NULL,
	[TicketUploadDate] [datetimeoffset](7) NULL,
	[AdditionalInfo] [varchar](1000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_LogisticAirfares] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_LogisticAirfares_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logistics](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[AttendeeCollaboratorId] [int] NOT NULL,
	[IsAirfareSponsored] [bit] NOT NULL,
	[AirfareAttendeeLogisticSponsorId] [int] NULL,
	[IsAccommodationSponsored] [bit] NOT NULL,
	[AccommodationAttendeeLogisticSponsorId] [int] NULL,
	[IsAirportTransferSponsored] [bit] NOT NULL,
	[AirportTransferAttendeeLogisticSponsorId] [int] NULL,
	[IsCityTransferRequired] [bit] NOT NULL,
	[IsVehicleDisposalRequired] [bit] NOT NULL,
	[AdditionalInfo] [varchar](1000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_Logistics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Logistics_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogisticSponsors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[IsAirfareTicketRequired] [bit] NOT NULL,
	[IsOtherRequired] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_LogisticSponsors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_LogisticSponsors_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogisticTransfers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[LogisticId] [int] NOT NULL,
	[FromAttendeePlaceId] [int] NOT NULL,
	[ToAttendeePlaceId] [int] NOT NULL,
	[Date] [datetimeoffset](7) NOT NULL,
	[AdditionalInfo] [varchar](1000) NULL,
	[LogisticTransferStatusId] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_LogisticTransfers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_LogisticTransfers_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogisticTransferStatuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Name] [varchar](300) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_LogisticTransferStatuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_LogisticTransferStatuses_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[EditionId] [int] NOT NULL,
	[SenderId] [int] NOT NULL,
	[RecipientId] [int] NOT NULL,
	[Text] [nvarchar](max) NULL,
	[SendDate] [datetimeoffset](7) NOT NULL,
	[ReadDate] [datetimeoffset](7) NULL,
	[NotificationEmailSendDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Messages_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MusicBandGenres](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[MusicBandId] [int] NOT NULL,
	[MusicGenreId] [int] NOT NULL,
	[AdditionalInfo] [varchar](200) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_MusicBandGenres] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_MusicBandGenres_MusicBandId_MusicGenreId] UNIQUE NONCLUSTERED 
(
	[MusicBandId] ASC,
	[MusicGenreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_MusicBandGenres_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MusicBandMembers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[MusicBandId] [int] NOT NULL,
	[Name] [varchar](300) NOT NULL,
	[MusicInstrumentName] [varchar](100) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_MusicBandMembers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_MusicBandMembers_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MusicBands](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[MusicBandTypeId] [int] NOT NULL,
	[Name] [varchar](300) NOT NULL,
	[ImageUrl] [varchar](300) NULL,
	[FormationDate] [varchar](300) NULL,
	[MainMusicInfluences] [varchar](600) NULL,
	[Facebook] [varchar](300) NULL,
	[Instagram] [varchar](300) NULL,
	[Twitter] [varchar](300) NULL,
	[Youtube] [varchar](300) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_MusicBands] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_MusicBands_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MusicBandTargetAudiences](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[MusicBandId] [int] NOT NULL,
	[TargetAudienceId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_MusicBandTargetAudiences] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_MusicBandTargetAudiences_MusicBandId_TargetAudienceId] UNIQUE NONCLUSTERED 
(
	[MusicBandId] ASC,
	[TargetAudienceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_MusicBandTargetAudiences_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MusicBandTeamMembers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[MusicBandId] [int] NOT NULL,
	[Name] [varchar](300) NOT NULL,
	[Role] [varchar](300) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_MusicBandTeamMembers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_MusicBandTeamMembers_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MusicBandTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_MusicBandTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_MusicBandTypes_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MusicGenres](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[HasAdditionalInfo] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_MusicGenres] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_MusicGenres_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MusicProjects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[AttendeeMusicBandId] [int] NOT NULL,
	[VideoUrl] [varchar](300) NULL,
	[Music1Url] [varchar](300) NULL,
	[Music2Url] [varchar](300) NULL,
	[Release] [varchar](max) NULL,
	[Clipping1] [varchar](300) NULL,
	[Clipping2] [varchar](300) NULL,
	[Clipping3] [varchar](300) NULL,
	[ProjectEvaluationStatusId] [int] NOT NULL,
	[ProjectEvaluationRefuseReasonId] [int] NULL,
	[Reason] [varchar](1500) NULL,
	[EvaluationUserId] [int] NULL,
	[EvaluationDate] [datetimeoffset](7) NULL,
	[EvaluationEmailSendDate] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_MusicProjects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_MusicProjects_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NegotiationConfigs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[EditionId] [int] NOT NULL,
	[StartDate] [datetimeoffset](7) NOT NULL,
	[EndDate] [datetimeoffset](7) NOT NULL,
	[RoundFirstTurn] [int] NOT NULL,
	[RoundSecondTurn] [int] NOT NULL,
	[TimeIntervalBetweenTurn] [time](7) NOT NULL,
	[TimeOfEachRound] [time](7) NOT NULL,
	[TimeIntervalBetweenRound] [time](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_NegotiationConfigs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_NegotiationConfigs_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NegotiationRoomConfigs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[RoomId] [int] NOT NULL,
	[NegotiationConfigId] [int] NOT NULL,
	[CountAutomaticTables] [int] NOT NULL,
	[CountManualTables] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_NegotiationRoomConfigs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_NegotiationRoomConfigs_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Negotiations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectBuyerEvaluationId] [int] NOT NULL,
	[RoomId] [int] NOT NULL,
	[StartDate] [datetimeoffset](7) NOT NULL,
	[EndDate] [datetimeoffset](7) NOT NULL,
	[TableNumber] [int] NOT NULL,
	[RoundNumber] [int] NOT NULL,
	[IsAutomatic] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_Negotiations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Negotiations_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrganizationActivities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[ActivityId] [int] NOT NULL,
	[AdditionalInfo] [nvarchar](200) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateuserId] [int] NOT NULL,
 CONSTRAINT [PK_OrganizationActivities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_OrganizationActivities_OrganizationId_ActivityId] UNIQUE NONCLUSTERED 
(
	[OrganizationId] ASC,
	[ActivityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_OrganizationActivities_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrganizationDescriptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Value] [varchar](8000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_OrganizationDescriptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_OrganizationDescriptions_OrganizationId_LanguageId] UNIQUE NONCLUSTERED 
(
	[OrganizationId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_OrganizationDescriptions_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrganizationInterests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[InterestId] [int] NOT NULL,
	[AdditionalInfo] [varchar](200) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_OrganizationInterests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_OrganizationInterests_OrganizationId_InterestId] UNIQUE NONCLUSTERED 
(
	[OrganizationId] ASC,
	[InterestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrganizationRestrictionSpecifics](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Value] [varchar](8000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_OrganizationRestrictionSpecifics] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_UQ_OrganizationRestrictionSpecifics_OrganizationId_LanguageId] UNIQUE NONCLUSTERED 
(
	[OrganizationId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_UQ_OrganizationRestrictionSpecifics_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organizations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[HoldingId] [int] NULL,
	[Name] [varchar](100) NOT NULL,
	[CompanyName] [varchar](100) NULL,
	[TradeName] [varchar](100) NULL,
	[Document] [varchar](50) NULL,
	[PhoneNumber] [varchar](50) NULL,
	[Website] [varchar](300) NULL,
	[Linkedin] [varchar](100) NULL,
	[Instagram] [varchar](100) NULL,
	[Twitter] [varchar](100) NULL,
	[Youtube] [varchar](300) NULL,
	[SocialMedia] [varchar](256) NULL,
	[AddressId] [int] NULL,
	[ImageUploadDate] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_Organizations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Organizations_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrganizationTargetAudiences](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[TargetAudienceId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_OrganizationTargetAudiences] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_OrganizationTargetAudiences_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrganizationTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NULL,
	[Name] [varchar](50) NOT NULL,
	[RelatedProjectTypeId] [int] NOT NULL,
	[IsSeller] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_OrganizationTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_OrganizationTypes_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pillars](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[EditionId] [int] NOT NULL,
	[Name] [varchar](600) NOT NULL,
	[Color] [varchar](10) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_Pillars] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Pillars_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Places](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[IsHotel] [bit] NOT NULL,
	[IsAirport] [bit] NOT NULL,
	[AddressId] [int] NULL,
	[Website] [varchar](300) NULL,
	[AdditionalInfo] [varchar](1000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_Places] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Places_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PresentationFormats](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[EditionId] [int] NOT NULL,
	[Name] [varchar](600) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_PresentationFormats] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_PresentationFormats_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectAdditionalInformations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Value] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectAdditionalInformations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectAdditionalInformations_ProjectId_LanguageId] UNIQUE NONCLUSTERED 
(
	[ProjectId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectAdditionalInformations_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectBuyerEvaluations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[BuyerAttendeeOrganizationId] [int] NOT NULL,
	[ProjectEvaluationStatusId] [int] NOT NULL,
	[ProjectEvaluationRefuseReasonId] [int] NULL,
	[Reason] [varchar](1500) NULL,
	[SellerUserId] [int] NOT NULL,
	[BuyerEvaluationUserId] [int] NULL,
	[EvaluationDate] [datetimeoffset](7) NULL,
	[BuyerEmailSendDate] [datetimeoffset](7) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectBuyerEvaluations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectBuyerEvaluations_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectEvaluationRefuseReasons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectTypeId] [int] NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[HasAdditionalInfo] [bit] NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectEvaluationRefuseReasons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectEvaluationRefuseReasons_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectEvaluationStatuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NULL,
	[Name] [varchar](50) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[IsEvaluated] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectEvaluationStatuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectEvaluationStatuses_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectImageLinks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Value] [varchar](3000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectImageLinks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectInterests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[InterestId] [int] NOT NULL,
	[AdditionalInfo] [varchar](200) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectInterests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectInterests_ProjectId_InterestId] UNIQUE NONCLUSTERED 
(
	[ProjectId] ASC,
	[InterestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectInterests_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectLogLines](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Value] [varchar](8000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectLogLines] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectLogLines_ProjectId_LanguageId] UNIQUE NONCLUSTERED 
(
	[ProjectId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectLogLines_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectProductionPlans](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Value] [varchar](3000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectProductionPlans] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectProductionPlans_ProjectId_LanguageId] UNIQUE NONCLUSTERED 
(
	[ProjectId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectProductionPlans_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Projects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectTypeId] [int] NOT NULL,
	[SellerAttendeeOrganizationId] [int] NOT NULL,
	[TotalPlayingTime] [varchar](10) NOT NULL,
	[NumberOfEpisodes] [int] NULL,
	[EachEpisodePlayingTime] [varchar](10) NULL,
	[ValuePerEpisode] [varchar](50) NULL,
	[TotalValueOfProject] [varchar](50) NULL,
	[ValueAlreadyRaised] [varchar](50) NULL,
	[ValueStillNeeded] [varchar](50) NULL,
	[IsPitching] [bit] NOT NULL,
	[FinishDate] [datetimeoffset](7) NULL,
	[ProjectBuyerEvaluationsCount] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_Projects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Projects_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectSummaries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Value] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectSummaries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectSummaries_ProjectId_LanguageId] UNIQUE NONCLUSTERED 
(
	[ProjectId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectSummaries_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectTargetAudiences](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[TargetAudienceId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectTargetAudiences] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectTargetAudiences_ProjectId_TargetAudienceId] UNIQUE NONCLUSTERED 
(
	[ProjectId] ASC,
	[TargetAudienceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectTargetAudiences_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectTeaserLinks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[Value] [varchar](3000) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectTeaserLinks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectTeaserLinks_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectTitles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Value] [varchar](256) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectTitles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectTitles_ProjectId_LanguageId] UNIQUE NONCLUSTERED 
(
	[ProjectId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ProjectTitles_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ProjectTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuizAnswers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[QuizOptionId] [int] NOT NULL,
	[Answer] [varbinary](200) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_QuizAnswers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_QuizAnswers_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuizOptions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[QuizQuestionId] [int] NOT NULL,
	[HasText] [bit] NOT NULL,
	[Option] [varchar](200) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_QuizOptions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_QuizOptions_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuizQuestions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[QuizId] [int] NOT NULL,
	[Question] [varchar](200) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_QuizQuestions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_QuizQuestions_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quizzes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[EditionId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_Quizzes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Quizzes_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReleasedMusicProjects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[MusicBandId] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Year] [varchar](300) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_ReleasedMusicProjects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_ReleasedMusicProjects_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](256) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Roles_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomNames](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[RoomId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Value] [varchar](256) NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_RoomNames] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_RoomNames_RoomId_Languageid] UNIQUE NONCLUSTERED 
(
	[RoomId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_RoomNames_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NULL,
	[EditionId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Rooms_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesPlatforms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[WebhookSecurityKey] [uniqueidentifier] NOT NULL,
	[ApiKey] [varchar](200) NULL,
	[ApiSecret] [varchar](200) NULL,
	[MaxProcessingCount] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
	[SecurityStamp] [varchar](36) NOT NULL,
 CONSTRAINT [PK_SalesPlatforms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_SalesPlatforms_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesPlatformWebhookRequests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[SalesPlatformId] [int] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[Endpoint] [varchar](250) NOT NULL,
	[Header] [varchar](1000) NULL,
	[Payload] [varchar](max) NULL,
	[IpAddress] [varchar](38) NOT NULL,
	[IsProcessed] [bit] NOT NULL,
	[IsProcessing] [bit] NOT NULL,
	[ProcessingCount] [int] NOT NULL,
	[LastProcessingDate] [datetimeoffset](7) NULL,
	[NextProcessingDate] [datetimeoffset](7) NULL,
	[ProcessingErrorCode] [varchar](10) NULL,
	[ProcessingErrorMessage] [varchar](250) NULL,
	[ManualProcessingUserId] [int] NULL,
	[SecurityStamp] [varchar](36) NOT NULL,
 CONSTRAINT [PK_SalesPlatformWebhookRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_SalesPlatformWebhookRequests_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SentEmails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[RecipientUserId] [int] NOT NULL,
	[EditionId] [int] NULL,
	[EmailType] [varchar](50) NOT NULL,
	[EmailSendDate] [datetimeoffset](7) NOT NULL,
	[EmailReadDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_SentEmails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[States](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[CountryId] [int] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Code] [varchar](2) NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsManual] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_States_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubscribeLists](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Description] [varchar](2000) NULL,
	[Code] [varchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_SubscribeLists] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_SubscribeLists_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemParameters](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Code] [int] NOT NULL,
	[SubCode] [varchar](150) NULL,
	[LanguageCode] [int] NOT NULL,
	[GroupCode] [int] NOT NULL,
	[TypeName] [varchar](150) NULL,
	[Description] [varchar](256) NULL,
	[Value] [varchar](1000) NULL,
	[DateChanges] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_SystemParameters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_SystemParameters_Code] UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_SystemParameters_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TargetAudiences](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[ProjectTypeId] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_TargetAudiences] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_TargetAudiences_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tracks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[EditionId] [int] NOT NULL,
	[Name] [varchar](600) NOT NULL,
	[Color] [varchar](10) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_Tracks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Tracks_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[ClaimType] [varchar](8000) NULL,
	[ClaimValue] [varchar](8000) NULL,
 CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogins](
	[LoginProvider] [varchar](128) NOT NULL,
	[ProviderKey] [varchar](128) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Active] [bit] NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[UserName] [varchar](256) NOT NULL,
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
	[PasswordNew] [varchar](50) NULL,
	[UserInterfaceLanguageId] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_Users_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_UsersRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserUnsubscribedLists](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
	[SubscribeListId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_UserUnsubscribedLists] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_UserUnsubscribedLists_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_UserUnsubscribedLists_UserId_SubscribeListId] UNIQUE NONCLUSTERED 
(
	[UserId] ASC,
	[SubscribeListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkDedications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreateDate] [datetimeoffset](7) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[UpdateDate] [datetimeoffset](7) NOT NULL,
	[UpdateUserId] [int] NOT NULL,
 CONSTRAINT [PK_WorkDedications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IDX_UQ_WorkDedications_Uid] UNIQUE NONCLUSTERED 
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_Activities_ProjectTYpeId] ON [dbo].[Activities]
(
	[ProjectTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_Addresses_ZipCode] ON [dbo].[Addresses]
(
	[ZipCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_AttendeeCollaboratorTickets_AttendeeCollaboratorId_AttendeeSalesPlatformTicketTypeId] ON [dbo].[AttendeeCollaboratorTickets]
(
	[AttendeeCollaboratorId] ASC,
	[AttendeeSalesPlatformTicketTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_AttendeeCollaboratorTypes_IsApiDisplayEnabled] ON [dbo].[AttendeeCollaboratorTypes]
(
	[IsApiDisplayEnabled] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_AttendeeInnovationOrganization_EvaluationuserId] ON [dbo].[AttendeeInnovationOrganization]
(
	[EvaluationUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_AttendeeInnovationOrganization_InnovationOrganizationId] ON [dbo].[AttendeeInnovationOrganization]
(
	[InnovationOrganizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_AttendeeInnovationOrganization_ProjectEvaluationStatusId] ON [dbo].[AttendeeInnovationOrganization]
(
	[ProjectEvaluationRefuseReasonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_AttendeeInnovationOrganizationCollaborators_AttendeeCollaboratorId] ON [dbo].[AttendeeInnovationOrganizationCollaborators]
(
	[AttendeeCollaboratorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_AttendeeLogisticSponsors_EditionId_IsDeleted] ON [dbo].[AttendeeLogisticSponsors]
(
	[EditionId] ASC,
	[IsDeleted] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_AttendeeLogisticSponsors_LogisticSponsorId_IsDeleted] ON [dbo].[AttendeeLogisticSponsors]
(
	[LogisticSponsorId] ASC,
	[IsDeleted] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_AttendeeMusicBandCollaborators_AttendeeCollaboratorId] ON [dbo].[AttendeeMusicBandCollaborators]
(
	[AttendeeCollaboratorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_AttendeeMusicBands_MusicBandId] ON [dbo].[AttendeeMusicBands]
(
	[MusicBandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_AttendeeOrganizationTypes_IsApiDisplayEnabled] ON [dbo].[AttendeeOrganizationTypes]
(
	[IsApiDisplayEnabled] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_AttendeePlaces_EditionId_IsDeleted] ON [dbo].[AttendeePlaces]
(
	[EditionId] ASC,
	[IsDeleted] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_AttendeePlaces_PlaceId_IsDeleted] ON [dbo].[AttendeePlaces]
(
	[PlaceId] ASC,
	[IsDeleted] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_Cities_StateId_Name] ON [dbo].[Cities]
(
	[StateId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_CollaboratorGenders_Name] ON [dbo].[CollaboratorGenders]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_CollaboratorIndustries_Name] ON [dbo].[CollaboratorIndustries]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_CollaboratorRoles_Name] ON [dbo].[CollaboratorRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_Collaborators_FirstName_LastNames] ON [dbo].[Collaborators]
(
	[FirstName] ASC,
	[LastNames] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_ConferenceParticipantRoles_IsLecturer] ON [dbo].[ConferenceParticipantRoles]
(
	[IsLecturer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_ConferencePillars_PillarId] ON [dbo].[ConferencePillars]
(
	[PillarId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_ConferencePresentationFormats_PresentationFormatId] ON [dbo].[ConferencePresentationFormats]
(
	[PresentationFormatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_Conferences_EditionEventId] ON [dbo].[Conferences]
(
	[EditionEventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_Conferences_RoomId] ON [dbo].[Conferences]
(
	[RoomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_ConferenceTracks_TrackId] ON [dbo].[ConferenceTracks]
(
	[TrackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_Connections_UserId] ON [dbo].[Connections]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_EditionEvents_EditionId] ON [dbo].[EditionEvents]
(
	[EditionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_InnovationOptionGroups_Name] ON [dbo].[InnovationOptionGroups]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_InnovationOptions_InnovationOptionGroupId] ON [dbo].[InnovationOptions]
(
	[InnovationOptionGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_InnovationOptions_Name] ON [dbo].[InnovationOptions]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_InnovationOrganizationOptions_InnovationOptionId] ON [dbo].[InnovationOrganizationOptions]
(
	[InnovationOptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_InnovationOrganizations_Document] ON [dbo].[InnovationOrganizations]
(
	[Document] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_InnovationOrganizations_Name] ON [dbo].[InnovationOrganizations]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_InterestGroups_ProjectTypeId] ON [dbo].[InterestGroups]
(
	[ProjectTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_LogisticAccommodations_LogisticId] ON [dbo].[LogisticAccommodations]
(
	[LogisticId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_LogisticAirfares_LogisticId] ON [dbo].[LogisticAirfares]
(
	[LogisticId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_Logistics_AttendeeCollaboratorId] ON [dbo].[Logistics]
(
	[AttendeeCollaboratorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_LogisticTransfers_LogisticId] ON [dbo].[LogisticTransfers]
(
	[LogisticId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_LogisticTransferStatuses_Name] ON [dbo].[LogisticTransferStatuses]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_Messages_NotificationEmailSendDate] ON [dbo].[Messages]
(
	[NotificationEmailSendDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_Messages_ReadDate] ON [dbo].[Messages]
(
	[ReadDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_Messages_RecipientId] ON [dbo].[Messages]
(
	[RecipientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_Messages_SenderId] ON [dbo].[Messages]
(
	[SenderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_MusicBandGenres_MusicGenreId] ON [dbo].[MusicBandGenres]
(
	[MusicGenreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_MusicBandMembers_MusicBandId] ON [dbo].[MusicBandMembers]
(
	[MusicBandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_MusicBands_MusicBandTypeId] ON [dbo].[MusicBands]
(
	[MusicBandTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_MusicBands_Name] ON [dbo].[MusicBands]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_MusicBandTargetAudiences_TargetAudienceId] ON [dbo].[MusicBandTargetAudiences]
(
	[TargetAudienceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_MusicBandTeamMembers_MusicBandId] ON [dbo].[MusicBandTeamMembers]
(
	[MusicBandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_MusicBandTypes_Name] ON [dbo].[MusicBandTypes]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_MusicGenres_Name] ON [dbo].[MusicGenres]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_MusicProjects_AttendeeMusicBandId] ON [dbo].[MusicProjects]
(
	[AttendeeMusicBandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_MusicProjects_EvaluationUserId] ON [dbo].[MusicProjects]
(
	[EvaluationUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_MusicProjects_ProjectEvaluationStatusId] ON [dbo].[MusicProjects]
(
	[ProjectEvaluationStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_NegotiationConfigs_EditionId] ON [dbo].[NegotiationConfigs]
(
	[EditionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_Organizations_Document] ON [dbo].[Organizations]
(
	[Document] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_Organizations_Name] ON [dbo].[Organizations]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_OrganizationTypes_RelatedProjectTypeId] ON [dbo].[OrganizationTypes]
(
	[RelatedProjectTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_Pillars_EditionId] ON [dbo].[Pillars]
(
	[EditionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_Pillars_Name] ON [dbo].[Pillars]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_PresentationFormats_EditionId] ON [dbo].[PresentationFormats]
(
	[EditionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_PresentationFormats_Name] ON [dbo].[PresentationFormats]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_ProjectBuyerEvaluations_BuyerAttendeeOrganizationId] ON [dbo].[ProjectBuyerEvaluations]
(
	[BuyerAttendeeOrganizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_ProjectBuyerEvaluations_ProjectId] ON [dbo].[ProjectBuyerEvaluations]
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_ProjectImageLinks_ProjectId] ON [dbo].[ProjectImageLinks]
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_Projects_SellerAttendeeOrganizationId] ON [dbo].[Projects]
(
	[SellerAttendeeOrganizationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_ProjectTeaserLinks_ProjectId] ON [dbo].[ProjectTeaserLinks]
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_UQ_ProjectTypes_Uid] ON [dbo].[ProjectTypes]
(
	[Uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_QuizAnswers_QuizOptionId] ON [dbo].[QuizAnswers]
(
	[QuizOptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_QuizOptions_QuizQuestionId] ON [dbo].[QuizOptions]
(
	[QuizQuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_QuizQuestions_QuizId] ON [dbo].[QuizQuestions]
(
	[QuizId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_Quizzes_EditionId] ON [dbo].[Quizzes]
(
	[EditionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_UQ_Quizzes_IsActive] ON [dbo].[Quizzes]
(
	[IsActive] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_ReleasedMusicProjects_MusicBandId] ON [dbo].[ReleasedMusicProjects]
(
	[MusicBandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_Rooms_EditionId] ON [dbo].[Rooms]
(
	[EditionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_SalesPlatforms_Name] ON [dbo].[SalesPlatforms]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_SalesPlatformWebhookRequests_IsProcessed_IsProcessing_CreateDate] ON [dbo].[SalesPlatformWebhookRequests]
(
	[IsProcessed] ASC,
	[IsProcessing] ASC,
	[CreateDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_States_CountryId_Name] ON [dbo].[States]
(
	[CountryId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_SubscribeLists_Code] ON [dbo].[SubscribeLists]
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_SubscribeLists_IsDeleted] ON [dbo].[SubscribeLists]
(
	[IsDeleted] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_TargetAudiences_ProjectTypeId] ON [dbo].[TargetAudiences]
(
	[ProjectTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_Tracks_EditionId] ON [dbo].[Tracks]
(
	[EditionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_Tracks_Name] ON [dbo].[Tracks]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_Users_IsDeleted] ON [dbo].[Users]
(
	[IsDeleted] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_Users_UserName] ON [dbo].[Users]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IDX_UserUnsubscribedLists_SubscribeListId] ON [dbo].[UserUnsubscribedLists]
(
	[SubscribeListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_WorkDedications_Name] ON [dbo].[WorkDedications]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Activities]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTypes_Activities_ProjectTypeId] FOREIGN KEY([ProjectTypeId])
REFERENCES [dbo].[ProjectTypes] ([Id])
GO
ALTER TABLE [dbo].[Activities] CHECK CONSTRAINT [FK_ProjectTypes_Activities_ProjectTypeId]
GO
ALTER TABLE [dbo].[Activities]  WITH CHECK ADD  CONSTRAINT [FK_Users_Activities_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Activities] CHECK CONSTRAINT [FK_Users_Activities_CreateUserId]
GO
ALTER TABLE [dbo].[Activities]  WITH CHECK ADD  CONSTRAINT [FK_Users_Activities_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Activities] CHECK CONSTRAINT [FK_Users_Activities_UpdateUserId]
GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Cities_Addresses_CityId] FOREIGN KEY([CityId])
REFERENCES [dbo].[Cities] ([Id])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Cities_Addresses_CityId]
GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Countries_Addresses_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Countries_Addresses_CountryId]
GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_States_Addresses_StateId] FOREIGN KEY([StateId])
REFERENCES [dbo].[States] ([Id])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_States_Addresses_StateId]
GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Users_Addresses_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Users_Addresses_CreateUserId]
GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Users_Addresses_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Users_Addresses_UpdateUserId]
GO
ALTER TABLE [dbo].[AttendeeCollaborators]  WITH CHECK ADD  CONSTRAINT [FK_Collaborators_AttendeeCollaborators_CollaboratorId] FOREIGN KEY([CollaboratorId])
REFERENCES [dbo].[Collaborators] ([Id])
GO
ALTER TABLE [dbo].[AttendeeCollaborators] CHECK CONSTRAINT [FK_Collaborators_AttendeeCollaborators_CollaboratorId]
GO
ALTER TABLE [dbo].[AttendeeCollaborators]  WITH CHECK ADD  CONSTRAINT [FK_Editions_AttendeeCollaborators_EditionId] FOREIGN KEY([EditionId])
REFERENCES [dbo].[Editions] ([Id])
GO
ALTER TABLE [dbo].[AttendeeCollaborators] CHECK CONSTRAINT [FK_Editions_AttendeeCollaborators_EditionId]
GO
ALTER TABLE [dbo].[AttendeeCollaborators]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeCollaborators_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeCollaborators] CHECK CONSTRAINT [FK_Users_AttendeeCollaborators_CreateUserId]
GO
ALTER TABLE [dbo].[AttendeeCollaborators]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeCollaborators_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeCollaborators] CHECK CONSTRAINT [FK_Users_AttendeeCollaborators_UpdateUserId]
GO
ALTER TABLE [dbo].[AttendeeCollaboratorTickets]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeCollaborators_AttendeeCollaboratorTickets_AttendeeCollaboratorId] FOREIGN KEY([AttendeeCollaboratorId])
REFERENCES [dbo].[AttendeeCollaborators] ([Id])
GO
ALTER TABLE [dbo].[AttendeeCollaboratorTickets] CHECK CONSTRAINT [FK_AttendeeCollaborators_AttendeeCollaboratorTickets_AttendeeCollaboratorId]
GO
ALTER TABLE [dbo].[AttendeeCollaboratorTickets]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeSalesPlatformTicketTypes_AttendeeCollaboratorTickets_AttendeeSalesPlatformTicketTypeId] FOREIGN KEY([AttendeeSalesPlatformTicketTypeId])
REFERENCES [dbo].[AttendeeSalesPlatformTicketTypes] ([Id])
GO
ALTER TABLE [dbo].[AttendeeCollaboratorTickets] CHECK CONSTRAINT [FK_AttendeeSalesPlatformTicketTypes_AttendeeCollaboratorTickets_AttendeeSalesPlatformTicketTypeId]
GO
ALTER TABLE [dbo].[AttendeeCollaboratorTickets]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeCollaboratorTickets_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeCollaboratorTickets] CHECK CONSTRAINT [FK_Users_AttendeeCollaboratorTickets_CreateUserId]
GO
ALTER TABLE [dbo].[AttendeeCollaboratorTickets]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeCollaboratorTickets_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeCollaboratorTickets] CHECK CONSTRAINT [FK_Users_AttendeeCollaboratorTickets_UpdateUserId]
GO
ALTER TABLE [dbo].[AttendeeCollaboratorTypes]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeCollaborators_AttendeeCollaboratorTypes_AttendeeCollaboratorId] FOREIGN KEY([AttendeeCollaboratorId])
REFERENCES [dbo].[AttendeeCollaborators] ([Id])
GO
ALTER TABLE [dbo].[AttendeeCollaboratorTypes] CHECK CONSTRAINT [FK_AttendeeCollaborators_AttendeeCollaboratorTypes_AttendeeCollaboratorId]
GO
ALTER TABLE [dbo].[AttendeeCollaboratorTypes]  WITH CHECK ADD  CONSTRAINT [FK_CollaboratorTypes_AttendeeCollaboratorTypes_CollaboratorTypeId] FOREIGN KEY([CollaboratorTypeId])
REFERENCES [dbo].[CollaboratorTypes] ([Id])
GO
ALTER TABLE [dbo].[AttendeeCollaboratorTypes] CHECK CONSTRAINT [FK_CollaboratorTypes_AttendeeCollaboratorTypes_CollaboratorTypeId]
GO
ALTER TABLE [dbo].[AttendeeCollaboratorTypes]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeCollaboratorTypes_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeCollaboratorTypes] CHECK CONSTRAINT [FK_Users_AttendeeCollaboratorTypes_CreateUserId]
GO
ALTER TABLE [dbo].[AttendeeCollaboratorTypes]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeCollaboratorTypes_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeCollaboratorTypes] CHECK CONSTRAINT [FK_Users_AttendeeCollaboratorTypes_UpdateUserId]
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganization]  WITH CHECK ADD  CONSTRAINT [FK_Editions_AttendeeInnovationOrganization_EditionId] FOREIGN KEY([EditionId])
REFERENCES [dbo].[Editions] ([Id])
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganization] CHECK CONSTRAINT [FK_Editions_AttendeeInnovationOrganization_EditionId]
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganization]  WITH CHECK ADD  CONSTRAINT [FK_InnovationOrganizations_AttendeeInnovationOrganization_InnovationOrganizationId] FOREIGN KEY([InnovationOrganizationId])
REFERENCES [dbo].[InnovationOrganizations] ([Id])
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganization] CHECK CONSTRAINT [FK_InnovationOrganizations_AttendeeInnovationOrganization_InnovationOrganizationId]
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganization]  WITH CHECK ADD  CONSTRAINT [FK_ProjectEvaluationRefuseReasons_AttendeeInnovationOrganization_ProjectEvaluationRefuseReasonId] FOREIGN KEY([ProjectEvaluationRefuseReasonId])
REFERENCES [dbo].[ProjectEvaluationRefuseReasons] ([Id])
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganization] CHECK CONSTRAINT [FK_ProjectEvaluationRefuseReasons_AttendeeInnovationOrganization_ProjectEvaluationRefuseReasonId]
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganization]  WITH CHECK ADD  CONSTRAINT [FK_ProjectEvaluationStatuses_AttendeeInnovationOrganization_ProjectEvaluationStatusId] FOREIGN KEY([ProjectEvaluationStatusId])
REFERENCES [dbo].[ProjectEvaluationStatuses] ([Id])
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganization] CHECK CONSTRAINT [FK_ProjectEvaluationStatuses_AttendeeInnovationOrganization_ProjectEvaluationStatusId]
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganization]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeInnovationOrganization_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganization] CHECK CONSTRAINT [FK_Users_AttendeeInnovationOrganization_CreateUserId]
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganization]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeInnovationOrganization_EvaluationUserId] FOREIGN KEY([EvaluationUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganization] CHECK CONSTRAINT [FK_Users_AttendeeInnovationOrganization_EvaluationUserId]
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganization]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeInnovationOrganization_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganization] CHECK CONSTRAINT [FK_Users_AttendeeInnovationOrganization_UpdateUserId]
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganizationCollaborators]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeCollaborators_AttendeeInnovationOrganizationCollaborators_AttendeeCollaboratorId] FOREIGN KEY([AttendeeCollaboratorId])
REFERENCES [dbo].[AttendeeCollaborators] ([Id])
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganizationCollaborators] CHECK CONSTRAINT [FK_AttendeeCollaborators_AttendeeInnovationOrganizationCollaborators_AttendeeCollaboratorId]
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganizationCollaborators]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeInnovationOrganization_AttendeeInnovationOrganizationCollaborators_AttendeeInnovationOrganizationId] FOREIGN KEY([AttendeeInnovationOrganizationId])
REFERENCES [dbo].[AttendeeInnovationOrganization] ([Id])
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganizationCollaborators] CHECK CONSTRAINT [FK_AttendeeInnovationOrganization_AttendeeInnovationOrganizationCollaborators_AttendeeInnovationOrganizationId]
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganizationCollaborators]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeInnovationOrganizationCollaborators_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganizationCollaborators] CHECK CONSTRAINT [FK_Users_AttendeeInnovationOrganizationCollaborators_CreateUserId]
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganizationCollaborators]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeInnovationOrganizationCollaborators_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeInnovationOrganizationCollaborators] CHECK CONSTRAINT [FK_Users_AttendeeInnovationOrganizationCollaborators_UpdateUserId]
GO
ALTER TABLE [dbo].[AttendeeLogisticSponsors]  WITH CHECK ADD  CONSTRAINT [FK_Editions_AttendeeLogisticSponsors_EditionId] FOREIGN KEY([EditionId])
REFERENCES [dbo].[Editions] ([Id])
GO
ALTER TABLE [dbo].[AttendeeLogisticSponsors] CHECK CONSTRAINT [FK_Editions_AttendeeLogisticSponsors_EditionId]
GO
ALTER TABLE [dbo].[AttendeeLogisticSponsors]  WITH CHECK ADD  CONSTRAINT [FK_LogisticSponsors_AttendeeLogisticSponsors_LogisticSponsorId] FOREIGN KEY([LogisticSponsorId])
REFERENCES [dbo].[LogisticSponsors] ([Id])
GO
ALTER TABLE [dbo].[AttendeeLogisticSponsors] CHECK CONSTRAINT [FK_LogisticSponsors_AttendeeLogisticSponsors_LogisticSponsorId]
GO
ALTER TABLE [dbo].[AttendeeLogisticSponsors]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeLogisticSponsors_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeLogisticSponsors] CHECK CONSTRAINT [FK_Users_AttendeeLogisticSponsors_CreateUserId]
GO
ALTER TABLE [dbo].[AttendeeLogisticSponsors]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeLogisticSponsors_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeLogisticSponsors] CHECK CONSTRAINT [FK_Users_AttendeeLogisticSponsors_UpdateUserId]
GO
ALTER TABLE [dbo].[AttendeeMusicBandCollaborators]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeCollaborators_AttendeeMusicBandCollaborators_AttendeeCollaboratorId] FOREIGN KEY([AttendeeCollaboratorId])
REFERENCES [dbo].[AttendeeCollaborators] ([Id])
GO
ALTER TABLE [dbo].[AttendeeMusicBandCollaborators] CHECK CONSTRAINT [FK_AttendeeCollaborators_AttendeeMusicBandCollaborators_AttendeeCollaboratorId]
GO
ALTER TABLE [dbo].[AttendeeMusicBandCollaborators]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeMusicBands_AttendeeMusicBandCollaborators_AttendeeMusicBandId] FOREIGN KEY([AttendeeMusicBandId])
REFERENCES [dbo].[AttendeeMusicBands] ([Id])
GO
ALTER TABLE [dbo].[AttendeeMusicBandCollaborators] CHECK CONSTRAINT [FK_AttendeeMusicBands_AttendeeMusicBandCollaborators_AttendeeMusicBandId]
GO
ALTER TABLE [dbo].[AttendeeMusicBandCollaborators]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeMusicBandCollaborators_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeMusicBandCollaborators] CHECK CONSTRAINT [FK_Users_AttendeeMusicBandCollaborators_CreateUserId]
GO
ALTER TABLE [dbo].[AttendeeMusicBandCollaborators]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeMusicBandCollaborators_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeMusicBandCollaborators] CHECK CONSTRAINT [FK_Users_AttendeeMusicBandCollaborators_UpdateUserId]
GO
ALTER TABLE [dbo].[AttendeeMusicBands]  WITH CHECK ADD  CONSTRAINT [FK_Editions_AttendeeMusicBands_EditionId] FOREIGN KEY([EditionId])
REFERENCES [dbo].[Editions] ([Id])
GO
ALTER TABLE [dbo].[AttendeeMusicBands] CHECK CONSTRAINT [FK_Editions_AttendeeMusicBands_EditionId]
GO
ALTER TABLE [dbo].[AttendeeMusicBands]  WITH CHECK ADD  CONSTRAINT [FK_MusicBands_AttendeeMusicBands_MusicBandId] FOREIGN KEY([MusicBandId])
REFERENCES [dbo].[MusicBands] ([Id])
GO
ALTER TABLE [dbo].[AttendeeMusicBands] CHECK CONSTRAINT [FK_MusicBands_AttendeeMusicBands_MusicBandId]
GO
ALTER TABLE [dbo].[AttendeeMusicBands]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeMusicBands_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeMusicBands] CHECK CONSTRAINT [FK_Users_AttendeeMusicBands_CreateUserId]
GO
ALTER TABLE [dbo].[AttendeeMusicBands]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeMusicBands_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeMusicBands] CHECK CONSTRAINT [FK_Users_AttendeeMusicBands_UpdateUserId]
GO
ALTER TABLE [dbo].[AttendeeOrganizationCollaborators]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeCollaborators_AttendeeOrganizationCollaborators_AttendeeCollaboratorId] FOREIGN KEY([AttendeeCollaboratorId])
REFERENCES [dbo].[AttendeeCollaborators] ([Id])
GO
ALTER TABLE [dbo].[AttendeeOrganizationCollaborators] CHECK CONSTRAINT [FK_AttendeeCollaborators_AttendeeOrganizationCollaborators_AttendeeCollaboratorId]
GO
ALTER TABLE [dbo].[AttendeeOrganizationCollaborators]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeOrganizations_AttendeeOrganizationCollaborators_AttendeeOrganizationId] FOREIGN KEY([AttendeeOrganizationId])
REFERENCES [dbo].[AttendeeOrganizations] ([Id])
GO
ALTER TABLE [dbo].[AttendeeOrganizationCollaborators] CHECK CONSTRAINT [FK_AttendeeOrganizations_AttendeeOrganizationCollaborators_AttendeeOrganizationId]
GO
ALTER TABLE [dbo].[AttendeeOrganizationCollaborators]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeOrganizationCollaborators_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeOrganizationCollaborators] CHECK CONSTRAINT [FK_Users_AttendeeOrganizationCollaborators_CreateUserId]
GO
ALTER TABLE [dbo].[AttendeeOrganizationCollaborators]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeOrganizationCollaborators_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeOrganizationCollaborators] CHECK CONSTRAINT [FK_Users_AttendeeOrganizationCollaborators_UpdateUserId]
GO
ALTER TABLE [dbo].[AttendeeOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_Editions_AttendeeOrganizations_EditionId] FOREIGN KEY([EditionId])
REFERENCES [dbo].[Editions] ([Id])
GO
ALTER TABLE [dbo].[AttendeeOrganizations] CHECK CONSTRAINT [FK_Editions_AttendeeOrganizations_EditionId]
GO
ALTER TABLE [dbo].[AttendeeOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_Organizations_AttendeeOrganizations_OrganizationId] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organizations] ([Id])
GO
ALTER TABLE [dbo].[AttendeeOrganizations] CHECK CONSTRAINT [FK_Organizations_AttendeeOrganizations_OrganizationId]
GO
ALTER TABLE [dbo].[AttendeeOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeOrganizations_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeOrganizations] CHECK CONSTRAINT [FK_Users_AttendeeOrganizations_CreateUserId]
GO
ALTER TABLE [dbo].[AttendeeOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeOrganizations_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeOrganizations] CHECK CONSTRAINT [FK_Users_AttendeeOrganizations_UpdateUserId]
GO
ALTER TABLE [dbo].[AttendeeOrganizationTypes]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeOrganizations_AttendeeOrganizationTypes_AttendeeOrganizationId] FOREIGN KEY([AttendeeOrganizationId])
REFERENCES [dbo].[AttendeeOrganizations] ([Id])
GO
ALTER TABLE [dbo].[AttendeeOrganizationTypes] CHECK CONSTRAINT [FK_AttendeeOrganizations_AttendeeOrganizationTypes_AttendeeOrganizationId]
GO
ALTER TABLE [dbo].[AttendeeOrganizationTypes]  WITH CHECK ADD  CONSTRAINT [FK_OrganizationTypes_AttendeeOrganizationTypes_OrganizationTypeId] FOREIGN KEY([OrganizationTypeId])
REFERENCES [dbo].[OrganizationTypes] ([Id])
GO
ALTER TABLE [dbo].[AttendeeOrganizationTypes] CHECK CONSTRAINT [FK_OrganizationTypes_AttendeeOrganizationTypes_OrganizationTypeId]
GO
ALTER TABLE [dbo].[AttendeeOrganizationTypes]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeOrganizationTypes_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeOrganizationTypes] CHECK CONSTRAINT [FK_Users_AttendeeOrganizationTypes_CreateUserId]
GO
ALTER TABLE [dbo].[AttendeeOrganizationTypes]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeOrganizationTypes_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeOrganizationTypes] CHECK CONSTRAINT [FK_Users_AttendeeOrganizationTypes_UpdateUserId]
GO
ALTER TABLE [dbo].[AttendeePlaces]  WITH CHECK ADD  CONSTRAINT [FK_Editions_AttendeePlaces_EditionId] FOREIGN KEY([EditionId])
REFERENCES [dbo].[Editions] ([Id])
GO
ALTER TABLE [dbo].[AttendeePlaces] CHECK CONSTRAINT [FK_Editions_AttendeePlaces_EditionId]
GO
ALTER TABLE [dbo].[AttendeePlaces]  WITH CHECK ADD  CONSTRAINT [FK_Places_AttendeePlaces_PlaceId] FOREIGN KEY([PlaceId])
REFERENCES [dbo].[Places] ([Id])
GO
ALTER TABLE [dbo].[AttendeePlaces] CHECK CONSTRAINT [FK_Places_AttendeePlaces_PlaceId]
GO
ALTER TABLE [dbo].[AttendeePlaces]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeePlaces_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeePlaces] CHECK CONSTRAINT [FK_Users_AttendeePlaces_CreateUserId]
GO
ALTER TABLE [dbo].[AttendeePlaces]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeePlaces_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeePlaces] CHECK CONSTRAINT [FK_Users_AttendeePlaces_UpdateUserId]
GO
ALTER TABLE [dbo].[AttendeeSalesPlatforms]  WITH CHECK ADD  CONSTRAINT [FK_Editions_AttendeeSalesPlatforms_EditionId] FOREIGN KEY([EditionId])
REFERENCES [dbo].[Editions] ([Id])
GO
ALTER TABLE [dbo].[AttendeeSalesPlatforms] CHECK CONSTRAINT [FK_Editions_AttendeeSalesPlatforms_EditionId]
GO
ALTER TABLE [dbo].[AttendeeSalesPlatforms]  WITH CHECK ADD  CONSTRAINT [FK_SalesPlatforms_AttendeeSalesPlatforms_SalesPlatformId] FOREIGN KEY([SalesPlatformId])
REFERENCES [dbo].[SalesPlatforms] ([Id])
GO
ALTER TABLE [dbo].[AttendeeSalesPlatforms] CHECK CONSTRAINT [FK_SalesPlatforms_AttendeeSalesPlatforms_SalesPlatformId]
GO
ALTER TABLE [dbo].[AttendeeSalesPlatforms]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeSalesPlatforms_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeSalesPlatforms] CHECK CONSTRAINT [FK_Users_AttendeeSalesPlatforms_CreateUserId]
GO
ALTER TABLE [dbo].[AttendeeSalesPlatforms]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeSalesPlatforms_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeSalesPlatforms] CHECK CONSTRAINT [FK_Users_AttendeeSalesPlatforms_UpdateUserId]
GO
ALTER TABLE [dbo].[AttendeeSalesPlatformTicketTypes]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeSalesPlatforms_AttendeeSalesPlatformTicketTypes_AttendeeSalesPlatformId] FOREIGN KEY([AttendeeSalesPlatformId])
REFERENCES [dbo].[AttendeeSalesPlatforms] ([Id])
GO
ALTER TABLE [dbo].[AttendeeSalesPlatformTicketTypes] CHECK CONSTRAINT [FK_AttendeeSalesPlatforms_AttendeeSalesPlatformTicketTypes_AttendeeSalesPlatformId]
GO
ALTER TABLE [dbo].[AttendeeSalesPlatformTicketTypes]  WITH CHECK ADD  CONSTRAINT [FK_CollaboratorTypes_AttendeeSalesPlatformTicketTypes_CollaboratorTypeId] FOREIGN KEY([CollaboratorTypeId])
REFERENCES [dbo].[CollaboratorTypes] ([Id])
GO
ALTER TABLE [dbo].[AttendeeSalesPlatformTicketTypes] CHECK CONSTRAINT [FK_CollaboratorTypes_AttendeeSalesPlatformTicketTypes_CollaboratorTypeId]
GO
ALTER TABLE [dbo].[AttendeeSalesPlatformTicketTypes]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeSalesPlatformTicketTypes_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeSalesPlatformTicketTypes] CHECK CONSTRAINT [FK_Users_AttendeeSalesPlatformTicketTypes_CreateUserId]
GO
ALTER TABLE [dbo].[AttendeeSalesPlatformTicketTypes]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeSalesPlatformTicketTypes_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[AttendeeSalesPlatformTicketTypes] CHECK CONSTRAINT [FK_Users_AttendeeSalesPlatformTicketTypes_UpdateUserId]
GO
ALTER TABLE [dbo].[Cities]  WITH CHECK ADD  CONSTRAINT [FK_States_Cities_StateId] FOREIGN KEY([StateId])
REFERENCES [dbo].[States] ([Id])
GO
ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_States_Cities_StateId]
GO
ALTER TABLE [dbo].[Cities]  WITH CHECK ADD  CONSTRAINT [FK_Users_Cities_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_Users_Cities_CreateUserId]
GO
ALTER TABLE [dbo].[Cities]  WITH CHECK ADD  CONSTRAINT [FK_Users_Cities_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_Users_Cities_UpdateUserId]
GO
ALTER TABLE [dbo].[CollaboratorEditionParticipations]  WITH CHECK ADD  CONSTRAINT [FK_Collaborators_CollaboratorEditionParticipations_CollaboratorId] FOREIGN KEY([CollaboratorId])
REFERENCES [dbo].[Collaborators] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorEditionParticipations] CHECK CONSTRAINT [FK_Collaborators_CollaboratorEditionParticipations_CollaboratorId]
GO
ALTER TABLE [dbo].[CollaboratorEditionParticipations]  WITH CHECK ADD  CONSTRAINT [FK_Editions_CollaboratorEditionParticipations_EditionId] FOREIGN KEY([EditionId])
REFERENCES [dbo].[Editions] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorEditionParticipations] CHECK CONSTRAINT [FK_Editions_CollaboratorEditionParticipations_EditionId]
GO
ALTER TABLE [dbo].[CollaboratorEditionParticipations]  WITH CHECK ADD  CONSTRAINT [FK_Users_CollaboratorEditionParticipations_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorEditionParticipations] CHECK CONSTRAINT [FK_Users_CollaboratorEditionParticipations_CreateUserId]
GO
ALTER TABLE [dbo].[CollaboratorEditionParticipations]  WITH CHECK ADD  CONSTRAINT [FK_Users_CollaboratorEditionParticipations_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorEditionParticipations] CHECK CONSTRAINT [FK_Users_CollaboratorEditionParticipations_UpdateUserId]
GO
ALTER TABLE [dbo].[CollaboratorGenders]  WITH CHECK ADD  CONSTRAINT [FK_Users_CollaboratorGenders_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorGenders] CHECK CONSTRAINT [FK_Users_CollaboratorGenders_CreateUserId]
GO
ALTER TABLE [dbo].[CollaboratorGenders]  WITH CHECK ADD  CONSTRAINT [FK_Users_CollaboratorGenders_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorGenders] CHECK CONSTRAINT [FK_Users_CollaboratorGenders_UpdateUserId]
GO
ALTER TABLE [dbo].[CollaboratorIndustries]  WITH CHECK ADD  CONSTRAINT [FK_Users_CollaboratorIndustries_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorIndustries] CHECK CONSTRAINT [FK_Users_CollaboratorIndustries_CreateUserId]
GO
ALTER TABLE [dbo].[CollaboratorIndustries]  WITH CHECK ADD  CONSTRAINT [FK_Users_CollaboratorIndustries_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorIndustries] CHECK CONSTRAINT [FK_Users_CollaboratorIndustries_UpdateUserId]
GO
ALTER TABLE [dbo].[CollaboratorJobTitles]  WITH CHECK ADD  CONSTRAINT [FK_Collaborators_CollaboratorJobTitles_CollaboratorId] FOREIGN KEY([CollaboratorId])
REFERENCES [dbo].[Collaborators] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorJobTitles] CHECK CONSTRAINT [FK_Collaborators_CollaboratorJobTitles_CollaboratorId]
GO
ALTER TABLE [dbo].[CollaboratorJobTitles]  WITH CHECK ADD  CONSTRAINT [FK_Languages_CollaboratorJobTitles_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorJobTitles] CHECK CONSTRAINT [FK_Languages_CollaboratorJobTitles_LanguageId]
GO
ALTER TABLE [dbo].[CollaboratorJobTitles]  WITH CHECK ADD  CONSTRAINT [FK_Users_CollaboratorJobTitles_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorJobTitles] CHECK CONSTRAINT [FK_Users_CollaboratorJobTitles_CreateUserId]
GO
ALTER TABLE [dbo].[CollaboratorJobTitles]  WITH CHECK ADD  CONSTRAINT [FK_Users_CollaboratorJobTitles_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorJobTitles] CHECK CONSTRAINT [FK_Users_CollaboratorJobTitles_UpdateUserId]
GO
ALTER TABLE [dbo].[CollaboratorMiniBios]  WITH CHECK ADD  CONSTRAINT [FK_Collaborators_CollaboratorMiniBios_CollaboratorId] FOREIGN KEY([CollaboratorId])
REFERENCES [dbo].[Collaborators] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorMiniBios] CHECK CONSTRAINT [FK_Collaborators_CollaboratorMiniBios_CollaboratorId]
GO
ALTER TABLE [dbo].[CollaboratorMiniBios]  WITH CHECK ADD  CONSTRAINT [FK_Languages_CollaboratorMiniBios_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorMiniBios] CHECK CONSTRAINT [FK_Languages_CollaboratorMiniBios_LanguageId]
GO
ALTER TABLE [dbo].[CollaboratorMiniBios]  WITH CHECK ADD  CONSTRAINT [FK_Users_CollaboratorMiniBios_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorMiniBios] CHECK CONSTRAINT [FK_Users_CollaboratorMiniBios_CreateUserId]
GO
ALTER TABLE [dbo].[CollaboratorMiniBios]  WITH CHECK ADD  CONSTRAINT [FK_Users_CollaboratorMiniBios_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorMiniBios] CHECK CONSTRAINT [FK_Users_CollaboratorMiniBios_UpdateUserId]
GO
ALTER TABLE [dbo].[CollaboratorRoles]  WITH CHECK ADD  CONSTRAINT [FK_Users_CollaboratorRoles_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorRoles] CHECK CONSTRAINT [FK_Users_CollaboratorRoles_CreateUserId]
GO
ALTER TABLE [dbo].[CollaboratorRoles]  WITH CHECK ADD  CONSTRAINT [FK_Users_CollaboratorRoles_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorRoles] CHECK CONSTRAINT [FK_Users_CollaboratorRoles_UpdateUserId]
GO
ALTER TABLE [dbo].[Collaborators]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Collaborators_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([Id])
GO
ALTER TABLE [dbo].[Collaborators] CHECK CONSTRAINT [FK_Addresses_Collaborators_AddressId]
GO
ALTER TABLE [dbo].[Collaborators]  WITH CHECK ADD  CONSTRAINT [FK_CollaboratorGenders_Collaborators_CollaboratorGenderId] FOREIGN KEY([CollaboratorGenderId])
REFERENCES [dbo].[CollaboratorGenders] ([Id])
GO
ALTER TABLE [dbo].[Collaborators] CHECK CONSTRAINT [FK_CollaboratorGenders_Collaborators_CollaboratorGenderId]
GO
ALTER TABLE [dbo].[Collaborators]  WITH CHECK ADD  CONSTRAINT [FK_CollaboratorIndustries_Collaborators_CollaboratorIndustryId] FOREIGN KEY([CollaboratorIndustryId])
REFERENCES [dbo].[CollaboratorIndustries] ([Id])
GO
ALTER TABLE [dbo].[Collaborators] CHECK CONSTRAINT [FK_CollaboratorIndustries_Collaborators_CollaboratorIndustryId]
GO
ALTER TABLE [dbo].[Collaborators]  WITH CHECK ADD  CONSTRAINT [FK_CollaboratorRoles_Collaborators_CollaboratorRoleId] FOREIGN KEY([CollaboratorRoleId])
REFERENCES [dbo].[CollaboratorRoles] ([Id])
GO
ALTER TABLE [dbo].[Collaborators] CHECK CONSTRAINT [FK_CollaboratorRoles_Collaborators_CollaboratorRoleId]
GO
ALTER TABLE [dbo].[Collaborators]  WITH CHECK ADD  CONSTRAINT [FK_Users_Collaborators_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Collaborators] CHECK CONSTRAINT [FK_Users_Collaborators_CreateUserId]
GO
ALTER TABLE [dbo].[Collaborators]  WITH CHECK ADD  CONSTRAINT [FK_Users_Collaborators_Id] FOREIGN KEY([Id])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Collaborators] CHECK CONSTRAINT [FK_Users_Collaborators_Id]
GO
ALTER TABLE [dbo].[Collaborators]  WITH CHECK ADD  CONSTRAINT [FK_Users_Collaborators_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Collaborators] CHECK CONSTRAINT [FK_Users_Collaborators_UpdateUserId]
GO
ALTER TABLE [dbo].[CollaboratorTypes]  WITH CHECK ADD  CONSTRAINT [FK_Roles_CollaboratorTypes_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorTypes] CHECK CONSTRAINT [FK_Roles_CollaboratorTypes_RoleId]
GO
ALTER TABLE [dbo].[CollaboratorTypes]  WITH CHECK ADD  CONSTRAINT [FK_Users_CollaboratorTypes_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorTypes] CHECK CONSTRAINT [FK_Users_CollaboratorTypes_CreateUserId]
GO
ALTER TABLE [dbo].[CollaboratorTypes]  WITH CHECK ADD  CONSTRAINT [FK_Users_CollaboratorTypes_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[CollaboratorTypes] CHECK CONSTRAINT [FK_Users_CollaboratorTypes_UpdateUserId]
GO
ALTER TABLE [dbo].[ConferenceParticipantRoles]  WITH CHECK ADD  CONSTRAINT [FK_Users_ConferenceParticipantRoles_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ConferenceParticipantRoles] CHECK CONSTRAINT [FK_Users_ConferenceParticipantRoles_CreateUserId]
GO
ALTER TABLE [dbo].[ConferenceParticipantRoles]  WITH CHECK ADD  CONSTRAINT [FK_Users_ConferenceParticipantRoles_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ConferenceParticipantRoles] CHECK CONSTRAINT [FK_Users_ConferenceParticipantRoles_UpdateUserId]
GO
ALTER TABLE [dbo].[ConferenceParticipantRoleTitles]  WITH CHECK ADD  CONSTRAINT [FK_ConferenceParticipantRoles_ConferenceParticipantRoleTitles_ConferenceParticipantRoleId] FOREIGN KEY([ConferenceParticipantRoleId])
REFERENCES [dbo].[ConferenceParticipantRoles] ([Id])
GO
ALTER TABLE [dbo].[ConferenceParticipantRoleTitles] CHECK CONSTRAINT [FK_ConferenceParticipantRoles_ConferenceParticipantRoleTitles_ConferenceParticipantRoleId]
GO
ALTER TABLE [dbo].[ConferenceParticipantRoleTitles]  WITH CHECK ADD  CONSTRAINT [FK_Languages_ConferenceParticipantRoleTitles_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[ConferenceParticipantRoleTitles] CHECK CONSTRAINT [FK_Languages_ConferenceParticipantRoleTitles_LanguageId]
GO
ALTER TABLE [dbo].[ConferenceParticipantRoleTitles]  WITH CHECK ADD  CONSTRAINT [FK_Users_ConferenceParticipantRoleTitles_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ConferenceParticipantRoleTitles] CHECK CONSTRAINT [FK_Users_ConferenceParticipantRoleTitles_CreateUserId]
GO
ALTER TABLE [dbo].[ConferenceParticipantRoleTitles]  WITH CHECK ADD  CONSTRAINT [FK_Users_ConferenceParticipantRoleTitles_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ConferenceParticipantRoleTitles] CHECK CONSTRAINT [FK_Users_ConferenceParticipantRoleTitles_UpdateUserId]
GO
ALTER TABLE [dbo].[ConferenceParticipants]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeCollaborators_ConferenceParticipants_AttendeeCollaboratorId] FOREIGN KEY([AttendeeCollaboratorId])
REFERENCES [dbo].[AttendeeCollaborators] ([Id])
GO
ALTER TABLE [dbo].[ConferenceParticipants] CHECK CONSTRAINT [FK_AttendeeCollaborators_ConferenceParticipants_AttendeeCollaboratorId]
GO
ALTER TABLE [dbo].[ConferenceParticipants]  WITH CHECK ADD  CONSTRAINT [FK_ConferenceParticipantRoles_ConferenceParticipants_ConferenceParticipantRoleId] FOREIGN KEY([ConferenceParticipantRoleId])
REFERENCES [dbo].[ConferenceParticipantRoles] ([Id])
GO
ALTER TABLE [dbo].[ConferenceParticipants] CHECK CONSTRAINT [FK_ConferenceParticipantRoles_ConferenceParticipants_ConferenceParticipantRoleId]
GO
ALTER TABLE [dbo].[ConferenceParticipants]  WITH CHECK ADD  CONSTRAINT [FK_Conferences_ConferenceParticipants_ConferenceId] FOREIGN KEY([ConferenceId])
REFERENCES [dbo].[Conferences] ([Id])
GO
ALTER TABLE [dbo].[ConferenceParticipants] CHECK CONSTRAINT [FK_Conferences_ConferenceParticipants_ConferenceId]
GO
ALTER TABLE [dbo].[ConferenceParticipants]  WITH CHECK ADD  CONSTRAINT [FK_Users_ConferenceParticipants_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ConferenceParticipants] CHECK CONSTRAINT [FK_Users_ConferenceParticipants_CreateUserId]
GO
ALTER TABLE [dbo].[ConferenceParticipants]  WITH CHECK ADD  CONSTRAINT [FK_Users_ConferenceParticipants_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ConferenceParticipants] CHECK CONSTRAINT [FK_Users_ConferenceParticipants_UpdateUserId]
GO
ALTER TABLE [dbo].[ConferencePillars]  WITH CHECK ADD  CONSTRAINT [FK_Conferences_ConferencePillars_ConferenceId] FOREIGN KEY([ConferenceId])
REFERENCES [dbo].[Conferences] ([Id])
GO
ALTER TABLE [dbo].[ConferencePillars] CHECK CONSTRAINT [FK_Conferences_ConferencePillars_ConferenceId]
GO
ALTER TABLE [dbo].[ConferencePillars]  WITH CHECK ADD  CONSTRAINT [FK_Pillars_ConferencePillars_PillarId] FOREIGN KEY([PillarId])
REFERENCES [dbo].[Pillars] ([Id])
GO
ALTER TABLE [dbo].[ConferencePillars] CHECK CONSTRAINT [FK_Pillars_ConferencePillars_PillarId]
GO
ALTER TABLE [dbo].[ConferencePillars]  WITH CHECK ADD  CONSTRAINT [FK_Users_ConferencePillars_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ConferencePillars] CHECK CONSTRAINT [FK_Users_ConferencePillars_CreateUserId]
GO
ALTER TABLE [dbo].[ConferencePillars]  WITH CHECK ADD  CONSTRAINT [FK_Users_ConferencePillars_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ConferencePillars] CHECK CONSTRAINT [FK_Users_ConferencePillars_UpdateUserId]
GO
ALTER TABLE [dbo].[ConferencePresentationFormats]  WITH CHECK ADD  CONSTRAINT [FK_Conferences_ConferencePresentationFormats_ConferenceId] FOREIGN KEY([ConferenceId])
REFERENCES [dbo].[Conferences] ([Id])
GO
ALTER TABLE [dbo].[ConferencePresentationFormats] CHECK CONSTRAINT [FK_Conferences_ConferencePresentationFormats_ConferenceId]
GO
ALTER TABLE [dbo].[ConferencePresentationFormats]  WITH CHECK ADD  CONSTRAINT [FK_PresentationFormats_ConferencePresentationFormats_PresentationFormatId] FOREIGN KEY([PresentationFormatId])
REFERENCES [dbo].[PresentationFormats] ([Id])
GO
ALTER TABLE [dbo].[ConferencePresentationFormats] CHECK CONSTRAINT [FK_PresentationFormats_ConferencePresentationFormats_PresentationFormatId]
GO
ALTER TABLE [dbo].[ConferencePresentationFormats]  WITH CHECK ADD  CONSTRAINT [FK_Users_ConferencePresentationFormats_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ConferencePresentationFormats] CHECK CONSTRAINT [FK_Users_ConferencePresentationFormats_CreateUserId]
GO
ALTER TABLE [dbo].[ConferencePresentationFormats]  WITH CHECK ADD  CONSTRAINT [FK_Users_ConferencePresentationFormats_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ConferencePresentationFormats] CHECK CONSTRAINT [FK_Users_ConferencePresentationFormats_UpdateUserId]
GO
ALTER TABLE [dbo].[Conferences]  WITH CHECK ADD  CONSTRAINT [FK_EditionEvents_Conferences_EditionEventId] FOREIGN KEY([EditionEventId])
REFERENCES [dbo].[EditionEvents] ([Id])
GO
ALTER TABLE [dbo].[Conferences] CHECK CONSTRAINT [FK_EditionEvents_Conferences_EditionEventId]
GO
ALTER TABLE [dbo].[Conferences]  WITH CHECK ADD  CONSTRAINT [FK_Rooms_Conferences_RoomId] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([Id])
GO
ALTER TABLE [dbo].[Conferences] CHECK CONSTRAINT [FK_Rooms_Conferences_RoomId]
GO
ALTER TABLE [dbo].[Conferences]  WITH CHECK ADD  CONSTRAINT [FK_Users_Conferences_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Conferences] CHECK CONSTRAINT [FK_Users_Conferences_CreateUserId]
GO
ALTER TABLE [dbo].[Conferences]  WITH CHECK ADD  CONSTRAINT [FK_Users_Conferences_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Conferences] CHECK CONSTRAINT [FK_Users_Conferences_UpdateUserId]
GO
ALTER TABLE [dbo].[ConferenceSynopsis]  WITH CHECK ADD  CONSTRAINT [FK_Conferences_ConferenceSynopsis_ConferenceId] FOREIGN KEY([ConferenceId])
REFERENCES [dbo].[Conferences] ([Id])
GO
ALTER TABLE [dbo].[ConferenceSynopsis] CHECK CONSTRAINT [FK_Conferences_ConferenceSynopsis_ConferenceId]
GO
ALTER TABLE [dbo].[ConferenceSynopsis]  WITH CHECK ADD  CONSTRAINT [FK_Languages_ConferenceSynopsis_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[ConferenceSynopsis] CHECK CONSTRAINT [FK_Languages_ConferenceSynopsis_LanguageId]
GO
ALTER TABLE [dbo].[ConferenceSynopsis]  WITH CHECK ADD  CONSTRAINT [FK_Users_ConferenceSynopsis_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ConferenceSynopsis] CHECK CONSTRAINT [FK_Users_ConferenceSynopsis_CreateUserId]
GO
ALTER TABLE [dbo].[ConferenceSynopsis]  WITH CHECK ADD  CONSTRAINT [FK_Users_ConferenceSynopsis_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ConferenceSynopsis] CHECK CONSTRAINT [FK_Users_ConferenceSynopsis_UpdateUserId]
GO
ALTER TABLE [dbo].[ConferenceTitles]  WITH CHECK ADD  CONSTRAINT [FK_Conferences_ConferenceTitles_ConferenceId] FOREIGN KEY([ConferenceId])
REFERENCES [dbo].[Conferences] ([Id])
GO
ALTER TABLE [dbo].[ConferenceTitles] CHECK CONSTRAINT [FK_Conferences_ConferenceTitles_ConferenceId]
GO
ALTER TABLE [dbo].[ConferenceTitles]  WITH CHECK ADD  CONSTRAINT [FK_Languages_ConferenceTitles_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[ConferenceTitles] CHECK CONSTRAINT [FK_Languages_ConferenceTitles_LanguageId]
GO
ALTER TABLE [dbo].[ConferenceTitles]  WITH CHECK ADD  CONSTRAINT [FK_Users_ConferenceTitles_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ConferenceTitles] CHECK CONSTRAINT [FK_Users_ConferenceTitles_CreateUserId]
GO
ALTER TABLE [dbo].[ConferenceTitles]  WITH CHECK ADD  CONSTRAINT [FK_Users_ConferenceTitles_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ConferenceTitles] CHECK CONSTRAINT [FK_Users_ConferenceTitles_UpdateUserId]
GO
ALTER TABLE [dbo].[ConferenceTracks]  WITH CHECK ADD  CONSTRAINT [FK_Conferences_ConferenceTracks_ConferenceId] FOREIGN KEY([ConferenceId])
REFERENCES [dbo].[Conferences] ([Id])
GO
ALTER TABLE [dbo].[ConferenceTracks] CHECK CONSTRAINT [FK_Conferences_ConferenceTracks_ConferenceId]
GO
ALTER TABLE [dbo].[ConferenceTracks]  WITH CHECK ADD  CONSTRAINT [FK_Tracks_ConferenceTracks_TrackId] FOREIGN KEY([TrackId])
REFERENCES [dbo].[Tracks] ([Id])
GO
ALTER TABLE [dbo].[ConferenceTracks] CHECK CONSTRAINT [FK_Tracks_ConferenceTracks_TrackId]
GO
ALTER TABLE [dbo].[ConferenceTracks]  WITH CHECK ADD  CONSTRAINT [FK_Users_ConferenceTracks_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ConferenceTracks] CHECK CONSTRAINT [FK_Users_ConferenceTracks_CreateUserId]
GO
ALTER TABLE [dbo].[ConferenceTracks]  WITH CHECK ADD  CONSTRAINT [FK_Users_ConferenceTracks_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ConferenceTracks] CHECK CONSTRAINT [FK_Users_ConferenceTracks_UpdateUserId]
GO
ALTER TABLE [dbo].[Connections]  WITH CHECK ADD  CONSTRAINT [FK_Users_Connections_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Connections] CHECK CONSTRAINT [FK_Users_Connections_UserId]
GO
ALTER TABLE [dbo].[Countries]  WITH CHECK ADD  CONSTRAINT [FK_Languages_Countries_DefaultLanguageId] FOREIGN KEY([DefaultLanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[Countries] CHECK CONSTRAINT [FK_Languages_Countries_DefaultLanguageId]
GO
ALTER TABLE [dbo].[Countries]  WITH CHECK ADD  CONSTRAINT [FK_Users_Countries_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Countries] CHECK CONSTRAINT [FK_Users_Countries_CreateUserId]
GO
ALTER TABLE [dbo].[Countries]  WITH CHECK ADD  CONSTRAINT [FK_Users_Countries_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Countries] CHECK CONSTRAINT [FK_Users_Countries_UpdateUserId]
GO
ALTER TABLE [dbo].[EditionEvents]  WITH CHECK ADD  CONSTRAINT [FK_Languages_EditionEvents_EditionId] FOREIGN KEY([EditionId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[EditionEvents] CHECK CONSTRAINT [FK_Languages_EditionEvents_EditionId]
GO
ALTER TABLE [dbo].[EditionEvents]  WITH CHECK ADD  CONSTRAINT [FK_Users_EditionEvents_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[EditionEvents] CHECK CONSTRAINT [FK_Users_EditionEvents_CreateUserId]
GO
ALTER TABLE [dbo].[EditionEvents]  WITH CHECK ADD  CONSTRAINT [FK_Users_EditionEvents_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[EditionEvents] CHECK CONSTRAINT [FK_Users_EditionEvents_UpdateUserId]
GO
ALTER TABLE [dbo].[Editions]  WITH CHECK ADD  CONSTRAINT [FK_Users_Editions_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Editions] CHECK CONSTRAINT [FK_Users_Editions_CreateUserId]
GO
ALTER TABLE [dbo].[Editions]  WITH CHECK ADD  CONSTRAINT [FK_Users_Editions_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Editions] CHECK CONSTRAINT [FK_Users_Editions_UpdateUserId]
GO
ALTER TABLE [dbo].[HoldingDescriptions]  WITH CHECK ADD  CONSTRAINT [FK_Holdings_HoldingDescriptions_HoldingId] FOREIGN KEY([HoldingId])
REFERENCES [dbo].[Holdings] ([Id])
GO
ALTER TABLE [dbo].[HoldingDescriptions] CHECK CONSTRAINT [FK_Holdings_HoldingDescriptions_HoldingId]
GO
ALTER TABLE [dbo].[HoldingDescriptions]  WITH CHECK ADD  CONSTRAINT [FK_Languages_HoldingDescriptions_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[HoldingDescriptions] CHECK CONSTRAINT [FK_Languages_HoldingDescriptions_LanguageId]
GO
ALTER TABLE [dbo].[HoldingDescriptions]  WITH CHECK ADD  CONSTRAINT [FK_Users_HoldingDescriptions_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[HoldingDescriptions] CHECK CONSTRAINT [FK_Users_HoldingDescriptions_CreateUserId]
GO
ALTER TABLE [dbo].[HoldingDescriptions]  WITH CHECK ADD  CONSTRAINT [FK_Users_HoldingDescriptions_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[HoldingDescriptions] CHECK CONSTRAINT [FK_Users_HoldingDescriptions_UpdateUserId]
GO
ALTER TABLE [dbo].[Holdings]  WITH CHECK ADD  CONSTRAINT [FK_Users_Holdings_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Holdings] CHECK CONSTRAINT [FK_Users_Holdings_CreateUserId]
GO
ALTER TABLE [dbo].[Holdings]  WITH CHECK ADD  CONSTRAINT [FK_Users_Holdings_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Holdings] CHECK CONSTRAINT [FK_Users_Holdings_UpdateUserId]
GO
ALTER TABLE [dbo].[InnovationOptionGroups]  WITH CHECK ADD  CONSTRAINT [FK_Users_InnovationOptionGroups_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[InnovationOptionGroups] CHECK CONSTRAINT [FK_Users_InnovationOptionGroups_CreateUserId]
GO
ALTER TABLE [dbo].[InnovationOptionGroups]  WITH CHECK ADD  CONSTRAINT [FK_Users_InnovationOptionGroups_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[InnovationOptionGroups] CHECK CONSTRAINT [FK_Users_InnovationOptionGroups_UpdateUserId]
GO
ALTER TABLE [dbo].[InnovationOptions]  WITH CHECK ADD  CONSTRAINT [FK_InnovationOptionGroups_InnovationOptions_InnovationOptionGroupId] FOREIGN KEY([InnovationOptionGroupId])
REFERENCES [dbo].[InnovationOptionGroups] ([Id])
GO
ALTER TABLE [dbo].[InnovationOptions] CHECK CONSTRAINT [FK_InnovationOptionGroups_InnovationOptions_InnovationOptionGroupId]
GO
ALTER TABLE [dbo].[InnovationOptions]  WITH CHECK ADD  CONSTRAINT [FK_Users_InnovationOptions_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[InnovationOptions] CHECK CONSTRAINT [FK_Users_InnovationOptions_CreateUserId]
GO
ALTER TABLE [dbo].[InnovationOptions]  WITH CHECK ADD  CONSTRAINT [FK_Users_InnovationOptions_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[InnovationOptions] CHECK CONSTRAINT [FK_Users_InnovationOptions_UpdateUserId]
GO
ALTER TABLE [dbo].[InnovationOrganizationOptions]  WITH CHECK ADD  CONSTRAINT [FK_InnovationOptions_InnovationOrganizationOptions_InnovationOptionId] FOREIGN KEY([InnovationOptionId])
REFERENCES [dbo].[InnovationOptions] ([Id])
GO
ALTER TABLE [dbo].[InnovationOrganizationOptions] CHECK CONSTRAINT [FK_InnovationOptions_InnovationOrganizationOptions_InnovationOptionId]
GO
ALTER TABLE [dbo].[InnovationOrganizationOptions]  WITH CHECK ADD  CONSTRAINT [FK_InnovationOrganizations_InnovationOrganizationOptions_InnovationOrganizationId] FOREIGN KEY([InnovationOrganizationId])
REFERENCES [dbo].[InnovationOrganizations] ([Id])
GO
ALTER TABLE [dbo].[InnovationOrganizationOptions] CHECK CONSTRAINT [FK_InnovationOrganizations_InnovationOrganizationOptions_InnovationOrganizationId]
GO
ALTER TABLE [dbo].[InnovationOrganizationOptions]  WITH CHECK ADD  CONSTRAINT [FK_Users_InnovationOrganizationOptions_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[InnovationOrganizationOptions] CHECK CONSTRAINT [FK_Users_InnovationOrganizationOptions_CreateUserId]
GO
ALTER TABLE [dbo].[InnovationOrganizationOptions]  WITH CHECK ADD  CONSTRAINT [FK_Users_InnovationOrganizationOptions_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[InnovationOrganizationOptions] CHECK CONSTRAINT [FK_Users_InnovationOrganizationOptions_UpdateUserId]
GO
ALTER TABLE [dbo].[InnovationOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_Users_InnovationOrganizations_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[InnovationOrganizations] CHECK CONSTRAINT [FK_Users_InnovationOrganizations_CreateUserId]
GO
ALTER TABLE [dbo].[InnovationOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_Users_InnovationOrganizations_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[InnovationOrganizations] CHECK CONSTRAINT [FK_Users_InnovationOrganizations_UpdateUserId]
GO
ALTER TABLE [dbo].[InnovationOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_WorkDedications_InnovationOrganizations_WorkDedicationId] FOREIGN KEY([WorkDedicationId])
REFERENCES [dbo].[WorkDedications] ([Id])
GO
ALTER TABLE [dbo].[InnovationOrganizations] CHECK CONSTRAINT [FK_WorkDedications_InnovationOrganizations_WorkDedicationId]
GO
ALTER TABLE [dbo].[InterestGroups]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTypes_InterestGroups_ProjectTypeId] FOREIGN KEY([ProjectTypeId])
REFERENCES [dbo].[ProjectTypes] ([Id])
GO
ALTER TABLE [dbo].[InterestGroups] CHECK CONSTRAINT [FK_ProjectTypes_InterestGroups_ProjectTypeId]
GO
ALTER TABLE [dbo].[InterestGroups]  WITH CHECK ADD  CONSTRAINT [FK_Users_InterestGroups_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[InterestGroups] CHECK CONSTRAINT [FK_Users_InterestGroups_CreateUserId]
GO
ALTER TABLE [dbo].[InterestGroups]  WITH CHECK ADD  CONSTRAINT [FK_Users_InterestGroups_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[InterestGroups] CHECK CONSTRAINT [FK_Users_InterestGroups_UpdateUserId]
GO
ALTER TABLE [dbo].[Interests]  WITH CHECK ADD  CONSTRAINT [FK_InterestGroups_Interests_InterestGroupId] FOREIGN KEY([InterestGroupId])
REFERENCES [dbo].[InterestGroups] ([Id])
GO
ALTER TABLE [dbo].[Interests] CHECK CONSTRAINT [FK_InterestGroups_Interests_InterestGroupId]
GO
ALTER TABLE [dbo].[Interests]  WITH CHECK ADD  CONSTRAINT [FK_Users_Interests_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Interests] CHECK CONSTRAINT [FK_Users_Interests_CreateUserId]
GO
ALTER TABLE [dbo].[Interests]  WITH CHECK ADD  CONSTRAINT [FK_Users_Interests_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Interests] CHECK CONSTRAINT [FK_Users_Interests_UpdateUserId]
GO
ALTER TABLE [dbo].[Languages]  WITH CHECK ADD  CONSTRAINT [FK_Users_Languages_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Languages] CHECK CONSTRAINT [FK_Users_Languages_CreateUserId]
GO
ALTER TABLE [dbo].[Languages]  WITH CHECK ADD  CONSTRAINT [FK_Users_Languages_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Languages] CHECK CONSTRAINT [FK_Users_Languages_UpdateUserId]
GO
ALTER TABLE [dbo].[LogisticAccommodations]  WITH CHECK ADD  CONSTRAINT [FK_AttendeePlaces_LogisticAccommodations_AttendeePlaceId] FOREIGN KEY([AttendeePlaceId])
REFERENCES [dbo].[AttendeePlaces] ([Id])
GO
ALTER TABLE [dbo].[LogisticAccommodations] CHECK CONSTRAINT [FK_AttendeePlaces_LogisticAccommodations_AttendeePlaceId]
GO
ALTER TABLE [dbo].[LogisticAccommodations]  WITH CHECK ADD  CONSTRAINT [FK_Logistics_LogisticAccommodations_LogisticId] FOREIGN KEY([LogisticId])
REFERENCES [dbo].[Logistics] ([Id])
GO
ALTER TABLE [dbo].[LogisticAccommodations] CHECK CONSTRAINT [FK_Logistics_LogisticAccommodations_LogisticId]
GO
ALTER TABLE [dbo].[LogisticAccommodations]  WITH CHECK ADD  CONSTRAINT [FK_Users_LogisticAccommodations_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[LogisticAccommodations] CHECK CONSTRAINT [FK_Users_LogisticAccommodations_CreateUserId]
GO
ALTER TABLE [dbo].[LogisticAccommodations]  WITH CHECK ADD  CONSTRAINT [FK_Users_LogisticAccommodations_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[LogisticAccommodations] CHECK CONSTRAINT [FK_Users_LogisticAccommodations_UpdateUserId]
GO
ALTER TABLE [dbo].[LogisticAirfares]  WITH CHECK ADD  CONSTRAINT [FK_Logistics_LogisticAirfares_LogisticId] FOREIGN KEY([LogisticId])
REFERENCES [dbo].[Logistics] ([Id])
GO
ALTER TABLE [dbo].[LogisticAirfares] CHECK CONSTRAINT [FK_Logistics_LogisticAirfares_LogisticId]
GO
ALTER TABLE [dbo].[LogisticAirfares]  WITH CHECK ADD  CONSTRAINT [FK_Users_LogisticAirfares_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[LogisticAirfares] CHECK CONSTRAINT [FK_Users_LogisticAirfares_CreateUserId]
GO
ALTER TABLE [dbo].[LogisticAirfares]  WITH CHECK ADD  CONSTRAINT [FK_Users_LogisticAirfares_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[LogisticAirfares] CHECK CONSTRAINT [FK_Users_LogisticAirfares_UpdateUserId]
GO
ALTER TABLE [dbo].[Logistics]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeCollaborators_Logistics_AttendeeCollaboratorId] FOREIGN KEY([AttendeeCollaboratorId])
REFERENCES [dbo].[AttendeeCollaborators] ([Id])
GO
ALTER TABLE [dbo].[Logistics] CHECK CONSTRAINT [FK_AttendeeCollaborators_Logistics_AttendeeCollaboratorId]
GO
ALTER TABLE [dbo].[Logistics]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeLogisticSponsors_Logistics_AccommodationAttendeeLogisticSponsorId] FOREIGN KEY([AccommodationAttendeeLogisticSponsorId])
REFERENCES [dbo].[AttendeeLogisticSponsors] ([Id])
GO
ALTER TABLE [dbo].[Logistics] CHECK CONSTRAINT [FK_AttendeeLogisticSponsors_Logistics_AccommodationAttendeeLogisticSponsorId]
GO
ALTER TABLE [dbo].[Logistics]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeLogisticSponsors_Logistics_AirfareAttendeeLogisticSponsorId] FOREIGN KEY([AirfareAttendeeLogisticSponsorId])
REFERENCES [dbo].[AttendeeLogisticSponsors] ([Id])
GO
ALTER TABLE [dbo].[Logistics] CHECK CONSTRAINT [FK_AttendeeLogisticSponsors_Logistics_AirfareAttendeeLogisticSponsorId]
GO
ALTER TABLE [dbo].[Logistics]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeLogisticSponsors_Logistics_AirportTransferAttendeeLogisticSponsorId] FOREIGN KEY([AirportTransferAttendeeLogisticSponsorId])
REFERENCES [dbo].[AttendeeLogisticSponsors] ([Id])
GO
ALTER TABLE [dbo].[Logistics] CHECK CONSTRAINT [FK_AttendeeLogisticSponsors_Logistics_AirportTransferAttendeeLogisticSponsorId]
GO
ALTER TABLE [dbo].[Logistics]  WITH CHECK ADD  CONSTRAINT [FK_Users_Logistics_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Logistics] CHECK CONSTRAINT [FK_Users_Logistics_CreateUserId]
GO
ALTER TABLE [dbo].[Logistics]  WITH CHECK ADD  CONSTRAINT [FK_Users_Logistics_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Logistics] CHECK CONSTRAINT [FK_Users_Logistics_UpdateUserId]
GO
ALTER TABLE [dbo].[LogisticSponsors]  WITH CHECK ADD  CONSTRAINT [FK_Users_LogisticSponsors_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[LogisticSponsors] CHECK CONSTRAINT [FK_Users_LogisticSponsors_CreateUserId]
GO
ALTER TABLE [dbo].[LogisticSponsors]  WITH CHECK ADD  CONSTRAINT [FK_Users_LogisticSponsors_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[LogisticSponsors] CHECK CONSTRAINT [FK_Users_LogisticSponsors_UpdateUserId]
GO
ALTER TABLE [dbo].[LogisticTransfers]  WITH CHECK ADD  CONSTRAINT [FK_AttendeePlaces_LogisticTransfers_FromAttendeePlaceId] FOREIGN KEY([FromAttendeePlaceId])
REFERENCES [dbo].[AttendeePlaces] ([Id])
GO
ALTER TABLE [dbo].[LogisticTransfers] CHECK CONSTRAINT [FK_AttendeePlaces_LogisticTransfers_FromAttendeePlaceId]
GO
ALTER TABLE [dbo].[LogisticTransfers]  WITH CHECK ADD  CONSTRAINT [FK_AttendeePlaces_LogisticTransfers_ToAttendeePlaceId] FOREIGN KEY([ToAttendeePlaceId])
REFERENCES [dbo].[AttendeePlaces] ([Id])
GO
ALTER TABLE [dbo].[LogisticTransfers] CHECK CONSTRAINT [FK_AttendeePlaces_LogisticTransfers_ToAttendeePlaceId]
GO
ALTER TABLE [dbo].[LogisticTransfers]  WITH CHECK ADD  CONSTRAINT [FK_Logistics_LogisticTransfers_LogisticId] FOREIGN KEY([LogisticId])
REFERENCES [dbo].[Logistics] ([Id])
GO
ALTER TABLE [dbo].[LogisticTransfers] CHECK CONSTRAINT [FK_Logistics_LogisticTransfers_LogisticId]
GO
ALTER TABLE [dbo].[LogisticTransfers]  WITH CHECK ADD  CONSTRAINT [FK_LogisticTransferStatuses_LogisticTransfers_LogisticTransferStatusId] FOREIGN KEY([LogisticTransferStatusId])
REFERENCES [dbo].[LogisticTransferStatuses] ([Id])
GO
ALTER TABLE [dbo].[LogisticTransfers] CHECK CONSTRAINT [FK_LogisticTransferStatuses_LogisticTransfers_LogisticTransferStatusId]
GO
ALTER TABLE [dbo].[LogisticTransfers]  WITH CHECK ADD  CONSTRAINT [FK_Users_LogisticTransfers_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[LogisticTransfers] CHECK CONSTRAINT [FK_Users_LogisticTransfers_CreateUserId]
GO
ALTER TABLE [dbo].[LogisticTransfers]  WITH CHECK ADD  CONSTRAINT [FK_Users_LogisticTransfers_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[LogisticTransfers] CHECK CONSTRAINT [FK_Users_LogisticTransfers_UpdateUserId]
GO
ALTER TABLE [dbo].[LogisticTransferStatuses]  WITH CHECK ADD  CONSTRAINT [FK_Users_LogisticTransferStatuses_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[LogisticTransferStatuses] CHECK CONSTRAINT [FK_Users_LogisticTransferStatuses_CreateUserId]
GO
ALTER TABLE [dbo].[LogisticTransferStatuses]  WITH CHECK ADD  CONSTRAINT [FK_Users_LogisticTransferStatuses_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[LogisticTransferStatuses] CHECK CONSTRAINT [FK_Users_LogisticTransferStatuses_UpdateUserId]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Editions_Messages_EditionId] FOREIGN KEY([EditionId])
REFERENCES [dbo].[Editions] ([Id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Editions_Messages_EditionId]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Users_Messages_RecipientId] FOREIGN KEY([RecipientId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Users_Messages_RecipientId]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Users_Messages_SenderId] FOREIGN KEY([SenderId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Users_Messages_SenderId]
GO
ALTER TABLE [dbo].[MusicBandGenres]  WITH CHECK ADD  CONSTRAINT [FK_MusicBands_MusicBandGenres_MusicBandId] FOREIGN KEY([MusicBandId])
REFERENCES [dbo].[MusicBands] ([Id])
GO
ALTER TABLE [dbo].[MusicBandGenres] CHECK CONSTRAINT [FK_MusicBands_MusicBandGenres_MusicBandId]
GO
ALTER TABLE [dbo].[MusicBandGenres]  WITH CHECK ADD  CONSTRAINT [FK_MusicGenres_MusicBandGenres_MusicGenreId] FOREIGN KEY([MusicGenreId])
REFERENCES [dbo].[MusicGenres] ([Id])
GO
ALTER TABLE [dbo].[MusicBandGenres] CHECK CONSTRAINT [FK_MusicGenres_MusicBandGenres_MusicGenreId]
GO
ALTER TABLE [dbo].[MusicBandGenres]  WITH CHECK ADD  CONSTRAINT [FK_Users_MusicBandGenres_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[MusicBandGenres] CHECK CONSTRAINT [FK_Users_MusicBandGenres_CreateUserId]
GO
ALTER TABLE [dbo].[MusicBandGenres]  WITH CHECK ADD  CONSTRAINT [FK_Users_MusicBandGenres_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[MusicBandGenres] CHECK CONSTRAINT [FK_Users_MusicBandGenres_UpdateUserId]
GO
ALTER TABLE [dbo].[MusicBandMembers]  WITH CHECK ADD  CONSTRAINT [FK_MusicBands_MusicBandMembers_MusicBandId] FOREIGN KEY([MusicBandId])
REFERENCES [dbo].[MusicBands] ([Id])
GO
ALTER TABLE [dbo].[MusicBandMembers] CHECK CONSTRAINT [FK_MusicBands_MusicBandMembers_MusicBandId]
GO
ALTER TABLE [dbo].[MusicBandMembers]  WITH CHECK ADD  CONSTRAINT [FK_Users_MusicBandMembers_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[MusicBandMembers] CHECK CONSTRAINT [FK_Users_MusicBandMembers_CreateUserId]
GO
ALTER TABLE [dbo].[MusicBandMembers]  WITH CHECK ADD  CONSTRAINT [FK_Users_MusicBandMembers_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[MusicBandMembers] CHECK CONSTRAINT [FK_Users_MusicBandMembers_UpdateUserId]
GO
ALTER TABLE [dbo].[MusicBands]  WITH CHECK ADD  CONSTRAINT [FK_MusicBandTypes_MusicBands_MusicBandTypeId] FOREIGN KEY([MusicBandTypeId])
REFERENCES [dbo].[MusicBandTypes] ([Id])
GO
ALTER TABLE [dbo].[MusicBands] CHECK CONSTRAINT [FK_MusicBandTypes_MusicBands_MusicBandTypeId]
GO
ALTER TABLE [dbo].[MusicBands]  WITH CHECK ADD  CONSTRAINT [FK_Users_MusicBands_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[MusicBands] CHECK CONSTRAINT [FK_Users_MusicBands_CreateUserId]
GO
ALTER TABLE [dbo].[MusicBands]  WITH CHECK ADD  CONSTRAINT [FK_Users_MusicBands_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[MusicBands] CHECK CONSTRAINT [FK_Users_MusicBands_UpdateUserId]
GO
ALTER TABLE [dbo].[MusicBandTargetAudiences]  WITH CHECK ADD  CONSTRAINT [FK_MusicBands_MusicBandTargetAudiences_MusicBandId] FOREIGN KEY([MusicBandId])
REFERENCES [dbo].[MusicBands] ([Id])
GO
ALTER TABLE [dbo].[MusicBandTargetAudiences] CHECK CONSTRAINT [FK_MusicBands_MusicBandTargetAudiences_MusicBandId]
GO
ALTER TABLE [dbo].[MusicBandTargetAudiences]  WITH CHECK ADD  CONSTRAINT [FK_TargetAudiences_MusicBandTargetAudiences_TargetAudienceId] FOREIGN KEY([TargetAudienceId])
REFERENCES [dbo].[TargetAudiences] ([Id])
GO
ALTER TABLE [dbo].[MusicBandTargetAudiences] CHECK CONSTRAINT [FK_TargetAudiences_MusicBandTargetAudiences_TargetAudienceId]
GO
ALTER TABLE [dbo].[MusicBandTargetAudiences]  WITH CHECK ADD  CONSTRAINT [FK_Users_MusicBandTargetAudiences_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[MusicBandTargetAudiences] CHECK CONSTRAINT [FK_Users_MusicBandTargetAudiences_CreateUserId]
GO
ALTER TABLE [dbo].[MusicBandTargetAudiences]  WITH CHECK ADD  CONSTRAINT [FK_Users_MusicBandTargetAudiences_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[MusicBandTargetAudiences] CHECK CONSTRAINT [FK_Users_MusicBandTargetAudiences_UpdateUserId]
GO
ALTER TABLE [dbo].[MusicBandTeamMembers]  WITH CHECK ADD  CONSTRAINT [FK_MusicBands_MusicBandTeamMembers_MusicBandId] FOREIGN KEY([MusicBandId])
REFERENCES [dbo].[MusicBands] ([Id])
GO
ALTER TABLE [dbo].[MusicBandTeamMembers] CHECK CONSTRAINT [FK_MusicBands_MusicBandTeamMembers_MusicBandId]
GO
ALTER TABLE [dbo].[MusicBandTeamMembers]  WITH CHECK ADD  CONSTRAINT [FK_Users_MusicBandTeamMembers_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[MusicBandTeamMembers] CHECK CONSTRAINT [FK_Users_MusicBandTeamMembers_CreateUserId]
GO
ALTER TABLE [dbo].[MusicBandTeamMembers]  WITH CHECK ADD  CONSTRAINT [FK_Users_MusicBandTeamMembers_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[MusicBandTeamMembers] CHECK CONSTRAINT [FK_Users_MusicBandTeamMembers_UpdateUserId]
GO
ALTER TABLE [dbo].[MusicBandTypes]  WITH CHECK ADD  CONSTRAINT [FK_Users_MusicBandTypes_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[MusicBandTypes] CHECK CONSTRAINT [FK_Users_MusicBandTypes_CreateUserId]
GO
ALTER TABLE [dbo].[MusicBandTypes]  WITH CHECK ADD  CONSTRAINT [FK_Users_MusicBandTypes_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[MusicBandTypes] CHECK CONSTRAINT [FK_Users_MusicBandTypes_UpdateUserId]
GO
ALTER TABLE [dbo].[MusicGenres]  WITH CHECK ADD  CONSTRAINT [FK_Users_MusicGenres_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[MusicGenres] CHECK CONSTRAINT [FK_Users_MusicGenres_CreateUserId]
GO
ALTER TABLE [dbo].[MusicGenres]  WITH CHECK ADD  CONSTRAINT [FK_Users_MusicGenres_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[MusicGenres] CHECK CONSTRAINT [FK_Users_MusicGenres_UpdateUserId]
GO
ALTER TABLE [dbo].[MusicProjects]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeMusicBands_MusicProjects_AttendeeMusicBandId] FOREIGN KEY([AttendeeMusicBandId])
REFERENCES [dbo].[AttendeeMusicBands] ([Id])
GO
ALTER TABLE [dbo].[MusicProjects] CHECK CONSTRAINT [FK_AttendeeMusicBands_MusicProjects_AttendeeMusicBandId]
GO
ALTER TABLE [dbo].[MusicProjects]  WITH CHECK ADD  CONSTRAINT [FK_ProjectEvaluationRefuseReasons_MusicProjects_ProjectEvaluationRefuseReasonId] FOREIGN KEY([ProjectEvaluationRefuseReasonId])
REFERENCES [dbo].[ProjectEvaluationRefuseReasons] ([Id])
GO
ALTER TABLE [dbo].[MusicProjects] CHECK CONSTRAINT [FK_ProjectEvaluationRefuseReasons_MusicProjects_ProjectEvaluationRefuseReasonId]
GO
ALTER TABLE [dbo].[MusicProjects]  WITH CHECK ADD  CONSTRAINT [FK_ProjectEvaluationStatuses_MusicProjects_ProjectEvaluationStatusId] FOREIGN KEY([ProjectEvaluationStatusId])
REFERENCES [dbo].[ProjectEvaluationStatuses] ([Id])
GO
ALTER TABLE [dbo].[MusicProjects] CHECK CONSTRAINT [FK_ProjectEvaluationStatuses_MusicProjects_ProjectEvaluationStatusId]
GO
ALTER TABLE [dbo].[MusicProjects]  WITH CHECK ADD  CONSTRAINT [FK_Users_MusicProjects_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[MusicProjects] CHECK CONSTRAINT [FK_Users_MusicProjects_CreateUserId]
GO
ALTER TABLE [dbo].[MusicProjects]  WITH CHECK ADD  CONSTRAINT [FK_Users_MusicProjects_EvaluationUserId] FOREIGN KEY([EvaluationUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[MusicProjects] CHECK CONSTRAINT [FK_Users_MusicProjects_EvaluationUserId]
GO
ALTER TABLE [dbo].[MusicProjects]  WITH CHECK ADD  CONSTRAINT [FK_Users_MusicProjects_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[MusicProjects] CHECK CONSTRAINT [FK_Users_MusicProjects_UpdateUserId]
GO
ALTER TABLE [dbo].[NegotiationConfigs]  WITH CHECK ADD  CONSTRAINT [FK_Editions_NegotiationConfigs_EditionId] FOREIGN KEY([EditionId])
REFERENCES [dbo].[Editions] ([Id])
GO
ALTER TABLE [dbo].[NegotiationConfigs] CHECK CONSTRAINT [FK_Editions_NegotiationConfigs_EditionId]
GO
ALTER TABLE [dbo].[NegotiationConfigs]  WITH CHECK ADD  CONSTRAINT [FK_Users_NegotiationConfigs_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[NegotiationConfigs] CHECK CONSTRAINT [FK_Users_NegotiationConfigs_CreateUserId]
GO
ALTER TABLE [dbo].[NegotiationConfigs]  WITH CHECK ADD  CONSTRAINT [FK_Users_NegotiationConfigs_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[NegotiationConfigs] CHECK CONSTRAINT [FK_Users_NegotiationConfigs_UpdateUserId]
GO
ALTER TABLE [dbo].[NegotiationRoomConfigs]  WITH CHECK ADD  CONSTRAINT [FK_NegotiationConfigs_NegotiationRoomConfigs_NegotiationConfigId] FOREIGN KEY([NegotiationConfigId])
REFERENCES [dbo].[NegotiationConfigs] ([Id])
GO
ALTER TABLE [dbo].[NegotiationRoomConfigs] CHECK CONSTRAINT [FK_NegotiationConfigs_NegotiationRoomConfigs_NegotiationConfigId]
GO
ALTER TABLE [dbo].[NegotiationRoomConfigs]  WITH CHECK ADD  CONSTRAINT [FK_Rooms_NegotiationRoomConfigs_RoomId] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([Id])
GO
ALTER TABLE [dbo].[NegotiationRoomConfigs] CHECK CONSTRAINT [FK_Rooms_NegotiationRoomConfigs_RoomId]
GO
ALTER TABLE [dbo].[NegotiationRoomConfigs]  WITH CHECK ADD  CONSTRAINT [FK_Users_NegotiationRoomConfigs_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[NegotiationRoomConfigs] CHECK CONSTRAINT [FK_Users_NegotiationRoomConfigs_CreateUserId]
GO
ALTER TABLE [dbo].[NegotiationRoomConfigs]  WITH CHECK ADD  CONSTRAINT [FK_Users_NegotiationRoomConfigs_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[NegotiationRoomConfigs] CHECK CONSTRAINT [FK_Users_NegotiationRoomConfigs_UpdateUserId]
GO
ALTER TABLE [dbo].[Negotiations]  WITH CHECK ADD  CONSTRAINT [FK_ProjectBuyerEvaluations_Negotiations_ProjectBuyerEvaluationId] FOREIGN KEY([ProjectBuyerEvaluationId])
REFERENCES [dbo].[ProjectBuyerEvaluations] ([Id])
GO
ALTER TABLE [dbo].[Negotiations] CHECK CONSTRAINT [FK_ProjectBuyerEvaluations_Negotiations_ProjectBuyerEvaluationId]
GO
ALTER TABLE [dbo].[Negotiations]  WITH CHECK ADD  CONSTRAINT [FK_Rooms_Negotiations_RoomId] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([Id])
GO
ALTER TABLE [dbo].[Negotiations] CHECK CONSTRAINT [FK_Rooms_Negotiations_RoomId]
GO
ALTER TABLE [dbo].[Negotiations]  WITH CHECK ADD  CONSTRAINT [FK_Users_Negotiations_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Negotiations] CHECK CONSTRAINT [FK_Users_Negotiations_CreateUserId]
GO
ALTER TABLE [dbo].[Negotiations]  WITH CHECK ADD  CONSTRAINT [FK_Users_Negotiations_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Negotiations] CHECK CONSTRAINT [FK_Users_Negotiations_UpdateUserId]
GO
ALTER TABLE [dbo].[OrganizationActivities]  WITH CHECK ADD  CONSTRAINT [FK_Activities_OrganizationActivities_ActivityId] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activities] ([Id])
GO
ALTER TABLE [dbo].[OrganizationActivities] CHECK CONSTRAINT [FK_Activities_OrganizationActivities_ActivityId]
GO
ALTER TABLE [dbo].[OrganizationActivities]  WITH CHECK ADD  CONSTRAINT [FK_Organizations_OrganizationActivities_OrganizationId] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organizations] ([Id])
GO
ALTER TABLE [dbo].[OrganizationActivities] CHECK CONSTRAINT [FK_Organizations_OrganizationActivities_OrganizationId]
GO
ALTER TABLE [dbo].[OrganizationActivities]  WITH CHECK ADD  CONSTRAINT [FK_Users_OrganizationActivities_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[OrganizationActivities] CHECK CONSTRAINT [FK_Users_OrganizationActivities_CreateUserId]
GO
ALTER TABLE [dbo].[OrganizationActivities]  WITH CHECK ADD  CONSTRAINT [FK_Users_OrganizationActivities_UpdateuserId] FOREIGN KEY([UpdateuserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[OrganizationActivities] CHECK CONSTRAINT [FK_Users_OrganizationActivities_UpdateuserId]
GO
ALTER TABLE [dbo].[OrganizationDescriptions]  WITH CHECK ADD  CONSTRAINT [FK_Languages_OrganizationDescriptions_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[OrganizationDescriptions] CHECK CONSTRAINT [FK_Languages_OrganizationDescriptions_LanguageId]
GO
ALTER TABLE [dbo].[OrganizationDescriptions]  WITH CHECK ADD  CONSTRAINT [FK_Organizations_OrganizationDescriptions_OrganizationId] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organizations] ([Id])
GO
ALTER TABLE [dbo].[OrganizationDescriptions] CHECK CONSTRAINT [FK_Organizations_OrganizationDescriptions_OrganizationId]
GO
ALTER TABLE [dbo].[OrganizationDescriptions]  WITH CHECK ADD  CONSTRAINT [FK_Users_OrganizationDescriptions_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[OrganizationDescriptions] CHECK CONSTRAINT [FK_Users_OrganizationDescriptions_CreateUserId]
GO
ALTER TABLE [dbo].[OrganizationDescriptions]  WITH CHECK ADD  CONSTRAINT [FK_Users_OrganizationDescriptions_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[OrganizationDescriptions] CHECK CONSTRAINT [FK_Users_OrganizationDescriptions_UpdateUserId]
GO
ALTER TABLE [dbo].[OrganizationInterests]  WITH CHECK ADD  CONSTRAINT [FK_Interests_OrganizationInterests_InterestId] FOREIGN KEY([InterestId])
REFERENCES [dbo].[Interests] ([Id])
GO
ALTER TABLE [dbo].[OrganizationInterests] CHECK CONSTRAINT [FK_Interests_OrganizationInterests_InterestId]
GO
ALTER TABLE [dbo].[OrganizationInterests]  WITH CHECK ADD  CONSTRAINT [FK_Organizations_OrganizationInterests_OrganizationId] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organizations] ([Id])
GO
ALTER TABLE [dbo].[OrganizationInterests] CHECK CONSTRAINT [FK_Organizations_OrganizationInterests_OrganizationId]
GO
ALTER TABLE [dbo].[OrganizationInterests]  WITH CHECK ADD  CONSTRAINT [FK_Users_OrganizationInterests_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[OrganizationInterests] CHECK CONSTRAINT [FK_Users_OrganizationInterests_CreateUserId]
GO
ALTER TABLE [dbo].[OrganizationInterests]  WITH CHECK ADD  CONSTRAINT [FK_Users_OrganizationInterests_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[OrganizationInterests] CHECK CONSTRAINT [FK_Users_OrganizationInterests_UpdateUserId]
GO
ALTER TABLE [dbo].[OrganizationRestrictionSpecifics]  WITH CHECK ADD  CONSTRAINT [FK_Languages_OrganizationRestrictionSpecifics_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[OrganizationRestrictionSpecifics] CHECK CONSTRAINT [FK_Languages_OrganizationRestrictionSpecifics_LanguageId]
GO
ALTER TABLE [dbo].[OrganizationRestrictionSpecifics]  WITH CHECK ADD  CONSTRAINT [FK_Organizations_OrganizationRestrictionSpecifics_OrganizationId] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organizations] ([Id])
GO
ALTER TABLE [dbo].[OrganizationRestrictionSpecifics] CHECK CONSTRAINT [FK_Organizations_OrganizationRestrictionSpecifics_OrganizationId]
GO
ALTER TABLE [dbo].[OrganizationRestrictionSpecifics]  WITH CHECK ADD  CONSTRAINT [FK_Users_OrganizationRestrictionSpecifics_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[OrganizationRestrictionSpecifics] CHECK CONSTRAINT [FK_Users_OrganizationRestrictionSpecifics_CreateUserId]
GO
ALTER TABLE [dbo].[OrganizationRestrictionSpecifics]  WITH CHECK ADD  CONSTRAINT [FK_Users_OrganizationRestrictionSpecifics_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[OrganizationRestrictionSpecifics] CHECK CONSTRAINT [FK_Users_OrganizationRestrictionSpecifics_UpdateUserId]
GO
ALTER TABLE [dbo].[Organizations]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Organizations_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([Id])
GO
ALTER TABLE [dbo].[Organizations] CHECK CONSTRAINT [FK_Addresses_Organizations_AddressId]
GO
ALTER TABLE [dbo].[Organizations]  WITH CHECK ADD  CONSTRAINT [FK_Holdings_Organizations_HoldingId] FOREIGN KEY([HoldingId])
REFERENCES [dbo].[Holdings] ([Id])
GO
ALTER TABLE [dbo].[Organizations] CHECK CONSTRAINT [FK_Holdings_Organizations_HoldingId]
GO
ALTER TABLE [dbo].[Organizations]  WITH CHECK ADD  CONSTRAINT [FK_Users_Organizations_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Organizations] CHECK CONSTRAINT [FK_Users_Organizations_CreateUserId]
GO
ALTER TABLE [dbo].[Organizations]  WITH CHECK ADD  CONSTRAINT [FK_Users_Organizations_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Organizations] CHECK CONSTRAINT [FK_Users_Organizations_UpdateUserId]
GO
ALTER TABLE [dbo].[OrganizationTargetAudiences]  WITH CHECK ADD  CONSTRAINT [FK_Organizations_OrganizationTargetAudiences_OrganizationId] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organizations] ([Id])
GO
ALTER TABLE [dbo].[OrganizationTargetAudiences] CHECK CONSTRAINT [FK_Organizations_OrganizationTargetAudiences_OrganizationId]
GO
ALTER TABLE [dbo].[OrganizationTargetAudiences]  WITH CHECK ADD  CONSTRAINT [FK_TargetAudiences_OrganizationTargetAudiences_TargetAudienceId] FOREIGN KEY([TargetAudienceId])
REFERENCES [dbo].[TargetAudiences] ([Id])
GO
ALTER TABLE [dbo].[OrganizationTargetAudiences] CHECK CONSTRAINT [FK_TargetAudiences_OrganizationTargetAudiences_TargetAudienceId]
GO
ALTER TABLE [dbo].[OrganizationTargetAudiences]  WITH CHECK ADD  CONSTRAINT [FK_Users_OrganizationTargetAudiences_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[OrganizationTargetAudiences] CHECK CONSTRAINT [FK_Users_OrganizationTargetAudiences_CreateUserId]
GO
ALTER TABLE [dbo].[OrganizationTargetAudiences]  WITH CHECK ADD  CONSTRAINT [FK_Users_OrganizationTargetAudiences_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[OrganizationTargetAudiences] CHECK CONSTRAINT [FK_Users_OrganizationTargetAudiences_UpdateUserId]
GO
ALTER TABLE [dbo].[OrganizationTypes]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTypes_OrganizationTypes_RelatedProjectTypeId] FOREIGN KEY([RelatedProjectTypeId])
REFERENCES [dbo].[ProjectTypes] ([Id])
GO
ALTER TABLE [dbo].[OrganizationTypes] CHECK CONSTRAINT [FK_ProjectTypes_OrganizationTypes_RelatedProjectTypeId]
GO
ALTER TABLE [dbo].[OrganizationTypes]  WITH CHECK ADD  CONSTRAINT [FK_Users_OrganizationTypes_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[OrganizationTypes] CHECK CONSTRAINT [FK_Users_OrganizationTypes_CreateUserId]
GO
ALTER TABLE [dbo].[OrganizationTypes]  WITH CHECK ADD  CONSTRAINT [FK_Users_OrganizationTypes_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[OrganizationTypes] CHECK CONSTRAINT [FK_Users_OrganizationTypes_UpdateUserId]
GO
ALTER TABLE [dbo].[Pillars]  WITH CHECK ADD  CONSTRAINT [FK_Editions_Pillars_EditionId] FOREIGN KEY([EditionId])
REFERENCES [dbo].[Editions] ([Id])
GO
ALTER TABLE [dbo].[Pillars] CHECK CONSTRAINT [FK_Editions_Pillars_EditionId]
GO
ALTER TABLE [dbo].[Pillars]  WITH CHECK ADD  CONSTRAINT [FK_Users_Pillars_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Pillars] CHECK CONSTRAINT [FK_Users_Pillars_CreateUserId]
GO
ALTER TABLE [dbo].[Pillars]  WITH CHECK ADD  CONSTRAINT [FK_Users_Pillars_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Pillars] CHECK CONSTRAINT [FK_Users_Pillars_UpdateUserId]
GO
ALTER TABLE [dbo].[Places]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Places_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([Id])
GO
ALTER TABLE [dbo].[Places] CHECK CONSTRAINT [FK_Addresses_Places_AddressId]
GO
ALTER TABLE [dbo].[Places]  WITH CHECK ADD  CONSTRAINT [FK_Users_Places_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Places] CHECK CONSTRAINT [FK_Users_Places_CreateUserId]
GO
ALTER TABLE [dbo].[Places]  WITH CHECK ADD  CONSTRAINT [FK_Users_Places_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Places] CHECK CONSTRAINT [FK_Users_Places_UpdateUserId]
GO
ALTER TABLE [dbo].[PresentationFormats]  WITH CHECK ADD  CONSTRAINT [FK_Editions_PresentationFormats_EditionId] FOREIGN KEY([EditionId])
REFERENCES [dbo].[Editions] ([Id])
GO
ALTER TABLE [dbo].[PresentationFormats] CHECK CONSTRAINT [FK_Editions_PresentationFormats_EditionId]
GO
ALTER TABLE [dbo].[PresentationFormats]  WITH CHECK ADD  CONSTRAINT [FK_Users_PresentationFormats_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[PresentationFormats] CHECK CONSTRAINT [FK_Users_PresentationFormats_CreateUserId]
GO
ALTER TABLE [dbo].[PresentationFormats]  WITH CHECK ADD  CONSTRAINT [FK_Users_PresentationFormats_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[PresentationFormats] CHECK CONSTRAINT [FK_Users_PresentationFormats_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectAdditionalInformations]  WITH CHECK ADD  CONSTRAINT [FK_Languages_ProjectAdditionalInformations_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[ProjectAdditionalInformations] CHECK CONSTRAINT [FK_Languages_ProjectAdditionalInformations_LanguageId]
GO
ALTER TABLE [dbo].[ProjectAdditionalInformations]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectAdditionalInformations_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ProjectAdditionalInformations] CHECK CONSTRAINT [FK_Projects_ProjectAdditionalInformations_ProjectId]
GO
ALTER TABLE [dbo].[ProjectAdditionalInformations]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectAdditionalInformations_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectAdditionalInformations] CHECK CONSTRAINT [FK_Users_ProjectAdditionalInformations_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectAdditionalInformations]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectAdditionalInformations_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectAdditionalInformations] CHECK CONSTRAINT [FK_Users_ProjectAdditionalInformations_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeOrganizations_ProjectBuyerEvaluations_BuyerAttendeeOrganizationId] FOREIGN KEY([BuyerAttendeeOrganizationId])
REFERENCES [dbo].[AttendeeOrganizations] ([Id])
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations] CHECK CONSTRAINT [FK_AttendeeOrganizations_ProjectBuyerEvaluations_BuyerAttendeeOrganizationId]
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations]  WITH CHECK ADD  CONSTRAINT [FK_ProjectEvaluationRefuseReasons_ProjectBuyerEvaluations_ProjectEvaluationRefuseReasonId] FOREIGN KEY([ProjectEvaluationRefuseReasonId])
REFERENCES [dbo].[ProjectEvaluationRefuseReasons] ([Id])
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations] CHECK CONSTRAINT [FK_ProjectEvaluationRefuseReasons_ProjectBuyerEvaluations_ProjectEvaluationRefuseReasonId]
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations]  WITH CHECK ADD  CONSTRAINT [FK_ProjectEvaluationStatuses_ProjectBuyerEvaluations_ProjectEvaluationStatusId] FOREIGN KEY([ProjectEvaluationStatusId])
REFERENCES [dbo].[ProjectEvaluationStatuses] ([Id])
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations] CHECK CONSTRAINT [FK_ProjectEvaluationStatuses_ProjectBuyerEvaluations_ProjectEvaluationStatusId]
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectBuyerEvaluations_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations] CHECK CONSTRAINT [FK_Projects_ProjectBuyerEvaluations_ProjectId]
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectBuyerEvaluations_BuyerEvaluationUserId] FOREIGN KEY([BuyerEvaluationUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations] CHECK CONSTRAINT [FK_Users_ProjectBuyerEvaluations_BuyerEvaluationUserId]
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectBuyerEvaluations_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations] CHECK CONSTRAINT [FK_Users_ProjectBuyerEvaluations_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectBuyerEvaluations_SellerUserId] FOREIGN KEY([SellerUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations] CHECK CONSTRAINT [FK_Users_ProjectBuyerEvaluations_SellerUserId]
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectBuyerEvaluations_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectBuyerEvaluations] CHECK CONSTRAINT [FK_Users_ProjectBuyerEvaluations_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectEvaluationRefuseReasons]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTypes_ProjectEvaluationRefuseReasons_ProjectTypeId] FOREIGN KEY([ProjectTypeId])
REFERENCES [dbo].[ProjectTypes] ([Id])
GO
ALTER TABLE [dbo].[ProjectEvaluationRefuseReasons] CHECK CONSTRAINT [FK_ProjectTypes_ProjectEvaluationRefuseReasons_ProjectTypeId]
GO
ALTER TABLE [dbo].[ProjectEvaluationRefuseReasons]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectEvaluationRefuseReasons_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectEvaluationRefuseReasons] CHECK CONSTRAINT [FK_Users_ProjectEvaluationRefuseReasons_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectEvaluationRefuseReasons]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectEvaluationRefuseReasons_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectEvaluationRefuseReasons] CHECK CONSTRAINT [FK_Users_ProjectEvaluationRefuseReasons_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectEvaluationStatuses]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectEvaluationStatuses_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectEvaluationStatuses] CHECK CONSTRAINT [FK_Users_ProjectEvaluationStatuses_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectEvaluationStatuses]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectEvaluationStatuses_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectEvaluationStatuses] CHECK CONSTRAINT [FK_Users_ProjectEvaluationStatuses_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectImageLinks]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectImageLinks_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ProjectImageLinks] CHECK CONSTRAINT [FK_Projects_ProjectImageLinks_ProjectId]
GO
ALTER TABLE [dbo].[ProjectImageLinks]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectImageLinks_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectImageLinks] CHECK CONSTRAINT [FK_Users_ProjectImageLinks_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectImageLinks]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectImageLinks_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectImageLinks] CHECK CONSTRAINT [FK_Users_ProjectImageLinks_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectInterests]  WITH CHECK ADD  CONSTRAINT [FK_Interests_ProjectInterests_InterestId] FOREIGN KEY([InterestId])
REFERENCES [dbo].[Interests] ([Id])
GO
ALTER TABLE [dbo].[ProjectInterests] CHECK CONSTRAINT [FK_Interests_ProjectInterests_InterestId]
GO
ALTER TABLE [dbo].[ProjectInterests]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectInterests_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ProjectInterests] CHECK CONSTRAINT [FK_Projects_ProjectInterests_ProjectId]
GO
ALTER TABLE [dbo].[ProjectInterests]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectInterests_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectInterests] CHECK CONSTRAINT [FK_Users_ProjectInterests_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectInterests]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectInterests_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectInterests] CHECK CONSTRAINT [FK_Users_ProjectInterests_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectLogLines]  WITH CHECK ADD  CONSTRAINT [FK_Languages_ProjectLogLines_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[ProjectLogLines] CHECK CONSTRAINT [FK_Languages_ProjectLogLines_LanguageId]
GO
ALTER TABLE [dbo].[ProjectLogLines]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectLogLines_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ProjectLogLines] CHECK CONSTRAINT [FK_Projects_ProjectLogLines_ProjectId]
GO
ALTER TABLE [dbo].[ProjectLogLines]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectLogLines_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectLogLines] CHECK CONSTRAINT [FK_Users_ProjectLogLines_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectLogLines]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectLogLines_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectLogLines] CHECK CONSTRAINT [FK_Users_ProjectLogLines_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectProductionPlans]  WITH CHECK ADD  CONSTRAINT [FK_Languages_ProjectProductionPlans_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[ProjectProductionPlans] CHECK CONSTRAINT [FK_Languages_ProjectProductionPlans_LanguageId]
GO
ALTER TABLE [dbo].[ProjectProductionPlans]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectProductionPlans_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ProjectProductionPlans] CHECK CONSTRAINT [FK_Projects_ProjectProductionPlans_ProjectId]
GO
ALTER TABLE [dbo].[ProjectProductionPlans]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectProductionPlans_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectProductionPlans] CHECK CONSTRAINT [FK_Users_ProjectProductionPlans_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectProductionPlans]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectProductionPlans_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectProductionPlans] CHECK CONSTRAINT [FK_Users_ProjectProductionPlans_UpdateUserId]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeOrganizations_Projects_SellerAttendeeOrganizationId] FOREIGN KEY([SellerAttendeeOrganizationId])
REFERENCES [dbo].[AttendeeOrganizations] ([Id])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_AttendeeOrganizations_Projects_SellerAttendeeOrganizationId]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTypes_Projects_ProjectTypeId] FOREIGN KEY([ProjectTypeId])
REFERENCES [dbo].[ProjectTypes] ([Id])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_ProjectTypes_Projects_ProjectTypeId]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Users_Projects_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Users_Projects_CreateUserId]
GO
ALTER TABLE [dbo].[Projects]  WITH CHECK ADD  CONSTRAINT [FK_Users_Projects_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Projects] CHECK CONSTRAINT [FK_Users_Projects_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectSummaries]  WITH CHECK ADD  CONSTRAINT [FK_Languages_ProjectSummaries_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[ProjectSummaries] CHECK CONSTRAINT [FK_Languages_ProjectSummaries_LanguageId]
GO
ALTER TABLE [dbo].[ProjectSummaries]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectSummaries_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ProjectSummaries] CHECK CONSTRAINT [FK_Projects_ProjectSummaries_ProjectId]
GO
ALTER TABLE [dbo].[ProjectSummaries]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectSummaries_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectSummaries] CHECK CONSTRAINT [FK_Users_ProjectSummaries_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectSummaries]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectSummaries_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectSummaries] CHECK CONSTRAINT [FK_Users_ProjectSummaries_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectTargetAudiences]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectTargetAudiences_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ProjectTargetAudiences] CHECK CONSTRAINT [FK_Projects_ProjectTargetAudiences_ProjectId]
GO
ALTER TABLE [dbo].[ProjectTargetAudiences]  WITH CHECK ADD  CONSTRAINT [FK_TargetAudiences_ProjectTargetAudiences_TargetAudienceId] FOREIGN KEY([TargetAudienceId])
REFERENCES [dbo].[TargetAudiences] ([Id])
GO
ALTER TABLE [dbo].[ProjectTargetAudiences] CHECK CONSTRAINT [FK_TargetAudiences_ProjectTargetAudiences_TargetAudienceId]
GO
ALTER TABLE [dbo].[ProjectTargetAudiences]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectTargetAudiences_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectTargetAudiences] CHECK CONSTRAINT [FK_Users_ProjectTargetAudiences_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectTargetAudiences]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectTargetAudiences_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectTargetAudiences] CHECK CONSTRAINT [FK_Users_ProjectTargetAudiences_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectTeaserLinks]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectTeaserLinks_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ProjectTeaserLinks] CHECK CONSTRAINT [FK_Projects_ProjectTeaserLinks_ProjectId]
GO
ALTER TABLE [dbo].[ProjectTeaserLinks]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectTeaserLinks_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectTeaserLinks] CHECK CONSTRAINT [FK_Users_ProjectTeaserLinks_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectTeaserLinks]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectTeaserLinks_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectTeaserLinks] CHECK CONSTRAINT [FK_Users_ProjectTeaserLinks_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectTitles]  WITH CHECK ADD  CONSTRAINT [FK_Languages_ProjectTitles_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[ProjectTitles] CHECK CONSTRAINT [FK_Languages_ProjectTitles_LanguageId]
GO
ALTER TABLE [dbo].[ProjectTitles]  WITH CHECK ADD  CONSTRAINT [FK_Projects_ProjectTitles_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Projects] ([Id])
GO
ALTER TABLE [dbo].[ProjectTitles] CHECK CONSTRAINT [FK_Projects_ProjectTitles_ProjectId]
GO
ALTER TABLE [dbo].[ProjectTitles]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectTitles_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectTitles] CHECK CONSTRAINT [FK_Users_ProjectTitles_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectTitles]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectTitles_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectTitles] CHECK CONSTRAINT [FK_Users_ProjectTitles_UpdateUserId]
GO
ALTER TABLE [dbo].[ProjectTypes]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectTypes_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectTypes] CHECK CONSTRAINT [FK_Users_ProjectTypes_CreateUserId]
GO
ALTER TABLE [dbo].[ProjectTypes]  WITH CHECK ADD  CONSTRAINT [FK_Users_ProjectTypes_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ProjectTypes] CHECK CONSTRAINT [FK_Users_ProjectTypes_UpdateUserId]
GO
ALTER TABLE [dbo].[QuizAnswers]  WITH CHECK ADD  CONSTRAINT [FK_QuizOptions_QuizAnswers_QuizOptionId] FOREIGN KEY([QuizOptionId])
REFERENCES [dbo].[QuizOptions] ([Id])
GO
ALTER TABLE [dbo].[QuizAnswers] CHECK CONSTRAINT [FK_QuizOptions_QuizAnswers_QuizOptionId]
GO
ALTER TABLE [dbo].[QuizAnswers]  WITH CHECK ADD  CONSTRAINT [FK_Users_QuizAnswers_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[QuizAnswers] CHECK CONSTRAINT [FK_Users_QuizAnswers_CreateUserId]
GO
ALTER TABLE [dbo].[QuizAnswers]  WITH CHECK ADD  CONSTRAINT [FK_Users_QuizAnswers_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[QuizAnswers] CHECK CONSTRAINT [FK_Users_QuizAnswers_UpdateUserId]
GO
ALTER TABLE [dbo].[QuizOptions]  WITH CHECK ADD  CONSTRAINT [FK_QuizQuestions_QuizOptions_QuizQuestionId] FOREIGN KEY([QuizQuestionId])
REFERENCES [dbo].[QuizQuestions] ([Id])
GO
ALTER TABLE [dbo].[QuizOptions] CHECK CONSTRAINT [FK_QuizQuestions_QuizOptions_QuizQuestionId]
GO
ALTER TABLE [dbo].[QuizOptions]  WITH CHECK ADD  CONSTRAINT [FK_Users_QuizOptions_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[QuizOptions] CHECK CONSTRAINT [FK_Users_QuizOptions_CreateUserId]
GO
ALTER TABLE [dbo].[QuizOptions]  WITH CHECK ADD  CONSTRAINT [FK_Users_QuizOptions_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[QuizOptions] CHECK CONSTRAINT [FK_Users_QuizOptions_UpdateUserId]
GO
ALTER TABLE [dbo].[QuizQuestions]  WITH CHECK ADD  CONSTRAINT [FK_Quizzes_QuizQuestions_QuizId] FOREIGN KEY([QuizId])
REFERENCES [dbo].[Quizzes] ([Id])
GO
ALTER TABLE [dbo].[QuizQuestions] CHECK CONSTRAINT [FK_Quizzes_QuizQuestions_QuizId]
GO
ALTER TABLE [dbo].[QuizQuestions]  WITH CHECK ADD  CONSTRAINT [FK_Users_QuizQuestions_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[QuizQuestions] CHECK CONSTRAINT [FK_Users_QuizQuestions_CreateUserId]
GO
ALTER TABLE [dbo].[QuizQuestions]  WITH CHECK ADD  CONSTRAINT [FK_Users_QuizQuestions_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[QuizQuestions] CHECK CONSTRAINT [FK_Users_QuizQuestions_UpdateUserId]
GO
ALTER TABLE [dbo].[Quizzes]  WITH CHECK ADD  CONSTRAINT [FK_Editions_Quizzes_EditionId] FOREIGN KEY([EditionId])
REFERENCES [dbo].[Editions] ([Id])
GO
ALTER TABLE [dbo].[Quizzes] CHECK CONSTRAINT [FK_Editions_Quizzes_EditionId]
GO
ALTER TABLE [dbo].[Quizzes]  WITH CHECK ADD  CONSTRAINT [FK_Users_Quizzes_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Quizzes] CHECK CONSTRAINT [FK_Users_Quizzes_CreateUserId]
GO
ALTER TABLE [dbo].[Quizzes]  WITH CHECK ADD  CONSTRAINT [FK_Users_Quizzes_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Quizzes] CHECK CONSTRAINT [FK_Users_Quizzes_UpdateUserId]
GO
ALTER TABLE [dbo].[ReleasedMusicProjects]  WITH CHECK ADD  CONSTRAINT [FK_MusicBands_ReleasedMusicProjects_MusicBandId] FOREIGN KEY([MusicBandId])
REFERENCES [dbo].[MusicBands] ([Id])
GO
ALTER TABLE [dbo].[ReleasedMusicProjects] CHECK CONSTRAINT [FK_MusicBands_ReleasedMusicProjects_MusicBandId]
GO
ALTER TABLE [dbo].[ReleasedMusicProjects]  WITH CHECK ADD  CONSTRAINT [FK_Users_ReleasedMusicProjects_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ReleasedMusicProjects] CHECK CONSTRAINT [FK_Users_ReleasedMusicProjects_CreateUserId]
GO
ALTER TABLE [dbo].[ReleasedMusicProjects]  WITH CHECK ADD  CONSTRAINT [FK_Users_ReleasedMusicProjects_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[ReleasedMusicProjects] CHECK CONSTRAINT [FK_Users_ReleasedMusicProjects_UpdateUserId]
GO
ALTER TABLE [dbo].[RoomNames]  WITH CHECK ADD  CONSTRAINT [FK_Languages_RoomNames_LanguageId] FOREIGN KEY([LanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[RoomNames] CHECK CONSTRAINT [FK_Languages_RoomNames_LanguageId]
GO
ALTER TABLE [dbo].[RoomNames]  WITH CHECK ADD  CONSTRAINT [FK_Rooms_RoomNames_RoomId] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([Id])
GO
ALTER TABLE [dbo].[RoomNames] CHECK CONSTRAINT [FK_Rooms_RoomNames_RoomId]
GO
ALTER TABLE [dbo].[RoomNames]  WITH CHECK ADD  CONSTRAINT [FK_Users_RoomNames_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[RoomNames] CHECK CONSTRAINT [FK_Users_RoomNames_CreateUserId]
GO
ALTER TABLE [dbo].[RoomNames]  WITH CHECK ADD  CONSTRAINT [FK_Users_RoomNames_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[RoomNames] CHECK CONSTRAINT [FK_Users_RoomNames_UpdateUserId]
GO
ALTER TABLE [dbo].[Rooms]  WITH CHECK ADD  CONSTRAINT [FK_Editions_Rooms_EditionId] FOREIGN KEY([EditionId])
REFERENCES [dbo].[Editions] ([Id])
GO
ALTER TABLE [dbo].[Rooms] CHECK CONSTRAINT [FK_Editions_Rooms_EditionId]
GO
ALTER TABLE [dbo].[Rooms]  WITH CHECK ADD  CONSTRAINT [FK_Users_Rooms_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Rooms] CHECK CONSTRAINT [FK_Users_Rooms_CreateUserId]
GO
ALTER TABLE [dbo].[Rooms]  WITH CHECK ADD  CONSTRAINT [FK_Users_Rooms_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Rooms] CHECK CONSTRAINT [FK_Users_Rooms_UpdateUserId]
GO
ALTER TABLE [dbo].[SalesPlatforms]  WITH CHECK ADD  CONSTRAINT [FK_Users_SalesPlatforms_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[SalesPlatforms] CHECK CONSTRAINT [FK_Users_SalesPlatforms_CreateUserId]
GO
ALTER TABLE [dbo].[SalesPlatforms]  WITH CHECK ADD  CONSTRAINT [FK_Users_SalesPlatforms_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[SalesPlatforms] CHECK CONSTRAINT [FK_Users_SalesPlatforms_UpdateUserId]
GO
ALTER TABLE [dbo].[SalesPlatformWebhookRequests]  WITH CHECK ADD  CONSTRAINT [FK_SalesPlatforms_SalesPlatformWebhookRequests_SalesPlatformId] FOREIGN KEY([SalesPlatformId])
REFERENCES [dbo].[SalesPlatforms] ([Id])
GO
ALTER TABLE [dbo].[SalesPlatformWebhookRequests] CHECK CONSTRAINT [FK_SalesPlatforms_SalesPlatformWebhookRequests_SalesPlatformId]
GO
ALTER TABLE [dbo].[SalesPlatformWebhookRequests]  WITH CHECK ADD  CONSTRAINT [FK_Users_SalesPlatformWebhookRequests_ManualProcessingUserId] FOREIGN KEY([ManualProcessingUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[SalesPlatformWebhookRequests] CHECK CONSTRAINT [FK_Users_SalesPlatformWebhookRequests_ManualProcessingUserId]
GO
ALTER TABLE [dbo].[SentEmails]  WITH CHECK ADD  CONSTRAINT [FK_Editions_SentEmails_EditionId] FOREIGN KEY([EditionId])
REFERENCES [dbo].[Editions] ([Id])
GO
ALTER TABLE [dbo].[SentEmails] CHECK CONSTRAINT [FK_Editions_SentEmails_EditionId]
GO
ALTER TABLE [dbo].[SentEmails]  WITH CHECK ADD  CONSTRAINT [FK_Users_SentEmails_RecipientUserId] FOREIGN KEY([RecipientUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[SentEmails] CHECK CONSTRAINT [FK_Users_SentEmails_RecipientUserId]
GO
ALTER TABLE [dbo].[States]  WITH CHECK ADD  CONSTRAINT [FK_Countries_States_CountryId] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[States] CHECK CONSTRAINT [FK_Countries_States_CountryId]
GO
ALTER TABLE [dbo].[States]  WITH CHECK ADD  CONSTRAINT [FK_Users_States_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[States] CHECK CONSTRAINT [FK_Users_States_CreateUserId]
GO
ALTER TABLE [dbo].[States]  WITH CHECK ADD  CONSTRAINT [FK_Users_States_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[States] CHECK CONSTRAINT [FK_Users_States_UpdateUserId]
GO
ALTER TABLE [dbo].[SubscribeLists]  WITH CHECK ADD  CONSTRAINT [FK_Users_SubscribeLists_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[SubscribeLists] CHECK CONSTRAINT [FK_Users_SubscribeLists_CreateUserId]
GO
ALTER TABLE [dbo].[SubscribeLists]  WITH CHECK ADD  CONSTRAINT [FK_Users_SubscribeLists_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[SubscribeLists] CHECK CONSTRAINT [FK_Users_SubscribeLists_UpdateUserId]
GO
ALTER TABLE [dbo].[TargetAudiences]  WITH CHECK ADD  CONSTRAINT [FK_ProjectTypes_TargetAudiences_ProjectTypeId] FOREIGN KEY([ProjectTypeId])
REFERENCES [dbo].[ProjectTypes] ([Id])
GO
ALTER TABLE [dbo].[TargetAudiences] CHECK CONSTRAINT [FK_ProjectTypes_TargetAudiences_ProjectTypeId]
GO
ALTER TABLE [dbo].[TargetAudiences]  WITH CHECK ADD  CONSTRAINT [FK_Users_TargetAudiences_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[TargetAudiences] CHECK CONSTRAINT [FK_Users_TargetAudiences_CreateUserId]
GO
ALTER TABLE [dbo].[TargetAudiences]  WITH CHECK ADD  CONSTRAINT [FK_Users_TargetAudiences_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[TargetAudiences] CHECK CONSTRAINT [FK_Users_TargetAudiences_UpdateUserId]
GO
ALTER TABLE [dbo].[Tracks]  WITH CHECK ADD  CONSTRAINT [FK_Editions_Tracks_EditionId] FOREIGN KEY([EditionId])
REFERENCES [dbo].[Editions] ([Id])
GO
ALTER TABLE [dbo].[Tracks] CHECK CONSTRAINT [FK_Editions_Tracks_EditionId]
GO
ALTER TABLE [dbo].[Tracks]  WITH CHECK ADD  CONSTRAINT [FK_Users_Tracks_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Tracks] CHECK CONSTRAINT [FK_Users_Tracks_CreateUserId]
GO
ALTER TABLE [dbo].[Tracks]  WITH CHECK ADD  CONSTRAINT [FK_Users_Tracks_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Tracks] CHECK CONSTRAINT [FK_Users_Tracks_UpdateUserId]
GO
ALTER TABLE [dbo].[UserClaims]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserClaims_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserClaims] CHECK CONSTRAINT [FK_Users_UserClaims_UserId]
GO
ALTER TABLE [dbo].[UserLogins]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserLogins_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserLogins] CHECK CONSTRAINT [FK_Users_UserLogins_UserId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Languages_Users_UserInterfaceLanguageId] FOREIGN KEY([UserInterfaceLanguageId])
REFERENCES [dbo].[Languages] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Languages_Users_UserInterfaceLanguageId]
GO
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_Roles_UsersRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_Roles_UsersRoles_RoleId]
GO
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_Users_UsersRoles_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_Users_UsersRoles_UserId]
GO
ALTER TABLE [dbo].[UserUnsubscribedLists]  WITH CHECK ADD  CONSTRAINT [FK_SubscribeLists_UserUnsubscribedLists_SubscribeListId] FOREIGN KEY([SubscribeListId])
REFERENCES [dbo].[SubscribeLists] ([Id])
GO
ALTER TABLE [dbo].[UserUnsubscribedLists] CHECK CONSTRAINT [FK_SubscribeLists_UserUnsubscribedLists_SubscribeListId]
GO
ALTER TABLE [dbo].[UserUnsubscribedLists]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserUnsubscribedLists_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserUnsubscribedLists] CHECK CONSTRAINT [FK_Users_UserUnsubscribedLists_CreateUserId]
GO
ALTER TABLE [dbo].[UserUnsubscribedLists]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserUnsubscribedLists_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserUnsubscribedLists] CHECK CONSTRAINT [FK_Users_UserUnsubscribedLists_UpdateUserId]
GO
ALTER TABLE [dbo].[UserUnsubscribedLists]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserUnsubscribedLists_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UserUnsubscribedLists] CHECK CONSTRAINT [FK_Users_UserUnsubscribedLists_UserId]
GO
ALTER TABLE [dbo].[WorkDedications]  WITH CHECK ADD  CONSTRAINT [FK_Users_WorkDedications_CreateUserId] FOREIGN KEY([CreateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[WorkDedications] CHECK CONSTRAINT [FK_Users_WorkDedications_CreateUserId]
GO
ALTER TABLE [dbo].[WorkDedications]  WITH CHECK ADD  CONSTRAINT [FK_Users_WorkDedications_UpdateUserId] FOREIGN KEY([UpdateUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[WorkDedications] CHECK CONSTRAINT [FK_Users_WorkDedications_UpdateUserId]
GO
USE [master]
GO
ALTER DATABASE [MyRio2C_Schema] SET  READ_WRITE 
GO
