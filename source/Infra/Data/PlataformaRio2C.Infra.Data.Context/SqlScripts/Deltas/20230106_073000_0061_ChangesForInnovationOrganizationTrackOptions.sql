--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done
--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION

		SET IDENTITY_INSERT [dbo].[InnovationOrganizationTrackOptions] ON;

		INSERT INTO [dbo].[InnovationOrganizationTrackOptions]
				([Id]
				,[Uid]
				,[Name]
				,[DisplayOrder]
				,[HasAdditionalInfo]
				,[IsDeleted]
				,[CreateDate]
				,[CreateUserId]
				,[UpdateDate]
				,[UpdateUserId]
				,[Description]
				,[InnovationOrganizationTrackOptionGroupId]
				,[IsActive])
			VALUES
				(33, 'B060E155-F7D7-4460-974E-3F7AD5B5B118', 'Cleantechs',			3, 0, 0, GETDATE(), 1 ,GETDATE(), 1, '', 4, 1);

		SET IDENTITY_INSERT [dbo].[InnovationOrganizationTrackOptions] OFF;

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