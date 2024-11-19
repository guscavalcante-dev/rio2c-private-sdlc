BEGIN TRY
	BEGIN TRANSACTION
		ALTER TABLE dbo.Editions ADD MusicPitchingMaximumProjectsInEdition int NULL;
		ALTER TABLE dbo.Editions ADD MusicPitchingMaximumProjectsPerAttendee int NULL;
		ALTER TABLE dbo.Editions ADD MusicPitchingMaximumApprovedProjectsPerMember int NULL;
		ALTER TABLE dbo.Editions ADD MusicPitchingMaximumApprovedProjectsByMembers int NULL;
		ALTER TABLE dbo.Editions ADD MusicPitchingEvaluationStartDateByCurator datetimeoffset NULL;
		ALTER TABLE dbo.Editions ADD MusicPitchingMaximumApprovedProjectsPerCurator int NULL;
		ALTER TABLE dbo.Editions ADD MusicPitchingEvaluationStartDateByPopularVote datetimeoffset NULL;
		ALTER TABLE dbo.Editions ADD MusicPitchingMaximumApprovedProjectsPerPopularVote int NULL;
		ALTER TABLE dbo.Editions ADD MusicPitchingEvaluationStartDateByRepechage datetimeoffset NULL;
		ALTER TABLE dbo.Editions ADD MusicPitchingMaximumApprovedProjectsPerRepechage int NULL;

		EXEC('UPDATE dbo.Editions SET MusicPitchingMaximumProjectsInEdition=400, MusicPitchingMaximumProjectsPerAttendee=3, MusicPitchingMaximumApprovedProjectsPerMember=2, MusicPitchingMaximumApprovedProjectsByMembers=40, MusicPitchingMaximumApprovedProjectsPerCurator=12, MusicPitchingMaximumApprovedProjectsPerPopularVote=5, MusicPitchingMaximumApprovedProjectsPerRepechage=3');
		EXEC('ALTER TABLE dbo.Editions ALTER COLUMN MusicPitchingMaximumProjectsInEdition int NOT NULL');
		EXEC('ALTER TABLE dbo.Editions ALTER COLUMN MusicPitchingMaximumProjectsPerAttendee int NOT NULL');
		EXEC('ALTER TABLE dbo.Editions ALTER COLUMN MusicPitchingMaximumApprovedProjectsPerMember int NOT NULL');
		EXEC('ALTER TABLE dbo.Editions ALTER COLUMN MusicPitchingMaximumApprovedProjectsByMembers int NOT NULL');
		EXEC('ALTER TABLE dbo.Editions ALTER COLUMN MusicPitchingMaximumApprovedProjectsPerCurator int NOT NULL');
		EXEC('ALTER TABLE dbo.Editions ALTER COLUMN MusicPitchingMaximumApprovedProjectsPerPopularVote int NOT NULL');
		EXEC('ALTER TABLE dbo.Editions ALTER COLUMN MusicPitchingMaximumApprovedProjectsPerRepechage int NOT NULL');
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