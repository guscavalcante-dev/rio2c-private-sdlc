BEGIN TRY
	BEGIN TRANSACTION
		ALTER TABLE dbo.Editions DROP COLUMN MusicPitchingMaximumProjectSubmissionsByEdition;
		ALTER TABLE dbo.Editions DROP COLUMN MusicPitchingMaximumProjectSubmissionsByParticipant;
		ALTER TABLE dbo.Editions DROP COLUMN MusicPitchingMaximumApprovedProjectsByCommissionMember;
		ALTER TABLE dbo.Editions DROP COLUMN MusicPitchingEvaluationStartDateByCurator;
		ALTER TABLE dbo.Editions DROP COLUMN MusicPitchingMaximumApprovedProjectsByCurator;
		ALTER TABLE dbo.Editions DROP COLUMN MusicPitchingMaximumApprovedProjectsByPopularVote;
		ALTER TABLE dbo.Editions DROP COLUMN MusicPitchingEvaluationStartDateByPopularVote;
		ALTER TABLE dbo.Editions DROP COLUMN MusicPitchingEvaluationStartDateByRepechage;
		ALTER TABLE dbo.Editions DROP COLUMN MusicPitchingMaximumApprovedProjectsByRepechage;
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
