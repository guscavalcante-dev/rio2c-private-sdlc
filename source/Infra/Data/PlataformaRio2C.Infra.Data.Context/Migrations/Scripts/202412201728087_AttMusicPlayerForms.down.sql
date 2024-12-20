BEGIN TRY
    BEGIN TRANSACTION

        -----------------------------------------------
        -- Revert Interests Entries
        -----------------------------------------------
        DELETE FROM [dbo].[Interests]
        WHERE [Uid] IN (
            'A7B3D9E1-4237-4F3D-B1C5-29A4FEC1239F',
            'E2B2C3D4-F5G6-7890-1234-56789ABCDEF2',
            'E3B2C3D4-F5G6-7890-1234-56789ABCDEF3',
            'E4B2C3D4-F5G6-7890-1234-56789ABCDEF4'
        );

        -----------------------------------------------
        -- Revert InterestGroups Entry
        -----------------------------------------------
        DELETE FROM [dbo].[InterestGroups]
        WHERE [Uid] = 'A1B2C3D4-E5F6-7890-1234-56789ABCDEF0';

           -----------------------------------------------
        -- Revert InterestGroups Name Update
        -----------------------------------------------
        IF EXISTS (SELECT 1 FROM [dbo].[InterestGroups] WHERE Uid = '33AE337F-99F1-4C8D-98EC-8044572A104D')
        BEGIN
            UPDATE [dbo].[InterestGroups]
            SET [Name] = 'Está buscando | Looking For'
            WHERE [Uid] = '33AE337F-99F1-4C8D-98EC-8044572A104D';
        END;

         -----------------------------------------------
        -- Reinsert Interest 'OTHERS' Record
        -----------------------------------------------
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
                   ,[HasAdditionalInfo])
             VALUES
                   ('E4D8195E-2A7F-48AC-85C2-065B6E4101E4' -- Uid
                   ,8 -- InterestGroupId
                   ,'Outros | Others' -- Name
                   ,5 -- DisplayOrder
                   ,0 -- IsDeleted
                   ,'2023-12-26 18:55:43.8000000 +00:00' -- CreateDate
                   ,1 -- CreateUserId
                   ,'2023-12-26 18:55:43.8000000 +00:00' -- UpdateDate
                   ,1 -- UpdateUserId
                   ,1); -- HasAdditionalInfo

        -----------------------------------------------
        -- Revert Interest Name Update
        -----------------------------------------------
        UPDATE [dbo].[Interests]
        SET [Name] = 'Captação de Novos Talentos/Novos Negócios | Talent/New Business Acquisition'
        WHERE [Uid] = '88E30899-BE53-408A-84F1-BBFF7C8CBB93';

            -----------------------------------------------
        -- Delete New Interests with Fixed UIDs
        -----------------------------------------------
        DELETE FROM [dbo].[Interests]
        WHERE [Uid] IN (
            'D5A4C8EF-1234-4DAB-9F72-8E46A1A2B2C3', -- Novo Trabalho / EP / Single / Feat
            '9F36A7DE-5678-4A9C-BD91-0E12A3B4C5D6'  -- Show / Tour
        );

          -----------------------------------------------
        -- Reinsert Deleted TargetAudiences Records
        -----------------------------------------------
        INSERT INTO [dbo].[TargetAudiences]
                   ([Uid]
                   ,[ProjectTypeId]
                   ,[Name]
                   ,[DisplayOrder]
                   ,[IsDeleted]
                   ,[CreateDate]
                   ,[CreateUserId]
                   ,[UpdateDate]
                   ,[UpdateUserId]
                   ,[HasAdditionalInfo])
             VALUES
                   ('A92A28DD-3732-4288-A092-55E4A0AED26A', 3, 'Empresário ou Agenciamento de Artistas | Artist Manager or Agency', 2, 0, '2023-12-26 18:55:43.7966667 +00:00', 1, '2023-12-26 18:55:43.7966667 +00:00', 1, 0),
                   ('8C35D600-7B9D-4F85-8990-CCF955C8C45C', 3, 'Marketing Musical | Music Marketing', 3, 0, '2023-12-26 18:55:43.7966667 +00:00', 1, '2023-12-26 18:55:43.7966667 +00:00', 1, 0),
                   ('30044E05-1F56-4CA7-B253-2CADD9407196', 3, 'Compositor | Composer', 5, 0, '2023-12-26 18:55:43.7966667 +00:00', 1, '2023-12-26 18:55:43.7966667 +00:00', 1, 0),
                   ('9743EFB8-3994-47E6-9BF4-1A8E82CF23B7', 3, 'Criadores de Podcast | Podcast Creators', 6, 0, '2023-12-26 18:55:43.7966667 +00:00', 1, '2023-12-26 18:55:43.7966667 +00:00', 1, 0),
                   ('B40A3592-BA93-44B0-A710-2702DB4F7C39', 3, 'Agências | Agencies', 7, 0, '2023-12-26 18:55:43.7966667 +00:00', 1, '2023-12-26 18:55:43.7966667 +00:00', 1, 0),
                   ('597CF20B-5774-4229-B584-5CE44B818E94', 3, 'Outros | Others', 7, 0, '2023-12-26 18:55:43.7966667 +00:00', 1, '2023-12-26 18:55:43.7966667 +00:00', 1, 1);


        -----------------------------------------------
        -- Revert new TargetAudiences records
        -----------------------------------------------
        DELETE FROM [dbo].[TargetAudiences]
        WHERE [Uid] IN (
            'D2B2D0E5-8337-4D91-8E98-AF1F0E0E1CC1', -- Empresarios artístico
            'F1C159F9-1F5F-45E2-8D2C-FF5429BB4213', -- Agentes
            'ABF2CDE5-88FF-4B8E-82F2-5E8F0F16D9A4', -- Produtor Musical
            '94BEF8B3-0A57-4774-99B0-BCBB4EBDFFDC'  -- Produtoras (agências de talentos)
        );

        -- Revert changes and delete inserted records
        DELETE FROM [dbo].[Activities]
        WHERE [Uid] IN (
            '6007DE3E-2DA9-4115-8727-419524F2F11E', -- Gravadoras | Record Labels
            '525C166D-0CAC-4F11-831E-974381DDAE49', -- Agregadoras | Aggregators
            'E47E5CFA-5082-48DE-B995-AC55323AEFB2', -- Editoras e Associações | Publishers and Associations
            '6007DE3E-2DA9-4115-8727-419524F2F11E', -- Selo | Label
            'E47E5CFA-5082-48DE-B995-AC55323AEFB2'  -- Palco (Festivais e Casas de Show) | Stage (Festivals and Concert Venues)
        );

        -- Revert the edit on "Editoras"
        UPDATE [dbo].[Activities]
        SET [Name] = 'Editoras | Publishers'
        WHERE [Uid] = 'E47E5CFA-5082-48DE-B995-AC55323AEFB2';

        COMMIT TRAN -- Transaction Success!
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN -- Rollback no caso de erro

    -- Tratamento de erro
    DECLARE @ErrorLine INT;
    DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;

    SELECT
        @ErrorLine = ERROR_LINE(),
        @ErrorMessage = ERROR_MESSAGE(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE();
     
    RAISERROR ('Erro encontrado na linha %i: %s', @ErrorSeverity, @ErrorState, @ErrorLine, @ErrorMessage) WITH SETERROR;
END CATCH;
