BEGIN TRY
	BEGIN TRANSACTION	

	INSERT INTO [dbo].[ProjectEvaluationRefuseReasons] (
		[Uid], 
		[Name], 
		[HasAdditionalInfo], 
		[DisplayOrder], 
		[IsDeleted], 
		[CreateDate], 
		[CreateUserId], 
		[UpdateDate], 
		[UpdateUserId], 
		[ProjectTypeId]
	)
	SELECT 
		'A5820D27-2B67-4EF2-8D23-94EFA9563ED5', 
		N'O projeto tem potencial, mas no momento não cabe nos parâmetros de nossa programação. 
		  | The project has potential, but doesn’t fit into our current program.', 
		0, 
		1, 
		0, 
		GETDATE(), 
		1, 
		GETDATE(), 
		1, 
		3
	WHERE NOT EXISTS (
		SELECT 1 
		FROM [dbo].[ProjectEvaluationRefuseReasons] 
		WHERE [Uid] = 'A5820D27-2B67-4EF2-8D23-94EFA9563ED5'
	);

	INSERT INTO [dbo].[ProjectEvaluationRefuseReasons] (
		[Uid], 
		[Name], 
		[HasAdditionalInfo], 
		[DisplayOrder], 
		[IsDeleted], 
		[CreateDate], 
		[CreateUserId], 
		[UpdateDate], 
		[UpdateUserId], 
		[ProjectTypeId]
	)
	SELECT 
		'2F7A0B9F-C88C-4186-8226-618AC085B784', 
		N'O projeto tem potencial, mas precisa de ser mais desenvolvido. 
		  | The project has potential, but needs to be better developed.', 
		0, 
		2, 
		0, 
		GETDATE(), 
		1, 
		GETDATE(), 
		1, 
		3
	WHERE NOT EXISTS (
		SELECT 1 
		FROM [dbo].[ProjectEvaluationRefuseReasons] 
		WHERE [Uid] = '2F7A0B9F-C88C-4186-8226-618AC085B784'
	);

	INSERT INTO [dbo].[ProjectEvaluationRefuseReasons] (
		[Uid], 
		[Name], 
		[HasAdditionalInfo], 
		[DisplayOrder], 
		[IsDeleted], 
		[CreateDate], 
		[CreateUserId], 
		[UpdateDate], 
		[UpdateUserId], 
		[ProjectTypeId]
	)
	SELECT 
		'7F8D9FF8-3337-4DFB-91A6-35697B188CED', 
		N'O projeto não está de acordo com nossa proposta de curadoria. 
		  | The project doesn’t meet our curatorship needs.', 
		0, 
		3, 
		0, 
		GETDATE(), 
		1, 
		GETDATE(), 
		1, 
		3
	WHERE NOT EXISTS (
		SELECT 1 
		FROM [dbo].[ProjectEvaluationRefuseReasons] 
		WHERE [Uid] = '7F8D9FF8-3337-4DFB-91A6-35697B188CED'
	);

	INSERT INTO [dbo].[ProjectEvaluationRefuseReasons] (
		[Uid], 
		[Name], 
		[HasAdditionalInfo], 
		[DisplayOrder], 
		[IsDeleted], 
		[CreateDate], 
		[CreateUserId], 
		[UpdateDate], 
		[UpdateUserId], 
		[ProjectTypeId]
	)
	SELECT 
		'5EF644C4-CA26-4FE9-B54F-5E16C3664B62', 
		N'O projeto foge aos interesses solicitados por nós. 
		  | The project doesn’t meet the interests requested by us.', 
		0, 
		4, 
		0, 
		GETDATE(), 
		1, 
		GETDATE(), 
		1, 
		3
	WHERE NOT EXISTS (
		SELECT 1 
		FROM [dbo].[ProjectEvaluationRefuseReasons] 
		WHERE [Uid] = '5EF644C4-CA26-4FE9-B54F-5E16C3664B62'
	);

	INSERT INTO [dbo].[ProjectEvaluationRefuseReasons] (
		[Uid], 
		[Name], 
		[HasAdditionalInfo], 
		[DisplayOrder], 
		[IsDeleted], 
		[CreateDate], 
		[CreateUserId], 
		[UpdateDate], 
		[UpdateUserId], 
		[ProjectTypeId]
	)
	SELECT 
		'49CA896C-B013-4807-BC87-EA098C6C38BA', 
		N'O projeto ainda está em estágio muito inicial, difícil de avaliar. 
		  | The project is still in a very early stage, difficult to evaluate.', 
		0, 
		5, 
		0, 
		GETDATE(), 
		1, 
		GETDATE(), 
		1, 
		3
	WHERE NOT EXISTS (
		SELECT 1 
		FROM [dbo].[ProjectEvaluationRefuseReasons] 
		WHERE [Uid] = '49CA896C-B013-4807-BC87-EA098C6C38BA'
	);

	INSERT INTO [dbo].[ProjectEvaluationRefuseReasons] (
		[Uid], 
		[Name], 
		[HasAdditionalInfo], 
		[DisplayOrder], 
		[IsDeleted], 
		[CreateDate], 
		[CreateUserId], 
		[UpdateDate], 
		[UpdateUserId], 
		[ProjectTypeId]
	)
	SELECT 
		'B144050F-BF2A-47FA-89CF-27665334BCFD', 
		N'Outros. | Other.', 
		1, 
		6, 
		0, 
		GETDATE(), 
		1, 
		GETDATE(), 
		1, 
		3
	WHERE NOT EXISTS (
		SELECT 1 
		FROM [dbo].[ProjectEvaluationRefuseReasons] 
		WHERE [Uid] = 'B144050F-BF2A-47FA-89CF-27665334BCFD'
	);

	-- Commit transaction if everything is successful
	COMMIT TRAN 
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN -- Rollback in case of error

	-- Raise ERROR with details
	DECLARE @ErrorLine INT;
	DECLARE @ErrorMessage NVARCHAR(4000);
	DECLARE @ErrorSeverity INT;
	DECLARE @ErrorState INT;

	SELECT
		@ErrorLine = ERROR_LINE(),
		@ErrorMessage = ERROR_MESSAGE(),
		@ErrorSeverity = ERROR_SEVERITY(),
		@ErrorState = ERROR_STATE();
		 
	RAISERROR (
		'Error found in line %i: %s', 
		@ErrorSeverity, 
		@ErrorState, 
		@ErrorLine, 
		@ErrorMessage
	) WITH SETERROR;
END CATCH;
