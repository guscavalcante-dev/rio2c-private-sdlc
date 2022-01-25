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
			[Title] [varchar](300) NULL,
			[LogLine] [varchar](3000) NULL,
			[Summary] [varchar](3000) NULL,
			[Motivation] [varchar](3000) NULL,
			[NumberOfEpisodes] [int] NULL,
			[EachEpisodePlayingTime] [varchar](10) NULL,
			[TotalValueOfProject] [varchar](50) NULL,
			[IsDeleted] [bit] NOT NULL,
			[CreateDate] [datetimeoffset](7) NULL,
			[CreateUserId] [int] NOT NULL,
			[UpdateDate] [datetimeoffset](7) NULL,
			[UpdateUserId] [int] NOT NULL,
		 CONSTRAINT [PK_CartoonProjects] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_CartoonProjects_Uid] UNIQUE NONCLUSTERED 
		(
			[Uid] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
		) ON [PRIMARY]

		ALTER TABLE [dbo].[CartoonProjects]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjects_CreateUserId] FOREIGN KEY([CreateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjects] CHECK CONSTRAINT [FK_Users_CartoonProjects_CreateUserId]

		ALTER TABLE [dbo].[CartoonProjects]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjects_UpdateUserId] FOREIGN KEY([UpdateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjects] CHECK CONSTRAINT [FK_Users_CartoonProjects_UpdateUserId]


		---------------------------------------------------------------------
		-- AttendeeCartoonProjects
		---------------------------------------------------------------------
		CREATE TABLE [dbo].[AttendeeCartoonProjects](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Uid] [uniqueidentifier] NOT NULL,
			[EditionId] [int] NOT NULL,
			[CartoonProjectId] [int] NOT NULL,
			[IsDeleted] [bit] NOT NULL,
			[CreateDate] [datetimeoffset](7) NOT NULL,
			[CreateUserId] [int] NOT NULL,
			[UpdateDate] [datetimeoffset](7) NOT NULL,
			[UpdateUserId] [int] NOT NULL,
			[Grade] [decimal](4, 2) NULL,
			[EvaluationsCount] [int] NOT NULL,
			[LastEvaluationDate] [datetimeoffset](7) NULL,
			[EvaluationEmailSendDate] [datetimeoffset](7) NULL,
		 CONSTRAINT [PK_AttendeeCartoonProjects] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_AttendeeCartoonProjects_EditionId_CartoonProjectId] UNIQUE NONCLUSTERED 
		(
			[EditionId] ASC,
			[CartoonProjectId] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_AttendeeCartoonProjects_Uid] UNIQUE NONCLUSTERED 
		(
			[Uid] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
		) ON [PRIMARY]

		ALTER TABLE [dbo].[AttendeeCartoonProjects] ADD  CONSTRAINT [DF_AttendeeCartoonProjects_EvaluationsCount]  DEFAULT ((0)) FOR [EvaluationsCount]

		ALTER TABLE [dbo].[AttendeeCartoonProjects]  WITH CHECK ADD  CONSTRAINT [FK_Editions_AttendeeCartoonProjects_EditionId] FOREIGN KEY([EditionId])
		REFERENCES [dbo].[Editions] ([Id])

		ALTER TABLE [dbo].[AttendeeCartoonProjects] CHECK CONSTRAINT [FK_Editions_AttendeeCartoonProjects_EditionId]

		ALTER TABLE [dbo].[AttendeeCartoonProjects]  WITH CHECK ADD  CONSTRAINT [FK_CartoonProjects_AttendeeCartoonProjects_CartoonProjectId] FOREIGN KEY([CartoonProjectId])
		REFERENCES [dbo].[CartoonProjects] ([Id])

		ALTER TABLE [dbo].[AttendeeCartoonProjects] CHECK CONSTRAINT [FK_CartoonProjects_AttendeeCartoonProjects_CartoonProjectId]

		ALTER TABLE [dbo].[AttendeeCartoonProjects]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeCartoonProjects_CreateUserId] FOREIGN KEY([CreateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[AttendeeCartoonProjects] CHECK CONSTRAINT [FK_Users_AttendeeCartoonProjects_CreateUserId]

		ALTER TABLE [dbo].[AttendeeCartoonProjects]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeCartoonProjects_UpdateUserId] FOREIGN KEY([UpdateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[AttendeeCartoonProjects] CHECK CONSTRAINT [FK_Users_AttendeeCartoonProjects_UpdateUserId]

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