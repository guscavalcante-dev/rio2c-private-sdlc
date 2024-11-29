BEGIN TRY
	BEGIN TRANSACTION
		ALTER TABLE dbo.AttendeeMusicBandEvaluations ADD PopularEvaluationStatusId int NULL;
		ALTER TABLE dbo.AttendeeMusicBandEvaluations ADD PopularEvaluationDate datetimeoffset NULL;
		CREATE NONCLUSTERED INDEX IDX_AttendeeMusicBandEvaluations_PopularEvaluationStatusId ON dbo.AttendeeMusicBandEvaluations (PopularEvaluationStatusId);
		ALTER TABLE dbo.AttendeeMusicBandEvaluations ADD CONSTRAINT FK_ProjectEvaluationStatuses_AttendeeMusicBandEvaluations_PopularEvaluationStatusId FOREIGN KEY (PopularEvaluationStatusId) REFERENCES dbo.ProjectEvaluationStatuses(Id);
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