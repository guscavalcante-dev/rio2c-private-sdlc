BEGIN TRY
	BEGIN TRANSACTION	
		-----------------------------------------------
		-- CREATOR LOTE 3
		-----------------------------------------------
		IF NOT EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = 'EBA3F1EE-346C-4303-A828-BCAA88C79EF9')
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
						
					   ('EBA3F1EE-346C-4303-A828-BCAA88C79EF9'
					   ,(select Id from AttendeeSalesPlatforms where Uid = 'B77A43B9-5DD3-4C0A-88EB-BABE62AACC18')
					   ,'CREATOR LOTE 3'
					   ,'CREATOR LOTE 3'
					   ,501 --Creator
					   ,0
					   ,GETDATE()
					   ,1
					   ,GETDATE()
					   ,1)
		END;

		-----------------------------------------------
		-- CREATOR LOTE 4
		-----------------------------------------------
		IF NOT EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = '9AA24767-D47D-4A11-9CFC-12EC30E4A823')
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
						
					   ('9AA24767-D47D-4A11-9CFC-12EC30E4A823'
					   ,(select Id from AttendeeSalesPlatforms where Uid = 'B77A43B9-5DD3-4C0A-88EB-BABE62AACC18')
					   ,'CREATOR LOTE 4'
					   ,'CREATOR LOTE 4'
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