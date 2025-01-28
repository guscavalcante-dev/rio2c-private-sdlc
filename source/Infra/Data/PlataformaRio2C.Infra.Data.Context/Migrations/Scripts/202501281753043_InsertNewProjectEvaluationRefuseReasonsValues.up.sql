BEGIN TRY
	BEGIN TRANSACTION	
		
	INSERT INTO [dbo].[ProjectEvaluationRefuseReasons]
           ([Uid]
           ,[Name]
           ,[HasAdditionalInfo]
           ,[DisplayOrder]
           ,[IsDeleted]
           ,[CreateDate]
           ,[CreateUserId]
           ,[UpdateDate]
           ,[UpdateUserId]
           ,[ProjectTypeId])
		VALUES
			('A5820D27-2B67-4EF2-8D23-94EFA9563ED5', N'O projeto tem potencial, mas no momento não cabe nos parâmetros de nossa programação. | The project has potential, but doesn’t fit into our current program.', 0, 1, 0, '2019-12-10 15:07:13.1166667 +00:00', 1, '2019-12-10 15:07:13.1166667 +00:00', 1, 3),
			('2F7A0B9F-C88C-4186-8226-618AC085B784', N'O projeto tem potencial, mas precisa de ser mais desenvolvido. | The project has potential, but needs to be better developed.', 0, 2, 0, '2019-12-10 15:07:13.1333333 +00:00', 1, '2019-12-10 15:07:13.1333333 +00:00', 1, 3),
			('7F8D9FF8-3337-4DFB-91A6-35697B188CED', N'O projeto não está de acordo com nossa proposta de curadoria. | The project doesn’t meet our curatorship needs.', 0, 3, 0, '2019-12-10 15:07:13.1333333 +00:00', 1, '2019-12-10 15:07:13.1333333 +00:00', 1, 3),
			('5EF644C4-CA26-4FE9-B54F-5E16C3664B62', N'O projeto foge aos interesses solicitados por nós. | The project doesn’t meet the interests requested by us.', 0, 4, 0, '2019-12-10 15:07:13.1366667 +00:00', 1, '2019-12-10 15:07:13.1366667 +00:00', 1, 3),
			('49CA896C-B013-4807-BC87-EA098C6C38BA', N'O projeto ainda está em estágio muito inicial, difícil de avaliar. | The project is still in a very early stage, difficult to evaluate.', 0, 5, 0, '2019-12-10 15:07:13.1400000 +00:00', 1, '2019-12-10 15:07:13.1400000 +00:00', 1, 3),
			('B144050F-BF2A-47FA-89CF-27665334BCFD', N'Outros. | Other.', 1, 6, 0, '2019-12-10 15:07:13.1433333 +00:00', 1, '2019-12-10 15:07:13.1433333 +00:00', 1, 3);

	-- Commands End
	COMMIT TRAN -- Transaction Success!
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN -- RollBack in case of Error

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
