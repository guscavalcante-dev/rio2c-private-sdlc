BEGIN TRY
    BEGIN TRANSACTION

        -- 3. Insert the new InterestGroup “Subgênero” with ProjectTypeId = 5
      
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
            ('55FD72C1-B3D5-4DD5-8A3E-44B1B0BAF5A3'  -- Uid
            , (select TOP 1 id from [ProjectTypes] where uid ='E5B7CBA4-10B5-4FCF-A778-09B40AEB6C01')-- ProjectTypeId
            ,'Subgênero'                           -- Name
            ,'Multiple'                            -- Type
            ,1                                     -- DisplayOrder
            ,0                                     -- IsDeleted
            ,SYSDATETIMEOFFSET()                   -- CreateDate
            ,1                                     -- CreateUserId
            ,SYSDATETIMEOFFSET()                   -- UpdateDate
            ,1                                     -- UpdateUserId
            ,0);                                   -- IsCommission


        -- 4. Insert new Interests linked to the InterestGroup (Subgênero)
        DECLARE @InterestGroupId INT = (SELECT TOP 1 [Id] FROM [dbo].[InterestGroups] WHERE [UID] = '55FD72C1-B3D5-4DD5-8A3E-44B1B0BAF5A3');

        INSERT INTO [dbo].[Interests]
            ([Uid], [InterestGroupId], [Name], [DisplayOrder], [IsDeleted], [CreateDate], [CreateUserId], [UpdateDate], [UpdateUserId], [HasAdditionalInfo], [Description])
        VALUES
            ('9BA3EBAF-803C-41A2-A76C-C19E46342F01', @InterestGroupId, 'Ação | Action', 1, 0, SYSDATETIMEOFFSET(), 1, SYSDATETIMEOFFSET(), 1, 0, NULL),
            ('4E74DD5A-7A41-4986-98F6-88BFB5A229EE', @InterestGroupId, 'Arte/Cultura | Art/Culture', 2, 0, SYSDATETIMEOFFSET(), 1, SYSDATETIMEOFFSET(), 1, 0, NULL),
            ('AD5C8E3A-6581-4701-83DA-36E493F429C3', @InterestGroupId, 'Aventura | Adventure', 3, 0, SYSDATETIMEOFFSET(), 1, SYSDATETIMEOFFSET(), 1, 0, NULL),
            ('1B47D798-F79E-4D31-897E-0C01BCE34D98', @InterestGroupId, 'Biografia | Biography', 4, 0, SYSDATETIMEOFFSET(), 1, SYSDATETIMEOFFSET(), 1, 0, NULL),
            ('6A88B927-B547-4399-8D89-54476F5CBE38', @InterestGroupId, 'Ciência/Educação | Science/Education', 5, 0, SYSDATETIMEOFFSET(), 1, SYSDATETIMEOFFSET(), 1, 0, NULL),
            ('E9783DFE-C6DE-4DA9-98D5-901ABBC3D117', @InterestGroupId, 'Comédia | Comedy', 6, 0, SYSDATETIMEOFFSET(), 1, SYSDATETIMEOFFSET(), 1, 0, NULL),
            ('9A94F8C1-A813-44A1-BC52-CAEA3ACD70FD', @InterestGroupId, 'Drama | Drama', 7, 0, SYSDATETIMEOFFSET(), 1, SYSDATETIMEOFFSET(), 1, 0, NULL),
            ('7FD69A37-53A3-4A3C-8737-AB71A871A432', @InterestGroupId, 'Esportes | Sports', 8, 0, SYSDATETIMEOFFSET(), 1, SYSDATETIMEOFFSET(), 1, 0, NULL),
            ('34D927F5-8836-4A84-9B01-932D68AC032D', @InterestGroupId, 'Factual Life Style | Factual Lifestyle', 9, 0, SYSDATETIMEOFFSET(), 1, SYSDATETIMEOFFSET(), 1, 0, NULL),
            ('BA4790DF-DC85-48F7-BD45-CB3DE1A403A8', @InterestGroupId, 'Faroeste | Western', 10, 0, SYSDATETIMEOFFSET(), 1, SYSDATETIMEOFFSET(), 1, 0, NULL),
            ('DD3FB774-828A-44F7-A8CC-3BDAF9C41572', @InterestGroupId, 'Ficção Científica | Science Fiction', 11, 0, SYSDATETIMEOFFSET(), 1, SYSDATETIMEOFFSET(), 1, 0, NULL),
            ('D221A7DC-2F71-4073-A2FD-87071659DF96', @InterestGroupId, 'Gastronomia | Gastronomy', 12, 0, SYSDATETIMEOFFSET(), 1, SYSDATETIMEOFFSET(), 1, 0, NULL),
            ('AA9B1DE3-8177-4DC5-8497-B87317EEDDE9', @InterestGroupId, 'Música | Music', 13, 0, SYSDATETIMEOFFSET(), 1, SYSDATETIMEOFFSET(), 1, 0, NULL),
            ('D57AF9CC-3FE4-49C3-9F87-03A5B5B73E90', @InterestGroupId, 'Policial | Crime', 14, 0, SYSDATETIMEOFFSET(), 1, SYSDATETIMEOFFSET(), 1, 0, NULL),
            ('5E21AD74-83C9-4E66-9287-2E0AA55B4037', @InterestGroupId, 'Romance | Romance', 15, 0, SYSDATETIMEOFFSET(), 1, SYSDATETIMEOFFSET(), 1, 0, NULL),
            ('BF9A83A7-84E1-4CB3-9B8E-81624F163E58', @InterestGroupId, 'Suspense/Mistério | Thriller/Mystery', 16, 0, SYSDATETIMEOFFSET(), 1, SYSDATETIMEOFFSET(), 1, 0, NULL),
            ('AFD91B89-4C95-4815-88B8-121FE343E6E2', @InterestGroupId, 'Terror | Horror', 17, 0, SYSDATETIMEOFFSET(), 1, SYSDATETIMEOFFSET(), 1, 0, NULL),
            ('C2E55E45-3D3E-4DA9-AD1B-6859F27477D5', @InterestGroupId, 'True Crime | True Crime', 18, 0, SYSDATETIMEOFFSET(), 1, SYSDATETIMEOFFSET(), 1, 0, NULL),
            ('A0973CC2-B0C5-43DE-8A4D-FD0F4A718821', @InterestGroupId, 'Viagem | Travel', 19, 0, SYSDATETIMEOFFSET(), 1, SYSDATETIMEOFFSET(), 1, 0, NULL);
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    DECLARE @ErrorMessage NVARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
    SELECT @ErrorMessage = ERROR_MESSAGE(), @ErrorSeverity = ERROR_SEVERITY(), @ErrorState = ERROR_STATE();
    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH;
