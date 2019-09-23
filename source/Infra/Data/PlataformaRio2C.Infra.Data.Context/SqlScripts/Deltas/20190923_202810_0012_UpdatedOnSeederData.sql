--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

--possible values are: "no", "yes, not done" and "yes, done"

UPDATE "dbo"."TargetAudiences" SET Name = 'Infanto-Juvenil | Tween (8 to 12)' WHERE Name = 'Infanto-Juvenil | Tween'
go
UPDATE "dbo"."TargetAudiences" SET Name = 'Pré-Escolar | Preschool (2 to 5)' WHERE Name = 'Pré-Escolar | Preschool'
go
UPDATE "dbo"."TargetAudiences" SET Name = 'Infantil | Children  (5 to 8)' WHERE Name = 'Infantil | Children'
go

UPDATE "dbo"."Interests" SET InterestGroupId = 7 WHERE Id IN (15, 19)
go

UPDATE "dbo"."Interests" SET DisplayOrder = 1 WHERE Id = 16
go
UPDATE "dbo"."Interests" SET DisplayOrder = 2 WHERE Id = 17
go
UPDATE "dbo"."Interests" SET DisplayOrder = 3 WHERE Id = 18
go

UPDATE "dbo"."Interests" SET DisplayOrder = 1  WHERE Id = 35
go
UPDATE "dbo"."Interests" SET DisplayOrder = 2 WHERE Id = 15
go
UPDATE "dbo"."Interests" SET DisplayOrder = 3 WHERE Id = 36
go
UPDATE "dbo"."Interests" SET DisplayOrder = 4 WHERE Id = 37
go
UPDATE "dbo"."Interests" SET DisplayOrder = 5 WHERE Id = 38
go
UPDATE "dbo"."Interests" SET DisplayOrder = 6 WHERE Id = 39
go
UPDATE "dbo"."Interests" SET DisplayOrder = 7 WHERE Id = 40
go
UPDATE "dbo"."Interests" SET DisplayOrder = 8 WHERE Id = 41
go
UPDATE "dbo"."Interests" SET DisplayOrder = 9 WHERE Id = 42
go
UPDATE "dbo"."Interests" SET DisplayOrder = 10 WHERE Id = 43
go
UPDATE "dbo"."Interests" SET DisplayOrder = 11 WHERE Id = 44
go
UPDATE "dbo"."Interests" SET DisplayOrder = 12 WHERE Id = 45
go
UPDATE "dbo"."Interests" SET DisplayOrder = 13 WHERE Id = 46
go
UPDATE "dbo"."Interests" SET DisplayOrder = 14 WHERE Id = 47
go
UPDATE "dbo"."Interests" SET DisplayOrder = 15 WHERE Id = 48
go
UPDATE "dbo"."Interests" SET DisplayOrder = 16 WHERE Id = 49
go
UPDATE "dbo"."Interests" SET DisplayOrder = 17 WHERE Id = 50
go
UPDATE "dbo"."Interests" SET DisplayOrder = 18 WHERE Id = 19
go
UPDATE "dbo"."Interests" SET DisplayOrder = 19 WHERE Id = 51
go
UPDATE "dbo"."Interests" SET DisplayOrder = 20 WHERE Id = 52
go
UPDATE "dbo"."Interests" SET DisplayOrder = 21 WHERE Id = 53
go