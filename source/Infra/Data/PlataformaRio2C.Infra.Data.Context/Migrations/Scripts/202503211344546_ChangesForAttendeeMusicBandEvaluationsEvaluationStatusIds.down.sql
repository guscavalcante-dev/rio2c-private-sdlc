BEGIN TRY
    BEGIN TRANSACTION
        -- Passo 1: Remover os índices recriados
        DROP INDEX IDX_AttendeeMusicBandEvaluations_CommissionEvaluationStatusId ON AttendeeMusicBandEvaluations;
        DROP INDEX IDX_AttendeeMusicBandEvaluations_CuratorEvaluationStatusId ON AttendeeMusicBandEvaluations;
        DROP INDEX IDX_AttendeeMusicBandEvaluations_PopularEvaluationStatusId ON AttendeeMusicBandEvaluations;
        DROP INDEX IDX_AttendeeMusicBandEvaluations_RepechageEvaluationStatusId ON AttendeeMusicBandEvaluations;

        -- Passo 2: Alterar as colunas para permitir valores NULL novamente
        ALTER TABLE AttendeeMusicBandEvaluations ALTER COLUMN CommissionEvaluationStatusId INT NULL;
        ALTER TABLE AttendeeMusicBandEvaluations ALTER COLUMN CuratorEvaluationStatusId INT NULL;
        ALTER TABLE AttendeeMusicBandEvaluations ALTER COLUMN PopularEvaluationStatusId INT NULL;
        ALTER TABLE AttendeeMusicBandEvaluations ALTER COLUMN RepechageEvaluationStatusId INT NULL;

        -- Passo 3: Recriar os índices removidos
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
