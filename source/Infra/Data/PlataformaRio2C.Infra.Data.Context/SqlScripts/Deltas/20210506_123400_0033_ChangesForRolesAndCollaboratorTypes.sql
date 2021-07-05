--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION
		
		--Changes for Roles
		ALTER TABLE "dbo"."Roles"
		ADD Description  varchar(256)  NULL
		;

		EXEC('UPDATE dbo."Roles" set Description = ''Administrador | Manager'' where Id = 1')
		;
		EXEC('UPDATE dbo."Roles" set Description = ''Administrador Parcial | Partial Manager'' where Id = 2')
		;
		EXEC('UPDATE dbo."Roles" set Description = ''Usuário | User'' where Id = 3')
		;

		ALTER TABLE "dbo"."Roles"
		ALTER COLUMN Description  varchar(256)  NOT NULL
		;

		--Changes for CollaboratorTypes
		ALTER TABLE "dbo"."CollaboratorTypes"
		ADD Description  varchar(256)  NULL
		;

		EXEC('UPDATE dbo."CollaboratorTypes" set Description = ''Administrador Audiovisual | Audiovisual Manager'' where Id = 100')
		;
		EXEC('UPDATE dbo."CollaboratorTypes" set Description = ''Administrador Logistica | Logistic Manager'' where Id = 101')
		;
		EXEC('UPDATE dbo."CollaboratorTypes" set Description = ''Curadoria Audiovisual | Audiovisual Curatorship'' where Id = 110')
		;
		EXEC('UPDATE dbo."CollaboratorTypes" set Description = ''Curadoria Musica | Music Curatorship'' where Id = 111')
		;
		EXEC('UPDATE dbo."CollaboratorTypes" set Description = ''Curadoria Inovação | Innovation Curatorship'' where Id = 112')
		;
		EXEC('UPDATE dbo."CollaboratorTypes" set Description = ''Executivo Audiovisual | Audiovisual Executive'' where Id = 200')
		;
		EXEC('UPDATE dbo."CollaboratorTypes" set Description = ''Executivo Música | Music Executive'' where Id = 201')
		;
		EXEC('UPDATE dbo."CollaboratorTypes" set Description = ''Executivo Inovação | Innovation Executive'' where Id = 202')
		;
		EXEC('UPDATE dbo."CollaboratorTypes" set Description = ''Comissão Audiovisual | Audiovisual Commission'' where Id = 300')
		;
		EXEC('UPDATE dbo."CollaboratorTypes" set Description = ''Comissão Música | Music Commission'' where Id = 301')
		;
		EXEC('UPDATE dbo."CollaboratorTypes" set Description = ''Comissão Inovação | Innovation Commission'' where Id = 302')
		;
		EXEC('UPDATE dbo."CollaboratorTypes" set Description = ''Palestrante | Speaker'' where Id = 400')
		;
		EXEC('UPDATE dbo."CollaboratorTypes" set Description = ''Indústria | Industry'' where Id = 500')
		;
		EXEC('UPDATE dbo."CollaboratorTypes" set Description = ''Produtor | Creator'' where Id = 501')
		;
		EXEC('UPDATE dbo."CollaboratorTypes" set Description = ''Summit'' where Id = 502')
		;
		EXEC('UPDATE dbo."CollaboratorTypes" set Description = ''Festvalia'' where Id = 503')
		;
		EXEC('UPDATE dbo."CollaboratorTypes" set Description = ''Musica | Music'' where Id = 601')
		;
		EXEC('UPDATE dbo."CollaboratorTypes" set Description = ''Inovação | Innovation'' where Id = 602')
		;

		ALTER TABLE "dbo"."CollaboratorTypes"
		ALTER COLUMN Description  varchar(256)  NOT NULL
		;

	-- Commands End
	COMMIT TRAN -- Transaction Success!
END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0
		ROLLBACK TRAN --RollBack in case of Error

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
