--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION

	SET IDENTITY_INSERT [dbo].[CollaboratorTypes] ON 
	;
	INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [Description], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (102, N'a838c20b-dd55-4b9d-aecb-37a7e5320db0', N'Admin | Music', 'Administrador Música | Music Administrator', 2, 0, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1)
	;
	INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [Description], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (103, N'adf9a699-a8bc-4a1d-8971-c0ac7776d335', N'Admin | Innovation', 'Administrador Inovação | Innovation Administrator', 2, 0, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1)
	;
	INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [Description], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (104, N'3cc40a76-5e69-43e0-872e-2da26c3c1434', N'Admin | Editorial', 'Administrador Editorial | Editorial Administrator', 2, 0, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1)
	;
	INSERT [dbo].[CollaboratorTypes] ([Id], [Uid], [Name], [Description], [RoleId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId]) VALUES (105, N'203d6bfb-3009-4e7e-8be9-a4f02da795bb', N'Admin | Conferences', 'Administrador Palestras | Conferences Administrator', 2, 0, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1, CAST(N'2019-09-26 17:48:45.397' AS DateTime), 1)
	;
	UPDATE [dbo].[CollaboratorTypes] SET [RoleId] = 3 WHERE [Uid] IN (N'4ac5a971-ba73-493b-9749-0f51bb6925b5', N'495b5126-0a9e-4658-ac42-8bba55818b6f', N'cb1a63d2-5862-4b40-8b8a-7f512e5d046d')
	;
	UPDATE dbo.AttendeeCollaboratorTypes SET CollaboratorTypeId = 300 WHERE CollaboratorTypeId = 110
	;
	UPDATE dbo.AttendeeCollaboratorTypes SET CollaboratorTypeId = 301 WHERE CollaboratorTypeId = 111
	;
	UPDATE dbo.AttendeeCollaboratorTypes SET CollaboratorTypeId = 302 WHERE CollaboratorTypeId = 112
	;
	DELETE FROM dbo.CollaboratorTypes WHERE Id IN (110, 111, 112)
	;
	SET IDENTITY_INSERT [dbo].[CollaboratorTypes] OFF
	;

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
