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
        IF NOT EXISTS (SELECT 1 FROM [dbo].[Interests] WHERE Uid = 'F7A9C3D2-85D7-4EEC-8D26-8F7F9382346B')
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
                       ('F7A9C3D2-85D7-4EEC-8D26-8F7F9382346B'
                       ,(SELECT Id FROM InterestGroups WHERE Uid = 'A1B2C3D4-E5F6-7890-1234-56789ABCDEF0')
                       ,'Produção Musical | Music Production'
                       ,1
                       ,0
                       ,SYSDATETIMEOFFSET()
                       ,1
                       ,SYSDATETIMEOFFSET()
                       ,1
                       ,0),
                       ('BC3D6F92-9B3E-4A92-9F32-AF74A7B0D832'
                       ,(SELECT Id FROM InterestGroups WHERE Uid = 'A1B2C3D4-E5F6-7890-1234-56789ABCDEF0')
                       ,'Promoção, Marketing, Estratégias de Lançamento | Promotion, Marketing, Launch Strategies'
                       ,2
                       ,0
                       ,SYSDATETIMEOFFSET()
                       ,1
                       ,SYSDATETIMEOFFSET()
                       ,1
                       ,0),
                       ('AE42C3A8-4734-4A5D-8C2B-CB39AB3DC6E9'
                       ,(SELECT Id FROM InterestGroups WHERE Uid = 'A1B2C3D4-E5F6-7890-1234-56789ABCDEF0')
                       ,'Shows, Planejamento Turnê | Shows, Tour Planning'
                       ,3
                       ,0
                       ,SYSDATETIMEOFFSET()
                       ,1
                       ,SYSDATETIMEOFFSET()
                       ,1
                       ,0),
                       ('A56EEDB0-234D-4732-B9D9-C9D1B7037C68'
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
        -- Mark OTHERS Interest Record as Deleted (IsDeleted = 1)
        -----------------------------------------------
        UPDATE [dbo].[Interests]
        SET [IsDeleted] = 1
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
            -- Update TargetAudiences Records
            -----------------------------------------------
            UPDATE [dbo].[TargetAudiences]
            SET [IsDeleted] = 1
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
        
              -----------------------------------------------
            -- Update IsDeleted to 1 for Activities
            -----------------------------------------------
            UPDATE [dbo].[Activities]
            SET [IsDeleted] = 1
            WHERE [Uid] IN (
                '0F2BB02C-1480-4E88-9B9A-4C1D12F03470', -- Streamings | Streaming Services
                '4E30B7E8-B9F4-4882-9C1C-D6760513DFD7', -- Canais | Channels
                '0B7524D8-7007-4289-8A01-BCD699353DD6', -- Agência de Publicidade | Advertising Agency
                '9E5481A5-B81E-4100-A6EB-08AEB8DFCDCD', -- Associação | Association
                '667142F8-8F28-49A4-817D-994298E3EDF0', -- Mídia | Media
                '28826C44-4276-4FEC-8014-EC451FFD6594', -- Palco
                'C830BFC0-33F1-47E5-8114-43A703AF7F9C'  -- Festivais | Festivals
            );
       
    -----------------------------------------------
-- Atividades.
-----------------------------------------------

-- 1. Check and update/insert for 'Gravadoras | Record Labels'
IF EXISTS (SELECT 1 FROM [dbo].[Activities] WHERE [Uid] = 'A93B2FCD-78E6-4A8D-B123-56C7E1F9D4B2')
BEGIN
    UPDATE [dbo].[Activities]
    SET [IsDeleted] = 0,
        [ProjectTypeId] = 3,
        [Name] = 'Gravadoras | Record Labels',
        [DisplayOrder] = 1,
        [CreateDate] = '2023-12-26 18:55:43.7766667 +00:00',
        [CreateUserId] = 1,
        [UpdateDate] = '2023-12-26 18:55:43.7766667 +00:00',
        [UpdateUserId] = 1,
        [HasAdditionalInfo] = 0
    WHERE [Uid] = 'A93B2FCD-78E6-4A8D-B123-56C7E1F9D4B2';
END
ELSE
BEGIN
    INSERT INTO [dbo].[Activities]
               ([Uid], [ProjectTypeId], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [HasAdditionalInfo])
         VALUES
               ('A93B2FCD-78E6-4A8D-B123-56C7E1F9D4B2', 3, 'Gravadoras | Record Labels', 1, 0, '2023-12-26 18:55:43.7766667 +00:00', 1, '2023-12-26 18:55:43.7766667 +00:00', 1, 0);
END

-- 2. Check and update/insert for 'Agregadoras | Aggregators'
IF EXISTS (SELECT 1 FROM [dbo].[Activities] WHERE [Uid] = '525C166D-0CAC-4F11-831E-974381DDAE49')
BEGIN
    UPDATE [dbo].[Activities]
    SET [IsDeleted] = 0,
        [ProjectTypeId] = 3,
        [Name] = 'Agregadoras | Aggregators',
        [DisplayOrder] = 2,
        [CreateDate] = '2023-12-26 18:55:43.7766667 +00:00',
        [CreateUserId] = 1,
        [UpdateDate] = '2023-12-26 18:55:43.7766667 +00:00',
        [UpdateUserId] = 1,
        [HasAdditionalInfo] = 0
    WHERE [Uid] = '525C166D-0CAC-4F11-831E-974381DDAE49';
END
ELSE
BEGIN
    INSERT INTO [dbo].[Activities]
               ([Uid], [ProjectTypeId], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [HasAdditionalInfo])
         VALUES
               ('525C166D-0CAC-4F11-831E-974381DDAE49', 3, 'Agregadoras | Aggregators', 2, 0, '2023-12-26 18:55:43.7766667 +00:00', 1, '2023-12-26 18:55:43.7766667 +00:00', 1, 0);
END

-- 3. Check and update/insert for 'Editoras e Associações | Publishers and Associations'
IF EXISTS (SELECT 1 FROM [dbo].[Activities] WHERE [Uid] = 'E57A1FC4-62F7-4D1B-9EA6-1207B1F2C3A8')
BEGIN
    UPDATE [dbo].[Activities]
    SET [IsDeleted] = 0,
        [ProjectTypeId] = 3,
        [Name] = 'Editoras e Associações | Publishers and Associations',
        [DisplayOrder] = 3,
        [CreateDate] = '2023-12-26 18:55:43.7766667 +00:00',
        [CreateUserId] = 1,
        [UpdateDate] = '2023-12-26 18:55:43.7766667 +00:00',
        [UpdateUserId] = 1,
        [HasAdditionalInfo] = 0
    WHERE [Uid] = 'E57A1FC4-62F7-4D1B-9EA6-1207B1F2C3A8';
END
ELSE
BEGIN
    INSERT INTO [dbo].[Activities]
               ([Uid], [ProjectTypeId], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [HasAdditionalInfo])
         VALUES
               ('E57A1FC4-62F7-4D1B-9EA6-1207B1F2C3A8', 3, 'Editoras e Associações | Publishers and Associations', 3, 0, '2023-12-26 18:55:43.7766667 +00:00', 1, '2023-12-26 18:55:43.7766667 +00:00', 1, 0);
END

-- 4. Check and update/insert for 'Selo | Label'
IF EXISTS (SELECT 1 FROM [dbo].[Activities] WHERE [Uid] = '6007DE3E-2DA9-4115-8727-419524F2F11E')
BEGIN
    UPDATE [dbo].[Activities]
    SET [IsDeleted] = 0,
        [ProjectTypeId] = 3,
        [Name] = 'Selo | Label',
        [DisplayOrder] = 4,
        [CreateDate] = '2023-12-26 18:55:43.7766667 +00:00',
        [CreateUserId] = 1,
        [UpdateDate] = '2023-12-26 18:55:43.7766667 +00:00',
        [UpdateUserId] = 1,
        [HasAdditionalInfo] = 0
    WHERE [Uid] = '6007DE3E-2DA9-4115-8727-419524F2F11E';
END
ELSE
BEGIN
    INSERT INTO [dbo].[Activities]
               ([Uid], [ProjectTypeId], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [HasAdditionalInfo])
         VALUES
               ('6007DE3E-2DA9-4115-8727-419524F2F11E', 3, 'Selo | Label', 4, 0, '2023-12-26 18:55:43.7766667 +00:00', 1, '2023-12-26 18:55:43.7766667 +00:00', 1, 0);
END

-- 5. Check and update/insert for 'Palco (Festivais e Casas de Show) | Stage (Festivals and Concert Venues)'
IF EXISTS (SELECT 1 FROM [dbo].[Activities] WHERE [Uid] = 'E47E5CFA-5082-48DE-B995-AC55323AEFB2')
BEGIN
    UPDATE [dbo].[Activities]
    SET [IsDeleted] = 0,
        [ProjectTypeId] = 3,
        [Name] = 'Palco (Festivais e Casas de Show) | Stage (Festivals and Concert Venues)',
        [DisplayOrder] = 5,
        [CreateDate] = '2023-12-26 18:55:43.7766667 +00:00',
        [CreateUserId] = 1,
        [UpdateDate] = '2023-12-26 18:55:43.7766667 +00:00',
        [UpdateUserId] = 1,
        [HasAdditionalInfo] = 0
    WHERE [Uid] = 'E47E5CFA-5082-48DE-B995-AC55323AEFB2';
END
ELSE
BEGIN
    INSERT INTO [dbo].[Activities]
               ([Uid], [ProjectTypeId], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [HasAdditionalInfo])
         VALUES
               ('E47E5CFA-5082-48DE-B995-AC55323AEFB2', 3, 'Palco (Festivais e Casas de Show) | Stage (Festivals and Concert Venues)', 5, 0, '2023-12-26 18:55:43.7766667 +00:00', 1, '2023-12-26 18:55:43.7766667 +00:00', 1, 0);
END


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
