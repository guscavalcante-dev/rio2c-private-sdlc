--must run on deploy | test: yes, done
--must run on deploy | prod: yes, not done
--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION

		SET IDENTITY_INSERT [dbo].[CollaboratorTypes] OFF 

		IF NOT EXISTS(SELECT 1 FROM [dbo].[CollaboratorTypes] WHERE Uid = N'C55E9C0C-2432-422C-87C6-199457A7C555')
		BEGIN
		INSERT INTO [dbo].[CollaboratorTypes] ([Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES ('C55E9C0C-2432-422C-87C6-199457A7C555', 'Admin | Cartoon', 2, 0, GETDATE(), 1, GETDATE(), 1, 'Administrador Cartoon | Cartoon Administrator')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[CollaboratorTypes] WHERE Uid = N'E632C88F-73D2-4EBA-9704-408EF4397DC2')
		BEGIN
		INSERT INTO [dbo].[CollaboratorTypes] ([Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES ('E632C88F-73D2-4EBA-9704-408EF4397DC2', 'Executive | Cartoon', 3, 0, GETDATE(), 1, GETDATE(), 1, 'Executivo Cartoon | Cartoon Executive')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[CollaboratorTypes] WHERE Uid = N'A2D0F90D-EA2E-4226-A3EC-47C3360CA1C0')
		BEGIN
		INSERT INTO [dbo].[CollaboratorTypes] ([Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES ('A2D0F90D-EA2E-4226-A3EC-47C3360CA1C0', 'Commission | Cartoon', 3, 0, GETDATE(), 1, GETDATE(), 1, 'Comissão Cartoon | Cartoon Commission')
		END

		IF NOT EXISTS(SELECT 1 FROM [dbo].[CollaboratorTypes] WHERE Uid = N'A1BBA990-1A08-4381-B233-E0AECE6532DB')
		BEGIN
		INSERT INTO [dbo].[CollaboratorTypes] ([Uid], [Name], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Description]) VALUES ('A1BBA990-1A08-4381-B233-E0AECE6532DB', 'Cartoon', 3, 0, GETDATE(), 1, GETDATE(), 1, 'Cartoon')
		END

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