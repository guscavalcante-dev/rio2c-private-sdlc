namespace PlataformaRio2C.Infra.CrossCutting.SystemParameter
{
    public enum SystemParameterCodes
    {
        [SystemParametersDescription((int)SiteUrl, "Url do site", typeof(string), (int)SystemParameterGroupCodes.General, (int)LanguageCodes.PtBr, "http://www.rio2c.com")]
        SiteUrl = 1,

        [SystemParametersDescription((int)MockEnableRecipientSendEmail, "Mock envio de destinatário ao enviar e-mail email", typeof(bool), (int)SystemParameterGroupCodes.General, (int)LanguageCodes.PtBr, false)]
        MockEnableRecipientSendEmail = 20,

        [SystemParametersDescription((int)MockRecipientSendEmail, "Mock envio de destinatário ao enviar e-mail email", typeof(bool), (int)SystemParameterGroupCodes.General, (int)LanguageCodes.PtBr, "credenciamento@rio2c.com.br")]
        MockRecipientSendEmail = 21,

        [SystemParametersDescription((int)SmtpHost, "SMTP HOST", typeof(string), (int)SystemParameterGroupCodes.Emails, (int)LanguageCodes.PtBr, "mail.agenciaguppy.com.br")]
        SmtpHost = 100,

        [SystemParametersDescription((int)SmtpPort, "SMTP Port", typeof(int), (int)SystemParameterGroupCodes.Emails, (int)LanguageCodes.PtBr, 465)]
        SmtpPort = 101,

        [SystemParametersDescription((int)SmtpUseDefaultCredentials, "SMTP UseDefaultCredentials", typeof(bool), (int)SystemParameterGroupCodes.Emails, (int)LanguageCodes.PtBr, false)]
        SmtpUseDefaultCredentials = 102,

        [SystemParametersDescription((int)SmtpDefaultSenderEmail, "SMTP email do rementente padrão", typeof(string), (int)SystemParameterGroupCodes.Emails, (int)LanguageCodes.PtBr, "credenciamento@rio2c.com")]
        SmtpDefaultSenderEmail = 103,

        [SystemParametersDescription((int)SmtpDefaultSenderName, "SMTP nome do rementente padrão", typeof(string), (int)SystemParameterGroupCodes.Emails, (int)LanguageCodes.PtBr, "Equipe Rio2C")]
        SmtpDefaultSenderName = 104,

        [SystemParametersDescription((int)SmtpIsBodyHtml, "SMTP IsBodyHtml", typeof(bool), (int)SystemParameterGroupCodes.Emails, (int)LanguageCodes.PtBr, true)]
        SmtpIsBodyHtml = 105,

        [SystemParametersDescription((int)SmtpCredentialUser, "SMTP Username da Credencial", typeof(string), (int)SystemParameterGroupCodes.Emails, (int)LanguageCodes.PtBr, "rio2c@agenciaguppy.com.br")]
        SmtpCredentialUser= 106,

        [SystemParametersDescription((int)SmtpCredentialPass, "SMTP Password da Credencial", typeof(string), (int)SystemParameterGroupCodes.Emails, (int)LanguageCodes.PtBr, "rio2c@2018")]
        SmtpCredentialPass = 107,


        [SystemParametersDescription((int)EmailTwoFactorSubject, "Subject do E-mail do segundo fator de autenticação", typeof(string), (int)SystemParameterGroupCodes.Emails, (int)LanguageCodes.PtBr, "Rio2C - Token de autenticação")]        
        EmailTwoFactorSubject = 108,

        [SystemParametersDescription((int)EmailTwoFactorMessageFormat, "Formato do corpo do E-mail do segundo fator de autenticação (Usar {0} para o valor do Token)", typeof(string), (int)SystemParameterGroupCodes.Emails, (int)LanguageCodes.PtBr, "Seu token de segurança é: {0}")]
        EmailTwoFactorMessageFormat = 109,

        [SystemParametersDescription((int)SmtpEnableSsl, "SMTP EnableSsl", typeof(bool), (int)SystemParameterGroupCodes.Emails, (int)LanguageCodes.PtBr, true)]
        SmtpEnableSsl = 110,

        [SystemParametersDescription((int)EmailInviteCollaboratorSubject, "Subject do E-mail de convite aos colaboradores", typeof(string), (int)SystemParameterGroupCodes.Emails, (int)LanguageCodes.PtBr, "Bem vindo ao Rio2C / Welcome to Rio2C")]
        EmailInviteCollaboratorSubject = 111,

        [SystemParametersDescription((int)EmailHiddenCopySender, "Destinatário em copia oculta", typeof(string), (int)SystemParameterGroupCodes.Emails, (int)LanguageCodes.PtBr, "rio2c@agenciaguppy.com.br")]
        EmailHiddenCopySender = 112,

        [SystemParametersDescription((int)EmailExceptionSender, "Destinatário de exceção", typeof(string), (int)SystemParameterGroupCodes.Emails, (int)LanguageCodes.PtBr, "rio2c@agenciaguppy.com.br")]
        EmailExceptionSender = 113,


        [SystemParametersDescription((int)SmsUrlGateway, "Url do Gateway de envio de SMS (Usar {0} para o numero de destino e {1} para o Corpo do SMS)", typeof(string), (int)SystemParameterGroupCodes.Sms, (int)LanguageCodes.PtBr, "http://10.190.4.11/sendsms.php?number={0}&text={1}")]
        SmsUrlGateway = 200,

        [SystemParametersDescription((int)SmsTwoFactorBodyFormat, "Formato do corpo do SMS do segundo fator de autenticação (Usar {0} para o valor do Token)", typeof(string), (int)SystemParameterGroupCodes.Sms, (int)LanguageCodes.PtBr, "Seu token de segurança é: {0}")]
        SmsTwoFactorBodyFormat = 201,


        [SystemParametersDescription((int)ProjectsMaxNumberPerProducer, "Número máximo de projeto por produtora", typeof(int), (int)SystemParameterGroupCodes.Projets, (int)LanguageCodes.PtBr, 3)]
        ProjectsMaxNumberPerProducer = 300,

        [SystemParametersDescription((int)ProjectsMaxNumberPlayerPerProject, "Número máximo de player por projeto", typeof(int), (int)SystemParameterGroupCodes.Projets, (int)LanguageCodes.PtBr, 5)]
        ProjectsMaxNumberPlayerPerProject = 301,

        [SystemParametersDescription((int)ProjectsMaximumDateForEvaluation, "Data máxima para avaliação", typeof(string), (int)SystemParameterGroupCodes.Projets, (int)LanguageCodes.PtBr, "2019-03-15 00:00:00")]
        ProjectsMaximumDateForEvaluation = 302,

        [SystemParametersDescription((int)ProjectsProducerPreRegistrationDisabled, "Pre-cadastro desativado", typeof(bool), (int)SystemParameterGroupCodes.Projets, (int)LanguageCodes.PtBr, false)]
        ProjectsProducerPreRegistrationDisabled = 303,

        [SystemParametersDescription((int)ProjectsRegisterDisabled, "Cadastro de projeto desativado", typeof(bool), (int)SystemParameterGroupCodes.Projets, (int)LanguageCodes.PtBr, false)]
        ProjectsRegisterDisabled = 304,

        [SystemParametersDescription((int)ProjectsSendToPlayersDisabled, "Envio de projeto para players desativado", typeof(bool), (int)SystemParameterGroupCodes.Projets, (int)LanguageCodes.PtBr, false)]
        ProjectsSendToPlayersDisabled = 305,

        [SystemParametersDescription((int)SymplaUrlBase, "Url base da API Sympla", typeof(string), (int)SystemParameterGroupCodes.Sympla, (int)LanguageCodes.PtBr, "https://api.sympla.com.br/")]
        SymplaUrlBase = 400,

        [SystemParametersDescription((int)SymplaUrlPathParticipants, "Url Path de participantes da API Sympla", typeof(string), (int)SystemParameterGroupCodes.Sympla, (int)LanguageCodes.PtBr, "v2/participants/205388")]
        SymplaUrlPathParticipants = 401,

        [SystemParametersDescription((int)SymplaHeaders, "Headers enviados para Api Sympla", typeof(string), (int)SystemParameterGroupCodes.Sympla, (int)LanguageCodes.PtBr, @"[{""Name"":""S_APP_TOKEN"",""Value"": ""fc1da583f7eaf3110ea8daf66db65931""},{""Name"": ""S_EMAIL"", ""Value"": ""angelica@rio2c.com""},{""Name"": ""S_TOKEN"",""Value"": ""47a5181e81d9f9c97bb7ccaab7056955""}]")]
        SymplaHeaders= 402,

        [SystemParametersDescription((int)SymplaFieldKeyCnpj, "Nome do campo chave para busca de CNPJ", typeof(string), (int)SystemParameterGroupCodes.Sympla, (int)LanguageCodes.PtBr, "CNPJ")]
        SymplaFieldKeyCnpj = 403,

      
        [SystemParametersDescription((int)SymplaOrderStatusConfirmPayment, "Valor order_status quando pagamento esta confirmado", typeof(string), (int)SystemParameterGroupCodes.Sympla, (int)LanguageCodes.PtBr, "A")]
        SymplaOrderStatusConfirmPayment = 404,

        [SystemParametersDescription((int)SymplaTicketNameForProducer, "Valor ticket_name quando produtora", typeof(string), (int)SystemParameterGroupCodes.Sympla, (int)LanguageCodes.PtBr, "Industry")]
        SymplaTicketNameForProducer = 405,

        [SystemParametersDescription((int)SymplaFieldKeyCompanyName, "Nome do campo chave para busca de Nome da empresa", typeof(string), (int)SystemParameterGroupCodes.Sympla, (int)LanguageCodes.PtBr, "Empresa (Razão Social) / Company Name")]
        SymplaFieldKeyCompanyName = 406,

        [SystemParametersDescription((int)SymplaMockEnable, "Mock ativo", typeof(bool), (int)SystemParameterGroupCodes.Sympla, (int)LanguageCodes.PtBr, false)]
        SymplaMockEnable = 420,

        [SystemParametersDescription((int)SymplaCnpjMock, "Mock Cnpj com credencial confirmada", typeof(string), (int)SystemParameterGroupCodes.Sympla, (int)LanguageCodes.PtBr, "31512320000154")]
        SymplaCnpjMock = 421,

        [SystemParametersDescription((int)SymplaEmailMock, "Mock Email com credencial confirmada", typeof(string), (int)SystemParameterGroupCodes.Sympla, (int)LanguageCodes.PtBr, "")]
        SymplaEmailMock = 422,


        [SystemParametersDescription((int)ScheduleNumberOfAutomaticProcessingTables, "Número de mesas para o processamento automático", typeof(int), (int)SystemParameterGroupCodes.ScheduleOneToOneMeetings, (int)LanguageCodes.PtBr, 55)]
        ScheduleNumberOfAutomaticProcessingTables = 500,

        [SystemParametersDescription((int)ScheduleNumberOfTablesManualProcessing, "Número de mesas para o processamento manual", typeof(int), (int)SystemParameterGroupCodes.ScheduleOneToOneMeetings, (int)LanguageCodes.PtBr, 5)]
        ScheduleNumberOfTablesManualProcessing = 501,

        [SystemParametersDescription((int)ScheduleRooms, "Ids das salas", typeof(string), (int)SystemParameterGroupCodes.ScheduleOneToOneMeetings, (int)LanguageCodes.PtBr, "1,2")]
        ScheduleRooms = 502,

        [SystemParametersDescription((int)ScheduleOneToOneMeetingsDuration, "Quanto tempo de duração (ex: 0,5 h)", typeof(string), (int)SystemParameterGroupCodes.ScheduleOneToOneMeetings, (int)LanguageCodes.PtBr, "0,5")]
        ScheduleOneToOneMeetingsDuration = 503,

        [SystemParametersDescription((int)ScheduleOneToOneMeetingsDates, "Datas das Rodadas de Negócio", typeof(string), (int)SystemParameterGroupCodes.ScheduleOneToOneMeetings, (int)LanguageCodes.PtBr, "06/04/2017,07/04/2017,08/04/2017")]
        ScheduleOneToOneMeetingsDates = 504,

        [SystemParametersDescription((int)ScheduleShowViewInSystem, "Exibir agenda da rodada de negócio no sistema", typeof(bool), (int)SystemParameterGroupCodes.ScheduleOneToOneMeetings, (int)LanguageCodes.PtBr, true)]
        ScheduleShowViewInSystem = 505,

        [SystemParametersDescription((int)NetworkRio2CEmailsThatShouldBeHidden, "E-mails que devem estar ocultos no download", typeof(string), (int)SystemParameterGroupCodes.NetworkRio2C, (int)LanguageCodes.PtBr, "rodrigo@agenciaguppy.com.br")]
        NetworkRio2CEmailsThatShouldBeHidden = 600,

        [SystemParametersDescription((int)FinancialReportUsersAllowed, "E-mails que podem visualizar o relatório financeiro", typeof(string), (int)SystemParameterGroupCodes.FinancialReport, (int)LanguageCodes.PtBr, "rodrigo@agenciaguppy.com.br")]
        FinancialReportUsersAllowed = 800,

        //[SystemParametersDescription((int)EMAIL_RECOVER_PASS_SUBJECT, "Subject do E-mail de recuperação de senha do Sistema", typeof(System.String), (int)SystemParameterGroupCodes.EMAIL, LanguageCodes.PT_BR, "TorcidApp - Recuperação de senha")]
        //[Description("Subject do E-mail de recuperação de senha do Sistema")]
        //[Group(2)]
        //EMAIL_RECOVER_PASS_SUBJECT = 14,

        //[SystemParametersDescription((int)EMAIL_RECOVER_PASS_BODY, "Formato do corpo do E-mail de recuperação de senha do Sistema (Usar {0} para a Url de Calback)", typeof(System.String), (int)SystemParameterGroupCodes.EMAIL, LanguageCodes.PT_BR, "Para alterar sua senha clique aqui: <a href='{0}'>Alterar senha</a>")]
        //[Description("Formato do corpo do E-mail de recuperação de senha do Sistema (Usar {0} para a Url de Calback)")]
        //[Group(2)]
        //EMAIL_RECOVER_PASS_BODY = 15,

        //[SystemParametersDescription((int)EMAIL_CADASTRO_USUARIO_SUBJECT, "Subject do E-mail de cadastro de usuário do Sistema", typeof(System.String), (int)SystemParameterGroupCodes.EMAIL, LanguageCodes.PT_BR, "TorcidApp - Dados do seu cadastro de usuário")]
        //[Description("Subject do E-mail de cadastro de usuário do Sistema")]
        //[Group(2)]
        //EMAIL_CADASTRO_USUARIO_SUBJECT = 16,

        //[SystemParametersDescription((int)EMAIL_CADASTRO_USUARIO_BODY, "Formato do corpo do E-mail de cadastro de usuário do Sistema (Usar {0} para a senha gerada)", typeof(System.String), (int)SystemParameterGroupCodes.EMAIL, LanguageCodes.PT_BR, "Sua conta de usuário foi criada com sucesso, para se logar utilize seu e-mail e a senha {0}.")]
        //[Description("Formato do corpo do E-mail de cadastro de usuário do Sistema (Usar {0} para a senha gerada)")]
        //[Group(2)]
        //EMAIL_CADASTRO_USUARIO_BODY = 17,


    }
}
