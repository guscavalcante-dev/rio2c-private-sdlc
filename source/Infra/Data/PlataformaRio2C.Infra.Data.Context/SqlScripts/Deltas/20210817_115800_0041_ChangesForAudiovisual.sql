--must run on deploy | test: yes, done
--must run on deploy | prod: yes, not done
--possible values are: "no", "yes, not done" and "yes, done"

BEGIN TRY
	BEGIN TRANSACTION
		ALTER TABLE "dbo"."InterestGroups"
		ADD IsCommission  bit NULL
		;

		EXEC('UPDATE InterestGroups SET IsCommission = 1 WHERE Uid = ''7B4A7C4A-EF10-483C-8854-87EBEB883583'';
			  UPDATE InterestGroups SET IsCommission = 0 WHERE Uid != ''7B4A7C4A-EF10-483C-8854-87EBEB883583''');

		ALTER TABLE "dbo"."InterestGroups"
		ALTER COLUMN IsCommission  bit NOT NULL
		;

		ALTER TABLE "dbo"."Projects"
		ADD CommissionEvaluationsCount  int  NOT NULL DEFAULT 0
		;

		ALTER TABLE "dbo"."Projects"
		ADD CommissionGrade  decimal(4,2)  NULL
		;

		ALTER TABLE "dbo"."Projects"
		ADD LastCommissionEvaluationDate  datetimeoffset  NULL
		;

		CREATE TABLE "CommissionAttendeeCollaboratorInterests"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"AttendeeCollaboratorId" int  NOT NULL ,
			"InterestId"         int  NOT NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)
		;
		CREATE NONCLUSTERED INDEX "IDX_CommissionAttendeeCollaboratorInterests_AttendeeCollaboratorId" ON "CommissionAttendeeCollaboratorInterests"
		( 
			"AttendeeCollaboratorId"  ASC
		)
		;
		CREATE NONCLUSTERED INDEX "IDX_CommissionAttendeeCollaboratorInterests_InterestId" ON "CommissionAttendeeCollaboratorInterests"
		( 
			"InterestId"          ASC
		)
		;
		CREATE TABLE "CommissionEvaluations"
		( 
			"Id"                 int IDENTITY ( 1,1 ) ,
			"Uid"                uniqueidentifier  NOT NULL ,
			"ProjectId"          int  NOT NULL ,
			"EvaluatorUserId"    int  NOT NULL ,
			"Grade"              decimal(4,2)  NOT NULL ,
			"IsDeleted"          bit  NOT NULL ,
			"CreateDate"         datetimeoffset  NOT NULL ,
			"CreateUserId"       int  NOT NULL ,
			"UpdateDate"         datetimeoffset  NOT NULL ,
			"UpdateUserId"       int  NOT NULL 
		)
		;
		CREATE NONCLUSTERED INDEX "IDX_CommissionEvaluations_EvaluatorUserId" ON "CommissionEvaluations"
		( 
			"EvaluatorUserId"     ASC
		)
		;

		ALTER TABLE "CommissionAttendeeCollaboratorInterests"
		ADD CONSTRAINT "PK_CommissionAttendeeCollaboratorInterests" PRIMARY KEY  CLUSTERED ("Id" ASC)
		;
		ALTER TABLE "CommissionAttendeeCollaboratorInterests"
		ADD CONSTRAINT "IDX_UQ_CommissionAttendeeCollaboratorInterests_Uid" UNIQUE ("Uid"  ASC)
		;
		ALTER TABLE "CommissionEvaluations"
		ADD CONSTRAINT "PK_CommissionEvaluations" PRIMARY KEY  CLUSTERED ("Id" ASC)
		;
		ALTER TABLE "CommissionEvaluations"
		ADD CONSTRAINT "IDX_UQ_CommissionEvaluations_Uid" UNIQUE ("Uid"  ASC)
		;
		ALTER TABLE "CommissionEvaluations"
		ADD CONSTRAINT "IDX_UQ_CommissionEvaluations" UNIQUE ("ProjectId"  ASC,"EvaluatorUserId"  ASC)
		;
		ALTER TABLE "CommissionAttendeeCollaboratorInterests"
			ADD CONSTRAINT "FK_AttendeeCollaborators_CommissionAttendeeCollaboratorInterests_AttendeeCollaboratorId" FOREIGN KEY ("AttendeeCollaboratorId") REFERENCES "dbo"."AttendeeCollaborators"("Id")
		;
		ALTER TABLE "CommissionAttendeeCollaboratorInterests"
			ADD CONSTRAINT "FK_Interests_CommissionAttendeeCollaboratorInterests_InterestId" FOREIGN KEY ("InterestId") REFERENCES "dbo"."Interests"("Id")
		;
		ALTER TABLE "CommissionAttendeeCollaboratorInterests"
			ADD CONSTRAINT "FK_Users_CommissionAttendeeCollaboratorInterests_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
		;
		ALTER TABLE "CommissionAttendeeCollaboratorInterests"
			ADD CONSTRAINT "FK_Users_CommissionAttendeeCollaboratorInterests_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
		;
		ALTER TABLE "CommissionEvaluations"
			ADD CONSTRAINT "FK_Projects_CommissionEvaluations_ProjectId" FOREIGN KEY ("ProjectId") REFERENCES "dbo"."Projects"("Id")
		;
		ALTER TABLE "CommissionEvaluations"
			ADD CONSTRAINT "FK_Users_CommissionEvaluations_EvaluatorUserId" FOREIGN KEY ("EvaluatorUserId") REFERENCES "dbo"."Users"("Id")
		;
		ALTER TABLE "CommissionEvaluations"
			ADD CONSTRAINT "FK_Users_CommissionEvaluations_CreateUserId" FOREIGN KEY ("CreateUserId") REFERENCES "dbo"."Users"("Id")
		;
		ALTER TABLE "CommissionEvaluations"
			ADD CONSTRAINT "FK_Users_CommissionEvaluations_UpdateUserId" FOREIGN KEY ("UpdateUserId") REFERENCES "dbo"."Users"("Id")
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
