BEGIN TRY
	BEGIN TRANSACTION	
		-----------------------------------------------
		-- FESTIVALIA - SOCIAL
		-----------------------------------------------
		IF NOT EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatforms] WHERE Uid = 'B6601073-646B-4FF5-BDBB-E98E3F664363')
		BEGIN
			INSERT INTO [dbo].[AttendeeSalesPlatforms]
					   ([Uid]
					   ,[EditionId]
					   ,[SalesPlatformId]
					   ,[SalesPlatformEventId]
					   ,[IsActive]
					   ,[IsDeleted]
					   ,[CreateDate]
					   ,[CreateUserId]
					   ,[UpdateDate]
					   ,[UpdateUserId]
					   ,[LastSalesPlatformOrderDate])
				 VALUES
					   ('B6601073-646B-4FF5-BDBB-E98E3F664363'
					   ,7
					   ,3
					   ,'2878998'
					   ,1
					   ,0
					   ,GETDATE()
					   ,1
					   ,GETDATE()
					   ,1
					   ,NULL)
		END;

		-----------------------------------------------
		-- FESTIVALIA 31.05 - INGRESSO SOCIAL
		-----------------------------------------------
		IF NOT EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = '7B9B51AC-7D6E-4639-B18F-760A8111D54C')
		BEGIN
			INSERT INTO [dbo].[AttendeeSalesPlatformTicketTypes]
					   ([Uid]
					   ,[AttendeeSalesPlatformId]
					   ,[TicketClassId]
					   ,[TicketClassName]
					   ,[CollaboratorTypeId]
					   ,[IsDeleted]
					   ,[CreateDate]
					   ,[CreateUserId]
					   ,[UpdateDate]
					   ,[UpdateUserId])
				 VALUES
						
					   ('7B9B51AC-7D6E-4639-B18F-760A8111D54C'
					   ,(select Id from AttendeeSalesPlatforms where Uid = 'B6601073-646B-4FF5-BDBB-E98E3F664363')
					   ,'FESTIVALIA 31.05 - INGRESSO SOCIAL'
					   ,'FESTIVALIA 31.05 - INGRESSO SOCIAL'
					   ,503 --Festivalia
					   ,0
					   ,GETDATE()
					   ,1
					   ,GETDATE()
					   ,1)
		END;

		-----------------------------------------------
		-- FESTIVALIA 01.06 - INGRESSO SOCIAL
		-----------------------------------------------
		IF NOT EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = 'E451E258-D18E-4816-A52C-FA26EDAF6D32')
		BEGIN
			INSERT INTO [dbo].[AttendeeSalesPlatformTicketTypes]
					   ([Uid]
					   ,[AttendeeSalesPlatformId]
					   ,[TicketClassId]
					   ,[TicketClassName]
					   ,[CollaboratorTypeId]
					   ,[IsDeleted]
					   ,[CreateDate]
					   ,[CreateUserId]
					   ,[UpdateDate]
					   ,[UpdateUserId])
				 VALUES
						
					   ('E451E258-D18E-4816-A52C-FA26EDAF6D32'
					   ,(select Id from AttendeeSalesPlatforms where Uid = 'B6601073-646B-4FF5-BDBB-E98E3F664363')
					   ,'FESTIVALIA 01.06 - INGRESSO SOCIAL'
					   ,'FESTIVALIA 01.06 - INGRESSO SOCIAL'
					   ,503 --Festivalia
					   ,0
					   ,GETDATE()
					   ,1
					   ,GETDATE()
					   ,1)
		END;

		-----------------------------------------------
		-- BACK SalesPlatformWebhookRequests TO THE PROCESSING QUEUE
		-----------------------------------------------
		EXEC('UPDATE SalesPlatformWebhookRequests SET ProcessingCount = 1, NextProcessingDate = GETDATE()
			WHERE IsProcessed = 0 AND ProcessingCount > 1 AND ProcessingErrorMessage != ''Usuário não encontrado.'' AND CreateDate >= ''2025-01-01''')

	-- Commands End
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