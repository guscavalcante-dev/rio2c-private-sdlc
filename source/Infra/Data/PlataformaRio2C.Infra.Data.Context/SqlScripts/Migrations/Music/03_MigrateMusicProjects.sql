IF EXISTS (SELECT 1 FROM sysobjects WHERE  id = object_id(N'[dbo].[MigrateMusicProjects]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    DROP PROCEDURE [dbo].[MigrateMusicProjects]
END
GO

CREATE PROCEDURE [dbo].[MigrateMusicProjects]
AS 
BEGIN
	/*
	// ***********************************************************************
	// Assembly         : Database
	// Author           : Rafael Dantas Ruiz
	// Created          : 02-26-2020
	//
	// Last Modified By : Rafael Dantas Ruiz
	// Last Modified On : 02-28-2020
	// ***********************************************************************
	// <copyright file="MigrateMusicProjects.sql" company="Softo">
	//     Copyright (c) Softo. All rights reserved.
	// </copyright>
	// <summary>
	//    v0.4 - Fix IsProcessed update for registers without projects 2 and 3
	// </summary>
	// ***********************************************************************
	*/
	-- Main properties
	DECLARE @EditionId int;
	SELECT @EditionId = [Id] FROM dbo.[Editions] WHERE [UrlCode] = '2020';

	DECLARE @CollaboratorTypeId int;
	SELECT @CollaboratorTypeId = [Id] FROM dbo.[CollaboratorTypes] WHERE [Name] = 'Music';

	DECLARE @ProjectEvaluationStatusId int;
	SELECT @ProjectEvaluationStatusId = [Id] FROM dbo.[ProjectEvaluationStatuses] WHERE [Code] = 'UnderEvaluation';

	-- Control properties
	DECLARE @UserId int;
	DECLARE @CollaboratorId int;
	DECLARE @AttendeeCollaboratorId int;
	DECLARE @AttendeeCollaboratorTypeId int;

	DECLARE @MusicBandId int;
	DECLARE @MusicBandTypeId int;
	DECLARE @AttendeeMusicBandId int;
	DECLARE @AttendeeMusicBandCollaboratorId int;
	DECLARE @MusicProjectId int;
	DECLARE @MusicGenreId int;
	DECLARE @MusicBandGenreId int;
	DECLARE @MusicBandGenre nvarchar(max);
	DECLARE @TargetAudienceId int;
	DECLARE @MusicBandTargetAudienceId int;
	DECLARE @ReleasedMusicProjectId int;
	DECLARE @MusicBandMemberId int;
	DECLARE @MusicBandTeamMemberId int;

	-- MySql properties
	DECLARE @Id int;
	DECLARE @Nome nvarchar(max);
	DECLARE @CPFCNPJ nvarchar(max);
	DECLARE @Endereco nvarchar(max);
	DECLARE @CEP nvarchar(max);
	DECLARE @Pais nvarchar(max);
	DECLARE @Estado nvarchar(max);
	DECLARE @Cidade nvarchar(max);
	DECLARE @Telefone nvarchar(max);
	DECLARE @Celular nvarchar(max);
	DECLARE @Email nvarchar(max);
	DECLARE @Projeto1Time1Nome nvarchar(max);
	DECLARE @Projeto1Time1Funcao nvarchar(max);
	DECLARE @Projeto1Time2Nome nvarchar(max);
	DECLARE @Projeto1Time2Funcao nvarchar(max);
	DECLARE @Projeto1Time3Nome nvarchar(max);
	DECLARE @Projeto1Time3Funcao nvarchar(max);
	DECLARE @Projeto1Time4Nome nvarchar(max);
	DECLARE @Projeto1Time4Funcao nvarchar(max);
	DECLARE @Projeto1Time5Nome nvarchar(max);
	DECLARE @Projeto1Time5Funcao nvarchar(max);
	DECLARE @Projeto1Foto nvarchar(max);
	DECLARE @Projeto1Estilos nvarchar(max);
	DECLARE @Projeto1Musica1 nvarchar(max);
	DECLARE @Projeto1Musica2 nvarchar(max);
	DECLARE @Projeto1Release nvarchar(max);
	DECLARE @Projeto1Twitter nvarchar(max);
	DECLARE @Projeto1Youtube nvarchar(max);
	DECLARE @Projeto1Instagram nvarchar(max);
	DECLARE @Projeto1Release1Nome nvarchar(max);
	DECLARE @Projeto1Release1Ano nvarchar(max);
	DECLARE @Projeto1Release2Nome nvarchar(max);
	DECLARE @Projeto1Release2Ano nvarchar(max);
	DECLARE @Projeto1Release3Nome nvarchar(max);
	DECLARE @Projeto1Release3Ano nvarchar(max);
	DECLARE @Projeto1Release4Nome nvarchar(max);
	DECLARE @Projeto1Release4Ano nvarchar(max);
	DECLARE @Projeto1Release5Nome nvarchar(max);
	DECLARE @Projeto1Release5Ano nvarchar(max);
	DECLARE @Projeto1Clipping1 nvarchar(max);
	DECLARE @Projeto1Clipping2 nvarchar(max);
	DECLARE @Projeto1Clipping3 nvarchar(max);
	DECLARE @Projeto1Integrante1Nome nvarchar(max);
	DECLARE @Projeto1Integrante1Instrumento nvarchar(max);
	DECLARE @Projeto1Integrante2Nome nvarchar(max);
	DECLARE @Projeto1Integrante2Instrumento nvarchar(max);
	DECLARE @Projeto1Integrante3Nome nvarchar(max);
	DECLARE @Projeto1Integrante3Instrumento nvarchar(max);
	DECLARE @Projeto1Integrante4Nome nvarchar(max);
	DECLARE @Projeto1Integrante4Instrumento nvarchar(max);
	DECLARE @Projeto1Integrante5Nome nvarchar(max);
	DECLARE @Projeto1Integrante5Instrumento nvarchar(max);
	DECLARE @Projeto1AnoDeInicio nvarchar(max);
	DECLARE @Projeto1Influencias nvarchar(max);
	DECLARE @Projeto1VideoClip nvarchar(max);
	DECLARE @Projeto1NomeDoArtista nvarchar(max);
	DECLARE @Projeto1TipoDoArtista nvarchar(max);
	DECLARE @Projeto1PublicoAlvo nvarchar(max);
	DECLARE @Projeto1FanpageNoFacebook nvarchar(max);
	DECLARE @Projeto2Time1Nome nvarchar(max);
	DECLARE @Projeto2Time1Funcao nvarchar(max);
	DECLARE @Projeto2Time2Nome nvarchar(max);
	DECLARE @Projeto2Time2Funcao nvarchar(max);
	DECLARE @Projeto2Time3Nome nvarchar(max);
	DECLARE @Projeto2Time3Funcao nvarchar(max);
	DECLARE @Projeto2Time4Nome nvarchar(max);
	DECLARE @Projeto2Time4Funcao nvarchar(max);
	DECLARE @Projeto2Time5Nome nvarchar(max);
	DECLARE @Projeto2Time5Funcao nvarchar(max);
	DECLARE @Projeto2Foto nvarchar(max);
	DECLARE @Projeto2Estilos nvarchar(max);
	DECLARE @Projeto2Musica1 nvarchar(max);
	DECLARE @Projeto2Musica2 nvarchar(max);
	DECLARE @Projeto2Release nvarchar(max);
	DECLARE @Projeto2Twitter nvarchar(max);
	DECLARE @Projeto2Youtube nvarchar(max);
	DECLARE @Projeto2Instagram nvarchar(max);
	DECLARE @Projeto2Release1Nome nvarchar(max);
	DECLARE @Projeto2Release1Ano nvarchar(max);
	DECLARE @Projeto2Release2Nome nvarchar(max);
	DECLARE @Projeto2Release2Ano nvarchar(max);
	DECLARE @Projeto2Release3Nome nvarchar(max);
	DECLARE @Projeto2Release3Ano nvarchar(max);
	DECLARE @Projeto2Release4Nome nvarchar(max);
	DECLARE @Projeto2Release4Ano nvarchar(max);
	DECLARE @Projeto2Release5Nome nvarchar(max);
	DECLARE @Projeto2Release5Ano nvarchar(max);
	DECLARE @Projeto2Clipping1 nvarchar(max);
	DECLARE @Projeto2Clipping2 nvarchar(max);
	DECLARE @Projeto2Clipping3 nvarchar(max);
	DECLARE @Projeto2Integrante1Nome nvarchar(max);
	DECLARE @Projeto2Integrante1Instrumento nvarchar(max);
	DECLARE @Projeto2Integrante2Nome nvarchar(max);
	DECLARE @Projeto2Integrante2Instrumento nvarchar(max);
	DECLARE @Projeto2Integrante3Nome nvarchar(max);
	DECLARE @Projeto2Integrante3Instrumento nvarchar(max);
	DECLARE @Projeto2Integrante4Nome nvarchar(max);
	DECLARE @Projeto2Integrante4Instrumento nvarchar(max);
	DECLARE @Projeto2Integrante5Nome nvarchar(max);
	DECLARE @Projeto2Integrante5Instrumento nvarchar(max);
	DECLARE @Projeto2AnoDeInicio nvarchar(max);
	DECLARE @Projeto2Influencias nvarchar(max);
	DECLARE @Projeto2VideoClip nvarchar(max);
	DECLARE @Projeto2NomeDoArtista nvarchar(max);
	DECLARE @Projeto2TipoDoArtista nvarchar(max);
	DECLARE @Projeto2PublicoAlvo nvarchar(max);
	DECLARE @Projeto2FanpageNoFacebook nvarchar(max);
	DECLARE @Projeto3Time1Nome nvarchar(max);
	DECLARE @Projeto3Time1Funcao nvarchar(max);
	DECLARE @Projeto3Time2Nome nvarchar(max);
	DECLARE @Projeto3Time2Funcao nvarchar(max);
	DECLARE @Projeto3Time3Nome nvarchar(max);
	DECLARE @Projeto3Time3Funcao nvarchar(max);
	DECLARE @Projeto3Time4Nome nvarchar(max);
	DECLARE @Projeto3Time4Funcao nvarchar(max);
	DECLARE @Projeto3Time5Nome nvarchar(max);
	DECLARE @Projeto3Time5Funcao nvarchar(max);
	DECLARE @Projeto3Foto nvarchar(max);
	DECLARE @Projeto3Estilos nvarchar(max);
	DECLARE @Projeto3Musica1 nvarchar(max);
	DECLARE @Projeto3Musica2 nvarchar(max);
	DECLARE @Projeto3Release nvarchar(max);
	DECLARE @Projeto3Twitter nvarchar(max);
	DECLARE @Projeto3Youtube nvarchar(max);
	DECLARE @Projeto3Instagram nvarchar(max);
	DECLARE @Projeto3Release1Nome nvarchar(max);
	DECLARE @Projeto3Release1Ano nvarchar(max);
	DECLARE @Projeto3Release2Nome nvarchar(max);
	DECLARE @Projeto3Release2Ano nvarchar(max);
	DECLARE @Projeto3Release3Nome nvarchar(max);
	DECLARE @Projeto3Release3Ano nvarchar(max);
	DECLARE @Projeto3Release4Nome nvarchar(max);
	DECLARE @Projeto3Release4Ano nvarchar(max);
	DECLARE @Projeto3Release5Nome nvarchar(max);
	DECLARE @Projeto3Release5Ano nvarchar(max);
	DECLARE @Projeto3Clipping1 nvarchar(max);
	DECLARE @Projeto3Clipping2 nvarchar(max);
	DECLARE @Projeto3Clipping3 nvarchar(max);
	DECLARE @Projeto3Integrante1Nome nvarchar(max);
	DECLARE @Projeto3Integrante1Instrumento nvarchar(max);
	DECLARE @Projeto3Integrante2Nome nvarchar(max);
	DECLARE @Projeto3Integrante2Instrumento nvarchar(max);
	DECLARE @Projeto3Integrante3Nome nvarchar(max);
	DECLARE @Projeto3Integrante3Instrumento nvarchar(max);
	DECLARE @Projeto3Integrante4Nome nvarchar(max);
	DECLARE @Projeto3Integrante4Instrumento nvarchar(max);
	DECLARE @Projeto3Integrante5Nome nvarchar(max);
	DECLARE @Projeto3Integrante5Instrumento nvarchar(max);
	DECLARE @Projeto3AnoDeInicio nvarchar(max);
	DECLARE @Projeto3Influencias nvarchar(max);
	DECLARE @Projeto3VideoClip nvarchar(max);
	DECLARE @Projeto3NomeDoArtista nvarchar(max);
	DECLARE @Projeto3TipoDoArtista nvarchar(max);
	DECLARE @Projeto3PublicoAlvo nvarchar(max);
	DECLARE @Projeto3FanpageNoFacebook nvarchar(max);

	-- Cursor
	DECLARE pitchingShowSubmissionsCursor CURSOR local fast_forward FOR  
	SELECT 
		[Id],
		[Nome],
		[CPF CNPJ],
		[Endereco],
		[CEP],
		[Pais],
		[Estado],
		[Cidade],
		[Telefone],
		[Celular],
		[Email],
		[Projeto 1 - Time 1 - Nome],
		[Projeto 1 - Time 1 - Funcao],
		[Projeto 1 - Time 2 - Nome],
		[Projeto 1 - Time 2 - Funcao],
		[Projeto 1 - Time 3 - Nome],
		[Projeto 1 - Time 3 - Funcao],
		[Projeto 1 - Time 4 - Nome],
		[Projeto 1 - Time 4 - Funcao],
		[Projeto 1 - Time 5 - Nome],
		[Projeto 1 - Time 5 - Funcao],
		[Projeto 1 - Foto],
		[Projeto 1 - Estilos],
		[Projeto 1 - Musica 1],
		[Projeto 1 - Musica 2],
		[Projeto 1 - Release],
		[Projeto 1 - Twitter],
		[Projeto 1 - Youtube],
		[Projeto 1 - Instagram],
		[Projeto 1 - Release 1 - Nome],
		[Projeto 1 - Release 1 - Ano],
		[Projeto 1 - Release 2 - Nome],
		[Projeto 1 - Release 2 - Ano],
		[Projeto 1 - Release 3 - Nome],
		[Projeto 1 - Release 3 - Ano],
		[Projeto 1 - Release 4 - Nome],
		[Projeto 1 - Release 4 - Ano],
		[Projeto 1 - Release 5 - Nome],
		[Projeto 1 - Release 5 - Ano],
		[Projeto 1 - Clipping - 1],
		[Projeto 1 - Clipping - 2],
		[Projeto 1 - Clipping - 3],
		[Projeto 1 - Integrante 1 - Nome],
		[Projeto 1 - Integrante 1 - Instrumento],
		[Projeto 1 - Integrante 2 - Nome],
		[Projeto 1 - Integrante 2 - Instrumento],
		[Projeto 1 - Integrante 3 - Nome],
		[Projeto 1 - Integrante 3 - Instrumento],
		[Projeto 1 - Integrante 4 - Nome],
		[Projeto 1 - Integrante 4 - Instrumento],
		[Projeto 1 - Integrante 5 - Nome],
		[Projeto 1 - Integrante 5 - Instrumento],
		[Projeto 1 - Ano de Inicio],
		[Projeto 1 - Influencias],
		[Projeto 1 - Video Clip],
		[Projeto 1 - Nome do Artista],
		[Projeto 1 - Tipo do Artista],
		[Projeto 1 - Publico Alvo],
		[Projeto 1 - Fan page no Facebook],
		[Projeto 2 - Time 1 - Nome],
		[Projeto 2 - Time 1 - Funcao],
		[Projeto 2 - Time 2 - Nome],
		[Projeto 2 - Time 2 - Funcao],
		[Projeto 2 - Time 3 - Nome],
		[Projeto 2 - Time 3 - Funcao],
		[Projeto 2 - Time 4 - Nome],
		[Projeto 2 - Time 4 - Funcao],
		[Projeto 2 - Time 5 - Nome],
		[Projeto 2 - Time 5 - Funcao],
		[Projeto 2 - Foto],
		[Projeto 2 - Estilos],
		[Projeto 2 - Musica 1],
		[Projeto 2 - Musica 2],
		[Projeto 2 - Release],
		[Projeto 2 - Twitter],
		[Projeto 2 - Youtube],
		[Projeto 2 - Instagram],
		[Projeto 2 - Release 1 - Nome],
		[Projeto 2 - Release 1 - Ano],
		[Projeto 2 - Release 2 - Nome],
		[Projeto 2 - Release 2 - Ano],
		[Projeto 2 - Release 3 - Nome],
		[Projeto 2 - Release 3 - Ano],
		[Projeto 2 - Release 4 - Nome],
		[Projeto 2 - Release 4 - Ano],
		[Projeto 2 - Release 5 - Nome],
		[Projeto 2 - Release 5 - Ano],
		[Projeto 2 - Clipping - 1],
		[Projeto 2 - Clipping - 2],
		[Projeto 2 - Clipping - 3],
		[Projeto 2 - Integrante 1 - Nome],
		[Projeto 2 - Integrante 1 - Instrumento],
		[Projeto 2 - Integrante 2 - Nome],
		[Projeto 2 - Integrante 2 - Instrumento],
		[Projeto 2 - Integrante 3 - Nome],
		[Projeto 2 - Integrante 3 - Instrumento],
		[Projeto 2 - Integrante 4 - Nome],
		[Projeto 2 - Integrante 4 - Instrumento],
		[Projeto 2 - Integrante 5 - Nome],
		[Projeto 2 - Integrante 5 - Instrumento],
		[Projeto 2 - Ano de Inicio],
		[Projeto 2 - Influencias],
		[Projeto 2 - Video Clip],
		[Projeto 2 - Nome do Artista],
		[Projeto 2 - Tipo do Artista],
		[Projeto 2 - Publico Alvo],
		[Projeto 2 - Fan page no Facebook],
		[Projeto 3 - Time 1 - Nome],
		[Projeto 3 - Time 1 - Funcao],
		[Projeto 3 - Time 2 - Nome],
		[Projeto 3 - Time 2 - Funcao],
		[Projeto 3 - Time 3 - Nome],
		[Projeto 3 - Time 3 - Funcao],
		[Projeto 3 - Time 4 - Nome],
		[Projeto 3 - Time 4 - Funcao],
		[Projeto 3 - Time 5 - Nome],
		[Projeto 3 - Time 5 - Funcao],
		[Projeto 3 - Foto],
		[Projeto 3 - Estilos],
		[Projeto 3 - Musica 1],
		[Projeto 3 - Musica 2],
		[Projeto 3 - Release],
		[Projeto 3 - Twitter],
		[Projeto 3 - Youtube],
		[Projeto 3 - Instagram],
		[Projeto 3 - Release 1 - Nome],
		[Projeto 3 - Release 1 - Ano],
		[Projeto 3 - Release 2 - Nome],
		[Projeto 3 - Release 2 - Ano],
		[Projeto 3 - Release 3 - Nome],
		[Projeto 3 - Release 3 - Ano],
		[Projeto 3 - Release 4 - Nome],
		[Projeto 3 - Release 4 - Ano],
		[Projeto 3 - Release 5 - Nome],
		[Projeto 3 - Release 5 - Ano],
		[Projeto 3 - Clipping - 1],
		[Projeto 3 - Clipping - 2],
		[Projeto 3 - Clipping - 3],
		[Projeto 3 - Integrante 1 - Nome],
		[Projeto 3 - Integrante 1 - Instrumento],
		[Projeto 3 - Integrante 2 - Nome],
		[Projeto 3 - Integrante 2 - Instrumento],
		[Projeto 3 - Integrante 3 - Nome],
		[Projeto 3 - Integrante 3 - Instrumento],
		[Projeto 3 - Integrante 4 - Nome],
		[Projeto 3 - Integrante 4 - Instrumento],
		[Projeto 3 - Integrante 5 - Nome],
		[Projeto 3 - Integrante 5 - Instrumento],
		[Projeto 3 - Ano de Inicio],
		[Projeto 3 - Influencias],
		[Projeto 3 - Video Clip],
		[Projeto 3 - Nome do Artista],
		[Projeto 3 - Tipo do Artista],
		[Projeto 3 - Publico Alvo],
		[Projeto 3 - Fan page no Facebook]
	FROM 
		dbo.pitching_show_submissions pss
	WHERE 
		pss.[IsProcessed] IS NULL
	ORDER BY pss.[Id];

	OPEN pitchingShowSubmissionsCursor;

	FETCH NEXT FROM pitchingShowSubmissionsCursor 
	INTO 
		@Id,
		@Nome,
		@CPFCNPJ,
		@Endereco,
		@CEP,
		@Pais,
		@Estado,
		@Cidade,
		@Telefone,
		@Celular,
		@Email,
		@Projeto1Time1Nome,
		@Projeto1Time1Funcao,
		@Projeto1Time2Nome,
		@Projeto1Time2Funcao,
		@Projeto1Time3Nome,
		@Projeto1Time3Funcao,
		@Projeto1Time4Nome,
		@Projeto1Time4Funcao,
		@Projeto1Time5Nome,
		@Projeto1Time5Funcao,
		@Projeto1Foto,
		@Projeto1Estilos,
		@Projeto1Musica1,
		@Projeto1Musica2,
		@Projeto1Release,
		@Projeto1Twitter,
		@Projeto1Youtube,
		@Projeto1Instagram,
		@Projeto1Release1Nome,
		@Projeto1Release1Ano,
		@Projeto1Release2Nome,
		@Projeto1Release2Ano,
		@Projeto1Release3Nome,
		@Projeto1Release3Ano,
		@Projeto1Release4Nome,
		@Projeto1Release4Ano,
		@Projeto1Release5Nome,
		@Projeto1Release5Ano,
		@Projeto1Clipping1,
		@Projeto1Clipping2,
		@Projeto1Clipping3,
		@Projeto1Integrante1Nome,
		@Projeto1Integrante1Instrumento,
		@Projeto1Integrante2Nome,
		@Projeto1Integrante2Instrumento,
		@Projeto1Integrante3Nome,
		@Projeto1Integrante3Instrumento,
		@Projeto1Integrante4Nome,
		@Projeto1Integrante4Instrumento,
		@Projeto1Integrante5Nome,
		@Projeto1Integrante5Instrumento,
		@Projeto1AnoDeInicio,
		@Projeto1Influencias,
		@Projeto1VideoClip,
		@Projeto1NomeDoArtista,
		@Projeto1TipoDoArtista,
		@Projeto1PublicoAlvo,
		@Projeto1FanpageNoFacebook,
		@Projeto2Time1Nome,
		@Projeto2Time1Funcao,
		@Projeto2Time2Nome,
		@Projeto2Time2Funcao,
		@Projeto2Time3Nome,
		@Projeto2Time3Funcao,
		@Projeto2Time4Nome,
		@Projeto2Time4Funcao,
		@Projeto2Time5Nome,
		@Projeto2Time5Funcao,
		@Projeto2Foto,
		@Projeto2Estilos,
		@Projeto2Musica1,
		@Projeto2Musica2,
		@Projeto2Release,
		@Projeto2Twitter,
		@Projeto2Youtube,
		@Projeto2Instagram,
		@Projeto2Release1Nome,
		@Projeto2Release1Ano,
		@Projeto2Release2Nome,
		@Projeto2Release2Ano,
		@Projeto2Release3Nome,
		@Projeto2Release3Ano,
		@Projeto2Release4Nome,
		@Projeto2Release4Ano,
		@Projeto2Release5Nome,
		@Projeto2Release5Ano,
		@Projeto2Clipping1,
		@Projeto2Clipping2,
		@Projeto2Clipping3,
		@Projeto2Integrante1Nome,
		@Projeto2Integrante1Instrumento,
		@Projeto2Integrante2Nome,
		@Projeto2Integrante2Instrumento,
		@Projeto2Integrante3Nome,
		@Projeto2Integrante3Instrumento,
		@Projeto2Integrante4Nome,
		@Projeto2Integrante4Instrumento,
		@Projeto2Integrante5Nome,
		@Projeto2Integrante5Instrumento,
		@Projeto2AnoDeInicio,
		@Projeto2Influencias,
		@Projeto2VideoClip,
		@Projeto2NomeDoArtista,
		@Projeto2TipoDoArtista,
		@Projeto2PublicoAlvo,
		@Projeto2FanpageNoFacebook,
		@Projeto3Time1Nome,
		@Projeto3Time1Funcao,
		@Projeto3Time2Nome,
		@Projeto3Time2Funcao,
		@Projeto3Time3Nome,
		@Projeto3Time3Funcao,
		@Projeto3Time4Nome,
		@Projeto3Time4Funcao,
		@Projeto3Time5Nome,
		@Projeto3Time5Funcao,
		@Projeto3Foto,
		@Projeto3Estilos,
		@Projeto3Musica1,
		@Projeto3Musica2,
		@Projeto3Release,
		@Projeto3Twitter,
		@Projeto3Youtube,
		@Projeto3Instagram,
		@Projeto3Release1Nome,
		@Projeto3Release1Ano,
		@Projeto3Release2Nome,
		@Projeto3Release2Ano,
		@Projeto3Release3Nome,
		@Projeto3Release3Ano,
		@Projeto3Release4Nome,
		@Projeto3Release4Ano,
		@Projeto3Release5Nome,
		@Projeto3Release5Ano,
		@Projeto3Clipping1,
		@Projeto3Clipping2,
		@Projeto3Clipping3,
		@Projeto3Integrante1Nome,
		@Projeto3Integrante1Instrumento,
		@Projeto3Integrante2Nome,
		@Projeto3Integrante2Instrumento,
		@Projeto3Integrante3Nome,
		@Projeto3Integrante3Instrumento,
		@Projeto3Integrante4Nome,
		@Projeto3Integrante4Instrumento,
		@Projeto3Integrante5Nome,
		@Projeto3Integrante5Instrumento,
		@Projeto3AnoDeInicio,
		@Projeto3Influencias,
		@Projeto3VideoClip,
		@Projeto3NomeDoArtista,
		@Projeto3TipoDoArtista,
		@Projeto3PublicoAlvo,
		@Projeto3FanpageNoFacebook;

	WHILE @@FETCH_STATUS = 0   
	BEGIN
		-- Unset control variables
		SELECT @UserId = NULL;
		SELECT @CollaboratorId = NULL;
		SELECT @AttendeeCollaboratorId = NULL;
		SELECT @AttendeeCollaboratorTypeId = NULL;

		-- Validations
		BEGIN
			-- Check if the name is null
			IF (@Nome IS NULL)
			BEGIN
				UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '0001', ErrorMessage = 'The field "Nome" is null.' WHERE Id = @id;
				GOTO ERROR_NEXTFETCH;
			END;

			-- Check if the email is null
			IF (@Email IS NULL)
			BEGIN
				UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '0002', ErrorMessage = 'The field "Email" is null.' WHERE Id = @id;
				GOTO ERROR_NEXTFETCH;
			END;

			-- Check if projet 1 nome do artista is null
			IF (@Projeto1NomeDoArtista IS NULL)
			BEGIN
				UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '0003', ErrorMessage = 'The registration does not have projects.' WHERE Id = @id;
				GOTO ERROR_NEXTFETCH;
			END;
		END;

		-- User Info ------------------------------------------------------------------------------------
		BEGIN
			-- Users
			SELECT @UserId = [Id] FROM dbo.Users WHERE LOWER([Email]) = LOWER(@Email);
			IF (@UserId IS NULL)
			BEGIN
				INSERT INTO [dbo].[Users] 
					([Uid], [Active], [Name], [UserName], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [PasswordNew], [UserInterfaceLanguageId], [IsDeleted], [CreateDate], [UpdateDate]) 
				VALUES
					(NEWID(), 1, @Nome, LOWER(@Email), LOWER(@Email), 0, NULL, LOWER(NEWID()), NULL, 0, 0, NULL, 1, 0, NULL, NULL, 0, GETUTCDATE(),  GETUTCDATE());

				SELECT @UserId = [Id] FROM dbo.Users WHERE LOWER([Email]) = LOWER(@Email);
				IF (@UserId IS NULL)
				BEGIN
					UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '1001', ErrorMessage = 'The user could not be created.' WHERE Id = @id;
					GOTO ERROR_NEXTFETCH;
				END;
			END;

			-- Collaborators
			SELECT @CollaboratorId = [Id] FROM dbo.Collaborators WHERE Id = @UserId;
			IF (@CollaboratorId IS NULL)
			BEGIN
				INSERT INTO [dbo].[Collaborators] 
					([Id], [Uid], [FirstName], [LastNames], [ImageUploadDate], [PhoneNumber], [CellPhone], [Badge], [PublicEmail], [AddressId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [Website], [Linkedin], [Instagram], [Twitter], [Youtube], [BirthDate], [CollaboratorGenderId], [CollaboratorGenderAdditionalInfo], [CollaboratorRoleId], [CollaboratorRoleAdditionalInfo], [CollaboratorIndustryId], [CollaboratorIndustryAdditionalInfo], [HasAnySpecialNeeds], [SpecialNeedsDescription]) 
				VALUES
					(@UserId, NEWID(), @Nome, NULL, NULL, @Telefone, @Celular, NULL, NULL, NULL, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);

				SELECT @CollaboratorId = [Id] FROM dbo.Collaborators WHERE Id = @UserId;
				IF (@CollaboratorId IS NULL)
				BEGIN
					UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '1002', ErrorMessage = 'The collaborator could not be created.' WHERE Id = @id;
					GOTO ERROR_NEXTFETCH;
				END;
			END;

			-- Attendee Collaborators
			SELECT @AttendeeCollaboratorId = [Id] FROM dbo.AttendeeCollaborators WHERE CollaboratorId = @CollaboratorId AND EditionId = @EditionId;
			IF (@AttendeeCollaboratorId IS NULL)
			BEGIN
				INSERT INTO [dbo].[AttendeeCollaborators]
					([Uid], [EditionId], [CollaboratorId], [WelcomeEmailSendDate], [OnboardingStartDate], [OnboardingFinishDate], [OnboardingUserDate], [OnboardingCollaboratorDate], [PlayerTermsAcceptanceDate], [ProducerTermsAcceptanceDate], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [OnboardingOrganizationDataSkippedDate], [SpeakerTermsAcceptanceDate])
				VALUES
					(NEWID(), @EditionId, @CollaboratorId, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId, NULL, NULL);

				SELECT @AttendeeCollaboratorId = [Id] FROM dbo.AttendeeCollaborators WHERE CollaboratorId = @CollaboratorId AND EditionId = @EditionId;
				IF (@AttendeeCollaboratorId IS NULL)
				BEGIN
					UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '1003', ErrorMessage = 'The attendee collaborator could not be created.' WHERE Id = @id;
					GOTO ERROR_NEXTFETCH;
				END;
			END;

			-- Attendee Collaborator Types
			SELECT @AttendeeCollaboratorTypeId = [Id] FROM dbo.AttendeeCollaboratorTypes WHERE AttendeeCollaboratorId = @AttendeeCollaboratorId AND CollaboratorTypeId = @CollaboratorTypeId;
			IF (@AttendeeCollaboratorTypeId IS NULL)
			BEGIN
				INSERT INTO [dbo].[AttendeeCollaboratorTypes]
					([Uid], [AttendeeCollaboratorId], [CollaboratorTypeId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [IsApiDisplayEnabled], [ApiHighlightPosition], [TermsAcceptanceDate])
				VALUES
					(NEWID(), @AttendeeCollaboratorId, @CollaboratorTypeId, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId, 0, NULL, NULL);

				SELECT @AttendeeCollaboratorTypeId = [Id] FROM dbo.AttendeeCollaboratorTypes WHERE AttendeeCollaboratorId = @AttendeeCollaboratorId AND CollaboratorTypeId = @CollaboratorTypeId;
				IF (@AttendeeCollaboratorTypeId IS NULL)
				BEGIN
					UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '1004', ErrorMessage = 'The attendee collaborator type could not be created.' WHERE Id = @id;
					GOTO ERROR_NEXTFETCH;
				END;
			END;
		END;

		-- PROJECT 1 ------------------------------------------------------------------------------------
		BEGIN
			-- Unset control variables
			SELECT @MusicBandId = NULL;
			SELECT @MusicBandTypeId = NULL;
			SELECT @AttendeeMusicBandId = NULL;
			SELECT @AttendeeMusicBandCollaboratorId = NULL;
			SELECT @MusicProjectId = NULL;
			SELECT @TargetAudienceId = NULL;
			SELECT @MusicBandTargetAudienceId = NULL;

			-- Music Bands
			SELECT @MusicBandId = [Id] FROM dbo.MusicBands WHERE Name = @Projeto1NomeDoArtista;
			IF (@MusicBandId IS NULL)
			BEGIN
				-- Music Band Type
				SELECT @MusicBandTypeId = Id FROM dbo.MusicBandTypes WHERE Name = ISNULL(@Projeto1TipoDoArtista, 'Banda / Grupo Musical');
				IF (@MusicBandTypeId IS NULL)
				BEGIN
					UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2001', ErrorMessage = 'The music band type could not be found.' WHERE Id = @id;
					GOTO ERROR_NEXTFETCH;
				END;

				IF (LOWER(@Projeto1Foto) NOT LIKE '%.jpg' AND LOWER(@Projeto1Foto) NOT LIKE '%.jpeg' AND LOWER(@Projeto1Foto) NOT LIKE '%.png')
				BEGIN
					SELECT @Projeto1Foto = NULL;
				END;

				PRINT N' ';
				PRINT N'@Projeto1NomeDoArtista (Name): ' + CAST(LEN(@Projeto1NomeDoArtista) as nvarchar(max));
				PRINT N'@Projeto1Foto (ImageUrl): ' + CAST(LEN(@Projeto1Foto) as nvarchar(max));
				PRINT N'@Projeto1AnoDeInicio (FormationDate): ' + CAST(LEN(@Projeto1AnoDeInicio) as nvarchar(max));
				PRINT N'@Projeto1Influencias (MainMusicInfluences): ' + CAST(LEN(@Projeto1Influencias) as nvarchar(max));
				PRINT N'@Projeto1FanpageNoFacebook (Facebook): ' + CAST(LEN(@Projeto1FanpageNoFacebook) as nvarchar(max));
				PRINT N'@Projeto1Instagram (Instagram): ' + CAST(LEN(@Projeto1Instagram) as nvarchar(max));
				PRINT N'@Projeto1Twitter (Twitter): ' + CAST(LEN(@Projeto1Twitter) as nvarchar(max));
				PRINT N'@Projeto1Youtube (Youtube): ' + CAST(LEN(@Projeto1Youtube) as nvarchar(max));

				INSERT INTO [dbo].[MusicBands] 
					([Uid], [MusicBandTypeId], [Name], [ImageUrl], [FormationDate], [MainMusicInfluences], [Facebook] ,[Instagram], [Twitter], [Youtube], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
				VALUES
					(NEWID(), @MusicBandTypeId, @Projeto1NomeDoArtista, @Projeto1Foto, @Projeto1AnoDeInicio, @Projeto1Influencias, @Projeto1FanpageNoFacebook, @Projeto1Instagram, @Projeto1Twitter, @Projeto1Youtube, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

				SELECT @MusicBandId = [Id] FROM dbo.MusicBands WHERE Name = @Projeto1NomeDoArtista;
				IF (@MusicBandId IS NULL)
				BEGIN
					UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2002', ErrorMessage = 'The music band could not be created.' WHERE Id = @id;
					GOTO ERROR_NEXTFETCH;
				END;
			END;

			-- Attendee Music Bands
			SELECT @AttendeeMusicBandId = [Id] FROM dbo.AttendeeMusicBands WHERE MusicBandId = @MusicBandId AND EditionId = @EditionId;
			IF (@AttendeeMusicBandId IS NULL)
			BEGIN
				INSERT INTO [dbo].[AttendeeMusicBands]
					([Uid], [EditionId], [MusicBandId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
				VALUES
					(NEWID(), @EditionId, @MusicBandId, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

				SELECT @AttendeeMusicBandId = [Id] FROM dbo.AttendeeMusicBands WHERE MusicBandId = @MusicBandId AND EditionId = @EditionId;
				IF (@AttendeeMusicBandId IS NULL)
				BEGIN
					UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2003', ErrorMessage = 'The attendee music band could not be created.' WHERE Id = @id;
					GOTO ERROR_NEXTFETCH;
				END
			END;

			-- Attendee Music Band Collaborators
			SELECT @AttendeeMusicBandCollaboratorId = [Id] FROM dbo.AttendeeMusicBandCollaborators WHERE AttendeeMusicBandId = @AttendeeMusicBandId AND AttendeeCollaboratorId = @AttendeeCollaboratorId;
			IF (@AttendeeMusicBandCollaboratorId IS NULL)
			BEGIN
				INSERT INTO [dbo].[AttendeeMusicBandCollaborators]
					([Uid], [AttendeeMusicBandId], [AttendeeCollaboratorId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
				VALUES
					(NEWID(), @AttendeeMusicBandId, @AttendeeCollaboratorId, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

				SELECT @AttendeeMusicBandCollaboratorId = [Id] FROM dbo.AttendeeMusicBandCollaborators WHERE AttendeeMusicBandId = @AttendeeMusicBandId AND AttendeeCollaboratorId = @AttendeeCollaboratorId;
				IF (@AttendeeMusicBandCollaboratorId IS NULL)
				BEGIN
					UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2004', ErrorMessage = 'The attendee music band collaborator could not be created.' WHERE Id = @id;
					GOTO ERROR_NEXTFETCH;
				END
			END;

			-- Music Projects
			SELECT @MusicProjectId = [Id] FROM dbo.MusicProjects WHERE AttendeeMusicBandId = @AttendeeMusicBandId AND Music1Url = @Projeto1Musica1 AND Music2Url = @Projeto1Musica2;
			IF (@MusicProjectId IS NULL)
			BEGIN
				PRINT N' ';
				PRINT N'@Projeto1VideoClip (VideoUrl): ' + CAST(LEN(@Projeto1VideoClip) as nvarchar(max));
				PRINT N'@Projeto1Musica1 (Music1Url): ' + CAST(LEN(@Projeto1Musica1) as nvarchar(max));
				PRINT N'@Projeto1Musica2 (Music2Url): ' + CAST(LEN(@Projeto1Musica2) as nvarchar(max));
				PRINT N'@Projeto1Release (Release): ' + CAST(LEN(@Projeto1Release) as nvarchar(max));
				PRINT N'@Projeto1Clipping1 (Clipping1): ' + CAST(LEN(@Projeto1Clipping1) as nvarchar(max));
				PRINT N'@Projeto1Clipping2 (Clipping2): ' + CAST(LEN(@Projeto1Clipping2) as nvarchar(max));
				PRINT N'@Projeto1Clipping3 (Clipping3): ' + CAST(LEN(@Projeto1Clipping3) as nvarchar(max));

				INSERT INTO [dbo].[MusicProjects]
					([Uid], [AttendeeMusicBandId], [VideoUrl], [Music1Url], [Music2Url], [Release], [Clipping1], [Clipping2], [Clipping3], [ProjectEvaluationStatusId], [ProjectEvaluationRefuseReasonId], [Reason], [EvaluationUserId], [EvaluationEmailSendDate], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
				VALUES
					(NEWID(), @AttendeeMusicBandId, @Projeto1VideoClip, @Projeto1Musica1, @Projeto1Musica2, @Projeto1Release, @Projeto1Clipping1, @Projeto1Clipping2, @Projeto1Clipping3, @ProjectEvaluationStatusId, NULL, NULL, NULL, NULL, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

				SELECT @MusicProjectId = [Id] FROM dbo.MusicProjects WHERE AttendeeMusicBandId = @AttendeeMusicBandId AND Music1Url = @Projeto1Musica1 AND Music2Url = @Projeto1Musica2;
				IF (@MusicProjectId IS NULL)
				BEGIN
					UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2005', ErrorMessage = 'The music project could not be created.' WHERE Id = @id;
					GOTO ERROR_NEXTFETCH;
				END
			END;

			-- Music Band Genres
			IF (@Projeto1Estilos IS NOT NULL)
			BEGIN
				SELECT @MusicBandGenre = NULL;

				-- Cursor
				DECLARE musicBandGendersCursor CURSOR local fast_forward FOR  
				SELECT val FROM [dbo].[Split](@Projeto1Estilos, ',');

				OPEN musicBandGendersCursor;

				FETCH NEXT FROM musicBandGendersCursor 
				INTO @MusicBandGenre;

				WHILE @@FETCH_STATUS = 0
				BEGIN
					SELECT @MusicGenreId = NULL;
					SELECT @MusicBandGenreId = NULL;

					IF (@MusicBandGenre IS NOT NULL) 
					BEGIN
						SELECT @MusicGenreId = [Id] FROM dbo.MusicGenres WHERE Name = @MusicBandGenre;
						IF (@MusicGenreId IS NOT NULL)
						BEGIN
							SELECT @MusicBandGenreId = [Id] FROM dbo.MusicBandGenres WHERE MusicBandId = @MusicBandId AND MusicGenreId = @MusicGenreId;
							IF (@MusicBandGenreId IS NULL)
							BEGIN
								INSERT INTO [dbo].[MusicBandGenres]
								([Uid], [MusicBandId], [MusicGenreId], [AdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
								VALUES
								(NEWID(), @MusicBandId, @MusicGenreId, NULL, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

								SELECT @MusicBandGenreId = [Id] FROM dbo.MusicBandGenres WHERE MusicBandId = @MusicBandId AND MusicGenreId = @MusicGenreId;
								IF (@MusicBandGenreId IS NULL)
								BEGIN
									UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2006', ErrorMessage = 'The music band genre could not be created.' WHERE Id = @id;
									GOTO ERROR_NEXTFETCH;
								END
							END;
						END;
					END;

					FETCH NEXT FROM musicBandGendersCursor 
					INTO @MusicBandGenre;
				END;

				CLOSE musicBandGendersCursor;
				DEALLOCATE musicBandGendersCursor;
			END;

			-- Music Band Target Audiences
			IF (@Projeto1PublicoAlvo IS NOT NULL)
			BEGIN
				SELECT @TargetAudienceId = [Id] FROM dbo.TargetAudiences WHERE ProjectTypeId = 2 AND  Name = CASE
																									 	 		WHEN LOWER(@Projeto1PublicoAlvo) = 'adult' THEN 'Adulto | Adult'
																									 	 		WHEN LOWER(@Projeto1PublicoAlvo) = 'young' THEN 'Jovem | Young Adults'
																									 	 		WHEN LOWER(@Projeto1PublicoAlvo) = 'children' THEN 'Infantil | Children'
																									 	 		ELSE 'NOT CONFIGURED'
																									 		 END;
				IF (@TargetAudienceId IS NOT NULL)
				BEGIN
					SELECT @MusicBandTargetAudienceId = [Id] FROM dbo.MusicBandTargetAudiences WHERE MusicBandId = @MusicBandId AND TargetAudienceId = @TargetAudienceId;
					IF (@MusicBandTargetAudienceId IS NULL)
					BEGIN
						INSERT INTO [dbo].[MusicBandTargetAudiences]
							([Uid], [MusicBandId], [TargetAudienceId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @TargetAudienceId, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandTargetAudienceId = [Id] FROM dbo.MusicBandTargetAudiences WHERE MusicBandId = @MusicBandId AND TargetAudienceId = @TargetAudienceId;
						IF (@MusicBandTargetAudienceId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2007', ErrorMessage = 'The music band target audience could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END

					END;
				END;
			END;

			-- Released Music Projects
			BEGIN
				-- Release 1
				SELECT @ReleasedMusicProjectId = NULL;

				IF (NULLIF(@Projeto1Release1Nome, '') IS NOT NULL AND NULLIF(@Projeto1Release1Ano, '') IS NOT NULL)
				BEGIN
					SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Release1Nome AND [Year] = @Projeto1Release1Ano;
					IF (@ReleasedMusicProjectId IS NULL)
					BEGIN
						INSERT INTO [dbo].[ReleasedMusicProjects]
							([Uid], [MusicBandId], [Name], [Year], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto1Release1Nome, @Projeto1Release1Ano, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Release1Nome AND [Year] = @Projeto1Release1Ano;
						IF (@ReleasedMusicProjectId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2008', ErrorMessage = 'The release 1 music project could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Release 2
				SELECT @ReleasedMusicProjectId = NULL;

				IF (NULLIF(@Projeto1Release2Nome, '') IS NOT NULL AND NULLIF(@Projeto1Release2Ano, '') IS NOT NULL)
				BEGIN
					SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Release2Nome AND [Year] = @Projeto1Release2Ano;
					IF (@ReleasedMusicProjectId IS NULL)
					BEGIN
						INSERT INTO [dbo].[ReleasedMusicProjects]
							([Uid], [MusicBandId], [Name], [Year], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto1Release2Nome, @Projeto1Release2Ano, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Release2Nome AND [Year] = @Projeto1Release2Ano;
						IF (@ReleasedMusicProjectId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2009', ErrorMessage = 'The release 2 music project could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Release 3
				SELECT @ReleasedMusicProjectId = NULL;

				IF (NULLIF(@Projeto1Release3Nome, '') IS NOT NULL AND NULLIF(@Projeto1Release3Ano, '') IS NOT NULL)
				BEGIN
					SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Release3Nome AND [Year] = @Projeto1Release3Ano;
					IF (@ReleasedMusicProjectId IS NULL)
					BEGIN
						INSERT INTO [dbo].[ReleasedMusicProjects]
							([Uid], [MusicBandId], [Name], [Year], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto1Release3Nome, @Projeto1Release3Ano, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Release3Nome AND [Year] = @Projeto1Release3Ano;
						IF (@ReleasedMusicProjectId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2010', ErrorMessage = 'The release 3 music project could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Release 4
				SELECT @ReleasedMusicProjectId = NULL;

				IF (NULLIF(@Projeto1Release4Nome, '') IS NOT NULL AND NULLIF(@Projeto1Release4Ano, '') IS NOT NULL)
				BEGIN
					SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Release4Nome AND [Year] = @Projeto1Release4Ano;
					IF (@ReleasedMusicProjectId IS NULL)
					BEGIN
						INSERT INTO [dbo].[ReleasedMusicProjects]
							([Uid], [MusicBandId], [Name], [Year], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto1Release4Nome, @Projeto1Release4Ano, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Release4Nome AND [Year] = @Projeto1Release4Ano;
						IF (@ReleasedMusicProjectId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2011', ErrorMessage = 'The release 4 music project could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Release 5
				SELECT @ReleasedMusicProjectId = NULL;

				IF (NULLIF(@Projeto1Release5Nome, '') IS NOT NULL AND NULLIF(@Projeto1Release5Ano, '') IS NOT NULL)
				BEGIN
					SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Release5Nome AND [Year] = @Projeto1Release5Ano;
					IF (@ReleasedMusicProjectId IS NULL)
					BEGIN
						INSERT INTO [dbo].[ReleasedMusicProjects]
							([Uid], [MusicBandId], [Name], [Year], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto1Release5Nome, @Projeto1Release5Ano, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Release5Nome AND [Year] = @Projeto1Release5Ano;
						IF (@ReleasedMusicProjectId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2012', ErrorMessage = 'The release 5 music project could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;
			END;

			-- Music Band Members
			BEGIN
				-- Member 1
				SELECT @MusicBandMemberId = NULL;

				IF (NULLIF(@Projeto1Integrante1Nome, '') IS NOT NULL AND NULLIF(@Projeto1Integrante1Instrumento, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Integrante1Nome AND [MusicInstrumentName] = @Projeto1Integrante1Instrumento;
					IF (@MusicBandMemberId IS NULL)
					BEGIN
						INSERT INTO [dbo].[MusicBandMembers]
							([Uid], [MusicBandId], [Name], [MusicInstrumentName], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto1Integrante1Nome, @Projeto1Integrante1Instrumento, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Integrante1Nome AND [MusicInstrumentName] = @Projeto1Integrante1Instrumento;
						IF (@MusicBandMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2013', ErrorMessage = 'The music band member 1 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Member 2
				SELECT @MusicBandMemberId = NULL;

				IF (NULLIF(@Projeto1Integrante2Nome, '') IS NOT NULL AND NULLIF(@Projeto1Integrante2Instrumento, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Integrante2Nome AND [MusicInstrumentName] = @Projeto1Integrante2Instrumento;
					IF (@MusicBandMemberId IS NULL)
					BEGIN
						INSERT INTO [dbo].[MusicBandMembers]
							([Uid], [MusicBandId], [Name], [MusicInstrumentName], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto1Integrante2Nome, @Projeto1Integrante2Instrumento, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Integrante2Nome AND [MusicInstrumentName] = @Projeto1Integrante2Instrumento;
						IF (@MusicBandMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2014', ErrorMessage = 'The music band member 2 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Member 3
				SELECT @MusicBandMemberId = NULL;

				IF (NULLIF(@Projeto1Integrante3Nome, '') IS NOT NULL AND NULLIF(@Projeto1Integrante3Instrumento, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Integrante3Nome AND [MusicInstrumentName] = @Projeto1Integrante3Instrumento;
					IF (@MusicBandMemberId IS NULL)
					BEGIN
						INSERT INTO [dbo].[MusicBandMembers]
							([Uid], [MusicBandId], [Name], [MusicInstrumentName], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto1Integrante3Nome, @Projeto1Integrante3Instrumento, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Integrante3Nome AND [MusicInstrumentName] = @Projeto1Integrante3Instrumento;
						IF (@MusicBandMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2015', ErrorMessage = 'The music band member 3 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Member 4
				SELECT @MusicBandMemberId = NULL;

				IF (NULLIF(@Projeto1Integrante4Nome, '') IS NOT NULL AND NULLIF(@Projeto1Integrante4Instrumento, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Integrante4Nome AND [MusicInstrumentName] = @Projeto1Integrante4Instrumento;
					IF (@MusicBandMemberId IS NULL)
					BEGIN
						INSERT INTO [dbo].[MusicBandMembers]
							([Uid], [MusicBandId], [Name], [MusicInstrumentName], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto1Integrante4Nome, @Projeto1Integrante4Instrumento, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Integrante4Nome AND [MusicInstrumentName] = @Projeto1Integrante4Instrumento;
						IF (@MusicBandMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2016', ErrorMessage = 'The music band member 4 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Member 5
				SELECT @MusicBandMemberId = NULL;

				IF (NULLIF(@Projeto1Integrante5Nome, '') IS NOT NULL AND NULLIF(@Projeto1Integrante5Instrumento, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Integrante5Nome AND [MusicInstrumentName] = @Projeto1Integrante5Instrumento;
					IF (@MusicBandMemberId IS NULL)
					BEGIN
						INSERT INTO [dbo].[MusicBandMembers]
							([Uid], [MusicBandId], [Name], [MusicInstrumentName], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto1Integrante5Nome, @Projeto1Integrante5Instrumento, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Integrante5Nome AND [MusicInstrumentName] = @Projeto1Integrante5Instrumento;
						IF (@MusicBandMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2017', ErrorMessage = 'The music band member 5 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;
			END;

			-- Music Band Teams
			BEGIN
				-- Team 1
				SELECT @MusicBandTeamMemberId = NULL;

				IF (NULLIF(@Projeto1Time1Nome, '') IS NOT NULL AND NULLIF(@Projeto1Time1Funcao, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Time1Nome AND [Role] = @Projeto1Time1Funcao;
					IF (@MusicBandTeamMemberId IS NULL)
					BEGIN
					INSERT INTO [dbo].[MusicBandTeamMembers]
						([Uid], [MusicBandId], [Name], [Role], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
					VALUES
						(NEWID(), @MusicBandId, @Projeto1Time1Nome, @Projeto1Time1Funcao, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Time1Nome AND [Role] = @Projeto1Time1Funcao;
						IF (@MusicBandTeamMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2018', ErrorMessage = 'The music band team member 1 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Team 2
				SELECT @MusicBandTeamMemberId = NULL;

				IF (NULLIF(@Projeto1Time2Nome, '') IS NOT NULL AND NULLIF(@Projeto1Time2Funcao, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Time2Nome AND [Role] = @Projeto1Time2Funcao;
					IF (@MusicBandTeamMemberId IS NULL)
					BEGIN
					INSERT INTO [dbo].[MusicBandTeamMembers]
						([Uid], [MusicBandId], [Name], [Role], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
					VALUES
						(NEWID(), @MusicBandId, @Projeto1Time2Nome, @Projeto1Time2Funcao, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Time2Nome AND [Role] = @Projeto1Time2Funcao;
						IF (@MusicBandTeamMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2019', ErrorMessage = 'The music band team member member 2 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Team 3
				SELECT @MusicBandTeamMemberId = NULL;

				IF (NULLIF(@Projeto1Time3Nome, '') IS NOT NULL AND NULLIF(@Projeto1Time3Funcao, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Time3Nome AND [Role] = @Projeto1Time3Funcao;
					IF (@MusicBandTeamMemberId IS NULL)
					BEGIN
					INSERT INTO [dbo].[MusicBandTeamMembers]
						([Uid], [MusicBandId], [Name], [Role], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
					VALUES
						(NEWID(), @MusicBandId, @Projeto1Time3Nome, @Projeto1Time3Funcao, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Time3Nome AND [Role] = @Projeto1Time3Funcao;
						IF (@MusicBandTeamMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2020', ErrorMessage = 'The music band team member 3 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Team 4
				SELECT @MusicBandTeamMemberId = NULL;

				IF (NULLIF(@Projeto1Time4Nome, '') IS NOT NULL AND NULLIF(@Projeto1Time4Funcao, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Time4Nome AND [Role] = @Projeto1Time4Funcao;
					IF (@MusicBandTeamMemberId IS NULL)
					BEGIN
					INSERT INTO [dbo].[MusicBandTeamMembers]
						([Uid], [MusicBandId], [Name], [Role], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
					VALUES
						(NEWID(), @MusicBandId, @Projeto1Time4Nome, @Projeto1Time4Funcao, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Time4Nome AND [Role] = @Projeto1Time4Funcao;
						IF (@MusicBandTeamMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2021', ErrorMessage = 'The music band team member 4 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Team 5
				SELECT @MusicBandTeamMemberId = NULL;

				IF (NULLIF(@Projeto1Time5Nome, '') IS NOT NULL AND NULLIF(@Projeto1Time5Funcao, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Time5Nome AND [Role] = @Projeto1Time5Funcao;
					IF (@MusicBandTeamMemberId IS NULL)
					BEGIN
					INSERT INTO [dbo].[MusicBandTeamMembers]
						([Uid], [MusicBandId], [Name], [Role], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
					VALUES
						(NEWID(), @MusicBandId, @Projeto1Time5Nome, @Projeto1Time5Funcao, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto1Time5Nome AND [Role] = @Projeto1Time5Funcao;
						IF (@MusicBandTeamMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '2022', ErrorMessage = 'The music band team member 5 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;
			END;
		END;

		-- PROJECT 2 ------------------------------------------------------------------------------------
		BEGIN
			IF (@Projeto2NomeDoArtista IS NULL)
			BEGIN
				GOTO SUCCESS_NEXTFETCH;
			END;

			-- Unset control variables
			SELECT @MusicBandId = NULL;
			SELECT @MusicBandTypeId = NULL;
			SELECT @AttendeeMusicBandId = NULL;
			SELECT @AttendeeMusicBandCollaboratorId = NULL;
			SELECT @MusicProjectId = NULL;
			SELECT @TargetAudienceId = NULL;
			SELECT @MusicBandTargetAudienceId = NULL;

			-- Music Bands
			SELECT @MusicBandId = [Id] FROM dbo.MusicBands WHERE Name = @Projeto2NomeDoArtista;
			IF (@MusicBandId IS NULL)
			BEGIN
				-- Music Band Type
				SELECT @MusicBandTypeId = Id FROM dbo.MusicBandTypes WHERE Name = ISNULL(@Projeto2TipoDoArtista, 'Banda / Grupo Musical');
				IF (@MusicBandTypeId IS NULL)
				BEGIN
					UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3001', ErrorMessage = 'The music band type could not be found.' WHERE Id = @id;
					GOTO ERROR_NEXTFETCH;
				END;

				IF (LOWER(@Projeto2Foto) NOT LIKE '%.jpg' AND LOWER(@Projeto2Foto) NOT LIKE '%.jpeg' AND LOWER(@Projeto2Foto) NOT LIKE '%.png')
				BEGIN
					SELECT @Projeto2Foto = NULL;
				END;

				PRINT N' ';
				PRINT N'@Projeto2NomeDoArtista (Name): ' + CAST(LEN(@Projeto2NomeDoArtista) as nvarchar(max));
				PRINT N'@Projeto2Foto (ImageUrl): ' + CAST(LEN(@Projeto2Foto) as nvarchar(max));
				PRINT N'@Projeto2AnoDeInicio (FormationDate): ' + CAST(LEN(@Projeto2AnoDeInicio) as nvarchar(max));
				PRINT N'@Projeto2Influencias (MainMusicInfluences): ' + CAST(LEN(@Projeto2Influencias) as nvarchar(max));
				PRINT N'@Projeto2FanpageNoFacebook (Facebook): ' + CAST(LEN(@Projeto2FanpageNoFacebook) as nvarchar(max));
				PRINT N'@Projeto2Instagram (Instagram): ' + CAST(LEN(@Projeto2Instagram) as nvarchar(max));
				PRINT N'@Projeto2Twitter (Twitter): ' + CAST(LEN(@Projeto2Twitter) as nvarchar(max));
				PRINT N'@Projeto2Youtube (Youtube): ' + CAST(LEN(@Projeto2Youtube) as nvarchar(max));

				INSERT INTO [dbo].[MusicBands] 
					([Uid], [MusicBandTypeId], [Name], [ImageUrl], [FormationDate], [MainMusicInfluences], [Facebook] ,[Instagram], [Twitter], [Youtube], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
				VALUES
					(NEWID(), @MusicBandTypeId, @Projeto2NomeDoArtista, @Projeto2Foto, @Projeto2AnoDeInicio, @Projeto2Influencias, @Projeto2FanpageNoFacebook, @Projeto2Instagram, @Projeto2Twitter, @Projeto2Youtube, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

				SELECT @MusicBandId = [Id] FROM dbo.MusicBands WHERE Name = @Projeto2NomeDoArtista;
				IF (@MusicBandId IS NULL)
				BEGIN
					UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3002', ErrorMessage = 'The music band could not be created.' WHERE Id = @id;
					GOTO ERROR_NEXTFETCH;
				END;
			END;

			-- Attendee Music Bands
			SELECT @AttendeeMusicBandId = [Id] FROM dbo.AttendeeMusicBands WHERE MusicBandId = @MusicBandId AND EditionId = @EditionId;
			IF (@AttendeeMusicBandId IS NULL)
			BEGIN
				INSERT INTO [dbo].[AttendeeMusicBands]
					([Uid], [EditionId], [MusicBandId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
				VALUES
					(NEWID(), @EditionId, @MusicBandId, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

				SELECT @AttendeeMusicBandId = [Id] FROM dbo.AttendeeMusicBands WHERE MusicBandId = @MusicBandId AND EditionId = @EditionId;
				IF (@AttendeeMusicBandId IS NULL)
				BEGIN
					UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3003', ErrorMessage = 'The attendee music band could not be created.' WHERE Id = @id;
					GOTO ERROR_NEXTFETCH;
				END
			END;

			-- Attendee Music Band Collaborators
			SELECT @AttendeeMusicBandCollaboratorId = [Id] FROM dbo.AttendeeMusicBandCollaborators WHERE AttendeeMusicBandId = @AttendeeMusicBandId AND AttendeeCollaboratorId = @AttendeeCollaboratorId;
			IF (@AttendeeMusicBandCollaboratorId IS NULL)
			BEGIN
				INSERT INTO [dbo].[AttendeeMusicBandCollaborators]
					([Uid], [AttendeeMusicBandId], [AttendeeCollaboratorId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
				VALUES
					(NEWID(), @AttendeeMusicBandId, @AttendeeCollaboratorId, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

				SELECT @AttendeeMusicBandCollaboratorId = [Id] FROM dbo.AttendeeMusicBandCollaborators WHERE AttendeeMusicBandId = @AttendeeMusicBandId AND AttendeeCollaboratorId = @AttendeeCollaboratorId;
				IF (@AttendeeMusicBandCollaboratorId IS NULL)
				BEGIN
					UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3004', ErrorMessage = 'The attendee music band collaborator could not be created.' WHERE Id = @id;
					GOTO ERROR_NEXTFETCH;
				END
			END;

			-- Music Projects
			SELECT @MusicProjectId = [Id] FROM dbo.MusicProjects WHERE AttendeeMusicBandId = @AttendeeMusicBandId AND Music1Url = @Projeto2Musica1 AND Music2Url = @Projeto2Musica2;
			IF (@MusicProjectId IS NULL)
			BEGIN
				PRINT N' ';
				PRINT N'@Projeto2VideoClip (VideoUrl): ' + CAST(LEN(@Projeto2VideoClip) as nvarchar(max));
				PRINT N'@Projeto2Musica1 (Music1Url): ' + CAST(LEN(@Projeto2Musica1) as nvarchar(max));
				PRINT N'@Projeto2Musica2 (Music2Url): ' + CAST(LEN(@Projeto2Musica2) as nvarchar(max));
				PRINT N'@Projeto2Release (Release): ' + CAST(LEN(@Projeto2Release) as nvarchar(max));
				PRINT N'@Projeto2Clipping1 (Clipping1): ' + CAST(LEN(@Projeto2Clipping1) as nvarchar(max));
				PRINT N'@Projeto2Clipping2 (Clipping2): ' + CAST(LEN(@Projeto2Clipping2) as nvarchar(max));
				PRINT N'@Projeto2Clipping3 (Clipping3): ' + CAST(LEN(@Projeto2Clipping3) as nvarchar(max));

				INSERT INTO [dbo].[MusicProjects]
					([Uid], [AttendeeMusicBandId], [VideoUrl], [Music1Url], [Music2Url], [Release], [Clipping1], [Clipping2], [Clipping3], [ProjectEvaluationStatusId], [ProjectEvaluationRefuseReasonId], [Reason], [EvaluationUserId], [EvaluationEmailSendDate], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
				VALUES
					(NEWID(), @AttendeeMusicBandId, @Projeto2VideoClip, @Projeto2Musica1, @Projeto2Musica2, @Projeto2Release, @Projeto2Clipping1, @Projeto2Clipping2, @Projeto2Clipping3, @ProjectEvaluationStatusId, NULL, NULL, NULL, NULL, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

				SELECT @MusicProjectId = [Id] FROM dbo.MusicProjects WHERE AttendeeMusicBandId = @AttendeeMusicBandId AND Music1Url = @Projeto2Musica1 AND Music2Url = @Projeto2Musica2;
				IF (@MusicProjectId IS NULL)
				BEGIN
					UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3005', ErrorMessage = 'The music project could not be created.' WHERE Id = @id;
					GOTO ERROR_NEXTFETCH;
				END
			END;

			-- Music Band Genres
			IF (@Projeto2Estilos IS NOT NULL)
			BEGIN
				SELECT @MusicBandGenre = NULL;

				-- Cursor
				DECLARE musicBandGendersCursor CURSOR local fast_forward FOR  
				SELECT val FROM [dbo].[Split](@Projeto2Estilos, ',');

				OPEN musicBandGendersCursor;

				FETCH NEXT FROM musicBandGendersCursor 
				INTO @MusicBandGenre;

				WHILE @@FETCH_STATUS = 0
				BEGIN
					SELECT @MusicGenreId = NULL;
					SELECT @MusicBandGenreId = NULL;

					IF (@MusicBandGenre IS NOT NULL) 
					BEGIN
						SELECT @MusicGenreId = [Id] FROM dbo.MusicGenres WHERE Name = @MusicBandGenre;
						IF (@MusicGenreId IS NOT NULL)
						BEGIN
							SELECT @MusicBandGenreId = [Id] FROM dbo.MusicBandGenres WHERE MusicBandId = @MusicBandId AND MusicGenreId = @MusicGenreId;
							IF (@MusicBandGenreId IS NULL)
							BEGIN
								INSERT INTO [dbo].[MusicBandGenres]
								([Uid], [MusicBandId], [MusicGenreId], [AdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
								VALUES
								(NEWID(), @MusicBandId, @MusicGenreId, NULL, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

								SELECT @MusicBandGenreId = [Id] FROM dbo.MusicBandGenres WHERE MusicBandId = @MusicBandId AND MusicGenreId = @MusicGenreId;
								IF (@MusicBandGenreId IS NULL)
								BEGIN
									UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3006', ErrorMessage = 'The music band genre could not be created.' WHERE Id = @id;
									GOTO ERROR_NEXTFETCH;
								END
							END;
						END;
					END;

					FETCH NEXT FROM musicBandGendersCursor 
					INTO @MusicBandGenre;
				END;

				CLOSE musicBandGendersCursor;
				DEALLOCATE musicBandGendersCursor;
			END;

			-- Music Band Target Audiences
			IF (@Projeto2PublicoAlvo IS NOT NULL)
			BEGIN
				SELECT @TargetAudienceId = [Id] FROM dbo.TargetAudiences WHERE ProjectTypeId = 2 AND  Name = CASE
																									 	 		WHEN LOWER(@Projeto2PublicoAlvo) = 'adult' THEN 'Adulto | Adult'
																									 	 		WHEN LOWER(@Projeto2PublicoAlvo) = 'young' THEN 'Jovem | Young Adults'
																									 	 		WHEN LOWER(@Projeto2PublicoAlvo) = 'children' THEN 'Infantil | Children'
																									 	 		ELSE 'NOT CONFIGURED'
																									 		 END;
				IF (@TargetAudienceId IS NOT NULL)
				BEGIN
					SELECT @MusicBandTargetAudienceId = [Id] FROM dbo.MusicBandTargetAudiences WHERE MusicBandId = @MusicBandId AND TargetAudienceId = @TargetAudienceId;
					IF (@MusicBandTargetAudienceId IS NULL)
					BEGIN
						INSERT INTO [dbo].[MusicBandTargetAudiences]
							([Uid], [MusicBandId], [TargetAudienceId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @TargetAudienceId, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandTargetAudienceId = [Id] FROM dbo.MusicBandTargetAudiences WHERE MusicBandId = @MusicBandId AND TargetAudienceId = @TargetAudienceId;
						IF (@MusicBandTargetAudienceId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3007', ErrorMessage = 'The music band target audience could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END

					END;
				END;
			END;

			-- Released Music Projects
			BEGIN
				-- Release 1
				SELECT @ReleasedMusicProjectId = NULL;

				IF (NULLIF(@Projeto2Release1Nome, '') IS NOT NULL AND NULLIF(@Projeto2Release1Ano, '') IS NOT NULL)
				BEGIN
					SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Release1Nome AND [Year] = @Projeto2Release1Ano;
					IF (@ReleasedMusicProjectId IS NULL)
					BEGIN
						INSERT INTO [dbo].[ReleasedMusicProjects]
							([Uid], [MusicBandId], [Name], [Year], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto2Release1Nome, @Projeto2Release1Ano, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Release1Nome AND [Year] = @Projeto2Release1Ano;
						IF (@ReleasedMusicProjectId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3008', ErrorMessage = 'The release 1 music project could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Release 2
				SELECT @ReleasedMusicProjectId = NULL;

				IF (NULLIF(@Projeto2Release2Nome, '') IS NOT NULL AND NULLIF(@Projeto2Release2Ano, '') IS NOT NULL)
				BEGIN
					SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Release2Nome AND [Year] = @Projeto2Release2Ano;
					IF (@ReleasedMusicProjectId IS NULL)
					BEGIN
						INSERT INTO [dbo].[ReleasedMusicProjects]
							([Uid], [MusicBandId], [Name], [Year], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto2Release2Nome, @Projeto2Release2Ano, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Release2Nome AND [Year] = @Projeto2Release2Ano;
						IF (@ReleasedMusicProjectId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3009', ErrorMessage = 'The release 2 music project could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Release 3
				SELECT @ReleasedMusicProjectId = NULL;

				IF (NULLIF(@Projeto2Release3Nome, '') IS NOT NULL AND NULLIF(@Projeto2Release3Ano, '') IS NOT NULL)
				BEGIN
					SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Release3Nome AND [Year] = @Projeto2Release3Ano;
					IF (@ReleasedMusicProjectId IS NULL)
					BEGIN
						INSERT INTO [dbo].[ReleasedMusicProjects]
							([Uid], [MusicBandId], [Name], [Year], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto2Release3Nome, @Projeto2Release3Ano, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Release3Nome AND [Year] = @Projeto2Release3Ano;
						IF (@ReleasedMusicProjectId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3010', ErrorMessage = 'The release 3 music project could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Release 4
				SELECT @ReleasedMusicProjectId = NULL;

				IF (NULLIF(@Projeto2Release4Nome, '') IS NOT NULL AND NULLIF(@Projeto2Release4Ano, '') IS NOT NULL)
				BEGIN
					SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Release4Nome AND [Year] = @Projeto2Release4Ano;
					IF (@ReleasedMusicProjectId IS NULL)
					BEGIN
						INSERT INTO [dbo].[ReleasedMusicProjects]
							([Uid], [MusicBandId], [Name], [Year], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto2Release4Nome, @Projeto2Release4Ano, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Release4Nome AND [Year] = @Projeto2Release4Ano;
						IF (@ReleasedMusicProjectId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3011', ErrorMessage = 'The release 4 music project could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Release 5
				SELECT @ReleasedMusicProjectId = NULL;

				IF (NULLIF(@Projeto2Release5Nome, '') IS NOT NULL AND NULLIF(@Projeto2Release5Ano, '') IS NOT NULL)
				BEGIN
					SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Release5Nome AND [Year] = @Projeto2Release5Ano;
					IF (@ReleasedMusicProjectId IS NULL)
					BEGIN
						INSERT INTO [dbo].[ReleasedMusicProjects]
							([Uid], [MusicBandId], [Name], [Year], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto2Release5Nome, @Projeto2Release5Ano, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Release5Nome AND [Year] = @Projeto2Release5Ano;
						IF (@ReleasedMusicProjectId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3012', ErrorMessage = 'The release 5 music project could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;
			END;

			-- Music Band Members
			BEGIN
				-- Member 1
				SELECT @MusicBandMemberId = NULL;

				IF (NULLIF(@Projeto2Integrante1Nome, '') IS NOT NULL AND NULLIF(@Projeto2Integrante1Instrumento, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Integrante1Nome AND [MusicInstrumentName] = @Projeto2Integrante1Instrumento;
					IF (@MusicBandMemberId IS NULL)
					BEGIN
						INSERT INTO [dbo].[MusicBandMembers]
							([Uid], [MusicBandId], [Name], [MusicInstrumentName], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto2Integrante1Nome, @Projeto2Integrante1Instrumento, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Integrante1Nome AND [MusicInstrumentName] = @Projeto2Integrante1Instrumento;
						IF (@MusicBandMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3013', ErrorMessage = 'The music band member 1 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Member 2
				SELECT @MusicBandMemberId = NULL;

				IF (NULLIF(@Projeto2Integrante2Nome, '') IS NOT NULL AND NULLIF(@Projeto2Integrante2Instrumento, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Integrante2Nome AND [MusicInstrumentName] = @Projeto2Integrante2Instrumento;
					IF (@MusicBandMemberId IS NULL)
					BEGIN
						INSERT INTO [dbo].[MusicBandMembers]
							([Uid], [MusicBandId], [Name], [MusicInstrumentName], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto2Integrante2Nome, @Projeto2Integrante2Instrumento, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Integrante2Nome AND [MusicInstrumentName] = @Projeto2Integrante2Instrumento;
						IF (@MusicBandMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3014', ErrorMessage = 'The music band member 2 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Member 3
				SELECT @MusicBandMemberId = NULL;

				IF (NULLIF(@Projeto2Integrante3Nome, '') IS NOT NULL AND NULLIF(@Projeto2Integrante3Instrumento, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Integrante3Nome AND [MusicInstrumentName] = @Projeto2Integrante3Instrumento;
					IF (@MusicBandMemberId IS NULL)
					BEGIN
						INSERT INTO [dbo].[MusicBandMembers]
							([Uid], [MusicBandId], [Name], [MusicInstrumentName], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto2Integrante3Nome, @Projeto2Integrante3Instrumento, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Integrante3Nome AND [MusicInstrumentName] = @Projeto2Integrante3Instrumento;
						IF (@MusicBandMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3015', ErrorMessage = 'The music band member 3 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Member 4
				SELECT @MusicBandMemberId = NULL;

				IF (NULLIF(@Projeto2Integrante4Nome, '') IS NOT NULL AND NULLIF(@Projeto2Integrante4Instrumento, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Integrante4Nome AND [MusicInstrumentName] = @Projeto2Integrante4Instrumento;
					IF (@MusicBandMemberId IS NULL)
					BEGIN
						INSERT INTO [dbo].[MusicBandMembers]
							([Uid], [MusicBandId], [Name], [MusicInstrumentName], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto2Integrante4Nome, @Projeto2Integrante4Instrumento, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Integrante4Nome AND [MusicInstrumentName] = @Projeto2Integrante4Instrumento;
						IF (@MusicBandMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3016', ErrorMessage = 'The music band member 4 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Member 5
				SELECT @MusicBandMemberId = NULL;

				IF (NULLIF(@Projeto2Integrante5Nome, '') IS NOT NULL AND NULLIF(@Projeto2Integrante5Instrumento, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Integrante5Nome AND [MusicInstrumentName] = @Projeto2Integrante5Instrumento;
					IF (@MusicBandMemberId IS NULL)
					BEGIN
						INSERT INTO [dbo].[MusicBandMembers]
							([Uid], [MusicBandId], [Name], [MusicInstrumentName], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto2Integrante5Nome, @Projeto2Integrante5Instrumento, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Integrante5Nome AND [MusicInstrumentName] = @Projeto2Integrante5Instrumento;
						IF (@MusicBandMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3017', ErrorMessage = 'The music band member 5 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;
			END;

			-- Music Band Teams
			BEGIN
				-- Team 1
				SELECT @MusicBandTeamMemberId = NULL;

				IF (NULLIF(@Projeto2Time1Nome, '') IS NOT NULL AND NULLIF(@Projeto2Time1Funcao, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Time1Nome AND [Role] = @Projeto2Time1Funcao;
					IF (@MusicBandTeamMemberId IS NULL)
					BEGIN
					INSERT INTO [dbo].[MusicBandTeamMembers]
						([Uid], [MusicBandId], [Name], [Role], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
					VALUES
						(NEWID(), @MusicBandId, @Projeto2Time1Nome, @Projeto2Time1Funcao, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Time1Nome AND [Role] = @Projeto2Time1Funcao;
						IF (@MusicBandTeamMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3018', ErrorMessage = 'The music band team member 1 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Team 2
				SELECT @MusicBandTeamMemberId = NULL;

				IF (NULLIF(@Projeto2Time2Nome, '') IS NOT NULL AND NULLIF(@Projeto2Time2Funcao, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Time2Nome AND [Role] = @Projeto2Time2Funcao;
					IF (@MusicBandTeamMemberId IS NULL)
					BEGIN
					INSERT INTO [dbo].[MusicBandTeamMembers]
						([Uid], [MusicBandId], [Name], [Role], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
					VALUES
						(NEWID(), @MusicBandId, @Projeto2Time2Nome, @Projeto2Time2Funcao, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Time2Nome AND [Role] = @Projeto2Time2Funcao;
						IF (@MusicBandTeamMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3019', ErrorMessage = 'The music band team member 2 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Team 3
				SELECT @MusicBandTeamMemberId = NULL;

				IF (NULLIF(@Projeto2Time3Nome, '') IS NOT NULL AND NULLIF(@Projeto2Time3Funcao, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Time3Nome AND [Role] = @Projeto2Time3Funcao;
					IF (@MusicBandTeamMemberId IS NULL)
					BEGIN
					INSERT INTO [dbo].[MusicBandTeamMembers]
						([Uid], [MusicBandId], [Name], [Role], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
					VALUES
						(NEWID(), @MusicBandId, @Projeto2Time3Nome, @Projeto2Time3Funcao, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Time3Nome AND [Role] = @Projeto2Time3Funcao;
						IF (@MusicBandTeamMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3020', ErrorMessage = 'The music band team member 3 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Team 4
				SELECT @MusicBandTeamMemberId = NULL;

				IF (NULLIF(@Projeto2Time4Nome, '') IS NOT NULL AND NULLIF(@Projeto2Time4Funcao, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Time4Nome AND [Role] = @Projeto2Time4Funcao;
					IF (@MusicBandTeamMemberId IS NULL)
					BEGIN
					INSERT INTO [dbo].[MusicBandTeamMembers]
						([Uid], [MusicBandId], [Name], [Role], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
					VALUES
						(NEWID(), @MusicBandId, @Projeto2Time4Nome, @Projeto2Time4Funcao, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Time4Nome AND [Role] = @Projeto2Time4Funcao;
						IF (@MusicBandTeamMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3021', ErrorMessage = 'The music band team member 4 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Team 5
				SELECT @MusicBandTeamMemberId = NULL;

				IF (NULLIF(@Projeto2Time5Nome, '') IS NOT NULL AND NULLIF(@Projeto2Time5Funcao, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Time5Nome AND [Role] = @Projeto2Time5Funcao;
					IF (@MusicBandTeamMemberId IS NULL)
					BEGIN
					INSERT INTO [dbo].[MusicBandTeamMembers]
						([Uid], [MusicBandId], [Name], [Role], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
					VALUES
						(NEWID(), @MusicBandId, @Projeto2Time5Nome, @Projeto2Time5Funcao, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto2Time5Nome AND [Role] = @Projeto2Time5Funcao;
						IF (@MusicBandTeamMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '3022', ErrorMessage = 'The music band team member 5 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;
			END;
		END;

		-- PROJECT 3 ------------------------------------------------------------------------------------
		BEGIN
			IF (@Projeto3NomeDoArtista IS NULL)
			BEGIN
				GOTO SUCCESS_NEXTFETCH;
			END;

			-- Unset control variables
			SELECT @MusicBandId = NULL;
			SELECT @MusicBandTypeId = NULL;
			SELECT @AttendeeMusicBandId = NULL;
			SELECT @AttendeeMusicBandCollaboratorId = NULL;
			SELECT @MusicProjectId = NULL;
			SELECT @TargetAudienceId = NULL;
			SELECT @MusicBandTargetAudienceId = NULL;

			-- Music Bands
			SELECT @MusicBandId = [Id] FROM dbo.MusicBands WHERE Name = @Projeto3NomeDoArtista;
			IF (@MusicBandId IS NULL)
			BEGIN
				-- Music Band Type
				SELECT @MusicBandTypeId = Id FROM dbo.MusicBandTypes WHERE Name = ISNULL(@Projeto3TipoDoArtista, 'Banda / Grupo Musical');
				IF (@MusicBandTypeId IS NULL)
				BEGIN
					UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4001', ErrorMessage = 'The music band type could not be found.' WHERE Id = @id;
					GOTO ERROR_NEXTFETCH;
				END;

				IF (LOWER(@Projeto3Foto) NOT LIKE '%.jpg' AND LOWER(@Projeto3Foto) NOT LIKE '%.jpeg' AND LOWER(@Projeto3Foto) NOT LIKE '%.png')
				BEGIN
					SELECT @Projeto3Foto = NULL;
				END;

				PRINT N' ';
				PRINT N'@Projeto3NomeDoArtista (Name): ' + CAST(LEN(@Projeto3NomeDoArtista) as nvarchar(max));
				PRINT N'@Projeto3Foto (ImageUrl): ' + CAST(LEN(@Projeto3Foto) as nvarchar(max));
				PRINT N'@Projeto3AnoDeInicio (FormationDate): ' + CAST(LEN(@Projeto3AnoDeInicio) as nvarchar(max));
				PRINT N'@Projeto3Influencias (MainMusicInfluences): ' + CAST(LEN(@Projeto3Influencias) as nvarchar(max));
				PRINT N'@Projeto3FanpageNoFacebook (Facebook): ' + CAST(LEN(@Projeto3FanpageNoFacebook) as nvarchar(max));
				PRINT N'@Projeto3Instagram (Instagram): ' + CAST(LEN(@Projeto3Instagram) as nvarchar(max));
				PRINT N'@Projeto3Twitter (Twitter): ' + CAST(LEN(@Projeto3Twitter) as nvarchar(max));
				PRINT N'@Projeto3Youtube (Youtube): ' + CAST(LEN(@Projeto3Youtube) as nvarchar(max));

				INSERT INTO [dbo].[MusicBands] 
					([Uid], [MusicBandTypeId], [Name], [ImageUrl], [FormationDate], [MainMusicInfluences], [Facebook] ,[Instagram], [Twitter], [Youtube], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
				VALUES
					(NEWID(), @MusicBandTypeId, @Projeto3NomeDoArtista, @Projeto3Foto, @Projeto3AnoDeInicio, @Projeto3Influencias, @Projeto3FanpageNoFacebook, @Projeto3Instagram, @Projeto3Twitter, @Projeto3Youtube, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

				SELECT @MusicBandId = [Id] FROM dbo.MusicBands WHERE Name = @Projeto3NomeDoArtista;
				IF (@MusicBandId IS NULL)
				BEGIN
					UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4002', ErrorMessage = 'The music band could not be created.' WHERE Id = @id;
					GOTO ERROR_NEXTFETCH;
				END;
			END;

			-- Attendee Music Bands
			SELECT @AttendeeMusicBandId = [Id] FROM dbo.AttendeeMusicBands WHERE MusicBandId = @MusicBandId AND EditionId = @EditionId;
			IF (@AttendeeMusicBandId IS NULL)
			BEGIN
				INSERT INTO [dbo].[AttendeeMusicBands]
					([Uid], [EditionId], [MusicBandId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
				VALUES
					(NEWID(), @EditionId, @MusicBandId, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

				SELECT @AttendeeMusicBandId = [Id] FROM dbo.AttendeeMusicBands WHERE MusicBandId = @MusicBandId AND EditionId = @EditionId;
				IF (@AttendeeMusicBandId IS NULL)
				BEGIN
					UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4003', ErrorMessage = 'The attendee music band could not be created.' WHERE Id = @id;
					GOTO ERROR_NEXTFETCH;
				END
			END;

			-- Attendee Music Band Collaborators
			SELECT @AttendeeMusicBandCollaboratorId = [Id] FROM dbo.AttendeeMusicBandCollaborators WHERE AttendeeMusicBandId = @AttendeeMusicBandId AND AttendeeCollaboratorId = @AttendeeCollaboratorId;
			IF (@AttendeeMusicBandCollaboratorId IS NULL)
			BEGIN
				INSERT INTO [dbo].[AttendeeMusicBandCollaborators]
					([Uid], [AttendeeMusicBandId], [AttendeeCollaboratorId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
				VALUES
					(NEWID(), @AttendeeMusicBandId, @AttendeeCollaboratorId, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

				SELECT @AttendeeMusicBandCollaboratorId = [Id] FROM dbo.AttendeeMusicBandCollaborators WHERE AttendeeMusicBandId = @AttendeeMusicBandId AND AttendeeCollaboratorId = @AttendeeCollaboratorId;
				IF (@AttendeeMusicBandCollaboratorId IS NULL)
				BEGIN
					UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4004', ErrorMessage = 'The attendee music band collaborator could not be created.' WHERE Id = @id;
					GOTO ERROR_NEXTFETCH;
				END
			END;

			-- Music Projects
			SELECT @MusicProjectId = [Id] FROM dbo.MusicProjects WHERE AttendeeMusicBandId = @AttendeeMusicBandId AND Music1Url = @Projeto3Musica1 AND Music2Url = @Projeto3Musica2;
			IF (@MusicProjectId IS NULL)
			BEGIN
				PRINT N' ';
				PRINT N'@Projeto3VideoClip (VideoUrl): ' + CAST(LEN(@Projeto3VideoClip) as nvarchar(max));
				PRINT N'@Projeto3Musica1 (Music1Url): ' + CAST(LEN(@Projeto3Musica1) as nvarchar(max));
				PRINT N'@Projeto3Musica2 (Music2Url): ' + CAST(LEN(@Projeto3Musica2) as nvarchar(max));
				PRINT N'@Projeto3Release (Release): ' + CAST(LEN(@Projeto3Release) as nvarchar(max));
				PRINT N'@Projeto3Clipping1 (Clipping1): ' + CAST(LEN(@Projeto3Clipping1) as nvarchar(max));
				PRINT N'@Projeto3Clipping2 (Clipping2): ' + CAST(LEN(@Projeto3Clipping2) as nvarchar(max));
				PRINT N'@Projeto3Clipping3 (Clipping3): ' + CAST(LEN(@Projeto3Clipping3) as nvarchar(max));

				INSERT INTO [dbo].[MusicProjects]
					([Uid], [AttendeeMusicBandId], [VideoUrl], [Music1Url], [Music2Url], [Release], [Clipping1], [Clipping2], [Clipping3], [ProjectEvaluationStatusId], [ProjectEvaluationRefuseReasonId], [Reason], [EvaluationUserId], [EvaluationEmailSendDate], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
				VALUES
					(NEWID(), @AttendeeMusicBandId, @Projeto3VideoClip, @Projeto3Musica1, @Projeto3Musica2, @Projeto3Release, @Projeto3Clipping1, @Projeto3Clipping2, @Projeto3Clipping3, @ProjectEvaluationStatusId, NULL, NULL, NULL, NULL, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

				SELECT @MusicProjectId = [Id] FROM dbo.MusicProjects WHERE AttendeeMusicBandId = @AttendeeMusicBandId AND Music1Url = @Projeto3Musica1 AND Music2Url = @Projeto3Musica2;
				IF (@MusicProjectId IS NULL)
				BEGIN
					UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4005', ErrorMessage = 'The music project could not be created.' WHERE Id = @id;
					GOTO ERROR_NEXTFETCH;
				END
			END;

			-- Music Band Genres
			IF (@Projeto3Estilos IS NOT NULL)
			BEGIN
				SELECT @MusicBandGenre = NULL;

				-- Cursor
				DECLARE musicBandGendersCursor CURSOR local fast_forward FOR  
				SELECT val FROM [dbo].[Split](@Projeto3Estilos, ',');

				OPEN musicBandGendersCursor;

				FETCH NEXT FROM musicBandGendersCursor 
				INTO @MusicBandGenre;

				WHILE @@FETCH_STATUS = 0
				BEGIN
					SELECT @MusicGenreId = NULL;
					SELECT @MusicBandGenreId = NULL;

					IF (@MusicBandGenre IS NOT NULL) 
					BEGIN
						SELECT @MusicGenreId = [Id] FROM dbo.MusicGenres WHERE Name = @MusicBandGenre;
						IF (@MusicGenreId IS NOT NULL)
						BEGIN
							SELECT @MusicBandGenreId = [Id] FROM dbo.MusicBandGenres WHERE MusicBandId = @MusicBandId AND MusicGenreId = @MusicGenreId;
							IF (@MusicBandGenreId IS NULL)
							BEGIN
								INSERT INTO [dbo].[MusicBandGenres]
								([Uid], [MusicBandId], [MusicGenreId], [AdditionalInfo], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
								VALUES
								(NEWID(), @MusicBandId, @MusicGenreId, NULL, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

								SELECT @MusicBandGenreId = [Id] FROM dbo.MusicBandGenres WHERE MusicBandId = @MusicBandId AND MusicGenreId = @MusicGenreId;
								IF (@MusicBandGenreId IS NULL)
								BEGIN
									UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4006', ErrorMessage = 'The music band genre could not be created.' WHERE Id = @id;
									GOTO ERROR_NEXTFETCH;
								END
							END;
						END;
					END;

					FETCH NEXT FROM musicBandGendersCursor 
					INTO @MusicBandGenre;
				END;

				CLOSE musicBandGendersCursor;
				DEALLOCATE musicBandGendersCursor;
			END;

			-- Music Band Target Audiences
			IF (@Projeto3PublicoAlvo IS NOT NULL)
			BEGIN
				SELECT @TargetAudienceId = [Id] FROM dbo.TargetAudiences WHERE ProjectTypeId = 2 AND  Name = CASE
																									 	 		WHEN LOWER(@Projeto3PublicoAlvo) = 'adult' THEN 'Adulto | Adult'
																									 	 		WHEN LOWER(@Projeto3PublicoAlvo) = 'young' THEN 'Jovem | Young Adults'
																									 	 		WHEN LOWER(@Projeto3PublicoAlvo) = 'children' THEN 'Infantil | Children'
																									 	 		ELSE 'NOT CONFIGURED'
																									 		 END;
				IF (@TargetAudienceId IS NOT NULL)
				BEGIN
					SELECT @MusicBandTargetAudienceId = [Id] FROM dbo.MusicBandTargetAudiences WHERE MusicBandId = @MusicBandId AND TargetAudienceId = @TargetAudienceId;
					IF (@MusicBandTargetAudienceId IS NULL)
					BEGIN
						INSERT INTO [dbo].[MusicBandTargetAudiences]
							([Uid], [MusicBandId], [TargetAudienceId], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @TargetAudienceId, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandTargetAudienceId = [Id] FROM dbo.MusicBandTargetAudiences WHERE MusicBandId = @MusicBandId AND TargetAudienceId = @TargetAudienceId;
						IF (@MusicBandTargetAudienceId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4007', ErrorMessage = 'The music band target audience could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END

					END;
				END;
			END;

			-- Released Music Projects
			BEGIN
				-- Release 1
				SELECT @ReleasedMusicProjectId = NULL;

				IF (NULLIF(@Projeto3Release1Nome, '') IS NOT NULL AND NULLIF(@Projeto3Release1Ano, '') IS NOT NULL)
				BEGIN
					SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Release1Nome AND [Year] = @Projeto3Release1Ano;
					IF (@ReleasedMusicProjectId IS NULL)
					BEGIN
						INSERT INTO [dbo].[ReleasedMusicProjects]
							([Uid], [MusicBandId], [Name], [Year], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto3Release1Nome, @Projeto3Release1Ano, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Release1Nome AND [Year] = @Projeto3Release1Ano;
						IF (@ReleasedMusicProjectId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4008', ErrorMessage = 'The release 1 music project could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Release 2
				SELECT @ReleasedMusicProjectId = NULL;

				IF (NULLIF(@Projeto3Release2Nome, '') IS NOT NULL AND NULLIF(@Projeto3Release2Ano, '') IS NOT NULL)
				BEGIN
					SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Release2Nome AND [Year] = @Projeto3Release2Ano;
					IF (@ReleasedMusicProjectId IS NULL)
					BEGIN
						INSERT INTO [dbo].[ReleasedMusicProjects]
							([Uid], [MusicBandId], [Name], [Year], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto3Release2Nome, @Projeto3Release2Ano, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Release2Nome AND [Year] = @Projeto3Release2Ano;
						IF (@ReleasedMusicProjectId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4009', ErrorMessage = 'The release 2 music project could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Release 3
				SELECT @ReleasedMusicProjectId = NULL;

				IF (NULLIF(@Projeto3Release3Nome, '') IS NOT NULL AND NULLIF(@Projeto3Release3Ano, '') IS NOT NULL)
				BEGIN
					SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Release3Nome AND [Year] = @Projeto3Release3Ano;
					IF (@ReleasedMusicProjectId IS NULL)
					BEGIN
						INSERT INTO [dbo].[ReleasedMusicProjects]
							([Uid], [MusicBandId], [Name], [Year], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto3Release3Nome, @Projeto3Release3Ano, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Release3Nome AND [Year] = @Projeto3Release3Ano;
						IF (@ReleasedMusicProjectId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4010', ErrorMessage = 'The release 3 music project could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Release 4
				SELECT @ReleasedMusicProjectId = NULL;

				IF (NULLIF(@Projeto3Release4Nome, '') IS NOT NULL AND NULLIF(@Projeto3Release4Ano, '') IS NOT NULL)
				BEGIN
					SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Release4Nome AND [Year] = @Projeto3Release4Ano;
					IF (@ReleasedMusicProjectId IS NULL)
					BEGIN
						INSERT INTO [dbo].[ReleasedMusicProjects]
							([Uid], [MusicBandId], [Name], [Year], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto3Release4Nome, @Projeto3Release4Ano, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Release4Nome AND [Year] = @Projeto3Release4Ano;
						IF (@ReleasedMusicProjectId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4011', ErrorMessage = 'The release 4 music project could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Release 5
				SELECT @ReleasedMusicProjectId = NULL;

				IF (NULLIF(@Projeto3Release5Nome, '') IS NOT NULL AND NULLIF(@Projeto3Release5Ano, '') IS NOT NULL)
				BEGIN
					SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Release5Nome AND [Year] = @Projeto3Release5Ano;
					IF (@ReleasedMusicProjectId IS NULL)
					BEGIN
						INSERT INTO [dbo].[ReleasedMusicProjects]
							([Uid], [MusicBandId], [Name], [Year], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto3Release5Nome, @Projeto3Release5Ano, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @ReleasedMusicProjectId = [Id] FROM dbo.ReleasedMusicProjects WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Release5Nome AND [Year] = @Projeto3Release5Ano;
						IF (@ReleasedMusicProjectId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4012', ErrorMessage = 'The release 5 music project could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;
			END;

			-- Music Band Members
			BEGIN
				-- Member 1
				SELECT @MusicBandMemberId = NULL;

				IF (NULLIF(@Projeto3Integrante1Nome, '') IS NOT NULL AND NULLIF(@Projeto3Integrante1Instrumento, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Integrante1Nome AND [MusicInstrumentName] = @Projeto3Integrante1Instrumento;
					IF (@MusicBandMemberId IS NULL)
					BEGIN
						INSERT INTO [dbo].[MusicBandMembers]
							([Uid], [MusicBandId], [Name], [MusicInstrumentName], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto3Integrante1Nome, @Projeto3Integrante1Instrumento, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Integrante1Nome AND [MusicInstrumentName] = @Projeto3Integrante1Instrumento;
						IF (@MusicBandMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4013', ErrorMessage = 'The music band member 1 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Member 2
				SELECT @MusicBandMemberId = NULL;

				IF (NULLIF(@Projeto3Integrante2Nome, '') IS NOT NULL AND NULLIF(@Projeto3Integrante2Instrumento, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Integrante2Nome AND [MusicInstrumentName] = @Projeto3Integrante2Instrumento;
					IF (@MusicBandMemberId IS NULL)
					BEGIN
						INSERT INTO [dbo].[MusicBandMembers]
							([Uid], [MusicBandId], [Name], [MusicInstrumentName], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto3Integrante2Nome, @Projeto3Integrante2Instrumento, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Integrante2Nome AND [MusicInstrumentName] = @Projeto3Integrante2Instrumento;
						IF (@MusicBandMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4014', ErrorMessage = 'The music band member 2 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Member 3
				SELECT @MusicBandMemberId = NULL;

				IF (NULLIF(@Projeto3Integrante3Nome, '') IS NOT NULL AND NULLIF(@Projeto3Integrante3Instrumento, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Integrante3Nome AND [MusicInstrumentName] = @Projeto3Integrante3Instrumento;
					IF (@MusicBandMemberId IS NULL)
					BEGIN
						INSERT INTO [dbo].[MusicBandMembers]
							([Uid], [MusicBandId], [Name], [MusicInstrumentName], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto3Integrante3Nome, @Projeto3Integrante3Instrumento, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Integrante3Nome AND [MusicInstrumentName] = @Projeto3Integrante3Instrumento;
						IF (@MusicBandMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4015', ErrorMessage = 'The music band member 3 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Member 4
				SELECT @MusicBandMemberId = NULL;

				IF (NULLIF(@Projeto3Integrante4Nome, '') IS NOT NULL AND NULLIF(@Projeto3Integrante4Instrumento, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Integrante4Nome AND [MusicInstrumentName] = @Projeto3Integrante4Instrumento;
					IF (@MusicBandMemberId IS NULL)
					BEGIN
						INSERT INTO [dbo].[MusicBandMembers]
							([Uid], [MusicBandId], [Name], [MusicInstrumentName], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto3Integrante4Nome, @Projeto3Integrante4Instrumento, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Integrante4Nome AND [MusicInstrumentName] = @Projeto3Integrante4Instrumento;
						IF (@MusicBandMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4016', ErrorMessage = 'The music band member 4 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Member 5
				SELECT @MusicBandMemberId = NULL;

				IF (NULLIF(@Projeto3Integrante5Nome, '') IS NOT NULL AND NULLIF(@Projeto3Integrante5Instrumento, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Integrante5Nome AND [MusicInstrumentName] = @Projeto3Integrante5Instrumento;
					IF (@MusicBandMemberId IS NULL)
					BEGIN
						INSERT INTO [dbo].[MusicBandMembers]
							([Uid], [MusicBandId], [Name], [MusicInstrumentName], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
						VALUES
							(NEWID(), @MusicBandId, @Projeto3Integrante5Nome, @Projeto3Integrante5Instrumento, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandMemberId = [Id] FROM dbo.MusicBandMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Integrante5Nome AND [MusicInstrumentName] = @Projeto3Integrante5Instrumento;
						IF (@MusicBandMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4017', ErrorMessage = 'The music band member 5 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;
			END;

			-- Music Band Teams
			BEGIN
				-- Team 1
				SELECT @MusicBandTeamMemberId = NULL;

				IF (NULLIF(@Projeto3Time1Nome, '') IS NOT NULL AND NULLIF(@Projeto3Time1Funcao, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Time1Nome AND [Role] = @Projeto3Time1Funcao;
					IF (@MusicBandTeamMemberId IS NULL)
					BEGIN
					INSERT INTO [dbo].[MusicBandTeamMembers]
						([Uid], [MusicBandId], [Name], [Role], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
					VALUES
						(NEWID(), @MusicBandId, @Projeto3Time1Nome, @Projeto3Time1Funcao, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Time1Nome AND [Role] = @Projeto3Time1Funcao;
						IF (@MusicBandTeamMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4018', ErrorMessage = 'The music band team member 1 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Team 2
				SELECT @MusicBandTeamMemberId = NULL;

				IF (NULLIF(@Projeto3Time2Nome, '') IS NOT NULL AND NULLIF(@Projeto3Time2Funcao, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Time2Nome AND [Role] = @Projeto3Time2Funcao;
					IF (@MusicBandTeamMemberId IS NULL)
					BEGIN
					INSERT INTO [dbo].[MusicBandTeamMembers]
						([Uid], [MusicBandId], [Name], [Role], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
					VALUES
						(NEWID(), @MusicBandId, @Projeto3Time2Nome, @Projeto3Time2Funcao, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Time2Nome AND [Role] = @Projeto3Time2Funcao;
						IF (@MusicBandTeamMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4019', ErrorMessage = 'The music band team member 2 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Team 3
				SELECT @MusicBandTeamMemberId = NULL;

				IF (NULLIF(@Projeto3Time3Nome, '') IS NOT NULL AND NULLIF(@Projeto3Time3Funcao, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Time3Nome AND [Role] = @Projeto3Time3Funcao;
					IF (@MusicBandTeamMemberId IS NULL)
					BEGIN
					INSERT INTO [dbo].[MusicBandTeamMembers]
						([Uid], [MusicBandId], [Name], [Role], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
					VALUES
						(NEWID(), @MusicBandId, @Projeto3Time3Nome, @Projeto3Time3Funcao, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Time3Nome AND [Role] = @Projeto3Time3Funcao;
						IF (@MusicBandTeamMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4020', ErrorMessage = 'The music band team member 3 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Team 4
				SELECT @MusicBandTeamMemberId = NULL;

				IF (NULLIF(@Projeto3Time4Nome, '') IS NOT NULL AND NULLIF(@Projeto3Time4Funcao, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Time4Nome AND [Role] = @Projeto3Time4Funcao;
					IF (@MusicBandTeamMemberId IS NULL)
					BEGIN
					INSERT INTO [dbo].[MusicBandTeamMembers]
						([Uid], [MusicBandId], [Name], [Role], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
					VALUES
						(NEWID(), @MusicBandId, @Projeto3Time4Nome, @Projeto3Time4Funcao, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Time4Nome AND [Role] = @Projeto3Time4Funcao;
						IF (@MusicBandTeamMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4021', ErrorMessage = 'The music band team member 4 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;

				-- Team 5
				SELECT @MusicBandTeamMemberId = NULL;

				IF (NULLIF(@Projeto3Time5Nome, '') IS NOT NULL AND NULLIF(@Projeto3Time5Funcao, '') IS NOT NULL)
				BEGIN
					SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Time5Nome AND [Role] = @Projeto3Time5Funcao;
					IF (@MusicBandTeamMemberId IS NULL)
					BEGIN
					INSERT INTO [dbo].[MusicBandTeamMembers]
						([Uid], [MusicBandId], [Name], [Role], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId])
					VALUES
						(NEWID(), @MusicBandId, @Projeto3Time5Nome, @Projeto3Time5Funcao, 0, GETUTCDATE(), @UserId, GETUTCDATE(), @UserId);

						SELECT @MusicBandTeamMemberId = [Id] FROM dbo.MusicBandTeamMembers WHERE [MusicBandId] = @MusicBandId AND [Name] = @Projeto3Time5Nome AND [Role] = @Projeto3Time5Funcao;
						IF (@MusicBandTeamMemberId IS NULL)
						BEGIN
							UPDATE dbo.pitching_show_submissions SET IsProcessed = 0, ErrorCode = '4022', ErrorMessage = 'The music band team member 5 could not be created.' WHERE Id = @id;
							GOTO ERROR_NEXTFETCH;
						END
					END;
				END;
			END;
		END;

		-- Update processing properties -----------------------------------------------------------------
		SUCCESS_NEXTFETCH:

		UPDATE dbo.pitching_show_submissions SET IsProcessed = 1 WHERE Id = @id;

		ERROR_NEXTFETCH:

		FETCH NEXT FROM pitchingShowSubmissionsCursor 
		INTO 
			@Id,
			@Nome,
			@CPFCNPJ,
			@Endereco,
			@CEP,
			@Pais,
			@Estado,
			@Cidade,
			@Telefone,
			@Celular,
			@Email,
			@Projeto1Time1Nome,
			@Projeto1Time1Funcao,
			@Projeto1Time2Nome,
			@Projeto1Time2Funcao,
			@Projeto1Time3Nome,
			@Projeto1Time3Funcao,
			@Projeto1Time4Nome,
			@Projeto1Time4Funcao,
			@Projeto1Time5Nome,
			@Projeto1Time5Funcao,
			@Projeto1Foto,
			@Projeto1Estilos,
			@Projeto1Musica1,
			@Projeto1Musica2,
			@Projeto1Release,
			@Projeto1Twitter,
			@Projeto1Youtube,
			@Projeto1Instagram,
			@Projeto1Release1Nome,
			@Projeto1Release1Ano,
			@Projeto1Release2Nome,
			@Projeto1Release2Ano,
			@Projeto1Release3Nome,
			@Projeto1Release3Ano,
			@Projeto1Release4Nome,
			@Projeto1Release4Ano,
			@Projeto1Release5Nome,
			@Projeto1Release5Ano,
			@Projeto1Clipping1,
			@Projeto1Clipping2,
			@Projeto1Clipping3,
			@Projeto1Integrante1Nome,
			@Projeto1Integrante1Instrumento,
			@Projeto1Integrante2Nome,
			@Projeto1Integrante2Instrumento,
			@Projeto1Integrante3Nome,
			@Projeto1Integrante3Instrumento,
			@Projeto1Integrante4Nome,
			@Projeto1Integrante4Instrumento,
			@Projeto1Integrante5Nome,
			@Projeto1Integrante5Instrumento,
			@Projeto1AnoDeInicio,
			@Projeto1Influencias,
			@Projeto1VideoClip,
			@Projeto1NomeDoArtista,
			@Projeto1TipoDoArtista,
			@Projeto1PublicoAlvo,
			@Projeto1FanpageNoFacebook,
			@Projeto2Time1Nome,
			@Projeto2Time1Funcao,
			@Projeto2Time2Nome,
			@Projeto2Time2Funcao,
			@Projeto2Time3Nome,
			@Projeto2Time3Funcao,
			@Projeto2Time4Nome,
			@Projeto2Time4Funcao,
			@Projeto2Time5Nome,
			@Projeto2Time5Funcao,
			@Projeto2Foto,
			@Projeto2Estilos,
			@Projeto2Musica1,
			@Projeto2Musica2,
			@Projeto2Release,
			@Projeto2Twitter,
			@Projeto2Youtube,
			@Projeto2Instagram,
			@Projeto2Release1Nome,
			@Projeto2Release1Ano,
			@Projeto2Release2Nome,
			@Projeto2Release2Ano,
			@Projeto2Release3Nome,
			@Projeto2Release3Ano,
			@Projeto2Release4Nome,
			@Projeto2Release4Ano,
			@Projeto2Release5Nome,
			@Projeto2Release5Ano,
			@Projeto2Clipping1,
			@Projeto2Clipping2,
			@Projeto2Clipping3,
			@Projeto2Integrante1Nome,
			@Projeto2Integrante1Instrumento,
			@Projeto2Integrante2Nome,
			@Projeto2Integrante2Instrumento,
			@Projeto2Integrante3Nome,
			@Projeto2Integrante3Instrumento,
			@Projeto2Integrante4Nome,
			@Projeto2Integrante4Instrumento,
			@Projeto2Integrante5Nome,
			@Projeto2Integrante5Instrumento,
			@Projeto2AnoDeInicio,
			@Projeto2Influencias,
			@Projeto2VideoClip,
			@Projeto2NomeDoArtista,
			@Projeto2TipoDoArtista,
			@Projeto2PublicoAlvo,
			@Projeto2FanpageNoFacebook,
			@Projeto3Time1Nome,
			@Projeto3Time1Funcao,
			@Projeto3Time2Nome,
			@Projeto3Time2Funcao,
			@Projeto3Time3Nome,
			@Projeto3Time3Funcao,
			@Projeto3Time4Nome,
			@Projeto3Time4Funcao,
			@Projeto3Time5Nome,
			@Projeto3Time5Funcao,
			@Projeto3Foto,
			@Projeto3Estilos,
			@Projeto3Musica1,
			@Projeto3Musica2,
			@Projeto3Release,
			@Projeto3Twitter,
			@Projeto3Youtube,
			@Projeto3Instagram,
			@Projeto3Release1Nome,
			@Projeto3Release1Ano,
			@Projeto3Release2Nome,
			@Projeto3Release2Ano,
			@Projeto3Release3Nome,
			@Projeto3Release3Ano,
			@Projeto3Release4Nome,
			@Projeto3Release4Ano,
			@Projeto3Release5Nome,
			@Projeto3Release5Ano,
			@Projeto3Clipping1,
			@Projeto3Clipping2,
			@Projeto3Clipping3,
			@Projeto3Integrante1Nome,
			@Projeto3Integrante1Instrumento,
			@Projeto3Integrante2Nome,
			@Projeto3Integrante2Instrumento,
			@Projeto3Integrante3Nome,
			@Projeto3Integrante3Instrumento,
			@Projeto3Integrante4Nome,
			@Projeto3Integrante4Instrumento,
			@Projeto3Integrante5Nome,
			@Projeto3Integrante5Instrumento,
			@Projeto3AnoDeInicio,
			@Projeto3Influencias,
			@Projeto3VideoClip,
			@Projeto3NomeDoArtista,
			@Projeto3TipoDoArtista,
			@Projeto3PublicoAlvo,
			@Projeto3FanpageNoFacebook;
	END;

	CLOSE pitchingShowSubmissionsCursor;
	DEALLOCATE pitchingShowSubmissionsCursor;
END;
