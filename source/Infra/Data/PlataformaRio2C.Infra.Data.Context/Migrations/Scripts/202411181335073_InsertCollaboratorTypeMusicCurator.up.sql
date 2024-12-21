BEGIN TRY
	BEGIN TRANSACTION

		SET IDENTITY_INSERT [dbo].[CollaboratorTypes] ON;
		INSERT INTO [dbo].[CollaboratorTypes]
				   ([Id]
				   ,[Uid]
				   ,[Name]
				   ,[RoleId]
				   ,[IsDeleted]
				   ,[CreateDate]
				   ,[CreateUserId]
				   ,[UpdateDate]
				   ,[UpdateUserId]
				   ,[Description])
			 VALUES
				   (
				   303
				   ,'D8C837A7-C612-4181-A37E-DA1FBF5339EE'
				   ,'Commission | Music Curator'
				   ,3
				   ,0
				   ,GETDATE()
				   ,1
				   ,GETDATE()
				   ,1
				   ,'Curador de Música | Music Curator')
		SET IDENTITY_INSERT [dbo].[CollaboratorTypes] OFF;

		-- Update the CollaboratorTypes Descriptions
		UPDATE CollaboratorTypes set Description = 'Comissão de Audiovisual | Audiovisual Commission' WHERE Uid = '60AAFB26-B483-425F-BFA6-ED0D45F3CBCB'
		UPDATE CollaboratorTypes set Description = 'Comissão de Música | Music Commission' WHERE Uid = '3633CF67-840F-4061-B480-C075A5E9F5EE'
		UPDATE CollaboratorTypes set Description = 'Comissão de Inovação | Innovation Commission' WHERE Uid = '758A53BB-7C3C-4B6F-967B-C6E613568586'
		UPDATE CollaboratorTypes set Description = 'Curador de Música | Music Curator' WHERE Uid = 'D8C837A7-C612-4181-A37E-DA1FBF5339EE'

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