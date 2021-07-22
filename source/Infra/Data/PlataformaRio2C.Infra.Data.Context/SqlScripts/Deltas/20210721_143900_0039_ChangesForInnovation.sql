--must run on deploy | test: yes, not done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION
		---------------------------------------------------------------------
		-- WorkDedications
		---------------------------------------------------------------------
		UPDATE WorkDedications SET Name = 'Parcial | Partial' 
		WHERE Uid = 'DCC9878D-EBC7-438C-8A0A-5952B75A8B54';
		UPDATE WorkDedications SET Name = 'Integral | Integral' 
		WHERE Uid = 'ADA0C122-45EF-41E4-9002-EDB9E9FBDB51';

		---------------------------------------------------------------------
		-- InnovationOrganizationExperienceOptions
		---------------------------------------------------------------------
		UPDATE InnovationOrganizationExperienceOptions SET Name = 'Recebeu apoio de incubadora/aceleradora | Received incubator/accelerator support' 
		WHERE Uid = '82167C1D-7CA6-447F-80C7-AE9188ADD436';
		UPDATE InnovationOrganizationExperienceOptions SET Name = 'Captou recursos para pesquisa e desenvolvimento de produtos/serviços tecnológicos | Raised resources for research and development of technological products/services' 
		WHERE Uid = '29B2CC2F-374D-4F2F-AC00-3513D02EC9C3';
		UPDATE InnovationOrganizationExperienceOptions SET Name = 'Se relacionou formalmente com grandes ou médias empresas | Formally related to large or medium-sized companies' 
		WHERE Uid = '2FD9F6BA-8852-4DD5-A402-DCD2C14923CB';
		UPDATE InnovationOrganizationExperienceOptions SET Name = 'Recebeu investimento de terceiros que envolveram parte do capital da empresa | Received investment from third parties that involved part of the company''s capital' 
		WHERE Uid = '60079B3B-A5D9-4E59-A964-725339AFBE7F';
		UPDATE InnovationOrganizationExperienceOptions SET Name = 'Nenhuma das opções acima | None of the above options' 
		WHERE Uid = '4F440536-BAB7-4E43-A3A4-F977ABAFBDA8';

		---------------------------------------------------------------------
		-- InnovationOrganizationObjectivesOptions 
		---------------------------------------------------------------------
		UPDATE InnovationOrganizationObjectivesOptions set Name = 'Desenvolvimento tecnológico de uma nova solução | Technological development of a new solution' 
		WHERE Uid = '9ECEFE6D-EA9A-4DCE-88A0-5B720E02EAE0';
		UPDATE InnovationOrganizationObjectivesOptions set Name = 'Aprimoramento tecnológico de uma solução existente | Technological improvement of an existing solution' 
		WHERE Uid = '0A0EA283-D9CB-4BCE-87B9-5582C57B6E42';
		UPDATE InnovationOrganizationObjectivesOptions set Name = 'Venda de produto/serviço/solução | Product/service/solution sale' 
		WHERE Uid = '0D2684E1-4A8A-4088-8547-6AC274EB1EE4';
		UPDATE InnovationOrganizationObjectivesOptions set Name = 'Oportunidade de conexão com médias/grandes empresas | Opportunity to connect with medium/large companies' 
		WHERE Uid = '064371D0-864F-4B79-B709-221F73B0D35D';
		UPDATE InnovationOrganizationObjectivesOptions set Name = 'Validação de ideias/protótipos | Validation of ideas/prototypes' 
		WHERE Uid = '38A07682-8BD4-4EDE-8BE2-1593BAD8E0B7';
		UPDATE InnovationOrganizationObjectivesOptions set Name = 'Oportunidade de investimento | Investment opportunity' 
		WHERE Uid = '5F872B5B-DA41-43E6-ABE7-CEF7E6BC0CE6';
		UPDATE InnovationOrganizationObjectivesOptions set Name = 'Acesso a serviços de incubação/aceleração | Access to incubation/acceleration services' 
		WHERE Uid = '1A144DB0-DBF2-4ECC-A4C0-267CEA374FAA';
		UPDATE InnovationOrganizationObjectivesOptions set Name = 'Outros | Others' 
		WHERE Uid = '5F62C762-01C0-4F55-A89D-D1E5690817F6';

		---------------------------------------------------------------------
		-- InnovationOrganizationTechnologyOptions 
		---------------------------------------------------------------------
		UPDATE InnovationOrganizationTechnologyOptions set Name = 'SAAS | SAAS' 
		WHERE Uid = '0EE805CD-7E63-47DE-8034-C405DC5E1DA3';
		UPDATE InnovationOrganizationTechnologyOptions set Name = 'AI | AI' 
		WHERE Uid = '6932AC40-E16D-4858-B550-1B4CD2F9461D';
		UPDATE InnovationOrganizationTechnologyOptions set Name = 'IOT | IOT' 
		WHERE Uid = '516E3187-CE60-4541-9F46-AC41F29EA0EB';
		UPDATE InnovationOrganizationTechnologyOptions set Name = 'Blockchain | Blockchain' 
		WHERE Uid = '9B3EFFFC-B4F9-4E65-B679-69EEE581DCC2';
		UPDATE InnovationOrganizationTechnologyOptions set Name = 'Robótica | Robotics' 
		WHERE Uid = '3663D8A3-DF1D-4A41-A3EF-13CF2602FA9D';
		UPDATE InnovationOrganizationTechnologyOptions set Name = 'Outros | Others' 
		WHERE Uid = 'F5B4623D-F4F9-4440-B575-140C273C41D2';

		---------------------------------------------------------------------
		-- MusicBandTypes 
		---------------------------------------------------------------------
		UPDATE MusicBandTypes set Name = 'Banda / Grupo Musical | Band / Musical Group' 
		WHERE Uid = 'DD8D2040-52D2-427B-962B-026B7B1C4604';
		UPDATE MusicBandTypes set Name = 'Artista Solo | Solo artist'
		WHERE Uid = '8B86B02C-179C-4C5B-B2DE-58066BEA209E';

		---------------------------------------------------------------------
		-- MusicGenres	
		---------------------------------------------------------------------
		UPDATE MusicGenres set Name = 'Soul | Soul' 
		WHERE Uid = '9A04295A-2E61-41D9-940F-686E9E87B4B4'
		UPDATE MusicGenres set Name = 'Blues | Blues' 
		WHERE Uid = 'D4E39AED-CCE7-498D-A234-E1D1EF542036'
		UPDATE MusicGenres set Name = 'Clássica | Classic' 
		WHERE Uid = 'D90FC220-9E9E-4E58-891C-61849D260300'
		UPDATE MusicGenres set Name = 'Country | Country' 
		WHERE Uid = '3C7F85A0-0D63-4D6B-A6F4-7C70E32195DC'
		UPDATE MusicGenres set Name = 'Forró | Forró' 
		WHERE Uid = '4044D656-E6EC-418C-9174-97EEFC91977B'
		UPDATE MusicGenres set Name = 'Funk | Funk' 
		WHERE Uid = '59D477E1-20C0-4B32-AF58-003E0C723A14'
		UPDATE MusicGenres set Name = 'Gospel | Gospel' 
		WHERE Uid = '99C7FD85-D42E-4DCD-B974-29E275D15E32'
		UPDATE MusicGenres set Name = 'Indie | Indie' 
		WHERE Uid = 'B3DDA8CE-1B7F-401C-8C73-E94D8FA22AA0'
		UPDATE MusicGenres set Name = 'Instrumental | Instrumental' 
		WHERE Uid = '21FDB552-E67E-4E6D-BE48-7E81A8A8D5B0'
		UPDATE MusicGenres set Name = 'Jazz | Jazz' 
		WHERE Uid = 'E2407520-62DD-419B-A918-3F01F8F881B9'
		UPDATE MusicGenres set Name = 'Kids | Kids'
		WHERE Uid = 'FCE5CDCA-A74E-4C9C-90D5-0DA4C351F579'
		UPDATE MusicGenres set Name = 'MPB | MPB' 
		WHERE Uid = 'D8070E89-48A0-47A6-83F0-0AB79EA153C1'
		UPDATE MusicGenres set Name = 'Eletrônica | Eletronic'
		WHERE Uid = '1E8F1594-95E5-42D0-85E6-DBF6EAEEAB55'
		UPDATE MusicGenres set Name = 'Pop Rock | Pop Rock' 
		WHERE Uid = '9E092C55-D4FD-47E6-9ED7-B2DF8EAC3370'
		UPDATE MusicGenres set Name = 'Punk Rock | Punk Rock'
		WHERE Uid = '58569CEF-2FB6-49F7-8A74-FC5BEAB28527'
		UPDATE MusicGenres set Name = 'Rap | Rap'
		WHERE Uid = '1C34BC4C-F51C-4DC1-BA3A-DE3BE2DB1F88'
		UPDATE MusicGenres set Name = 'Reggae | Reggae' 
		WHERE Uid = '5474000E-2A7D-44C9-8DD1-BD38E4649F73'
		UPDATE MusicGenres set Name = 'Reggaeton | Reggaeton'
		WHERE Uid = 'F3E1FBA6-C370-4AE0-9B2D-B08A8A34BF3B'
		UPDATE MusicGenres set Name = 'Heavy Metal | Heavy Metal'
		WHERE Uid = 'CAFC1D28-7477-4AB3-99F8-FDBD4281CB41'
		UPDATE MusicGenres set Name = 'Rock Progressivo | Progressive Rock' 
		WHERE Uid = '8C7E52D5-1E42-46C2-94BC-5AE7830D95C5'
		UPDATE MusicGenres set Name = 'Samba | Samba' 
		WHERE Uid = 'BF3DDDD0-230A-4B9C-98E5-9BD31F5E0E79'
		UPDATE MusicGenres set Name = 'Axé | Axé' 
		WHERE Uid = '1B3723DF-BEAA-4729-8D84-46919463ADE8'
		UPDATE MusicGenres set Name = 'Pagode | Pagode' 
		WHERE Uid = 'E76D7959-2757-4E30-A3EF-16AF2B4F9820'
		UPDATE MusicGenres set Name = 'Sertanejo | Sertanejo' 
		WHERE Uid = 'B3291C19-212A-46F5-814C-A7983927D4BA'
		UPDATE MusicGenres set Name = 'Surf Music | Surf Music' 
		WHERE Uid = 'DDD19940-7CC6-41FF-A492-E0BBFA48A5FD'
		UPDATE MusicGenres set Name = 'Techno Brega | Techno Brega'
		WHERE Uid = '141D6A5B-5610-424D-9779-710266FA38B0'
		UPDATE MusicGenres set Name = 'Outros | Others' 
		WHERE Uid = '668BC592-16FF-4CED-A2AA-829947E0CF68'

		---------------------------------------------------------------------
		-- InnovationOrganizationTrackOptions
		---------------------------------------------------------------------
		ALTER TABLE "InnovationOrganizationTrackOptions"
		ADD Description  varchar(1000) NULL;

		EXEC('
			UPDATE InnovationOrganizationTrackOptions 
			SET 
				Name = ''Mídia e Entretenimento | Media and Entertainment'',
				Description = ''Educação; gamification, plataforma de vídeo; publicidade; mídia social, VR/AR | Education; gamification, video platform; publicity; social media, VR/AR''
			WHERE Uid = ''702A9B2E-DBCB-4BB0-8405-C5BD72EAF627'';

			UPDATE InnovationOrganizationTrackOptions SET 
				Name = ''Otimização de Negócios | Business Optimization'',
				Description = ''Internet das coisas (IoT); SaaS/PaaS; Inteligência Artificial (AI); economia colaborativa; e-commerce; cibersegurança; business inteligence – dados (geração de valor em dados) | Internet of Things (IoT); SaaS/PaaS; Artificial Intelligence (AI); collaborative economy; e-commerce; cybersecurity; business intelligence – data (generating value in data)''
			WHERE Uid = ''0B1BD552-AD36-4A4D-A55D-D7B07EB6E4E0'';

			UPDATE InnovationOrganizationTrackOptions SET 
				Name = ''Fintech | Fintech'',
				Description = ''Blockchain/crypto; insurance tech; investimentos; pagamentos; banking; robôs de investimento | Blockchain/crypto; insurance technology; investments; payments; banking; investment robots''
			WHERE Uid = ''FB997A1A-CBF0-446B-82FF-FF7B5EAA21BF'';

			UPDATE InnovationOrganizationTrackOptions SET 
				Name = ''A nova sociedade | The new society'', 
				Description = ''Construtech, smatcities, mercado imobiliário,tecnologia limpa; sustentabilidade social e ambiental; amplo acesso àtecnologia; políticas públicas; questões sociais; sistema de inovação; atendimento a família; comunidade | Construtech, smartcities, real estate, clean technology; social and environmental sustainability; broad access to technology; public policy; social questions; innovation system; family care; community''
			WHERE Uid = ''1646A4D0-F43A-4633-9730-3F8893A627CE'';

			UPDATE InnovationOrganizationTrackOptions SET 
				Name = ''Saúde e Bem estar | Health and wellness'',
				Description = ''Diagnósticos; computer visioning para diagnósticos; devices de health tech, inteligência artificial para saúde,wellness e mindfullness; biotech | Diagnostics; computer visioning for diagnostics; health tech devices, artificial intelligence for health, wellness and mindfullness; biotech''
			WHERE Uid = ''A7EEDEF9-88B9-4EF4-A6EA-32EA5DB28598'';

			UPDATE InnovationOrganizationTrackOptions SET 
				Name = ''Alimentação | Food'',
				Description = ''Agritech, foodtech (dietas personalizadas, dietas genéticas, comida funcional); farmtech; inovação em restaurantes e supermercados | Agritech, foodtech (custom diets, genetic diets, functional food); farmtech; innovation in restaurants and supermarkets''
			WHERE Uid = ''B05FDB02-F880-49D0-818D-65BA716EC0B8'';
		');

		ALTER TABLE "InnovationOrganizationTrackOptions"
		ALTER COLUMN Description  varchar(1000) NOT NULL;

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
