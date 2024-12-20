BEGIN TRY
    BEGIN TRANSACTION

        -----------------------------------------------
        -- Revert Interests Entries
        -----------------------------------------------
        DELETE FROM [dbo].[Interests]
        WHERE [Uid] IN (
            'F7A9C3D2-85D7-4EEC-8D26-8F7F9382346B',
            'BC3D6F92-9B3E-4A92-9F32-AF74A7B0D832',
            'AE42C3A8-4734-4A5D-8C2B-CB39AB3DC6E9',
            'A56EEDB0-234D-4732-B9D9-C9D1B7037C68' 
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
        -- Revert OTHERS Interest 'IsDeleted' Update
        -----------------------------------------------
        UPDATE [dbo].[Interests]
        SET [IsDeleted] = 0
        WHERE [Uid] = 'E4D8195E-2A7F-48AC-85C2-065B6E4101E4';

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
        -- Revert IsDeleted Status for TargetAudiences Records
        -----------------------------------------------
        UPDATE [dbo].[TargetAudiences]
        SET [IsDeleted] = 0
        WHERE [Uid] IN (
            'A92A28DD-3732-4288-A092-55E4A0AED26A', -- Empresário ou Agenciamento de Artistas
            '8C35D600-7B9D-4F85-8990-CCF955C8C45C', -- Marketing Musical
            '30044E05-1F56-4CA7-B253-2CADD9407196', -- Compositor
            '9743EFB8-3994-47E6-9BF4-1A8E82CF23B7', -- Criadores de Podcast
            'B40A3592-BA93-44B0-A710-2702DB4F7C39', -- Agências
            '597CF20B-5774-4229-B584-5CE44B818E94'  -- Outros
        );
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

        -----------------------------------------------
        -- Update IsDeleted to 1 for Activities instead of deleting
        -----------------------------------------------
        UPDATE [dbo].[Activities]
        SET [IsDeleted] = 1
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
