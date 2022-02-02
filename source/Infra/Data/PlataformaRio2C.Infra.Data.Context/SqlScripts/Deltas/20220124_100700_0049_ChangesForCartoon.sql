--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done
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
			[CartoonProjectFormatId] [int] NOT NULL,
			[IsDeleted] [bit] NOT NULL,
			[CreateDate] [datetimeoffset](7) NULL,
			[CreateUserId] [int] NOT NULL,
			[UpdateDate] [datetimeoffset](7) NULL,
			[UpdateUserId] [int] NOT NULL,
		 CONSTRAINT [PK_CartoonProjects] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_CartoonProjects_Uid] UNIQUE NONCLUSTERED 
		(
			[Uid] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_AttendeeCartoonProjects_EditionId_CartoonProjectId] UNIQUE NONCLUSTERED 
		(
			[EditionId] ASC,
			[CartoonProjectId] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_AttendeeCartoonProjects_Uid] UNIQUE NONCLUSTERED 
		(
			[Uid] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_CartoonProjectFormats_Uid] UNIQUE NONCLUSTERED 
		(
			[Uid] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]

		ALTER TABLE [dbo].[CartoonProjectFormats]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectFormats_CreateUserId] FOREIGN KEY([CreateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectFormats] CHECK CONSTRAINT [FK_Users_CartoonProjectFormats_CreateUserId]

		ALTER TABLE [dbo].[CartoonProjectFormats]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectFormats_UpdateUserId] FOREIGN KEY([UpdateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectFormats] CHECK CONSTRAINT [FK_Users_CartoonProjectFormats_UpdateUserId]

		ALTER TABLE [dbo].[CartoonProjects]  WITH CHECK ADD  CONSTRAINT [FK_CartoonProjectFormats_CartoonProjects_CartoonProjectFormatId] FOREIGN KEY([CartoonProjectFormatId])
		REFERENCES [dbo].[CartoonProjectFormats] ([Id])

		ALTER TABLE [dbo].[CartoonProjects] CHECK CONSTRAINT [FK_CartoonProjectFormats_CartoonProjects_CartoonProjectFormatId]


		INSERT INTO [dbo].[CartoonProjectFormats] ([Uid],[Name],[DisplayOrder],[HasAdditionalInfo],[IsDeleted],[CreateDate],[CreateUserId],[UpdateDate],[UpdateUserId],[Description])
		VALUES ('44ab63de-66ba-4032-b9ec-171539413e85', 'Live Action', 1, 0, 0, GETDATE(), 1, GETDATE(), 1, ''),
			   ('0d872c2e-0101-4a15-b203-ba98e1e6f7b9', 'Animação', 1, 0, 0, GETDATE(), 1, GETDATE(), 1, '')

		---------------------------------------------------------------------
		-- Editions
		---------------------------------------------------------------------
		ALTER TABLE "dbo"."Editions"
		ADD CartoonProjectSubmitStartDate  datetimeoffset  NULL

		ALTER TABLE "dbo"."Editions"
		ADD CartoonProjectSubmitEndDate  datetimeoffset  NULL

		ALTER TABLE "dbo"."Editions"
		ADD CartoonCommissionEvaluationStartDate  datetimeoffset  NULL

		ALTER TABLE "dbo"."Editions"
		ADD CartoonCommissionEvaluationEndDate  datetimeoffset  NULL

		ALTER TABLE "dbo"."Editions"
		ADD CartoonCommissionMinimumEvaluationsCount  int  NULL

		ALTER TABLE "dbo"."Editions"
		ADD CartoonCommissionMaximumApprovedProjectsCount  int  NULL

		EXEC('UPDATE [dbo].[Editions] SET [CartoonProjectSubmitStartDate] = [InnovationProjectSubmitStartDate]')
		EXEC('UPDATE [dbo].[Editions] SET [CartoonProjectSubmitEndDate] = [InnovationProjectSubmitEndDate]')
		EXEC('UPDATE [dbo].[Editions] SET [CartoonCommissionEvaluationStartDate] = [InnovationCommissionEvaluationStartDate]')
		EXEC('UPDATE [dbo].[Editions] SET [CartoonCommissionEvaluationEndDate] = [InnovationCommissionEvaluationEndDate]')
		EXEC('UPDATE [dbo].[Editions] SET [CartoonCommissionMinimumEvaluationsCount] = 3')
		EXEC('UPDATE [dbo].[Editions] SET [CartoonCommissionMaximumApprovedProjectsCount] = 3')

		ALTER TABLE "dbo"."Editions"
		ALTER COLUMN CartoonProjectSubmitStartDate  datetimeoffset  NOT NULL

		ALTER TABLE "dbo"."Editions"
		ALTER COLUMN CartoonProjectSubmitEndDate  datetimeoffset  NOT NULL

		ALTER TABLE "dbo"."Editions"
		ALTER COLUMN CartoonCommissionEvaluationStartDate  datetimeoffset  NOT NULL

		ALTER TABLE "dbo"."Editions"
		ALTER COLUMN CartoonCommissionEvaluationEndDate  datetimeoffset  NOT NULL

		ALTER TABLE "dbo"."Editions"
		ALTER COLUMN CartoonCommissionMinimumEvaluationsCount  int  NOT NULL

		ALTER TABLE "dbo"."Editions"
		ALTER COLUMN CartoonCommissionMaximumApprovedProjectsCount  int  NOT NULL

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