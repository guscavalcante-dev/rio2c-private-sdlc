BEGIN TRY
	BEGIN TRANSACTION	

		-----------------------------------------------
		-- Deactivate to stop consulting the 2024 Edition events on Sympla
		-----------------------------------------------
		update AttendeeSalesPlatforms set IsActive = 0 WHERE EditionId = 6
		
		-----------------------------------------------
		-- RIO2C 2025 | INDUSTRY
		-----------------------------------------------
		IF NOT EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatforms] WHERE Uid = '06177CC4-B5AD-4FAD-A774-C8F40F3F2EED')
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
					   ('06177CC4-B5AD-4FAD-A774-C8F40F3F2EED'
					   ,7
					   ,3
					   ,'2730109'
					   ,1
					   ,0
					   ,GETDATE()
					   ,1
					   ,GETDATE()
					   ,1
					   ,NULL)
		END;

		IF NOT EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = 'B94E8A51-041A-4628-B328-7192D7F3E866')
		BEGIN
			-----------------------------------------------
			-- INDUSTRY PRÉ-VENDA
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
					   ('B94E8A51-041A-4628-B328-7192D7F3E866'
					   ,(select Id from AttendeeSalesPlatforms where Uid = '06177CC4-B5AD-4FAD-A774-C8F40F3F2EED')
					   ,'INDUSTRY PRÉ-VENDA'
					   ,'INDUSTRY PRÉ-VENDA'
					   ,500
					   ,0
					   ,GETDATE()
					   ,1
					   ,GETDATE()
					   ,1) 
		END;
		
		IF NOT EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = 'A0673F26-5D38-4ABA-9B7F-3F3FF1C15898')
		BEGIN
			-----------------------------------------------
			-- INDUSTRY PRÉ-VENDA
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
					   ('A0673F26-5D38-4ABA-9B7F-3F3FF1C15898'
					   ,(select Id from AttendeeSalesPlatforms where Uid = '06177CC4-B5AD-4FAD-A774-C8F40F3F2EED')
					   ,'INDUSTRY LOTE 1'
					   ,'INDUSTRY LOTE 1'
					   ,500
					   ,0
					   ,GETDATE()
					   ,1
					   ,GETDATE()
					   ,1)            
		END;          
		
		
		IF NOT EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatforms] WHERE Uid = 'B77A43B9-5DD3-4C0A-88EB-BABE62AACC18')
		BEGIN
			-----------------------------------------------
			-- RIO2C 2025 | CREATOR
			-----------------------------------------------
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
					   ('B77A43B9-5DD3-4C0A-88EB-BABE62AACC18'
					   ,7
					   ,3
					   ,'2734309'
					   ,1
					   ,0
					   ,GETDATE()
					   ,1
					   ,GETDATE()
					   ,1
					   ,NULL)           
		END;
		
		IF NOT EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = '7A63E214-E9A8-4CA2-BAFD-880E6648F184')
		BEGIN
			-----------------------------------------------
			-- CREATOR PRÉ-VENDA
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
					   ('7A63E214-E9A8-4CA2-BAFD-880E6648F184'
					   ,(select Id from AttendeeSalesPlatforms where Uid = 'B77A43B9-5DD3-4C0A-88EB-BABE62AACC18')
					   ,'CREATOR PRÉ-VENDA'
					   ,'CREATOR PRÉ-VENDA'
					   ,501
					   ,0
					   ,GETDATE()
					   ,1
					   ,GETDATE()
					   ,1)                      
		END;
		
		IF NOT EXISTS (SELECT 1 FROM [dbo].[AttendeeSalesPlatformTicketTypes] WHERE Uid = '0BF41BD7-DCDC-4957-BF7B-DF56979A6996')
		BEGIN
			-----------------------------------------------
			-- CREATOR LOTE 1
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
					   ('0BF41BD7-DCDC-4957-BF7B-DF56979A6996'
					   ,(select Id from AttendeeSalesPlatforms where Uid = 'B77A43B9-5DD3-4C0A-88EB-BABE62AACC18')
					   ,'CREATOR LOTE 1'
					   ,'CREATOR LOTE 1'
					   ,501
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