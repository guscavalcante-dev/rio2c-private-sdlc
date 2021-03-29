BEGIN TRY
	BEGIN TRANSACTION
		INSERT INTO [dbo].[Users]
           ([Uid]
           ,[Active]
           ,[Name]
           ,[UserName]
           ,[Email]
           ,[EmailConfirmed]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[PhoneNumber]
           ,[PhoneNumberConfirmed]
           ,[TwoFactorEnabled]
           ,[LockoutEndDateUtc]
           ,[LockoutEnabled]
           ,[AccessFailedCount]
           ,[PasswordNew]
           ,[UserInterfaceLanguageId]
           ,[IsDeleted]
           ,[CreateDate]
           ,[UpdateDate])
        VALUES
           ('d08cbb7c-6197-4b8a-b91b-40bbb38bdd2d'
           ,1
           ,'Batch Process'
           ,'batchprocess@rio2c.com'
           ,'batchprocess@rio2c.com'
           ,1
           ,'AASiG74iuI5m4netRri1IAcxrLRh7oRHMozrKLUJRbs8ghGm4gYM7NrAJmCkRyP1IA=='
           ,'35653e02-c5e8-492a-b879-a70c1834aa3f'
           ,NULL
           ,0
           ,0
           ,NULL
           ,1
           ,0
           ,NULL
           ,2
           ,0
           ,GETDATE()
		   ,GETDATE());

        INSERT INTO [dbo].[UsersRoles]
            ([UserId]
            ,[RoleId])
        VALUES
            (scope_identity()
            ,1);
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
