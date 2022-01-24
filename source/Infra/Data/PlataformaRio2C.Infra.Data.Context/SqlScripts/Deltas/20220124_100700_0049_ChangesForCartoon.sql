--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done
--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION
		---------------------------------------------------------------------
		-- CartoonProjects
		---------------------------------------------------------------------
		CREATE TABLE [dbo].[CartoonProjects](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Uid] [uniqueidentifier] NOT NULL,
			[SellerAttendeeOrganizationId] [int] NULL,
			[NumberOfEpisodes] [int] NULL,
			[ValuePerEpisode] [varchar](50) NULL,
			[TotalValueOfProject] [varchar](50) NULL,
			[ValueAlreadyRaised] [varchar](50) NULL,
			[ValueStillNeeded] [varchar](50) NULL,
			[IsDeleted] [bit] NOT NULL,
			[CreateDate] [datetimeoffset](7) NULL,
			[CreateUserId] [int] NOT NULL,
			[UpdateDate] [datetimeoffset](7) NULL,
			[UpdateUserId] [int] NOT NULL,
			[EachEpisodePlayingTime] [varchar](10) NULL,
			[TotalPlayingTime] [varchar](10) NOT NULL,
			[IsPitching] [bit] NOT NULL,
			[FinishDate] [datetimeoffset](7) NULL,
			[ProjectBuyerEvaluationsCount] [int] NOT NULL,
			[CommissionEvaluationsCount] [int] NOT NULL,
			[CommissionGrade] [decimal](4, 2) NULL,
			[LastCommissionEvaluationDate] [datetimeoffset](7) NULL,
		 CONSTRAINT [PK_CartoonProjects] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_CartoonProjects_Uid] UNIQUE NONCLUSTERED 
		(
			[Uid] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
		) ON [PRIMARY]

		ALTER TABLE [dbo].[CartoonProjects] ADD  DEFAULT ((0)) FOR [CommissionEvaluationsCount]

		ALTER TABLE [dbo].[CartoonProjects]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeOrganizations_CartoonProjects_SellerAttendeeOrganizationId] FOREIGN KEY([SellerAttendeeOrganizationId])
		REFERENCES [dbo].[AttendeeOrganizations] ([Id])

		ALTER TABLE [dbo].[CartoonProjects] CHECK CONSTRAINT [FK_AttendeeOrganizations_CartoonProjects_SellerAttendeeOrganizationId]

		ALTER TABLE [dbo].[CartoonProjects]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjects_CreateUserId] FOREIGN KEY([CreateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjects] CHECK CONSTRAINT [FK_Users_CartoonProjects_CreateUserId]

		ALTER TABLE [dbo].[CartoonProjects]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjects_UpdateUserId] FOREIGN KEY([UpdateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjects] CHECK CONSTRAINT [FK_Users_CartoonProjects_UpdateUserId]


		---------------------------------------------------------------------
		-- CartoonProjectTitles
		---------------------------------------------------------------------
		CREATE TABLE [dbo].[CartoonProjectTitles](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Uid] [uniqueidentifier] NOT NULL,
			[CartoonProjectId] [int] NOT NULL,
			[LanguageId] [int] NOT NULL,
			[Value] [varchar](256) NULL,
			[IsDeleted] [bit] NOT NULL,
			[CreateDate] [datetimeoffset](7) NULL,
			[CreateUserId] [int] NOT NULL,
			[UpdateDate] [datetimeoffset](7) NULL,
			[UpdateUserId] [int] NOT NULL,
		 CONSTRAINT [PK_CartoonProjectTitles] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_CartoonProjectTitles_CartoonProjectId_LanguageId] UNIQUE NONCLUSTERED 
		(
			[CartoonProjectId] ASC,
			[LanguageId] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_CartoonProjectTitles_Uid] UNIQUE NONCLUSTERED 
		(
			[Uid] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
		) ON [PRIMARY]

		ALTER TABLE [dbo].[CartoonProjectTitles]  WITH CHECK ADD  CONSTRAINT [FK_Languages_CartoonProjectTitles_LanguageId] FOREIGN KEY([LanguageId])
		REFERENCES [dbo].[Languages] ([Id])

		ALTER TABLE [dbo].[CartoonProjectTitles] CHECK CONSTRAINT [FK_Languages_CartoonProjectTitles_LanguageId]

		ALTER TABLE [dbo].[CartoonProjectTitles]  WITH CHECK ADD  CONSTRAINT [FK_CartoonProjects_CartoonProjectTitles_CartoonProjectId] FOREIGN KEY([CartoonProjectId])
		REFERENCES [dbo].[CartoonProjects] ([Id])

		ALTER TABLE [dbo].[CartoonProjectTitles] CHECK CONSTRAINT [FK_CartoonProjects_CartoonProjectTitles_CartoonProjectId]

		ALTER TABLE [dbo].[CartoonProjectTitles]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectTitles_CreateUserId] FOREIGN KEY([CreateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectTitles] CHECK CONSTRAINT [FK_Users_CartoonProjectTitles_CreateUserId]

		ALTER TABLE [dbo].[CartoonProjectTitles]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectTitles_UpdateUserId] FOREIGN KEY([UpdateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectTitles] CHECK CONSTRAINT [FK_Users_CartoonProjectTitles_UpdateUserId]


		---------------------------------------------------------------------
		-- CartoonProjectLogLines
		---------------------------------------------------------------------
		CREATE TABLE [dbo].[CartoonProjectLogLines](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Uid] [uniqueidentifier] NOT NULL,
			[CartoonProjectId] [int] NOT NULL,
			[LanguageId] [int] NOT NULL,
			[Value] [varchar](8000) NULL,
			[IsDeleted] [bit] NOT NULL,
			[CreateDate] [datetimeoffset](7) NULL,
			[CreateUserId] [int] NOT NULL,
			[UpdateDate] [datetimeoffset](7) NULL,
			[UpdateUserId] [int] NOT NULL,
		 CONSTRAINT [PK_CartoonProjectLogLines] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_CartoonProjectLogLines_CartoonProjectId_LanguageId] UNIQUE NONCLUSTERED 
		(
			[CartoonProjectId] ASC,
			[LanguageId] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_CartoonProjectLogLines_Uid] UNIQUE NONCLUSTERED 
		(
			[Uid] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
		) ON [PRIMARY]

		ALTER TABLE [dbo].[CartoonProjectLogLines]  WITH CHECK ADD  CONSTRAINT [FK_Languages_CartoonProjectLogLines_LanguageId] FOREIGN KEY([LanguageId])
		REFERENCES [dbo].[Languages] ([Id])

		ALTER TABLE [dbo].[CartoonProjectLogLines] CHECK CONSTRAINT [FK_Languages_CartoonProjectLogLines_LanguageId]

		ALTER TABLE [dbo].[CartoonProjectLogLines]  WITH CHECK ADD  CONSTRAINT [FK_CartoonProjects_CartoonProjectLogLines_CartoonProjectId] FOREIGN KEY([CartoonProjectId])
		REFERENCES [dbo].[CartoonProjects] ([Id])

		ALTER TABLE [dbo].[CartoonProjectLogLines] CHECK CONSTRAINT [FK_CartoonProjects_CartoonProjectLogLines_CartoonProjectId]

		ALTER TABLE [dbo].[CartoonProjectLogLines]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectLogLines_CreateUserId] FOREIGN KEY([CreateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectLogLines] CHECK CONSTRAINT [FK_Users_CartoonProjectLogLines_CreateUserId]

		ALTER TABLE [dbo].[CartoonProjectLogLines]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectLogLines_UpdateUserId] FOREIGN KEY([UpdateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectLogLines] CHECK CONSTRAINT [FK_Users_CartoonProjectLogLines_UpdateUserId]


		---------------------------------------------------------------------
		-- CartoonProjectSummaries
		---------------------------------------------------------------------
		CREATE TABLE [dbo].[CartoonProjectSummaries](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Uid] [uniqueidentifier] NOT NULL,
			[CartoonProjectId] [int] NOT NULL,
			[LanguageId] [int] NOT NULL,
			[Value] [nvarchar](max) NULL,
			[IsDeleted] [bit] NOT NULL,
			[CreateDate] [datetimeoffset](7) NULL,
			[CreateUserId] [int] NOT NULL,
			[UpdateDate] [datetimeoffset](7) NULL,
			[UpdateUserId] [int] NOT NULL,
		 CONSTRAINT [PK_CartoonProjectSummaries] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_CartoonProjectSummaries_CartoonProjectId_LanguageId] UNIQUE NONCLUSTERED 
		(
			[CartoonProjectId] ASC,
			[LanguageId] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_CartoonProjectSummaries_Uid] UNIQUE NONCLUSTERED 
		(
			[Uid] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
		) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

		ALTER TABLE [dbo].[CartoonProjectSummaries]  WITH CHECK ADD  CONSTRAINT [FK_Languages_CartoonProjectSummaries_LanguageId] FOREIGN KEY([LanguageId])
		REFERENCES [dbo].[Languages] ([Id])

		ALTER TABLE [dbo].[CartoonProjectSummaries] CHECK CONSTRAINT [FK_Languages_CartoonProjectSummaries_LanguageId]

		ALTER TABLE [dbo].[CartoonProjectSummaries]  WITH CHECK ADD  CONSTRAINT [FK_CartoonProjects_CartoonProjectSummaries_CartoonProjectId] FOREIGN KEY([CartoonProjectId])
		REFERENCES [dbo].[CartoonProjects] ([Id])

		ALTER TABLE [dbo].[CartoonProjectSummaries] CHECK CONSTRAINT [FK_CartoonProjects_CartoonProjectSummaries_CartoonProjectId]

		ALTER TABLE [dbo].[CartoonProjectSummaries]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectSummaries_CreateUserId] FOREIGN KEY([CreateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectSummaries] CHECK CONSTRAINT [FK_Users_CartoonProjectSummaries_CreateUserId]

		ALTER TABLE [dbo].[CartoonProjectSummaries]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectSummaries_UpdateUserId] FOREIGN KEY([UpdateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectSummaries] CHECK CONSTRAINT [FK_Users_CartoonProjectSummaries_UpdateUserId]


		---------------------------------------------------------------------
		-- CartoonProjectMotivations
		---------------------------------------------------------------------
		CREATE TABLE [dbo].[CartoonProjectMotivations](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Uid] [uniqueidentifier] NOT NULL,
			[CartoonProjectId] [int] NOT NULL,
			[LanguageId] [int] NOT NULL,
			[Value] [nvarchar](max) NULL,
			[IsDeleted] [bit] NOT NULL,
			[CreateDate] [datetimeoffset](7) NULL,
			[CreateUserId] [int] NOT NULL,
			[UpdateDate] [datetimeoffset](7) NULL,
			[UpdateUserId] [int] NOT NULL,
		 CONSTRAINT [PK_CartoonProjectMotivations] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_CartoonProjectMotivations_CartoonProjectId_LanguageId] UNIQUE NONCLUSTERED 
		(
			[CartoonProjectId] ASC,
			[LanguageId] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_CartoonProjectMotivations_Uid] UNIQUE NONCLUSTERED 
		(
			[Uid] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
		) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

		ALTER TABLE [dbo].[CartoonProjectMotivations]  WITH CHECK ADD  CONSTRAINT [FK_Languages_CartoonProjectMotivations_LanguageId] FOREIGN KEY([LanguageId])
		REFERENCES [dbo].[Languages] ([Id])

		ALTER TABLE [dbo].[CartoonProjectMotivations] CHECK CONSTRAINT [FK_Languages_CartoonProjectMotivations_LanguageId]

		ALTER TABLE [dbo].[CartoonProjectMotivations]  WITH CHECK ADD  CONSTRAINT [FK_CartoonProjects_CartoonProjectMotivations_CartoonProjectId] FOREIGN KEY([CartoonProjectId])
		REFERENCES [dbo].[CartoonProjects] ([Id])

		ALTER TABLE [dbo].[CartoonProjectMotivations] CHECK CONSTRAINT [FK_CartoonProjects_CartoonProjectMotivations_CartoonProjectId]

		ALTER TABLE [dbo].[CartoonProjectMotivations]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectMotivations_CreateUserId] FOREIGN KEY([CreateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectMotivations] CHECK CONSTRAINT [FK_Users_CartoonProjectMotivations_CreateUserId]

		ALTER TABLE [dbo].[CartoonProjectMotivations]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectMotivations_UpdateUserId] FOREIGN KEY([UpdateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectMotivations] CHECK CONSTRAINT [FK_Users_CartoonProjectMotivations_UpdateUserId]


		---------------------------------------------------------------------
		-- CartoonProjectFormats
		---------------------------------------------------------------------
		CREATE TABLE [dbo].[CartoonProjectFormats](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Uid] [uniqueidentifier] NOT NULL,
			[Name] [varchar](500) NOT NULL,
			[DisplayOrder] [int] NOT NULL,
			[HasAdditionalInfo] [bit] NOT NULL,
			[IsDeleted] [bit] NOT NULL,
			[CreateDate] [datetimeoffset](7) NOT NULL,
			[CreateUserId] [int] NOT NULL,
			[UpdateDate] [datetimeoffset](7) NOT NULL,
			[UpdateUserId] [int] NOT NULL,
			[Description] [varchar](1000) NOT NULL,
		 CONSTRAINT [PK_CartoonProjectFormats] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_CartoonProjectFormats_Uid] UNIQUE NONCLUSTERED 
		(
			[Uid] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
		) ON [PRIMARY]

		ALTER TABLE [dbo].[CartoonProjectFormats]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectFormats_CreateUserId] FOREIGN KEY([CreateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectFormats] CHECK CONSTRAINT [FK_Users_CartoonProjectFormats_CreateUserId]

		ALTER TABLE [dbo].[CartoonProjectFormats]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectFormats_UpdateUserId] FOREIGN KEY([UpdateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectFormats] CHECK CONSTRAINT [FK_Users_CartoonProjectFormats_UpdateUserId]


		---------------------------------------------------------------------
		-- AttendeeCartoonProjectFormats
		---------------------------------------------------------------------
		CREATE TABLE [dbo].[AttendeeCartoonProjectFormats](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Uid] [uniqueidentifier] NOT NULL,
			[CartoonProjectId] [int] NOT NULL,
			[CartoonProjectFormatId] [int] NOT NULL,
			[AdditionalInfo] [varchar](200) NULL,
			[IsDeleted] [bit] NOT NULL,
			[CreateDate] [datetimeoffset](7) NOT NULL,
			[CreateUserId] [int] NOT NULL,
			[UpdateDate] [datetimeoffset](7) NOT NULL,
			[UpdateUserId] [int] NOT NULL,
		 CONSTRAINT [PK_AttendeeCartoonProjectFormats] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_AttendeeCartoonProjectFormats_Uid] UNIQUE NONCLUSTERED 
		(
			[Uid] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
		) ON [PRIMARY]

		ALTER TABLE [dbo].[AttendeeCartoonProjectFormats]  WITH CHECK ADD  CONSTRAINT [FK_CartoonProjects_AttendeeCartoonProjectFormats_CartoonProjectId] FOREIGN KEY([CartoonProjectId])
		REFERENCES [dbo].[CartoonProjects] ([Id])

		ALTER TABLE [dbo].[AttendeeCartoonProjectFormats] CHECK CONSTRAINT [FK_CartoonProjects_AttendeeCartoonProjectFormats_CartoonProjectId]

		ALTER TABLE [dbo].[AttendeeCartoonProjectFormats]  WITH CHECK ADD  CONSTRAINT [FK_CartoonProjectFormats_AttendeeCartoonProjectFormats_CartoonProjectFormatId] FOREIGN KEY([CartoonProjectFormatId])
		REFERENCES [dbo].[CartoonProjectFormats] ([Id])

		ALTER TABLE [dbo].[AttendeeCartoonProjectFormats] CHECK CONSTRAINT [FK_CartoonProjectFormats_AttendeeCartoonProjectFormats_CartoonProjectFormatId]

		ALTER TABLE [dbo].[AttendeeCartoonProjectFormats]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeCartoonProjectFormats_CreateUserId] FOREIGN KEY([CreateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[AttendeeCartoonProjectFormats] CHECK CONSTRAINT [FK_Users_AttendeeCartoonProjectFormats_CreateUserId]

		ALTER TABLE [dbo].[AttendeeCartoonProjectFormats]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeCartoonProjectFormats_UpdateUserId] FOREIGN KEY([UpdateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[AttendeeCartoonProjectFormats] CHECK CONSTRAINT [FK_Users_AttendeeCartoonProjectFormats_UpdateUserId]


		---------------------------------------------------------------------
		-- CartoonProjectProductionPlans
		---------------------------------------------------------------------
		CREATE TABLE [dbo].[CartoonProjectProductionPlans](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Uid] [uniqueidentifier] NOT NULL,
			[CartoonProjectId] [int] NOT NULL,
			[LanguageId] [int] NOT NULL,
			[Value] [varchar](3000) NULL,
			[IsDeleted] [bit] NOT NULL,
			[CreateDate] [datetimeoffset](7) NULL,
			[CreateUserId] [int] NOT NULL,
			[UpdateDate] [datetimeoffset](7) NULL,
			[UpdateUserId] [int] NOT NULL,
		 CONSTRAINT [PK_CartoonProjectProductionPlans] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_CartoonProjectProductionPlans_CartoonProjectId_LanguageId] UNIQUE NONCLUSTERED 
		(
			[CartoonProjectId] ASC,
			[LanguageId] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_CartoonProjectProductionPlans_Uid] UNIQUE NONCLUSTERED 
		(
			[Uid] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
		) ON [PRIMARY]

		ALTER TABLE [dbo].[CartoonProjectProductionPlans]  WITH CHECK ADD  CONSTRAINT [FK_Languages_CartoonProjectProductionPlans_LanguageId] FOREIGN KEY([LanguageId])
		REFERENCES [dbo].[Languages] ([Id])

		ALTER TABLE [dbo].[CartoonProjectProductionPlans] CHECK CONSTRAINT [FK_Languages_CartoonProjectProductionPlans_LanguageId]

		ALTER TABLE [dbo].[CartoonProjectProductionPlans]  WITH CHECK ADD  CONSTRAINT [FK_CartoonProjects_CartoonProjectProductionPlans_CartoonProjectId] FOREIGN KEY([CartoonProjectId])
		REFERENCES [dbo].[CartoonProjects] ([Id])

		ALTER TABLE [dbo].[CartoonProjectProductionPlans] CHECK CONSTRAINT [FK_CartoonProjects_CartoonProjectProductionPlans_CartoonProjectId]

		ALTER TABLE [dbo].[CartoonProjectProductionPlans]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectProductionPlans_CreateUserId] FOREIGN KEY([CreateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectProductionPlans] CHECK CONSTRAINT [FK_Users_CartoonProjectProductionPlans_CreateUserId]

		ALTER TABLE [dbo].[CartoonProjectProductionPlans]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectProductionPlans_UpdateUserId] FOREIGN KEY([UpdateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectProductionPlans] CHECK CONSTRAINT [FK_Users_CartoonProjectProductionPlans_UpdateUserId]


		---------------------------------------------------------------------
		-- CartoonProjectTeaserLinks
		---------------------------------------------------------------------
		CREATE TABLE [dbo].[CartoonProjectTeaserLinks](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Uid] [uniqueidentifier] NOT NULL,
			[CartoonProjectId] [int] NOT NULL,
			[Value] [varchar](3000) NULL,
			[IsDeleted] [bit] NOT NULL,
			[CreateDate] [datetimeoffset](7) NULL,
			[CreateUserId] [int] NOT NULL,
			[UpdateDate] [datetimeoffset](7) NULL,
			[UpdateUserId] [int] NOT NULL,
		 CONSTRAINT [PK_CartoonProjectTeaserLinks] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_CartoonProjectTeaserLinks_Uid] UNIQUE NONCLUSTERED 
		(
			[Uid] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
		) ON [PRIMARY]

		ALTER TABLE [dbo].[CartoonProjectTeaserLinks]  WITH CHECK ADD  CONSTRAINT [FK_CartoonProjects_CartoonProjectTeaserLinks_CartoonProjectId] FOREIGN KEY([CartoonProjectId])
		REFERENCES [dbo].[CartoonProjects] ([Id])

		ALTER TABLE [dbo].[CartoonProjectTeaserLinks] CHECK CONSTRAINT [FK_CartoonProjects_CartoonProjectTeaserLinks_CartoonProjectId]

		ALTER TABLE [dbo].[CartoonProjectTeaserLinks]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectTeaserLinks_CreateUserId] FOREIGN KEY([CreateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectTeaserLinks] CHECK CONSTRAINT [FK_Users_CartoonProjectTeaserLinks_CreateUserId]

		ALTER TABLE [dbo].[CartoonProjectTeaserLinks]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectTeaserLinks_UpdateUserId] FOREIGN KEY([UpdateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectTeaserLinks] CHECK CONSTRAINT [FK_Users_CartoonProjectTeaserLinks_UpdateUserId]


		---------------------------------------------------------------------
		-- CartoonProjectBibleLinks
		---------------------------------------------------------------------
		CREATE TABLE [dbo].[CartoonProjectBibleLinks](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Uid] [uniqueidentifier] NOT NULL,
			[CartoonProjectId] [int] NOT NULL,
			[Value] [varchar](3000) NULL,
			[IsDeleted] [bit] NOT NULL,
			[CreateDate] [datetimeoffset](7) NULL,
			[CreateUserId] [int] NOT NULL,
			[UpdateDate] [datetimeoffset](7) NULL,
			[UpdateUserId] [int] NOT NULL,
		 CONSTRAINT [PK_CartoonProjectBibleLinks] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_CartoonProjectBibleLinks_Uid] UNIQUE NONCLUSTERED 
		(
			[Uid] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
		) ON [PRIMARY]

		ALTER TABLE [dbo].[CartoonProjectBibleLinks]  WITH CHECK ADD  CONSTRAINT [FK_CartoonProjects_CartoonProjectBibleLinks_CartoonProjectId] FOREIGN KEY([CartoonProjectId])
		REFERENCES [dbo].[CartoonProjects] ([Id])

		ALTER TABLE [dbo].[CartoonProjectBibleLinks] CHECK CONSTRAINT [FK_CartoonProjects_CartoonProjectBibleLinks_CartoonProjectId]

		ALTER TABLE [dbo].[CartoonProjectBibleLinks]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectBibleLinks_CreateUserId] FOREIGN KEY([CreateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectBibleLinks] CHECK CONSTRAINT [FK_Users_CartoonProjectBibleLinks_CreateUserId]

		ALTER TABLE [dbo].[CartoonProjectBibleLinks]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectBibleLinks_UpdateUserId] FOREIGN KEY([UpdateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectBibleLinks] CHECK CONSTRAINT [FK_Users_CartoonProjectBibleLinks_UpdateUserId]
	-- Commands End
	COMMIT TRAN -- Transaction Success!
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN --RollBack in case of Error

	-- Raise ERROR with RAISEERROR() Statement including the details of the exception
	DECLARE @ErrorLine INT;
	DECLARE @ErrorMessage NVARCHAR(4000);
	DECLARE @ErrorSeverity INT;
	DECLARE @ErrorState INT;

	SELECT
		@ErrorLine = ERROR_LINE(),
		@ErrorMessage = ERROR_MESSAGE(),
		@ErrorSeverity = ERROR_SEVERITY(),
		@ErrorState = ERROR_STATE();
		 
	RAISERROR ('Error found in line %i: %s', @ErrorSeverity, @ErrorState, @ErrorLine, @ErrorMessage) WITH SETERROR
END CATCH