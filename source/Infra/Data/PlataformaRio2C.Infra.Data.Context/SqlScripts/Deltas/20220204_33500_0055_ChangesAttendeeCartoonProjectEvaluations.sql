--must run on deploy | test: no, not done
--must run on deploy | prod: no, not done
--possible values are: "no", "yes, not done" and "yes, done"
BEGIN TRY
	BEGIN TRANSACTION

		CREATE TABLE [dbo].[AttendeeCartoonProjectEvaluations](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Uid] [uniqueidentifier] NOT NULL,
		[AttendeeCartoonProjectId] [int] NOT NULL,
		[EvaluatorUserId] [int] NOT NULL,
		[Grade] [decimal](4, 2) NOT NULL,
		[IsDeleted] [bit] NOT NULL,
		[CreateDate] [datetimeoffset](7) NOT NULL,
		[CreateUserId] [int] NOT NULL,
		[UpdateDate] [datetimeoffset](7) NOT NULL,
		[UpdateUserId] [int] NOT NULL,
	 CONSTRAINT [PK_AttendeeCartoonProjectEvaluations] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[AttendeeCartoonProjectEvaluations]  WITH CHECK ADD  CONSTRAINT [FK_AttendeeCartoonProjects_AttendeeCartoonProjectEvaluations_AttendeeCartoonProjectId] FOREIGN KEY([AttendeeCartoonProjectId])
	REFERENCES [dbo].[AttendeeCartoonProjects] ([Id])

	ALTER TABLE [dbo].[AttendeeCartoonProjectEvaluations] CHECK CONSTRAINT [FK_AttendeeCartoonProjects_AttendeeCartoonProjectEvaluations_AttendeeCartoonProjectId]

	ALTER TABLE [dbo].[AttendeeCartoonProjectEvaluations]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeCartoonProjectEvaluations_CreateUserId] FOREIGN KEY([CreateUserId])
	REFERENCES [dbo].[Users] ([Id])

	ALTER TABLE [dbo].[AttendeeCartoonProjectEvaluations] CHECK CONSTRAINT [FK_Users_AttendeeCartoonProjectEvaluations_CreateUserId]

	ALTER TABLE [dbo].[AttendeeCartoonProjectEvaluations]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeCartoonProjectEvaluations_EvaluatorUserId] FOREIGN KEY([EvaluatorUserId])
	REFERENCES [dbo].[Users] ([Id])

	ALTER TABLE [dbo].[AttendeeCartoonProjectEvaluations] CHECK CONSTRAINT [FK_Users_AttendeeCartoonProjectEvaluations_EvaluatorUserId]

	ALTER TABLE [dbo].[AttendeeCartoonProjectEvaluations]  WITH CHECK ADD  CONSTRAINT [FK_Users_AttendeeCartoonProjectEvaluations_UpdateUserId] FOREIGN KEY([UpdateUserId])
	REFERENCES [dbo].[Users] ([Id])

	ALTER TABLE [dbo].[AttendeeCartoonProjectEvaluations] CHECK CONSTRAINT [FK_Users_AttendeeCartoonProjectEvaluations_UpdateUserId]



	
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