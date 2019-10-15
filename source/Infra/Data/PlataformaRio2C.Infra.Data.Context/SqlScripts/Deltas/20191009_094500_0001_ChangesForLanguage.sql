--must run on deploy | test: yes, done
--must run on deploy | prod: yes, not done

--possible values are: "no", "yes, not done" and "yes, done"

UPDATE "dbo"."Languages"  SET Code = 'en-us'  WHERE Code = 'en'
GO