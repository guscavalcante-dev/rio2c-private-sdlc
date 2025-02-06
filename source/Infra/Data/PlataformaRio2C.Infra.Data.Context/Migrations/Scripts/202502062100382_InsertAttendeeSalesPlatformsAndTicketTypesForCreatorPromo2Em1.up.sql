BEGIN TRY
	BEGIN TRANSACTION	
		-----------------------------------------------
		-- RIO2C 2025 - CREATOR - PROMO 2 EM 1
		-----------------------------------------------
		IF NOT EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatforms] WHERE Uid = '18C99DF0-1515-4D35-A145-707112F84FF1')
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
					   ('18C99DF0-1515-4D35-A145-707112F84FF1'
					   ,7
					   ,3
					   ,'2502390'
					   ,1
					   ,0
					   ,GETDATE()
					   ,1
					   ,GETDATE()
					   ,1
					   ,NULL)
		END;

		IF NOT EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = 'DF04700C-63B7-4593-A010-402598A4AEC4')
		BEGIN
			-----------------------------------------------
			-- RIO2C 2025 - PROMO 2 em 1
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
					   ('DF04700C-63B7-4593-A010-402598A4AEC4'
					   ,(select Id from AttendeeSalesPlatforms where Uid = '18C99DF0-1515-4D35-A145-707112F84FF1')
					   ,'RIO2C 2025 - PROMO 2 em 1'
					   ,'RIO2C 2025 - PROMO 2 em 1'
					   ,501 --Creator
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