BEGIN TRY
    BEGIN TRANSACTION

        -- Insert 'Segmento | Segment' InterestGroup
        IF NOT EXISTS (
            SELECT 1 
            FROM [dbo].[InterestGroups] 
            WHERE [Uid] = 'DFFD8186-1E7E-4730-B005-56D1506C9DB6'
        )
        BEGIN
            INSERT INTO [dbo].[InterestGroups]
               ([Uid]
               ,[ProjectTypeId]
               ,[Name]
               ,[Type]
               ,[DisplayOrder]
               ,[IsDeleted]
               ,[CreateDate]
               ,[CreateUserId]
               ,[UpdateDate]
               ,[UpdateUserId]
               ,[IsCommission])
                VALUES
                ('DFFD8186-1E7E-4730-B005-56D1506C9DB6'  -- Uid
                ,(select TOP 1 id from [ProjectTypes] where uid ='E5B7CBA4-10B5-4FCF-A778-09B40AEB6C01') -- Audiovisual - Pitching
                ,'Segmento | Segment'                  -- Name
                ,'Unique'                              -- Type
                ,1                                     -- DisplayOrder
                ,0                                     -- IsDeleted
                ,SYSDATETIMEOFFSET()                   -- CreateDate
                ,1                                     -- CreateUserId
                ,SYSDATETIMEOFFSET()                   -- UpdateDate
                ,1                                     -- UpdateUserId
                ,0);                                   -- IsCommission
        END

        -- Insert 'Doc / Factual'
        IF NOT EXISTS (
            SELECT 1 
            FROM [dbo].[Interests] 
            WHERE [Uid] = 'E3B5C1A2-4D4F-4D72-9D6F-8C8F4A2D9B74'
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
                   ('E3B5C1A2-4D4F-4D72-9D6F-8C8F4A2D9B74'
                   ,(select TOP 1 id from [InterestGroups] where uid ='DFFD8186-1E7E-4730-B005-56D1506C9DB6')
                   ,'Doc/Factual'
                   ,6
                   ,0
                   ,GETDATE()
                   ,1
                   ,GETDATE()
                   ,1
                   ,0
                   ,NULL)
        END

        -- Insert 'Animação'
        IF NOT EXISTS (
            SELECT 1 
            FROM [dbo].[Interests] 
            WHERE [Uid] = '9F7C8E62-3A49-4DA8-80B2-3E6A417E57A2'
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
                   ('9F7C8E62-3A49-4DA8-80B2-3E6A417E57A2'
                   ,(select TOP 1 id from [InterestGroups] where uid ='DFFD8186-1E7E-4730-B005-56D1506C9DB6')
                   ,'Animação'
                   ,7
                   ,0
                   ,GETDATE()
                   ,1
                   ,GETDATE()
                   ,1
                   ,0
                   ,NULL)
        END

        -- Insert 'Reality Show'
        IF NOT EXISTS (
            SELECT 1 
            FROM [dbo].[Interests] 
            WHERE [Uid] = '5D2F9B8A-1E5D-4B49-9B3E-2F1D6C7A8F9E'
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
                   ('5D2F9B8A-1E5D-4B49-9B3E-2F1D6C7A8F9E'
                   ,(select TOP 1 id from [InterestGroups] where uid ='DFFD8186-1E7E-4730-B005-56D1506C9DB6')
                   ,'Reality Show'
                   ,8
                   ,0
                   ,GETDATE()
                   ,1
                   ,GETDATE()
                   ,1
                   ,0
                   ,NULL)
        END

        -- Insert 'Ficção'
        IF NOT EXISTS (
            SELECT 1 
            FROM [dbo].[Interests] 
            WHERE [Uid] = '4A6D3E7C-9F2B-42A7-B6D8-5E3F1A8B9C7D'
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
                   ('4A6D3E7C-9F2B-42A7-B6D8-5E3F1A8B9C7D'
                   ,(select TOP 1 id from [InterestGroups] where uid ='DFFD8186-1E7E-4730-B005-56D1506C9DB6')
                   ,'Ficção'
                   ,9
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
