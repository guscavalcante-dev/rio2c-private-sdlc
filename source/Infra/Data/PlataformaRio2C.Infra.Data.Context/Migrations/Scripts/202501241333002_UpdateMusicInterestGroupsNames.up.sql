BEGIN TRY
	BEGIN TRANSACTION

		UPDATE InterestGroups SET Name = 'Descreva o tipo de negócios, perfil ou gênero que está buscando no Mercado | Describe the type of business, profile or genre you are looking for in the Market' 
		WHERE Uid = '33AE337F-99F1-4C8D-98EC-8044572A104D' 
		
		UPDATE InterestGroups SET Name = 'Descreva quais oportunidades você pode oferecer | Describe what opportunities you can offer' 
		WHERE Uid = 'A1B2C3D4-E5F6-7890-1234-56789ABCDEF0' 	

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