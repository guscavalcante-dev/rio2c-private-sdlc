--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done
--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION

		update OrganizationTypes set IsSeller = 0		 where Uid != '7CE5A34F-E31F-4C26-BED9-CDD6A0206185'; --The Producer must be the only one IsSeller = 1
		update OrganizationTypes set Name = 'Producer'	 where Uid = '7CE5A34F-E31F-4C26-BED9-CDD6A0206185';
		update OrganizationTypes set Name = 'Investor'	 where Uid = '7EB327A9-95E8-4514-8E66-39510FC9ED03';
		update OrganizationTypes set Name = 'Music Band' where Uid = 'D077BA5C-2982-4B69-95D4-D9AA1BF8E7F4';
		update OrganizationTypes set Name = 'Recorder'	 where Uid = '243AAFB2-B610-49B4-B9BC-33CDF631C367';

		update CollaboratorTypes set Name = 'Executive | Audiovisual Player' where uid = '2D6F0E07-7990-458A-8207-1471DC3D1833'

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
