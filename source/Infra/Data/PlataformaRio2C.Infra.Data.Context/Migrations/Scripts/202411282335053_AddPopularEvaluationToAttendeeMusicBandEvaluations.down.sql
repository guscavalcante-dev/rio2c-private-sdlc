BEGIN TRY
	BEGIN TRANSACTION
		DROP INDEX IDX_AttendeeMusicBandEvaluations_PopularEvaluationStatusId ON dbo.AttendeeMusicBandEvaluations;
		ALTER TABLE dbo.AttendeeMusicBandEvaluations DROP CONSTRAINT FK_ProjectEvaluationStatuses_AttendeeMusicBandEvaluations_PopularEvaluationStatusId;
		ALTER TABLE dbo.AttendeeMusicBandEvaluations DROP COLUMN PopularEvaluationStatusId;
		ALTER TABLE dbo.AttendeeMusicBandEvaluations DROP COLUMN PopularEvaluationDate;
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