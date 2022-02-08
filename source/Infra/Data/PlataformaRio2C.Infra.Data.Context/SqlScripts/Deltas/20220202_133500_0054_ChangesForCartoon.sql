--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done
--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION
		---------------------------------------------------------------------
		-- CartoonProjects
		---------------------------------------------------------------------
		ALTER TABLE CartoonProjects ALTER COLUMN Title VARCHAR(300) NOT NULL
		ALTER TABLE CartoonProjects ALTER COLUMN LogLine VARCHAR(3000) NOT NULL
		ALTER TABLE CartoonProjects ALTER COLUMN Summary VARCHAR(3000) NOT NULL
		ALTER TABLE CartoonProjects ALTER COLUMN Motivation VARCHAR(3000) NOT NULL
		ALTER TABLE CartoonProjects ALTER COLUMN NumberOfEpisodes int NOT NULL
		ALTER TABLE CartoonProjects ALTER COLUMN EachEpisodePlayingTime VARCHAR(10) NOT NULL

		---------------------------------------------------------------------
		-- CartoonProjectCreators
		---------------------------------------------------------------------
		CREATE TABLE [dbo].[CartoonProjectCreators](
		[Id] [int] IDENTITY(1,1) NOT NULL, 
		[Uid] [uniqueidentifier] NOT NULL,
		[CartoonProjectId] [int] NOT NULL,

		[FirstName] [varchar](300) NOT NULL,
		[LastName] [varchar](300) NOT NULL,
		[Document] [varchar](50) NOT NULL,
		[Email] [varchar](300) NOT NULL,
		[CellPhone] [varchar](50) NOT NULL,
		[PhoneNumber] [varchar](50) NULL,
		[MiniBio] [varchar](3000) NOT NULL,
		[IsResponsible] [bit] NOT NULL,

		[IsDeleted] [bit] NOT NULL,
		[CreateDate] [datetimeoffset](7) NOT NULL,
		[CreateUserId] [int] NOT NULL,
		[UpdateDate] [datetimeoffset](7) NOT NULL,
		[UpdateUserId] [int] NOT NULL,
		 CONSTRAINT [PK_CartoonProjectCreators] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_CartoonProjectCreators_Uid] UNIQUE NONCLUSTERED 
		(
			[Uid] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]

		ALTER TABLE [dbo].[CartoonProjectCreators]  WITH CHECK ADD  CONSTRAINT [FK_CartoonProjects_CartoonProjectCreators_CartoonProjectId] FOREIGN KEY([CartoonProjectId])
		REFERENCES [dbo].[CartoonProjects] ([Id])

		ALTER TABLE [dbo].[CartoonProjectCreators] CHECK CONSTRAINT [FK_CartoonProjects_CartoonProjectCreators_CartoonProjectId]

		ALTER TABLE [dbo].[CartoonProjectCreators]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectCreators_CreateUserId] FOREIGN KEY([CreateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectCreators] CHECK CONSTRAINT [FK_Users_CartoonProjectCreators_CreateUserId]

		ALTER TABLE [dbo].[CartoonProjectCreators]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectCreators_UpdateUserId] FOREIGN KEY([UpdateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectCreators] CHECK CONSTRAINT [FK_Users_CartoonProjectCreators_UpdateUserId]

		---------------------------------------------------------------------
		-- CartoonProjectOrganizations
		---------------------------------------------------------------------
		CREATE TABLE [dbo].[CartoonProjectOrganizations](
		[Id] [int] IDENTITY(1,1) NOT NULL, 
		[Uid] [uniqueidentifier] NOT NULL,
		[CartoonProjectId] [int] NOT NULL,
		[AddressId] [int] NULL,
		[Name] [varchar](300) NULL,
		[TradeName] [varchar](300) NULL,
		[Document] [varchar](50) NULL,
		[PhoneNumber] [varchar](50) NULL,
		[ReelUrl] [varchar](100) NULL,
		[IsDeleted] [bit] NOT NULL,
		[CreateDate] [datetimeoffset](7) NOT NULL,
		[CreateUserId] [int] NOT NULL,
		[UpdateDate] [datetimeoffset](7) NOT NULL,
		[UpdateUserId] [int] NOT NULL,

		CONSTRAINT [PK_CartoonProjectOrganizations] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
		 CONSTRAINT [IDX_UQ_CartoonProjectOrganizations_Uid] UNIQUE NONCLUSTERED 
		(
			[Uid] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]

		ALTER TABLE [dbo].[CartoonProjectOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_CartoonProjectOrganizations_AddressId] FOREIGN KEY([AddressId])
		REFERENCES [dbo].[Addresses] ([Id])

		ALTER TABLE [dbo].[CartoonProjectOrganizations] CHECK CONSTRAINT [FK_Addresses_CartoonProjectOrganizations_AddressId]

		ALTER TABLE [dbo].[CartoonProjectOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_CartoonProjects_CartoonProjectOrganizations_CartoonProjectId] FOREIGN KEY([CartoonProjectId])
		REFERENCES [dbo].[CartoonProjects] ([Id])

		ALTER TABLE [dbo].[CartoonProjectOrganizations] CHECK CONSTRAINT [FK_CartoonProjects_CartoonProjectOrganizations_CartoonProjectId]

		ALTER TABLE [dbo].[CartoonProjectOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectOrganizations_CreateUserId] FOREIGN KEY([CreateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectOrganizations] CHECK CONSTRAINT [FK_Users_CartoonProjectOrganizations_CreateUserId]

		ALTER TABLE [dbo].[CartoonProjectOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_Users_CartoonProjectOrganizations_UpdateUserId] FOREIGN KEY([UpdateUserId])
		REFERENCES [dbo].[Users] ([Id])

		ALTER TABLE [dbo].[CartoonProjectOrganizations] CHECK CONSTRAINT [FK_Users_CartoonProjectOrganizations_UpdateUserId]

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