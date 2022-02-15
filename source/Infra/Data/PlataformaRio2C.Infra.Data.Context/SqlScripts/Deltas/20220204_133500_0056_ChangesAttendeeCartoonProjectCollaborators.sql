--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done
--possible values are: "no", "yes, not done" and "yes, done"
BEGIN TRY
	BEGIN TRANSACTION
		CREATE TABLE [dbo].[AttendeeCartoonProjectCollaborators] (
			[Id] [int] IDENTITY(1, 1) NOT NULL
			,[Uid] [uniqueidentifier] NOT NULL
			,[AttendeeCartoonProjectId] [int] NOT NULL
			,[AttendeeCollaboratorId] [int] NOT NULL
			,[IsDeleted] [bit] NOT NULL
			,[CreateDate] [datetimeoffset](7) NOT NULL
			,[CreateUserId] [int] NOT NULL
			,[UpdateDate] [datetimeoffset](7) NOT NULL
			,[UpdateUserId] [int] NOT NULL
			,CONSTRAINT [PK_AttendeeCartoonProjectCollaborators] PRIMARY KEY CLUSTERED ([Id] ASC) WITH (
				PAD_INDEX = OFF
				,STATISTICS_NORECOMPUTE = OFF
				,IGNORE_DUP_KEY = OFF
				,ALLOW_ROW_LOCKS = ON
				,ALLOW_PAGE_LOCKS = ON
				) ON [PRIMARY]
			,CONSTRAINT [IDX_UQ_AttendeeCartoonProjectCollaborators_AttendeeCartoonProjectId_AttendeeCollaboratorId] UNIQUE NONCLUSTERED (
				[AttendeeCartoonProjectId] ASC
				,[AttendeeCollaboratorId] ASC
				) WITH (
				PAD_INDEX = OFF
				,STATISTICS_NORECOMPUTE = OFF
				,IGNORE_DUP_KEY = OFF
				,ALLOW_ROW_LOCKS = ON
				,ALLOW_PAGE_LOCKS = ON
				) ON [PRIMARY]
			,CONSTRAINT [IDX_UQ_AttendeeCartoonProjectCollaborators_Uid] UNIQUE NONCLUSTERED ([Uid] ASC) WITH (
				PAD_INDEX = OFF
				,STATISTICS_NORECOMPUTE = OFF
				,IGNORE_DUP_KEY = OFF
				,ALLOW_ROW_LOCKS = ON
				,ALLOW_PAGE_LOCKS = ON
				) ON [PRIMARY]
			) ON [PRIMARY]

		ALTER TABLE [dbo].[AttendeeCartoonProjectCollaborators]
			WITH NOCHECK ADD CONSTRAINT [FK_AttendeeCollaborators_AttendeeCartoonProjectCollaborators_AttendeeCollaboratorId] FOREIGN KEY ([AttendeeCollaboratorId]) REFERENCES [dbo].[AttendeeCollaborators]([Id])

		ALTER TABLE [dbo].[AttendeeCartoonProjectCollaborators] CHECK CONSTRAINT [FK_AttendeeCollaborators_AttendeeCartoonProjectCollaborators_AttendeeCollaboratorId]

		ALTER TABLE [dbo].[AttendeeCartoonProjectCollaborators]
			WITH NOCHECK ADD CONSTRAINT [FK_AttendeeCartoonProjects_AttendeeCartoonProjectCollaborators_AttendeeCartoonProjectId] FOREIGN KEY ([AttendeeCartoonProjectId]) REFERENCES [dbo].[AttendeeCartoonProjects]([Id])

		ALTER TABLE [dbo].[AttendeeCartoonProjectCollaborators] CHECK CONSTRAINT [FK_AttendeeCartoonProjects_AttendeeCartoonProjectCollaborators_AttendeeCartoonProjectId]

		ALTER TABLE [dbo].[AttendeeCartoonProjectCollaborators]
			WITH NOCHECK ADD CONSTRAINT [FK_Users_AttendeeCartoonProjectCollaborators_CreateUserId] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[Users]([Id])

		ALTER TABLE [dbo].[AttendeeCartoonProjectCollaborators] CHECK CONSTRAINT [FK_Users_AttendeeCartoonProjectCollaborators_CreateUserId]

		ALTER TABLE [dbo].[AttendeeCartoonProjectCollaborators]
			WITH NOCHECK ADD CONSTRAINT [FK_Users_AttendeeCartoonProjectCollaborators_UpdateUserId] FOREIGN KEY ([UpdateUserId]) REFERENCES [dbo].[Users]([Id])

		ALTER TABLE [dbo].[AttendeeCartoonProjectCollaborators] CHECK CONSTRAINT [FK_Users_AttendeeCartoonProjectCollaborators_UpdateUserId]
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

	SELECT @ErrorLine = ERROR_LINE()
		,@ErrorMessage = ERROR_MESSAGE()
		,@ErrorSeverity = ERROR_SEVERITY()
		,@ErrorState = ERROR_STATE();

	RAISERROR (
			'Error found in line %i: %s'
			,@ErrorSeverity
			,@ErrorState
			,@ErrorLine
			,@ErrorMessage
			)
	WITH SETERROR
END CATCH