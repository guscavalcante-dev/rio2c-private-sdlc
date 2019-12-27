// ***********************************************************************
// Assembly         : PlataformaRio2C.Infra.Data.Context
// Author           : Rafael Dantas Ruiz
// Created          : 06-19-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 12-27-2019
// ***********************************************************************
// <copyright file="Configuration.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace PlataformaRio2C.Infra.Data.Context.Migrations
{
    using CrossCutting.Tools.Extensions;
    using Domain.Entities;
    using Domain.Enums;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    /// <summary>Configuration</summary>
    internal sealed class Configuration : DbMigrationsConfiguration<PlataformaRio2C.Infra.Data.Context.PlataformaRio2CContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true; //TODO: Check if this is necessary
        }

        protected override void Seed(PlataformaRio2C.Infra.Data.Context.PlataformaRio2CContext context)
        {
            //SeedCountry(context);
            //SeedState(context);

            SeedLanguages(context);
            SeedHoldings(context);
            SeedEvent(context);
            SeedInterests(context);
            SeedActivitys(context);
            SeedTargetAudiences(context);
            SeedProjectStatus(context);
            //SeedRooms(context);
            //SeedRoleLecturer(context);
            //SeedCity(context);

            SeedQuiz(context);
        }

        private void SeedQuiz(PlataformaRio2C.Infra.Data.Context.PlataformaRio2CContext context)
        {
            var quiz = new Quiz(1, "Pesquisa 2018");
            context.Quiz.Add(quiz);

            context.QuizQuestion.AddRange(
                new List<QuizQuestion>()
                {
                    new QuizQuestion(1,"1.	Sua empresa participou da edição do Rio2C de 2018 como Player? / Did your company acted as a player in the 2018 edition of Rio2C"),
                    new QuizQuestion(1,"2.	Caso sim, sua empresa adiquiriu algum projeto? / if positive, did your company acquried any Project?"),
                    new QuizQuestion(1,"3.  Qual Projeto? / Which Project? (ter a opção de ir abrindo mais campos, conforme a dupla abaixo)"),
                }
            );

            context.QuizOption.AddRange(
                new List<QuizOption>()
                {
                    new QuizOption(1,"Sim / Yes", false),
                    new QuizOption(1,"Não / No", false),

                    new QuizOption(2,"Sim / Yes", false),
                    new QuizOption(2,"Não / No", false),

                    new QuizOption(3,"Projeto / Project", true),
                    new QuizOption(3,"Valor investido / Total amount invested", true),

                }
          );
        }

        private void SeedLanguages(PlataformaRio2C.Infra.Data.Context.PlataformaRio2CContext context)
        {
            var languages = default(LanguageCodes).ToEnumStrings(false);

            foreach (var language in languages)
            {
                if (!context.Languages.Any(e => e.Code == language.Value))
                {
                    context.Languages.Add(new Language(language.Description, language.Value));
                }
            }
        }
        private void SeedHoldings(PlataformaRio2C.Infra.Data.Context.PlataformaRio2CContext context)
        {
            //var playerGloboSat = new Holding("GLOBOSAT");
            //var playerMarlin = new Holding("MARLIN");

            //if (!context.Holdings.Any())
            //{
            //    context.Holdings.AddRange(
            //        new List<Holding>()
            //        {
            //            playerGloboSat
            //        }
            //    );
            //}

            //if (!context.Players.Any())
            //{
            //    context.Players.AddRange(
            //       new List<Player>()
            //       {
            //            new Player("Gloob", playerGloboSat),
            //            new Player("Multishow", playerGloboSat),
            //            new Player("GNT", playerGloboSat),
            //            //new Player("Marlin desenvolvimento", playerMarlin),
            //       }
            //   );
            //}
        }
        private void SeedEvent(PlataformaRio2C.Infra.Data.Context.PlataformaRio2CContext context)
        {
            if (!context.Events.Any())
            {
                var eventInitial = new Edition("Rio2c 2018");
                eventInitial.SetStartDate(new DateTime(2018, 3, 1));
                eventInitial.SetEndDate(new DateTime(2018, 4, 1));

                context.Events.AddRange(
                   new List<Edition>()
                   {
                        eventInitial
                   }
               );
            }
        }
        //private void SeedCity(PlataformaRio2C.Infra.Data.Context.PlataformaRio2CContext context)
        //{
        //if (!context.City.Any())
        //{
        //    context.Languages.AddRange(
        //        new List<City>()
        //        {
        //            New City("Barrocas",5),
        //        }
        //}
        //}

        //private void SeedCountry(PlataformaRio2C.Infra.Data.Context.PlataformaRio2CContext context)
        //{
        //    if (!context.Country.Any())
        //    {
        //        context.Country.AddRange(
        //           new List<Country>()
        //           {
        //            new Country("AF","Afghanistan"),
        //            new Country("AL","Albania"),
        //            new Country("DZ","Algeria"),
        //            new Country("DS","American Samoa"),
        //            new Country("AD","Andorra"),
        //            new Country("AO","Angola"),
        //            new Country("AI","Anguilla"),
        //            new Country("AQ","Antarctica"),
        //            new Country("AG","Antigua and Barbuda"),
        //            new Country("AR","Argentina"),
        //            new Country("AM","Armenia"),
        //            new Country("AW","Aruba"),
        //            new Country("AU","Australia"),
        //            new Country("AT","Austria"),
        //            new Country("AZ","Azerbaijan"),
        //            new Country("BS","Bahamas"),
        //            new Country("BH","Bahrain"),
        //            new Country("BD","Bangladesh"),
        //            new Country("BB","Barbados"),
        //            new Country("BY","Belarus"),
        //            new Country("BE","Belgium"),
        //            new Country("BZ","Belize"),
        //            new Country("BJ","Benin"),
        //            new Country("BM","Bermuda"),
        //            new Country("BT","Bhutan"),
        //            new Country("BO","Bolivia"),
        //            new Country("BA","Bosnia and Herzegovina"),
        //            new Country("BW","Botswana"),
        //            new Country("BV","Bouvet Island"),
        //            new Country("BR","Brazil"),
        //            new Country("IO","British Indian Ocean Territory"),
        //            new Country("BN","Brunei Darussalam"),
        //            new Country("BG","Bulgaria"),
        //            new Country("BF","Burkina Faso"),
        //            new Country("BI","Burundi"),
        //            new Country("KH","Cambodia"),
        //            new Country("CM","Cameroon"),
        //            new Country("CA","Canada"),
        //            new Country("CV","Cape Verde"),
        //            new Country("KY","Cayman Islands"),
        //            new Country("CF","Central African Republic"),
        //            new Country("TD","Chad"),
        //            new Country("CL","Chile"),
        //            new Country("CN","China"),
        //            new Country("CX","Christmas Island"),
        //            new Country("CC","Cocos (Keeling) Islands"),
        //            new Country("CO","Colombia"),
        //            new Country("KM","Comoros"),
        //            new Country("CG","Congo"),
        //            new Country("CK","Cook Islands"),
        //            new Country("CR","Costa Rica"),
        //            new Country("HR","Croatia (Hrvatska)"),
        //            new Country("CU","Cuba"),
        //            new Country("CY","Cyprus"),
        //            new Country("CZ","Czech Republic"),
        //            new Country("DK","Denmark"),
        //            new Country("DJ","Djibouti"),
        //            new Country("DM","Dominica"),
        //            new Country("DO","Dominican Republic"),
        //            new Country("TP","East Timor"),
        //            new Country("EC","Ecuador"),
        //            new Country("EG","Egypt"),
        //            new Country("SV","El Salvador"),
        //            new Country("GQ","Equatorial Guinea"),
        //            new Country("ER","Eritrea"),
        //            new Country("EE","Estonia"),
        //            new Country("ET","Ethiopia"),
        //            new Country("FK","Falkland Islands (Malvinas)"),
        //            new Country("FO","Faroe Islands"),
        //            new Country("FJ","Fiji"),
        //            new Country("FI","Finland"),
        //            new Country("FR","France"),
        //            new Country("FX","France, Metropolitan"),
        //            new Country("GF","French Guiana"),
        //            new Country("PF","French Polynesia"),
        //            new Country("TF","French Southern Territories"),
        //            new Country("GA","Gabon"),
        //            new Country("GM","Gambia"),
        //            new Country("GE","Georgia"),
        //            new Country("DE","Germany"),
        //            new Country("GH","Ghana"),
        //            new Country("GI","Gibraltar"),
        //            new Country("GK","Guernsey"),
        //            new Country("GR","Greece"),
        //            new Country("GL","Greenland"),
        //            new Country("GD","Grenada"),
        //            new Country("GP","Guadeloupe"),
        //            new Country("GU","Guam"),
        //            new Country("GT","Guatemala"),
        //            new Country("GN","Guinea"),
        //            new Country("GW","Guinea-Bissau"),
        //            new Country("GY","Guyana"),
        //            new Country("HT","Haiti"),
        //            new Country("HM","Heard and Mc Donald Islands"),
        //            new Country("HN","Honduras"),
        //            new Country("HK","Hong Kong"),
        //            new Country("HU","Hungary"),
        //            new Country("IS","Iceland"),
        //            new Country("IN","India"),
        //            new Country("IM","Isle of Man"),
        //            new Country("ID","Indonesia"),
        //            new Country("IR","Iran (Islamic Republic of)"),
        //            new Country("IQ","Iraq"),
        //            new Country("IE","Ireland"),
        //            new Country("IL","Israel"),
        //            new Country("IT","Italy"),
        //            new Country("CI","Ivory Coast"),
        //            new Country("JE","Jersey"),
        //            new Country("JM","Jamaica"),
        //            new Country("JP","Japan"),
        //            new Country("JO","Jordan"),
        //            new Country("KZ","Kazakhstan"),
        //            new Country("KE","Kenya"),
        //            new Country("KI","Kiribati"),
        //            new Country("KP","Korea, Democratic People''s Republic of"),
        //            new Country("KR","Korea, Republic of"),
        //            new Country("XK","Kosovo"),
        //            new Country("KW","Kuwait"),
        //            new Country("KG","Kyrgyzstan"),
        //            new Country("LA","Lao People''s Democratic Republic"),
        //            new Country("LV","Latvia"),
        //            new Country("LB","Lebanon"),
        //            new Country("LS","Lesotho"),
        //            new Country("LR","Liberia"),
        //            new Country("LY","Libyan Arab Jamahiriya"),
        //            new Country("LI","Liechtenstein"),
        //            new Country("LT","Lithuania"),
        //            new Country("LU","Luxembourg"),
        //            new Country("MO","Macau"),
        //            new Country("MK","Macedonia"),
        //            new Country("MG","Madagascar"),
        //            new Country("MW","Malawi"),
        //            new Country("MY","Malaysia"),
        //            new Country("MV","Maldives"),
        //            new Country("ML","Mali"),
        //            new Country("MT","Malta"),
        //            new Country("MH","Marshall Islands"),
        //            new Country("MQ","Martinique"),
        //            new Country("MR","Mauritania"),
        //            new Country("MU","Mauritius"),
        //            new Country("TY","Mayotte"),
        //            new Country("MX","Mexico"),
        //            new Country("FM","Micronesia, Federated States of"),
        //            new Country("MD","Moldova, Republic of"),
        //            new Country("MC","Monaco"),
        //            new Country("MN","Mongolia"),
        //            new Country("ME","Montenegro"),
        //            new Country("MS","Montserrat"),
        //            new Country("MA","Morocco"),
        //            new Country("MZ","Mozambique"),
        //            new Country("MM","Myanmar"),
        //            new Country("NA","Namibia"),
        //            new Country("NR","Nauru"),
        //            new Country("NP","Nepal"),
        //            new Country("NL","Netherlands"),
        //            new Country("AN","Netherlands Antilles"),
        //            new Country("NC","New Caledonia"),
        //            new Country("NZ","New Zealand"),
        //            new Country("NI","Nicaragua"),
        //            new Country("NE","Niger"),
        //            new Country("NG","Nigeria"),
        //            new Country("NU","Niue"),
        //            new Country("NF","Norfolk Island"),
        //            new Country("MP","Northern Mariana Islands"),
        //            new Country("NO","Norway"),
        //            new Country("OM","Oman"),
        //            new Country("PK","Pakistan"),
        //            new Country("PW","Palau"),
        //            new Country("PS","Palestine"),
        //            new Country("PA","Panama"),
        //            new Country("PG","Papua New Guinea"),
        //            new Country("PY","Paraguay"),
        //            new Country("PE","Peru"),
        //            new Country("PH","Philippines"),
        //            new Country("PN","Pitcairn"),
        //            new Country("PL","Poland"),
        //            new Country("PT","Portugal"),
        //            new Country("PR","Puerto Rico"),
        //            new Country("QA","Qatar"),
        //            new Country("RE","Reunion"),
        //            new Country("RO","Romania"),
        //            new Country("RU","Russian Federation"),
        //            new Country("RW","Rwanda"),
        //            new Country("KN","Saint Kitts and Nevis"),
        //            new Country("LC","Saint Lucia"),
        //            new Country("VC","Saint Vincent and the Grenadines"),
        //            new Country("WS","Samoa"),
        //            new Country("SM","San Marino"),
        //            new Country("ST","Sao Tome and Principe"),
        //            new Country("SA","Saudi Arabia"),
        //            new Country("SN","Senegal"),
        //            new Country("RS","Serbia"),
        //            new Country("SC","Seychelles"),
        //            new Country("SL","Sierra Leone"),
        //            new Country("SG","Singapore"),
        //            new Country("SK","Slovakia"),
        //            new Country("SI","Slovenia"),
        //            new Country("SB","Solomon Islands"),
        //            new Country("SO","Somalia"),
        //            new Country("ZA","South Africa"),
        //            new Country("GS","South Georgia South Sandwich Islands"),
        //            new Country("SS","South Sudan"),
        //            new Country("ES","Spain"),
        //            new Country("LK","Sri Lanka"),
        //            new Country("SH","St. Helena"),
        //            new Country("PM","St. Pierre and Miquelon"),
        //            new Country("SD","Sudan"),
        //            new Country("SR","Suriname"),
        //            new Country("SJ","Svalbard and Jan Mayen Islands"),
        //            new Country("SZ","Swaziland"),
        //            new Country("SE","Sweden"),
        //            new Country("CH","Switzerland"),
        //            new Country("SY","Syrian Arab Republic"),
        //            new Country("TW","Taiwan"),
        //            new Country("TJ","Tajikistan"),
        //            new Country("TZ","Tanzania, United Republic of"),
        //            new Country("TH","Thailand"),
        //            new Country("TG","Togo"),
        //            new Country("TK","Tokelau"),
        //            new Country("TO","Tonga"),
        //            new Country("TT","Trinidad and Tobago"),
        //            new Country("TN","Tunisia"),
        //            new Country("TR","Turkey"),
        //            new Country("TM","Turkmenistan"),
        //            new Country("TC","Turks and Caicos Islands"),
        //            new Country("TV","Tuvalu"),
        //            new Country("UG","Uganda"),
        //            new Country("UA","Ukraine"),
        //            new Country("AE","United Arab Emirates"),
        //            new Country("GB","United Kingdom"),
        //            new Country("US","United States"),
        //            new Country("UM","United States minor outlying islands"),
        //            new Country("UY","Uruguay"),
        //            new Country("UZ","Uzbekistan"),
        //            new Country("VU","Vanuatu"),
        //            new Country("VA","Vatican City State"),
        //            new Country("VE","Venezuela"),
        //            new Country("VN","Vietnam"),
        //            new Country("VG","Virgin Islands (British)"),
        //            new Country("VI","Virgin Islands (U.S.)"),
        //            new Country("WF","Wallis and Futuna Islands"),
        //            new Country("EH","Western Sahara"),
        //            new Country("YE","Yemen"),
        //            new Country("ZR","Zaire"),
        //            new Country("ZM","Zambia"),
        //            new Country("ZW","Zimbabwe"),
        //           }
        //       );
        //    }
        //}

        //private void SeedState(PlataformaRio2C.Infra.Data.Context.PlataformaRio2CContext context)
        //{
        //    if (!context.State.Any())
        //    {
        //        context.State.AddRange(
        //           new List<State>()
        //           {
        //            new State("AC","Acre",30),
        //            new State("AL","Alagoas",30),
        //            new State("AM","Amazonas",30),
        //            new State("AP","Amapá",30),
        //            new State("BA","Bahia",30),
        //            new State("CE","Ceará",30),
        //            new State("DF","Distrito Federal",30),
        //            new State("ES","Espírito Santo",30),
        //            new State("GO","Goiás",30),
        //            new State("MA","Maranhão",30),
        //            new State("MG","Minas Gerais",30),
        //            new State("MS","Mato Grosso do Sul",30),
        //            new State("MT","Mato Grosso",30),
        //            new State("PA","Pará",30),
        //            new State("PB","Paraíba",30),
        //            new State("PE","Pernambuco",30),
        //            new State("PI","Piauí",30),
        //            new State("PR","Paraná",30),
        //            new State("RJ","Rio de Janeiro",30),
        //            new State("RN","Rio Grande do Norte",30),
        //            new State("RO","Rondônia",30),
        //            new State("RR","Roraima",30),
        //            new State("RS","Rio Grande do Sul",30),
        //            new State("SC","Santa Catarina",30),
        //            new State("SE","Sergipe",30),
        //            new State("SP","São Paulo",30),
        //            new State("TO","Tocantins",30),


        //           }
        //       );
        //    }
        //}

        private void SeedActivitys(PlataformaRio2C.Infra.Data.Context.PlataformaRio2CContext context)
        {
            if (!context.Activities.Any())
            {
                context.Activities.AddRange(
                   new List<Activity>()
                   {
                       new Activity("Distribuidor | Distributor"),
                       new Activity("Canal de TV Aberta | Broadcast TV Channel"),
                       new Activity("VOD / OTT / Streaming"),
                       new Activity("Programador | Programmer"),
                       new Activity("Canal de TV por assinatura | Pay-TV Channel"),
                       new Activity("Produtora  | Producer"),
                       new Activity("Outras Mídias | Other Media")
                   }
               );
            }
        }
        private void SeedInterests(PlataformaRio2C.Infra.Data.Context.PlataformaRio2CContext context)
        {
            if (!context.InterestGroups.Any())
            {
                var groupPlatform = new InterestGroup("Plataformas | Platforms", InterestGroupTypeCodes.Multiple);
                var groupProjectStatus = new InterestGroup("Status do projeto | Project Status", InterestGroupTypeCodes.Multiple);
                var groupSeeking = new InterestGroup("Está no mercado buscando | Seeking", InterestGroupTypeCodes.Multiple);
                var groupFormat = new InterestGroup("Formato | Format", InterestGroupTypeCodes.Multiple);
                var groupAudiovisualGenre = new InterestGroup("Gênero | Genre", InterestGroupTypeCodes.Multiple);
                var groupSubgenre = new InterestGroup("Subgênero | Sub-genre", InterestGroupTypeCodes.Multiple);

                context.InterestGroups.AddRange(
                   new List<InterestGroup>()
                   {
                       groupPlatform,
                       groupProjectStatus,
                       groupSeeking,
                       groupFormat,
                       groupAudiovisualGenre,
                       groupSubgenre
                   }
               );

                if (!context.Interests.Any())
                {
                    context.Interests.AddRange(
                      new List<Interest>()
                      {
                              // -----------------------------------
                               new Interest("App", groupPlatform),
                               new Interest("Cinema", groupPlatform),
                               new Interest("Digital", groupPlatform),
                               new Interest("TV", groupPlatform),
                               new Interest("Outras Mídias | Other Media", groupPlatform),
                               // -----------------------------------
                               new Interest("Desenvolvimento | In Development", groupProjectStatus),
                               new Interest("Produção | In Production", groupProjectStatus),
                               new Interest("Finalizado (inédito) | Finished (no prior exhibition)", groupProjectStatus),
                               new Interest("Catálogo | Catalogue", groupProjectStatus),
                               // -----------------------------------
                               new Interest("Aquisição/ Licenciamento | Acquisition/ Licensing", groupSeeking),
                               new Interest("Coprodução | Co-production", groupSeeking),
                               // -----------------------------------
                               new Interest("Games e APPs", groupFormat),
                               new Interest("Novela | Telenovela", groupFormat),
                               new Interest("Série | Series", groupFormat),
                               new Interest("Minissérie | Miniseries", groupFormat),
                               new Interest("Longa | Feature Film", groupFormat),
                               new Interest("Interprograma | Short-format series", groupFormat),
                               new Interest("Curta | Short Film", groupFormat),
                               new Interest("Podcast", groupFormat),
                               new Interest("Reality Show", groupFormat),
                               new Interest("Streaming/Live", groupFormat),
                               new Interest("VR + AR + XR", groupFormat),
                               new Interest("Variedades | Variety Shows", groupFormat),
                               // -----------------------------------
                               new Interest("Animação | Animation", groupAudiovisualGenre),
                               new Interest("Documentário/Factual | Documentary/Factual", groupAudiovisualGenre),
                               new Interest("Ficção | Fiction", groupAudiovisualGenre),
                               new Interest("Kids", groupAudiovisualGenre),
                               new Interest("Show | Concert", groupAudiovisualGenre),
                               // -----------------------------------
                               new Interest("Ação | Action", groupSubgenre),
                               new Interest("Arte/Cultura | Art/Culture", groupSubgenre),
                               new Interest("Aventura | Adventure", groupSubgenre),
                               new Interest("Biografia | Biography ", groupSubgenre),
                               new Interest("Comédia | Comedy", groupSubgenre),
                               new Interest("Drama", groupSubgenre),
                               new Interest("Esportes | Sports", groupSubgenre),
                               new Interest("Família | Family", groupSubgenre),
                               new Interest("Faroeste | Western", groupSubgenre),
                               new Interest("Ficção | Sci-Fi", groupSubgenre),
                               new Interest("Gastronomia | Gastronomy", groupSubgenre),
                               new Interest("Música | Music", groupSubgenre),
                               new Interest("Policial/Crime | Cop/Crime", groupSubgenre),
                               new Interest("Romance", groupSubgenre),
                               new Interest("Suspense/Mistério | Thriller/Mystery", groupSubgenre),
                               new Interest("Terror | Horror", groupSubgenre),
                               new Interest("Viagem | Travel", groupSubgenre),
                               new Interest("Factual Life Style", groupSubgenre),
                      }
                    );
                }
            }

        }
        private void SeedTargetAudiences(PlataformaRio2C.Infra.Data.Context.PlataformaRio2CContext context)
        {
            if (!context.TargetAudiences.Any())
            {
                context.TargetAudiences.AddRange(
                   new List<TargetAudience>()
                   {
                       new TargetAudience("Pré-Escolar | Preschool"),
                       new TargetAudience("Infantil | Children"),
                       new TargetAudience("Infanto-Juvenil | Tween"),
                       new TargetAudience("Jovem | Young Adults"),
                       new TargetAudience("Adulto | Adult"),
                   }
               );
            }
        }
        private void SeedProjectStatus(PlataformaRio2CContext context)
        {
            var statusDtos = default(StatusProjectCodes).ToEnumStrings(false);

            foreach (var statusItem in statusDtos)
            {
                if (!context.ProjectStatus.Any(e => e.Code == statusItem.Value))
                {
                    context.ProjectStatus.Add(new ProjectStatus(statusItem.Value, statusItem.Description));
                }
            }
        }

        //private void SeedRooms(PlataformaRio2C.Infra.Data.Context.PlataformaRio2CContext context)
        //{
        //    if (!context.Rooms.Any())
        //    {
        //        var languagePt = context.Languages.FirstOrDefault(e => e.Code == "PtBr");
        //        var languageEn = context.Languages.FirstOrDefault(e => e.Code == "En");

        //        var roomNameGrandeSalaPt = new RoomName("Sala 1: Grande Sala", "PtBr");
        //        roomNameGrandeSalaPt.SetLanguage(languagePt);
        //        var roomNameGrandeSalaEn = new RoomName("Sala 1: Grande Sala", "En");
        //        roomNameGrandeSalaEn.SetLanguage(languageEn);
        //        var namesGrandeSala = new List<RoomName>() { roomNameGrandeSalaPt, roomNameGrandeSalaEn };

        //        var roomNameTeatroCamaraPt = new RoomName("Sala 2: Teatro de Câmara", "PtBr");
        //        roomNameTeatroCamaraPt.SetLanguage(languagePt);
        //        var roomNameTeatroCamaraEn = new RoomName("Sala 2: Teatro de Câmara", "En");
        //        roomNameTeatroCamaraEn.SetLanguage(languageEn);
        //        var namesTeatroCamara = new List<RoomName>() { roomNameTeatroCamaraPt, roomNameTeatroCamaraEn };

        //        var roomNameSalaMusicaPt = new RoomName("Sala 3: Sala de Música", "PtBr");
        //        roomNameSalaMusicaPt.SetLanguage(languagePt);
        //        var roomNameSalaMusicaEn = new RoomName("Sala 3: Sala de Música", "En");
        //        roomNameSalaMusicaEn.SetLanguage(languageEn);
        //        var namesSalaMusica = new List<RoomName>() { roomNameSalaMusicaPt, roomNameSalaMusicaEn };

        //        var roomNameSalaInovacaoPt = new RoomName("Sala 4: Sala de Inovação", "PtBr");
        //        roomNameSalaInovacaoPt.SetLanguage(languagePt);
        //        var roomNameSalaInovacaoEn = new RoomName("Sala 4: Sala de Inovação", "En");
        //        roomNameSalaInovacaoEn.SetLanguage(languageEn);
        //        var namesSalaInovacao = new List<RoomName>() { roomNameSalaInovacaoPt, roomNameSalaInovacaoEn };

        //        var roomNameBrazilianContentPt = new RoomName("Sala 5: Sala Brazilian Content", "PtBr");
        //        roomNameBrazilianContentPt.SetLanguage(languagePt);
        //        var roomNameBrazilianContentEn = new RoomName("Sala 5: Sala Brazilian Content", "En");
        //        roomNameBrazilianContentEn.SetLanguage(languageEn);
        //        var namesBrazilianContent = new List<RoomName>() { roomNameBrazilianContentPt, roomNameBrazilianContentEn };

        //        var roomNamePitchingAudioVisualPt = new RoomName("Sala 6: Sala de Pitching de ÁudioVisual", "PtBr");
        //        roomNamePitchingAudioVisualPt.SetLanguage(languagePt);
        //        var roomNamePitchingAudioVisualEn = new RoomName("Sala 6: Sala de Pitching de ÁudioVisual", "En");
        //        roomNamePitchingAudioVisualEn.SetLanguage(languageEn);
        //        var namesPitchingAudioVisual = new List<RoomName>() { roomNamePitchingAudioVisualPt, roomNamePitchingAudioVisualEn };

        //        var roomNameRodadasNegocio1Pt = new RoomName("Rodadas de Negócio (Sala 1)", "PtBr");
        //        roomNameRodadasNegocio1Pt.SetLanguage(languagePt);
        //        var roomNameRodadasNegocio1En = new RoomName("One-to-One meetings - ROOM 1", "En");
        //        roomNameRodadasNegocio1En.SetLanguage(languageEn);
        //        var namesRodadasNegocio1 = new List<RoomName>() { roomNameRodadasNegocio1Pt, roomNameRodadasNegocio1En };

        //        var roomNameRodadasNegocio2Pt = new RoomName("Rodadas de Negócio (Sala 2)", "PtBr");
        //        roomNameRodadasNegocio2Pt.SetLanguage(languagePt);
        //        var roomNameRodadasNegocio2En = new RoomName("One-to-One meetings - ROOM 2", "En");
        //        roomNameRodadasNegocio2En.SetLanguage(languageEn);
        //        var namesRodadasNegocio2 = new List<RoomName>() { roomNameRodadasNegocio2Pt, roomNameRodadasNegocio2En };

        //        context.Rooms.AddRange(
        //            new List<Room>()
        //            {
        //                new Room(namesGrandeSala),
        //                new Room(namesTeatroCamara),
        //                new Room(namesSalaMusica),
        //                new Room(namesSalaInovacao),
        //                new Room(namesBrazilianContent),
        //                new Room(namesPitchingAudioVisual),
        //                new Room(namesRodadasNegocio1),
        //                 new Room(namesRodadasNegocio2)
        //            }
        //        );
        //    }
        //}

        //private void SeedRoleLecturer(PlataformaRio2C.Infra.Data.Context.PlataformaRio2CContext context)
        //{
        //    if (!context.RoleLecturers.Any())
        //    {
        //        var languagePt = context.Languages.FirstOrDefault(e => e.Code == "PtBr");
        //        var languageEn = context.Languages.FirstOrDefault(e => e.Code == "En");

        //        var titlePalestrantePt = new RoleLecturerTitle("Palestrante", "PtBr");
        //        titlePalestrantePt.SetLanguage(languagePt);
        //        var titlePalestranteEn = new RoleLecturerTitle("Speaker", "En");
        //        titlePalestranteEn.SetLanguage(languageEn);
        //        var titlesPalestrante = new List<RoleLecturerTitle>() { titlePalestrantePt, titlePalestranteEn };

        //        var titleKeynotePt = new RoleLecturerTitle("Keynote", "PtBr");
        //        titleKeynotePt.SetLanguage(languagePt);
        //        var titleKeynoteEn = new RoleLecturerTitle("Keynote", "En");
        //        titleKeynoteEn.SetLanguage(languageEn);
        //        var titlesKeynotes = new List<RoleLecturerTitle>() { titleKeynotePt, titleKeynoteEn };

        //        var titleModeradorPt = new RoleLecturerTitle("Moderador", "PtBr");
        //        titleModeradorPt.SetLanguage(languagePt);
        //        var titleModeradorEn = new RoleLecturerTitle("Moderator", "En");
        //        titleModeradorEn.SetLanguage(languageEn);
        //        var titlesModeradores = new List<RoleLecturerTitle>() { titleModeradorPt, titleModeradorEn };

        //        var titleInterventorPt = new RoleLecturerTitle("Interventor", "PtBr");
        //        titleInterventorPt.SetLanguage(languagePt);
        //        var titleInterventorEn = new RoleLecturerTitle("Interventor", "En");
        //        titleInterventorEn.SetLanguage(languageEn);
        //        var titlesInterventores = new List<RoleLecturerTitle>() { titleInterventorPt, titleInterventorEn };

        //        var titleEntrevistadorPt = new RoleLecturerTitle("Entrevistador", "PtBr");
        //        titleEntrevistadorPt.SetLanguage(languagePt);
        //        var titleEntrevistadorEn = new RoleLecturerTitle("Interviewer", "En");
        //        titleEntrevistadorEn.SetLanguage(languageEn);
        //        var titlesEntrevistadores = new List<RoleLecturerTitle>() { titleEntrevistadorPt, titleEntrevistadorEn };

        //        var titleApresentadorPt = new RoleLecturerTitle("Apresentador", "PtBr");
        //        titleApresentadorPt.SetLanguage(languagePt);
        //        var titleApresentadorEn = new RoleLecturerTitle("Presenter", "En");
        //        titleApresentadorEn.SetLanguage(languageEn);
        //        var titlesApresentadores = new List<RoleLecturerTitle>() { titleApresentadorPt, titleApresentadorEn };

        //        context.RoleLecturers.AddRange(
        //            new List<RoleLecturer>()
        //            {
        //                        new RoleLecturer(titlesPalestrante),
        //                        new RoleLecturer(titlesKeynotes),
        //                        new RoleLecturer(titlesModeradores),
        //                        new RoleLecturer(titlesInterventores),
        //                        new RoleLecturer(titlesEntrevistadores),
        //                        new RoleLecturer(titlesApresentadores)
        //                    }
        //                );
        //    }
        //}
    }
}
