CREATE TABLE [dbo].[Activities] (
    [Id] [int] NOT NULL IDENTITY,
    [ProjectTypeId] [int] NOT NULL,
    [Name] [varchar](50),
    [DisplayOrder] [int] NOT NULL,
    [HasAdditionalInfo] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Activities] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ProjectTypeId] ON [dbo].[Activities]([ProjectTypeId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Activities]([Uid])
CREATE TABLE [dbo].[ProjectTypes] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](50) NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ProjectTypes] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ProjectTypes]([Uid])
CREATE TABLE [dbo].[InterestGroups] (
    [Id] [int] NOT NULL IDENTITY,
    [ProjectTypeId] [int] NOT NULL,
    [Name] [varchar](150) NOT NULL,
    [Type] [varchar](100) NOT NULL,
    [DisplayOrder] [int] NOT NULL,
    [IsCommission] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.InterestGroups] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ProjectTypeId] ON [dbo].[InterestGroups]([ProjectTypeId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[InterestGroups]([Uid])
CREATE TABLE [dbo].[OrganizationTypes] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](50) NOT NULL,
    [RelatedProjectTypeId] [int] NOT NULL,
    [IsSeller] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.OrganizationTypes] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_RelatedProjectTypeId] ON [dbo].[OrganizationTypes]([RelatedProjectTypeId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[OrganizationTypes]([Uid])
CREATE TABLE [dbo].[AttendeeOrganizationTypes] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeOrganizationId] [int] NOT NULL,
    [OrganizationTypeId] [int] NOT NULL,
    [IsApiDisplayEnabled] [bit] NOT NULL,
    [ApiHighlightPosition] [int],
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeOrganizationTypes] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeOrganizationId] ON [dbo].[AttendeeOrganizationTypes]([AttendeeOrganizationId])
CREATE INDEX [IX_OrganizationTypeId] ON [dbo].[AttendeeOrganizationTypes]([OrganizationTypeId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeOrganizationTypes]([Uid])
CREATE TABLE [dbo].[AttendeeOrganizations] (
    [Id] [int] NOT NULL IDENTITY,
    [EditionId] [int] NOT NULL,
    [OrganizationId] [int] NOT NULL,
    [OnboardingStartDate] [datetimeoffset](7),
    [OnboardingFinishDate] [datetimeoffset](7),
    [OnboardingOrganizationDate] [datetimeoffset](7),
    [OnboardingInterestsDate] [datetimeoffset](7),
    [ProjectSubmissionOrganizationDate] [datetimeoffset](7),
    [SellProjectsCount] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeOrganizations] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_EditionId] ON [dbo].[AttendeeOrganizations]([EditionId])
CREATE INDEX [IX_OrganizationId] ON [dbo].[AttendeeOrganizations]([OrganizationId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeOrganizations]([Uid])
CREATE TABLE [dbo].[AttendeeOrganizationCollaborators] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeOrganizationId] [int] NOT NULL,
    [AttendeeCollaboratorId] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeOrganizationCollaborators] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeOrganizationId] ON [dbo].[AttendeeOrganizationCollaborators]([AttendeeOrganizationId])
CREATE INDEX [IX_AttendeeCollaboratorId] ON [dbo].[AttendeeOrganizationCollaborators]([AttendeeCollaboratorId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeOrganizationCollaborators]([Uid])
CREATE TABLE [dbo].[AttendeeCollaborators] (
    [Id] [int] NOT NULL IDENTITY,
    [EditionId] [int] NOT NULL,
    [CollaboratorId] [int] NOT NULL,
    [WelcomeEmailSendDate] [datetimeoffset](7),
    [OnboardingStartDate] [datetimeoffset](7),
    [OnboardingFinishDate] [datetimeoffset](7),
    [OnboardingUserDate] [datetimeoffset](7),
    [OnboardingCollaboratorDate] [datetimeoffset](7),
    [OnboardingOrganizationDataSkippedDate] [datetimeoffset](7),
    [AudiovisualPlayerTermsAcceptanceDate] [datetimeoffset](7),
    [InnovationPlayerTermsAcceptanceDate] [datetimeoffset](7),
    [MusicPlayerTermsAcceptanceDate] [datetimeoffset](7),
    [ProducerTermsAcceptanceDate] [datetimeoffset](7),
    [SpeakerTermsAcceptanceDate] [datetimeoffset](7),
    [AvailabilityBeginDate] [datetimeoffset](7),
    [AvailabilityEndDate] [datetimeoffset](7),
    [AgendaEmailSendDate] [datetimeoffset](7),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeCollaborators] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_EditionId] ON [dbo].[AttendeeCollaborators]([EditionId])
CREATE INDEX [IX_CollaboratorId] ON [dbo].[AttendeeCollaborators]([CollaboratorId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeCollaborators]([Uid])
CREATE TABLE [dbo].[AttendeeCartoonProjectCollaborators] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeCartoonProjectId] [int] NOT NULL,
    [AttendeeCollaboratorId] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeCartoonProjectCollaborators] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeCartoonProjectId] ON [dbo].[AttendeeCartoonProjectCollaborators]([AttendeeCartoonProjectId])
CREATE INDEX [IX_AttendeeCollaboratorId] ON [dbo].[AttendeeCartoonProjectCollaborators]([AttendeeCollaboratorId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeCartoonProjectCollaborators]([Uid])
CREATE TABLE [dbo].[AttendeeCartoonProjects] (
    [Id] [int] NOT NULL IDENTITY,
    [EditionId] [int] NOT NULL,
    [CartoonProjectId] [int] NOT NULL,
    [Grade] [decimal](18, 2),
    [EvaluationsCount] [int] NOT NULL,
    [LastEvaluationDate] [datetimeoffset](7),
    [EvaluationEmailSendDate] [datetimeoffset](7),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeCartoonProjects] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_EditionId] ON [dbo].[AttendeeCartoonProjects]([EditionId])
CREATE INDEX [IX_CartoonProjectId] ON [dbo].[AttendeeCartoonProjects]([CartoonProjectId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeCartoonProjects]([Uid])
CREATE TABLE [dbo].[AttendeeCartoonProjectEvaluations] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeCartoonProjectId] [int] NOT NULL,
    [EvaluatorUserId] [int] NOT NULL,
    [Grade] [decimal](18, 2) NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeCartoonProjectEvaluations] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeCartoonProjectId] ON [dbo].[AttendeeCartoonProjectEvaluations]([AttendeeCartoonProjectId])
CREATE INDEX [IX_EvaluatorUserId] ON [dbo].[AttendeeCartoonProjectEvaluations]([EvaluatorUserId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeCartoonProjectEvaluations]([Uid])
CREATE TABLE [dbo].[Users] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](150) NOT NULL,
    [Active] [bit] NOT NULL,
    [UserName] [varchar](256) NOT NULL,
    [Email] [varchar](256),
    [EmailConfirmed] [bit] NOT NULL,
    [PasswordHash] [varchar](8000),
    [PasswordNew] [varchar](50),
    [SecurityStamp] [varchar](8000),
    [PhoneNumber] [varchar](8000),
    [PhoneNumberConfirmed] [bit] NOT NULL,
    [TwoFactorEnabled] [bit] NOT NULL,
    [LockoutEndDateUtc] [datetime],
    [LockoutEnabled] [bit] NOT NULL,
    [AccessFailedCount] [int] NOT NULL,
    [UserInterfaceLanguageId] [int],
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    CONSTRAINT [PK_dbo.Users] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_UserInterfaceLanguageId] ON [dbo].[Users]([UserInterfaceLanguageId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Users]([Uid])
CREATE TABLE [dbo].[AttendeeCreatorProjectEvaluations] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeCreatorProjectId] [int] NOT NULL,
    [EvaluatorUserId] [int] NOT NULL,
    [Grade] [decimal](18, 2) NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeCreatorProjectEvaluations] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeCreatorProjectId] ON [dbo].[AttendeeCreatorProjectEvaluations]([AttendeeCreatorProjectId])
CREATE INDEX [IX_EvaluatorUserId] ON [dbo].[AttendeeCreatorProjectEvaluations]([EvaluatorUserId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeCreatorProjectEvaluations]([Uid])
CREATE TABLE [dbo].[AttendeeCreatorProjects] (
    [Id] [int] NOT NULL IDENTITY,
    [CreatorProjectId] [int] NOT NULL,
    [EditionId] [int] NOT NULL,
    [Grade] [decimal](18, 2),
    [EvaluationsCount] [int] NOT NULL,
    [LastEvaluationDate] [datetimeoffset](7),
    [EvaluationEmailSendDate] [datetimeoffset](7),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeCreatorProjects] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_CreatorProjectId] ON [dbo].[AttendeeCreatorProjects]([CreatorProjectId])
CREATE INDEX [IX_EditionId] ON [dbo].[AttendeeCreatorProjects]([EditionId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeCreatorProjects]([Uid])
CREATE TABLE [dbo].[CreatorProjects] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](500),
    [Document] [varchar](50),
    [Email] [varchar](50),
    [AgentName] [varchar](300),
    [PhoneNumber] [varchar](50),
    [Curriculum] [varchar](750),
    [Title] [varchar](300),
    [Logline] [varchar](300),
    [Description] [varchar](5000),
    [MotivationToDevelop] [varchar](3000),
    [MotivationToTransform] [varchar](3000),
    [DiversityAndInclusionElements] [varchar](3000),
    [ThemeRelevation] [varchar](3000),
    [MarketingStrategy] [varchar](3000),
    [SimilarAudiovisualProjects] [varchar](3000),
    [OnlinePlatformsWhereProjectIsAvailable] [varchar](300),
    [OnlinePlatformsAudienceReach] [varchar](300),
    [ProjectAwards] [varchar](300),
    [ProjectPublicNotice] [varchar](300),
    [PreviouslyDevelopedProjects] [varchar](300),
    [AssociatedInstitutions] [varchar](300),
    [ArticleFileUploadDate] [datetimeoffset](7),
    [ArticleFileExtension] [varchar](10),
    [ClippingFileUploadDate] [datetimeoffset](7),
    [ClippingFileExtension] [varchar](10),
    [OtherFileUploadDate] [datetimeoffset](7),
    [OtherFileExtension] [varchar](10),
    [OtherFileDescription] [varchar](400),
    [Links] [varchar](700),
    [TermsAcceptanceDate] [datetimeoffset](7) NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.CreatorProjects] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[CreatorProjects]([Uid])
CREATE TABLE [dbo].[CreatorProjectInterests] (
    [Id] [int] NOT NULL IDENTITY,
    [CreatorProjectId] [int] NOT NULL,
    [InterestId] [int] NOT NULL,
    [AdditionalInfo] [varchar](50),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.CreatorProjectInterests] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_CreatorProjectId] ON [dbo].[CreatorProjectInterests]([CreatorProjectId])
CREATE INDEX [IX_InterestId] ON [dbo].[CreatorProjectInterests]([InterestId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[CreatorProjectInterests]([Uid])
CREATE TABLE [dbo].[Interests] (
    [Id] [int] NOT NULL IDENTITY,
    [InterestGroupId] [int] NOT NULL,
    [DisplayOrder] [int] NOT NULL,
    [HasAdditionalInfo] [bit] NOT NULL,
    [Name] [varchar](150) NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Interests] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_InterestGroupId] ON [dbo].[Interests]([InterestGroupId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Interests]([Uid])
CREATE TABLE [dbo].[AttendeeCollaboratorInterests] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeCollaboratorId] [int] NOT NULL,
    [InterestId] [int] NOT NULL,
    [AdditionalInfo] [varchar](50),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeCollaboratorInterests] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeCollaboratorId] ON [dbo].[AttendeeCollaboratorInterests]([AttendeeCollaboratorId])
CREATE INDEX [IX_InterestId] ON [dbo].[AttendeeCollaboratorInterests]([InterestId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeCollaboratorInterests]([Uid])
CREATE TABLE [dbo].[Editions] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](50),
    [UrlCode] [int] NOT NULL,
    [IsCurrent] [bit] NOT NULL,
    [IsActive] [bit] NOT NULL,
    [StartDate] [datetimeoffset](7) NOT NULL,
    [EndDate] [datetimeoffset](7) NOT NULL,
    [SellStartDate] [datetimeoffset](7) NOT NULL,
    [SellEndDate] [datetimeoffset](7) NOT NULL,
    [OneToOneMeetingsScheduleDate] [datetimeoffset](7) NOT NULL,
    [SpeakersApiHighlightPositionsCount] [int] NOT NULL,
    [ProjectSubmitStartDate] [datetimeoffset](7) NOT NULL,
    [ProjectSubmitEndDate] [datetimeoffset](7) NOT NULL,
    [ProjectEvaluationStartDate] [datetimeoffset](7) NOT NULL,
    [ProjectEvaluationEndDate] [datetimeoffset](7) NOT NULL,
    [NegotiationStartDate] [datetimeoffset](7) NOT NULL,
    [NegotiationEndDate] [datetimeoffset](7) NOT NULL,
    [AudiovisualNegotiationsCreateStartDate] [datetimeoffset](7),
    [AudiovisualNegotiationsCreateEndDate] [datetimeoffset](7),
    [AttendeeOrganizationMaxSellProjectsCount] [int] NOT NULL,
    [ProjectMaxBuyerEvaluationsCount] [int] NOT NULL,
    [AudiovisualNegotiationsVirtualMeetingsJoinMinutes] [smallint] NOT NULL,
    [AudiovisualCommissionEvaluationStartDate] [datetimeoffset](7) NOT NULL,
    [AudiovisualCommissionEvaluationEndDate] [datetimeoffset](7) NOT NULL,
    [AudiovisualCommissionMinimumEvaluationsCount] [int] NOT NULL,
    [AudiovisualCommissionMaximumApprovedProjectsCount] [int] NOT NULL,
    [MusicPitchingIndividualMaxSellProjectsCount] [int] NOT NULL,
    [MusicPitchingEntityMaxSellProjectsCount] [int] NOT NULL,
    [MusicBusinessRoundsMaxSellProjectsCount] [int] NOT NULL,
    [MusicProjectSubmitStartDate] [datetimeoffset](7) NOT NULL,
    [MusicProjectSubmitEndDate] [datetimeoffset](7) NOT NULL,
    [MusicCommissionEvaluationStartDate] [datetimeoffset](7) NOT NULL,
    [MusicCommissionEvaluationEndDate] [datetimeoffset](7) NOT NULL,
    [MusicCommissionMinimumEvaluationsCount] [int] NOT NULL,
    [MusicCommissionMaximumApprovedBandsCount] [int] NOT NULL,
    [InnovationPitchingMaxSellProjectsCount] [int] NOT NULL,
    [InnovationBusinessRoundsMaxSellProjectsCount] [int] NOT NULL,
    [InnovationProjectSubmitStartDate] [datetimeoffset](7) NOT NULL,
    [InnovationProjectSubmitEndDate] [datetimeoffset](7) NOT NULL,
    [InnovationCommissionEvaluationStartDate] [datetimeoffset](7) NOT NULL,
    [InnovationCommissionEvaluationEndDate] [datetimeoffset](7) NOT NULL,
    [InnovationCommissionMinimumEvaluationsCount] [int] NOT NULL,
    [InnovationCommissionMaximumApprovedCompaniesCount] [int] NOT NULL,
    [CartoonProjectSubmitStartDate] [datetimeoffset](7),
    [CartoonProjectSubmitEndDate] [datetimeoffset](7),
    [CartoonCommissionEvaluationStartDate] [datetimeoffset](7),
    [CartoonCommissionEvaluationEndDate] [datetimeoffset](7),
    [CartoonCommissionMinimumEvaluationsCount] [int],
    [CartoonCommissionMaximumApprovedProjectsCount] [int],
    [CreatorProjectSubmitStartDate] [datetimeoffset](7) NOT NULL,
    [CreatorProjectSubmitEndDate] [datetimeoffset](7) NOT NULL,
    [CreatorCommissionEvaluationStartDate] [datetimeoffset](7) NOT NULL,
    [CreatorCommissionEvaluationEndDate] [datetimeoffset](7) NOT NULL,
    [CreatorCommissionMinimumEvaluationsCount] [int] NOT NULL,
    [CreatorCommissionMaximumApprovedProjectsCount] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Editions] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Editions]([Uid])
CREATE TABLE [dbo].[AttendeeSalesPlatforms] (
    [Id] [int] NOT NULL IDENTITY,
    [EditionId] [int] NOT NULL,
    [SalesPlatformId] [int] NOT NULL,
    [SalesPlatformEventid] [varchar](50),
    [IsActive] [bit] NOT NULL,
    [LastSalesPlatformOrderDate] [datetime],
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeSalesPlatforms] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_EditionId] ON [dbo].[AttendeeSalesPlatforms]([EditionId])
CREATE INDEX [IX_SalesPlatformId] ON [dbo].[AttendeeSalesPlatforms]([SalesPlatformId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeSalesPlatforms]([Uid])
CREATE TABLE [dbo].[AttendeeSalesPlatformTicketTypes] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeSalesPlatformId] [int] NOT NULL,
    [CollaboratorTypeId] [int] NOT NULL,
    [TicketClassId] [varchar](30),
    [TicketClassName] [varchar](200),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeSalesPlatformTicketTypes] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeSalesPlatformId] ON [dbo].[AttendeeSalesPlatformTicketTypes]([AttendeeSalesPlatformId])
CREATE INDEX [IX_CollaboratorTypeId] ON [dbo].[AttendeeSalesPlatformTicketTypes]([CollaboratorTypeId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeSalesPlatformTicketTypes]([Uid])
CREATE TABLE [dbo].[AttendeeCollaboratorTickets] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeCollaboratorId] [int] NOT NULL,
    [AttendeeSalesPlatformTicketTypeId] [int] NOT NULL,
    [SalesPlatformAttendeeId] [varchar](40),
    [SalesPlatformUpdateDate] [datetimeoffset](7) NOT NULL,
    [FirstName] [varchar](100),
    [LastNames] [varchar](200),
    [CellPhone] [varchar](50),
    [JobTitle] [varchar](200),
    [Barcode] [varchar](50),
    [IsBarcodePrinted] [bit] NOT NULL,
    [IsBarcodeUsed] [bit] NOT NULL,
    [BarcodeUpdateDate] [datetimeoffset](7),
    [TicketUrl] [varchar](400),
    [IsTicketPrinted] [bit] NOT NULL,
    [IsTicketUsed] [bit] NOT NULL,
    [TicketUpdateDate] [datetimeoffset](7),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeCollaboratorTickets] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeCollaboratorId] ON [dbo].[AttendeeCollaboratorTickets]([AttendeeCollaboratorId])
CREATE INDEX [IX_AttendeeSalesPlatformTicketTypeId] ON [dbo].[AttendeeCollaboratorTickets]([AttendeeSalesPlatformTicketTypeId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeCollaboratorTickets]([Uid])
CREATE TABLE [dbo].[CollaboratorTypes] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](256),
    [Description] [varchar](50),
    [RoleId] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.CollaboratorTypes] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_RoleId] ON [dbo].[CollaboratorTypes]([RoleId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[CollaboratorTypes]([Uid])
CREATE TABLE [dbo].[AttendeeCollaboratorTypes] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeCollaboratorId] [int] NOT NULL,
    [CollaboratorTypeId] [int] NOT NULL,
    [IsApiDisplayEnabled] [bit] NOT NULL,
    [ApiHighlightPosition] [int],
    [TermsAcceptanceDate] [datetimeoffset](7),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeCollaboratorTypes] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeCollaboratorId] ON [dbo].[AttendeeCollaboratorTypes]([AttendeeCollaboratorId])
CREATE INDEX [IX_CollaboratorTypeId] ON [dbo].[AttendeeCollaboratorTypes]([CollaboratorTypeId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeCollaboratorTypes]([Uid])
CREATE TABLE [dbo].[Roles] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](50),
    [Description] [varchar](50),
    CONSTRAINT [PK_dbo.Roles] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[SalesPlatforms] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](100),
    [WebhookSecurityKey] [uniqueidentifier] NOT NULL,
    [ApiKey] [varchar](200),
    [ApiSecret] [varchar](200),
    [MaxProcessingCount] [int] NOT NULL,
    [SecurityStamp] [varchar](50),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.SalesPlatforms] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[SalesPlatforms]([Uid])
CREATE TABLE [dbo].[EditionEvents] (
    [Id] [int] NOT NULL IDENTITY,
    [EditionId] [int] NOT NULL,
    [Name] [varchar](50),
    [StartDate] [datetimeoffset](7) NOT NULL,
    [EndDate] [datetimeoffset](7) NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.EditionEvents] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_EditionId] ON [dbo].[EditionEvents]([EditionId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[EditionEvents]([Uid])
CREATE TABLE [dbo].[Conferences] (
    [Id] [int] NOT NULL IDENTITY,
    [EditionEventId] [int] NOT NULL,
    [RoomId] [int] NOT NULL,
    [StartDate] [datetimeoffset](7) NOT NULL,
    [EndDate] [datetimeoffset](7) NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Conferences] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_EditionEventId] ON [dbo].[Conferences]([EditionEventId])
CREATE INDEX [IX_RoomId] ON [dbo].[Conferences]([RoomId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Conferences]([Uid])
CREATE TABLE [dbo].[ConferenceDynamics] (
    [Id] [int] NOT NULL IDENTITY,
    [ConferenceId] [int] NOT NULL,
    [LanguageId] [int] NOT NULL,
    [Value] [varchar](1000),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ConferenceDynamics] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ConferenceId] ON [dbo].[ConferenceDynamics]([ConferenceId])
CREATE INDEX [IX_LanguageId] ON [dbo].[ConferenceDynamics]([LanguageId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ConferenceDynamics]([Uid])
CREATE TABLE [dbo].[Languages] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](50) NOT NULL,
    [Code] [varchar](50) NOT NULL,
    [IsDefault] [bit] NOT NULL,
    [IsActive] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Languages] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Languages]([Uid])
CREATE TABLE [dbo].[ConferenceParticipants] (
    [Id] [int] NOT NULL IDENTITY,
    [ConferenceId] [int] NOT NULL,
    [AttendeeCollaboratorId] [int] NOT NULL,
    [ConferenceParticipantRoleId] [int] NOT NULL,
    [IsPreRegistered] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ConferenceParticipants] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ConferenceId] ON [dbo].[ConferenceParticipants]([ConferenceId])
CREATE INDEX [IX_AttendeeCollaboratorId] ON [dbo].[ConferenceParticipants]([AttendeeCollaboratorId])
CREATE INDEX [IX_ConferenceParticipantRoleId] ON [dbo].[ConferenceParticipants]([ConferenceParticipantRoleId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ConferenceParticipants]([Uid])
CREATE TABLE [dbo].[ConferenceParticipantRoles] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](80),
    [IsLecturer] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ConferenceParticipantRoles] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ConferenceParticipantRoles]([Uid])
CREATE TABLE [dbo].[ConferenceParticipantRoleTitles] (
    [Id] [int] NOT NULL IDENTITY,
    [ConferenceParticipantRoleId] [int] NOT NULL,
    [LanguageId] [int] NOT NULL,
    [Value] [varchar](256),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ConferenceParticipantRoleTitles] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ConferenceParticipantRoleId] ON [dbo].[ConferenceParticipantRoleTitles]([ConferenceParticipantRoleId])
CREATE INDEX [IX_LanguageId] ON [dbo].[ConferenceParticipantRoleTitles]([LanguageId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ConferenceParticipantRoleTitles]([Uid])
CREATE TABLE [dbo].[ConferencePillars] (
    [Id] [int] NOT NULL IDENTITY,
    [ConferenceId] [int] NOT NULL,
    [PillarId] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ConferencePillars] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ConferenceId] ON [dbo].[ConferencePillars]([ConferenceId])
CREATE INDEX [IX_PillarId] ON [dbo].[ConferencePillars]([PillarId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ConferencePillars]([Uid])
CREATE TABLE [dbo].[Pillars] (
    [Id] [int] NOT NULL IDENTITY,
    [EditionId] [int] NOT NULL,
    [Name] [varchar](600),
    [Color] [varchar](10),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Pillars] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_EditionId] ON [dbo].[Pillars]([EditionId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Pillars]([Uid])
CREATE TABLE [dbo].[ConferencePresentationFormats] (
    [Id] [int] NOT NULL IDENTITY,
    [ConferenceId] [int] NOT NULL,
    [PresentationFormatId] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ConferencePresentationFormats] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ConferenceId] ON [dbo].[ConferencePresentationFormats]([ConferenceId])
CREATE INDEX [IX_PresentationFormatId] ON [dbo].[ConferencePresentationFormats]([PresentationFormatId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ConferencePresentationFormats]([Uid])
CREATE TABLE [dbo].[PresentationFormats] (
    [Id] [int] NOT NULL IDENTITY,
    [EditionId] [int] NOT NULL,
    [Name] [varchar](500),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.PresentationFormats] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_EditionId] ON [dbo].[PresentationFormats]([EditionId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[PresentationFormats]([Uid])
CREATE TABLE [dbo].[ConferenceSynopsis] (
    [Id] [int] NOT NULL IDENTITY,
    [ConferenceId] [int] NOT NULL,
    [LanguageId] [int] NOT NULL,
    [Value] [varchar](1000),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ConferenceSynopsis] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ConferenceId] ON [dbo].[ConferenceSynopsis]([ConferenceId])
CREATE INDEX [IX_LanguageId] ON [dbo].[ConferenceSynopsis]([LanguageId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ConferenceSynopsis]([Uid])
CREATE TABLE [dbo].[ConferenceTitles] (
    [Id] [int] NOT NULL IDENTITY,
    [ConferenceId] [int] NOT NULL,
    [LanguageId] [int] NOT NULL,
    [Value] [varchar](200),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ConferenceTitles] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ConferenceId] ON [dbo].[ConferenceTitles]([ConferenceId])
CREATE INDEX [IX_LanguageId] ON [dbo].[ConferenceTitles]([LanguageId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ConferenceTitles]([Uid])
CREATE TABLE [dbo].[ConferenceTracks] (
    [Id] [int] NOT NULL IDENTITY,
    [ConferenceId] [int] NOT NULL,
    [TrackId] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ConferenceTracks] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ConferenceId] ON [dbo].[ConferenceTracks]([ConferenceId])
CREATE INDEX [IX_TrackId] ON [dbo].[ConferenceTracks]([TrackId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ConferenceTracks]([Uid])
CREATE TABLE [dbo].[Tracks] (
    [Id] [int] NOT NULL IDENTITY,
    [EditionId] [int] NOT NULL,
    [Name] [varchar](600),
    [Color] [varchar](10),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Tracks] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_EditionId] ON [dbo].[Tracks]([EditionId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Tracks]([Uid])
CREATE TABLE [dbo].[Rooms] (
    [Id] [int] NOT NULL IDENTITY,
    [EditionId] [int] NOT NULL,
    [IsVirtualMeeting] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Rooms] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_EditionId] ON [dbo].[Rooms]([EditionId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Rooms]([Uid])
CREATE TABLE [dbo].[NegotiationRoomConfigs] (
    [Id] [int] NOT NULL IDENTITY,
    [RoomId] [int] NOT NULL,
    [NegotiationConfigId] [int] NOT NULL,
    [CountAutomaticTables] [int] NOT NULL,
    [CountManualTables] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.NegotiationRoomConfigs] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_RoomId] ON [dbo].[NegotiationRoomConfigs]([RoomId])
CREATE INDEX [IX_NegotiationConfigId] ON [dbo].[NegotiationRoomConfigs]([NegotiationConfigId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[NegotiationRoomConfigs]([Uid])
CREATE TABLE [dbo].[NegotiationConfigs] (
    [Id] [int] NOT NULL IDENTITY,
    [EditionId] [int] NOT NULL,
    [StartDate] [datetimeoffset](7) NOT NULL,
    [EndDate] [datetimeoffset](7) NOT NULL,
    [RoundFirstTurn] [int] NOT NULL,
    [RoundSecondTurn] [int] NOT NULL,
    [TimeIntervalBetweenTurn] [time](7) NOT NULL,
    [TimeOfEachRound] [time](7) NOT NULL,
    [TimeIntervalBetweenRound] [time](7) NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.NegotiationConfigs] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_EditionId] ON [dbo].[NegotiationConfigs]([EditionId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[NegotiationConfigs]([Uid])
CREATE TABLE [dbo].[RoomNames] (
    [Id] [int] NOT NULL IDENTITY,
    [RoomId] [int] NOT NULL,
    [LanguageId] [int] NOT NULL,
    [Value] [varchar](256),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.RoomNames] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_RoomId] ON [dbo].[RoomNames]([RoomId])
CREATE INDEX [IX_LanguageId] ON [dbo].[RoomNames]([LanguageId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[RoomNames]([Uid])
CREATE TABLE [dbo].[AttendeeInnovationOrganizationEvaluations] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeInnovationOrganizationId] [int] NOT NULL,
    [EvaluatorUserId] [int] NOT NULL,
    [Grade] [decimal](18, 2),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeInnovationOrganizationEvaluations] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeInnovationOrganizationId] ON [dbo].[AttendeeInnovationOrganizationEvaluations]([AttendeeInnovationOrganizationId])
CREATE INDEX [IX_EvaluatorUserId] ON [dbo].[AttendeeInnovationOrganizationEvaluations]([EvaluatorUserId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeInnovationOrganizationEvaluations]([Uid])
CREATE TABLE [dbo].[AttendeeInnovationOrganizations] (
    [Id] [int] NOT NULL IDENTITY,
    [EditionId] [int] NOT NULL,
    [InnovationOrganizationId] [int] NOT NULL,
    [EvaluationEmailSendDate] [datetimeoffset](7),
    [Grade] [decimal](18, 2),
    [EvaluationsCount] [int] NOT NULL,
    [LastEvaluationDate] [datetimeoffset](7),
    [AccumulatedRevenue] [decimal](18, 2) NOT NULL,
    [MarketSize] [varchar](300),
    [BusinessDefinition] [varchar](300),
    [BusinessFocus] [varchar](300),
    [BusinessEconomicModel] [varchar](300),
    [BusinessDifferentials] [varchar](300),
    [BusinessStage] [varchar](300),
    [PresentationUploadDate] [datetimeoffset](7),
    [BusinessOperationalModel] [varchar](300),
    [VideoUrl] [varchar](300),
    [PresentationFileExtension] [varchar](50),
    [WouldYouLikeParticipateBusinessRound] [bit] NOT NULL,
    [WouldYouLikeParticipatePitching] [bit] NOT NULL,
    [AccumulatedRevenueForLastTwelveMonths] [decimal](18, 2),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeInnovationOrganizations] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_EditionId] ON [dbo].[AttendeeInnovationOrganizations]([EditionId])
CREATE INDEX [IX_InnovationOrganizationId] ON [dbo].[AttendeeInnovationOrganizations]([InnovationOrganizationId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeInnovationOrganizations]([Uid])
CREATE TABLE [dbo].[AttendeeInnovationOrganizationCollaborators] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeInnovationOrganizationId] [int] NOT NULL,
    [AttendeeCollaboratorId] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeInnovationOrganizationCollaborators] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeInnovationOrganizationId] ON [dbo].[AttendeeInnovationOrganizationCollaborators]([AttendeeInnovationOrganizationId])
CREATE INDEX [IX_AttendeeCollaboratorId] ON [dbo].[AttendeeInnovationOrganizationCollaborators]([AttendeeCollaboratorId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeInnovationOrganizationCollaborators]([Uid])
CREATE TABLE [dbo].[AttendeeInnovationOrganizationCompetitors] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeInnovationOrganizationId] [int] NOT NULL,
    [Name] [varchar](300),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeInnovationOrganizationCompetitors] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeInnovationOrganizationId] ON [dbo].[AttendeeInnovationOrganizationCompetitors]([AttendeeInnovationOrganizationId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeInnovationOrganizationCompetitors]([Uid])
CREATE TABLE [dbo].[AttendeeInnovationOrganizationExperiences] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeInnovationOrganizationId] [int] NOT NULL,
    [InnovationOrganizationExperienceOptionId] [int] NOT NULL,
    [AdditionalInfo] [varchar](50),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeInnovationOrganizationExperiences] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeInnovationOrganizationId] ON [dbo].[AttendeeInnovationOrganizationExperiences]([AttendeeInnovationOrganizationId])
CREATE INDEX [IX_InnovationOrganizationExperienceOptionId] ON [dbo].[AttendeeInnovationOrganizationExperiences]([InnovationOrganizationExperienceOptionId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeInnovationOrganizationExperiences]([Uid])
CREATE TABLE [dbo].[InnovationOrganizationExperienceOptions] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](50),
    [DisplayOrder] [int] NOT NULL,
    [HasAdditionalInfo] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.InnovationOrganizationExperienceOptions] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[InnovationOrganizationExperienceOptions]([Uid])
CREATE TABLE [dbo].[AttendeeInnovationOrganizationFounders] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeInnovationOrganizationId] [int] NOT NULL,
    [Fullname] [varchar](200),
    [Curriculum] [varchar](710),
    [WorkDedicationId] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeInnovationOrganizationFounders] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeInnovationOrganizationId] ON [dbo].[AttendeeInnovationOrganizationFounders]([AttendeeInnovationOrganizationId])
CREATE INDEX [IX_WorkDedicationId] ON [dbo].[AttendeeInnovationOrganizationFounders]([WorkDedicationId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeInnovationOrganizationFounders]([Uid])
CREATE TABLE [dbo].[WorkDedications] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](50),
    [DisplayOrder] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.WorkDedications] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[WorkDedications]([Uid])
CREATE TABLE [dbo].[AttendeeInnovationOrganizationObjectives] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeInnovationOrganiaztionId] [int] NOT NULL,
    [InnovationOrganizationObjectiveOptionId] [int] NOT NULL,
    [AdditionalInfo] [varchar](50),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeInnovationOrganizationObjectives] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeInnovationOrganiaztionId] ON [dbo].[AttendeeInnovationOrganizationObjectives]([AttendeeInnovationOrganiaztionId])
CREATE INDEX [IX_InnovationOrganizationObjectiveOptionId] ON [dbo].[AttendeeInnovationOrganizationObjectives]([InnovationOrganizationObjectiveOptionId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeInnovationOrganizationObjectives]([Uid])
CREATE TABLE [dbo].[InnovationOrganizationObjectivesOptions] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](50),
    [DisplayOrder] [int] NOT NULL,
    [HasAdditionalInfo] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.InnovationOrganizationObjectivesOptions] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[InnovationOrganizationObjectivesOptions]([Uid])
CREATE TABLE [dbo].[AttendeeInnovationOrganizationSustainableDevelopmentObjectives] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeInnovationOrganizationId] [int] NOT NULL,
    [InnovationOrganizationSustainableDevelopmentObjectiveOptionId] [int] NOT NULL,
    [AdditionalInfo] [varchar](50),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeInnovationOrganizationSustainableDevelopmentObjectives] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeInnovationOrganizationId] ON [dbo].[AttendeeInnovationOrganizationSustainableDevelopmentObjectives]([AttendeeInnovationOrganizationId])
CREATE INDEX [IX_InnovationOrganizationSustainableDevelopmentObjectiveOptionId] ON [dbo].[AttendeeInnovationOrganizationSustainableDevelopmentObjectives]([InnovationOrganizationSustainableDevelopmentObjectiveOptionId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeInnovationOrganizationSustainableDevelopmentObjectives]([Uid])
CREATE TABLE [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](50),
    [Description] [varchar](50),
    [DisplayOrder] [int] NOT NULL,
    [HasAdditionalInfo] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.InnovationOrganizationSustainableDevelopmentObjectivesOptions] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions]([Uid])
CREATE TABLE [dbo].[AttendeeInnovationOrganizationTechnologies] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeInnovationOrganizationId] [int] NOT NULL,
    [InnovationOrganizationTechnologyOptionId] [int] NOT NULL,
    [AdditionalInfo] [varchar](50),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeInnovationOrganizationTechnologies] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeInnovationOrganizationId] ON [dbo].[AttendeeInnovationOrganizationTechnologies]([AttendeeInnovationOrganizationId])
CREATE INDEX [IX_InnovationOrganizationTechnologyOptionId] ON [dbo].[AttendeeInnovationOrganizationTechnologies]([InnovationOrganizationTechnologyOptionId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeInnovationOrganizationTechnologies]([Uid])
CREATE TABLE [dbo].[InnovationOrganizationTechnologyOptions] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](50),
    [DisplayOrder] [int] NOT NULL,
    [HasAdditionalInfo] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.InnovationOrganizationTechnologyOptions] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[InnovationOrganizationTechnologyOptions]([Uid])
CREATE TABLE [dbo].[AttendeeInnovationOrganizationTracks] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeInnovationOrganizationId] [int] NOT NULL,
    [InnovationOrganizationTrackOptionId] [int] NOT NULL,
    [AdditionalInfo] [varchar](50),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeInnovationOrganizationTracks] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeInnovationOrganizationId] ON [dbo].[AttendeeInnovationOrganizationTracks]([AttendeeInnovationOrganizationId])
CREATE INDEX [IX_InnovationOrganizationTrackOptionId] ON [dbo].[AttendeeInnovationOrganizationTracks]([InnovationOrganizationTrackOptionId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeInnovationOrganizationTracks]([Uid])
CREATE TABLE [dbo].[InnovationOrganizationTrackOptions] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](50),
    [Description] [varchar](50),
    [DisplayOrder] [int] NOT NULL,
    [HasAdditionalInfo] [bit] NOT NULL,
    [IsActive] [bit] NOT NULL,
    [InnovationOrganizationTrackOptionGroupId] [int],
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.InnovationOrganizationTrackOptions] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_InnovationOrganizationTrackOptionGroupId] ON [dbo].[InnovationOrganizationTrackOptions]([InnovationOrganizationTrackOptionGroupId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[InnovationOrganizationTrackOptions]([Uid])
CREATE TABLE [dbo].[AttendeeCollaboratorInnovationOrganizationTracks] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeCollaboratorId] [int] NOT NULL,
    [InnovationOrganizationTrackOptionId] [int] NOT NULL,
    [AdditionalInfo] [varchar](50),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeCollaboratorInnovationOrganizationTracks] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeCollaboratorId] ON [dbo].[AttendeeCollaboratorInnovationOrganizationTracks]([AttendeeCollaboratorId])
CREATE INDEX [IX_InnovationOrganizationTrackOptionId] ON [dbo].[AttendeeCollaboratorInnovationOrganizationTracks]([InnovationOrganizationTrackOptionId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeCollaboratorInnovationOrganizationTracks]([Uid])
CREATE TABLE [dbo].[InnovationOrganizationTrackOptionGroups] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](50),
    [DisplayOrder] [int] NOT NULL,
    [IsActive] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.InnovationOrganizationTrackOptionGroups] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[InnovationOrganizationTrackOptionGroups]([Uid])
CREATE TABLE [dbo].[InnovationOrganizations] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](300),
    [Document] [varchar](50),
    [ServiceName] [varchar](300),
    [Description] [varchar](600),
    [Website] [varchar](300),
    [ImageUploadDate] [datetimeoffset](7),
    [FoundationYear] [int],
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.InnovationOrganizations] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[InnovationOrganizations]([Uid])
CREATE TABLE [dbo].[AttendeeMusicBandEvaluations] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeMusicBandId] [int] NOT NULL,
    [EvaluatorUserId] [int] NOT NULL,
    [Grade] [decimal](18, 2) NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeMusicBandEvaluations] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeMusicBandId] ON [dbo].[AttendeeMusicBandEvaluations]([AttendeeMusicBandId])
CREATE INDEX [IX_EvaluatorUserId] ON [dbo].[AttendeeMusicBandEvaluations]([EvaluatorUserId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeMusicBandEvaluations]([Uid])
CREATE TABLE [dbo].[AttendeeMusicBands] (
    [Id] [int] NOT NULL IDENTITY,
    [EditionId] [int] NOT NULL,
    [MusicBandId] [int] NOT NULL,
    [Grade] [decimal](18, 2),
    [EvaluationsCount] [int] NOT NULL,
    [LastEvaluationDate] [datetimeoffset](7),
    [EvaluationEmailSendDate] [datetimeoffset](7),
    [WouldYouLikeParticipateBusinessRound] [bit] NOT NULL,
    [WouldYouLikeParticipatePitching] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeMusicBands] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_EditionId] ON [dbo].[AttendeeMusicBands]([EditionId])
CREATE INDEX [IX_MusicBandId] ON [dbo].[AttendeeMusicBands]([MusicBandId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeMusicBands]([Uid])
CREATE TABLE [dbo].[AttendeeMusicBandCollaborators] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeMusicBandId] [int] NOT NULL,
    [AttendeeCollaboratorId] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeMusicBandCollaborators] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeMusicBandId] ON [dbo].[AttendeeMusicBandCollaborators]([AttendeeMusicBandId])
CREATE INDEX [IX_AttendeeCollaboratorId] ON [dbo].[AttendeeMusicBandCollaborators]([AttendeeCollaboratorId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeMusicBandCollaborators]([Uid])
CREATE TABLE [dbo].[MusicBands] (
    [Id] [int] NOT NULL IDENTITY,
    [MusicBandTypeId] [int] NOT NULL,
    [Name] [varchar](300),
    [FormationDate] [varchar](300),
    [MainMusicInfluences] [varchar](600),
    [Facebook] [varchar](300),
    [Instagram] [varchar](300),
    [Twitter] [varchar](300),
    [Youtube] [varchar](300),
    [Tiktok] [varchar](300),
    [ImageUploadDate] [datetimeoffset](7),
    [ImageUrl] [varchar](50),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.MusicBands] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_MusicBandTypeId] ON [dbo].[MusicBands]([MusicBandTypeId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[MusicBands]([Uid])
CREATE TABLE [dbo].[MusicBandGenres] (
    [Id] [int] NOT NULL IDENTITY,
    [MusicBandId] [int] NOT NULL,
    [MusicGenreId] [int] NOT NULL,
    [AdditionalInfo] [varchar](200),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.MusicBandGenres] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_MusicBandId] ON [dbo].[MusicBandGenres]([MusicBandId])
CREATE INDEX [IX_MusicGenreId] ON [dbo].[MusicBandGenres]([MusicGenreId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[MusicBandGenres]([Uid])
CREATE TABLE [dbo].[MusicGenres] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](100),
    [DisplayOrder] [int] NOT NULL,
    [HasAdditionalInfo] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.MusicGenres] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[MusicGenres]([Uid])
CREATE TABLE [dbo].[MusicBandMembers] (
    [Id] [int] NOT NULL IDENTITY,
    [MusicBandId] [int] NOT NULL,
    [Name] [varchar](300),
    [MusicInstrumentName] [varchar](100),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.MusicBandMembers] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_MusicBandId] ON [dbo].[MusicBandMembers]([MusicBandId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[MusicBandMembers]([Uid])
CREATE TABLE [dbo].[MusicBandTargetAudiences] (
    [Id] [int] NOT NULL IDENTITY,
    [MusicBandId] [int] NOT NULL,
    [TargetAudienceId] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.MusicBandTargetAudiences] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_MusicBandId] ON [dbo].[MusicBandTargetAudiences]([MusicBandId])
CREATE INDEX [IX_TargetAudienceId] ON [dbo].[MusicBandTargetAudiences]([TargetAudienceId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[MusicBandTargetAudiences]([Uid])
CREATE TABLE [dbo].[TargetAudiences] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](50),
    [DisplayOrder] [int] NOT NULL,
    [ProjectTypeId] [int] NOT NULL,
    [HasAdditionalInfo] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.TargetAudiences] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ProjectTypeId] ON [dbo].[TargetAudiences]([ProjectTypeId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[TargetAudiences]([Uid])
CREATE TABLE [dbo].[MusicBandTeamMembers] (
    [Id] [int] NOT NULL IDENTITY,
    [MusicBandId] [int] NOT NULL,
    [Name] [varchar](300),
    [Role] [varchar](300),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.MusicBandTeamMembers] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_MusicBandId] ON [dbo].[MusicBandTeamMembers]([MusicBandId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[MusicBandTeamMembers]([Uid])
CREATE TABLE [dbo].[MusicBandTypes] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](100),
    [DisplayOrder] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.MusicBandTypes] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[MusicBandTypes]([Uid])
CREATE TABLE [dbo].[ReleasedMusicProjects] (
    [Id] [int] NOT NULL IDENTITY,
    [MusicBandId] [int] NOT NULL,
    [Name] [varchar](200),
    [Year] [varchar](300),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ReleasedMusicProjects] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_MusicBandId] ON [dbo].[ReleasedMusicProjects]([MusicBandId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ReleasedMusicProjects]([Uid])
CREATE TABLE [dbo].[MusicProjects] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeMusicBandId] [int] NOT NULL,
    [VideoUrl] [varchar](300),
    [VideoUrlPassword] [varchar](100),
    [Music1Url] [varchar](300),
    [Music2Url] [varchar](300),
    [Release] [varchar](8000),
    [Clipping1] [varchar](5000),
    [Clipping2] [varchar](5000),
    [Clipping3] [varchar](5000),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.MusicProjects] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeMusicBandId] ON [dbo].[MusicProjects]([AttendeeMusicBandId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[MusicProjects]([Uid])
CREATE TABLE [dbo].[Collaborators] (
    [Id] [int] NOT NULL,
    [FirstName] [varchar](100) NOT NULL,
    [LastNames] [varchar](200),
    [Document] [varchar](100),
    [Badge] [varchar](50),
    [PhoneNumber] [varchar](50),
    [CellPhone] [varchar](50),
    [PublicEmail] [varchar](50),
    [Website] [varchar](300),
    [Linkedin] [varchar](100),
    [Twitter] [varchar](100),
    [Instagram] [varchar](100),
    [Youtube] [varchar](300),
    [AddressId] [int],
    [ImageUploadDate] [datetimeoffset](7),
    [BirthDate] [datetime],
    [CollaboratorGenderId] [int],
    [CollaboratorGenderAdditionalInfo] [varchar](300),
    [CollaboratorRoleId] [int],
    [CollaboratorRoleAdditionalInfo] [varchar](300),
    [CollaboratorIndustryId] [int],
    [CollaboratorIndustryAdditionalInfo] [varchar](300),
    [HasAnySpecialNeeds] [bit],
    [SpecialNeedsDescription] [varchar](300),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Collaborators] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_Id] ON [dbo].[Collaborators]([Id])
CREATE INDEX [IX_AddressId] ON [dbo].[Collaborators]([AddressId])
CREATE INDEX [IX_CollaboratorGenderId] ON [dbo].[Collaborators]([CollaboratorGenderId])
CREATE INDEX [IX_CollaboratorRoleId] ON [dbo].[Collaborators]([CollaboratorRoleId])
CREATE INDEX [IX_CollaboratorIndustryId] ON [dbo].[Collaborators]([CollaboratorIndustryId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Collaborators]([Uid])
CREATE INDEX [IX_CreateUserId] ON [dbo].[Collaborators]([CreateUserId])
CREATE INDEX [IX_UpdateUserId] ON [dbo].[Collaborators]([UpdateUserId])
CREATE TABLE [dbo].[Addresses] (
    [Id] [int] NOT NULL IDENTITY,
    [CountryId] [int],
    [StateId] [int],
    [CityId] [int],
    [Address1] [varchar](500),
    [ZipCode] [varchar](10),
    [IsManual] [bit] NOT NULL,
    [Latitude] [decimal](18, 2),
    [Longitude] [decimal](18, 2),
    [IsGeoLocationUpdated] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Addresses] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_CountryId] ON [dbo].[Addresses]([CountryId])
CREATE INDEX [IX_StateId] ON [dbo].[Addresses]([StateId])
CREATE INDEX [IX_CityId] ON [dbo].[Addresses]([CityId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Addresses]([Uid])
CREATE TABLE [dbo].[Cities] (
    [Id] [int] NOT NULL IDENTITY,
    [StateId] [int] NOT NULL,
    [Name] [varchar](100),
    [IsManual] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Cities] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_StateId] ON [dbo].[Cities]([StateId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Cities]([Uid])
CREATE TABLE [dbo].[States] (
    [Id] [int] NOT NULL IDENTITY,
    [CountryId] [int] NOT NULL,
    [Name] [varchar](100),
    [Code] [varchar](2),
    [IsManual] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.States] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_CountryId] ON [dbo].[States]([CountryId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[States]([Uid])
CREATE TABLE [dbo].[Countries] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](100),
    [Code] [varchar](3),
    [CompanyNumberMask] [varchar](50),
    [ZipCodeMask] [varchar](50),
    [PhoneNumberMask] [varchar](50),
    [MobileMask] [varchar](50),
    [IsManual] [bit] NOT NULL,
    [IsCompanyNumberRequired] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Countries] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Countries]([Uid])
CREATE TABLE [dbo].[Organizations] (
    [Id] [int] NOT NULL IDENTITY,
    [HoldingId] [int],
    [Name] [varchar](81) NOT NULL,
    [CompanyName] [varchar](100),
    [TradeName] [varchar](100),
    [Document] [varchar](50),
    [PhoneNumber] [varchar](50),
    [Website] [varchar](300),
    [Linkedin] [varchar](100),
    [Twitter] [varchar](100),
    [Instagram] [varchar](100),
    [Youtube] [varchar](300),
    [AddressId] [int],
    [ImageUploadDate] [datetimeoffset](7),
    [IsVirtualMeeting] [bit],
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Organizations] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_HoldingId] ON [dbo].[Organizations]([HoldingId])
CREATE INDEX [IX_AddressId] ON [dbo].[Organizations]([AddressId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Organizations]([Uid])
CREATE INDEX [IX_UpdateUserId] ON [dbo].[Organizations]([UpdateUserId])
CREATE TABLE [dbo].[Holdings] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](81) NOT NULL,
    [ImageUploadDate] [datetimeoffset](7),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Holdings] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Holdings]([Uid])
CREATE INDEX [IX_UpdateUserId] ON [dbo].[Holdings]([UpdateUserId])
CREATE TABLE [dbo].[HoldingDescriptions] (
    [Id] [int] NOT NULL IDENTITY,
    [HoldingId] [int] NOT NULL,
    [LanguageId] [int] NOT NULL,
    [Value] [varchar](8000),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.HoldingDescriptions] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_HoldingId] ON [dbo].[HoldingDescriptions]([HoldingId])
CREATE INDEX [IX_LanguageId] ON [dbo].[HoldingDescriptions]([LanguageId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[HoldingDescriptions]([Uid])
CREATE TABLE [dbo].[OrganizationActivities] (
    [Id] [int] NOT NULL IDENTITY,
    [OrganizationId] [int] NOT NULL,
    [ActivityId] [int] NOT NULL,
    [AdditionalInfo] [varchar](200),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.OrganizationActivities] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_OrganizationId] ON [dbo].[OrganizationActivities]([OrganizationId])
CREATE INDEX [IX_ActivityId] ON [dbo].[OrganizationActivities]([ActivityId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[OrganizationActivities]([Uid])
CREATE TABLE [dbo].[OrganizationDescriptions] (
    [Id] [int] NOT NULL IDENTITY,
    [OrganizationId] [int] NOT NULL,
    [LanguageId] [int] NOT NULL,
    [Value] [varchar](8000),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.OrganizationDescriptions] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_OrganizationId] ON [dbo].[OrganizationDescriptions]([OrganizationId])
CREATE INDEX [IX_LanguageId] ON [dbo].[OrganizationDescriptions]([LanguageId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[OrganizationDescriptions]([Uid])
CREATE TABLE [dbo].[OrganizationInterests] (
    [Id] [int] NOT NULL IDENTITY,
    [OrganizationId] [int] NOT NULL,
    [InterestId] [int] NOT NULL,
    [AdditionalInfo] [varchar](200),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.OrganizationInterests] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_OrganizationId] ON [dbo].[OrganizationInterests]([OrganizationId])
CREATE INDEX [IX_InterestId] ON [dbo].[OrganizationInterests]([InterestId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[OrganizationInterests]([Uid])
CREATE TABLE [dbo].[OrganizationRestrictionSpecifics] (
    [Id] [int] NOT NULL IDENTITY,
    [OrganizationId] [int] NOT NULL,
    [LanguageId] [int] NOT NULL,
    [Value] [varchar](8000),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.OrganizationRestrictionSpecifics] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_OrganizationId] ON [dbo].[OrganizationRestrictionSpecifics]([OrganizationId])
CREATE INDEX [IX_LanguageId] ON [dbo].[OrganizationRestrictionSpecifics]([LanguageId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[OrganizationRestrictionSpecifics]([Uid])
CREATE TABLE [dbo].[OrganizationTargetAudiences] (
    [Id] [int] NOT NULL IDENTITY,
    [OrganizationId] [int] NOT NULL,
    [TargetAudienceId] [int] NOT NULL,
    [AdditionalInfo] [varchar](50),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.OrganizationTargetAudiences] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_OrganizationId] ON [dbo].[OrganizationTargetAudiences]([OrganizationId])
CREATE INDEX [IX_TargetAudienceId] ON [dbo].[OrganizationTargetAudiences]([TargetAudienceId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[OrganizationTargetAudiences]([Uid])
CREATE TABLE [dbo].[CollaboratorEditionParticipations] (
    [Id] [int] NOT NULL IDENTITY,
    [CollaboratorId] [int] NOT NULL,
    [EditionId] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.CollaboratorEditionParticipations] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_CollaboratorId] ON [dbo].[CollaboratorEditionParticipations]([CollaboratorId])
CREATE INDEX [IX_EditionId] ON [dbo].[CollaboratorEditionParticipations]([EditionId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[CollaboratorEditionParticipations]([Uid])
CREATE TABLE [dbo].[CollaboratorGenders] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](300),
    [HasAdditionalInfo] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.CollaboratorGenders] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[CollaboratorGenders]([Uid])
CREATE TABLE [dbo].[CollaboratorIndustries] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](300),
    [HasAdditionalInfo] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.CollaboratorIndustries] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[CollaboratorIndustries]([Uid])
CREATE TABLE [dbo].[CollaboratorJobTitles] (
    [Id] [int] NOT NULL IDENTITY,
    [CollaboratorId] [int] NOT NULL,
    [LanguageId] [int] NOT NULL,
    [Value] [varchar](256),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.CollaboratorJobTitles] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_CollaboratorId] ON [dbo].[CollaboratorJobTitles]([CollaboratorId])
CREATE INDEX [IX_LanguageId] ON [dbo].[CollaboratorJobTitles]([LanguageId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[CollaboratorJobTitles]([Uid])
CREATE TABLE [dbo].[CollaboratorMiniBios] (
    [Id] [int] NOT NULL IDENTITY,
    [CollaboratorId] [int] NOT NULL,
    [LanguageId] [int] NOT NULL,
    [Value] [varchar](8000),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.CollaboratorMiniBios] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_CollaboratorId] ON [dbo].[CollaboratorMiniBios]([CollaboratorId])
CREATE INDEX [IX_LanguageId] ON [dbo].[CollaboratorMiniBios]([LanguageId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[CollaboratorMiniBios]([Uid])
CREATE TABLE [dbo].[CollaboratorRoles] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](300),
    [HasAdditionalInfo] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.CollaboratorRoles] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[CollaboratorRoles]([Uid])
CREATE TABLE [dbo].[CommissionEvaluations] (
    [Id] [int] NOT NULL IDENTITY,
    [ProjectId] [int] NOT NULL,
    [EvaluatorUserId] [int] NOT NULL,
    [Grade] [decimal](18, 2),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.CommissionEvaluations] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ProjectId] ON [dbo].[CommissionEvaluations]([ProjectId])
CREATE INDEX [IX_EvaluatorUserId] ON [dbo].[CommissionEvaluations]([EvaluatorUserId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[CommissionEvaluations]([Uid])
CREATE TABLE [dbo].[Projects] (
    [Id] [int] NOT NULL IDENTITY,
    [ProjectTypeId] [int] NOT NULL,
    [SellerAttendeeOrganizationId] [int] NOT NULL,
    [TotalPlayingTime] [varchar](10) NOT NULL,
    [NumberOfEpisodes] [int],
    [EachEpisodePlayingTime] [varchar](10),
    [ValuePerEpisode] [varchar](50),
    [TotalValueOfProject] [varchar](50),
    [ValueAlreadyRaised] [varchar](50),
    [ValueStillNeeded] [varchar](50),
    [IsPitching] [bit] NOT NULL,
    [FinishDate] [datetimeoffset](7),
    [ProjectBuyerEvaluationsCount] [int] NOT NULL,
    [CommissionEvaluationsCount] [int] NOT NULL,
    [CommissionGrade] [decimal](18, 2),
    [LastCommissionEvaluationDate] [datetimeoffset](7),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Projects] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ProjectTypeId] ON [dbo].[Projects]([ProjectTypeId])
CREATE INDEX [IX_SellerAttendeeOrganizationId] ON [dbo].[Projects]([SellerAttendeeOrganizationId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Projects]([Uid])
CREATE TABLE [dbo].[ProjectAdditionalInformations] (
    [Id] [int] NOT NULL IDENTITY,
    [ProjectId] [int] NOT NULL,
    [LanguageId] [int] NOT NULL,
    [Value] [nvarchar](max),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ProjectAdditionalInformations] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ProjectId] ON [dbo].[ProjectAdditionalInformations]([ProjectId])
CREATE INDEX [IX_LanguageId] ON [dbo].[ProjectAdditionalInformations]([LanguageId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ProjectAdditionalInformations]([Uid])
CREATE TABLE [dbo].[ProjectBuyerEvaluations] (
    [Id] [int] NOT NULL IDENTITY,
    [ProjectId] [int] NOT NULL,
    [BuyerAttendeeOrganizationId] [int] NOT NULL,
    [ProjectEvaluationStatusId] [int] NOT NULL,
    [ProjectEvaluationRefuseReasonId] [int],
    [Reason] [varchar](500),
    [SellerUserId] [int] NOT NULL,
    [BuyerEvaluationUserId] [int] NOT NULL,
    [EvaluationDate] [datetimeoffset](7),
    [BuyerEmailSendDate] [datetimeoffset](7),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ProjectBuyerEvaluations] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ProjectId] ON [dbo].[ProjectBuyerEvaluations]([ProjectId])
CREATE INDEX [IX_BuyerAttendeeOrganizationId] ON [dbo].[ProjectBuyerEvaluations]([BuyerAttendeeOrganizationId])
CREATE INDEX [IX_ProjectEvaluationStatusId] ON [dbo].[ProjectBuyerEvaluations]([ProjectEvaluationStatusId])
CREATE INDEX [IX_ProjectEvaluationRefuseReasonId] ON [dbo].[ProjectBuyerEvaluations]([ProjectEvaluationRefuseReasonId])
CREATE INDEX [IX_SellerUserId] ON [dbo].[ProjectBuyerEvaluations]([SellerUserId])
CREATE INDEX [IX_BuyerEvaluationUserId] ON [dbo].[ProjectBuyerEvaluations]([BuyerEvaluationUserId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ProjectBuyerEvaluations]([Uid])
CREATE TABLE [dbo].[Negotiations] (
    [Id] [int] NOT NULL IDENTITY,
    [ProjectBuyerEvaluationId] [int] NOT NULL,
    [RoomId] [int] NOT NULL,
    [StartDate] [datetimeoffset](7) NOT NULL,
    [EndDate] [datetimeoffset](7) NOT NULL,
    [TableNumber] [int] NOT NULL,
    [RoundNumber] [int] NOT NULL,
    [IsAutomatic] [bit] NOT NULL,
    [EditionId] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Negotiations] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ProjectBuyerEvaluationId] ON [dbo].[Negotiations]([ProjectBuyerEvaluationId])
CREATE INDEX [IX_RoomId] ON [dbo].[Negotiations]([RoomId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Negotiations]([Uid])
CREATE INDEX [IX_UpdateUserId] ON [dbo].[Negotiations]([UpdateUserId])
CREATE TABLE [dbo].[AttendeeNegotiationCollaborators] (
    [Id] [int] NOT NULL IDENTITY,
    [NegotiationId] [int] NOT NULL,
    [AttendeeCollaboratorId] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeNegotiationCollaborators] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_NegotiationId] ON [dbo].[AttendeeNegotiationCollaborators]([NegotiationId])
CREATE INDEX [IX_AttendeeCollaboratorId] ON [dbo].[AttendeeNegotiationCollaborators]([AttendeeCollaboratorId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeNegotiationCollaborators]([Uid])
CREATE TABLE [dbo].[ProjectEvaluationRefuseReasons] (
    [Id] [int] NOT NULL IDENTITY,
    [ProjectTypeId] [int] NOT NULL,
    [Name] [varchar](500),
    [HasAdditionalInfo] [bit] NOT NULL,
    [DisplayOrder] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ProjectEvaluationRefuseReasons] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ProjectTypeId] ON [dbo].[ProjectEvaluationRefuseReasons]([ProjectTypeId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ProjectEvaluationRefuseReasons]([Uid])
CREATE TABLE [dbo].[ProjectEvaluationStatuses] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](50),
    [Code] [varchar](50),
    [IsEvaluated] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ProjectEvaluationStatuses] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ProjectEvaluationStatuses]([Uid])
CREATE TABLE [dbo].[ProjectImageLinks] (
    [Id] [int] NOT NULL IDENTITY,
    [ProjectId] [int] NOT NULL,
    [Value] [varchar](3000),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ProjectImageLinks] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ProjectId] ON [dbo].[ProjectImageLinks]([ProjectId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ProjectImageLinks]([Uid])
CREATE TABLE [dbo].[ProjectInterests] (
    [Id] [int] NOT NULL IDENTITY,
    [ProjectId] [int] NOT NULL,
    [InterestId] [int] NOT NULL,
    [AdditionalInfo] [varchar](200),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ProjectInterests] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ProjectId] ON [dbo].[ProjectInterests]([ProjectId])
CREATE INDEX [IX_InterestId] ON [dbo].[ProjectInterests]([InterestId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ProjectInterests]([Uid])
CREATE TABLE [dbo].[ProjectLogLines] (
    [Id] [int] NOT NULL IDENTITY,
    [ProjectId] [int] NOT NULL,
    [LanguageId] [int] NOT NULL,
    [Value] [varchar](8000),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ProjectLogLines] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ProjectId] ON [dbo].[ProjectLogLines]([ProjectId])
CREATE INDEX [IX_LanguageId] ON [dbo].[ProjectLogLines]([LanguageId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ProjectLogLines]([Uid])
CREATE TABLE [dbo].[ProjectProductionPlans] (
    [Id] [int] NOT NULL IDENTITY,
    [ProjectId] [int] NOT NULL,
    [LanguageId] [int] NOT NULL,
    [Value] [varchar](3000),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ProjectProductionPlans] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ProjectId] ON [dbo].[ProjectProductionPlans]([ProjectId])
CREATE INDEX [IX_LanguageId] ON [dbo].[ProjectProductionPlans]([LanguageId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ProjectProductionPlans]([Uid])
CREATE TABLE [dbo].[ProjectSummaries] (
    [Id] [int] NOT NULL IDENTITY,
    [ProjectId] [int] NOT NULL,
    [LanguageId] [int] NOT NULL,
    [Value] [nvarchar](max),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ProjectSummaries] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ProjectId] ON [dbo].[ProjectSummaries]([ProjectId])
CREATE INDEX [IX_LanguageId] ON [dbo].[ProjectSummaries]([LanguageId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ProjectSummaries]([Uid])
CREATE TABLE [dbo].[ProjectTargetAudiences] (
    [Id] [int] NOT NULL IDENTITY,
    [ProjectId] [int] NOT NULL,
    [TargetAudienceId] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ProjectTargetAudiences] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ProjectId] ON [dbo].[ProjectTargetAudiences]([ProjectId])
CREATE INDEX [IX_TargetAudienceId] ON [dbo].[ProjectTargetAudiences]([TargetAudienceId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ProjectTargetAudiences]([Uid])
CREATE TABLE [dbo].[ProjectTeaserLinks] (
    [Id] [int] NOT NULL IDENTITY,
    [ProjectId] [int] NOT NULL,
    [Value] [varchar](3000),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ProjectTeaserLinks] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ProjectId] ON [dbo].[ProjectTeaserLinks]([ProjectId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ProjectTeaserLinks]([Uid])
CREATE TABLE [dbo].[ProjectTitles] (
    [Id] [int] NOT NULL IDENTITY,
    [ProjectId] [int] NOT NULL,
    [LanguageId] [int] NOT NULL,
    [Value] [varchar](81),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ProjectTitles] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_ProjectId] ON [dbo].[ProjectTitles]([ProjectId])
CREATE INDEX [IX_LanguageId] ON [dbo].[ProjectTitles]([LanguageId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ProjectTitles]([Uid])
CREATE TABLE [dbo].[Messages] (
    [Id] [int] NOT NULL IDENTITY,
    [EditionId] [int] NOT NULL,
    [SenderId] [int] NOT NULL,
    [RecipientId] [int] NOT NULL,
    [Text] [varchar](1200),
    [SendDate] [datetimeoffset](7) NOT NULL,
    [ReadDate] [datetimeoffset](7),
    [NotificationEmailSendDate] [datetimeoffset](7),
    [Uid] [uniqueidentifier] NOT NULL,
    CONSTRAINT [PK_dbo.Messages] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_EditionId] ON [dbo].[Messages]([EditionId])
CREATE INDEX [IX_SenderId] ON [dbo].[Messages]([SenderId])
CREATE INDEX [IX_RecipientId] ON [dbo].[Messages]([RecipientId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Messages]([Uid])
CREATE TABLE [dbo].[UserUnsubscribedLists] (
    [Id] [int] NOT NULL IDENTITY,
    [UserId] [int] NOT NULL,
    [SubscribeListId] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.UserUnsubscribedLists] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_UserId] ON [dbo].[UserUnsubscribedLists]([UserId])
CREATE INDEX [IX_SubscribeListId] ON [dbo].[UserUnsubscribedLists]([SubscribeListId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[UserUnsubscribedLists]([Uid])
CREATE TABLE [dbo].[SubscribeLists] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](200) NOT NULL,
    [Description] [varchar](2000) NOT NULL,
    [Code] [varchar](50) NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.SubscribeLists] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[SubscribeLists]([Uid])
CREATE TABLE [dbo].[CartoonProjects] (
    [Id] [int] NOT NULL IDENTITY,
    [Title] [varchar](300),
    [LogLine] [varchar](3000),
    [Summary] [varchar](3000),
    [Motivation] [varchar](3000),
    [ProductionPlan] [varchar](3000),
    [TeaserUrl] [varchar](300),
    [BibleUrl] [varchar](300),
    [NumberOfEpisodes] [int] NOT NULL,
    [EachEpisodePlayingTime] [varchar](10),
    [TotalValueOfProject] [varchar](50),
    [CartoonProjectFormatId] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.CartoonProjects] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_CartoonProjectFormatId] ON [dbo].[CartoonProjects]([CartoonProjectFormatId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[CartoonProjects]([Uid])
CREATE TABLE [dbo].[CartoonProjectCreators] (
    [Id] [int] NOT NULL IDENTITY,
    [CartoonProjectId] [int] NOT NULL,
    [FirstName] [varchar](300),
    [LastName] [varchar](300),
    [Document] [varchar](50),
    [Email] [varchar](300),
    [CellPhone] [varchar](50),
    [PhoneNumber] [varchar](50),
    [MiniBio] [varchar](3000),
    [IsResponsible] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.CartoonProjectCreators] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_CartoonProjectId] ON [dbo].[CartoonProjectCreators]([CartoonProjectId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[CartoonProjectCreators]([Uid])
CREATE TABLE [dbo].[CartoonProjectFormats] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](50),
    [DisplayOrder] [int] NOT NULL,
    [HasAdditionalInfo] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.CartoonProjectFormats] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[CartoonProjectFormats]([Uid])
CREATE TABLE [dbo].[CartoonProjectOrganizations] (
    [Id] [int] NOT NULL IDENTITY,
    [CartoonProjectId] [int] NOT NULL,
    [AddressId] [int],
    [Name] [varchar](300),
    [TradeName] [varchar](300),
    [Document] [varchar](50),
    [PhoneNumber] [varchar](50),
    [ReelUrl] [varchar](100),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.CartoonProjectOrganizations] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_CartoonProjectId] ON [dbo].[CartoonProjectOrganizations]([CartoonProjectId])
CREATE INDEX [IX_AddressId] ON [dbo].[CartoonProjectOrganizations]([AddressId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[CartoonProjectOrganizations]([Uid])
CREATE TABLE [dbo].[AttendeeCollaboratorActivities] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeCollaboratorId] [int] NOT NULL,
    [ActivityId] [int] NOT NULL,
    [AdditionalInfo] [varchar](50),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeCollaboratorActivities] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeCollaboratorId] ON [dbo].[AttendeeCollaboratorActivities]([AttendeeCollaboratorId])
CREATE INDEX [IX_ActivityId] ON [dbo].[AttendeeCollaboratorActivities]([ActivityId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeCollaboratorActivities]([Uid])
CREATE TABLE [dbo].[AttendeeCollaboratorTargetAudiences] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeCollaboratorId] [int] NOT NULL,
    [TargetAudienceId] [int] NOT NULL,
    [AdditionalInfo] [varchar](50),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeCollaboratorTargetAudiences] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeCollaboratorId] ON [dbo].[AttendeeCollaboratorTargetAudiences]([AttendeeCollaboratorId])
CREATE INDEX [IX_TargetAudienceId] ON [dbo].[AttendeeCollaboratorTargetAudiences]([TargetAudienceId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeCollaboratorTargetAudiences]([Uid])
CREATE TABLE [dbo].[Logistics] (
    [Id] [int] NOT NULL IDENTITY,
    [AttendeeCollaboratorId] [int] NOT NULL,
    [IsAirfareSponsored] [bit] NOT NULL,
    [AirfareAttendeeLogisticSponsorId] [int],
    [IsAccommodationSponsored] [bit] NOT NULL,
    [AccommodationAttendeeLogisticSponsorId] [int],
    [IsAirportTransferSponsored] [bit] NOT NULL,
    [AirportTransferAttendeeLogisticSponsorId] [int],
    [IsCityTransferRequired] [bit] NOT NULL,
    [IsVehicleDisposalRequired] [bit] NOT NULL,
    [AdditionalInfo] [varchar](1000),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Logistics] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AttendeeCollaboratorId] ON [dbo].[Logistics]([AttendeeCollaboratorId])
CREATE INDEX [IX_AirfareAttendeeLogisticSponsorId] ON [dbo].[Logistics]([AirfareAttendeeLogisticSponsorId])
CREATE INDEX [IX_AccommodationAttendeeLogisticSponsorId] ON [dbo].[Logistics]([AccommodationAttendeeLogisticSponsorId])
CREATE INDEX [IX_AirportTransferAttendeeLogisticSponsorId] ON [dbo].[Logistics]([AirportTransferAttendeeLogisticSponsorId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Logistics]([Uid])
CREATE INDEX [IX_CreateUserId] ON [dbo].[Logistics]([CreateUserId])
CREATE TABLE [dbo].[AttendeeLogisticSponsors] (
    [Id] [int] NOT NULL IDENTITY,
    [EditionId] [int] NOT NULL,
    [LogisticSponsorId] [int] NOT NULL,
    [IsOther] [bit] NOT NULL,
    [IsLogisticListDisplayed] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeeLogisticSponsors] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_EditionId] ON [dbo].[AttendeeLogisticSponsors]([EditionId])
CREATE INDEX [IX_LogisticSponsorId] ON [dbo].[AttendeeLogisticSponsors]([LogisticSponsorId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeeLogisticSponsors]([Uid])
CREATE TABLE [dbo].[LogisticSponsors] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](100),
    [IsAirfareTicketRequired] [bit] NOT NULL,
    [IsOtherRequired] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.LogisticSponsors] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[LogisticSponsors]([Uid])
CREATE TABLE [dbo].[LogisticAccommodations] (
    [Id] [int] NOT NULL IDENTITY,
    [LogisticId] [int] NOT NULL,
    [AttendeePlaceId] [int] NOT NULL,
    [CheckInDate] [datetimeoffset](7) NOT NULL,
    [CheckOutDate] [datetimeoffset](7) NOT NULL,
    [AdditionalInfo] [varchar](1000),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.LogisticAccommodations] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_LogisticId] ON [dbo].[LogisticAccommodations]([LogisticId])
CREATE INDEX [IX_AttendeePlaceId] ON [dbo].[LogisticAccommodations]([AttendeePlaceId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[LogisticAccommodations]([Uid])
CREATE TABLE [dbo].[AttendeePlaces] (
    [Id] [int] NOT NULL IDENTITY,
    [EditionId] [int] NOT NULL,
    [PlaceId] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.AttendeePlaces] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_EditionId] ON [dbo].[AttendeePlaces]([EditionId])
CREATE INDEX [IX_PlaceId] ON [dbo].[AttendeePlaces]([PlaceId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[AttendeePlaces]([Uid])
CREATE TABLE [dbo].[Places] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](100),
    [IsHotel] [bit] NOT NULL,
    [IsAirport] [bit] NOT NULL,
    [AddressId] [int],
    [Website] [varchar](300),
    [AdditionalInfo] [varchar](1000),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Places] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_AddressId] ON [dbo].[Places]([AddressId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Places]([Uid])
CREATE TABLE [dbo].[LogisticAirfares] (
    [Id] [int] NOT NULL IDENTITY,
    [LogisticId] [int] NOT NULL,
    [IsNational] [bit] NOT NULL,
    [IsArrival] [bit] NOT NULL,
    [From] [varchar](100),
    [To] [varchar](100),
    [TicketNumber] [varchar](20),
    [AdditionalInfo] [varchar](1000),
    [DepartureDate] [datetimeoffset](7) NOT NULL,
    [ArrivalDate] [datetimeoffset](7) NOT NULL,
    [TicketUploadDate] [datetimeoffset](7),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.LogisticAirfares] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_LogisticId] ON [dbo].[LogisticAirfares]([LogisticId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[LogisticAirfares]([Uid])
CREATE TABLE [dbo].[LogisticTransfers] (
    [Id] [int] NOT NULL IDENTITY,
    [LogisticId] [int] NOT NULL,
    [FromAttendeePlaceId] [int] NOT NULL,
    [ToAttendeePlaceId] [int] NOT NULL,
    [Date] [datetimeoffset](7) NOT NULL,
    [AdditionalInfo] [varchar](1000),
    [LogisticTransferStatusId] [int],
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.LogisticTransfers] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_LogisticId] ON [dbo].[LogisticTransfers]([LogisticId])
CREATE INDEX [IX_FromAttendeePlaceId] ON [dbo].[LogisticTransfers]([FromAttendeePlaceId])
CREATE INDEX [IX_ToAttendeePlaceId] ON [dbo].[LogisticTransfers]([ToAttendeePlaceId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[LogisticTransfers]([Uid])
CREATE TABLE [dbo].[Connections] (
    [ConnectionId] [uniqueidentifier] NOT NULL,
    [UserId] [int] NOT NULL,
    [UserAgent] [varchar](500),
    [Uid] [uniqueidentifier] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    CONSTRAINT [PK_dbo.Connections] PRIMARY KEY ([ConnectionId])
)
CREATE INDEX [IX_UserId] ON [dbo].[Connections]([UserId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Connections]([Uid])
CREATE TABLE [dbo].[ProjectStatus] (
    [Id] [int] NOT NULL IDENTITY,
    [Code] [varchar](50),
    [Name] [varchar](50),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.ProjectStatus] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[ProjectStatus]([Uid])
CREATE TABLE [dbo].[Quizzes] (
    [Id] [int] NOT NULL IDENTITY,
    [EditionId] [int] NOT NULL,
    [Name] [varchar](50),
    [IsActive] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.Quizzes] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_EditionId] ON [dbo].[Quizzes]([EditionId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[Quizzes]([Uid])
CREATE TABLE [dbo].[QuizQuestion] (
    [Id] [int] NOT NULL IDENTITY,
    [QuizId] [int] NOT NULL,
    [Question] [varchar](50),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.QuizQuestion] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_QuizId] ON [dbo].[QuizQuestion]([QuizId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[QuizQuestion]([Uid])
CREATE TABLE [dbo].[QuizOption] (
    [Id] [int] NOT NULL,
    [QuestionId] [int] NOT NULL,
    [Text] [bit] NOT NULL,
    [Value] [varchar](50),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.QuizOption] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_Id] ON [dbo].[QuizOption]([Id])
CREATE INDEX [IX_QuestionId] ON [dbo].[QuizOption]([QuestionId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[QuizOption]([Uid])
CREATE TABLE [dbo].[QuizAnswer] (
    [Id] [int] NOT NULL IDENTITY,
    [UserId] [int] NOT NULL,
    [OptionId] [int] NOT NULL,
    [Value] [varchar](50),
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.QuizAnswer] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_UserId] ON [dbo].[QuizAnswer]([UserId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[QuizAnswer]([Uid])
CREATE TABLE [dbo].[SalesPlatformWebhookRequests] (
    [Id] [int] NOT NULL IDENTITY,
    [SalesPlatformId] [int] NOT NULL,
    [Endpoint] [varchar](250),
    [Header] [varchar](1000),
    [Payload] [varchar](5000),
    [IpAddress] [varchar](38),
    [IsProcessed] [bit] NOT NULL,
    [IsProcessing] [bit] NOT NULL,
    [ProcessingCount] [int] NOT NULL,
    [LastProcessingDate] [datetimeoffset](7),
    [NextProcessingDate] [datetimeoffset](7),
    [ProcessingErrorCode] [varchar](10),
    [ProcessingErrorMessage] [varchar](250),
    [ManualProcessingUserId] [int],
    [SecurityStamp] [varchar](50),
    [Uid] [uniqueidentifier] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    CONSTRAINT [PK_dbo.SalesPlatformWebhookRequests] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_SalesPlatformId] ON [dbo].[SalesPlatformWebhookRequests]([SalesPlatformId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[SalesPlatformWebhookRequests]([Uid])
CREATE TABLE [dbo].[UserRole] (
    [Id] [int] NOT NULL IDENTITY,
    [RoleId] [int] NOT NULL,
    [UserId] [int] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.UserRole] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_RoleId] ON [dbo].[UserRole]([RoleId])
CREATE INDEX [IX_UserId] ON [dbo].[UserRole]([UserId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[UserRole]([Uid])
CREATE TABLE [dbo].[SentEmails] (
    [Id] [int] NOT NULL IDENTITY,
    [RecipientUserId] [int] NOT NULL,
    [EditionId] [int],
    [EmailType] [varchar](81) NOT NULL,
    [EmailSendDate] [datetimeoffset](7) NOT NULL,
    [EmailReadDate] [datetimeoffset](7),
    [Uid] [uniqueidentifier] NOT NULL,
    CONSTRAINT [PK_dbo.SentEmails] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[SentEmails]([Uid])
CREATE TABLE [dbo].[WeConnectPublications] (
    [Id] [int] NOT NULL IDENTITY,
    [SocialMediaPlatformId] [int],
    [SocialMediaPlatformPublicationId] [varchar](20),
    [PublicationText] [varchar](3000),
    [ImageUploadDate] [datetimeoffset](7),
    [IsVideo] [bit] NOT NULL,
    [IsFixedOnTop] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.WeConnectPublications] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_SocialMediaPlatformId] ON [dbo].[WeConnectPublications]([SocialMediaPlatformId])
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[WeConnectPublications]([Uid])
CREATE TABLE [dbo].[SocialMediaPlatforms] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [varchar](50),
    [ApiKey] [varchar](50),
    [EndpointUrl] [varchar](50),
    [PublicationsRootUrl] [varchar](50),
    [IsSyncActive] [bit] NOT NULL,
    [Uid] [uniqueidentifier] NOT NULL,
    [IsDeleted] [bit] NOT NULL,
    [CreateDate] [datetimeoffset](7) NOT NULL,
    [CreateUserId] [int] NOT NULL,
    [UpdateDate] [datetimeoffset](7) NOT NULL,
    [UpdateUserId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.SocialMediaPlatforms] PRIMARY KEY ([Id])
)
CREATE UNIQUE INDEX [IX_Uid] ON [dbo].[SocialMediaPlatforms]([Uid])
CREATE TABLE [dbo].[UsersRoles] (
    [UserId] [int] NOT NULL,
    [RoleId] [int] NOT NULL,
    CONSTRAINT [PK_dbo.UsersRoles] PRIMARY KEY ([UserId], [RoleId])
)
CREATE INDEX [IX_UserId] ON [dbo].[UsersRoles]([UserId])
CREATE INDEX [IX_RoleId] ON [dbo].[UsersRoles]([RoleId])
ALTER TABLE [dbo].[Activities] ADD CONSTRAINT [FK_dbo.Activities_dbo.ProjectTypes_ProjectTypeId] FOREIGN KEY ([ProjectTypeId]) REFERENCES [dbo].[ProjectTypes] ([Id])
ALTER TABLE [dbo].[InterestGroups] ADD CONSTRAINT [FK_dbo.InterestGroups_dbo.ProjectTypes_ProjectTypeId] FOREIGN KEY ([ProjectTypeId]) REFERENCES [dbo].[ProjectTypes] ([Id])
ALTER TABLE [dbo].[OrganizationTypes] ADD CONSTRAINT [FK_dbo.OrganizationTypes_dbo.ProjectTypes_RelatedProjectTypeId] FOREIGN KEY ([RelatedProjectTypeId]) REFERENCES [dbo].[ProjectTypes] ([Id])
ALTER TABLE [dbo].[AttendeeOrganizationTypes] ADD CONSTRAINT [FK_dbo.AttendeeOrganizationTypes_dbo.AttendeeOrganizations_AttendeeOrganizationId] FOREIGN KEY ([AttendeeOrganizationId]) REFERENCES [dbo].[AttendeeOrganizations] ([Id])
ALTER TABLE [dbo].[AttendeeOrganizationTypes] ADD CONSTRAINT [FK_dbo.AttendeeOrganizationTypes_dbo.OrganizationTypes_OrganizationTypeId] FOREIGN KEY ([OrganizationTypeId]) REFERENCES [dbo].[OrganizationTypes] ([Id])
ALTER TABLE [dbo].[AttendeeOrganizations] ADD CONSTRAINT [FK_dbo.AttendeeOrganizations_dbo.Editions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [dbo].[Editions] ([Id])
ALTER TABLE [dbo].[AttendeeOrganizations] ADD CONSTRAINT [FK_dbo.AttendeeOrganizations_dbo.Organizations_OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organizations] ([Id])
ALTER TABLE [dbo].[AttendeeOrganizationCollaborators] ADD CONSTRAINT [FK_dbo.AttendeeOrganizationCollaborators_dbo.AttendeeCollaborators_AttendeeCollaboratorId] FOREIGN KEY ([AttendeeCollaboratorId]) REFERENCES [dbo].[AttendeeCollaborators] ([Id])
ALTER TABLE [dbo].[AttendeeOrganizationCollaborators] ADD CONSTRAINT [FK_dbo.AttendeeOrganizationCollaborators_dbo.AttendeeOrganizations_AttendeeOrganizationId] FOREIGN KEY ([AttendeeOrganizationId]) REFERENCES [dbo].[AttendeeOrganizations] ([Id])
ALTER TABLE [dbo].[AttendeeCollaborators] ADD CONSTRAINT [FK_dbo.AttendeeCollaborators_dbo.Collaborators_CollaboratorId] FOREIGN KEY ([CollaboratorId]) REFERENCES [dbo].[Collaborators] ([Id])
ALTER TABLE [dbo].[AttendeeCollaborators] ADD CONSTRAINT [FK_dbo.AttendeeCollaborators_dbo.Editions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [dbo].[Editions] ([Id])
ALTER TABLE [dbo].[AttendeeCartoonProjectCollaborators] ADD CONSTRAINT [FK_dbo.AttendeeCartoonProjectCollaborators_dbo.AttendeeCartoonProjects_AttendeeCartoonProjectId] FOREIGN KEY ([AttendeeCartoonProjectId]) REFERENCES [dbo].[AttendeeCartoonProjects] ([Id])
ALTER TABLE [dbo].[AttendeeCartoonProjectCollaborators] ADD CONSTRAINT [FK_dbo.AttendeeCartoonProjectCollaborators_dbo.AttendeeCollaborators_AttendeeCollaboratorId] FOREIGN KEY ([AttendeeCollaboratorId]) REFERENCES [dbo].[AttendeeCollaborators] ([Id])
ALTER TABLE [dbo].[AttendeeCartoonProjects] ADD CONSTRAINT [FK_dbo.AttendeeCartoonProjects_dbo.CartoonProjects_CartoonProjectId] FOREIGN KEY ([CartoonProjectId]) REFERENCES [dbo].[CartoonProjects] ([Id])
ALTER TABLE [dbo].[AttendeeCartoonProjects] ADD CONSTRAINT [FK_dbo.AttendeeCartoonProjects_dbo.Editions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [dbo].[Editions] ([Id])
ALTER TABLE [dbo].[AttendeeCartoonProjectEvaluations] ADD CONSTRAINT [FK_dbo.AttendeeCartoonProjectEvaluations_dbo.AttendeeCartoonProjects_AttendeeCartoonProjectId] FOREIGN KEY ([AttendeeCartoonProjectId]) REFERENCES [dbo].[AttendeeCartoonProjects] ([Id])
ALTER TABLE [dbo].[AttendeeCartoonProjectEvaluations] ADD CONSTRAINT [FK_dbo.AttendeeCartoonProjectEvaluations_dbo.Users_EvaluatorUserId] FOREIGN KEY ([EvaluatorUserId]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[Users] ADD CONSTRAINT [FK_dbo.Users_dbo.Languages_UserInterfaceLanguageId] FOREIGN KEY ([UserInterfaceLanguageId]) REFERENCES [dbo].[Languages] ([Id])
ALTER TABLE [dbo].[AttendeeCreatorProjectEvaluations] ADD CONSTRAINT [FK_dbo.AttendeeCreatorProjectEvaluations_dbo.AttendeeCreatorProjects_AttendeeCreatorProjectId] FOREIGN KEY ([AttendeeCreatorProjectId]) REFERENCES [dbo].[AttendeeCreatorProjects] ([Id])
ALTER TABLE [dbo].[AttendeeCreatorProjectEvaluations] ADD CONSTRAINT [FK_dbo.AttendeeCreatorProjectEvaluations_dbo.Users_EvaluatorUserId] FOREIGN KEY ([EvaluatorUserId]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[AttendeeCreatorProjects] ADD CONSTRAINT [FK_dbo.AttendeeCreatorProjects_dbo.CreatorProjects_CreatorProjectId] FOREIGN KEY ([CreatorProjectId]) REFERENCES [dbo].[CreatorProjects] ([Id])
ALTER TABLE [dbo].[AttendeeCreatorProjects] ADD CONSTRAINT [FK_dbo.AttendeeCreatorProjects_dbo.Editions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [dbo].[Editions] ([Id])
ALTER TABLE [dbo].[CreatorProjectInterests] ADD CONSTRAINT [FK_dbo.CreatorProjectInterests_dbo.CreatorProjects_CreatorProjectId] FOREIGN KEY ([CreatorProjectId]) REFERENCES [dbo].[CreatorProjects] ([Id])
ALTER TABLE [dbo].[CreatorProjectInterests] ADD CONSTRAINT [FK_dbo.CreatorProjectInterests_dbo.Interests_InterestId] FOREIGN KEY ([InterestId]) REFERENCES [dbo].[Interests] ([Id])
ALTER TABLE [dbo].[Interests] ADD CONSTRAINT [FK_dbo.Interests_dbo.InterestGroups_InterestGroupId] FOREIGN KEY ([InterestGroupId]) REFERENCES [dbo].[InterestGroups] ([Id])
ALTER TABLE [dbo].[AttendeeCollaboratorInterests] ADD CONSTRAINT [FK_dbo.AttendeeCollaboratorInterests_dbo.AttendeeCollaborators_AttendeeCollaboratorId] FOREIGN KEY ([AttendeeCollaboratorId]) REFERENCES [dbo].[AttendeeCollaborators] ([Id])
ALTER TABLE [dbo].[AttendeeCollaboratorInterests] ADD CONSTRAINT [FK_dbo.AttendeeCollaboratorInterests_dbo.Interests_InterestId] FOREIGN KEY ([InterestId]) REFERENCES [dbo].[Interests] ([Id])
ALTER TABLE [dbo].[AttendeeSalesPlatforms] ADD CONSTRAINT [FK_dbo.AttendeeSalesPlatforms_dbo.Editions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [dbo].[Editions] ([Id])
ALTER TABLE [dbo].[AttendeeSalesPlatforms] ADD CONSTRAINT [FK_dbo.AttendeeSalesPlatforms_dbo.SalesPlatforms_SalesPlatformId] FOREIGN KEY ([SalesPlatformId]) REFERENCES [dbo].[SalesPlatforms] ([Id])
ALTER TABLE [dbo].[AttendeeSalesPlatformTicketTypes] ADD CONSTRAINT [FK_dbo.AttendeeSalesPlatformTicketTypes_dbo.CollaboratorTypes_CollaboratorTypeId] FOREIGN KEY ([CollaboratorTypeId]) REFERENCES [dbo].[CollaboratorTypes] ([Id])
ALTER TABLE [dbo].[AttendeeSalesPlatformTicketTypes] ADD CONSTRAINT [FK_dbo.AttendeeSalesPlatformTicketTypes_dbo.AttendeeSalesPlatforms_AttendeeSalesPlatformId] FOREIGN KEY ([AttendeeSalesPlatformId]) REFERENCES [dbo].[AttendeeSalesPlatforms] ([Id])
ALTER TABLE [dbo].[AttendeeCollaboratorTickets] ADD CONSTRAINT [FK_dbo.AttendeeCollaboratorTickets_dbo.AttendeeCollaborators_AttendeeCollaboratorId] FOREIGN KEY ([AttendeeCollaboratorId]) REFERENCES [dbo].[AttendeeCollaborators] ([Id])
ALTER TABLE [dbo].[AttendeeCollaboratorTickets] ADD CONSTRAINT [FK_dbo.AttendeeCollaboratorTickets_dbo.AttendeeSalesPlatformTicketTypes_AttendeeSalesPlatformTicketTypeId] FOREIGN KEY ([AttendeeSalesPlatformTicketTypeId]) REFERENCES [dbo].[AttendeeSalesPlatformTicketTypes] ([Id])
ALTER TABLE [dbo].[CollaboratorTypes] ADD CONSTRAINT [FK_dbo.CollaboratorTypes_dbo.Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id])
ALTER TABLE [dbo].[AttendeeCollaboratorTypes] ADD CONSTRAINT [FK_dbo.AttendeeCollaboratorTypes_dbo.AttendeeCollaborators_AttendeeCollaboratorId] FOREIGN KEY ([AttendeeCollaboratorId]) REFERENCES [dbo].[AttendeeCollaborators] ([Id])
ALTER TABLE [dbo].[AttendeeCollaboratorTypes] ADD CONSTRAINT [FK_dbo.AttendeeCollaboratorTypes_dbo.CollaboratorTypes_CollaboratorTypeId] FOREIGN KEY ([CollaboratorTypeId]) REFERENCES [dbo].[CollaboratorTypes] ([Id])
ALTER TABLE [dbo].[EditionEvents] ADD CONSTRAINT [FK_dbo.EditionEvents_dbo.Editions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [dbo].[Editions] ([Id])
ALTER TABLE [dbo].[Conferences] ADD CONSTRAINT [FK_dbo.Conferences_dbo.EditionEvents_EditionEventId] FOREIGN KEY ([EditionEventId]) REFERENCES [dbo].[EditionEvents] ([Id])
ALTER TABLE [dbo].[Conferences] ADD CONSTRAINT [FK_dbo.Conferences_dbo.Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Rooms] ([Id])
ALTER TABLE [dbo].[ConferenceDynamics] ADD CONSTRAINT [FK_dbo.ConferenceDynamics_dbo.Conferences_ConferenceId] FOREIGN KEY ([ConferenceId]) REFERENCES [dbo].[Conferences] ([Id])
ALTER TABLE [dbo].[ConferenceDynamics] ADD CONSTRAINT [FK_dbo.ConferenceDynamics_dbo.Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id])
ALTER TABLE [dbo].[ConferenceParticipants] ADD CONSTRAINT [FK_dbo.ConferenceParticipants_dbo.AttendeeCollaborators_AttendeeCollaboratorId] FOREIGN KEY ([AttendeeCollaboratorId]) REFERENCES [dbo].[AttendeeCollaborators] ([Id])
ALTER TABLE [dbo].[ConferenceParticipants] ADD CONSTRAINT [FK_dbo.ConferenceParticipants_dbo.Conferences_ConferenceId] FOREIGN KEY ([ConferenceId]) REFERENCES [dbo].[Conferences] ([Id])
ALTER TABLE [dbo].[ConferenceParticipants] ADD CONSTRAINT [FK_dbo.ConferenceParticipants_dbo.ConferenceParticipantRoles_ConferenceParticipantRoleId] FOREIGN KEY ([ConferenceParticipantRoleId]) REFERENCES [dbo].[ConferenceParticipantRoles] ([Id])
ALTER TABLE [dbo].[ConferenceParticipantRoleTitles] ADD CONSTRAINT [FK_dbo.ConferenceParticipantRoleTitles_dbo.ConferenceParticipantRoles_ConferenceParticipantRoleId] FOREIGN KEY ([ConferenceParticipantRoleId]) REFERENCES [dbo].[ConferenceParticipantRoles] ([Id])
ALTER TABLE [dbo].[ConferenceParticipantRoleTitles] ADD CONSTRAINT [FK_dbo.ConferenceParticipantRoleTitles_dbo.Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id])
ALTER TABLE [dbo].[ConferencePillars] ADD CONSTRAINT [FK_dbo.ConferencePillars_dbo.Conferences_ConferenceId] FOREIGN KEY ([ConferenceId]) REFERENCES [dbo].[Conferences] ([Id])
ALTER TABLE [dbo].[ConferencePillars] ADD CONSTRAINT [FK_dbo.ConferencePillars_dbo.Pillars_PillarId] FOREIGN KEY ([PillarId]) REFERENCES [dbo].[Pillars] ([Id])
ALTER TABLE [dbo].[Pillars] ADD CONSTRAINT [FK_dbo.Pillars_dbo.Editions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [dbo].[Editions] ([Id])
ALTER TABLE [dbo].[ConferencePresentationFormats] ADD CONSTRAINT [FK_dbo.ConferencePresentationFormats_dbo.Conferences_ConferenceId] FOREIGN KEY ([ConferenceId]) REFERENCES [dbo].[Conferences] ([Id])
ALTER TABLE [dbo].[ConferencePresentationFormats] ADD CONSTRAINT [FK_dbo.ConferencePresentationFormats_dbo.PresentationFormats_PresentationFormatId] FOREIGN KEY ([PresentationFormatId]) REFERENCES [dbo].[PresentationFormats] ([Id])
ALTER TABLE [dbo].[PresentationFormats] ADD CONSTRAINT [FK_dbo.PresentationFormats_dbo.Editions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [dbo].[Editions] ([Id])
ALTER TABLE [dbo].[ConferenceSynopsis] ADD CONSTRAINT [FK_dbo.ConferenceSynopsis_dbo.Conferences_ConferenceId] FOREIGN KEY ([ConferenceId]) REFERENCES [dbo].[Conferences] ([Id])
ALTER TABLE [dbo].[ConferenceSynopsis] ADD CONSTRAINT [FK_dbo.ConferenceSynopsis_dbo.Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id])
ALTER TABLE [dbo].[ConferenceTitles] ADD CONSTRAINT [FK_dbo.ConferenceTitles_dbo.Conferences_ConferenceId] FOREIGN KEY ([ConferenceId]) REFERENCES [dbo].[Conferences] ([Id])
ALTER TABLE [dbo].[ConferenceTitles] ADD CONSTRAINT [FK_dbo.ConferenceTitles_dbo.Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id])
ALTER TABLE [dbo].[ConferenceTracks] ADD CONSTRAINT [FK_dbo.ConferenceTracks_dbo.Conferences_ConferenceId] FOREIGN KEY ([ConferenceId]) REFERENCES [dbo].[Conferences] ([Id])
ALTER TABLE [dbo].[ConferenceTracks] ADD CONSTRAINT [FK_dbo.ConferenceTracks_dbo.Tracks_TrackId] FOREIGN KEY ([TrackId]) REFERENCES [dbo].[Tracks] ([Id])
ALTER TABLE [dbo].[Tracks] ADD CONSTRAINT [FK_dbo.Tracks_dbo.Editions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [dbo].[Editions] ([Id])
ALTER TABLE [dbo].[Rooms] ADD CONSTRAINT [FK_dbo.Rooms_dbo.Editions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [dbo].[Editions] ([Id])
ALTER TABLE [dbo].[NegotiationRoomConfigs] ADD CONSTRAINT [FK_dbo.NegotiationRoomConfigs_dbo.NegotiationConfigs_NegotiationConfigId] FOREIGN KEY ([NegotiationConfigId]) REFERENCES [dbo].[NegotiationConfigs] ([Id])
ALTER TABLE [dbo].[NegotiationRoomConfigs] ADD CONSTRAINT [FK_dbo.NegotiationRoomConfigs_dbo.Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Rooms] ([Id])
ALTER TABLE [dbo].[NegotiationConfigs] ADD CONSTRAINT [FK_dbo.NegotiationConfigs_dbo.Editions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [dbo].[Editions] ([Id])
ALTER TABLE [dbo].[RoomNames] ADD CONSTRAINT [FK_dbo.RoomNames_dbo.Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id])
ALTER TABLE [dbo].[RoomNames] ADD CONSTRAINT [FK_dbo.RoomNames_dbo.Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Rooms] ([Id])
ALTER TABLE [dbo].[AttendeeInnovationOrganizationEvaluations] ADD CONSTRAINT [FK_dbo.AttendeeInnovationOrganizationEvaluations_dbo.AttendeeInnovationOrganizations_AttendeeInnovationOrganizationId] FOREIGN KEY ([AttendeeInnovationOrganizationId]) REFERENCES [dbo].[AttendeeInnovationOrganizations] ([Id])
ALTER TABLE [dbo].[AttendeeInnovationOrganizationEvaluations] ADD CONSTRAINT [FK_dbo.AttendeeInnovationOrganizationEvaluations_dbo.Users_EvaluatorUserId] FOREIGN KEY ([EvaluatorUserId]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[AttendeeInnovationOrganizations] ADD CONSTRAINT [FK_dbo.AttendeeInnovationOrganizations_dbo.Editions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [dbo].[Editions] ([Id])
ALTER TABLE [dbo].[AttendeeInnovationOrganizations] ADD CONSTRAINT [FK_dbo.AttendeeInnovationOrganizations_dbo.InnovationOrganizations_InnovationOrganizationId] FOREIGN KEY ([InnovationOrganizationId]) REFERENCES [dbo].[InnovationOrganizations] ([Id])
ALTER TABLE [dbo].[AttendeeInnovationOrganizationCollaborators] ADD CONSTRAINT [FK_dbo.AttendeeInnovationOrganizationCollaborators_dbo.AttendeeCollaborators_AttendeeCollaboratorId] FOREIGN KEY ([AttendeeCollaboratorId]) REFERENCES [dbo].[AttendeeCollaborators] ([Id])
ALTER TABLE [dbo].[AttendeeInnovationOrganizationCollaborators] ADD CONSTRAINT [FK_dbo.AttendeeInnovationOrganizationCollaborators_dbo.AttendeeInnovationOrganizations_AttendeeInnovationOrganizationId] FOREIGN KEY ([AttendeeInnovationOrganizationId]) REFERENCES [dbo].[AttendeeInnovationOrganizations] ([Id])
ALTER TABLE [dbo].[AttendeeInnovationOrganizationCompetitors] ADD CONSTRAINT [FK_dbo.AttendeeInnovationOrganizationCompetitors_dbo.AttendeeInnovationOrganizations_AttendeeInnovationOrganizationId] FOREIGN KEY ([AttendeeInnovationOrganizationId]) REFERENCES [dbo].[AttendeeInnovationOrganizations] ([Id])
ALTER TABLE [dbo].[AttendeeInnovationOrganizationExperiences] ADD CONSTRAINT [FK_dbo.AttendeeInnovationOrganizationExperiences_dbo.AttendeeInnovationOrganizations_AttendeeInnovationOrganizationId] FOREIGN KEY ([AttendeeInnovationOrganizationId]) REFERENCES [dbo].[AttendeeInnovationOrganizations] ([Id])
ALTER TABLE [dbo].[AttendeeInnovationOrganizationExperiences] ADD CONSTRAINT [FK_dbo.AttendeeInnovationOrganizationExperiences_dbo.InnovationOrganizationExperienceOptions_InnovationOrganizationExperienceOpt] FOREIGN KEY ([InnovationOrganizationExperienceOptionId]) REFERENCES [dbo].[InnovationOrganizationExperienceOptions] ([Id])
ALTER TABLE [dbo].[AttendeeInnovationOrganizationFounders] ADD CONSTRAINT [FK_dbo.AttendeeInnovationOrganizationFounders_dbo.AttendeeInnovationOrganizations_AttendeeInnovationOrganizationId] FOREIGN KEY ([AttendeeInnovationOrganizationId]) REFERENCES [dbo].[AttendeeInnovationOrganizations] ([Id])
ALTER TABLE [dbo].[AttendeeInnovationOrganizationFounders] ADD CONSTRAINT [FK_dbo.AttendeeInnovationOrganizationFounders_dbo.WorkDedications_WorkDedicationId] FOREIGN KEY ([WorkDedicationId]) REFERENCES [dbo].[WorkDedications] ([Id])
ALTER TABLE [dbo].[AttendeeInnovationOrganizationObjectives] ADD CONSTRAINT [FK_dbo.AttendeeInnovationOrganizationObjectives_dbo.AttendeeInnovationOrganizations_AttendeeInnovationOrganiaztionId] FOREIGN KEY ([AttendeeInnovationOrganiaztionId]) REFERENCES [dbo].[AttendeeInnovationOrganizations] ([Id])
ALTER TABLE [dbo].[AttendeeInnovationOrganizationObjectives] ADD CONSTRAINT [FK_dbo.AttendeeInnovationOrganizationObjectives_dbo.InnovationOrganizationObjectivesOptions_InnovationOrganizationObjectiveOptio] FOREIGN KEY ([InnovationOrganizationObjectiveOptionId]) REFERENCES [dbo].[InnovationOrganizationObjectivesOptions] ([Id])
ALTER TABLE [dbo].[AttendeeInnovationOrganizationSustainableDevelopmentObjectives] ADD CONSTRAINT [FK_dbo.AttendeeInnovationOrganizationSustainableDevelopmentObjectives_dbo.AttendeeInnovationOrganizations_AttendeeInnovationOrga] FOREIGN KEY ([AttendeeInnovationOrganizationId]) REFERENCES [dbo].[AttendeeInnovationOrganizations] ([Id])
ALTER TABLE [dbo].[AttendeeInnovationOrganizationSustainableDevelopmentObjectives] ADD CONSTRAINT [FK_dbo.AttendeeInnovationOrganizationSustainableDevelopmentObjectives_dbo.InnovationOrganizationSustainableDevelopmentObjectives] FOREIGN KEY ([InnovationOrganizationSustainableDevelopmentObjectiveOptionId]) REFERENCES [dbo].[InnovationOrganizationSustainableDevelopmentObjectivesOptions] ([Id])
ALTER TABLE [dbo].[AttendeeInnovationOrganizationTechnologies] ADD CONSTRAINT [FK_dbo.AttendeeInnovationOrganizationTechnologies_dbo.AttendeeInnovationOrganizations_AttendeeInnovationOrganizationId] FOREIGN KEY ([AttendeeInnovationOrganizationId]) REFERENCES [dbo].[AttendeeInnovationOrganizations] ([Id])
ALTER TABLE [dbo].[AttendeeInnovationOrganizationTechnologies] ADD CONSTRAINT [FK_dbo.AttendeeInnovationOrganizationTechnologies_dbo.InnovationOrganizationTechnologyOptions_InnovationOrganizationTechnologyOp] FOREIGN KEY ([InnovationOrganizationTechnologyOptionId]) REFERENCES [dbo].[InnovationOrganizationTechnologyOptions] ([Id])
ALTER TABLE [dbo].[AttendeeInnovationOrganizationTracks] ADD CONSTRAINT [FK_dbo.AttendeeInnovationOrganizationTracks_dbo.AttendeeInnovationOrganizations_AttendeeInnovationOrganizationId] FOREIGN KEY ([AttendeeInnovationOrganizationId]) REFERENCES [dbo].[AttendeeInnovationOrganizations] ([Id])
ALTER TABLE [dbo].[AttendeeInnovationOrganizationTracks] ADD CONSTRAINT [FK_dbo.AttendeeInnovationOrganizationTracks_dbo.InnovationOrganizationTrackOptions_InnovationOrganizationTrackOptionId] FOREIGN KEY ([InnovationOrganizationTrackOptionId]) REFERENCES [dbo].[InnovationOrganizationTrackOptions] ([Id])
ALTER TABLE [dbo].[InnovationOrganizationTrackOptions] ADD CONSTRAINT [FK_dbo.InnovationOrganizationTrackOptions_dbo.InnovationOrganizationTrackOptionGroups_InnovationOrganizationTrackOptionGroupId] FOREIGN KEY ([InnovationOrganizationTrackOptionGroupId]) REFERENCES [dbo].[InnovationOrganizationTrackOptionGroups] ([Id])
ALTER TABLE [dbo].[AttendeeCollaboratorInnovationOrganizationTracks] ADD CONSTRAINT [FK_dbo.AttendeeCollaboratorInnovationOrganizationTracks_dbo.AttendeeCollaborators_AttendeeCollaboratorId] FOREIGN KEY ([AttendeeCollaboratorId]) REFERENCES [dbo].[AttendeeCollaborators] ([Id])
ALTER TABLE [dbo].[AttendeeCollaboratorInnovationOrganizationTracks] ADD CONSTRAINT [FK_dbo.AttendeeCollaboratorInnovationOrganizationTracks_dbo.InnovationOrganizationTrackOptions_InnovationOrganizationTrackOption] FOREIGN KEY ([InnovationOrganizationTrackOptionId]) REFERENCES [dbo].[InnovationOrganizationTrackOptions] ([Id])
ALTER TABLE [dbo].[AttendeeMusicBandEvaluations] ADD CONSTRAINT [FK_dbo.AttendeeMusicBandEvaluations_dbo.AttendeeMusicBands_AttendeeMusicBandId] FOREIGN KEY ([AttendeeMusicBandId]) REFERENCES [dbo].[AttendeeMusicBands] ([Id])
ALTER TABLE [dbo].[AttendeeMusicBandEvaluations] ADD CONSTRAINT [FK_dbo.AttendeeMusicBandEvaluations_dbo.Users_EvaluatorUserId] FOREIGN KEY ([EvaluatorUserId]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[AttendeeMusicBands] ADD CONSTRAINT [FK_dbo.AttendeeMusicBands_dbo.Editions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [dbo].[Editions] ([Id])
ALTER TABLE [dbo].[AttendeeMusicBands] ADD CONSTRAINT [FK_dbo.AttendeeMusicBands_dbo.MusicBands_MusicBandId] FOREIGN KEY ([MusicBandId]) REFERENCES [dbo].[MusicBands] ([Id])
ALTER TABLE [dbo].[AttendeeMusicBandCollaborators] ADD CONSTRAINT [FK_dbo.AttendeeMusicBandCollaborators_dbo.AttendeeCollaborators_AttendeeCollaboratorId] FOREIGN KEY ([AttendeeCollaboratorId]) REFERENCES [dbo].[AttendeeCollaborators] ([Id])
ALTER TABLE [dbo].[AttendeeMusicBandCollaborators] ADD CONSTRAINT [FK_dbo.AttendeeMusicBandCollaborators_dbo.AttendeeMusicBands_AttendeeMusicBandId] FOREIGN KEY ([AttendeeMusicBandId]) REFERENCES [dbo].[AttendeeMusicBands] ([Id])
ALTER TABLE [dbo].[MusicBands] ADD CONSTRAINT [FK_dbo.MusicBands_dbo.MusicBandTypes_MusicBandTypeId] FOREIGN KEY ([MusicBandTypeId]) REFERENCES [dbo].[MusicBandTypes] ([Id])
ALTER TABLE [dbo].[MusicBandGenres] ADD CONSTRAINT [FK_dbo.MusicBandGenres_dbo.MusicBands_MusicBandId] FOREIGN KEY ([MusicBandId]) REFERENCES [dbo].[MusicBands] ([Id])
ALTER TABLE [dbo].[MusicBandGenres] ADD CONSTRAINT [FK_dbo.MusicBandGenres_dbo.MusicGenres_MusicGenreId] FOREIGN KEY ([MusicGenreId]) REFERENCES [dbo].[MusicGenres] ([Id])
ALTER TABLE [dbo].[MusicBandMembers] ADD CONSTRAINT [FK_dbo.MusicBandMembers_dbo.MusicBands_MusicBandId] FOREIGN KEY ([MusicBandId]) REFERENCES [dbo].[MusicBands] ([Id])
ALTER TABLE [dbo].[MusicBandTargetAudiences] ADD CONSTRAINT [FK_dbo.MusicBandTargetAudiences_dbo.MusicBands_MusicBandId] FOREIGN KEY ([MusicBandId]) REFERENCES [dbo].[MusicBands] ([Id])
ALTER TABLE [dbo].[MusicBandTargetAudiences] ADD CONSTRAINT [FK_dbo.MusicBandTargetAudiences_dbo.TargetAudiences_TargetAudienceId] FOREIGN KEY ([TargetAudienceId]) REFERENCES [dbo].[TargetAudiences] ([Id])
ALTER TABLE [dbo].[TargetAudiences] ADD CONSTRAINT [FK_dbo.TargetAudiences_dbo.ProjectTypes_ProjectTypeId] FOREIGN KEY ([ProjectTypeId]) REFERENCES [dbo].[ProjectTypes] ([Id])
ALTER TABLE [dbo].[MusicBandTeamMembers] ADD CONSTRAINT [FK_dbo.MusicBandTeamMembers_dbo.MusicBands_MusicBandId] FOREIGN KEY ([MusicBandId]) REFERENCES [dbo].[MusicBands] ([Id])
ALTER TABLE [dbo].[ReleasedMusicProjects] ADD CONSTRAINT [FK_dbo.ReleasedMusicProjects_dbo.MusicBands_MusicBandId] FOREIGN KEY ([MusicBandId]) REFERENCES [dbo].[MusicBands] ([Id])
ALTER TABLE [dbo].[MusicProjects] ADD CONSTRAINT [FK_dbo.MusicProjects_dbo.AttendeeMusicBands_AttendeeMusicBandId] FOREIGN KEY ([AttendeeMusicBandId]) REFERENCES [dbo].[AttendeeMusicBands] ([Id])
ALTER TABLE [dbo].[Collaborators] ADD CONSTRAINT [FK_dbo.Collaborators_dbo.Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Addresses] ([Id])
ALTER TABLE [dbo].[Collaborators] ADD CONSTRAINT [FK_dbo.Collaborators_dbo.Users_CreateUserId] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[Collaborators] ADD CONSTRAINT [FK_dbo.Collaborators_dbo.CollaboratorGenders_CollaboratorGenderId] FOREIGN KEY ([CollaboratorGenderId]) REFERENCES [dbo].[CollaboratorGenders] ([Id])
ALTER TABLE [dbo].[Collaborators] ADD CONSTRAINT [FK_dbo.Collaborators_dbo.CollaboratorIndustries_CollaboratorIndustryId] FOREIGN KEY ([CollaboratorIndustryId]) REFERENCES [dbo].[CollaboratorIndustries] ([Id])
ALTER TABLE [dbo].[Collaborators] ADD CONSTRAINT [FK_dbo.Collaborators_dbo.CollaboratorRoles_CollaboratorRoleId] FOREIGN KEY ([CollaboratorRoleId]) REFERENCES [dbo].[CollaboratorRoles] ([Id])
ALTER TABLE [dbo].[Collaborators] ADD CONSTRAINT [FK_dbo.Collaborators_dbo.Users_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[Collaborators] ADD CONSTRAINT [FK_dbo.Collaborators_dbo.Users_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[Addresses] ADD CONSTRAINT [FK_dbo.Addresses_dbo.Cities_CityId] FOREIGN KEY ([CityId]) REFERENCES [dbo].[Cities] ([Id])
ALTER TABLE [dbo].[Addresses] ADD CONSTRAINT [FK_dbo.Addresses_dbo.States_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[States] ([Id])
ALTER TABLE [dbo].[Addresses] ADD CONSTRAINT [FK_dbo.Addresses_dbo.Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([Id])
ALTER TABLE [dbo].[Cities] ADD CONSTRAINT [FK_dbo.Cities_dbo.States_StateId] FOREIGN KEY ([StateId]) REFERENCES [dbo].[States] ([Id])
ALTER TABLE [dbo].[States] ADD CONSTRAINT [FK_dbo.States_dbo.Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Countries] ([Id])
ALTER TABLE [dbo].[Organizations] ADD CONSTRAINT [FK_dbo.Organizations_dbo.Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Addresses] ([Id])
ALTER TABLE [dbo].[Organizations] ADD CONSTRAINT [FK_dbo.Organizations_dbo.Holdings_HoldingId] FOREIGN KEY ([HoldingId]) REFERENCES [dbo].[Holdings] ([Id])
ALTER TABLE [dbo].[Organizations] ADD CONSTRAINT [FK_dbo.Organizations_dbo.Users_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[Holdings] ADD CONSTRAINT [FK_dbo.Holdings_dbo.Users_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[HoldingDescriptions] ADD CONSTRAINT [FK_dbo.HoldingDescriptions_dbo.Holdings_HoldingId] FOREIGN KEY ([HoldingId]) REFERENCES [dbo].[Holdings] ([Id])
ALTER TABLE [dbo].[HoldingDescriptions] ADD CONSTRAINT [FK_dbo.HoldingDescriptions_dbo.Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id])
ALTER TABLE [dbo].[OrganizationActivities] ADD CONSTRAINT [FK_dbo.OrganizationActivities_dbo.Activities_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [dbo].[Activities] ([Id])
ALTER TABLE [dbo].[OrganizationActivities] ADD CONSTRAINT [FK_dbo.OrganizationActivities_dbo.Organizations_OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organizations] ([Id])
ALTER TABLE [dbo].[OrganizationDescriptions] ADD CONSTRAINT [FK_dbo.OrganizationDescriptions_dbo.Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id])
ALTER TABLE [dbo].[OrganizationDescriptions] ADD CONSTRAINT [FK_dbo.OrganizationDescriptions_dbo.Organizations_OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organizations] ([Id])
ALTER TABLE [dbo].[OrganizationInterests] ADD CONSTRAINT [FK_dbo.OrganizationInterests_dbo.Interests_InterestId] FOREIGN KEY ([InterestId]) REFERENCES [dbo].[Interests] ([Id])
ALTER TABLE [dbo].[OrganizationInterests] ADD CONSTRAINT [FK_dbo.OrganizationInterests_dbo.Organizations_OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organizations] ([Id])
ALTER TABLE [dbo].[OrganizationRestrictionSpecifics] ADD CONSTRAINT [FK_dbo.OrganizationRestrictionSpecifics_dbo.Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id])
ALTER TABLE [dbo].[OrganizationRestrictionSpecifics] ADD CONSTRAINT [FK_dbo.OrganizationRestrictionSpecifics_dbo.Organizations_OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organizations] ([Id])
ALTER TABLE [dbo].[OrganizationTargetAudiences] ADD CONSTRAINT [FK_dbo.OrganizationTargetAudiences_dbo.Organizations_OrganizationId] FOREIGN KEY ([OrganizationId]) REFERENCES [dbo].[Organizations] ([Id])
ALTER TABLE [dbo].[OrganizationTargetAudiences] ADD CONSTRAINT [FK_dbo.OrganizationTargetAudiences_dbo.TargetAudiences_TargetAudienceId] FOREIGN KEY ([TargetAudienceId]) REFERENCES [dbo].[TargetAudiences] ([Id])
ALTER TABLE [dbo].[CollaboratorEditionParticipations] ADD CONSTRAINT [FK_dbo.CollaboratorEditionParticipations_dbo.Collaborators_CollaboratorId] FOREIGN KEY ([CollaboratorId]) REFERENCES [dbo].[Collaborators] ([Id])
ALTER TABLE [dbo].[CollaboratorEditionParticipations] ADD CONSTRAINT [FK_dbo.CollaboratorEditionParticipations_dbo.Editions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [dbo].[Editions] ([Id])
ALTER TABLE [dbo].[CollaboratorJobTitles] ADD CONSTRAINT [FK_dbo.CollaboratorJobTitles_dbo.Collaborators_CollaboratorId] FOREIGN KEY ([CollaboratorId]) REFERENCES [dbo].[Collaborators] ([Id])
ALTER TABLE [dbo].[CollaboratorJobTitles] ADD CONSTRAINT [FK_dbo.CollaboratorJobTitles_dbo.Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id])
ALTER TABLE [dbo].[CollaboratorMiniBios] ADD CONSTRAINT [FK_dbo.CollaboratorMiniBios_dbo.Collaborators_CollaboratorId] FOREIGN KEY ([CollaboratorId]) REFERENCES [dbo].[Collaborators] ([Id])
ALTER TABLE [dbo].[CollaboratorMiniBios] ADD CONSTRAINT [FK_dbo.CollaboratorMiniBios_dbo.Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id])
ALTER TABLE [dbo].[CommissionEvaluations] ADD CONSTRAINT [FK_dbo.CommissionEvaluations_dbo.Users_EvaluatorUserId] FOREIGN KEY ([EvaluatorUserId]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[CommissionEvaluations] ADD CONSTRAINT [FK_dbo.CommissionEvaluations_dbo.Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id])
ALTER TABLE [dbo].[Projects] ADD CONSTRAINT [FK_dbo.Projects_dbo.ProjectTypes_ProjectTypeId] FOREIGN KEY ([ProjectTypeId]) REFERENCES [dbo].[ProjectTypes] ([Id])
ALTER TABLE [dbo].[Projects] ADD CONSTRAINT [FK_dbo.Projects_dbo.AttendeeOrganizations_SellerAttendeeOrganizationId] FOREIGN KEY ([SellerAttendeeOrganizationId]) REFERENCES [dbo].[AttendeeOrganizations] ([Id])
ALTER TABLE [dbo].[ProjectAdditionalInformations] ADD CONSTRAINT [FK_dbo.ProjectAdditionalInformations_dbo.Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id])
ALTER TABLE [dbo].[ProjectAdditionalInformations] ADD CONSTRAINT [FK_dbo.ProjectAdditionalInformations_dbo.Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id])
ALTER TABLE [dbo].[ProjectBuyerEvaluations] ADD CONSTRAINT [FK_dbo.ProjectBuyerEvaluations_dbo.AttendeeOrganizations_BuyerAttendeeOrganizationId] FOREIGN KEY ([BuyerAttendeeOrganizationId]) REFERENCES [dbo].[AttendeeOrganizations] ([Id])
ALTER TABLE [dbo].[ProjectBuyerEvaluations] ADD CONSTRAINT [FK_dbo.ProjectBuyerEvaluations_dbo.Users_BuyerEvaluationUserId] FOREIGN KEY ([BuyerEvaluationUserId]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[ProjectBuyerEvaluations] ADD CONSTRAINT [FK_dbo.ProjectBuyerEvaluations_dbo.Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id])
ALTER TABLE [dbo].[ProjectBuyerEvaluations] ADD CONSTRAINT [FK_dbo.ProjectBuyerEvaluations_dbo.ProjectEvaluationRefuseReasons_ProjectEvaluationRefuseReasonId] FOREIGN KEY ([ProjectEvaluationRefuseReasonId]) REFERENCES [dbo].[ProjectEvaluationRefuseReasons] ([Id])
ALTER TABLE [dbo].[ProjectBuyerEvaluations] ADD CONSTRAINT [FK_dbo.ProjectBuyerEvaluations_dbo.ProjectEvaluationStatuses_ProjectEvaluationStatusId] FOREIGN KEY ([ProjectEvaluationStatusId]) REFERENCES [dbo].[ProjectEvaluationStatuses] ([Id])
ALTER TABLE [dbo].[ProjectBuyerEvaluations] ADD CONSTRAINT [FK_dbo.ProjectBuyerEvaluations_dbo.Users_SellerUserId] FOREIGN KEY ([SellerUserId]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[Negotiations] ADD CONSTRAINT [FK_dbo.Negotiations_dbo.ProjectBuyerEvaluations_ProjectBuyerEvaluationId] FOREIGN KEY ([ProjectBuyerEvaluationId]) REFERENCES [dbo].[ProjectBuyerEvaluations] ([Id])
ALTER TABLE [dbo].[Negotiations] ADD CONSTRAINT [FK_dbo.Negotiations_dbo.Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [dbo].[Rooms] ([Id])
ALTER TABLE [dbo].[Negotiations] ADD CONSTRAINT [FK_dbo.Negotiations_dbo.Users_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[AttendeeNegotiationCollaborators] ADD CONSTRAINT [FK_dbo.AttendeeNegotiationCollaborators_dbo.AttendeeCollaborators_AttendeeCollaboratorId] FOREIGN KEY ([AttendeeCollaboratorId]) REFERENCES [dbo].[AttendeeCollaborators] ([Id])
ALTER TABLE [dbo].[AttendeeNegotiationCollaborators] ADD CONSTRAINT [FK_dbo.AttendeeNegotiationCollaborators_dbo.Negotiations_NegotiationId] FOREIGN KEY ([NegotiationId]) REFERENCES [dbo].[Negotiations] ([Id])
ALTER TABLE [dbo].[ProjectEvaluationRefuseReasons] ADD CONSTRAINT [FK_dbo.ProjectEvaluationRefuseReasons_dbo.ProjectTypes_ProjectTypeId] FOREIGN KEY ([ProjectTypeId]) REFERENCES [dbo].[ProjectTypes] ([Id])
ALTER TABLE [dbo].[ProjectImageLinks] ADD CONSTRAINT [FK_dbo.ProjectImageLinks_dbo.Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id])
ALTER TABLE [dbo].[ProjectInterests] ADD CONSTRAINT [FK_dbo.ProjectInterests_dbo.Interests_InterestId] FOREIGN KEY ([InterestId]) REFERENCES [dbo].[Interests] ([Id])
ALTER TABLE [dbo].[ProjectInterests] ADD CONSTRAINT [FK_dbo.ProjectInterests_dbo.Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id])
ALTER TABLE [dbo].[ProjectLogLines] ADD CONSTRAINT [FK_dbo.ProjectLogLines_dbo.Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id])
ALTER TABLE [dbo].[ProjectLogLines] ADD CONSTRAINT [FK_dbo.ProjectLogLines_dbo.Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id])
ALTER TABLE [dbo].[ProjectProductionPlans] ADD CONSTRAINT [FK_dbo.ProjectProductionPlans_dbo.Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id])
ALTER TABLE [dbo].[ProjectProductionPlans] ADD CONSTRAINT [FK_dbo.ProjectProductionPlans_dbo.Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id])
ALTER TABLE [dbo].[ProjectSummaries] ADD CONSTRAINT [FK_dbo.ProjectSummaries_dbo.Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id])
ALTER TABLE [dbo].[ProjectSummaries] ADD CONSTRAINT [FK_dbo.ProjectSummaries_dbo.Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id])
ALTER TABLE [dbo].[ProjectTargetAudiences] ADD CONSTRAINT [FK_dbo.ProjectTargetAudiences_dbo.Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id])
ALTER TABLE [dbo].[ProjectTargetAudiences] ADD CONSTRAINT [FK_dbo.ProjectTargetAudiences_dbo.TargetAudiences_TargetAudienceId] FOREIGN KEY ([TargetAudienceId]) REFERENCES [dbo].[TargetAudiences] ([Id])
ALTER TABLE [dbo].[ProjectTeaserLinks] ADD CONSTRAINT [FK_dbo.ProjectTeaserLinks_dbo.Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id])
ALTER TABLE [dbo].[ProjectTitles] ADD CONSTRAINT [FK_dbo.ProjectTitles_dbo.Languages_LanguageId] FOREIGN KEY ([LanguageId]) REFERENCES [dbo].[Languages] ([Id])
ALTER TABLE [dbo].[ProjectTitles] ADD CONSTRAINT [FK_dbo.ProjectTitles_dbo.Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([Id])
ALTER TABLE [dbo].[Messages] ADD CONSTRAINT [FK_dbo.Messages_dbo.Editions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [dbo].[Editions] ([Id])
ALTER TABLE [dbo].[Messages] ADD CONSTRAINT [FK_dbo.Messages_dbo.Users_RecipientId] FOREIGN KEY ([RecipientId]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[Messages] ADD CONSTRAINT [FK_dbo.Messages_dbo.Users_SenderId] FOREIGN KEY ([SenderId]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[UserUnsubscribedLists] ADD CONSTRAINT [FK_dbo.UserUnsubscribedLists_dbo.SubscribeLists_SubscribeListId] FOREIGN KEY ([SubscribeListId]) REFERENCES [dbo].[SubscribeLists] ([Id])
ALTER TABLE [dbo].[UserUnsubscribedLists] ADD CONSTRAINT [FK_dbo.UserUnsubscribedLists_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[CartoonProjects] ADD CONSTRAINT [FK_dbo.CartoonProjects_dbo.CartoonProjectFormats_CartoonProjectFormatId] FOREIGN KEY ([CartoonProjectFormatId]) REFERENCES [dbo].[CartoonProjectFormats] ([Id])
ALTER TABLE [dbo].[CartoonProjectCreators] ADD CONSTRAINT [FK_dbo.CartoonProjectCreators_dbo.CartoonProjects_CartoonProjectId] FOREIGN KEY ([CartoonProjectId]) REFERENCES [dbo].[CartoonProjects] ([Id])
ALTER TABLE [dbo].[CartoonProjectOrganizations] ADD CONSTRAINT [FK_dbo.CartoonProjectOrganizations_dbo.Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Addresses] ([Id])
ALTER TABLE [dbo].[CartoonProjectOrganizations] ADD CONSTRAINT [FK_dbo.CartoonProjectOrganizations_dbo.CartoonProjects_CartoonProjectId] FOREIGN KEY ([CartoonProjectId]) REFERENCES [dbo].[CartoonProjects] ([Id])
ALTER TABLE [dbo].[AttendeeCollaboratorActivities] ADD CONSTRAINT [FK_dbo.AttendeeCollaboratorActivities_dbo.Activities_ActivityId] FOREIGN KEY ([ActivityId]) REFERENCES [dbo].[Activities] ([Id])
ALTER TABLE [dbo].[AttendeeCollaboratorActivities] ADD CONSTRAINT [FK_dbo.AttendeeCollaboratorActivities_dbo.AttendeeCollaborators_AttendeeCollaboratorId] FOREIGN KEY ([AttendeeCollaboratorId]) REFERENCES [dbo].[AttendeeCollaborators] ([Id])
ALTER TABLE [dbo].[AttendeeCollaboratorTargetAudiences] ADD CONSTRAINT [FK_dbo.AttendeeCollaboratorTargetAudiences_dbo.AttendeeCollaborators_AttendeeCollaboratorId] FOREIGN KEY ([AttendeeCollaboratorId]) REFERENCES [dbo].[AttendeeCollaborators] ([Id])
ALTER TABLE [dbo].[AttendeeCollaboratorTargetAudiences] ADD CONSTRAINT [FK_dbo.AttendeeCollaboratorTargetAudiences_dbo.TargetAudiences_TargetAudienceId] FOREIGN KEY ([TargetAudienceId]) REFERENCES [dbo].[TargetAudiences] ([Id])
ALTER TABLE [dbo].[Logistics] ADD CONSTRAINT [FK_dbo.Logistics_dbo.AttendeeLogisticSponsors_AccommodationAttendeeLogisticSponsorId] FOREIGN KEY ([AccommodationAttendeeLogisticSponsorId]) REFERENCES [dbo].[AttendeeLogisticSponsors] ([Id])
ALTER TABLE [dbo].[Logistics] ADD CONSTRAINT [FK_dbo.Logistics_dbo.AttendeeLogisticSponsors_AirfareAttendeeLogisticSponsorId] FOREIGN KEY ([AirfareAttendeeLogisticSponsorId]) REFERENCES [dbo].[AttendeeLogisticSponsors] ([Id])
ALTER TABLE [dbo].[Logistics] ADD CONSTRAINT [FK_dbo.Logistics_dbo.AttendeeLogisticSponsors_AirportTransferAttendeeLogisticSponsorId] FOREIGN KEY ([AirportTransferAttendeeLogisticSponsorId]) REFERENCES [dbo].[AttendeeLogisticSponsors] ([Id])
ALTER TABLE [dbo].[Logistics] ADD CONSTRAINT [FK_dbo.Logistics_dbo.AttendeeCollaborators_AttendeeCollaboratorId] FOREIGN KEY ([AttendeeCollaboratorId]) REFERENCES [dbo].[AttendeeCollaborators] ([Id])
ALTER TABLE [dbo].[Logistics] ADD CONSTRAINT [FK_dbo.Logistics_dbo.Users_CreateUserId] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[AttendeeLogisticSponsors] ADD CONSTRAINT [FK_dbo.AttendeeLogisticSponsors_dbo.Editions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [dbo].[Editions] ([Id])
ALTER TABLE [dbo].[AttendeeLogisticSponsors] ADD CONSTRAINT [FK_dbo.AttendeeLogisticSponsors_dbo.LogisticSponsors_LogisticSponsorId] FOREIGN KEY ([LogisticSponsorId]) REFERENCES [dbo].[LogisticSponsors] ([Id])
ALTER TABLE [dbo].[LogisticAccommodations] ADD CONSTRAINT [FK_dbo.LogisticAccommodations_dbo.AttendeePlaces_AttendeePlaceId] FOREIGN KEY ([AttendeePlaceId]) REFERENCES [dbo].[AttendeePlaces] ([Id])
ALTER TABLE [dbo].[LogisticAccommodations] ADD CONSTRAINT [FK_dbo.LogisticAccommodations_dbo.Logistics_LogisticId] FOREIGN KEY ([LogisticId]) REFERENCES [dbo].[Logistics] ([Id])
ALTER TABLE [dbo].[AttendeePlaces] ADD CONSTRAINT [FK_dbo.AttendeePlaces_dbo.Editions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [dbo].[Editions] ([Id])
ALTER TABLE [dbo].[AttendeePlaces] ADD CONSTRAINT [FK_dbo.AttendeePlaces_dbo.Places_PlaceId] FOREIGN KEY ([PlaceId]) REFERENCES [dbo].[Places] ([Id])
ALTER TABLE [dbo].[Places] ADD CONSTRAINT [FK_dbo.Places_dbo.Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Addresses] ([Id])
ALTER TABLE [dbo].[LogisticAirfares] ADD CONSTRAINT [FK_dbo.LogisticAirfares_dbo.Logistics_LogisticId] FOREIGN KEY ([LogisticId]) REFERENCES [dbo].[Logistics] ([Id])
ALTER TABLE [dbo].[LogisticTransfers] ADD CONSTRAINT [FK_dbo.LogisticTransfers_dbo.AttendeePlaces_FromAttendeePlaceId] FOREIGN KEY ([FromAttendeePlaceId]) REFERENCES [dbo].[AttendeePlaces] ([Id])
ALTER TABLE [dbo].[LogisticTransfers] ADD CONSTRAINT [FK_dbo.LogisticTransfers_dbo.Logistics_LogisticId] FOREIGN KEY ([LogisticId]) REFERENCES [dbo].[Logistics] ([Id])
ALTER TABLE [dbo].[LogisticTransfers] ADD CONSTRAINT [FK_dbo.LogisticTransfers_dbo.AttendeePlaces_ToAttendeePlaceId] FOREIGN KEY ([ToAttendeePlaceId]) REFERENCES [dbo].[AttendeePlaces] ([Id])
ALTER TABLE [dbo].[Connections] ADD CONSTRAINT [FK_dbo.Connections_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[Quizzes] ADD CONSTRAINT [FK_dbo.Quizzes_dbo.Editions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [dbo].[Editions] ([Id])
ALTER TABLE [dbo].[QuizQuestion] ADD CONSTRAINT [FK_dbo.QuizQuestion_dbo.Quizzes_QuizId] FOREIGN KEY ([QuizId]) REFERENCES [dbo].[Quizzes] ([Id])
ALTER TABLE [dbo].[QuizOption] ADD CONSTRAINT [FK_dbo.QuizOption_dbo.QuizAnswer_Id] FOREIGN KEY ([Id]) REFERENCES [dbo].[QuizAnswer] ([Id])
ALTER TABLE [dbo].[QuizOption] ADD CONSTRAINT [FK_dbo.QuizOption_dbo.QuizQuestion_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[QuizQuestion] ([Id])
ALTER TABLE [dbo].[QuizAnswer] ADD CONSTRAINT [FK_dbo.QuizAnswer_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[SalesPlatformWebhookRequests] ADD CONSTRAINT [FK_dbo.SalesPlatformWebhookRequests_dbo.SalesPlatforms_SalesPlatformId] FOREIGN KEY ([SalesPlatformId]) REFERENCES [dbo].[SalesPlatforms] ([Id])
ALTER TABLE [dbo].[UserRole] ADD CONSTRAINT [FK_dbo.UserRole_dbo.Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id])
ALTER TABLE [dbo].[UserRole] ADD CONSTRAINT [FK_dbo.UserRole_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[WeConnectPublications] ADD CONSTRAINT [FK_dbo.WeConnectPublications_dbo.SocialMediaPlatforms_SocialMediaPlatformId] FOREIGN KEY ([SocialMediaPlatformId]) REFERENCES [dbo].[SocialMediaPlatforms] ([Id])
ALTER TABLE [dbo].[UsersRoles] ADD CONSTRAINT [FK_dbo.UsersRoles_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[UsersRoles] ADD CONSTRAINT [FK_dbo.UsersRoles_dbo.Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id])
CREATE TABLE [dbo].[__MigrationHistory] (
    [MigrationId] [nvarchar](150) NOT NULL,
    [ContextKey] [nvarchar](300) NOT NULL,
    [Model] [varbinary](max) NOT NULL,
    [ProductVersion] [nvarchar](32) NOT NULL,
    CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])
)
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'202408202223049_Initial', N'PlataformaRio2C.Infra.Data.Context.Migrations.Configuration',  0x1F8B0800000000000400ECBDEB72DC48922EF87FCDF61D64FAB9764EA95535973E63DD7B8CA22EC53E624943525536E70F0DCC0C911821811C5C58C57AB5FDB18FB4AFB0B86602018F08F7B80149858D4D359570F7F0F0F0F8DCE3FEFFFD3FFFEFDFFEE71FBBE4C523CB8B384BFFFEF2F50F7F79F982A59B6C1BA7F77F7F59955FFFFB5F5FFECFFFFBFFFC3FFEF66EBBFBE3C5AF03DD4F0D5DCD99167F7FF95096FB7F7BF5AAD83CB05D54FCB08B377956645FCB1F36D9EE55B4CD5EFDF897BFFC8F57AF5FBF62B58897B5AC172FFE7655A565BC63ED3FEA7F9E67E986EDCB2A4A2EB32D4B8AFEF7FACB752BF5C52FD18E15FB68C3FEFEF2731295D1D72CDF455771F6E3F90F17E9D73CFAE16DFDE30FB59892FD51BE7C7196C451ADD9354BBEBE7C11A569564665ADF7BF7D29D8759967E9FDF5BEFE214A6E9EF6ACA6FB1A2505EBEBF36F47726CD5FEF26353B55747C641D4A62ACA6C4714F8FAA7DE56AF78762D8BBF3CD8B2B6E6BBDAEAE55353EBD6A27F7F79B629E3C7FAA7972FF8C2FEED3CC91BC2B9C1DFD66471FA432B2B66C50F838CFFF602A6FC6F07B7A9BDABF9BFFFF6E2BC4ACA2A677F4F5955E6515273567749BCF95FECE926FBC6D2BFA755928C15AF55AFBF4D7EA87FFA9C677B96974F57EC6B5F9D8BEDCB17AFA67CAF78C603DB88A7ABE8455AFEF4E3CB17BFD4854777093BF8C5C828D76596B30F2C657954B2EDE7A82C599E3632586BD959E95C59F53FFF936DCAA63875B17251CD7F0709B54FD7DDF5E58BCBE88F8F2CBD2F1FFEFEF29FEBFEF93EFE836D871F7AA15FD2B8EEDC354F9957CA32DEC6C53E899E3EE55B969B69FB73549C6DB77163C328A97B6C36887B9365098B52B2C02FF1C17E1FAAE66F22FF45F19625AC6E435345CE7356BB420D3F87D668FEBEA9C1EDD3D7AF052B3505D648959BBAC897FDD6AE6A9D402DD57E891EE3FBB60F89BBC4CB17572C69698A8778DFC1F7015D6E2784EFF36C77952523041B7FBFBDCEAA7CD3D43A9310DD44F97D6384B1A67F7B75844829704EB4D1C7CE9198009F4AF8B483790169BE4FA4E941A0EE76F68066C01029D00C688455B4AE28CB59517EC8B36A0F2B3B2111692C249AA92DA6A4EAFE29BF8FD2F8CFF6539B5D83EAF354B72D4D8D08704510E4B32A6178A0CAA123C0C466263160222844811525D1AFDD4494CEBDC5A5FEC549A9F692F98BE23CDBEDE2A268C7A4218F9709FC6EA2AB2A8F378C587C46AF8E6D5AA0CE470D135CE76505685F67823F4F0C4C3BEC4551BB7C7284D9008EDF39389ED5FD20DD3286CB8E45D4B773741A0D4FB04CF3310B9A933A1880B27A3FA3013E5C5046105A81436444A3897581CC1048948104329D29D2F0CD601E25CEF6719F95BF4B1B46639CAE05FE1CDF3F24F5FF979FB3222E47197AAF648838DF6DC4A1051B580222E0401FE633F2246E089A29B3504B8459748D8501DA5A20B21D8442005206A077DD62A7CD98632C2BBDCBA2BCD9D8715D4679290330ACA0F7711A170F36248D2B6A43DE303F50E80BEB73C3EBEAAE9FEBB2A3633334EC4517E759959686D12844ECE71BB1CFB3465856435096E3C78A632EAD30AE14801A43AAA550C7939686D176331BF470DA8A09FAB882AEF0ED81415EBF810E95B61C884DF2327C1528ED740B5A575A19A34CB347F237D513CBDF3D4649D5D508AC1C4C7BDBFE5BE59354DE995B9205503D731CD764D5BFEDE646911596508BAA2863315AF155819CED347B2C3BA4DC8BCCF90C32C74D619C8D848CED79656C530C3049D2A6923492B4C98F98E8A796428D8846334F1E52576DAB28F3047220B11D3C42C058628EC6666CF88D259B6CC7DED52D955CD76D6A632A6485B33D0D0EDB903336BD8359A8E8FA5BBCDF33835638ABB671F618175594D4DDB14EB56F58BE2BCE36CD69A728DD48239C220BA8BBC16387BF76055F5645BCB12CB3FEE7B6DA581478BD67D1378BF2CE1EEB0E17DDC5490D1B6FD87D6C30BF3716F5CEA4079FDDD7001059808290703EB784B306F4ACFDDA8C77F19384623E74064A14219C1DA3CAD19D2A1C0B511D0010B33CE1ED8365165B062DC1864D8E6164325F99479B6F782B498468198E2A0F654BB2503BE6ED97A108B6EC38340D2761465A4926C186493A2D9AC488D5211B6F98299F967910225046C2C8B162AA78F38D117CA7A3D7338D9815671209BF1553289779786A3D33881871461072EB9A00062D7CC457F3A3CDA4294A68385D79BAA66CC7356FA2748BB71EC88236189E5B682382085DB3FCC2EEB33226BA9580096D1A0ABFD038242136969A2DAEC6DB9DE8D55E8D3732117ADA7B520A058D6F41FD64087C0BDA055FA3F46B9DFDD431FD733D3C8937F13E4A05E1172455362F9E6BD6A60456177B0A260548F6144074B896D3DD53F031BB8F8BDA20703B0D5F954D23259CB5869CDACA2AB378846C65A940283D2C1CA0579AA7460C6BCD61EACFF3D49FF16C1F274D6FBE6F4222067BB224DD95677C62E27F3ED4C43AAAD4C630C4D80F2B2194785B83B61A893EE4D1F608866C13EFA2E4E58BCF79FD577F4DE45F5FBEB8DE448D40E591B2D1AE480BDBDD3F46457994A8BF2676941116D84294F5B9C0E620E49A2CB2C1C1DBCC60AA8DD02A2E6D632945204DA596439E2CD1CDDB6EA936B8D5CAC7782E6AEE859A439816219B450029B15591CD2418E64847C7B09F2D1D6587BC69A12178DF04596E23E211B3A830B20F39875684F0123771D08B8FBFE8B832EE917A46E144106C31E1A49B60CA6E148D3AD5F5234EC31FA28AE9FD638EEE836C777931531C6E9A58A1FF8FFFFC2F2EF46F47D0168A4594D2AC82C5F9CE3C6A7D8E8AE2F72CDFFE1C150F12D5FFFA17E4659CB8D27E61BF4B0AB37267FF35DB5479EDEAD765B4DB7BA8D94396B25FAADDDDF1B63B2F855973849BDFB3F7D1A6860D4B57307DCC36DFB2AA7CD7CD277D29377CEA811660E746A8CD8615C5FBBAEBB0AD8DDB269AD4A7D922F835DAB08F517A5F45F7F38BB0BED7CCD53CDFF43DD1622B37224FABC0A915D91A4D0B6639D51A022E8435509C626BE0D8ED6E16C45A45C5ADB60E4D027163A0756B1DF6D6610D0430A86DA26452EFFDB35673E5BAE8648698AFD1EC23B04589A7A06B38DC2BAE6A148852DC1A6A6AA0324A1672EDDA7883D87F3ADDD1D68186B029FAEFF2D61888E837B86EE27D5C0F9E2EEB2CA20EF5B0BEFDC7DB03F558D9D9C799A6730AB29AB51C58B5B6A1FACF479D8EBFCE46F5A34FD4B98A2EFC53DBB7E39274B5EEBBA2B7F54454C3F52AFF9C25CDF15758DBFE23A428F769A623FF5D53BDD14E5F58C51101A426F079A62A44A3A9EE386CA9DF3F811486BE4B5F38D157194AE8C53D4940CEF52C900AEE693029B9E7D552BEA44575576CF2F8AE1E64C6A2235710E52CD40989666D20A6B4B3075490305A597C12C80ED384F8C5A78909C3E2D33318C27FAF8B4F134FD61B3D8B641186D1308178E58528C7E1E293EB2905B2092C2E3E891AD6760C0A914719792C471C3B9B4FC376D1B05D34444F7BD3D0EE022979625A119049335F3AE9C52DD506B75AA903CFE564F7E3B408D9EE4790125B152BBB1FED85FA10E2EDEE41F9672B2BE96FB34DB563C7E8EA6A37826A6B8895429A8BD24A85D97EF2B8DBC14AA5CEAB3C8F375552ED2425FDAB95A26EE232716FBB8FD97D12A7EECB79CB9AB9B87D07AEB26E64A3B0CBAC8CBBB5CA9BEC2D7B644926DB73F393FD426FF2282D1A38755FECDBF8B1C1E8F2E92CDD5EA49BA46A17C712D6E048E1BEF89B87BAA43ABEB2C748D1B6B6CC1CE5DF58D95EA0DA20F9FD93FB22AFE35D9C44F9F8D6D0C38DFAAECBFE9436DDB309CF8D3B15BF3DB09C0DE3CCA2BFD4D2034C706A0C976C5DB16823DBA86709DFBBFA9EFD1EE55B85C9ED95D6E535BFD49D7AE32182E5EC31CEAA2279EAE1EAF006A6FBFA9E1545B6899BA4E8222DEA54A8EAC748AE8B6DAE4B49D8FB38615FF64916995CC97A14F5EE8F3AFB2EE440F4DA4A2290C4FB7D7BFBB2B9FE6359DE2AF0A9ACA1C486F60741FE55C72515FF6427518AD36FB25EF1AF76C229FEEAE4B03814A6B78E4378CAA496F1F40D6EC24A34E9A3373D25BFB315A6955414C530DFDD83E232DA640017616FAE679018E67C3C2FEB0C8637BE906ADB4D2546C945FA35733DE111C2C633091B88C97E4720CACF90D3A0175BBF2352E26B76E451D6E9F007B236873F8C26FC6DC07F007C34E00FA6FA9067D5DE1415DEC6C53E899E3EE5DBE3B4B49EA49FA302067D5D2C55CCCA3B3A501B42C9330925E09598FA4F2A40288C62A03D9D3043706A64695101ACDEAC8C9EF4581F9862164C0464D69FB5B31157647243ACC1EF56B678AF6C186284B8B0B2B8B086177630179DE3DEE8B1321871140C49B5B4333C39EC96D28F22BD881030CC7720D980E23C39CF8EBB8635A350D1EC92196D63D2C5E38BC2CE1D3CCAA756A91BB4E5DB8CA9DAB124B1AD6123D3AE969F527693D5FFB964EDE68BE27AF3C0B655623324F5AF871667FBF8E7F8FE21A9FFBFFC9C15B1AD5DE7FD14CD7575B78B4BDB169F08B76BFACFFCFE5F47BA8F76D35BD57F7486D7B6E623D176751EEDF5191F73EE123AC3A79BA5B215D5C08D29C66790EB10D1A0C1B04C68AF1FD582DF544F2CB77B3444609C5FE3BCAC7F1AD0E71F599C5EC66955B26254DCEB7F31290EBA5AC2B6BF2A8A73E6C3C7C26AABC5BB6AE7ACD14625457F34259DEDF779F678DCCF64A1B8EE2DECB8DC3CD4BE70916EE3C778DB3887754F9F14D465BF8E0A7953FF2765457155CBDB16AE6AE23200CE4BB0EBCDAD7C1F7D545890D3FA38E9977C19D31ED9DC6264A394D19BF77D57B1EFBFC7325CF794516D5C76174131767DEC58888F8E232FCD7DCD9C7421B0A0693FAA3FECEB748BD9286E7A331FD2F5E8228DB2CC5EA0964B694BB6AB31CE55A842F1090F65DF911BFC81CAB0DB45FB127C208FA428C775728239F3529CA5D261EDE519AFBDE0D7E2C96FC76256DE6747A4A97551DFD80651CAEA02D1A1DE6936AECB7594B0E2707E4A5A9909A9AC3620A1B03A30B5E6ABC4EF1E99E80DE63105A43DF47DA6344864E5F2B689196CEC7F98080CEB58BEDE569C98DDAAB0D6E98E71D1D55A9BADD5ADE6329D89FEED8E43285C8564E03B4B06266E71136FBEB1B22980127ED4A25471492541B8718128C6C5ED3086A1185733D9D530B20A70910CA806C2829306C4D0CF42358AC97EE83E36BDF5207E141DC2397A1FA3D5783C1E4634724CE5752D7A9E444571210BEC3FD9B9B8E5509862CFCE8F56CE0C87B0FDCCC2F6C4FB5B67C28FE43B7A55B4548DF3715250B300485156C6D5EBCA6B700371747A843EE8C761A7F2AD83D6FE147B9099A5AF24E024D8B9B87DE697B60F40745243DAB0C8F1078513599D24383C8B244B27FEC9CAED53E362ADC7B1F7715EA86EE97B6DE70295A82B4876898A9D84E8BCD911D0DC09E87A0AE71FD99DEA963E3B357A13E59BD1BE6C7753527D419F6BF91612C283BC3A4F3216368852F6004C76FE2597DD7F69E7C6A08BA22BCB9A317BD52DD8B2976468CA30F078C6030FDD1107E5D8968415759C49C64F9D4D538E98D63800D330926A2CA79560CFC71C06D71571B242326D7A34CCCE4BDFD81B7C2D14D538B331F086E0F47C83937A156B36C33087280502434CB8192F90D3CA04976A0DCFF3B4CEEC2E24ED8921AC559A727035EF2825B56B7E56D7A0A5B27EFF86AD65AB102B5731F1647BB9EAA23903DBDF1FF52E6D188DA31074A896B4299970336D08CCDF6D60A6C564AD31A38811371812725323116A99C5713682AEB1308FD18A685D68D58F5E0D7F8854ABB8F0C3D9A00EED4CD6F6E3867DB856DDCBCEA2CF6FECEE21CBBE5DB34D95D73AD5F5378AFD75163312E16CB9A52EA6D63867B287B8EC94540BAC7FD9B0A2A8E55B38CF3318FABA8C76B2378FC2C56D2193D33AA4E2644B299FC790F6A16A459DF1F9120BD797B57242CCF175F6C34766B4EE4BC302F43E13E83DCFD2AF2C6FDE5213CDE80EDF6FA798359ED90449804D77309DE611407B87FF78F8979E10D45C9F1CAA6EB632394809508F85FAB6114D7BF25596991FF10B781EF0DC279EBF7D4AA35DBC51C17A4F763B862808DBE77412800788E9DBC807D6CF51F3BE62BC8F4427BE41526585605A49A5040C06158BEB4616DC92C053A9ABC393C96A32A335A844CE8A1A625BA2F74D3C5237D28C435D39198BACA2523EFD4A5F3FA5D9BE50A64C1D595C28EB07104A6A0551EBD7A5DD41ACAA494BA4AC064F25A9C38CD4A00279B4F9A6AC4043A4AE004725AB004F6A729385EDD47BBEAD409EA2E3B73F64F021A751011D0DA863F349A65BFBDD52A2DD87203BF9762F2CA4DDEA370B0F36334DB73E46E97D15DD1BCBF9354A2AC54243988C0E1934E0C1EE326731064AD26C6C05868E8354FF482E537EA042A87E203542F2A35AFA003EC808B8ED69B19FBC6FCDC699320DA0FD1AD5CD6C7E20CACE054A21723C93C8D14883C721CD97DBB6B0E6DDA3AFD18641A82BA69A8D4324A466AF7F43B31D7672E891C080C71EF368371B8E8156B5716AE6A2F89CB32B761F17CDFB60C68816A0F599402B7AAF2F3C5DABDAE88BE792E4BF4A56FA165FE448C4C2B437AE5A06A312216468D56D26035F558E955C739EDFD27CD5AC4696636ED8F46C631CF4573B17337C649BC6BA79886F7281DF4D7C137659D4EA0CC46300960819B865528CA005D7852D0611EAAA31C60AE641A4BF1CC84124692587704218C6591E28F95A1DB17377438853CF3D4EAD2F42A1F27A4AA8B3BCFE026A215F8C91B2502B6C679986DF546429DAB4B2427CF1384DD8993CDC7A13809E38F7A4BB3D510257C2AD8C58ED072CC2683ED04AB4EEFF47A971FF3F46806A0EA3013C57751AEC5FECDC299B25592E29E575D8C314D0DB748F391E0A957BCB39C8B471B6AB17099CEA9A7E99E134F7D956BE3BDB5B6E29F79DC90D50EE330F9E993FE4C401558939B1ED132E92CC137532069D37039846AD2924035963E02752CD819FCC7271AB101F807DB539FA3F87230001E89D9D6EB48B88A4D38E1244B59292CFC543E9B9906A9EAA8B492DA5EDC3E1493BC9FA202D207938192644A810169E5B587079EE5B9CF1CA4E895B5E9C3C14255F909C9161B4B7BDF06871974BD8D6F25CB13C3C751BA05C07CA352FBE10C3A0F08E0CCB088ED94F82DE40E266C7487B858625E06E4405E0F608DCADC5C3D478C053229EEADDC3238126D1953D58D57B144268DD538A15EEFEABD2B5FFAF09861A2367C0CB354D3F872D2201B197436CC2FD69480454DD9A36C5491B13D19D4460EE79F26106CDD3AF868FD26446CF8734FC01907D01F245F16B9C9755945C3256B640190E6206589C4095F23E74D5458792FBCF271721DAC0BF5620007FE3DF67E837F9484D9B7F61F75919B75F1A394DE5E27BD86620E9CC7C62AA992525A454A3364CCD5FB0E6C3D799B2930F33FDA65F8DCE768255358933A0C010789481C7C61DF023DB777637BF87A74ACBB3AA718532DEDC349C85058997515A87461BE2425C7C267171E6BA04A4077855B03F6399052F2C9FB52B8E8DA318AE06E6772003E6B6122E42A8F03B4659F78B215775A0D8BE8FF3A2BCA972FE29671D59D76C93A55B73614D9DDA5B111FA3E40D2B7F672C1D0B6D3E6BC9FCF4F55DB47968553596C5E9672E34C4D96712676583BC797003467C422259FCE128BD8F05AD6408B851A238B3D09EF6EB66F7CDA6FE1A1921B47A198585AB7302B22F80ECD27D3B87192368C3CEEC23389337A5B036EC414E810935321FCF0C77A95ED428F2D8AAF729BF8FD2F8CFF6EF77751A55455D14D487606C1901A295102D37A56957EF9B22CB6DE0C6873C3A3E40F0966DE25D94BC7CF139AFFF6ADBF2E5EBBFBE7C71BD891A813F06A0FE4E805AEEC0204862E1E35625FB08AF9644CE80D9965C6A8099745C332372A2346C3691A06FA2A9188741CE5D680B01CDDB960397F1B0F1CBBAD9926B269FE4F3180F8F9A15EDA292E9A0AD288F12F5AB78B6D954BB2A691CE18A3DB2B422D497A8F265947F63E575FCA76C90F89395AD7F6FAA224E59D1BC6C14A7FD9490A722DF673534792BEDDD264BB35DBCB9AC39137F668DBFB63B37CAB816E2ADD4EBB21D073A2E6D7CE2FACB3EC92203FC1834FFB46FA0B61618257E1AEAD778CBB22FB9FB8226E7D3E384BDFBA30EAE85BCBF219F319317FC5B5625DBFFC8AA8FF1B7E375AE251B2C3E5941D01D3808CAF81C979B070B7BE4E6D0FB3ECB1B5CBFF99D258FEC324BCB87228CC6C2688C301A1BBFAF032F38E0F9AD0DCE0842678B16F6245377C2A94ADEED599D2DEB9A79E0B66864A448B289B172ED1A78942E9FDA1403CDC0F4A90B4B06FEA3A68DC53B7BB1DCF60C8C154935305AAE5D03BF6F7210D10B9828566BA6C5C923DA1529D4AE513FDDFD276B5F99D531EB81D99A61B11289A6458BB56BDCEBAA28A3386DF292B775469A64FB5D9DDC1F94D1B0B842A2B576302B87D83A8685D96DB31BB67948B324BB8FB5BAC481FDC95A5BA04512CD8E976BD9C2E2F38008467B664548A35A1423D2E6B11C4551C0F62D1C0771C9427B6397B525B05B536F1008A01A82B0986569E966FAE4ACAB659C7129614967E13D0A2E5EDC0EF359CF6C3E4BF992B5C64C90E8796B4351449055CBA38621EB3B32169FF6B360D12542D93035E632900D658430B67018EBB6B53B5E500B71ED99C5358B18BDE09A01159FA96B112E36421FA67D1D6E843E9411D07961745635D0A7BD95A1CCB69B3988928BF46BE67A934308062118AC70F98DB83F98BCAC67361BC7777933EB62CBD0B0324EB4BEB591F28D4230D63CFA0118574208BFCAF0AB18BE5809586FE3629F444F9FF22DCBCD60FDE7A880636DB891EE3B88A5968600FD0E0577F97F5F40409F8593FFF735792AC7373B0F5E9C57791E6FAAA4DA498AFA572B370BFF96E5DFDEB26DBCB162A1007F27047F5E87120BED35A3A5B5C40D6C58634EFB9881F1784154634DF9358DC309314AECF90AE947D0A9A4102A9F59A21E02CB0905164B79F56847A8ABCCFA5044000CEDDC3AFAD3DDC4FAA17DC2BC7AC0ACE7930C2F78428096F391B7B69B4DAA1F8F5D684FAA1F35C6164137314EB2B6A991E21DCCA8CF8D637B469D2F21C4DD6796A88719F5EF398A5ACAFC9527C45C8D07140507B45AE5F61B45AB85B14340BDE733763891B3AEB4E4D7D2C15AB3D1070A461CB4989136B69AD34009CB6D6DA289833191A2606723255CB9212359C7F889159B3CEE3D210CD542D222B6D15A92164B43B5E37509EE4665C73202DCAD7200766CA030D60AB0F57CC65A8B5E5B43CBAAE9D7E1980D96F82E6F665D6C191A56C689D6B73652BE8391C9DC3CB6C7207C0921FCAE63B41186002196AE6808D0DCEFE530FB6FC407E45967E2DFB44DC8F9034E3DA39CDFFF758AC4DC937243A361927FECDEDA964448A619532950CB9E6AA92E32F8B115AC27EF47E1217AAE236F0FAB046DE83CEB77F118CA5179FD873CABF6331F0A79C17795174C6E03157B8CFCE265A410BD58674DB8F05A667B25506F69C6F551D0F63856B5CEC5D8E6D684CE6C6D4F326463F2481DD9E43646EDC8A2420E821EC1DBBCC1388CDB437C3E95F86C2506636F653691271C611A09F53B663F919CC6D4D24B8FEBFB04C7E1E0BE2D2144D7758CF0AD0DBB6D8D9243C07C26011333D039BD61140FEFF607681661DD3E8C07D836856D3BF7E1BFCD3655B3BDDE757CB866F963BC617EEA849A6DFE172B65FDC6EE8AB8745FA78B5D74CF4CDF4B6F2FB26A7DF33F58C487E9104DBF8B682A5F12D47986CFD95B6BB4C7F6542FB619CD2A5E5645BC7913A5DBE36BC236661001B12130A2670B0FD633EDB8BDF5B3DC060A7CC8A3ED1100D826DE45C9CB179FF3FAAFB6F95EBEFEEBCB17D79BA81118AE83FBEE71F8E0C452E40580E2169030475C14A370AE09C74D9DBE9B743772B5396E5C95274CA4EA4E39ADBCA7336A308B2124040E65E0E89FA535C5176B7187182A5041ACE948E7759E5F9AA9F6312ACAA344FD21C751C6BBDA1993EBDA67F585FD9655C9F63FB2EA63FC8D7D8EF232DEC4FB5AC09BBA3D52561457CDF0C634AE09CAF81C979B8776681766256502BFDFF83D5E97910FA34016623897B30A874E587EDD17DE81088AB4859BC4466907545A844E6C104FB31FA54B5E639F11A95316ED37D78909E82DAE556E09C9E5AD7E2AD972D6BF357783C08E36A6907B969C72E64A0A72BBD30EB65F750705870CD2FFD4838B9D4F21C7786639067AAB903CBCAB3606E1B9D598EEEE517662BC729763E9D9401EEBD011C2CA1C42983BC023FFC1564D81A6E8E36519F47DD39AFCE8DD596997B5BBB436BA48BF2655F37264E17CF1F57DB4617759F6CDFDEA6B5A94D17D1EC95E25B353D2CDEF71E398CECBF98FAC2AAB3BF76E71137F2B7D34908DE5F14E489E84ADDC213FD34B4990731DF431B4721E03186F932700EAE09833C928FA4003EB2FA28147CE10A1B6E6976C77C704B36E1C9142779E48ACFC8C525BFBAE88B36A1BF76153568B29B1A236226271AD841CFAB563D10ED53E474255AD0042498D206AFDDAD4C021AFC62D470B54604A321B4888E8A823A65A4B16156CAB9E248328E1469053CE5A41416E34493685112BE3A05652180CE107435696525BAB7B3B8D67E7A1E290C33D931C4E3E9D6494F708715D94209174EE510FA9744F2DD7BAFB13A376F7A7F90C961DE80EB06DE544C16B3BBBEFD77B014B00ED13026D7A1ED865F95612C14E5480147F99A09729F17E82BAA8699B434F5EF03080CE09818E854C516F9A49987409273ACCB0723ACF630533A7220376FAC3CEA9E5C3F691005814C0B233A32C0430E51C34B63A3C6411EAC4B3AA2BC6FD135B3BF09F9A586D0FA203323FEB1B53FA79751B1B53C2903B0413187D474E06422F877C13F223DC8AA966102B21B594021F1606EDA4BF077101609FD9B441E390CE0B0948F94C90129B761B6D771027A4B2CD118680D982B90DA8ACC903483EB7E59A0060270460E8DE0FEDE0310101485EC082D5244C7676C78CAF170B0953C09B569266C2646D6F229F3221B732EAE74C16D032A0E4B2C7737F8DB72C931F09B1036643419FA3A2F83DCBB7CEF3BED644AF7D54AD2DE9471F25F53D5A52CE5FFF62A5A0F324DEEF6BD9AFA593C4568BFAD15F513FB92F2AC4EE6712BB71A7B22721D6E4120E70DA434C6E14C36DDDB9F15D5CB121F7BCF7715ED8DA6DA5719159F397EC34B29D410FE22A6D3BA1FB4DB4BD77BE3CF9F9214BD92F55B73CE2B6A87396246D71CEEBD4F6A0F6223AD745F9BA17FC639C7E63DB5876D1B91DA7531F4CB7530EE6A8BD9D927C1D813FDB6E735614C4F738AD9C687F13E7E503C4AEEC92A398F5A109AAF394812A007D40CB8ED5C70A34B98281FE0DFB82DA5FA4DB3AF7C89F0C6A3088F05C8B663749FA74BD679B384A7E616C5BCCD275B980312BEE558730B518862773EC050725D3EB9906C2E35004FA3E1B808044BA575BE1AF109D144B79F0EF16BC834BF6C6DC2D78F117B66AADBF096E2D9B9632100A1AA0FF2E6F8081887C2379078B87FB7E53F1C5A5E3F278B6F6FE4D516B9018672D43E3A6B65217A1D58D34D009DAA8FB2C6FA29E86FEE46317C1D43A1E29055A0E04723D0F54544DFF91DDDDC46522B8C7635CC6408972199058EA263007F9E68B388DDFC499BA363D21AA3210ADB42E2003B52ADD362E95FF745402DF697E92FB4D4B41F5992EF4213AE08150A05FFF5DAEE24044D652F45CC3547A21D30F7A7E614E61F6CCC210CA0DAEC9ED443CDBD93A7B2B6EED5303E4B1C9755D387944566B4364E99B51B14A6361BCF0BFE3FD79767CCA019CB2B03137525C46691525A6A38A8F75D39795BDA7273E66E9BD558117C507967DCC36AD8B7670653C940A63BA6732A63B6F61090A43F587610CC6A643B9C99779A631FD4C1EE328876D5A434D593E341B8FE2756DF15AA065FB4D6042FE23A01F4F41554EFD12E1E4653FC090D0F799A2201155D73684813AB65F6023729F669AF1DF8DEE47EBFA89C1BA65CD1F3220650604E7327677655A5A5AB1943E8448FA4C22E908A34CC2E96C3C07475B5BC87ADEC2D31C56BBDF0598DA7F3402D45E317D446D050448D51E54AE115415E3BE1F036E07DCF68BDB84FC93476E517E4A189AC552AD14D80DEBC301BBCDA14E5B0038CEE9BE080739FD67B3047D50D0646F612B22C494751CA65404839FAC14B1DB47E953B733EF322A646F6B58D9C9D6CF6DFA286AB4E7D0477197D95D9C78A998AD087E514C9AFF8AFD5715E7616A54253024063AF37BF3653AD10C20696427D74D1990455A71F15A2B208F27064DA2F2584E08CDCAD0FC73966C6BD8252EEE2902FA5F5FBB384D30C0AF8F64E2A6791DDE4B4988930BA776A620ECBF0FFBEF97DF7F7F51FC1AE7659DF85D3256B6B5236D7F0E79D9F3CACBEC2D69F2599074DD93BA0D59BD0C0B51DE4EB3A7F9366421B1701BB29883BA5ADBA7186065FA6FB75CB58F150009664AC354264BE0679B327E14CFA801A44FC24650124B57C9610E93BA8D4E73A86B3722465550442FADA390C9A49A3570B0BA2F0A9E5D832851150489A5B583394CAA76554BCBE34DF3677B4CE76BBC51D71260425558C527ADBB92D9C40C98C71BC5F4A8CA4B58A4F596F151AB2CDBA13C110CEC5086BECB23986C87327A347FC07CFD817C2F228CE14DA7D7DD8CC6ADE4E821C97E2649B632A5E87BF338D01F306296ED0154A2940F22B5BFF551334DE571569ECCDA080743094024E03E0995B389FF9373C3C6A160242D4405ED995DF27985F4BEAAA1DEF836B728A9A4512A5C6E15220A6DD6403F9808B04F1677B07A0FFD05ABF8915EAAF9408651FD406B6D416E9883B0B53037C80B30AE84F1C9448261C71FCC1EDEFB0E98BEC854FC0146B053AA470EC574EAE10FD9DCC69C9A8AEED32923F713C3A8DA809352C6986F297717880CC8EF15F943161F107F09C49766C3A21520282556D14A91129D1C5B837E6BAB66D86AB98901C3CA95AD0030C80BE8EF15FD07B387BC3F448125A2C01146B00BF1470EC522FCE10F1950CEA99D82BF85ED04A8DAB8C17C60C5DE16FC03A2432408E30009188508F03C22007A1C00ED17528D07643C522495323A0D11CEF66251ABEB26864C773FD90A1F53A92172788D1C53E37B1B4958399012C2C8330923248CB5BCDB5386AC985DA2D83AF2D849AB25CF8DAA27F74F424DC17F6A0610D535DBB6DE818264876082B818E8684253B0E99BC018B302AE3F0F5C9FDE5BBEDCCDFFB2DBB1F1EF06606BDD4BD4ABF0819950D7FE477A3507466BF83E3C666007D13B6901C34D4F0AD87B79084CEFC31529DF01CC6BC1C1F1DD103B8030C80B9010202140C24942C2F0828E2D4818E40548F03AD2F3B5E0F4E33FFF4B9828FCBE60C5DA80D2C25B60B21195FCF5302B2B686071D0B2999410570B3BA74B8007C66C217D2F2E00FDB304FAB0B32020BD36D21BBF93284348E9C38AD6617E284D85F23C1DAA0AF631BE7BD9D10EC037B202BA87917D00E6A581990005BB5D5C34FDF2DD631DE02DACE7CEE50548504242FDCFFF641BE37300BDCDB3DC86337F68AEDA3DF8B1E1E395013D4E083DA46BA363171324437308B8E5D8C629918A1A488C942CD4ECAEEF7FF8FA1C181435E9E9707518888D92BB8366FA20DE8B08B88DC5EDA638D3DE5FFB5AC272E89657E31DA2756593BA299FEA4CB3411349EE897C9199A84077BBF9A7AFEFF671514B29B8EA28625AB479E819ADD60131DBF199E57DC9AE37C2B64DD496F9E9EBA103BB2DB22DED2CA9A3D7F6E92A8A8B6368755AE2751927C92F8C6DDD9777517C8ECBCD0374C538D181DFD7C3EFE241FF3AC5BE49DF544FB5471DF0BE689FE930EBDB501CB12BD76A1AF8312A4A48E57053E5779F58829EEC2E219BBF6187C8DE88E9E47402A6CEA2C47592714075C3D0CFEA8862D2AC2B8F6DB25A72B492FA0928453513916BD6A9BD4AB779CC445A9B0395A41E331A510DE684BABA4B2F59E788649A732442C5793A4DBD3F66F775F50577864F69245A731422A579324D9DEBFFD956EDF1BD3A4395BBFE945452039850541101B5667DAEABDD2ECA454F2C4C889E2455E02844BAF3649A4A63EE9C07492535800945151150EBD68745757C55A2CF914C568F1991B00E734A5DFD9B4D0952D56F39CA99DA5382D91C0A4CA539F5D3243128655B3AB1AAF567A5A20D0DF99940E1F480546B19DBBC12626A619D242C36A6B1C05CC5C2DC1628374C78F95AA8F0B6FBF0F53FFDEB3FFDF5A77FF9A77F0D5B53C25013B597433A48823675A01844F8A9E0B2B98EE1648849AA169F7B9804066EA867212470124330F0150C5AC3BB58F8E8F53BB669F31E72357FF4D2546CDD6255C1AEEAD419D0592EAB6392CE84DB980AEFB2341B00CFF5121B226D4C03776AD5FD33B9AEDD284C277FF7315E082A9489498910E554A590571433F102A85901D067C956E0D971F59F72916ACEB152EBFC0BBBCFCA5832173D22B815E514C74AAAA967332A0816CDA915B773EBC846922573883AC0D153A3662241D8FAC2FC442B088418DBA64B588CAC3288A0DBA3E3D4B644CFAE37D145C5A83197B2A647626CE5461C4683971126988C584662C230053B4CE1DAD4340BBACAB29DF19EAF32CA4B8B89D43B79EA4BBE59B0A6EDF66E999AAA4AB736045D146755D365CAE6EA63B3FC3ADC7415C60B639943A23D82D6F1D12638062B986E27707F0C4B04B6595649E1B5B395C34FF6CC076242C28DAD5E83D8CACA7444B0EACD37A9A22D81CD6763C7C281A76381CF52FDAC3C21ABF04093BC46213AE43AEAB38547CB195F05DC3786CDC3E8215C3EB370A93CE5AD8A57B0207CAC84F86718A825C460C669A964815A6F28D130599413CDC518AFCDC182433CF07940A9F9AFF3852AEBC7D6DFC6C53E899E3EE55BD3C167885DCF24768DFA846C8A5130A73BE19E4D35AA9944538E084EBB103D4C0C5B04E74E648065D32B40AC1C7F3BF7706EF1A2E81BDF1CD502BC9E10BC5251E770ACC502DC1C64059CF1B5434BB5CDF6A770F7DB77070186FB14F44ECD097227F1F13A23D0B2F0C832272A40962FC80A4F2B07085C1202A54F2B73A800BEAA2CA21162A0F15BCA18D4D63830ACD2D72664F7077A2D20762F2900F6733B1216EE290E404D3D0C36DC132039FEC59388606F46E700A5E9F7232894B509D19F2757155840EAA9C000D8CF0DB0C3E442006C2A604F314186DB024A11228AC81DA0F867C3AB627035B009EDFD4D2E1630BD9714C0FCB98179B8902140BA1EA40FD74449B09C271141E08CCE017AD3AFC752286B13A8F9C7E88DF17A2A30C0B62FD89EDA3DECCE0D788AC527D33BF00468A5B8310F5B011EA0D0F5E01955D5E1FE89AB15F84F43443EDCBB67038D0FC20212872D1201A24F15A235AFF6148198F8125023E4327D0C7A2C27E0D5731BF0FFF57540BD807A9481BEF8916490400877EA47910DF1997A7FB1E0CA65C12DC75A987CC98AA2359A3E1CF72202122B91D8D21512CD8575E6DDFF8A6DE27D3D1E319F52607FC8DE1E7A6D679F9BEA963E72ED23834BFF7EC9CAF86BBC699D62893B04C58FED751E068250DF4F6F0F3447F8E13ECD1092FF4EBE4861F035A96223AAB96A878F42E58E14F41BA4D2ADE03E8541F8403257ACFB22D4AAFF6C94383721F54B5A5477C5268FEFEA7E149BED3186E405F856C2B78DA4EB7A307A63F330FD2A17F8DD64B913AF007108EAB2B71CDB119BD4D433BC42B090EFA9115DC90796C55FC52724C2A96E7EF51E675D7DB89D080A386B7AD6159B4F52CFFDB3A691F65D6A242BDC49E976CEDE86F8F17CE3071AB7CEA3BCCC5A30EEE623F4816B2A29209712B9FAA96DD99A8E85A1F0E1CC8AEBB5A3C3F63CD7055DD6E3E9C74801BD96CAFACC6D27775D5EB78AF2254F9CFBC59BB8F66A1F05299EE72602E232EF752FF080F6144EDFB7AFDC8421A05CE0B30AE1B2C1D2E146B98993C86F919DD2DEF2817F7E259C94417873AC9C8B7A67EC94BD6D65D16DB920A9A49A18FAF93BCE1826B34A765D1D51C55B984F5443907C363CC6F05087F65321E3B746302D39799B04DB9C1226459BCA388D16CF40D7B19770F70243DEADCCBBA776338D17EFE3BC281593109632F9C853416FB34DB56B5756DCA638ED6298F3DA9CB324F9FC9049874056AAD316327D87C0555197711ABF8965F785581A8F5C1457ACD8D7E8DC8C164CB3BE907E3E93F4938FC2BEB23179B2A248E12C04ED21C5B215B33B792164AFE2424B7B57F95ABF653800E70901A726B64CDF7FB4853063A901673C0F0D6A10C859317F10D808EBEC24C53779B465CF6A30E131FBBE622C91CF9CBF0EA771BE37D8974ED77640409DEC3AB0A166B97A6A45920CB2984DE82D318747A8A4CDE100F492CBD9A68C1FDBD862FE32152437846D65D876F196D4607E6F37BC5A094D21643C9790714015D9921E0017B7474E60494FC6207CDF49CE450D1EE4B7BDE0E2910F7BA19989B5C73EE965145AEC5DA4A2961EC2CC2261C6EE152B21D8846063B29D440792B9EB4A748019210205CF18390EEE8A41A822BE3886CCAC63099B57CA7CCCEEE3A27D225D3F1C0D3242D05924E85C146771FE35CAD975B3B45BAB678CC0BDBC41D9A17D7BF9C419D05ABDCD26DBEDB26DF7EC9B3525C752ADA91AE7FB2C2F6FF2282DBEB2DCA645C7726DA97BDE74E65EE615FBAF2AB6A0EA45F12B7B8837096B96D0B2224A6C0946A733AFC3F54721A121F67430940F34B73809C7304E629C85701A3779C42D0567851D14BC9005A42C92BACBF9346A8D005065ED5132602B2058A5D6C0F03B9B7D392AA248E8A58492FAD948CE8FB026AFC3980ED0FCF859ACEF8886AAE52063D2B9E1D52090F4F69872CF958729679B9C15E4D42DEB07715D9F5554A623925783A311578027D4557DE85972DD072AA9F23322A1F6734AA33DE54240329F29E44486919AAF0B9B54193E3927FD543E1CF744E8E7F8835ECDBD04FD5EB9F02CB34AE0F799710FAE228756C739B6088269193A31C7C6D6DC7A562DAE2D2E1BD7CCAAF1F5759C47CBEA4FC9C3B17690DD9D262A03B84B4D412A9CEB15D1EBA6A6B2D190A84C49EB2159D0D5C38C7AC813D81692A5902459DEC96F67E3E4617EFB26DE7C63A5BD79CE3695B2252EE43ECF25F781514B7E1CDF019A8A8EE46351D8084D2759950D4C9D080CC8AA44D6C16EC67B487AAFA90D6EBE21E5FC816DBE5DA436B1A591F8A92A2D8A0CCB4C01F88D80BFED2A84F95C8E4F35A93B2117CE50CB787487056EE7A8713599CDA91ACD94F626379F1F6D0585B0E46B56D44A340AC0FB4C801733FBD2219F64CE6542209C8A985291DFF9104686A9FC5924003E2B5414233DFECD2553700CA0B8AA59909FB39225A65875D8E266613797C671E8DFD85D1197326BD939A71C06012116593FDCDB6132708E77F261FEC6D4E4ABEE169F568A7C128A107C44934B5080329B48EAA66EAD4C2175A24240F236797451FC1275286A21ECE479FC682EA8716BE7B1F64611316C14D1AE65286FB8F8F11483E15BB68FF2A6E3D99CD5EBDCC7A2C4AE05BEEC93CCE439B010F89F49E0C74D92E9EC7D144E8C8936491A05DC616B848D883BC80A21D75BC86DFCC9EA9ACD4D6655DC092FD4F05E7D5DB77C451DC006BC7F26783FEB68B88DE3009B6407F98C5A180C242C4E967B3477C22BD597063399D61C4EE1949F3149EAC0D1AAABC233985D3D9BA529DB98EEA6384A5941483E2AA3139CA7DC06886A05B16A1967F7AA7B0DD73051681DF6CDC19AFE08E4B1ED674F3F729FE657C371DFCD5648BA5BE4BA44C068A5642C68055D73EDD9B29D270F8D5665C29D39DF5B368886857FAFE23F4DD0A0E10F20E06B2F898F7EDEDC6152C68FCCB49B06BC3821BCD0DDB2D2747F68A7CAF8F7595E33F9481D3BFD7BC50AA93A03C16D076D539D261F672B83730AA375C1B138538C1DE404AC55626D632ED38E796CB69054059044C0D2A7BD1494BACFB747B79AC212F71904269E867AEEB3834373D084D01C86556DD01C8C6906999D94670B983800335E52617F186F9F6B9F1F0E481A9014B7092C2D7E17CCA535BDBAFB7C3B40C414A9261F41149D52D03154917C12711EC252512CD046D3C1A06668DA4979B6686A2FFDB4D1513B1F3095127037E0AEB50C9680BB10AAC1C86CBCBA3212CDAFAE709F645A99AFAE5C47092B1A686C90F13776F79065DF9ADB166A0C37015E89D880C44A249E58CFB40FBF4BB7FB2C96AE9AFE6805507F66D156BA4FD6D216A1CFD153B31D54BE0A6C6542797F3846203E09F3572B33D7F50F9BBA201B17A6F4A25A45CD641D259D67D5D18134EFD88B8AF228507F2FEF2FF5B8CE869CA38C77799EE58AC5CED7561C775AE465FD77742F2BD54EBFBC8CD22A4A8E8583D15701486C53E535705D97D16EBFF6C4CC3C9F1286F3093083715D12F86E39EE63C44733CD72013CA75196D0B84C53A0494A30C808F15F19FF1B33D9D82D150EEFCB057E3743A4AEEF02703574CADB8EE20849930F33D8997EB536283A88E58744930F626D2C0C87EA0EFAAE46A3C468F033080950A7863AB689F7714D6BA32F8A36AB28B89AA66AE82589CD5F5FA3121BAABE4DC9B5B7488FB6E9C8BC62FE8ECBA1FBD66FACDF17DAB9B6F1AD7590BCD0E3D4930BD9268E924BB68D23E1140359C2A8092E6423732B476547858D57F9E07B2AAC4C04ECEA719AE921D48BE2D778CB32D354E8A268ABF329BDC9F6A6B2427EF74CF23BA0478209160499B720F331F7C2F2CCD23234A359C606696F90BBCDC58598627AB9929569A1B37D5C57D97529C36CF9973C715DD4A85B145759E6A3C88BE2FA29DD846DD221788C6442380DDF61E4347CF09B5EC87107133ECE8AA291D0C81ADC30ADA18E15E5873CABF6B79FBB3362ED68706ABE1A185E748A09398E11B0EBC48DC926C47587AE0344BCAFAB53EBF6F797FFD7AC9130A51C0C752C65AC3657C6EB59197558A925D7E68992DAC2451DA86ABC9BC7A038AD47E65182558713800C634D4B1D8AE2BFBC65FBE6646F5A620D8FD161C406AB7328958BB72AB3FDEDD5C8B9E43E7778882FCACBACED8F8D4AEF1EA3A49A5E253C25107AA4A63CC85F55A2282EACAB16E0E00283B8F17543BD3DF404C306C768084B58ACCBB43139CB07357A24B89DFE2CEC20286EA83BC08C944E802B1A7079AE6A6E5C9DA49D07C7263514461F4EE0E2883F7E527EA81DF8DCAB1254B192A4280F08D14278B432326C9FBC76EB16D9A9FAFAC4746AC392D07C2C7C959DE1E0865A3E77F86329A73FFC0138BAA8301FCECDEBB5B443F30D85D2A7E759CC7167DA77A33AD5106E4A2E1B25EA0C1039E912BF03C7A096C787B0361E0787B0B129EED532AE2DD155C2A282CF6B72BB200822355A2E953D49E03B40F9340F478EBE50DCD2882D1D0AA162B5B4E8A5465F24ED7C466C4C439DF8E8ABBB59DA7CEC2591834D423B11A629A84C91F58DBA10DA2E94A2221AF4598CB8B87A4E768B77DFA4AB20FA2217ED16229D243D446499C53A8BA20EEBE8378AC6A7742181A875F5A6A66EE6C1442405DD6700C724F718A1122B0C232A5D97EA0CAA867C1E21A4A9E5CCFDC8BE36FBC1B7B3CF7E8006048A825D3BB948C7251D5CD470A8A101C7BBDC848BA8DB0AC24E219E82A14A022765C87E66A28079DA33EF707AF336BAB6F33193A36BD6D3EE07ED9130B4AB4DCF89E1FD593ABD081602F86C777ACDCD84A24C87259C6F6C684CF9C361D245B386E959646D84D51327CB27B833D978ACD5546535806B664A8F59879E81B5C7972BEB2AC315C534379C5D604C747C4C160D9708F8F7E19665A709B3549DC5DC956B088C1EA343AC8BF821A2BF89011AC30C79A5360CA30AC482AE23A8A518C583A7524C8651671DF059F3B7B236ECED531AEDE2CDEDF11749162B668293598E9E96CD4A0A03671C0EFABB4A6DD50A79C970D58D801B490D5C2B72C28F517A5F35B73CE1BD6260F1E08087A200F73B68EECBF9786516713DDEF81825069E15B8DDE7282FE3A6EA2971691B2F42EE96236E3DD75416BFEC22045D51AF6E8C6DBC935B7E80AB480AF130A347775E4DB8972BB598C33E83B03FAA4E53D39BB84CD8ADF033CD691102D1CE7C9065ECD518ADA4DECEDBC4A3F313745FAA53101A9DD65938216BED3B84FC59CABF68CF58459A8DD26C79377F4609B825DCB700F73673975301F7F562FA7700E5713D64C889E939CFA3F0EE965CD3B167452D9C8F8BF4F1EBA1A20638912CBCD75FB5623625839C8CEE5A9C4CEF8B6070F91EBC0736E6292C73CDFCBE6F75347AF4FFE31AA4FAFF812EFD0185BB02A7A91E4B00D3D4E0180D3A8E35B858CE8A5A66FBF17D738F2075EA4AC6AF70C119ABA63B4A5558387E6274F3EBB298063B95B83AAF8B32C60A59C0786BE4A292B2FCC761A52E3E62B2D2F827169FE7F5013C460BC1809F960253E027F0A23F95023EC055ACEBD2202B6E5054BE30E35E81FB5F3FA5D9BE880B5AC60070C95D7B60D07368A8B865730289465E9D54D2102712FF81BA1066E5673C5EDC701533EE426D9671C0139F59E757C110BEC7B3C85DCF60D56756D0B2D82752C7ABDF898C7F72A8475E88A4AC3C1A3BDD2A906E058B89CF62F5F0268F36DF8818C7B128DCADA1D67437BEA085314EA08E5FA71318FF4430AE535F359933A182DC8BEC545389DE6769C0E23DF80D68C8D39A8BE9AAD035381632BAFFBA85A5FEBF734F02253B02A389120BE0D0C4D098F25B861578D5D027DE3DD692117E35A1977B969E534D0B100354A7B06BEF02B5F1EA5FA0C109C8D5F22DE6685759A63E483B26825CAAF94E71A6893CEF210E2ADD83C740463C8500F70BBBCFCAEE97C6E9E37BA5BB083920DF9911531C495C9277AF52AAE2C1C594863F317F6BBA4C5F91B99B207C42C6AFF0C523ABA63F4ACB06BC53D90FECFB294645BF5E8B69308C4633E69579731B30694ED4860F7F3EDB1607DE26340FF54E3D73ACC862CE38363E4689867ED194AEF94B3D2D3BA31425770D1135C19B0AF63F032BD4C153AE071AF624E65C0FDA4B616A4265DD7116C01FB06C9FCE725228331A004BDD84A3733223B180B3084AF73BF370520E339926510D212162C87564734D32E701E57B1F33CAB4F0E04932239FC24851F02407F65A3898CDEB5B2B8BDF0C27D7C7830FE21AE374BD117A77D6E46520A53CBCFF1A3E5C4C544B7635CD72EF09A1F55EAC2BA01B1CA3212C61BD5DA6FF33CBBF140CF15A044ACCE21D64AA0DD02FDACA2ED40B40E5D6E0FC601BA2E2C298717157BF48D3ECB1FDF029BF8FD2F8CF7E9272740D95D64B299A72659D412D52A75BE82ABA8EF7550CB5F7D8910C1D82144DD670099A46BD61529B5D0D265D75A713A82CE97E0233AEA623CA6BB4CE2E29771C4AE78425ADB69BEEF6AC8C1D7652A47C9D2E3A88B6D941B1EA9E4AF724D667059D93E830CFB66BBEFBA3AE45DC4E3B3BE99A68F9F4AE79146DAF6BE2D53D8DAE49AECFE25D93EC30DF43D754117CDADBE9A2B87256D755916A035D1669D885BB2EAD7E6BEAC2348742698E92B8D22EFD3EABEA8F8EB2609C707AE7EDE5DAEBB948454F23C2D22AB378DFA439C9B38DAD83197ECBF26F6FD936DED8E87853612BEA689C6240C7E2ECB0644782955D4DC7811B19A3DE9473A51DE3D35DB340133F3A1A0E62C5D33BCF41B2BDEE8356F6342215B53A8B7739AAB39844ABE8CF93E8948AEF85D140905ACCCA3A295269F4287066D5653B2DAD7A2BEABC3467D21F031E24AE7A08785D15652D3BBA4BD85BF6C8926CBFAB8B751C76CD0AA5F7734579F67ABF61C54E2370DBA9E4E28860C7099FED9054651E2D2EA37CC0A146270C2926B546E71E0A714B66241EECB37AA8B2E0F9FA790EAA989582DC0DDB3CA45992DD3FB94974D0F2E90074146D0F6BF0EA9E46A642AECFE23D9DEC30CF36FF1859424560945590CB595D5745AA8D0EF633C32EDC7569F55B5317A639947E08E625AEB54BB7B7E3B909B408D11A1D977A29A205254F24B212AAB27C8F2438C73388A79383120883904E009908979E89C3C9D53A2267A2F23A0E04D9A882C76E68C349281D71554783B095977C4326B4D64A5A79C754EB8FCF6D47E65D479745576E85FD17ED5806A9ED51D8623D5BA9D9873CABF66A6B14C21E6DAD04A827E384F3DDE12F3FFC30EF112E34B6D177E768A3D577AD3783873E6BBD21ACF4D5B6D0C543B1DBF0EB2CE47A1C8C9E64683DB5701A42A8C214E8CBB4E4ECF43E65AF3B2D7FC9164EAFC5DDFE942FDD12D4C8EA4CA640DAA2BE8D9FAA5CD114A542EBE53B82F944E44A272061B5802BC6AC761CB47C8DF3A246577499AA7B1A2B02E4FA2CDE01C90EF30CD6069496A0DD854713B7BA8EB7863BF2F4945C53E7792677E65D5645BC7913A55BF36BF2F0A2645D0294A2D30F08EAAC63B98BAEB0C7FE406FDC935DCA9257F5F0D5B03F1CBEAEA0331C7591F48463C597EC06335557D307660D4AE90007E6F5783F7A7A6AC681F268232F5E7EDE49A8CA12FE788AB34B07E53FB03467B76A58153140DE36A5A5B89AB014C0D35C23A24A170FAEA6323A4685E5B10DAA45E7191437E8FE74EE6DDD9F2277038B70E86F136D1672B889E1D11ED7722DEF72976C77570F1009F8C673485DAE23D6F2B959394B429C48199F2E2732FC89815C67B9B36ADBDDFE46F03C11A7D403A74C5A9E282C77498F5429E5D333550D731A1ECAD5A27F87A2695BA16B8A59209FD477454939800F8E3577E3856A7D3CB89FDAF81825466CABC346CE61A840C5FDD32F4A82FF247407C77809ABB72068C28D855168CAB9022766D18E9E56425C72973D30E8B92B54DEA2015DA29057BF9434C46904F2A3DAC74AC9A2B8805EEA7D5A3EC7C997799BC3E0AD50C7A7AFC1062779D9A281FB8A252C2AD8B6D56678944F0D7A7236F0416D8083F4B8B6BCC405700FA7910767C435C66920DF7CEA5D633D50EE8996574A16F541843A8BAC979CA8F74D7A107E415ACE268CC21A28A82869C1D5659C66BE42B3B231308AAC78ED18D864A88193322928E434DB4887D3634D3B26308A2E81B798867C3E1E4FDB4CAA94B090A7AF6197285AAF859DFAC4F7829ED76D777BB6DDE6AC2898F8B8F6940C72CB868278A29A130AA169F7D1D1E167B8521EFC09AE38A6E08673315FB92EA392219C85A383BCA52521BA0B2FD6BBBF08EAE5C1610455C794DCB22EEC32B5D7C64A7FE988D0CEA2F6945E20E026105659F591695DBC39C8B4C6EBF78EF3AC4ACB1C157F784A30047544D4283413ED3F10896AE7231689AA8F0A471DF3E20ED4BAB1DA7B3A3282EB60FCA6170A380D885B965D665A258FFE32ADF62938CBE480E3D0A145AD0B11438EA37BFE1C948F071E10D3B41C49A688077792D919352DD0B12CE6543F67C9364EEFDFB26293C7DDF525FD4F42D712B3400E36A7A6B899A42CC0D906CDDD8CECD5BA78F037B5F1314AF42C6BF2BA8F517A5F45F7E28578098F17BF3B140638DE41796F9EC76BB38CEBF10D80D162E059DAF96EC7D02D0EA420B5C4E188D93B2C1E70315AA0367133B8AAFE1C0CB6C509C1DAED97FDB64E2EC5F3E71C1DC19B10987590EA77DE5B50BA47B7E1AC8929B9635974027BECEC67CDFB3BEDDC6AFF072ABD9F71A9F2FC8374CD7C7F5E2094F80B0A7190F60BF5F19CFF0B1B023510E879D6E588A85B9E949CDE1C52750B93873B97D07A2DE59CBAF724ADE256A4B112A4D1838A51E5A29AE30865B1FE071358953CFBE7C90F2B44952143A888D9B38BAE0C4B55AA2DE8AECF06512FD23A7F6645793BFC81F2D71997CA510FD235BD745E2078AF295C8803D714EAE3D927850D81D2A3E759972392C113E4F4E6902BC34CA95E4B39E7B341CBABBA3679BC69FEBCDEB34DFC35DED0F250990095CB02BCBADE2B5563D9FC14A39A673FC634DAC9E5AB50A5C8D0AB12B2B04BAF0C9BB12AAEC0BD9F0D6273C7E8C91E2EE15739B7FE1D0A580596776984769EBD19D160CFC191915781A0252CE2CC2BB91384ACE1B22E7DF237834CFAA46AE1142276B6AB6D997554990A9E5DED445754A7371C2BF64942C4F0065BBD4BAC41F94BEC939429E2C1AF6476C614BFF43EC989FEE73993BE2A00113B73AA41BE5F9892A9E0DB9D380B638A6F59D60353FD85D69FA3BC8C9BFAB7E88B7AC0822445E58390005DBF542B031D6C72FF6285968E9E3D1ADD9028575FC3FB14CADAA92EECC70A58DCC517BBDD9FAAD91A7CFA04EFFE9F74C30F4D69C83CA0A3759606F4E215A0DA2BEC27CB9CAAE43B2B98DA9B8A941DF73ABCEC22DD564573BA0FE50803B5334F3B14A0F0B583DA7EBC8D57CBB7BFF17627C7E69E7F153EF78FECEE262E13464F42414E952F0E4CBA3E0917BA7C8229D5CBB3834A1BE62413C9438D94ABEE522E6FCEB9E0A23A4A9FA51CF22497CFC735B98CD3F84D9CD1C1126254B963CFA3EB8D6091CB23A54C2DCF7E296B9493C4C9A1422498E4993CB9E54A3052A4CE42AE78F20879DBD40D377E697E72367669852BD0AE55D5CF9865AC8EEFF1CAD8CE54586B78D7E157AA156388D899772DB3622C53C1B7533D831563E9CDA7334AAA2F292FF3E2E52FE94B7E6F2915DA56BF6C2FBEB3DBC54551FF40BE4657CD0A7BD79C8B8658CA527DFB1C56212F4E886D13D462C52AEEC8ED6F6C3FDB764B275172917ECDF25D5737E5A000C50D39AA8C91E2B03805FC8F17487A79705D52439DC408425AA3E189072DB7191E3158CA6D87F2C50F342EE1B49C564BFB2CD74818757A96A53DF64DF5C4F2510069FF3DDCC28E3A76401524F1644E86860FE3B590BC28E1E13082AEBAFE1C9DDC9E18D5844256D90D8EFF9626C77811FE5D9F2BDF6FBA4C576C61F7865B0BEDD853F6C55F4DF985DD6765F765BAA7B9FF8E5A03D211267B4B452047E73D15924A12B8F7B07E64A2B2873E61D2C818F520FED5768FD1EFDABD62F4FBC29D61AC09D007C6955DC6F5010557E0F1400362B41AB12DE6DF63D505C985C899D4AC90374B7C48E6B988C2C4634E45BA64C977F12A7A70597CE310869F9C845538ED5596ED502EDA103A72C85634E07EAD6EEE9D6D5CBC67D71A1B15537443BF0AB751ADAF02B48E9C6799C55589069E5DE834975605988A9CBC15B0791DF12F3D55ABD067B9C1FDE94ECF1E2B51CBAE0A76C5A2E258A1A681558EA99620F1519859C355115A88BDB6ADA653CFC5ABE7CF89F10D47F0E7866D699F16F44E81AFE961A0C0780BA0B14013B1B7E3FA9CBD6D7E26EA2F0EE9F27626740C58D0EABB4AF3365C25BE33842666D9EED1EB80E9187DAD97486644FAAEA8274C1B53AB0F7422D6E6FDB533244CBE514DC9E9D5C747C5AE62E56DAECF726E3B6F128C2E47AEA59DF36217DDB38F71FA0D3B429C31485CF140ABE184F372961A0F0A35F1E77542A39FD018107FD7BD8841E6691A178A0B8BF17FB9BD4A158F9E76D257DAF395C0621A47EFD8D116073481220B78D9E9C2D9C7ECBE0664C4517801BDC4C77A520D179B15B2D8266D9126FE7C4C64708C066BD9883DD40189631CB953175B1AC4047AF8F7AFD385B0FA7FB6557BF3FCE724C21F4C11B049BC6DCAA1E174A2221783378542FEBC50D118A704765C55909807737974C6A58150AECE629E78BAB0785DED7651FE84C6439E5EE27B3DA986D3CD0A590CFA449AF8F33491C14F09EC863A20518E2377EA624B039A400FFFFE75BA10C63D52817432984BE26BFACF92C84B5CCAF3E4EAF8734079439CAE1F229FCDC1307BF4CA95BC9343516E31573DF9D771866AB1A86039655D6CCE2173D103B18E7BCE4B5A0C3085AA78F440A1E14F092871D7AA82D4323FA35EA30A17B0D85863B16B53A5863EA571C6EDB826CA1D56536A8963A99B5D255BB24F16E7B4267E05D7D39F5FC1B63821BCA26CDD466ED2D680A9D56CBC5E768BF533D84CDDEFA3D2B9B145C26AD9DB2405ADE21A1684821E9D52DD2C1865C452D67501A16A942063F276E9E062C3058C361E5C13D308A710842F595134F9A8EA0D2F8E0EF2B39E84E25ABC54EF0F6F0914F0E0400283624A5EFA19AD41F52B56D734AE652ABDE64069D96F8E72FDEE261796EFD1716636C5947D605ADC79AE9B82C4C716A66496DDA617BA8CCF4C0BF7E8305353E232A674C987D4DAA3104D1DC4930D4712C845A0D694F9C7481A78D385722E815AB9F6D846B381F56BB461CA293B318BB5CAC3D2099376F64E61AAF5F1D077D426C728010A58B45B7D498BEAAED8E4F11DDB7E8C9BADF9C33F9B7F493D50CE2AF2449E8BEA998A52010F9DD6C80DA6E335F3E4ABB8C64161FF98715DBE2A3DEF28E4F0E6990B9C6E54EAB194FB514F33AEE29AD0F3282FB32CED87E6E4A721686264B7218A24E85C8788D4C6AFDFEA29E7C199F5DA1035785FC5B312D37A9DE7ACBDE371FAAB780212C10C4E44427CA499484CC1D00B61D37A399A982428E7638292D0481875A69C2B715CAE3EEFDBE707907E0BF2AADD56DF5FE10295EEDA57CA87D34A35F4EEB3D206A2BB6CC7BF12C79DAC619D6DB7392BC4931C6A56B5DBCA960EF12E0C960D2D58F63572F712235A2DEF6E2B6B1F8C323DCB1A1D552B3F904858C46D57992E20345CD6914F3E7180337AAC4BA3B8F14339F301DC5ADC98A4DD6203B767EBBEAA657B399B57875D6C8D1FA7CF72BE79823B00E09AC02FA498002C4222DE874D1F14A1AB267B626739702668BE58A720343C2AA706259C44F7A13C494594B7AEAEB3BEB7A934B55E59A7794E2F548D9539DB94F163DDECB7C31FEAEE21E39676068051AB1B4815809C5E50986D47C7E8E5D3AD310D8572E29E67A58EAB85F15849CB39F48A119DAAEFE24EFFDC519C3BE56DDC2510F2B01D43FFF0BFAE62EBEB2404AD17EA2A84067F8E1D06790F0659D22A3AC94AAECBD0D6741D5DE2E42FD200FBB47684B8D58905A68E7DABC2F985F01DD46B21B7051B063545BF56ACC64FCF034CDE5C73F9897989364BF9E2094FCA7FCCEEE3A28C37D7FB5A20C109057C323FE458745C5154EA62DEA850C8A3432A1AE4947D92771CAA97F0FFF6EBA3FCBFA1F351F2521DFBAC40C1057D57D060188D38D6C57C79D0E3F66CB3C976BB6CDB7E14B999C8A7485220BF1E48288E4C2B54320DA1706B7B1BFCB434F6E0DF5ACD879BAA56CB5B81EBC7F9D72867FA4E2FE5B7EFEEF2E2D6E5E8285DBDBA38AAB150CE2D95B40AB7DE677979934769F1F57847908E7B23E4B870734CB16B737782CE9EDD9ED08848F747485C4137A0ACBE48B9ECBBF8FAD64B50BA7975DCE7B4F671A8557B748D494FDC02B4D61D7024DAEF195989063E9D6B6E5AD4D4EC816BF12988CF49B451DF730652CBA6175A429D498569098B4D77816A789C28000D7D0A535B9DE2AAB37B132AF01251AAFF4C252E71F80ED4C083CB80B64405B6858FD44D7DBD6B711C3C74FF75073FDD7FA14B3C21B94EA067A28277E0991818537ACBB0785634992DBA9D3A832A9591F0CAB2A6099B4E0A252B5792C9BBF444826A1E732D440351D2F9357AEC2115A739CD211BF5E7A78722258B3C5EBD93576831C7E41B83B296B3BC3B7633920447E418A42ED8D16A391F5FCC826E2750C5A7C3098C7E52AE364CFBDDBECFB31D2D548B5965EE3770E9F89FA4C48563B45A338F8EA96E188C3233E6F5F82A1A17671CCE3D7305D028D46509177C1EE07893694223C7E8DCFDF8F2D6028B02BD96F04841936054E158179FB2195F82637E409B244D36ED2312A4331344536A1D6794B474F638B9A4D5D09441FD2AD6E8D0B5453DC2A5256D359D64456F7819E9BCA64EA2FBD217C4BFAA4E825E868498B02E6FEAE6CBAF4DCAB459C84F4F71A512AC873624DFEAC0AFB12FAAE075215805F55ACA3575E172B530D9BE426A9C4308A5609D177AF495EAC06225D6972B28755DC8C1950DF92C7283B69633F723FBDAEC07DFCE3EFB4181DA0E9F5626EBB8A4838B1A8E8AE48BBEC53CABC4154BA2926D3F23DEFA46F042CE6CE2C498221779149CA098079F25B40C469B39F772403C5CB8847150881884579D9BB520D98BB89E4C131FF8283132A6F8357855CD9FD63A34198A74FF3A47073FE23D90D09EEE9E0AF6BB735D50BA07E711181453F2A27BD5FFBD8AFF54CE0D8D89205769BE539C6422CFFB5C0F54BA070F818C780A73398DDE6769F17BEDD49FF64A3799508A7CA5235237AF4C34E0360D51AFA23BCF012BE8C97D400BE897EDD779A4F188A3A3398E0A6AC682FDC62341E97EDDE5E4E251EFDFFF5EB14289371CADC871605850390E2F5C80390745DD399140134F8E243032A6F4816751873A28DE262AB2169F508A9C49D4DE2A779A0A173893432702CBF7E442A061710E14FFB998F35C47092B3E2751F935CB77BFB1BB872CFB76C5FEABA9CBEDE49BD0A9D01220679330537C0FAF03F4A8F9A49A6E9C93ACA007A725371C46A709E3A24F9C3795BC6DFE23F4DC0995E829F35602F1F9F2A350C0DD2081169F2C9F95EDC1934043A2662B6BFAE59D449AB74FA8AC3BC942EFDACFCAF6E9242795ADFFC6FA79AFCFD55DDD10C3D2684D945CB26D1C29C3235600E45A102FC5CDD065435111A8A3B3D3EF54453DB82BB5DD50C171CEEEC9B1DFD53CE553CD53D61C2C3F9EBB2EA3468BE82ACE7E3C6F3EB33F6A37D8544599EDA234CDCA56D2BFD55DF03CC91BEF28FEFEB2CCAB794C6DC45FB372BAF413B3E2E58BEE1BB8843373DCA994D172032467B25CA3107551D72CAF139B0F7956ED21611302A5387ED10E92385F3B550815AD5E833614EE32D028045B8096F0F126546C41D38DC5C842B105E909173E0A262D4AF2E09B56C1F8C2340B78F7182595D229443CCA429B500E09EED21DACC6CDFD47594ED358C0A35928BE2865016AC14602075C530B1E28D1582A8351AD5E2B132CA35716D62F3041720F0B71487D27E35B99A2DCAC828EF89B78F38D09C39F8245AB053A76ACFD3B6AB5438E590495E169F4B457588A5C4893094202BBE19E8259E929340FE91DF5DD639DFE491CB9FD8E6892B44D243782C618BE1204BD7D4AA35DBC91CBEB8994623F46E97D15DD83DA0DDF08BA7DAEE355DC64FDB0E940423DF1228F1112EB17731397C4B25A0E4A8171DD5FC078CDD3A85379A128A480518935E4D73EDE86EFF7CDD845D5A6337AC4C003538486E0A356D74F69B62F62B9EA4722B45C845750DDE0268F36DF14221B12A548A1201CFB55968138DAFCAE64FE85DD6765372E6EE81BD5E37B481A4848118F128D14DB68D0FC25AA76F30D1D2A2FEAF1FC635BFA78AC85CB9F55BC864AD08B362C103B8253731B2BB2DBB39A414F8D81D7D407FED8B33C16A503585EC4C0412EA05B84878715184E432BBCCFAAFAA3463BF48CCAE27FCBF26F6FD9B69F4C84CA995218D6E7D35D33AC8B1F351AF5C0AAD9A6C7A2A96DCA731ADAE0BA2A9A19CFE82E616FD9234BB2FDAE0ED906965108D4B4974A4DAA1571F20C6D7BC3360F699664F782695E1CB37A1A5825806A1C9ED3D40CA2AC06C1A65BF786975CED2393E6EC8C56ED9122CC2D2199DAC7706A2A802F106DF4CBAA88376FA2748B4BCD00727A51A802E862B16916C8A02C4EAA3D5EEB03E50796E620944D297002E5C208829A722FD9EE0ECE4C3812BCC8E93BC452D1FC83D0AA619E523251E0511116ED10A6389211440BE60E2704EAF11A4B5854B06DCB2599D287E870BA4A849284A97A266D88D3DD050FA701C3CDF92A85E2664578AE478C5828BE2EA3924133ACDDEF4A5354699983850F5F08CBC1AAA560A5B09FB3641BA7E00442FF092BE22D2B36792C4C13E654A48A4AD7F801429A1915BA0B484945C8969B203A92F0AB9A218FDBE372D77BB689BFC253E30A16DA460425EE8AA94978D12F351C6697458DA4E22115FA818906E7732A92E08B745B358657AE4BF5846A3F1E33FD23BB934CC9CEE948C22FE3347E13672AD93D1949B47821634A8310BADBC545A19C6984E8B03B8424BB83B022EA30D53A68945CA4ED8E28919E327A6C616FAA2796CBCD015352E6A11533D0E8647E326B8D4BE7052C58F31CEB7BC5BE5605BBAA7325A99560067A714DBE50A1CA192871255CECA27BF6314EC181324F83162A095FD42D1D3DFDC7ECBED640B6D9AEA7C00AACFF675BB511ED7312C95A704A88157F5DED769100B92724F8BD86CA180A12A2C537597FAEF08423115AAC28C28CBFABC717759A2E5873EF3FA1F6777D498BEAAEC9C8EED8F6630CFB2744A7CEF2077291D409813A308D37B1811189B6A18EDBECD7EDAD52CBED0989E2250BDF001D51B86A3823A6D69A58946F161633A83B35B8F147D9C3D55CEAFD2BFDEDDDE0FE95C375FA48EDB9276D652ACF9E1C46EA2911AD2B72F2AE8D4C30F7B610D228ED4DE63253F4F7C4AB00542406C7CEBDA922ADE6F07E0D52E470F1BB4CE6F1EE7DF5668EFEEE16C13E8EC33D38C8C0AB489490E951771A7826A4FB19C17B3CA60CCA387E46C81A56464049C86593F1050EA09CE12365EBE0F464A67223217F8816B3235B34D83B1E6D5329CCD2F2DD2E8A1350BDE1A37A811C3873032E9383E7A2543ACE0FDF80DA42E79E38D1A34336F36DD1ED6AD2E4CEB31723FAC31291887852525B8DE1349790E77074EC50E5F98197573A7287636123B9D333399C5D5E4D0D83309AEA44C12D4C0099545794D8309A12A1E6409CB690B490AE2240FB09CF951837257C9AE0963FBD306F381CA3D83A287EA851842725244D812B0C30FCEC9C87BDBE031C89809F5291F51AB410849B6265497B0A7C3004D34BD0C5CBFAC774AACB6D631DE769880D7460D4B4CAE10F7F0D71F80330BE50BC86C16705F6BB3CC4B198A35407CC29832C0AE30230274F622038AADB036D9943AA58C8D82973428B10EDCBF1E073808838886344743B19BFB4A7CB439446618BC5C1EE689A51149489A0619F441216780F07F388B02B2B7AE1E8C7A9263CC6A8D55A226946D61308F5DC86222D24CD293E55EAA6659F466F4A68753DA100A2E54472D04D0659C9A0D8A5BB5CA3D8FC142CA15166CC1A9699FDE0BE31663F40814955944E7A25F20A718F04122EB210496E4495052665A446A117B90C92CDF4EC663E11ED31BF644E6E80C97573F6EC3AB9706E24B69FDDB58628D3BB01753C5A5392BAE3D304CA80873FBD8F401D62E1CBC6EBA9B2875B30B06D05DEA78FB00A7FC5BE0BCBF3D7EE8FCA385E04626C48446B83BE8FE2135719C30E59976055541158EFB583CCDCFD11B7E31B2A208096D0CB0055CC06C3F5FCEE0B295E4BC483F9C8E88E0E07363CDEA381B1E0819A52C181C989F50EC201DB8DEE0FB168B9D1751AE8E10D811B53799510B9A5A7F7A0A0ACAD2CD0E39006D60A8B05021EAA11B0986068EB65B001BA30E656F8196D6F8C2CA2811022D1ED33BE4D87DA50183DA40D38BF3CC8757BE2805FCEAA6D255C48B0DE38CB060BF31E64AFE358E82F6678B6A2DED15E3D850F2033729411782E8599878BB270169E09771E2CFA222523468E425C8F292164178C3538296E477D33D30FD797211CA72725B46BFF3FF61DA6FF1F687F172CCECC54B38BD2F07D4ECA8AAAB14C82C2B4D0357038334B0B75DF47E7C5CBFAAB985AD2EB844C603F265A5222DD5BFF9EAB005DF847745E4084A63F013FF97366E02770AFA8B248A3261AAE4744A309C480B100C02737B6E8E246827C8FA3BE43F1B891C19C9C5447DC0840D782FE137D7EF42737DE8C1A533B9E496E3AD2686926DAA3DF5186A31AE34FCA8053C3660B785A736515DED3786A54D5382685D5FACBBF7056E3453BF7B4AE4449E63325105762420799046188A90C5F594C576A7FC39ADA5B3A427C6B76FFB5ED23FD7FE72682651919686891FEDE749989A6A498EA4C38E466C25A682A52EC46C33DF1C6A66AAE1996F5A1C977710DC6649025FACB9D2536984870DB7F661735CB0C202616D745C8039906BA5B5A6227B16C6F463B5E9C7D0BDC792D35A19415556999048579273783E34C2C2D0D30B8BA31AD99BEBB171D6BED969A5AE5B64FBAB4695B00B8A709800B4D746BFE9266A17322394A4D684560D710A9016F2ACA71A6792850E439530284E622FF201AC0B5178CC2ABA8EA3C092A568BAAAF11F65D9B60925648A21D4827AE01440E59449AB5A825BA8D6C82B32E884D73020EF58E3698D1F1E19D25AD091D66D63C31A51645B58C5222BE61C847BE898AC8B6E3B83B792552B2FF33CBBB07FE345A702AC1C05E13410BB4D7B47CA099FAB711AD358AFAE912F2191D5D916AB3694A96B523EAE117448BEAAAB6C0591F82AAA23BEAAD34BE40B8555BC3A42B7308819212D710BE32E0DC4986B7841CB80856B4BEED9125E8B8C7E87D2603E7C02AB842D7383EB364DD35F0A2752D8F2E81EE1A9357B3B45D03AFE0BA5D03FB4497818B208BB0D012B89256E0324845C12B2F908FB13976A1FEF134EBD08294ABDB0638F1740F393E42A7ED1E48D556082783E6FC3378BA6DCFC93135E854DCA26DCBA902B4E5ECB141C76D77785CCE7A4F464BD6352AB6007A8B8F1F1AD46E73B47A2BECD147DDB1CF3DEA7B07B204F366C015B4B8B720D544E706C073948EBD47F18CA575A4312C4FB7C9CC8AA5FB99FA51546DEF33ACCA0A114C55232D2E5DB073A98CAB2637D0E9A45CDBA49E680C463F14ECB8571CDFD0B50EC278D1BA2D892E81EE7FD33791B57D0DAFE10A1173A43CF60166031F411661A1257025ADC167909AA25107786BDBB50FB55B80AD430B46AAB6FD11C2359C43BD7DDA825A8BA208F28570C32BCD8972112BFD06E2F1F76D1BFA8491928B5F8F2ED657F24D1D53EC1562AF019465ADCE65D41AE3E3CB91C9CED5EF8A42BAFBD531BACC9DC89E707153582B03BE931E235C7563BD2D0597F2114CD0340219D7C0E20C4C9CE621EB050D458684D8ECABE0D4351C66F32F3249D329D5CF66608112B6127F91206DEBD84AEF8D9A089FCC2F351500EC57B6D5A478D1DADB2CB02568EC0321EE2A36557085B3443ABBC489922CD815BD6BDC533B7BDF457E5915F1E64D946E8D368E13A4A88D8417266B30500AAE95080A2C3084966B77F8AADF5C4711A6A63A7C5DA4A18EA54B5AE940E4A4893069E59C9850714CF228AE2252B6DB14F150DE0796E6EC56EAC0425A71B5442C90C5A6B4727309E502D6B2EA6450C1ED9F588375C4B49A757F3A3059F7A7C866B05013A35DB2DD5D1D59715E362346D48DE7911AAD23465A6D26D99BA775059D55DB6E5332CE784226445545BC52634E9990461596E4DAB85CC18AF7A425D4E24A8A99C03B8E08E6934876FFA6B4A8EDA6FFA4F826C749771BEE9FAEBD14FC27A5318DACCEA21D094541064CA5013EB9690F0C58B342257843D56399A25E2F2245546DCA21351BD2589C4499996CF5F42B96B0A860DB56F070025EEA740A0E7135E58CE01D270087E2BE137919AE1D6F9EE0D3068B283B4AB89C8C513CF7DBA1D550C36D0587A2CF0919859D19E5840AD98B0D9581C94E9A7B4A05105C4A2607E5C2D449415CC96B6818F444AE9A59CF2CE8E95AAB8DE16D52F6BC16787BB6DDD663DE42F056E894425C9F2921784955FD87FC76AAA908C8018FAA1AD7FDBA8C4A26AF3C4F22569DA3041FCC6B48A4F5E7857831406DF45856FBFEBB4AEB8ECCA4DEBD04E8125FC071B42E65ABD23257F9FB8C48E2AF3C2D7C355B4BA4B8968D17E4B8E987025BCB4BEDD053A875EF084D2DD04B01AA0FBA9146D527CB98BD4D21038074E20A40E49031F06BF5A044C77EF173966CE3F4FE2D2B3679DC6D9FE97F824C24A116574BCC04996B4E2D379A443A60BAFE9323CBC96E009591936A27BB15D482F17CDD143A3412D73D8486E30895759AD24B8C85B210270DB08DEDAD0F43C15FF6DB1A06C154982751D7A2A734B6C620C761A23AB6E7597332AD4D15FB3F54F03D67C0A1EE8C4F05E83D9DEAD88DBC0C08E285726D9952B5754ACD44ACAE6ABB932DB3AA762DD9EEA86379D860A0E4C1D5191B16043C78E32E112044E5531C57C847AF36C57D8D6DBCA40F5FA435B2B3A2BC1DFE501979CE80ABF08C4F65D6810E6FD37911E0CE5681585B86A4782CCC44AC2DC5570D8CBAA4975ED50AE4F1A625D8B34DFC55F11C3D9A176703990895C5015EBCF1A5057B4464480F8A9F2BF9F5CD41F17EEB6DB1649FE0D6EC29CD2163C519432241D50894FD0AD8229735BD7A9F089E59CB16EADD220E1A60893D239346978C95413AE4549C64D4AC39B9E761FC3CDDAF2C9EF504E964D3B67372780A18BBD31A94E87C367C54667FDFBED238031DB22A3DB945E30C123D794DBF01FB739497F126DE779EAB3AAE411380ABB8528ECAC69000BCDDD5C583CF50DA3D9AA15448720C00CD6B600FC92101C72DE1E708C1A4BD3F34EBF86AC0E8C990BDBBA3B60817BD4085737654D64D74916EABA25D035418E94088ACD5406FD15007910A53F574762665C782FF91DDDDF48F28E3D11566C25519E455597460C25B162EC6335A1E9490BF122D6320D656FE5EB41583FA7B39FA58F8659CC66FE28CE4A4200FAEAE10ABCAA23D0FDEA060219E1D74D001EB9F337A5A55B1DEA96DCB257CF3B65147196B5A226450687EB218635A710AC76A68AC1B46322606E990F5918C89352DE47B4C2CDA603A27C25640B06154D71EAE778266BB5D5C1431ED1E050497AC7E2A66D87E732E951D95E538346CBFF3FD6CDB8D3EA2E422FD9AE5BB4E0B19C8E318C5D546F143169631CA2D8D2BD27120902A2179FB13C5A75977C9AB9E8EAC2D79BED3E27B9DBDA837D513CB479DABFDF7B0C15DB5F04096A1B4085A94A43D3819A896C0972B395B617B5D42A6DBF1DF22802770EB99672AC4478370257AB8F266F412BDD1A537243962E3E888931D7D11C8C11D7F2129B1C0E53722FD46BFEB34D9985DDF48A3DFBD37D0B86CA05D469F6D34C7B834B87343AD80E0121B40CD0CD95C566FAA7871EC5681A1A185AFB26CA7B2674B83AB5E436ACD56AD30C032CDEFB6ED20192D4364B80A48C6CA5A06F1305216F8A73A9916719023B73A81B6911F784D9A8FE55EB1AF55C1AE58541C7510DDD0806756D65E2D43626E9819657544B9EEEF74917B00AC22DDD10572741D5360392F9D4250B6B8A5901EE2B0ED9A238315B8CD8528C1DC669D20DF2DD5978A69A3C156AE1AA7367BC284F3886A26B2318EBC8EAD3E2AC87D08BED845F7EC639C7E4304DF39ADB26E331689ED0EB428ABCD257B09B2A8E30B425A75AD308716385A9CB97C1F55E0CB45B8174F8AAF14C2B9B48CE5D5B53E66F7B53FCB371288489555E23924A6EA4951969A89F533173F14AB762A9E125D23B54BE9D8C9AB43D5FFB3ADDAF3039F9308B53624E250D64FC028B1DE9403654451217E7C8E2B5DED7A02066A35D58E686E4AAF6E795DED7651FE84F1C719A9B2623C87C46C1DA9EA10B048AE1FA71B8A557B1B4F89AE91DABFB40CE5D5A5E0DB5C25D61230286B07F3496C4739FB222F630943AA0F1AA1F8A855561F2FB266DD250E150D9A345769E6C8B11740ACAEEA8C4766CA0331CE8C73D97E1C54B9D71626545748B9B7764C88B391D7BDB44333706A0A0DC4112A6B33A5971808651B4E9A647E1634B705FBC8E7C36933DFC8396E9A65BCCE5BF7B34EC43D36322E7545C5CCC6369488F6B85506DC2828C17A293D713FA204F12DED78F483FB97AC281AF4941C40E349C4F5E028C17B883B12B93D78396E0F870DA55DB14DBCAFB310D077E6446AF50FB4C686384A7238653D14762D3C1FC751A8F5BE169E8A2355FF5A7816CE56DDDBE5807E07FFBCDEA3AF62658F44507D414545FCE076093B61BA2DA5F94F3B09FB35DA3059B227A15654046432300C2CCF71CED714FA252DAABBE66EA73BB6FD18372B06C33F9B7F898CA6E0925756CE2C3222CFA536AAA21CE852D73181330B8B561DC5C4C47A8A56192D99D1D73ED5F3282FB32CEDB3019D6BD89112C4F5A709923E612F9080DBF5882CDFE5819C8906C3650FD35FC1A414C327491D11EC60920AF129B2544C51D021B931857D53731ABC6F8F54A82D0DB3616B0F722BED4CB12F5C82CABC3D9D7D2B63AFE14670612D80BDA25BCC45B1F712D7774B34A002878C59CB045408B162F705C0048E2008FBE318A9A10B61779851274EAEC8DE887770051CE44A235EC4B5605A3F731770E1F0C91C4D8FC608A35A072113DF3CF433327465642799E4BEE2B8519147D1A8A2ECD8107920CD47732E7C2C6D2C17751F3E8E11611B19BFB451E68CAA2D0AB8325DDF972FD782DA6FD04274CD42ED2BB69B65851D84DB3460D266185134CB212462DB8FB2BD4257957535A57AC30B5D8891CDD45B613C34DC129B644067D1E961D8CBC194BC58E3D3CDEDFD7E305009CC0807A227D61533BA31B3A6DF91CDC7EC3E2ECA7873BDAFE5E2CC286251D753C0293326C782B3A7A89C654CCAFD9B625A9E955E75FEDFAE4DCDFF1B5A425494A361FA41649D066FB2DD2EDBB6DF053A420D401320360B490ED4180389DCFAB46224498AD3B688F3AF51CEB45A41CE8A318C54820DCBCB0B58CEE6FB2C2F6FF2282DBE1EB790116D8F1181331142929DB6C014B4549B20075872068C19908325AA75171EF81C1469D7449968D11D2243D4EE486DC15423611E36287C4EA28D746B214CA80EEA137A59CAD012E21285A94CB7995857966419754A20D9903BA603B7F5AA0D3095E178C9736AEDF6BF6AC7E8C8B04DD8FDD7A65374FF8536E282920C2064922EDD4EB490418A8C4D8D0A126E19E44CD870F8232B4902D88ECD3CFC8AB7F081835AE50310BBB4EBA110C9D8C6AA35BB4C1367479E1651398E456ABB8E1669355EB0277B0D69E0EDFB3CDBA1FBB8844B5D5531B3CC9A0317CE9C92327CF6ED831A18779C1313EA8971484D132EE6923719DD21791E423D39560796E44BF0E18A83C0F1DE26A3E57B9A20755A4392274BA5448270D9154D8D0596B2940AAA0EF2E909B26039D5713F4F0DB8D4D140B07CC44014A4A7D901332CC56F969496E067B96052B48EC3631FA455F23A34A9EFE7672125DA23C926C8221640B386500EB601C0D3D306C52E8C1CAD62FC0FA44699316B5866F683FBC698FDA0E819B64ECDCFCABD624954B2ADE272010C9BD80A086EE553CC4A53630A717F2BC161FB97C2A0209DC48F0072D04B911BD72071EE8D739EA5296B2FEF92BC32352511D781A384EF0B184854B7044C45399CC0FFF72AFE53962E4DBE8B351E9341356FBE4BAB3C11E036DB698A3A4B8BDF6BC37EDACBEA3D2592EB3EA11559A02352DA612A0B30464334A86ED31CA24EC093A0D4177502BA217CF481DED8FF5EB142E6123C995C7F8E5A640DB82995D2048E71AC8215C31C8AEBBA306C962991BC1A135A9149449550481398C48229AEA384159FEB00DE3CE4F51BBB7BC8B26F57ECBFAAF666E1F137C84478667165D13220934A98E591085F2A7487C098C0D61D028D5EC25744A704E26A4DE884775580377148E438BECCA32D437663C29100A1B1EC66044ACD5D83F36FACCF863E577749BC1906A7355D72C9B67124EB77685E713DB122205342BC72ABA24B837ADB9C8C62FFBFBDEA64D50A94519CB2FCF0ED6FAFAE370F6C17F53FFCED554DB261FBB2AA4BCBB62C29860F97D17E1FA7F7C591B3FFE5C5F53EDA3479ED7FBF7EF9E28F5D92167F7FF95096FB7F7BF5AA6845173FECE24D9E15D9D7F2874DB67B156DB3573FFEE52FFFE3D5EBD7AF769D8C579BC9EE88BF71DA1E4A2AB33CBA67DCD726A5DEB2F7715E946FA332BA8B8ABAA5CEB7BB195963B9A87DABF12ACE7E3C6FBEB03FB8538F7F3B187D287432D48AC1BB831A8E66D832B0347F1F76888C8BFCE122FD9A473F346AFED017FF83E814D8D1C2EFEB4AEF585AB6F567F30118C05A335F6FA224CAEB41D59EE5E5535F8D8B6D6D9A2CA976E9F1DFBCCB8AB94723345E10F7092FB3F9EF5454F70B5EC2DBB8D827D1D3A7BCBDEE6A2C69FA052FF1E7A8983EEF39150B7CC6CBFE1273966B7FC0F35F146F59C24AC637E4F167BCAC6E1B59ED8D5C0B8C7FA74A6B6F78DA42F2862F045BB56F6CCDF51BFF4E9506E937FD3297F8B7575C4FE421E0D50C033864E651058539930911CBA823990942008F94DB0DF698E344E879A1E7217BDEF0B4C9873CABF6D6FBDE44BA46EF53F07F4FB1BF9BF11E4BE87E59327BB8288E17C1F25031FE12902B209775E49A2F6359062FD5122802BFD422D69A42CC57F9788D600A4A57ED2EE5E67BEAF06B008D001AD64143BCDDC0F65C87A0209DB90FB42837600295CF4B14D1E04BE16BC797007DA774F1B37DDCA739EFD2E82E997776808060A37DFC737CFF90D4FF5F7ECE8A7E1D7A62219022805C00392F20E705E02C819B2F60EB3763F042463FEBC1970CBA8852D3BB2CCAB7B501AFCB282FE71E0D12E8C87F1FA771F1202B604CA153C2D80AB272E6743AA51DDE779515C51191A708AEABBB7E8C2BAF1C821C5F76931F0F77379E6755F3FAC7B82CE073083221C8780932D3F3181E028EEC149166F0918B3CE50C1B3A7B242A85A709101220C4198478810D4B50B1043CD8CC53659D5FBFD3FFC6924DB663EF76519C346F5FCD5D1AA6F83E33E1A623CAE41FBFEB481FB7A2AC94399D85AC3EBAFE16EFF70C7000240B219C55DB387B8C8B2A4A6A187862F90DCB77C5D9A6D9D315A51B0056711C845095A6D9635B0DA40228067CF99755116F9045AB6849639F6DB541142925248C77F62CFA86284E4647F0AAC71AA0A2BB38A981FF0DBB8F81D19C8044AF8C771058820404F9F735C04612300609428A17523C77299EE4C50457099FB04893F48F20D4ED5871AA89701C37A30AE345B1AC00262708269E00C41A689CE4A8510A362620F3218FB69C37F73F116A7A7C7B1398109F7FC54BFE1815A3B739E71D0FFAAEA3B72457131205880D10EB0962C78FDB7A01DB6381D6605726F2D433B5C99BC133C0E73FFA05E7003E017C90E0D39DB5B50C30D041620488C06C6E80A2F9AFD96EDDF678243FC9D3FF467098BACE735D8EBF1200A949553818EA7E22CAA85BF16B9CEFF8DECF7F23CC5E4645F17B966F7F8E8A076EBA72F2852EF117F63B2CB0FD40D9DFB1A9F2C60DCB68B7E7F7764C3E11747CC852F64BB5BBE3B75B4F3E68C913B4104C4138EEF27BF63EDAD49D12DCAD39FF4AC8E9B3CDB7AC2AFB79D52FE5864BE9E79F3564033AF3DF28BD7BC38AE27DEDF26C0B0C7080CFB43EDF6E83FA1A6DD8C728BDAFA2FBD9DE5B215148037483F6D2F97D53D52CF798DF0B0A34C9EFD1221DE7F7133D84F9FD8C2AE4F7EBEED821BFF7023E9E20C71AD0F8821739AC18C189C599E1307F1BE66F03C4AE06621D43AB31A42E03A5E6132C6FB34DD554682AE5F8ABDFA99166FF4E39AFD4E8E7E5A606CEAB3C8F37552D87EBD0A3DF09D3007199F0D75E743F5106E5F7499CF2C83FFC48F001566CF2783F3FDA39F940D8BE979571B71BF0267BCB1E599271D33D20819EFCEE7599F67E4151092312824DE247961775EF3E4BB717E926A99AD34DEF12D6F48A82B3929C94E0130F35CF551DBA1EA3795BCC3E12EC15E5DF58D96E20CE6B6CBF7FE26C35FF4C98D48B7771FDEB7807EA00D1D3193E091D657B6EE3DB87EB137F7B60391B32C5A2DF5CC8772B2C8FB6164D9D58BAA91B27DA3C48CBE628495B531B7DCF7E8FF22D6758EE1359667793E52F757FD9C087FBA60414F9EC31CEAA2279EABBF8E14A8D590D248484D8D1DF9CC9EA4E58D471B9EAAFF39C0412010DA194BCB643C2DEC709FBB24FB208DA840A936895F1EE8F7AA436BF8508A620C4B224EED219493544347AA5082A222021F4C5B2EECFB26A80041AF2051580BE6B4817866098829023C4E937AE13F43F11E2926A87BAE1D6F4300C0CC340AD61E0E198BBDBE1E0508CF1B0502CE8D466DA869ACCF41BFD4E0878929B7AC335BD0156C6DF9D5E16EAEC9E50832B427D81C5E44A5251BF3E7CA48CA44FE96E6FF319B5003B01760CEE09700645B2C22CDD1BE01BB2FC1CE40AB94E001D95B413029DC34B7D96F145F00821024A849C6B5D34FB9227CD7B355C730F3F527A5DB378345B7D1BFD4C9105ED953EFE4A98D287EF21D1BA7D04BC0540E3E47F73F99B48AFE9279A4C50BFC907CAB200BBC9EAFF5CB27635A5685E65DA56098034724A420DBA9B210AE80A56F00A3D043D7901A1BDFCAF14348E8846B314B0B9600A7209C7FD2CF2BA807406A5C9EA045011708EDD67652CAB114CA15502580BE83B21533AAE158E04155DD817D408CB63490BF8921314073DCB1D5F2F7419FDA1B82E13CF45F6DD5ACC9BEA89E5F2ED6D4A62E336F835CEDB47EE7A14FD4716A797715A958C5FF5A3B36BE9767C19448925782E6B9AA87C55CA63A8456DD77857EDE40E43E334D528FAA3917BB6DFE7D9E371C119AD96949DB035A3BDBD2A2E370FEDADBDDBF831DE363EA9ECDE24464D7DBA149DA88B8C89A8C79BFA3F292B8AAB9A7B5B20F5C03051ED814874A48426E581DD5642462C8B8059187A0BA58B2B6C09A13859286CC2F2E86B3105943751EDBC083DC45C94F9ADC3E57D7D1F5677352C8F8E16D46E4FE3D4B20B0200D4D4C625833D4345AB532A0114D04CB6F45018C1124640025140416234D467DAF9EB0FFB3AD36768ADE4EC8419DAC9051FD24EA220352B13F40C2921B93C42C7C07158D14056714BFD61260DD519F05C269AA0337922ABEE3E1B790F90939A95093B828C905C1EA507A038AC6820ABB8AD1EC04BC3F50034978926F81E40630DEB8F61FDD1D9A687EB2861C5E16884ABDD0E93520CB63928E4B859A9B4799E7952015EE2ECA3A6DC778FB50D792880299658D96C4E454FB469B79EC1A7A7457401130326FAC1C49B78F38D954E1F711594670B276512DDEE0893A29D9088D00346BBC9A0475DA1EF840337ADDDCE93A82878C1DC272D99CD0F42A9DDC7807201E5BC6C77ED3CCFCB66D7AE284B5B5D45C24E79A3AB02C351303A25D74C2307C9D234754CA4598EA83F0A89F0E5BC8FF302B8C962F4332D6D6DFEE2CF711E7F26805AB322D25C80C121DAF167BCAC7F6477C03516C75FF192DE44F966B6A1F2F0232598F44C9FF33805620AFF5543728DAA22B1DD2772AD455E087CA686FB2F790205FAF6674ADD3B368151B98F74B99049C75FC8B5161874FE35A43921CDB17F4699CBFDED9F4EE60AD03997AC14E126919947446A30B47D59D15596CCF28CE1B7000F011EFC8C825C4EF258800BBCA8531EFFB89DD4B9688E5DF4479EC17BD44102828D80431D9C85400A427615EE810920BA0E106D22B475C06C846A8023CCF6DCF2A7851ADAED2AB1E9EAF022ABC2E63EF01BBB7BC8B26FC3AB2BFF8B71374242DF4981682671F88D24A52E3F67FC698CE3CF84BDD6D11FF52FCD6322EDF3F0F35DD5C077C294A283776D42280CA190769EBFDD58E1EA507F2B5CFF64BF807DFD9B66CCA1768D87E903B8047041CF65A65F59DEDC59EC60167310AD357F2966760A2B2D9409B0E5F08D32FB98CDB6A70CBF05900920F39D81CCDBA734DAC51B8758D39760043942196E90E758F07C4272FC85B2A1007E0E51EF05C45FA3A4E21CB7FF29A045400BEB683138A9759018046B60839875AD5340F3FBCBE89797BD655FA32A995D5E76F879892DFE0148029090D38ECF51F39E46BC8F1CCCA080A518A51F5239A79282F85AD1058C06EDF8901252D0E373CEAED87D5C3457C3CE3084FB18502DA09A1F5473B25C2A2CC916BA9DD6C2EA45F1916DCA2AE76FB91FFF1E7A7CE8F1FE7A7CBB0DDF5FB7EF76FD5BECFB0281AE531C3FA942988391B646809C13819CB8CECD739720D31660062B0211A73256EAF4E7A51D7F0D5D3C7471EB5DDC51C7D6EECE7E3BF1BA3691D4BC59CE6349FB53E8FAA1EBBB8CEE392BEA62DB8BCADE37FDDAE9FCE8AC30B3A88F10773219C0AC2EB36C00A408F010E0C17E66E01E14AC40C17200B0AEEC2174EDD0B5C991FFFA29CDF645EC30DC8B4A200579B1905309ED611E506AEF8017A78117AED71ACC1717165A4D0838117022E0C4B117E6D1E69B4B9C68E49BE1042CE15470A2559F1776F831F4EED0BBADF76E377D5AB7277BEDBFEB1AE9877502B5B4D0EF2D5EC292D9BF92A311AA75090BC4B6FE4E7FC13D43CBF71AFE6BE8C8A1235BEFC8A357919B7ED4A49EF1BDF59E0D96A2D1D59172DCF47D3B07B64755E8D4E74582049444A04ACBB3AACC76B588CD4D73975CC1E7051005B184CB28ADB149287EFA392057402E97C8E51EB56C20965FB4B2FA80D40A2F99681FFA6DEFD4BFA9F29447E5E937A2D46BB6C9D2AD40ECF823616A26DEB18BDAABF2C72879C3CADF194BE7F28544B4723E7D7D176D1E5A65E7F2271F8DF4171400538508102280934168F3979381683B01A337188559D79C9486152699AC0003AB8681C36B408797CE3FE5F7511AFFD9BDF77B7C6ED7D52DEBAA820D6E5DA78B760333727D78D96A6A42BED8D535CB218F9C7DC4CBFD9047FC3528FD4F01A40248790629CFD0641D904E71508B83328B10D600F72E8A936B060E8145447E214DFEA6BDC9DBF5CD9B7547FEB901A0EF8400B5D954BBAAEE186C7BC51E59CA27A0D077BCF4CB28FFC6CAEBF84F4EEAF877BCB4375511A7AC686ECB8A53E0B10EE83B5DFAFB6C5315B0E0FE135DE6BB4D9666BB7873996D5902CBE64834AC127F6D77BF94719408F4E748E8655C97CD8D6DA0ECFE93DE019D2FFB248B80CE2DA2A16BFEA9FEB11513259246985311466FF19665B3D7038FBF6A1E5D8A13F6EE8F3A861433779790E1CBFA2DAB92ED7F64D5C7F8DBF17286920D060126AB701CC61A7C8ECBCDC36C7557496C827CEFB3BC01D39BDF59F2C82EEB8CE0A1508121C81212E190087B4E84CF47F7BD794E8AC7455B4F90E5C29FDB98DDCF0D7F017402E858029DDD9ED57C0B40CE50B003C0118B7E6E70D3AEB718ED9E0D5012A0C4CE6AC31F75E562278FA6600BB6BFDA2011FDDCA04465814F7BFC2421C445B0C1B69BD48C92DA1732AEC6DCB7007401E8AC031DCEA9ADC31CAE580D90D315EC06E2CC7396FEB9E84FF996BF7678FA052FF1E7A890A10EF039004F001ECF19D6FB665E96F91EA9F5A55ACFAD84729F5B62F5BE4A92748679C75F09FDADCAF37853D542B8DE36FA9D326B9F7F7BCBB6F106B4C0FC6B40BC8078D6116FEA66D6A16D2A5E03C35402BE9F142974EAD0A9ADA4319FEEFE93B52F9C794E640EE55A4F652492FD2633D19F9464E6486D3A4B743000659208600A734401E34E04E354D8E6758E882FD6DA1C915AF0F793008539A2003C13796B4CAEAEABA28CE2B4B97DE02D7B6449B66F345B2AE55268633D112397F7DCE69AB4CC4249DAD0A2422A1710F54410550F49BD26783865ACA57DBAC5AD361964C5268FF7F3F326930F21B90C50F89D43A13C17B9619B87344BB2FBD877227928F9C97ACE2813FD7DA487470B503241882B247D01E94E04E9704EED29BDE38BB596C8A905AF36650B2956001EB4B413021E4536E2E456794499F6F32A9F37D2AF2EA56A2A4FCAA6A60C21910A78762278A6F4675F39D4B1447BE9934CE66A33A7EF7AB2EBA238EBD67A38E0197EB588EC1FF2ACDA93E1FDC015D038A0B1B3ECF27C7CB7C0029926B27C83AC53BB04B719E8B9D38B1F42B619F0EDBBC6375C78F59F72B6C5BAC83B0582579B7C5A4F16ED2574016602CC18C18C2758B1062327041BD9A66A2AC641C6E157BCA46B963FC61B365769F261B9D1F46FECAE88F95E75F8910046BBE89E892EDD9C7DC4CB6D8FE5B6DEF31F2CE2209CFF16A03740AFB311EC6555C49B3751BAF5F08C015096C1C81425CDED28F4A08268083A21C0CB0F4F12E064058C392D8C718F2C36F0E414DF1A10229126027DCF37FDFB7A4DE1FBBD513CC49010437463C8B98F0BBCC1D26CC416B9BCD3CC56C365DC0133568519EEF24D933CD37B7E7928B0A9A2302F1C3EFA9C877CDF18194EDDB84F84DA4671DA56AA6EB6A4EA2E489ED6182220E81C6DD85D967DE3D43DFC4A595D2ECAE83E8FB8EBE5463FE365DDFC1ED7C8CA4D201E7EC4CBA9F3BAB2BAE3DAE2F023419FF85BC9DB68F86DF959D78E957F79E8F86B8846211AB98B461F589ADB3FD137156F129704021C07274B13162D5B5B0350E0E14BD8841380E39480C3216818018657B030CF384F6B777500890012D4ECE292EDEE1CDC5ACEC937C92F44124E23C130C7A07EF0599479BBDD662E102408A81150C31D6ADC44F93D2BCFAAAD9B27A504E598A0884AD269A0C9B416BCD0F9D780020105ACA380E3CE6FDCE797E9EA6B1C6CD4FF6C2E858326F2B94F61001340E8A440E818D859B4733D88399661948248A49C46FA618E715759C249E87E09181130C22146D4721DA243CD65840B20FFF793A5841E1D7A34B2475FD52D1A156CDBF69D3E85B5DEB1A14234FA374ECCF712F8E7E7D5C229B500132E03BF2B7830848525E0C0F556DE5FE32DCB661BA48EBFD2257D8E8AE2F72CDFC2128F5F8990F87AA6E4E867A2AC1F61593F1265F561821B140D3F124024895BC77BCD21C8F167BAAC1F61593FEAC8FA0996F553080121043808014ECF7B181EEF58E234C7FB382F80B5E3D1CF7859CD39B9E62F6EA7F4E867C270D2DADD126FA2ED3D57BBFE27C284F94396B25FAA768E6E3A5D3EFE40E8D62C495A56AE4F1F7F26E856DD25F1A63D36C8E936FE809767EBAE8B8F71FA8D6D63EE028EE3AFFE77A5DBDC296F6B87FBD9769BB3A298255EC79F97DFE7FE26CECB87B9C4D1CF04CF1F61DC8726B79C073690C2A404D9BA949A5AAFE466FA5A56B3E1BBBE746CAD205ABD522FD26D5594F993AC5E631AB352B0F513D1D3962ED3A7EB3DDBC451F20B635B2E8041DFF1D2C77CC27B89844421090E49B0FD53D05D7471F01E542758E768B388D34DDADBDE110121D9E16742072FEB36E6251D7E24E854DB62A650FF1B39A1780DE613A4D1FEFF8EF7E7197F6DC7E1470AB05C466915F1C7E40EBF520618B5BF54BC4AC75F0992B2F41E1275FC9952BF0F2CFB986DDA73A65D8F9F61284411A03D40BBFDF98DFA47EBD31A804CCC7406C8E606CF6D61F07C26843A09620FED42AF0FBD1ED9EB5B4FB7DDED41A1887E2FE05B7F2267DEF9E7F9D272C952808F001FE84591B6B7D85F0E01C5A25642049C6E40640D1DFF3CDBEDA3F4A95B4BB88C8A6FBCB8D967F2486E2E75F2416B39642E73F691B07E9EDDC509A0E6F8F725A0F4A29898FF8AFD5715E77360141005C80E906D1DB29DBE7060F8AEC112AF19FC9C25DB5A275EC8E867BFE1A0438299A0C907C20A687359EE5CDAE8E72556D66DAF8A8795678CACEF69E5F9A2F835CECB3A5A5F3256CEAE109E7F0DA136845AEBA1B68F21D6A36C2F5723C00A39D73A3C72850FA117875E4CEBC5A3CD15AE3AF478FF8676DF960A597F0AFD314AEFABBA5BF3C2C6BF1376FF4749C5796DFF53808A00154EC7D6ED83957119DBDF2B0394A233558A13E30631C665F392F86F8481495F8BD9C864F43B699813EE5D0D68F2621568E232FB10946308290BE4216E50256424325901434E06432EEAEE9EB3C2FE0166A81043F4108B3925E8186A31D36DF47B484802989C20985CD5EE9BC79BE6CFF608CAD778E3145780F20C210625F194D026242A3259015B4E065B1CDF072B2ECA105196B927D60D98B8BB183AA431016A7A79CBDFE4D13F727D7C47D8C5048BAA40C31B3F7022DD80CFB9E4AD55FE1B5EAACD17C903300460D00086EE4A07A750D01561D8F94542D6BAA123DCF61EBAFA44DEF25DBDBF07C4C14A2D508ADEA1168C98D0E3438F97DA2AF4F8A113FD23BBBB89CBC46D871F0A31ECF06231A794D187B949A9CD03749C0A745CC669FC26CE9C22475F86217008A504DC08B8117063FADD396E3497293A058DF6D92333C48045847145E8E4525B7DEF9D7CB78B8BA276B3778F752C71B582302F44ABB363C4B8E9F0FF7F7B5FD65CC78DACF957147E9A99B8D1B2ECE9DB3D1DEE89A016CAEC21459A87B2E6CE0BA358073CC4A84ED5712DB4E989FBDFA750FB827D29A04A78B02C1D24329189C487040A48DCD42F544C990C7E16F83E50B73E49717E322BE4E7FB115D8B1E736B7EF2D0E1A1433B74DC187A50E646FA2D19624DA398A0F539D91D882290B6EFCFD00E47D029058E4A24791095DDF2525AF60ECE522DCC4A0582A42A13C2F5E38713CC92FDF4798479A9008206E153538FD872128DE07AEB06A40D17CCCA6B582868F1AAFEF563EBB273A34F0904DB7D1695A8B67FB90D603605635CB920F75D0EA32A6B3796F7A85464EEB88179F884C977D0FFCECFED1CC630C3A4EF1FFE2E3CD6DF162F6597F7915295B00B3BF40994223B1298C80C238F4627230D1345CC0A457640B21CD7C279BFD0297D04E323185311CC78A15D86264616413461F2B10E273BF717457EAB94C6CB03C91A80643AE71B8290891879F06032721F362A1578964B5442E1B6F74643599F8B5936310A9982AC5BF05864E01604D95C3F2631BFDCBACE987DFB9BE84A163788C725827DDD2B88634D2011DE9BC346A8F23169DD2CF422DDAEF4BF39675CB99FA8FC44A57DA2FA040E490ECDCC4E03DE125312B5B6D1796882190460C55009206A921CA77CDBDF0410350FD27CEEBA839F05700E07431FC4B1E72E7888B0B950470522962AE23D8EDFA84004DBCE8A3C41CBA2708A6E83027FB7C3E3B6D3B8DD06AE03941C1E7DD0FFA01D5D9ECC4377A21C0D1DDCE8C54F194D8AF879B6AAD1CE9091683C5A78B430B51D815F0B9ADA95C04B93DF9CE0E5B79E6FBBE84F770F8DBD87D9290A5EAED3FD34F41A9778C4F288651EB1EAFD32E3508517230352244E86C2186528D1F1645BA3FD7C800E0A3C5C78B830051755067DF4BE8929A0E804C823048585FB5F57FC675436373FACB50F6B53993E27FC1506F5C2F93D758E699FD5D363068BDBFA30E332399493ACFE3B6A63F6F2884164E03E60F89359345E1E1ED6000FE5FFF645952CB71CF3C6B640C752E4C182C5C76386C70C8F19A63163571C8F8189BC5A23FE3219B5580C3C3E787CF0F8601A1F0CE7F9C64A91070B3BD9BD756286B99CDE7EECFBB12F38F64150B6CCE4A7875E82C298A7F0707FBCFB599DCDCD8F6CED23DB4866CD217385F1BC64264D1FEE536DEB81E15B01862B9065A58F6AC78486AF041C106B9A41029D771E7655C2FF29ABFE57819B222084A732D89F21D4A840607951F6C9644951FD22A61BE67A8EC48DBE5B106038F5BF0A1C144B72F4345C75448D72019142A61F532D8D63042C9FE3AC78402FFB3E80FD253470C40027446284F3B13133DCB1D02C3C69ECDAB6A3A6CFC6FBB4D04FDC7EE2D63EE0475EA67DA48FB84B0C71467D33631BFDA9766E78F830FAF896C0B060C973C81E113C22F066BB0DD23C49E27A0DAB3FCDED90BB0C24B01898C1847A57611CF9D63F092CF09BE34DE3D57DFBA348D4507FD29C440BCD8FFC7CAECA78F6399883D4F077A1AD90E1798CE97EC8A84C64C18176673FA7D174D5D1FDCCCFEB2D7C88C08C55FFABC00CB1F2A49DA6936B8EC7E87995726D86CE041A3FA7F939CDEC9C965746349010002B45798A23F23133D38D85D347AD18E7739866F93CB81EFC2CB2598E63D5FF2A10AB2761817A6112A877BF0A6037DA139A4075FD9380F54114DD3C25D32861F0B3C08C8C2AE0D2C48C0A04E285E6B5A171B0D0FE2882BBB7203B25718666DE29F68E8AFC5CE0E702C373411D78189E0A6A21CA3301898DB3DB20DAB325F807813C848CF8B90021C344A9868164284A194EE8CCD6165D96033F05D92CC9ECE0E725A1EF0EBD01306733F8D94684AA3B22BC05209A6DA8743F7AE8F5D06B2CC9DFBB418EB8B33087CFE5ACAFFFF809459ACC9D14317666107899447CAD5633CE83DF85B0DD5FA0F7A8F3CA29D4317CB5852D521300D9B9F4B20C0C99BB0EE321C94352C3CF0E245D260798E550FF17FA96B104BC90ABAE19442EB233983E0629D8A19DE952E3D9E89A970BE850D76D9BD99AB0E135D386492DA4571826C763B2AF933312B52350894483030EBC9A72D611ECC75392E6E52A3CCE1E414AEB4F029D50BF0E79F0F72F672D11BDDF213C6858DE82DF0A88D1194F2322E557F004C308A0BDDD240B22922022999F7CFDE4BB92C997302E8D2D02267214227F26273333B5CEEB310C0855C4CAEBFC69BA3BDAFD28C2A76D063AB9DC7CEE9A230381C86396C72C630B065358A58E5196B0A9FE1EA3F2B5A75B01DCC1F02BC849910F8148189D48FC27851E453C8A184391D1F2CC18968CA428200A838F195C6985930214B9BD8ED27AF39DCC59A1C0087902E1D70BCC4B9EA302417ED705E60DBE71895FED79CC5B09E68D4697B1355EC55D616547A8EFFE7A0E8B681248E647B41FD1BC798C8C8C64D911BCE8C8D5B1DAF939C941341D25CD8F12FBF184ED775BA7FFBE80870C4E0746F7A38F5B3CCAAD04E5BAD54FBDEF606E9556F357599F9138AC616576917D0AEAB13C1D52FDEF42A898A6F079CEACFB999FD7799A1CC76CEA5F040ED04C70EB4E08ABEA9D2EECCBE7A312DB98FA1E9C82342F520C544C8A045A5A77D79CE3A840D4969F4F51824B7B352FF5338A9F518CCD28ED410163534A2B40614E21B358C3A482809ABAE587251001762A774CB1009CCE51D4892DBFA967D4CFD192FA6B4EE521D543AAFEBB84491C83D0CCDDC18EB5CC5D414A65A273747566CE312A11E84E2D7916518DB3C3EC0ADDE0E7E586B6DEE12837782CBF1964F2D972D5C7CA977DA25C3DB19FFA769E9FABFC5CC539807F29E09FBAC72D8E27C770C55773FF6397FA784507F273F83CCB9CD3FEEA47BE1FF94646FE2F05C870F1A00E0420F1E644027275338880644E39B4BF8970695A3DE1D3FEEA47B21FC94646F2F5C9D438C673E61CC5A4CAA6C6703DCEE6E3B8FF5D603F4DF9650CFF620F9B9BC701AD38701667BFCF37C875E0009E33270E902A9BC1013D5B4C35764DF9F4BF7A1CF038E0240EEC8208646854A341FD053C3C25C95774870498780E842C4BE67110116E66B063D48429AB59A1C08E43BC3F2570BA5FDDFFCACFE96710CCB26FB6BF091CD10D5ED0C982C911DDF647015C393527F22646EF7F16C1A8F287B0AC3447A9418104BFD2E7080CAB1201AB75B5DE25C5B433678502DF31832CEFABCF910B572EB043558E541A775CB98C4D3EA46992CE37C0B104D2FCDB570069223A1A7E2957415C0451CF0787F7241A017401619122B4CB83E369822DE322D73F5F597CCD2EBB4D0CBC558B3823C692CFD6E1AB9A99A390AC2987F6B7C53FC4FAB8D4C7A59C716929A97A3E417F14DA72968939C9750D8DDEF6A1565CD7CD0AED7CD3AA0C82BA60C2ABFF599017FEFD55E93757AB8AF8E7622745FA91CCD2E8F9029A133737C543D4BC59AB7D20E184488C293E36861670490883E80AEC61405CC6E14994640C34E51037A1160886FB8AF3FDE959A1C0347C2CE365D251F459A1C8F4FE2BDC83643AB9373F8AF039877F80FD757C979CA6CC86253E70F1818BFEC0653E80F58730731932C10C0F1733B88BFE543B06737682FF0B4C1EC16C7F13DF689BBD62302A9042DC72E199CCF96209447064F71287F80340C3128F6C1ED9E491ED2CCB103454F95E717B2AF7F59E8AF896495B91B427F20603947BCC4E44CDE77E971429F6C63D176291EC8BACD309156A4F9D505CB63DA44D19727B7E7A8DEDA9014DDBD3A813021883744AD2B952F34BF7EFACFD01F55519D15D257B10657DBD5DF8048E41A54A760A42501F5DAE1E7344DDFE1064A026F9EE55D9F6E732844BCB19E725CBC1B1F68BDD6FD1BB0856A7EF5B82AB20868F20CBEF92AF20FEE7773F7CFFE687EF5E9D4530C8D0CA3B7AFCEED51FC728CEFE1116599E1C83384EF24AF57F7EF794E7A77FBC7E9D5512B3BF1C61982659F298FF254C8EAF837DF2BAE4F5E3EB376F5E83FDF1F5B47AC3968BCBF7FFA3E59265FBD12EC0604E6FA728ECD31E3F9553D4B487DBDEBD058F03E7783DE9EC69C59F300E85C4FFF3BBEA235135C83E82B2D34B30D9DF04790ED2185181AAA1DFBDFA54A094D85149FF1844D90CBBA6EC6FEA13F248C244D2D4A0FFB888F7E08F7F7EF7FFAA9AFF7875F1BFEF4795FFED55F5F2DB3F5E7DFFEA3F859B51070AB5F4E7200D9F82F4BB5757C11F97203EE44FFFFCEEAFDF0F59E6E9FCCBF994E3F835BA815E822DC33C3F57337B80E2CCAA69B8AE5EC4F0B702C0AADF1E216A2387C1CBFA9D997FF8B75717D9E78ACB3F5EDD95169130FB604297576A3891D75CD04497C323481E1F335041010861568DE9BF49B26F715DBE2387F3B981668EA777DE660E27752AEC0C86DA4690476CC8FBE1E58797C1E1755186B36919A47C4C93E2B49101B692A9FD8DC440AF3F4050787E2FCE5357C07091BD4B8E4798655510EA63050F664B83D9F00D581F30D058DE82A86A9B0A52E2782801E645562E8BA31E843C7878F0580E3CDA14311B05119C7AE2631ECF4569D44FED2DDEA8390745183A3BC12628FA10A38A4A83B764F6333C3C45E57FF94D92C17C101E4DDC97677BC7039C07387D00B711701B1CF412838EAEA2360853832FD586C40F4990EE617CD8E5419ACB782A0F06F562CE610CB327F3728636322FADDD14C94C896A62E65DF1D0AC9797D00F05D78DE0ACB9A5A100857E1AF2D390B669E8DDE039DB8D4C49CEC6DBA42784E51A36E6A2D4300F291E52D4206583306239B25503098DE0F00544617204935B132683C08D45D068F0999732ECF1C55705C1EE2B3C9D8031DF382BF630798619BA088BDEB64DEF407A440FCA83531EC4A1147CF2C8BD28C7D873A5E4A262AF8A0C868B4A2CFFB92FC2C5C4ED4E20F8BA98B4B3E712BA82071895D3C05B7080C6D67943411FCC2125CA35BC0F8C03B28F0A7D54A8181596F378526267BDF5B1C11811AFA8C29A6EC2C72F373DB078606101CB46C0C4F6825311C3B462D7C734D8F71E5A3AE411BD583670CD377F2F3B200C10C31F44039B0FCF4154D457E79477E3518EA49E9FA950AC97E0A33E0FCEAB02E7DE753702D34EC77C8DB59314D3CB3C33C9B8BA1D08F731A4872983308518D35CE43F378253E84FDD9720DAB4000AA7844BEBB35AF6C35FFF5D1CF8AA645AFC4CB9A22EC4F35D123FC2F4A836B06F822CFB3D49F73F07D913AD917FFFFE7BE15BA72DEF4FE077FEA3EA7CA75646B92135B7FA298941FB6EAF31D65A3AEFEEF7E43C08CBD1A9E148EA65127E4D8ABCD91CFE9C8753041555B663A87E5A36440946CF4B97077BF5434A08BDD159AEC7200497417C288283C4F962021B525CE2D7372667644DECC5D71348B924DDFC7A62A4A6C27A62C2C7AF27FC7AC2AF2716D8F6188DBB8D80932A28E90523BB1BE47E4FDAEF497B705E17386F129459FB277F155F33BF4FC2E2087AD8D1B383C0DC94116759BDB3CE32C08FC6F623C41BFCAE485318165171A4F1FD9B38E33B9847BAAD70991C2218EBE6FA1E64610A4FC3EBB704B715667D95E4B03EA07997BC07CF204AA83B553F2A8AB84B8338ABF2DB6A16F21E3E83342BC1E02CDE5FC46154203CFD1001342633DDC2EE9E4ABEB7E50CF61CB0FA44CA6041FAB59C20D0396E04788717DD0276F008A3201D1E0A6E2ED7E996741DA3E1D0A55FFEF20452D0C6B25973DA53FB209C08456A82382C3B2C08A99BC732A857EB72F67B90EE59C693E45D272AFE540EA2503B66A7E019264516BD3443BF4B89A25B97363D2A28076756CED945FD0E8266216969A3089CC36894F6DEC0F9E15ED0873FCAC56BC6008137E2935E04EBBCC2C675194A32A4CC755E0E7BF39A74624CABC1391DFF7789F001C65FA9A3E26F129395FA1D01BF01E8D7988BAD31DB0C061B596B3AB501D8DA56BC197D4DB523F5D8B4CC7A168A1E803C00C901D0C620679418577EA837D595C6BB9319DD4D1CECF2E0E3C147F1F3E7F07ED8B600C9D98B743E1EF290E421690649CDE7FE8DA00FFB6B9FF0C84A23F4C6928A835C64E8BBD6E083A1CC60B8C8D4CF934BE78AE13B712277A681AFE5208ACCB61E4930A9C1750CEE92F28F2B507DEEC9D03B5DFB2232871B4DDA900C97D857FDD8CCCD2035646EB66746A24C76D1CDF4D0EC227A0D8E0619D4ED133824395C40AB812093FA0CBE670E2466F59C6E34271655B2C94436981C8BE5CCA6355D6AC3A864FBB67801A9CE737604B3FD0AD3BC40EFE1D6A8F8AF04C657302E72D07D1AC98E4114294AEC5F7C596A7033842F34347AD1A551E1B1381AEAD1819CE00F24E7EC744A93E7FE13B3B2B03ACB17CCC3A72AF1F11E3EC33DF21BCD036024A68E958D88785BFE11832CBB2DB9ED33335A2C3743CFE599F4EF4ADAF2239A2876415D0D8CE2A984F1F87D1B94EEA92C639019B0195ABA3DBE976076640D34596E7811849AF4BB5EE4F2038D2E7B69AD0D0C39AC98F1B82B0B4E659407D4858D336628BA2BD721238C40836171234EAB9B2ACA5D525B31F79413211ED271C9191D7658024771124DC249236F7904A5085E545F03D839976168C1E1BFA8F82F2A6A1F79774104B2F664FA46BEAF58BE553A32A9782326D5F535E5C333B267D71E3D5F9E747CF141B75B472DAD34C68D2CFFC5D9E3A3457CBC83E15750BDCEBB11A4C46A297F104627740D0FD5A0D6A8BD985273506A50DDF9EFA220CB2EA820FAA3C4C5CF8E35EB6CC00FE2F72D3C087A10D47712B0F6D58DE19F73E70019D38F26881E32D41767B6A2E828F9DFC52F090F85181E58E730CD984909DE48DCA70B6AB6D43B751218FF0E7D3F40090FF446F7FF4A1E984909245AFBB6643338AEA66B25D2B0BD49CB41A136BB74BC4A685562D4B291F656FEF0E1734A4DCC2171F9F322AB396B3168D348457B365C8C9AD3C74B3E5E928B97A62B8E8D4449CC3589782266EE0432A28C6F9348223AAA6BF927903C6A58400DEC2A6B3BE8E1EC1ACBB91DA60B740CBEB9A0AA23FD37E64CBDC2A7F0859EF6F438EA71540E47D124BE11CC64455CE27191A6808BBB33B6F85DD9C07ED017F0F094245FDB87394A05C9E0270EFF03769A766E4AA6655B53906BE65BD62E7F41CF55540F9F2B1E8BE17EE7C4DF61F6D3D352D35373B8A43A85B11140B47CD046FF2CB9DEBBC71E973C2EC96E5AC68F2045F968B7854A15D04A4353535B099F6E9344E2304D5D4BED73AC87310F63DF2C8CBD7F8983230C378266BD5E32FB937D5D2540917FEF8FFDC41F57037E0DA282B5F6F66B390F360B814DEBD51BC118B18514276C899CEFE11E1E8F4111E52AC343C77D058F1B1E375483949B003DD9004FC16636839C08541CFED88CE979B9C32A14568A9F9FCBA1730B0E3043994495C0C423A447488D08F90D7DD9FDBBC429DD4B10E645DAE733F723D68F5807466C7D73601BC37605D3B7FB7B3412E7843D2C795852862558F24B370744165759B545C51BD0D6F307ED3D245880844D0181E3676FFE5DE20E6B1225D4A7BAC55FC8F458E1B142397C4841563A4DE522E7497A0CFC86ADCE5062665D89B002C3C387181E362CC0C666C1C2F170E3AFFEF4871FE4CBC706BB9738396530DBC820772222707F1FD39F35F36863036DB6F941C5430DED9389471A8F341690260DC2AF1E69F4214D655071F94D35BF99E1F1C0021E6C09051CDFBFF09F4B3C54AC192AD0A5558F143A90E262F2DAAC3FD4E9C7F3F2E379F0FA311ADA280E86878D0C706BD7F20746AD0D2ADE0A0C0BC56B29459C9F15486C0EC33B543353F1FA8ADF551097F8A5CECCC397872F65F8DA1474D97ED96CB57945AA8796AB471DEE8A949405959FD30E8449BC576575576A7711972EF11C446F41FE3B00F19065FDE09992DA48C2F5E387207CAA9AAD99F3A4EDBA4578F8F7F02FBF1AAD765CB681FAD602D6157C1FF3578A3CAC2C052BDDC356A57B3C57EE719D1E8218FE593F5ADD3D20BD11D8A1AB2B9F5581C44F09291AEB2729A6F779E2E37175A5A67C4C833EF7CDBE74E763108D1CFBCDDFCBCE0A03C4F0078F5E1EBD5C40AF8D6096EDCD7B4D50691222D154750C60B40372EB6D1E94320A81BD1E9972A678F41E63CFCF9439CEC2B038161172F15BF00CE242CA365C0A5D05E95790EFE09FD4B0F94789371B8B0CC6204339D8603C7A4B46B380F3A41C2086787F08933839C2F02AD903EA6B892A06828FD599A11C96FD6548C62E47C907F5F21E5E5DF97C8A92C0183AB45A5C9F10EC973C82C84487FC5A864609E3554C453B9DC3087CF8A39C5733C678104F45FF2529A2FD7F24C525FCDA270AC9416BBBD1669B4CCC45E07F03F3F049F1B3F31CEECE931421EDDDEF207A0657499C3F653E40F601F29A02E4612EC08D04CBAB5AE03B9BD9D1038E071C3380733C95AA78B8B10237D5E723BD81A3070A0F14463E3CFC5136056EE8019F550105AB4FAE4F3AB700E77CD582AA7DBDED194417F163A27701EBE1CEC39D1CDCF1B9FE46C08E1568483CF55BBF205E0D1D1527FA39C8F0F8E0AF2278347025F83947FBA0C02F912C443EE72579CC402F89FC05EF8A3485611115471AE3BF89DF6EFC92A45FDF833D0C258D39ADEF778D3C1E5AC0C3B11B6E04F8DC8D82FC40F503D544E072FDF07F41FD12DD36463049DBE04FBDA14BCBCFC0A64DD7257AF76C666CFD968DC7BA8D611DC3F333BF65E3B76C3C1A7C2B68408F7C76A5EB04304662DE83671025A763E957DF4A3CE4E4568E5447E98D923885F9D8C9A3E5C6D0526A3CF8888A1151812C4CE189754FC1876A1E7CBE69F0A1871577207C8A932839BC6C0468361095F57DA237009BF3F5B19687BB8DC11D9FEB6F04ECFC3E9547038F06F2C1CF86D29F6F21EE41DDA139E419B2F4D18EC7B78DE11BD3EB37826E7EFB482D82BAC8CEEA4F2F2A3C58BEF6314D8A9301E46EF892E0DB23AC4758F311E4E83EF937134D3A7717DF478E1ED73CAE2D113956B3EE46E0CCDD7D321D819907010F023A41E01B19F412093EDE2761810E69E8C5921D489F61084CB4976F912BF136E117F090C15C777B2F8EC101984D4D585D27ADDCFC3F4040826E1F7A79D435BFA4BC2A3218BE0DE2FD6613CB771ACAAF1D072CBEF9F4F13E1AF4B8B4242E6D048D2CA78C5700415DE0E7B3B5135A6C3C4FFE8A933CFB89C44F249A26920DA7567628C475F6A38D47128F247248B2B550B4D307C9508807EBEAAEE53F3E4FD2E32C70D3C4FB2A8071A5FE45FC1815284924F5E915892DCDF320040F49F255F79E669CE5C1210DA8C9CE24F8DEFD0E91376AE65A869279F1A0BBF3EEE0D75CBF61CD6F16D722E80FCCF88FF67E3A5C7C3A2CA7A4742B79141CD81FA9F85436956C435377A173421239373DE678CC51C09C2DE10D2BF07D23F19DDDC533DF7EC8FB21AF18665C81E3C366526F3B1067185873376BE2ACA44527930C809BC7118F238A387217A407909F15FB2D3D62E4009E8CED2ADE8E697DBFCBEF71C2024E6C121E5833B1BD3B07E53F518E39B94F10A3CA4A78E1173B1E7C1C009F3E4801C1D12F789C5FF0DC26916E961E3C3C78A88247F9DB4660C3E1FD513F50FD40951BA8B7653F0519D85703B6896137325E5730CD4B7CB01CDECEF3D3BC470FFBD3FCB650C3C1F3CBBF96E331619CB69218F62DDB9B20CB7E4FD2BDE6D8A6D2FE8DFE66577C7FD0CFB79909695CFFFEBDC4A3AE113C9D607C7843DFF39267FC8329C63FEA66ECE7193FCFC8CD330EDC93E14998A5F8AA344C33D1EFD4DC9707D1DFA807C32542519EAC1F1213C7DB607FD0FC85E0E62989C1A7A2DAC3D4CAF81D88A28AB9E6F6160F110CAB4B987A199B494F7209E3AF600FA9B954241C81E324BF0457AE7B07127CCDDC1038DBEF5390651241685B51253DE8025709DEC2347FC231171E8A8339E2238ABF25EEF8E178A8D86FCE8FFF30B384B30CC5A1CF016A06A839E8521F715B4CF952B152D7F445CD003D175D4668391A3504FA7E1CBFEC4EE5980CA24F00EC33427CCB95836BC086336B96DF81B2BB32E0F1F2415DB5B34D9696153C3E31A82BA023FF65FE7A82DDC8B65795A1440E309B8A2A18B92BD94BCC564D3525742E8D25A174554B456EE33DAC5D2151B6FF079EDE257B6A10FA463C62CEAE82B808221530BC2C4D9917E692ED5C26F1C1A8808BEC23482E93B072891A5B9466073FE5F9CD30C9CD3084EFDB9875CCE0BEADD31C3A70D2C38287053958A806C54670C15C346A0B1B585199443CE2C1C6838D2DB069C6D946E0C6C278FF519CE1F114C42FF547ACAB20A3660D12FF30D42C1CF5331E7C7AD3CFFC2A7980918146EB00D78B6CD465B7E0B702A67ECDE8F19AC2DE185E6FF019939F93680FE383788CD85554D9BF634D197F7F2311F4D670A17F32BA4369AE0DF035F3EC8BB1C31AFEF0833FFCE0D6E1878BEC5798E665A071054A9EC4B4E4FE13AA8F0D1CFD0ADA4CA61B89290CCCEA0BA0881FFA7EE8DB1BFAC35341DB4001732B0BCE4FE5F1A12841435C7E5F53EDBE5110157418F4973F3C4859D9BBA81E05DECEF7EFA16AE2C37D5C5B2DC772635789A55657D32779F6D8B365ECD95EA0E30CFCF890C7C38E871D3CEC5CC4E52807D956F22B388339AD5D25EEF976357DC8E3B167CBD8735B7A790A43F4D7EA22D8230C3D0CF9D0C7C38F879F25E06793F9AF9D411EA792E4F30743FE5D410F46CB1D73465CEABBFB1F6A07ED1F93DFCE3E90DAC3D81A1FC46E6C2CDE86AEA27FA9C3038565A0A813BD6C041A58276224F393F8F72DFC907568C8B67979FCA0F583D60FDA950CDA7F250F77308FB6B22FE04C10EEFE8EE40F7FFD77BF07E0216779C8B982317C0B138F38DF1AE2F86F201E72EC404EF5ACDB36F0C62F4BFC80DDEA803D1E61861AF7E1B99C4AB6F46DE0A67EDD477C6AEE2AAA7D11A8ED99A47277A026D5959AF211DDA2EFBC53737A498F3D1E7BE4B0E76653CF6FDDB8F106F70E441148DB87BCD48E6CD078A91DE02865463751F002E3C31D6465F7100FD7AA141CD78F1F4E304BF6FDEB3113DFE541B70F41F8D4B0916A2FD77366680D7703D2468EDE232495A92B09D78FED80D32AA0E27D169588B67FB90D60D603AF46FEBB1C46552A7FDDDC2FB21B98874FE40C165C0E770E639861DF02D1714BBEE9B6B7C54BE9235D94965599EC54660F5CECA793ABD1B0033D0D8553C0A72AF0618E9361CE78B59F1EFD524BDB52CBAD5DD8B89999FECB31F8E3BF7A98F130B328CC4CA2040F303A00A632AA9E3515859552131B45FB9E4759D50B890C7444467A9B57BA5B91815B1064325664B053499E57F3A0AF32849719F54A5A6E27705857DD8B7B93C9B506CB44C746A9C1D8BD6E337A9374570E3BBF42F053B75B53F727704872B8C1F97A0216D2383FE3A30438B74972146F4A5D4B6D6F360FD2DC98937E908336CE1B7725ED38B3B1CC30BD4D8A78AFCEE6223B2B5077E53054C136FC9D1DFFE9DDA33E03F51D4861D9AE200653C7F0ECCD46A6918176E25D30AAAC76C5B731B6DAB1433C177FEDCF838F10F868DD2DC22FA037821E8E7C91477F6ADE4ED07A78F03DCC4E51F05269E823210F460E8051BDE5B81118628F7FD1E1CF7AA24DE61844637CFFCC9787008B1050BD3981DE25DAC8D8B7FCDD8A793BE8477F3BC80FF2C507F9B6F2C25A1EE33E1BAC071B0F3644B0B94C0E653CB1958B88FEA09DBFEEEC31C6358C29FFB72FAA5CD33751B0B1DD530F357EEDE4A1C619A8D915C763B099746F1E63FCBD010F2C4E00CB2633D65BC617A7F2D47B84F008A18610202825F88F33FEE38C1FE51B1EE51B4A4CEB9717ACDDD2371E5F3CBE2C832F5720CB4A97DE08B4587EEF6557BDD4217397B4AEA7767FAA74B253B9249100D64155B59515F823A701DB1B894FCDB27743396D1618BB78FA2941001C5636DFF82D576EB44130F639CE8A07F4F2F903D85FC28CEAA1FFB9115C92BCADA47E8B7BD79A1A595A0298C6D5FD4E878F51C8EC8DC528232FFC16E002FD2970628BEF3E0940363C55B7D9E9ACC5798B9D7CF768E1D1C264EEEC20CD9324BED9541ADB7AFF87BE2B291A33B667F034EF75B6DFC235B3BD2AC3E9E780055F329C27E7843473AFF7E73FA791E6DE7B0B4B52FD6C3993F4F2ADC617C8D26B3C91EE184ECEAB1491E281349E8B8FA7FD0C4966BFD00C59D96433B929C6BAA90E54C5217A0ED32C6705F33233776084EDFB242C8E20D60CA0D5DE97E696BE035174F394D08317F1A6562CC78990F4306EDF79D31C5B5C64B7203B257186620115D8F573899F4B74CC257560B391A98485B0E230A02BA7877FA5CC8F7DE7C6FE306BF24610C0A960B21CF129C8245247771555F22F1B0837EFD04324AB09638DC586B700448C7D9C37FEC4A147F5A5501D9710F12CCCE13342D06DE0BAB399235B3B4B34A6ABB950D60871A0F390E421491F246DF26694B3C0E4D49D290F521EA4DC03A9CBE4003394FBDE43915128BAC8CE60FA18A4608776BE4BA5958646C3AB6D67DB890D6F098519FC54D6E0A5E661981C8FC9BE4EC7AA45FF21477D56E0E2AA680B989E9234BF4B83387B04A92E6F18F2D4E9155C7CD52CF20E015223E216FC5640456B5C64BF8227184600ED5B275910E960CA3F79BFF1172DED4EDF3C3B9C83BA6AA759563DF71306F4464201CBB7AA94D1971B663987F475FED46F02CB016BDB2474A2BDF92AE8737C3B037CDF14766D13B358DF95243EA9740B8F3B187E05B99E08ABC2121DAC3C007800500380D1A26D2330D0EA261F34687AA4EC260A64F64D27D5959AF2EE09845F2FA49EF3E5E77F5D987B4AD32F1D3D80BA08A0A341BA11E0B4BCE693844B1D30E971C0E3801C0E6C69FC1B593FFD9CE420525B2F353BE98A3BD0160F527E010F19CCA9B69538F3E843230F892E4262B7B6AC774E36028ED6579517D9A7A01EEE8A689AA6F0598DC9799A1C354F14772C001366586DD8B10F6FFFE018EEBE07A720CD8BD4DCF06F1CC018FFDAF29F4F51622ED19A9F9DFCECA4363BB58726FCF4A4677A427382E2C6278685DA01D644B14133064ACD59E91EE974C0D42F8213C7A6476F8FDE06AF9F26710CC23A2394E69C803D6B19781FD726F9B6A8316DA510452CCE0ECCFB94ABDF54303C66B58E35E1F7C92A9CDE487823966D52C7E5667F8DC4CF6A4BCD6ABF14F0CF8D0C54CBDF10F58F6A74012387CF4065107964F0C8208F0CBF1420DBD0F12CA492383CD4B594B0A1B3A39FF5FDD876646C5F9F2C8E6C0EEFD434E464C67B5B53DBEB34320381F96E97070C0F184B02C6599CFDBE998F16D6F6B76AD855F3250F0D1E1A9C81865D1081EC260AF2C7243D7E010F4F49F215DD302A27D18D60C5484371D0985457428F0FF1FE9440FAE6F80FE2A3FF6710ECE96755643EDFDD042FE8200663235F7C5BE4D49C8CA41E6AFCBBF86E4BF94358B255BD63D7B081F141854FCFE55D52F41D2E832F287F7BCFCED4B9984F65BC6B5E4A2FE1439A26296B735EFC858F8980F6C152BDE3ED2A888B20EA4551C199EFD9CAB0484B6CDBE5C1F1B4EDC840EBA42EF494E46DB29947B1912AE233595D4BF9F3AE9DEFCA8EB9B10F705713E0967E52BF2AB291B1DF3EBFACDEB1F84F6E62F34B655A549B366D8DDFA8E76B9BCA23C8FC124CBEE9BC926796BF80E608D24DF110354F506F64ACEC921006D115D8C34061018863A272A90BC37060FA0BEA8A4BFCD2C18035EBC575A957858E657C6DF6C8FE45F66B3972949E2DB9C8CEE11F607F1DDF2527153E3E0CF161886418321FF41B0159FD2767CE4EB0D4492FCF76EB8DF17886C4531F3DBE66B749A25FC045B67B89437F9688022E1EBB8C6257BB7BF24606B1DA56BD6693B6DB1AA200676D5762A17D1852479D65199A559098764CC52598832CFF9826C5E9FEA63ECF5DADCEC67A9468F80A35A33BF33D5CC1ED40F4F897D1EF574594C35309736523FEF9DD9B99597A76A3068C184E4AC62CFFDB8C65E9152579A9761095CB932C4F83D2BC73178271B91A0E228C32135ACED91419BBE33A2D790F4EE84E579C6395E5113868205E762762320458E6F8E9F5C019E83ED265591FBD37F6E139888A8AEC1E4F40F42002F9B0EF4924FC7EC56A3487B821B111EFE3B19B1947642ACCD3063C136B6EDABC34DD36A3196AF7E39F894E39211B7AC7B488DF09F14DA230EF498C381CCD16661C8DA01E8FE44955EBE8377C63A2752E5C211BF786C458181A1148601EA6A94C41865D8F6D29330E48555508E5262F95B8E78C78B530B11636CCDA8AA30975AE6DE76A89AD39D4CC7930A1A97AB4CEB300E0724ACD3EC21F876B0AFC859CA26A9D6BE1D47220E35AF0B424B028844DD621A503C5910E56E3707C93F0331555C86AE370827A2B8FC3EBFC5F2B88C2EB8632C5B4649B8FC01B4537117F4F9C7074FCBB2EA3EEA7B2EAE17C8648BB1D2FE532A3D30E4BD0C02DDF2D1BB406F8240D8439D1F6A1937714B80F9CC8F9E8AA0D03439A3FB0FC606D0E27D5E1369D6D5AC9DEB295846E04389EA6AD5AD0E10CCCF81B703F9589DF5D2FC4DC25E91DA22A1CF67AFD83C056898AAB6AF299B98666FC44CA37F10745169EFB469E2D8F4BD88A6CD0D81642E1555C094C617938E69ECD550FA217B6E54357E87E53F4347167D6E45558A517F4232EC1834B3856FC8503C6C8E045EE67BD6065CD85DC00242EC16EC04F59BFE21582F72F717084E17DFF0B2536EF48C6A14EFFB348F43469018167576A28E6C66B6DC66BE65AF1C5D76D2D87DCE532880F05BAB34FEADF8E60D8ADFD8F6B7314BCBE2EB949DB42079CE426487388548D5DFB4C846D23C1A146149BDAE1C4EBB8BAED4DBCC3D99BC85C70295B139AA8233935A90D1A8FF4BA837904EE89C51C6E35ADC1F406F1BD275ADBF9E435A486DD906ABC05BDB2D758CC3D272C5CF5561BF1989BAE67275293F63447C3B6F5A19F0353AE2B58B77E8883657899DA0CE5AA06905CAA29DC5600572BB592D8AD719085F6CB31DE60D40716DC1117E876DB5BE03378C0B55DA6D75C8704813EB2010735A90B6E9182ACE459159E27E931B0BA19306B0CC9753084DB9A59E60AAE6596993BD452330EDD7B16F39925672239377168569A7B0B4B279D3DBE565892EC77DBF034AFE6800BEE5EE2E494C1CCE2BCD73681C0B42FDED41CD7A9B592990DE332367610ED3A8B9D9D42215771687770FA55646960A16D2F2FF32963593F91F958E18C9BD800135B0E62074656F985E12E0DC2AF363104C927B9485DB62D0CA9745A0986D4CEB1D0027BEE0926FB7FC165347F97BBB372AE7B1ED372F10E731B07F83BC7020454940E78430B011F9EC1F453E61C076A220C18340532DEC139E5E8C5068CB6A65D430425AAE659738EDB2459EC720C9235B917986CE0EA4BA5C51AA6854FE090E4F52FC855E161A98E9F091E31C494AEDD25E62AADCC3F904F373EC2D0455F37737A50DF3412FB2185114F92EB5E7D3E35509047F0ACB98E79D81C4045278DF5B80EF76461CD5B10B9D57004FD6DC16DAE56E4CCC9EA1F57BFB1D5A9B28A1DADCE01CC82828D4E5F6AE80B75B8D5D13E58999AED6E5B2BD0A5BA5C70E569B5D387CBDEA51620525B1AEB5B76086F78D85E711072EB2E9CACC39534BA16D27548E4CF75D36770CFF7F06568D6E605E2AEC67ABE87486CF6CEB423D99C659FEF712CBF33C94D9BBF26297A878DE89D55E1D039EA1FB6E579730B38EC67A37EB3EE5E17719C3C5705D7E92188E19FCD364F9F44C0B13C11FC0DC78AE5A9B6A98C12028A0BA1A40B6926249C184FCA746742357E0FFB965C9CC7C6AB72763C3B67DDFE780239DC9AD3B74A09B97C5FE91B75F8CE009B75F70F7F945AC06A0F703BEEDE2B25207E58E99B74F78101BE057767115C9FA86ECF597DFC6C195F956D0F0319BBAF6038F0A9E5E8B0384F8AB2704B114FA39180ECAEC63709FEADF69B45FED6C5BF24E9D7F7600F43BA4B4FC8866E342DDAAACBD22CE5BA8B8EDBEEA84B5E3FA0FD4FF8BCA5D0BBD34940FAA0CE3789BDBDFE2AE81BFCB90A576794675261F7AC3A3BEC9E57D9F21090B1BAFB4381C1C3E9907B576479C93B7888C07BF00CA2E4742CC56E714A60682AD02626A76F72FA605965B3213D6B0849D5929A7F183CF967255E467E6C6AE88E4D8C5C0527777458DF81F0294EA2E4F0B2A149B0574A40FCB0D23739B50D0CB0D9596CE0EE2C02A9B969569D3D0BCDAB6C7B18C8D87D05C3814F2D578745953A634313C02C330717FDB709FBDCA93D1C47FCD1313F0E3777EB8C2867EB99B2ED39B8ADD3A2BCDA8BB8B8534746791D9B522617CC0C6A72C43143EA6FC9FF05CDBE9EC1C054CCDAC860B6EC639A1427F680203F76CC27416C583455C64EF8FD5FFE421B1E8647E65243A0567D8171A0693072E9637D62D8EC64E07E54EF14E86F17E8E91A2E7D795AFB0A77BD97A91556B3AE5CAA26F894E0EE07874F98DEEDB0EF80AEEE6D28A09FA3EE89B9FBBFFEED3AC60D6D76A56F72E34E3267C05ADD7DE11C026BF2D4A5730A68F147B7720B5C15190CDF06F17E05E904B06DC54A22506E721B18AFEB6A377DE90ED99532BDB1A7A43AC8F6FC906022D79DB0E3E08E072EBDCC9674D9F52EA6C55CD5F6FAB96BED4710A7E09E0D45F8FE94829EB16C3CBFA6C888732C0A2A138D7824DA470F9C7760DA3FED53427F62FBD27907E1EF2F4B1E5291DB77912B707C00A92504A985E319B6651BC29046A59581C85D901E407E56ECEB3412763C65DC083CE329CD863C67A2DA3A3C68E2384DFA43D47344D719D28C1FCD1EFC2EF0361CD96B167116A2CA66DC45C24B062D740E6968EAE8E8E475018F44E75A449F7115075C0B0447ABA14EDF00820F0DCAB73471F56AAD63D2EA775C7A1D6833D6980ADFB582B396A0FBE9F68FA5662B31871C35CFDE733120024106F6556BDAE7031605145C0BC66F956009D60F2958BDD68129F3DDDC455DC68D3D5DBBDF0256E229235C71E24313116716C0172B9F9184F1C5E1AF4698934A6E7DB5649CE5C0D26DC7D5686A6EC7F3163E34E49E6F2D7D3448D69DDC380DF4AE34FDFDD97E9F822C03E4CB41886CFCB85EF583D0C59E46CAD83DDADF8CB842D5C8255CA1D18247146A93B5DEDEE5410E38BABBA21B7554F38BE31D5EB7D2B11EAF1A65B9CB4BAF83EAFD4D7D84930721D6D6CDDC0062B78FDF25459CA75C385E534EDE496D7E737C70B7ED746C7837CDB2DEF9950B6AE9795AB77322C5FABA9C1F536C77F8E8EE16D653E5862963C013EFCC2C70DB4A643C2AFA81F08D97A66DD6DCE1E724DAC3F8F01E64610AABBBABF7CD4F44A768CB87BDD8FDC68F0573C9388EA36223DE8155D78C776074E291DA5473C9472E83F8500407F2A7B68E60D8A7FD8FEB7313BCC64EF949DB44DB8E723F4441F2E42280230E4F2E0BC287F0E4E20870DC7F3EEDCBF0C8E07EA2802BAD6C9F50C4BD6A335BDD101CBAE819CAE65B6D10367F2187992DC128CEEC7EE4F7039C7822209004E88A3AB15A9BF112AC5A5CE16743EC96C370DDCB9702FDB538CFD237E7A51DC8893BF1C346D88952092D20BACFB6E2559266AB085A49CE631F84DC70249B5024EA4CCEA1D1455CC6BD20CBEFDBBF50F2223504E34C48ED8F721E84658A273094E708A7B579BF1193DB10BBE530F6E1C7B6F3D8041E2107720E756ECB96A730447FDD9D40081F61682916C2B484E84E58DA4DC546380D571723E19CCB3E58B9E96836214CD6D99C43B3C94D43FBBE46B9664823DB9C87495C3574DDB96C5E6375D5C596BFCCAAE8648EDC671D9DB430FE09C4E637B0A53E8608839D035F44C6E9E8163B6F434C31B74446B9E5CEDB88A78DB37CDE66E40EEF5240CD90A90C0A36BD602950107681CAEAEE8042939EEE2648738814AEA60BAE04AA52BDCBEB2EB8661185E0890D1DEA5C365D2A534D2E977321532AD3E9164A5BB9062F5B30A9A51607B39DE37234AD7D44D2F860AB2125F5785BBC9AD007D37827A7C05933DDF09C8B785F64B323FB84BEED88497DDC13ACD27FF0C670D083DA863AE143FF4A1EEE601E010742A8B62944C63DC1E642A54EB55586479D132DF7D5CC25C759F2FB98B4D358FF28366CF9158CE15B9838003A4D4B887CBBF2CD414EABD92A11A775203B8063D7696CC18D88C3388536F748172E78A90849BD5A17AE32369E1BC0BCBB08030B62E186BF18FFFAE4379A57F0F5C96C4E272917C0C2CDB6BCC062DF1F8F30CBA08D570171B2274E812358393E6054E2DABC752297579346F16C5F6F2507D145FC98A4C7DA6D960B4B69CDC03D3340205C7DA04A556F15112BD5A1B0493BE76E80EBF1AD789348DE52DBCED430B0ED4B6F8B17900E66B3EADF6D2A45A1D79C996F38CB1E14C5B714E76B3312A3C952973E284A50924732B1579D74BFFEDF66432AD71C6BA9B04AD591C6FD633DEBEE277048F2BA64054F27135A8B9545A4DDE4F3C9246D799AE0E403CA24C71CFC4EF4C721CDD03546BF6FC9FB884671D7E90675ADF9DAA00DF73CB86E62FAA3399E841FEB5D0CC8CC728ABE25EACAF8963AE152B74972243A5055387A7CA7FA610DCE31D7CB0D5740ED72A2E38D7FDFB0D7F74B85D9A27DEFC0C70DC2AA6CD9AD2567D663CB6F27C94C568E6C24F5AD2E791719B80541D6BBCE92CFDE521B841340A2DCC2A3B8741505FCCBEAC383745CE251CEBC7F30CE79380A6E0A8E611DF2F06D77DE495166FB827C6395444F75CC9666F5932DDD4CEE7B62DD6AD77CB0ECCE08183EA0E09A43AD6037BDEF16DB0E73710C0EE012C65F178EF53BB9387E83C28DC4F7BD462B8AEC2D645C9C48C63AC776F22C4E35E212693BC5E2D43916C60D7B9E610135441CC311D0B84C0E25CE2D79DD6B2C18E7185DD1568EB2B50AF14874E5F05AEB18CBE28535AF581E2D449CC211B028FFB72FAABC8A375160E168EC583ECE49A6145B4190895E6B029289D32C8B278E78CCF2E822E1308E80CCAE381E83F46579746904E39CA42BDA0A9EB40AAD09485AC7581641AC79C5F29821E2148E80C52489EDB2AE4149554BA0D888A34864A675D35F6C263D76CB89964F752CEF4B8E64396E15004106520B7BF2BD60ACD70C4AB7023BBD4A6B829C85D3290DC5621D6323E99346EAAC298EBD1FB69C79BE401758D870070B10C1ED0D8EA083C583773C9EB5EAC3746B3B36D71CFF70F256EF82CE62F9DEAEC08910A76EEA62939E2C1491BA94F464C149473AEF89EDC9E70A645919092D95F7BC113762D3FDB6F62CE6AD223CA26C272B6FFBFD1694AA95AB673230281F335CBECF973A4828D2E19DA1AD77F90E093278ACD4F7771D17C43653CA2393542932B3FB5D52A4942D4DE5FE46F4E86F6F665C9A5F57DDE7BD1E3CC2AC9E0C1EF479BD314AB9073CD902A87F70BDCFAB563AD7E788D26E9F575E878E1C3E0621D0BBBFC8B823C5051E2BDC52E44617ACE9AD7AC3E7382B1EB230850F607F09B3FC7ED7FE13FD8BE81563AA61974E4AC44062DA9A99B3CC098C380FC508E63C68A61B57E0306CA95B9E64F63A924BEEB2647421E5254EE4F17A17A47992C437D32B754B65D865B5039F0E8C48BC6A4F62AAC7B52BE14416DEB10ACD03A1F7E35F894E35211B6D3D4E8A047635712DA2F0EE28CC3C0240B18419E7C26BC723785CD311979AF8D279950696D3A31A6272DFB704B2DE25E0B246FC09678D25BC4AC29DEAA63AE254A3A7CE177CE49AD8088A232DF01EFA920F609375E3916EFD396CB21BB933E7B9E358B6673F61FF72640AC40788565D0CDF248EA87D33AE45506FFD6EB5D0F764D73C68C1AFCD0AAE63FBE333BEE9F854E07CF0A4CD0FC49D8D9D7F9C426E362DB82370269F1BDC65885B41E6FAD538AAA5FCF5BADCD4A514F6C3C69C85397C2E3BECBEFD0BD9255B82915F743F4AB81EA6194C1F2709D4E56E582B1876319C7A5CCED5103BEA506EE29D8B4E671BE3A41DD075749BDCE074DF2529173C79C837EF9E121740D7E6A436AF19AFC52597BF80ACC9191DB98D8CD3E69E0B15A59E2F167539619835F074BA2DA7E2DBD87315CB16DFD473C06B6C6CE8893A8C2BDB7997C90166390C77A792E1F2EE32118FF59819CD569C66AAD89AFD86AACBF02033A5C3191DBD2E7F92E95C8B7E35A963CDBFDA76DC9F8561723C26FBAA904B3333EEC038CAD1D6C63AB2E1F59F0517EB34E3DB0E6377A1038E06D3C72005DEC556E962D4CE73C2B94E499ADFA5419C3DF62907BC93ADCCC9383AD10167736A37D5A6472DBD6F20E64E2EED7F76CE531D100766EF7ED87089A5EE7208B9406F6CEB0BB99B2808174B2532128A459BA6642B4BFC5A9D352CEC6B3F58EE0CFCDC054C76FD8227DBF9BBDCF621F6310460DA2DDE5DEE0E7EFE6E597CE85794D62380D16AFD9EA2857A97F2C408A3E66003860985D1807231EFC12B271256BAE84DF8E04836325C8FFB080585D63CA7AD69DF69EADD1C1BEE524BC63B4A5BB61D1769345A9573B4DB2FF7E76972B43E3DB5ADC17A4C5FB8AD49A9D38B47E6AC9BDCF1A0E5F1C5A6BBD84018214F710F62EE120F30CE03CCA493AC2FA387978C5770D188D45C66FEF16FE09211515D91859813FBFBDCCEE946BEFA3539E7D219EEB53AA71349EF718D59FA5384922BAFF7C384B0F3DAFE3E81F5152ED022F6B04E90B2E53A2E8010975C670107F5DB0A66C1D9F34164A2CDCF7ADC2F11393FDB55CE47578D8064337F60F9C1DA1C4EAAC36D3ADBB49235479B39D82D88821CEC6F967FAC4CC963D7F97C9994EFCC7BC81E4CB5F9022CB88B504A8B75BA87D07D7E17FCA1AC1F976D40E1B6D9D381BDA0C93DD6FEE7559F101C28C223CDEAE9C05F0AF8E7522B71246BC4A3FE61ED2BED4A8B35ACAC5143CFE2EC7790DE5F9FA81DDE53CEFAABFD59ACDB1B795366EDCF1C070AA5BC60D0E0851C0167582169CBFA8159A01776A39501BDA07B5907FADA39EF7F2940C61CFE1DD1B4E7FA028310A0C91146ED750F01DAA659758AB611F7F3794C74EE66B980B04F697483A5BA5FC8DD50056B9DBF0B2290DD94CBD1C7243D7E010F4F49F2F516FC86DA7F3F2A233AC5986AF42CD5B884DF4D288D220B98D29979A48A6C1233DE44539147FCA8BED5B7AA903ED5DB87444FAA0A75BC79887DF270232F1EF26D3025961F3CAC3ADBFC63644B77F6D24F9ABA1F517E01CD46C74DF1501ABBFE50BB4344D115D8C3803D7D606847188F2B17BA7B866BE248049EC0CCFCC1B28C197FC26AC8357DCCDBBB90AB7D28EBE42F659DBCAC01D26E7B720FCE619AE5EF833C7808B2F964826AED403ED9DEFEEE555D82DD68DE854FE018FCF3BBFD4352FA45F030D8B88520C378D658C668F77B2666548A93342060CBAA1E322DE7FE8F69529C30D226E538792312B6C4F9679B99D039094EEE948A2D9AFC0972DE9944526CE712A8E59AC4D91CFEA6C835637C168EAB49E32ABCCD1BD6E26F2A67F3F89B24D90C4A067D72A32895A84D24D6936D307723451A26DB98E1CB9C9CCD1A56E16F605F8BDDD43A8C9C35A7FE192712950858A07E5951C802C42A540B106AC93695BB8122CD623786D9081EE16A42DB098F29BC276437A2A5E59FB229B3357DA296C3588A583A392FE6F237AFFB4A386B49578213DA14F2AB3FD96F22EA3DA1A3293C22956CC91D0CBF025278C8ACC1DDBABE929CC3D4F539DDA525E675969A9E63E40EEBE02D3627C18ED60995A44DE89DC6D7141235BB49F54EC64C7CFD334E142A61B3658D138EF121382E9AA1FCE1B95C099261A029A6604145C1E34571B5EA0CF1FED317E23DA72D1711F4FE250E8EE8B628455E474317DB90B1A55F06F1A1080E3825FB229CACB65444C19B32228368B301DB83043ABAA20352C9961006088596BB457C438958F50EE6624D6B2A08B5AFAA23D4485842102E3C9E93301A525171EC8D9004D2C4F0321F34A70C444A64A802E4F6756F9A8E187286BEB31A3CFB421C8DE26D8A4C03FAE6EF5EE2E494C18C6A959E886E8B9E8E5B3E7B3470B9BFB0BFDFA541F8952EB7A660C845446CB9246914199C9C6F93043751D73FE3038184635AFE040E495E6FBDA20A486178C0C821D0E1046349855AC2D30ACE16F04A472D457F2398B82E22991995F2479717719C3C578D1B6EA971ED23B0ABD2A24F566D5515841B2ED35CD54672EEF7F154166FBCDCFE1F89D7F104CA0A526AF4556594686B2BFBFC1F2790424290CE5F55C2E7BBDA3C9B37740EEDC135CCD60E5F45FCC60F4F5D55FB9F27455928EE3F5D3D71CB3755D92DFF92A45FDF837DF72D76D6C42901AE2D631A55735D3FA06D3FF82CEEAD839AE226EB2ACBFA6ACF40D057E715F97D755A57D5F8BB22439F8191C8F7E01944C9E95806C3F25DC2E427DE510C96B2DDC7622BD8A9BCECF8BB9A8FA3AA03DC81F0294EA2E480FD92CF5D55BC5BBBDA3C2701580D10ECAC7945FE6E99D655EE00C2DA86AB9684D9F9D64594CAA2B61ED61130735F4DF6338D8CB5B939F07FCC31D90BE4D32A7C15A5FA83F37C0BF76A466415A3BA7AB92A3218BE0DE23DD7C2104B4DEB794C0589B6F1B488B31D12D2399772047AAE56892DD8687661D843C00E1DE94710A7B8C0674A409557D1700AA50A64091311841A76058E0FD8B5C98C82AA5F4D242077FA7C3259FE9492DA8E3131C74E22AB193CD24585F6AD05C191DD01432ABAF21DA1481BF05F5E27E574B97CDF584104820CECAB6AE4632A7832EC9E2086925371B278B65831710CF46483A5E06656FB68C81CA7DB122C22D7853C5BFE107BC6B7FE19DB7EBEB3BDBB3CC8717ED8FC8EFD348E8A787AA088F314DBE8B6046F7754C8D3744638C30E62C442979F93680F63DCBE7D578213D21472F37F0FB23085A4D81E4744913AA013B327E558399E8C655F81E3E6C36A746B102959AD91B60BE5D0199E8CD512FE4366C35AB7658D1456C937762710C247ECC10C660D56E3309504CFD3B3A6791A31F38CBDE0F43F44F4E6B84D77DC80E060EC2AACA903574BACA91F0161171B47C46A4E4D27D6808B785F642424C791B11AD150F260C1B0DABF9207F267761C19AB1D2DA55833AE600CDFC284D18A8E8AD5888650AC0DC4634153129674DE4340C723CC32C8F88E8B27C337614EC97D0F897C07897AFF889B7F1994556336882EE2C7EA240A5E5D3A39A525D81ADCCD7B5BBC8094DA0F24424A9326B442E719E82719986718F8B74646071FB83647883568DB23844ADC1DD49BF1163C1619B82D574BB47E22D153BA0B5F45A28528962F70CB16222557AB6A62FEAB8517C7E0002E618CDB129E93509AD051F18B2647745CB72684AF4B34152E9343D94ECA85CA8E8022B5A1E1165AFE6F5F54F1DC4D14505C724A4769C29894BB25BBE2780CB031C5948022BBA611B9C2CA8A470974B46BAD8251685B0D6DA1A474971FD2D05AD091F14B27C451E3629A4CBEB8E90A6419FEFC765782DDEAA90BF9EEC27D8EB3E2012DE71EC0FE126207329E8C74576E4AC9B189D2D213E44FCAB19B2A43128E888C756992E7B2A4E825C9C96DCFFABA18537647C76E42432ADA12F281682C19BB1DDCE78F47B518FB5134627693E43EB30DE3175AA6002A39EF2756812D1EEC8D211636F354E2BE9E2488DAFD6369B366F545D89B284D29BF4DDA1ABB53D9D3D4E8764649D37E42CCAF32B91D5CF2A5E54E1E34254A9FD0D1DA3022E5EF91E6D133623F34E534EB57241CD3334112450227E7D9EB9F647BB614544BD644FC72FB27DE88827B129AE4968AEB9644976919B357D11712EE4634E5FC212D632DC5B1826A491802EBAC843339F5CF38F6A8E44F8EDE1A6729C40AE88B49827A0A0E69C4534CC34292A4B69C434E9B89152BA72D24C969CB45AE9D4EB3F5CD83421A35F34AEAB8025FA44CD835EC8B4811715DCAD21EC4F9876300239CAE7D1956B3B698E3E0343695D54C209E0C7B881A43C911F1E39276CDD5C651610D3027E4EFD237943E7D43EED40CBFF73BC856354FF1501D391BBDA4F06A40DF9D2323118F24551A611F5EE8149DFCFE9A5C1F9B36AAE230299968FB7AAC2E872958095CEEF1043843C9B2229B819E53A7B2078984625CDEE43814FE4362E52EC0272AB99F265A991B9CAF22D910F85C2F95DAD3228A39E9C95B30DC7A127DDE8BC973827FD895E6B7DC4C387C0BB3A13FF6A711018FB75272C510392F65EA5E8EA079BB8A6C2CC4C2E02A4C37D3B6016FF25C33A1D43E51C81B581FBED17C8655C584B738816323011C53005F45D353003D83D9788851B96AC2A53A7B91D2044063611BFEC7999F887C5BB2254C4C4C8725656D1237B6991889BC46B622D2AEB13B5E06CFA44AF93B9181756F27F5DF9CC89C69E7B204CC3AAB4CC1639AE22C859D3225D1B9C849F03071803093254D6B006C7418BADEDEE230262E63FD40CDE1FE5AA50B6EDF4C9F891586EBC8B8528E26C949D047D89EB122BFC39BACCB10CA6B63C29B7F03B527E9482BF5BADF44CD25DE0512A6E1F022AC1772D5232B4C5654D1C71631DA2CA1E2FD30F3230ECE28F4348C9AE69A6CD0A9FF998A7084049213265DA901B3F4B922798CD25193959AE6A5AC74E97F74D41C83DC8ADC91AF40ED25C35E6AD6CE8929471486CCC93BF00875CC0F3E27CCD465F4BC2716739B8F8797A06166E11B854AD4E8B38CA974010DA9E96EE003467A55B328E9A8093538F03AFC7669B0A812E3F2C3E98C7C01241D25FC9DDAA529543649A319657130A1D0BF1AC028AA4DBD5917B69C397ABB21D5D070AB3D4C4BBDCC3F00A85517180CC40CD053B36108D507C95C7BDA8021531B183C74C3183107D52D7012053D0BC3C2A0019CF4B8798E70EEA18AAB607E804EF3A24FB810339D6B310D5F783927371B52DA30C974A542B7C88CDABC9FD0026BFD91B4C8EA63D1E5C6C26640B9FCF8BD624ABD80570CB3344ECD5197299BA3568B326F8F09F44FD5732575A936EDBB862FBB936B42E5265BECD59E63DB73CDDB4734E5C7A4CC9E1E3DB634ECEEA680CB189C0344C204E8E9049A5F8FCAF5BBF5F0458DE66364A2E3ABC3ECF5099A8E6462FD0A135FD8A838604A759AA27F0AE41E23896A186A5563EA9278CD9F4999F21B52183261EDAABC56ABA815078223E6689F5BA1C64673221341D1F4C998CE74F58FFA542575F6984067FF9A566D30A391949B92E8546F89C96D38D3D226002C9D7EEC978A08F41D3BE638C341A861EE1087D5A3C584A79DEF499279EDC666B5806138B8B2EE461189CD7741F3D724AD1F0C97B0FC9803D934C387CA2BF5EB1F56684CF67357C2478965592E79CA82BFAD58393CD56C7411E99D032D9D45602E6B6401C36EBE2BDB77D90C74242F6BD7BB71FA661E5727F6950C7761FFC09BF62EE467ED7617CEDF0CE49037ACB45C17F23E00A8D0959C22C8C6167BDD70575F7DE4ABB2D92E6EDE30D43E4439F9BA3D3E276F4A7208EB6A2CD46DD3872365BB69C2876C30FC53969565A6451B347BF70EA0F6F1C2CDD9ED11337BC39243DCA0CE62DDC7FB46A87C37724A109DCD484F655266B379958D762FE3ED4EED6356519EDB2399F38D5A8E46303959760BA95AB20061B231A25822F6182F0561781979671C3C64AB1D8EF859BB8D3CF3C79139E40D2B2DD785BC0F1D2B7425A708D1B14F7AC59932CAE755B6DBC5D5C12CED039487ABE36373769A8C8BDE507A35B691E592DB09F2B5F19546F0D16DA230B7BA8DE33173F53E640B1106D3F90BED341C1D526FA6ABF91E30671B1F9BF9411F7385AEC52511E4AB42CD2E68D4A10CCD8C4AE3744B63D3CD59122F85E7E012A3A6B9034CDA831AFDE6D315069218897A36873B6FC4F4988360DA3E2573B3763B30671C9B62575AAE0BB9CFA40972327136CD75635F15190CDF06F15EE9789A00171B6B1D6CF3B0AC09940B99BB2B95B775CF42C02C7453ACD5C43CE1CA9CD85C8422696E095374BC3E823805F754B722D29215C22BC2E72F637178064D91113334BCF9EC5013339421288255C2BE29AEC0F101A49C2E312336EC13B53C3C87B64C9F29C68FFA719A8458C9B069F0CF1D8E394D69D42F5B8F95653CE443A1262BA8FA920FC52EDACD417282A9207EF799D434A0A643DED47306C1510887B0154C0FB84E26C13C83727DA6E9F5238D3212298F4AD3413629E1310B9F49652E7882083D4CBDAF78B6F7DEA8EEC1A861C63F7042C7F72DB10406A25BB1C58271C3588A72DB7EE75A42316A2CB362227A8E698FC1EC5489F9109581A5E5266327054B67D4B4DC9B53ECCA26F6A3EC1AEE5D29E6FE6CBF4F4196115EA4185390154284E30BEBD50F3413D45CC7EAB6BF29AB86DEE20574DDA624E4A65694A38636BF5856EF5DF35E3B49B7A65CAF625C3D2D9577A188F394E58F33224A436BDA491A85E6376B3DD76A50199AAA6343A15F41CE5E97506EF459ACB3D95C452C9D961E19D5237EB6D1FCB5EBE724DAC3F8F01E64610AAB8FCDF7CD4F38E529D464553A7E032DBADF2826980BC3B118159B30072D230E8D9CAC987C6E1C8B26B91F3A1E7664E009F5FBC5C243E3FEF3695F420C36029B92E88CB304CCA3087767E8BC79152D357F61E1DEBC0205003B9E4304EC7EE4EC672C173C8119D3B03EEDB32B697668E74CC58B99CC3A26809320946823BD104A5259C4A988F596F22BBB66E37A8C985E81ACA6FC53C4388944EB687C8618ABA9883BE12B2DE54BB64C755BB24C6158119C40081F198F7471D7358D5918E144DB61698D9B51C4F998F597F243B7CC3AF9A42762515AD5A58C49F9144823336942F6F754FECAA63FA9BA664ADABA074BA773F1B3D41A6F7CF28DBCEB83A5D3BFEB433CF2A6F984DB489D268F2153ED964E673FDB50B83969D73D25563931FB3D4A11069A152672C0B584C8154F6CDEB494C393DC75C9E6903D4BE9B219EF3F823A8710634436647C4AB63C096AB5C58E0DD672F9B62FB2EAB300C3181D219F023D5F82223D816326F957F270D73CD4C38F59F84A4BE1542B9DC8A92730632AFA933FB40A2656762E98E60AC6F02D4C849C085B67291F6A84131975E546CCC4EB40337AD3FE63C72CF7A8214C48AE88F8D4A8F91174A80B1D8361DAFA084BB7F6B8F99E740E694EB44E558F47986550EC8220472DBDC6988B9B180547A06C9CE63061B986AD82DF20BA881FABB72D21E36B0F5F45132049938CBB204020346B3ACA43145CF598EAE3345D93D9DE162F201D0CADEADFEDD142D60EA9300FCA8E0E5624E6B823F7D629BE713813CF488C1AB7FF3709F3046AEBC43E9B066B7B77F4BC99C23567213E6CAFD47FD199D0402C7322AD71B30F7E97B1F6B03AD924232103F547BFAFC6A403CEF7A4E132B72447AD0506AE6AB7289A8BE37540A36F022EA426655D8323D309F1CB284B98BFD8B118A986B128CC6670D0F3BC058F45066E4190F53A932E15F257662A2F7D9397DA061C4712A5614F238915F53F029FC50DE49A0733CD85EE0E14D8EFBD821C044CDDCAA419B9A55997794BC34580B851C2AEB495B542C3F8E2181CC0258CBF724C2C735A63534A270AC76050A8CD0C3C073789B46475E4CF6C4E8461EDA0EFF8E154350E6798929AF385454D70991C4AD7A27F8B23911ADC206C64E16CD015E93601DB09A694C67C6049F5CBFFED8BEA04E54D1470ED1B936A18F487B1489C5DA61486CCC37612420563BE62CF34BBE2780CD2171E9799911AF4954616CE125D916E13B0DD624A69CC1F96541F9F138A6205420563C6A09CD9255018320DFBD033573DB2C27ACE3BDB371BCAAE937246E81862739ED4C9C29A6350AACD14CC4352784283D03A3F14352ED0A5FAFD902D6D2F604268AEF797569CBE93B7EC9E1D8FD11454AE771B043F1FD36A59F8606CC844D8832C145CA4D2EB1F1CB60EBE5C812C2B118B76887D4A425642F6AC7A236154AFFB4D9B8AB72084A7728EC576F79C48E776DD320AEE88A7EA2714EB510D89AF8E5966F7BBA448F1E1DE9C48A7828800FDEDCDAC5AF3AB4E25EB5890A1644344FB0A3B996199074C175212FD516DBC3D0621A08563146A1331199757482AFD39CE8A079436E001EC2F61564EB7ED3FD1BF48CA336A915599701FE83429619863DA809979E60466CC45FAE04226D63DFA6D98A23BD814A47992C4B34F68FCD9133939E8341A4B34FE081791583DF61BB16EEE79DE8F7FC5C6803CF52831DD44C0309A9B14D122435C2328CC3A0ACD669BE87D5E9D85655B0D5F8D57DD560A51DB9680DB8002DDA06C32DE9C7B1CB528834DF62E365128C544BAAFA79315171D9DB4CACB0E513BA6C4C3288719F92A9A3621BE151C138539D3713C8A43A8A17F85EC9E79F0C7BA25BD8D87D902C6E1E0CA3E894E215FB42B789F631664C56130FD2F30BBD80503B65C0926F92A528C219D68922699D9411AB329D22DA0F08C389D89158F75D0D493CFA12A06E76165DBEC940FAF3CE44B7401FB4BB63813D39FB5D7626A66E2077625DAD71AD5CC0F5AC6802E53F144BA387A8371AE25B35C260798E530DC9D9238E3B30CA98A39E34C2462ED33A33166A299247E534DAB52B6F6292A335475C8842DC732DC0B93E331D957E5447973438A3158C40CD86EC2F68F5603C2F431488194E9E855B76DB45392E6776910678FFD810F41E3F1B0D8B21139C3667A852583E3C54C537D7900A4EF5138329D1F9D4CABD9DAFD260A42EA79193CA1B970A092837591A644FD7859A509E503C69840FFB78AB98ABA541B7756C395D5A7359972732DF5663B264631C4FD440A79FCD2AA69570F3BC2472DC00EF709852193F5E0C26BADAE86261873CD3C7568C5679829AD5193D4C2F0C668CBB499A18D8EEECFD3E4C83DAE28B59619566D03B056EA0BF59B89C75DE6C426FDC59A29EE12717F99D6D996B7E08E912B7DF012636463179BD442E6F17A439B624C8BB12E1AC831123394DADD8335989E67CD81A537B7F450EA005D6691F1BEE55E9071C954E89692D2A02533B03D586717B3C844E64C3B972560D659653EAF9C29CE52D82953CEAC700BA220077BC6DD3D9E6A94E5B9E25D3E25F3CBF85B7BBC8161152C9D3933081D4891BAB117C7A0CAB540C9CC3D26D1B99FD8F39E7CE1ED7F5656F19702FE499BD647E5FAA771C47E54A9FE418B5A6771F67BB938B93ED1741B13D11B5AD3CE9ADBFECC50F37AFEB0E7F0679D2A939C754AA2D359850D24A9626DAFFB5F0A90D1FA754A466F78CF6DD2F4BEC081DE6D1B735F0F12BCE26322C591A76E280955774104B29B723645C9CEBF8087A724F97A0B7E4302EE47653813F057262B3A1132BCC0362EA1188BD20E32C7299D96EB6CA83DC4C744C6046475E4EF7362AF73EA548D763BAF27D00975A655FB029A29FEA6788860582F1777882EBA027B18D0FC9FBB2EC5757192862E8B2BA7980BD7A411433C01B7197F7A5DF32A99E4018C41DA95FDF47A173E8163D0FC50FE334FD2E000AE923D88B2EAD79F5EDF1665ED23A8FFF51E64F0D0B3F8A98FB57AA62D0D7A8DA18C544F20ADF418B6A825698BBB6BF279B00FF2E02CCDE16350E5F20A4196C1F8F0DDAB5F83A84081D4F101EC2FE2EB223F1579A932383E44A350F6A7D774F93FBD9EB5F9A77A16CA74A8503613A2ACD4D7F1DB0246FBAEDDE741944D7C9FC4E25D69FD8FA0FCBDEECBBCFC3F38BC749C3E25D3847824468DF9DE83135A1CC6F91D389ED0222CBB8E77C13390695B398A2FC121085FCADF9F6195CB80C484DD1163B3FFF41E06873438660D8FBE7EF9CFD287F7C73FFEE7FF07C6E361CA7F3D1000 , N'6.5.1')

