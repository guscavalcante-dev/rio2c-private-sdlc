BEGIN TRY
    BEGIN TRANSACTION

        IF NOT EXISTS (
            SELECT 1 
            FROM [dbo].[Interests] 
            WHERE [Uid] = '7EF2AF9A-BBD1-48FC-A2AB-7109B17C0FD4'
        )
        BEGIN
            INSERT INTO [dbo].[Interests]
                   ([Uid]
                   ,[InterestGroupId]
                   ,[Name]
                   ,[DisplayOrder]
                   ,[IsDeleted]
                   ,[CreateDate]
                   ,[CreateUserId]
                   ,[UpdateDate]
                   ,[UpdateUserId]
                   ,[HasAdditionalInfo]
                   ,[Description])
             VALUES
                   ('7EF2AF9A-BBD1-48FC-A2AB-7109B17C0FD4'
                   ,13 -- Descreva quais oportunidades você pode oferecer | Describe what opportunities you can offer
                   ,'Licenciamento e/ou gestão de direitos musicais | Licensing and/or management of music rights'
                   ,5
                   ,0
                   ,GETDATE()
                   ,1
                   ,GETDATE()
                   ,1
                   ,0
                   ,NULL)
        END

    COMMIT TRANSACTION
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    -- Re-raise the error
    DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
    SELECT 
        @ErrorMessage = ERROR_MESSAGE(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE();
    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH;
