BEGIN TRY
	BEGIN TRANSACTION
		---------------------------------------
		-- InterestGroups
		---------------------------------------
		UPDATE dbo.InterestGroups
			SET Name=N'Janelas de Exibição | Display Windows'
			WHERE Name='Plataformas | Platforms';
		
		---------------------------------------
		-- Interests - Plataformas -> Janelas de Exibição
		---------------------------------------
		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = 'f47ac10b-58cc-4372-a567-0e02b2c3d479')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('f47ac10b-58cc-4372-a567-0e02b2c3d479',4,N'Streaming',2,0,1,1,0);
		END
		
		UPDATE dbo.Interests
			SET DisplayOrder=1
			WHERE Id=21;
		
		UPDATE dbo.Interests
			SET DisplayOrder=4
			WHERE Id=23;
		
		UPDATE dbo.Interests
			SET DisplayOrder=3
			WHERE Id=24;
				
		UPDATE dbo.Interests
			SET DisplayOrder=0,IsDeleted=1
			WHERE Id=22;
		
		UPDATE dbo.Interests
			SET DisplayOrder=0,IsDeleted=1
			WHERE Id=20;

		---------------------------------------
		-- Interests - Formato
		---------------------------------------
		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '550e8400-e29b-41d4-a716-446655440000')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('550e8400-e29b-41d4-a716-446655440000',2,N'Música (Show/Performance) | Music',7,0,1,1,0);
		END
		
		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '67e55044-10b1-426f-9247-bb680e5fe0c8')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('67e55044-10b1-426f-9247-bb680e5fe0c8',2,N'Animação | Animation',1,0,1,1,0);
		END
		
		UPDATE dbo.Interests
			SET Name=N'Reality Show (competição, makeover ou expositivo) | Reality Show'
			WHERE Id=10;
		
		UPDATE dbo.Interests
			SET Name=N'Variedades (talk show, gameshow, auditório, revista eletrônica) | Variety Shows'
			WHERE Id=13;

		UPDATE dbo.Interests
			SET DisplayOrder=2
			WHERE Id=3;
		
		UPDATE dbo.Interests
			SET DisplayOrder=3
			WHERE Id=4;
		
		UPDATE dbo.Interests
			SET DisplayOrder=4
			WHERE Id=5;
		
		UPDATE dbo.Interests
			SET DisplayOrder=5
			WHERE Id=6;
		
		UPDATE dbo.Interests
			SET DisplayOrder=6
			WHERE Id=7;
		
		UPDATE dbo.Interests
			SET DisplayOrder=8
			WHERE Id=8;
		
		UPDATE dbo.Interests
			SET DisplayOrder=9
			WHERE Id=9;
		
		UPDATE dbo.Interests
			SET DisplayOrder=13
			WHERE Id=10;

		UPDATE dbo.Interests
			SET DisplayOrder=12
			WHERE Id=13;

		UPDATE dbo.Interests
			SET DisplayOrder=14
			WHERE Id=14;
		
		UPDATE dbo.Interests
			SET IsDeleted=1
			WHERE Id=55;
		
		---------------------------------------
		-- Interests - Gênero
		---------------------------------------
		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '7c9e6679-7425-40de-944b-e07fc1f90ae7')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('7c9e6679-7425-40de-944b-e07fc1f90ae7',3,N'Ação | Action',1,0,1,1,0);
		END
		
		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '6ba7b810-9dad-11d1-80b4-00c04fd430c8')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('6ba7b810-9dad-11d1-80b4-00c04fd430c8',3,N'Aventura | Adventure',2,0,1,1,0);
		END
		
		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '6ba7b811-9dad-11d1-80b4-00c04fd430c8')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('6ba7b811-9dad-11d1-80b4-00c04fd430c8',3,N'Comédia | Comedy',3,0,1,1,0);
		END
		
		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '6ba7b812-9dad-11d1-80b4-00c04fd430c8')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('6ba7b812-9dad-11d1-80b4-00c04fd430c8',3,N'Drama | Drama',5,0,1,1,0);
		END
		
		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '6ba7b813-9dad-11d1-80b4-00c04fd430c8')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('6ba7b813-9dad-11d1-80b4-00c04fd430c8',3,N'Fantasia | Fantasy',7,0,1,1,0);
		END
		
		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '6ba7b814-9dad-11d1-80b4-00c04fd430c8')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('6ba7b814-9dad-11d1-80b4-00c04fd430c8',3,N'Romance | Romance',9,0,1,1,0);
		END
		
		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '6ba7b815-9dad-11d1-80b4-00c04fd430c8')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('6ba7b815-9dad-11d1-80b4-00c04fd430c8',3,N'Terror | Horror',10,0,1,1,0);
		END

		UPDATE dbo.Interests
			SET Name=N'Documentário | Documentary',DisplayOrder=4
			WHERE Id=16;
		
		UPDATE dbo.Interests
			SET Name=N'Ficção Científica | Science Fiction',DisplayOrder=8
			WHERE Id=17;

		UPDATE dbo.Interests
			SET DisplayOrder=0
			WHERE Id=56;
				
		UPDATE dbo.Interests
			SET DisplayOrder=6
			WHERE Id=75;

		UPDATE dbo.Interests
			SET IsDeleted=1,DisplayOrder=0
			WHERE Id=73;
		
		UPDATE dbo.Interests
			SET IsDeleted=1,DisplayOrder=0
			WHERE Id=74;
				
		UPDATE dbo.Interests
			SET IsDeleted=1,DisplayOrder=0
			WHERE Id=18;
		
		---------------------------------------
		-- Interests - Subgênero
		---------------------------------------
				IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '6ba7b816-9dad-11d1-80b4-00c04fd430c8')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('6ba7b816-9dad-11d1-80b4-00c04fd430c8',7,N'Anime',1,0,1,1,0);
		END

		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '6ba7b817-9dad-11d1-80b4-00c04fd430c8')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('6ba7b817-9dad-11d1-80b4-00c04fd430c8',7,N'Distopia',5,0,1,1,0);
		END

		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '6ba7b818-9dad-11d1-80b4-00c04fd430c8')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('6ba7b818-9dad-11d1-80b4-00c04fd430c8',7,N'Espionagem',7,0,1,1,0);
		END

		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '6ba7b819-9dad-11d1-80b4-00c04fd430c8')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('6ba7b819-9dad-11d1-80b4-00c04fd430c8',7,N'Ficção Hitórica | Historical Fiction',10,0,1,1,0);
		END

		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '6ba7b820-9dad-11d1-80b4-00c04fd430c8')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('6ba7b820-9dad-11d1-80b4-00c04fd430c8',7,N'História e Cultura | History and Culture',12,0,1,1,0);
		END

		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '6ba7b821-9dad-11d1-80b4-00c04fd430c8')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('6ba7b821-9dad-11d1-80b4-00c04fd430c8',7,N'Humor',13,0,1,1,0);
		END

		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '6ba7b822-9dad-11d1-80b4-00c04fd430c8')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('6ba7b822-9dad-11d1-80b4-00c04fd430c8',7,N'Investigativo | Investigative',14,0,1,1,0);
		END

		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '6ba7b823-9dad-11d1-80b4-00c04fd430c8')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('6ba7b823-9dad-11d1-80b4-00c04fd430c8',7,N'Mockumentary',15,0,1,1,0);
		END

		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '6ba7b824-9dad-11d1-80b4-00c04fd430c8')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('6ba7b824-9dad-11d1-80b4-00c04fd430c8',7,N'Natureza e vida selvagem | Nature and wildlife',17,0,1,1,0);
		END

		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '6ba7b825-9dad-11d1-80b4-00c04fd430c8')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('6ba7b825-9dad-11d1-80b4-00c04fd430c8',7,N'Psicológico | Psychological',19,0,1,1,0);
		END

		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '6ba7b826-9dad-11d1-80b4-00c04fd430c8')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('6ba7b826-9dad-11d1-80b4-00c04fd430c8',7,N'Religião e Espiritualidade | Religion and Spirituality',20,0,1,1,0);
		END

		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '6ba7b827-9dad-11d1-80b4-00c04fd430c8')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('6ba7b827-9dad-11d1-80b4-00c04fd430c8',7,N'Saúde e bem-estar | Health and well-being',21,0,1,1,0);
		END

		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '6ba7b828-9dad-11d1-80b4-00c04fd430c8')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('6ba7b828-9dad-11d1-80b4-00c04fd430c8',7,N'Sobrenatural | Supernatural',22,0,1,1,0);
		END

		IF NOT EXISTS (SELECT 1 FROM dbo.Interests WHERE Uid = '6ba7b829-9dad-11d1-80b4-00c04fd430c8')
		BEGIN
			INSERT INTO dbo.Interests (Uid,InterestGroupId,Name,DisplayOrder,IsDeleted,CreateUserId,UpdateUserId,HasAdditionalInfo)
				VALUES ('6ba7b829-9dad-11d1-80b4-00c04fd430c8',7,N'Super-herói | Superhero',23,0,1,1,0);
		END
		
		UPDATE dbo.Interests
			SET Name=N'Educação | Educational'
			WHERE Id=39;
		
		UPDATE dbo.Interests
			SET Name=N'Músical | Musical',DisplayOrder=16
			WHERE Id=48;

		UPDATE dbo.Interests
			SET Name=N'Policial/Criminal | Cop/Criminal',DisplayOrder=18
			WHERE Id=49;

		UPDATE dbo.Interests
			SET DisplayOrder=2
			WHERE Id=36;

		UPDATE dbo.Interests
			SET DisplayOrder=3
			WHERE Id=38;

		UPDATE dbo.Interests
			SET DisplayOrder=8
			WHERE Id=43;

		UPDATE dbo.Interests
			SET DisplayOrder=9
			WHERE Id=45;

		UPDATE dbo.Interests
			SET DisplayOrder=11
			WHERE Id=47;

		UPDATE dbo.Interests
			SET DisplayOrder=0
			WHERE Id=57;

		UPDATE dbo.Interests
			SET DisplayOrder=24
			WHERE Id=53;

		UPDATE dbo.Interests
			SET DisplayOrder=0
			WHERE Id=58;

		UPDATE dbo.Interests
			SET IsDeleted=0,DisplayOrder=4
			WHERE Id=59;

		UPDATE dbo.Interests
			SET IsDeleted=1,DisplayOrder=0
			WHERE Id=35;

		UPDATE dbo.Interests
			SET IsDeleted=1,DisplayOrder=0
			WHERE Id=15;

		UPDATE dbo.Interests
			SET IsDeleted=1,DisplayOrder=0
			WHERE Id=37;
		
		UPDATE dbo.Interests
			SET IsDeleted=1,DisplayOrder=0
			WHERE Id=40;

		UPDATE dbo.Interests
			SET IsDeleted=1,DisplayOrder=0
			WHERE Id=41;

		UPDATE dbo.Interests
			SET IsDeleted=1,DisplayOrder=0
			WHERE Id=42;

		UPDATE dbo.Interests
			SET IsDeleted=1,DisplayOrder=0
			WHERE Id=44;

		UPDATE dbo.Interests
			SET IsDeleted=1,DisplayOrder=0
			WHERE Id=46;

		UPDATE dbo.Interests
			SET IsDeleted=1,DisplayOrder=0
			WHERE Id=50;

		UPDATE dbo.Interests
			SET IsDeleted=1,DisplayOrder=0
			WHERE Id=19;

		UPDATE dbo.Interests
			SET IsDeleted=1,DisplayOrder=0
			WHERE Id=51;

		UPDATE dbo.Interests
			SET IsDeleted=1,DisplayOrder=0
			WHERE Id=52;

		UPDATE Interests set CreateDate = GETDATE(), UpdateDate = GETDATE() where CreateDate is null or UpdateDate is null
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