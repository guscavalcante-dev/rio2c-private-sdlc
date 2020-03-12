--must run on deploy | test: yes, done
--must run on deploy | prod: yes, done

--possible values are: "no", "yes, not done" and "yes, done"

UPDATE dbo.ProjectEvaluationStatuses SET Name = 'Em avaliação | Under evaluation', Code = 'UnderEvaluation' where Uid = '44368049-923D-41C6-9EAB-A9CECA05C296'
