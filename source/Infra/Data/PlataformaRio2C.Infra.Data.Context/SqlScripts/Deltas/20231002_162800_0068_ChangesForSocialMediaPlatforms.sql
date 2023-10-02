--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done
--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION

		EXEC('UPDATE SocialMediaPlatforms set EndpointUrl = ''https://graph.facebook.com/v18.0/17841406507227120/media?fields=id,caption,timestamp,media_product_type,media_type,permalink,media_url,shortcode&access_token=''')
		EXEC('UPDATE SocialMediaPlatforms set ApiKey = ''EAAI2ALZBntaABOZCzEkcM93pEaUZCdrS7LRfueBWWQtZAYb4yI2mIrihOAsXehp3uDJqiZCOTDvcABYtX9QG6b8tp9raWWL5wQ8TZAZBDCe7wDCmUg7vDBQBXIp3wZCQfM9hpSwVPH74jEANamEYI5HAovCZC3A0Cin3glLKZBzOfFWDq4WKo2TVHwWVOcBXzxwU4W''')

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