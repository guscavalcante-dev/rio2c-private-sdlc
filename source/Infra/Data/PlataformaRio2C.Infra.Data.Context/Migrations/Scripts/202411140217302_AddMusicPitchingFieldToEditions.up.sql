BEGIN TRY
	BEGIN TRANSACTION
		IF COL_LENGTH('Editions', 'MusicPitchingMaximumProjectSubmissionsByEdition') IS NULL
            ALTER TABLE dbo.Editions ADD MusicPitchingMaximumProjectSubmissionsByEdition int NULL;

        IF COL_LENGTH('Editions', 'MusicPitchingMaximumProjectSubmissionsByParticipant') IS NULL
            ALTER TABLE dbo.Editions ADD MusicPitchingMaximumProjectSubmissionsByParticipant int NULL;

        IF COL_LENGTH('Editions', 'MusicPitchingMaximumApprovedProjectsByCommissionMember') IS NULL
            ALTER TABLE dbo.Editions ADD MusicPitchingMaximumApprovedProjectsByCommissionMember int NULL;

        IF COL_LENGTH('Editions', 'MusicPitchingCuratorEvaluationStartDate') IS NULL
            ALTER TABLE dbo.Editions ADD MusicPitchingCuratorEvaluationStartDate datetimeoffset NULL;

        IF COL_LENGTH('Editions', 'MusicPitchingMaximumApprovedProjectsByCurator') IS NULL
            ALTER TABLE dbo.Editions ADD MusicPitchingMaximumApprovedProjectsByCurator int NULL;

        IF COL_LENGTH('Editions', 'MusicPitchingPopularEvaluationStartDate') IS NULL
            ALTER TABLE dbo.Editions ADD MusicPitchingPopularEvaluationStartDate datetimeoffset NULL;

        IF COL_LENGTH('Editions', 'MusicPitchingMaximumApprovedProjectsByPopularVote') IS NULL
            ALTER TABLE dbo.Editions ADD MusicPitchingMaximumApprovedProjectsByPopularVote int NULL;

        IF COL_LENGTH('Editions', 'MusicPitchingRepechageEvaluationStartDate') IS NULL
            ALTER TABLE dbo.Editions ADD MusicPitchingRepechageEvaluationStartDate datetimeoffset NULL;

        IF COL_LENGTH('Editions', 'MusicPitchingMaximumApprovedProjectsByRepechage') IS NULL
            ALTER TABLE dbo.Editions ADD MusicPitchingMaximumApprovedProjectsByRepechage int NULL;

		EXEC('UPDATE dbo.Editions SET MusicPitchingMaximumProjectSubmissionsByEdition=400, MusicPitchingMaximumProjectSubmissionsByParticipant=3, MusicPitchingMaximumApprovedProjectsByCommissionMember=2, MusicPitchingMaximumApprovedProjectsByCurator=12, MusicPitchingMaximumApprovedProjectsByPopularVote=5, MusicPitchingMaximumApprovedProjectsByRepechage=3');
		EXEC('ALTER TABLE dbo.Editions ALTER COLUMN MusicPitchingMaximumProjectSubmissionsByEdition int NOT NULL');
		EXEC('ALTER TABLE dbo.Editions ALTER COLUMN MusicPitchingMaximumProjectSubmissionsByParticipant int NOT NULL');
		EXEC('ALTER TABLE dbo.Editions ALTER COLUMN MusicPitchingMaximumApprovedProjectsByCommissionMember int NOT NULL');
		EXEC('ALTER TABLE dbo.Editions ALTER COLUMN MusicPitchingMaximumApprovedProjectsByCurator int NOT NULL');
		EXEC('ALTER TABLE dbo.Editions ALTER COLUMN MusicPitchingMaximumApprovedProjectsByPopularVote int NOT NULL');
		EXEC('ALTER TABLE dbo.Editions ALTER COLUMN MusicPitchingMaximumApprovedProjectsByRepechage int NOT NULL');
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