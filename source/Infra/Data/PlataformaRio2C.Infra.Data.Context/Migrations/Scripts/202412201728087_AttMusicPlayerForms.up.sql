BEGIN TRY
    BEGIN TRANSACTION

        -----------------------------------------------
        -- Insert into InterestGroups
        -----------------------------------------------
        IF NOT EXISTS (SELECT 1 FROM [dbo].[InterestGroups] WHERE Uid = 'A1B2C3D4-E5F6-7890-1234-56789ABCDEF0')
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
                       ('A1B2C3D4-E5F6-7890-1234-56789ABCDEF0'
                       ,3
                       ,'Descreva quais oportunidades você pode oferecer'
                       ,'Multiple'
                       ,2
                       ,0
                       ,SYSDATETIMEOFFSET()
                       ,1
                       ,SYSDATETIMEOFFSET()
                       ,1
                       ,0)
        END;

        -----------------------------------------------
        -- Insert into Interests
        -----------------------------------------------
        IF NOT EXISTS (SELECT 1 FROM [dbo].[Interests] WHERE Uid = 'A7B3D9E1-4237-4F3D-B1C5-29A4FEC1239F')
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
                       ,[HasAdditionalInfo])
                 VALUES
                       ('A7B3D9E1-4237-4F3D-B1C5-29A4FEC1239F'
                       ,(SELECT Id FROM InterestGroups WHERE Uid = 'A1B2C3D4-E5F6-7890-1234-56789ABCDEF0')
                       ,'Produção Musical | Music Production'
                       ,1
                       ,0
                       ,SYSDATETIMEOFFSET()
                       ,1
                       ,SYSDATETIMEOFFSET()
                       ,1
                       ,0),
                       ('E2B2C3D4-F5G6-7890-1234-56789ABCDEF2'
                       ,(SELECT Id FROM InterestGroups WHERE Uid = 'A1B2C3D4-E5F6-7890-1234-56789ABCDEF0')
                       ,'Promoção, Marketing, Estratégias de Lançamento | Promotion, Marketing, Launch Strategies'
                       ,2
                       ,0
                       ,SYSDATETIMEOFFSET()
                       ,1
                       ,SYSDATETIMEOFFSET()
                       ,1
                       ,0),
                       ('E3B2C3D4-F5G6-7890-1234-56789ABCDEF3'
                       ,(SELECT Id FROM InterestGroups WHERE Uid = 'A1B2C3D4-E5F6-7890-1234-56789ABCDEF0')
                       ,'Shows, Planejamento Turnê | Shows, Tour Planning'
                       ,3
                       ,0
                       ,SYSDATETIMEOFFSET()
                       ,1
                       ,SYSDATETIMEOFFSET()
                       ,1
                       ,0),
                       ('E4B2C3D4-F5G6-7890-1234-56789ABCDEF4'
                       ,(SELECT Id FROM InterestGroups WHERE Uid = 'A1B2C3D4-E5F6-7890-1234-56789ABCDEF0')
                       ,'Distribuição Digital | Digital Distribution'
                       ,4
                       ,0
                       ,SYSDATETIMEOFFSET()
                       ,1
                       ,SYSDATETIMEOFFSET()
                       ,1
                       ,0)
        END;

          -----------------------------------------------
        -- Update InterestGroups Name
        -----------------------------------------------
        IF EXISTS (SELECT 1 FROM [dbo].[InterestGroups] WHERE Uid = '33AE337F-99F1-4C8D-98EC-8044572A104D')
        BEGIN
            UPDATE [dbo].[InterestGroups]
            SET [Name] = 'Descreva o tipo de negócios, perfil ou gênero que está buscando no Mercado'
            WHERE [Uid] = '33AE337F-99F1-4C8D-98EC-8044572A104D';
        END;

         -----------------------------------------------
        -- Delete OTHERS from Interest Record - Music Player
        -----------------------------------------------
        DELETE FROM [dbo].[Interests]
        WHERE [Uid] = 'E4D8195E-2A7F-48AC-85C2-065B6E4101E4';

        -----------------------------------------------
        -- Update Interest Name
        -----------------------------------------------
        UPDATE [dbo].[Interests]
        SET [Name] = 'Novos Talentos/Novos Negócios | New Talents/New Businesses'
        WHERE [Uid] = '88E30899-BE53-408A-84F1-BBFF7C8CBB93';

        -----------------------------------------------
        -- Insert New Interests with Fixed UIDs
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
                   ('D5A4C8EF-1234-4DAB-9F72-8E46A1A2B2C3', -- Novo Trabalho / EP / Single / Feat
                    8,
                    'Novo Trabalho / EP / Single / Feat | New Work / EP / Single / Feat',
                    3,
                    0,
                    SYSDATETIMEOFFSET(),
                    1,
                    SYSDATETIMEOFFSET(),
                    1,
                    0),
                   ('9F36A7DE-5678-4A9C-BD91-0E12A3B4C5D6', -- Show / Tour
                    8,
                    'Show / Tour | Show / Tour',
                    3,
                    0,
                    SYSDATETIMEOFFSET(),
                    1,
                    SYSDATETIMEOFFSET(),
                    1,
                    0);


         -----------------------------------------------
        -- Delete TargetAudiences Records
        -----------------------------------------------
        DELETE FROM [dbo].[TargetAudiences]
        WHERE [Uid] IN (
            'A92A28DD-3732-4288-A092-55E4A0AED26A', -- Empresário ou Agenciamento de Artistas
            '8C35D600-7B9D-4F85-8990-CCF955C8C45C', -- Marketing Musical
            '30044E05-1F56-4CA7-B253-2CADD9407196', -- Compositor
            '9743EFB8-3994-47E6-9BF4-1A8E82CF23B7', -- Criadores de Podcast
            'B40A3592-BA93-44B0-A710-2702DB4F7C39', -- Agências
            '597CF20B-5774-4229-B584-5CE44B818E94'  -- Outros
        );

         -----------------------------------------------
        -- Insert new TargetAudiences records
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
                   ('D2B2D0E5-8337-4D91-8E98-AF1F0E0E1CC1', 3, 'Empresarios artístico | Artistic Managers', 2, 0, '2023-12-26 18:55:43.7966667 +00:00', 1, '2023-12-26 18:55:43.7966667 +00:00', 1, 0),
                   ('F1C159F9-1F5F-45E2-8D2C-FF5429BB4213', 3, 'Agentes | Agents/Bookers', 3, 0, '2023-12-26 18:55:43.7966667 +00:00', 1, '2023-12-26 18:55:43.7966667 +00:00', 1, 0),
                   ('ABF2CDE5-88FF-4B8E-82F2-5E8F0F16D9A4', 3, 'Produtor Musical | Music Producer', 4, 0, '2023-12-26 18:55:43.7966667 +00:00', 1, '2023-12-26 18:55:43.7966667 +00:00', 1, 0),
                   ('94BEF8B3-0A57-4774-99B0-BCBB4EBDFFDC', 3, 'Produtoras (agências de talentos) | Production Companies (Talent Agencies)', 5, 0, '2023-12-26 18:55:43.7966667 +00:00', 1, '2023-12-26 18:55:43.7966667 +00:00', 1, 0);
        
         -- Removing useless activties.
        DELETE FROM [dbo].[Activities]
        WHERE [Uid] IN (
            '0F2BB02C-1480-4E88-9B9A-4C1D12F03470', -- Streamings | Streaming Services
            '4E30B7E8-B9F4-4882-9C1C-D6760513DFD7', -- Canais | Channels
            '0B7524D8-7007-4289-8A01-BCD699353DD6', -- Agência de Publicidade | Advertising Agency
            '9E5481A5-B81E-4100-A6EB-08AEB8DFCDCD', -- Associação | Association
            '667142F8-8F28-49A4-817D-994298E3EDF0', -- Mídia | Media
            'C830BFC0-33F1-47E5-8114-43A703AF7F9C'  -- Festivais | Festivals
        );

        -- updating the name of "Editoras"
        UPDATE [dbo].[Activities]
        SET [Name] = 'Editoras e Associações | Publishers and Associations'
        WHERE [Uid] = 'E47E5CFA-5082-48DE-B995-AC55323AEFB2';

        -- New activities
        INSERT INTO [dbo].[Activities]
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
                   ('6007DE3E-2DA9-4115-8727-419524F2F11E', 3, 'Gravadoras | Record Labels', 1, 0, '2023-12-26 18:55:43.7766667 +00:00', 1, '2023-12-26 18:55:43.7766667 +00:00', 1, 0),
                   ('525C166D-0CAC-4F11-831E-974381DDAE49', 3, 'Agregadoras | Aggregators', 2, 0, '2023-12-26 18:55:43.7766667 +00:00', 1, '2023-12-26 18:55:43.7766667 +00:00', 1, 0),
                   ('E47E5CFA-5082-48DE-B995-AC55323AEFB2', 3, 'Editoras e Associações | Publishers and Associations', 3, 0, '2023-12-26 18:55:43.7766667 +00:00', 1, '2023-12-26 18:55:43.7766667 +00:00', 1, 0),
                   ('6007DE3E-2DA9-4115-8727-419524F2F11E', 3, 'Selo | Label', 4, 0, '2023-12-26 18:55:43.7766667 +00:00', 1, '2023-12-26 18:55:43.7766667 +00:00', 1, 0),
                   ('E47E5CFA-5082-48DE-B995-AC55323AEFB2', 3, 'Palco (Festivais e Casas de Show) | Stage (Festivals and Concert Venues)', 5, 0, '2023-12-26 18:55:43.7766667 +00:00', 1, '2023-12-26 18:55:43.7766667 +00:00', 1, 0);



        COMMIT TRAN -- Sucesso na transação!
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
