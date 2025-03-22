BEGIN TRY
    BEGIN TRANSACTION
        -- Passo 1: Remover os índices dependentes
        DROP INDEX IDX_AttendeeMusicBandEvaluations_CommissionEvaluationStatusId ON AttendeeMusicBandEvaluations;
        DROP INDEX IDX_AttendeeMusicBandEvaluations_CuratorEvaluationStatusId ON AttendeeMusicBandEvaluations;
        DROP INDEX IDX_AttendeeMusicBandEvaluations_PopularEvaluationStatusId ON AttendeeMusicBandEvaluations;
        DROP INDEX IDX_AttendeeMusicBandEvaluations_RepechageEvaluationStatusId ON AttendeeMusicBandEvaluations;

        -- Passo 2: Atualizar valores NULL para evitar erro ao alterar para NOT NULL
        EXEC('UPDATE AttendeeMusicBandEvaluations SET CommissionEvaluationStatusId = 1 WHERE CommissionEvaluationStatusId IS NULL')
        EXEC('UPDATE AttendeeMusicBandEvaluations SET CuratorEvaluationStatusId = 1 WHERE CuratorEvaluationStatusId IS NULL')
        EXEC('UPDATE AttendeeMusicBandEvaluations SET PopularEvaluationStatusId = 1 WHERE PopularEvaluationStatusId IS NULL')
        EXEC('UPDATE AttendeeMusicBandEvaluations SET RepechageEvaluationStatusId = 1 WHERE RepechageEvaluationStatusId IS NULL')

        -- Passo 3: Alterar as colunas para NOT NULL
        ALTER TABLE AttendeeMusicBandEvaluations ALTER COLUMN CommissionEvaluationStatusId INT NOT NULL;
        ALTER TABLE AttendeeMusicBandEvaluations ALTER COLUMN CuratorEvaluationStatusId INT NOT NULL;
        ALTER TABLE AttendeeMusicBandEvaluations ALTER COLUMN PopularEvaluationStatusId INT NOT NULL;
        ALTER TABLE AttendeeMusicBandEvaluations ALTER COLUMN RepechageEvaluationStatusId INT NOT NULL;

        -- Passo 4: Recriar os índices removidos
        CREATE INDEX IDX_AttendeeMusicBandEvaluations_CommissionEvaluationStatusId 
        ON AttendeeMusicBandEvaluations (CommissionEvaluationStatusId);

        CREATE INDEX IDX_AttendeeMusicBandEvaluations_CuratorEvaluationStatusId 
        ON AttendeeMusicBandEvaluations (CuratorEvaluationStatusId);

        CREATE INDEX IDX_AttendeeMusicBandEvaluations_PopularEvaluationStatusId 
        ON AttendeeMusicBandEvaluations (PopularEvaluationStatusId);

        CREATE INDEX IDX_AttendeeMusicBandEvaluations_RepechageEvaluationStatusId 
        ON AttendeeMusicBandEvaluations (RepechageEvaluationStatusId);
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
