BEGIN TRY
	BEGIN TRANSACTION

		UPDATE Interests set Name = 'Espionagem | Espionage' WHERE uid = 'BEC8636F-B4F2-43D3-9B6B-EC24C320D665'
		UPDATE TargetAudiences set Name = 'Pré-Escolar | Preschool (2 to 5 years old)' where uid = 'EBC80A23-D52C-4EC6-906C-E7F62E80532A'
		UPDATE TargetAudiences set Name = 'Infantil | Children (5 to 8 years old)' where uid = '32B3EF84-8E06-4DF9-8BEC-4AA9E7623163'
		UPDATE TargetAudiences set Name = 'Infanto-Juvenil | Teen (8 to 12 years old)' where uid = '8F00BCFC-95C2-4D91-BE3B-13146E160306'

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