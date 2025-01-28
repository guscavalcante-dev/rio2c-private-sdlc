BEGIN TRY
	BEGIN TRANSACTION	

		DELETE FROM [dbo].[ProjectEvaluationRefuseReasons]
		WHERE [Uid] IN (
			'A5820D27-2B67-4EF2-8D23-94EFA9563ED5',
			'2F7A0B9F-C88C-4186-8226-618AC085B784',
			'7F8D9FF8-3337-4DFB-91A6-35697B188CED',
			'5EF644C4-CA26-4FE9-B54F-5E16C3664B62',
			'49CA896C-B013-4807-BC87-EA098C6C38BA',
			'B144050F-BF2A-47FA-89CF-27665334BCFD'
		);

	-- Commands End
	COMMIT TRAN -- Transaction Success!
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN -- RollBack in case of Error

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
