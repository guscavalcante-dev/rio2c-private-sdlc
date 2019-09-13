--must run on deploy | test: yes, done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

ALTER TABLE "dbo"."Activities"
ADD DisplayOrder  int  NULL
go

ALTER TABLE "dbo"."TargetAudiences"
ADD DisplayOrder  int  NULL
go

ALTER TABLE "dbo"."InterestGroups"
ADD DisplayOrder  int  NULL
go

ALTER TABLE "dbo"."Interests"
ADD DisplayOrder  int  NULL
go


DELETE FROM dbo.OrganizationInterests WHERE InterestId IN (SELECT Id FROM dbo.Interests WHERE InterestGroupId = (SELECT Id FROM dbo.InterestGroups WHERE Name = 'Público alvo | Target audience'))
GO
DELETE FROM dbo.Interests WHERE InterestGroupId = (SELECT Id FROM dbo.InterestGroups WHERE Name = 'Público alvo | Target audience')
GO
DELETE FROM dbo.InterestGroups WHERE Name = 'Público alvo | Target audience'
GO



UPDATE [dbo].[InterestGroups] SET DisplayOrder = 1 WHERE [Uid] = 'd45503a8-40d9-4cd8-8db3-d76f2f24fae7'
GO
UPDATE [dbo].[InterestGroups] SET DisplayOrder = 4 WHERE [Uid] = '2d5ae955-8d8f-4763-aee4-964980ffb170'
GO
UPDATE [dbo].[InterestGroups] SET DisplayOrder = 5 WHERE [Uid] = '7b4a7c4a-ef10-483c-8854-87ebeb883583'
GO
UPDATE [dbo].[InterestGroups] SET DisplayOrder = 3 WHERE [Uid] = '6590e0f1-b8da-45d0-be2c-e4b7ccf3751b'
GO
UPDATE [dbo].[InterestGroups] SET DisplayOrder = 2 WHERE [Uid] = '379a311f-3bec-4a9f-8e6e-539b1fd8ab87'
GO
UPDATE [dbo].[InterestGroups] SET DisplayOrder = 6 WHERE [Uid] = 'bbfa501d-a4d2-4500-8d7d-8a133685e6d2'
GO


UPDATE [dbo].[Interests] SET DisplayOrder = 1 Where [Uid] = '435f9446-3b7e-418b-b8f8-0bade09a15f9'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 2 Where [Uid] = '6a900079-1ba6-4cbe-a5ce-4dd25191a024'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 1 Where [Uid] = '87650c72-bd0c-4a56-820a-5afe6482d811'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 2 Where [Uid] = '5469497b-96f7-4d65-9389-a426f4571ffe'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 3 Where [Uid] = 'e0a815cb-347f-41d3-a9ef-96552e38388b'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 4 Where [Uid] = '375fdb6e-ff12-4bcf-a8ad-d6bdd47a2eb1'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 5 Where [Uid] = '3ec6464b-c7d7-4892-8589-ac79c3a01ce3'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 6 Where [Uid] = '22be4472-1ca4-4b83-89c6-75e4723f8dad'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 7 Where [Uid] = '067125a9-7905-4ceb-a61a-8bf73e4ad609'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 8 Where [Uid] = '244514b5-035e-42cf-9eab-8bb8ec652c4a'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 9 Where [Uid] = 'e477b8ee-16b2-4519-97cb-ea043afab445'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 10 Where [Uid] = '3ec67bb6-04ec-4332-9603-0be3a636acd3'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 11 Where [Uid] = '4be84282-b81f-44e0-80d6-d87a0e12b541'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 12 Where [Uid] = '73343db6-250c-42bd-8507-dfecb109cb5b'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 1 Where [Uid] = '150b21eb-b796-4a85-b3c4-ac76cc23e5c2'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 2 Where [Uid] = 'a875b4b0-4eba-45f9-a553-53d3aa445738'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 3 Where [Uid] = '662dbd0f-00da-4346-b39f-e6abf612fd03'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 4 Where [Uid] = '598436b7-8b6e-4b03-b02f-3dc97fe9567b'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 5 Where [Uid] = 'ace340eb-1171-4fde-b255-0c6dae83bf06'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 1 Where [Uid] = '2f0a9f48-6537-4194-a07a-838be23bc18b'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 2 Where [Uid] = '0cc4a618-e774-4ebd-b929-dde6cba151c0'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 3 Where [Uid] = '974759c4-3a1c-4bdb-a401-282fe3ae35d5'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 5 Where [Uid] = 'ebc31ded-127d-4b04-a379-5cf22c4dc77c'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 4 Where [Uid] = '6a306849-63b1-47a7-b935-983078eba09f'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 4 Where [Uid] = 'f8c1da23-8934-471c-aa74-c0688f812d8d'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 5 Where [Uid] = '15feb98b-8e47-4c1a-971f-a827db7cf120'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 1 Where [Uid] = '2f7d3d66-32b1-43be-b7d8-43e6da698c64'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 3 Where [Uid] = 'a636e125-a6e5-4261-8e4b-b2c93e29ccf9'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 2 Where [Uid] = '994e0e73-2470-4af3-b9ad-ec6424927346'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 1 Where [Uid] = '8b51ae1f-c285-444c-a835-e8db66e28d3d'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 2 Where [Uid] = 'c35e5d8f-a48f-4e34-b273-3bce5d6e8bb5'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 3 Where [Uid] = '86c74766-d5c5-400d-9a4e-a22a7eea01ee'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 4 Where [Uid] = '77921836-2eca-424d-a63a-07f0f7e90ca8'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 5 Where [Uid] = '621c39f5-5f8c-4d3b-9243-9d3dfbea6100'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 6 Where [Uid] = '6bb90d4e-abdf-47b6-b1f5-927bc7d875f4'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 7 Where [Uid] = 'fdbb1adf-a3ca-49ae-b9de-d9cfe95aa03c'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 8 Where [Uid] = 'e562b0c7-e9f3-4ba1-922d-920e637e9488'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 9 Where [Uid] = 'a1d26b56-5308-44c7-a5db-448f705e3a74'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 10 Where [Uid] = 'a79b1dda-26d2-4c48-85ec-ee4b705a1e3a'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 11 Where [Uid] = 'ee104cc9-76d1-4f9a-afa9-c228a2de40ce'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 12 Where [Uid] = 'd91f6eec-8376-4f49-87b2-cb5eb8566dd0'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 13 Where [Uid] = '49a2fbab-6bca-40bb-ac46-c08f5f38c447'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 14 Where [Uid] = 'f8cee819-68bf-4de0-a221-0b2a573fef1a'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 15 Where [Uid] = 'd0c3a9b1-4be1-453b-b2f1-a383b82eafb5'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 16 Where [Uid] = '51c05548-a2fb-4e42-a38c-cf7bc445a14c'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 17 Where [Uid] = 'e6fef5e4-310d-4906-a8ed-c5bf43d5c730'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 18 Where [Uid] = 'a62b347d-c035-4ee8-ac13-663d56eac78d'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 19 Where [Uid] = 'e4de2b29-2930-47af-b9cb-7e1bbd174474'
GO
UPDATE [dbo].[Interests] SET DisplayOrder = 3 Where [Uid] = '91e45dbd-c56d-4108-abd1-3c68b33a3b41'
GO



UPDATE [dbo].[Activities] SET DisplayOrder = 1 WHERE [Uid] = '976bc557-1c5b-4254-a1c8-42781911b0fe'
GO
UPDATE [dbo].[Activities] SET DisplayOrder = 2 WHERE [Uid] = 'df787ac9-1ece-46cb-9c41-ab2c9d5097ac'
GO
UPDATE [dbo].[Activities] SET DisplayOrder = 3 WHERE [Uid] = '94f6c36c-98e9-427b-990d-bda4af3203fa'
GO
UPDATE [dbo].[Activities] SET DisplayOrder = 7 WHERE [Uid] = '15ee708c-b79c-4d21-a8e1-c86b92027707'
GO
UPDATE [dbo].[Activities] SET DisplayOrder = 4 WHERE [Uid] = '19e092ae-536d-43e9-b7fe-279ac2d52987'
GO
UPDATE [dbo].[Activities] SET DisplayOrder = 5 WHERE [Uid] = '2377b4cb-341c-4958-a3d0-a2957ee75a07'
GO
UPDATE [dbo].[Activities] SET DisplayOrder = 6 WHERE [Uid] = '93511fa2-9752-4f0e-a014-5985596e0134'
GO


UPDATE [dbo].[TargetAudiences] SET DisplayOrder = 1 WHERE [Uid] = '99cb313e-825a-457d-aa55-e600ebb79212'
GO
UPDATE [dbo].[TargetAudiences] SET DisplayOrder = 5 WHERE [Uid] = '32b3ef84-8e06-4df9-8bec-4aa9e7623163'
GO
UPDATE [dbo].[TargetAudiences] SET DisplayOrder = 3 WHERE [Uid] = '8f00bcfc-95c2-4d91-be3b-13146e160306'
GO
UPDATE [dbo].[TargetAudiences] SET DisplayOrder = 2 WHERE [Uid] = '30a128a9-81b7-46e2-a84e-609b9f1763d9'
GO
UPDATE [dbo].[TargetAudiences] SET DisplayOrder = 4 WHERE [Uid] = 'ebc80a23-d52c-4ec6-906c-e7f62e80532a'
GO




ALTER TABLE "dbo"."Activities"
ALTER COLUMN DisplayOrder  int  NOT NULL
go

ALTER TABLE "dbo"."TargetAudiences"
ALTER COLUMN DisplayOrder  int  NOT NULL
go

ALTER TABLE "dbo"."InterestGroups"
ALTER COLUMN DisplayOrder  int  NOT NULL
go

ALTER TABLE "dbo"."Interests"
ALTER COLUMN DisplayOrder  int  NOT NULL
go
