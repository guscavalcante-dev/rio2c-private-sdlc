BEGIN TRY
	BEGIN TRANSACTION	
		-----------------------------------------------
		-- RIO2C 2025 | BUSINESS
		-----------------------------------------------
		IF NOT EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatforms] WHERE Uid = '07937D79-3A74-455A-8FD7-BE0AD638F91B')
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
					   ('07937D79-3A74-455A-8FD7-BE0AD638F91B'
					   ,7
					   ,3
					   ,'2761483'
					   ,1
					   ,0
					   ,GETDATE()
					   ,1
					   ,GETDATE()
					   ,1
					   ,NULL)
		END;

		IF NOT EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = 'DE5D4CD9-CEC9-4E18-8172-406FA8E07DAD')
		BEGIN
			-----------------------------------------------
			-- BUSINESS LOTE 1
			-----------------------------------------------
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
					   ('DE5D4CD9-CEC9-4E18-8172-406FA8E07DAD'
					   ,(select Id from AttendeeSalesPlatforms where Uid = '07937D79-3A74-455A-8FD7-BE0AD638F91B')
					   ,'BUSINESS LOTE 1'
					   ,'BUSINESS LOTE 1'
					   ,500 --Precisamos definir qual CollaboratorType a credencial Business vai usar
					   ,0
					   ,GETDATE()
					   ,1
					   ,GETDATE()
					   ,1) 
		END;
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