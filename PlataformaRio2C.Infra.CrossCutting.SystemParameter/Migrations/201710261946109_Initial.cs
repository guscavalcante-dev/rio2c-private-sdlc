namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppAesEncryptionInfo",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Password = c.String(maxLength: 150, unicode: false),
                    Salt = c.String(maxLength: 150, unicode: false),
                    PasswordIterations = c.Int(nullable: false),
                    InitialVector = c.String(maxLength: 150, unicode: false),
                    KeySize = c.Int(nullable: false),
                    Code = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.SystemParameter",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Uid = c.Guid(nullable: false),
                    Code = c.Int(nullable: false),
                    LanguageCode = c.Int(nullable: false),
                    GroupCode = c.Int(nullable: false),
                    TypeName = c.String(maxLength: 150, unicode: false),
                    Description = c.String(maxLength: 256, unicode: false),
                    Value = c.String(maxLength: 1000, unicode: false),
                    SubCode = c.String(maxLength: 150, unicode: false),
                    DateChanges = c.DateTime(),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Uid, unique: true);

            Sql(@"IF NOT EXISTS (SELECT * FROM sys.symmetric_keys WHERE symmetric_key_id = 101)
                    BEGIN
	                    CREATE MASTER KEY ENCRYPTION BY PASSWORD = '93727kdAND18PRM6710#uen5%qlait2e7iw581q$$#1961fma$h47wlisdfgr';
	                    PRINT 'A MASTER KEY FOI CRIADA!';
                    END
                    GO
                  
                    IF NOT EXISTS (SELECT * FROM sys.certificates WHERE name = 'PlataformaRio2CCertificate')
                    BEGIN
	                    CREATE CERTIFICATE PlataformaRio2CCertificate WITH SUBJECT = 'Certificado PlataformaRio2CED';
	                    PRINT 'O CERTIFICADO PlataformaRio2CCertificate FOI CRIADO!';
                    END
                    GO
                  
                    IF NOT EXISTS (SELECT * FROM sys.symmetric_keys where name = 'PlataformaRio2CEDSymmetricKey')
                    BEGIN 
	                    CREATE SYMMETRIC KEY PlataformaRio2CEDSymmetricKey WITH ALGORITHM = AES_256 ENCRYPTION BY CERTIFICATE PlataformaRio2CCertificate;
	                    PRINT 'A CHAVE SIMÉTRICA PlataformaRio2CEDSymmetricKey FOI CRIADA!';
                    END
                    GO ");

            Sql(@"OPEN SYMMETRIC KEY [PlataformaRio2CEDSymmetricKey]
                    DECRYPTION BY CERTIFICATE [PlataformaRio2CCertificate];
                    GO

                    IF NOT EXISTS (SELECT * FROM [dbo].[AppAesEncryptionInfo] WHERE [code] = 0)
                    BEGIN
                    INSERT INTO [dbo].[AppAesEncryptionInfo] (Password, Salt, PasswordIterations, InitialVector, KeySize, Code) 
	                    VALUES (EncryptByKey(Key_GUID('PlataformaRio2CEDSymmetricKey'), 'ByMaRIinServpxdZapH')
                                ,EncryptByKey(Key_GUID('PlataformaRio2CEDSymmetricKey'), 'PlataformaRio2C2017')
                                ,2
                                ,EncryptByKey(Key_GUID('PlataformaRio2CEDSymmetricKey'), 'ZVshj92o#dqy14wx')
                                ,256
                                ,0);
                    PRINT 'A INFORMAÇÃO DE CRIPTOGRAFIA DE CODE 0 FOI ADICIONADA!'; 
                    END
                    GO

                    IF NOT EXISTS (SELECT * FROM [dbo].[AppAesEncryptionInfo] WHERE [code] = 1)
                    BEGIN
                    INSERT INTO [dbo].[AppAesEncryptionInfo] (Password, Salt, PasswordIterations, InitialVector, KeySize, Code) 
	                    VALUES (EncryptByKey(Key_GUID('PlataformaRio2CEDSymmetricKey'), 'ByMaRIinLoggerxdZapH')
                                ,EncryptByKey(Key_GUID('PlataformaRio2CEDSymmetricKey'), 'PlataformaRio2C2017')
                                ,2
                                ,EncryptByKey(Key_GUID('PlataformaRio2CEDSymmetricKey'), 'ZUshj92o#hjky14wx')
                                ,256
                                ,1);
                    PRINT 'A INFORMAÇÃO DE CRIPTOGRAFIA DE CODE 1 FOI ADICIONADA!';
                    END
                    GO  

                    IF NOT EXISTS (SELECT * FROM [dbo].[AppAesEncryptionInfo] WHERE [code] = 2)
                    BEGIN
                    INSERT INTO [dbo].[AppAesEncryptionInfo] (Password, Salt, PasswordIterations, InitialVector, KeySize, Code) 
	                    VALUES (EncryptByKey(Key_GUID('PlataformaRio2CEDSymmetricKey'), 'ByMaRIinApixdZapH')
                                ,EncryptByKey(Key_GUID('PlataformaRio2CEDSymmetricKey'), 'PlataformaRio2C2017')
                                ,2
                                ,EncryptByKey(Key_GUID('PlataformaRio2CEDSymmetricKey'), 'ZKshj67o#hjkf14yb')
                                ,256
                                ,2);
                    PRINT 'A INFORMAÇÃO DE CRIPTOGRAFIA DE CODE 2 FOI ADICIONADA!';
                    END
                GO ");

            Sql(@"SET ANSI_NULLS ON
                GO
                                  
                SET QUOTED_IDENTIFIER ON
                GO
                  
                IF EXISTS (SELECT * FROM sysobjects where id = object_id(N'[dbo].[GetCryptInfoByCode]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
                BEGIN
	                DROP PROCEDURE [dbo].[GetCryptInfoByCode];
	                PRINT 'A PROCEDURE [dbo].[GetCryptInfoByCode] FOI DROPADA!';
                END
                GO
                                                                                          
                CREATE PROCEDURE [dbo].[GetCryptInfoByCode] 
                (
                    @code int
                )
                AS
                BEGIN
                -- SET NOCOUNT ON added to prevent extra result sets from
                -- interfering with SELECT statements.
                SET NOCOUNT ON;
                OPEN SYMMETRIC KEY [PlataformaRio2CEDSymmetricKey] 
                    DECRYPTION BY CERTIFICATE [PlataformaRio2CCertificate];
                  
	                SELECT top 1
			                [Id] As 'Id'
		                ,CONVERT(varchar, DecryptByKey([Password])) AS 'Password'
		                ,CONVERT(varchar, DecryptByKey([Salt])) AS 'Salt'
		                ,[PasswordIterations] AS 'PasswordIterations'
		                ,CONVERT(varchar, DecryptByKey([InitialVector])) AS 'InitialVector'
		                ,[KeySize] AS 'KeySize'
			                ,[Code] As Code
		                FROM [dbo].[AppAesEncryptionInfo] 
		                WHERE [Code] = @code
                  
	                END                  
	                GO

                PRINT 'A PROCEDURE [dbo].[GetCryptInfoByCode] FOI CRIADA!';  
                GO");

        }

        public override void Down()
        {
            DropIndex("dbo.SystemParameter", new[] { "Uid" });
            DropTable("dbo.SystemParameter");
            DropTable("dbo.AppAesEncryptionInfo");
        }
    }
}
