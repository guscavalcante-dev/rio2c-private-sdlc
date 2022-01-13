import pyodbc
import xlsxwriter
import pandas as pd
from datetime import date
import time
import argparse
import settings
from send_email import Email

parser = argparse.ArgumentParser()
parser.add_argument('--edition', default='2022')
args = parser.parse_args()
today = date.today()
timestr = time.strftime("%H_%M_%S")
editionId = 4
edition = args.edition

if edition == '2022':
    editionId = 4
if edition == '2020':
    editionId = 2
if edition == '2019':
    editionId = 1
if edition == '2018':
    editionId = 3

def send_mail(destinatariosEmail, file_path, file_name):
    remetente = settings.REMETENTE

    assuntoEmail = f'Relatório - Players Por Edição - '+edition

    mensagemEmail = f'<p>Prezados, segue em anexo o relatório de players por edição '+edition+'.</p>'
    Email.send_email(remetente, destinatariosEmail, assuntoEmail, mensagemEmail, file_path, file_name)

connection = pyodbc.connect('Driver={SQL Server};'
                    'Server=rio2c-sqlserver.cb5mokkxuw8r.us-east-1.rds.amazonaws.com;'
                    'Port=1433;'
                    'Database=MyRio2C_Prod;'
                    'uid=admin;'
                    'pwd=u-7JcerZR,otmC')

query_players = f'''
    SELECT
o.Name AS 'Nome (Admin)',
o.CompanyName AS 'Razão Social',
o.TradeName AS 'Nome Fantasia',
o.Document AS 'CNPJ',
o.Website,
o.SocialMedia AS 'Redes Sociais',
c.Name AS 'País',
REPLACE(REPLACE(REPLACE(od1.Value, char(9), ''), char(10), ''), char(13), '') AS 'Descrição (EN)',
REPLACE(REPLACE(REPLACE(od2.Value, char(9), ''), char(10), ''), char(13), '') AS 'Descrição (PT)',
STUFF
    (
        (
            SELECT ', ' + Interest  FROM (
                SELECT
                    CONVERT(varchar(max), i.Name) as 'Interest'
                FROM
                    dbo.OrganizationInterests oi
                    INNER JOIN dbo.Interests i ON oi.InterestId = i.Id AND i.IsDeleted = 0
                    INNER JOIN dbo.InterestGroups ig ON i.InterestGroupId = ig.Id AND ig.IsDeleted = 0
                WHERE
                    o.Id = oi.OrganizationId
                    AND ig.Name = 'Está buscando | Looking For'
                    AND  oi.IsDeleted = 0
            ) as InterestStuff
            FOR XML PATH (''), ROOT('MyString'), TYPE).value('/MyString[1]','varchar(max)')
    , 1, 2, '') AS 'Intereses - Está buscando | Looking For',
STUFF
    (
        (
            SELECT ', ' + Interest  FROM (
                SELECT
                    CONVERT(varchar(max), i.Name) as 'Interest'
                FROM
                    dbo.OrganizationInterests oi
                    INNER JOIN dbo.Interests i ON oi.InterestId = i.Id AND i.IsDeleted = 0
                    INNER JOIN dbo.InterestGroups ig ON i.InterestGroupId = ig.Id AND ig.IsDeleted = 0
                WHERE
                    o.Id = oi.OrganizationId
                    AND ig.Name = 'Status do projeto | Project Status'
                    AND  oi.IsDeleted = 0
            ) as InterestStuff
            FOR XML PATH (''), ROOT('MyString'), TYPE).value('/MyString[1]','varchar(max)')
    , 1, 2, '') AS 'Intereses - Status do projeto | Project Status',
STUFF
    (
        (
            SELECT ', ' + Interest  FROM (
                SELECT
                    CONVERT(varchar(max), i.Name) as 'Interest'
                FROM
                    dbo.OrganizationInterests oi
                    INNER JOIN dbo.Interests i ON oi.InterestId = i.Id AND i.IsDeleted = 0
                    INNER JOIN dbo.InterestGroups ig ON i.InterestGroupId = ig.Id AND ig.IsDeleted = 0
                WHERE
                    o.Id = oi.OrganizationId
                    AND ig.Name = 'Plataformas | Platforms'
                    AND  oi.IsDeleted = 0
            ) as InterestStuff
            FOR XML PATH (''), ROOT('MyString'), TYPE).value('/MyString[1]','varchar(max)')
    , 1, 2, '') AS 'Intereses - Plataformas | Platforms',
STUFF
    (
        (
            SELECT ', ' + Interest  FROM (
                SELECT
                    CONVERT(varchar(max), i.Name) as 'Interest'
                FROM
                    dbo.OrganizationInterests oi
                    INNER JOIN dbo.Interests i ON oi.InterestId = i.Id AND i.IsDeleted = 0
                    INNER JOIN dbo.InterestGroups ig ON i.InterestGroupId = ig.Id AND ig.IsDeleted = 0
                WHERE
                    o.Id = oi.OrganizationId
                    AND ig.Name = 'Formato | Format'
                    AND  oi.IsDeleted = 0
            ) as InterestStuff
            FOR XML PATH (''), ROOT('MyString'), TYPE).value('/MyString[1]','varchar(max)')
    , 1, 2, '') AS 'Intereses - Formato | Format',
STUFF
    (
        (
            SELECT ', ' + Interest  FROM (
                SELECT
                    CONVERT(varchar(max), i.Name) as 'Interest'
                FROM
                    dbo.OrganizationInterests oi
                    INNER JOIN dbo.Interests i ON oi.InterestId = i.Id AND i.IsDeleted = 0
                    INNER JOIN dbo.InterestGroups ig ON i.InterestGroupId = ig.Id AND ig.IsDeleted = 0
                WHERE
                    o.Id = oi.OrganizationId
                    AND ig.Name = 'Gênero | Genre'
                    AND  oi.IsDeleted = 0
            ) as InterestStuff
            FOR XML PATH (''), ROOT('MyString'), TYPE).value('/MyString[1]','varchar(max)')
    , 1, 2, '') AS 'Intereses - Gênero | Genre',
STUFF
    (
        (
            SELECT ', ' + Interest  FROM (
                SELECT
                    CONVERT(varchar(max), i.Name) as 'Interest'
                FROM
                    dbo.OrganizationInterests oi
                    INNER JOIN dbo.Interests i ON oi.InterestId = i.Id AND i.IsDeleted = 0
                    INNER JOIN dbo.InterestGroups ig ON i.InterestGroupId = ig.Id AND ig.IsDeleted = 0
                WHERE
                    o.Id = oi.OrganizationId
                    AND ig.Name = 'Subgênero | Sub-genre'
                    AND  oi.IsDeleted = 0
            ) as InterestStuff
            FOR XML PATH (''), ROOT('MyString'), TYPE).value('/MyString[1]','varchar(max)')
    , 1, 2, '') AS 'Intereses - Subgênero | Sub-genre',
STUFF
    (
        (
            SELECT ', ' + Audiencia  FROM (
                SELECT
                    CONVERT(varchar(max), ta.Name) as 'Audiencia'
                FROM
                    dbo.OrganizationTargetAudiences ota
                    INNER JOIN dbo.TargetAudiences ta ON  ota.TargetAudienceId  =ta.Id
                WHERE
                    o.Id = ota.OrganizationId
                    AND ota.IsDeleted = 0
            ) as InterestStuff
            FOR XML PATH (''), ROOT('MyString'), TYPE).value('/MyString[1]','varchar(max)')
    , 1, 2, '') AS 'Público-Alvo',
    CASE
        WHEN o.ImageUploadDate IS NOT NULL THEN 'https://assets.my.rio2c.com/img/organizations/' + lower(o.uid) + '_thumbnail.png'
        ELSE NULL
    END AS 'Logo',
CASE
    WHEN aot.IsApiDisplayEnabled = 1 THEN 'Sim'
    ELSE 'Não'
END AS 'Exibido no site?',
ao.OnboardingStartDate AS 'Dt. Início Onboarding',
ao.OnboardingFinishDate AS 'Dt. Término Onboarding',
(
    SELECT COUNT(*)
    FROM
        dbo.ProjectBuyerEvaluations pbe
        INNER JOIN dbo.Projects p ON pbe.ProjectId = p.Id AND p.IsDeleted = 0 AND p.FinishDate IS NOT NULL
    WHERE
        ao.Id = pbe.BuyerAttendeeOrganizationId
        AND pbe.IsDeleted = 0
) as 'Qtde. Projeto Recebidos'
FROM
Organizations o
INNER JOIN AttendeeOrganizations ao ON o.Id = ao.OrganizationId AND ao.EditionId = '{editionId}' AND ao.IsDeleted = 0
INNER JOIN AttendeeOrganizationTypes aot ON ao.Id = aot.AttendeeOrganizationId AND aot.OrganizationTypeId = 1 AND aot.IsDeleted = 0
LEFT OUTER JOIN Addresses a ON o.AddressId = a.id AND o.IsDeleted = 0
LEFT OUTER JOIN Countries c ON a.CountryId = c.id AND c.IsDeleted = 0
LEFT OUTER JOIN OrganizationDescriptions od1 ON o.Id = od1.OrganizationId AND od1.LanguageId = 1 AND od1.IsDeleted = 0
LEFT OUTER JOIN OrganizationDescriptions od2 ON o.Id = od2.OrganizationId AND od2.LanguageId = 2 AND od2.IsDeleted = 0
WHERE
o.IsDeleted = 0
ORDER BY
o.Name;

    '''
df_players = pd.read_sql_query(query_players, connection)

if df_players.empty:
    print('Não foram encontrados players para geração do arquivo')
else:
    file_name = 'relatorio-players-edicao-' + \
        edition+'_em_'+today.strftime("%d_%m_%Y")+'_as_'+timestr+'.xlsx'
    file_path = settings.DEFAULT_FILE_PATH

    workbook = xlsxwriter.Workbook(file_path + file_name)
    worksheet = workbook.add_worksheet()

    worksheet.write('A1', 'Nome (Admin)')
    worksheet.write('B1', 'Razão Social')
    worksheet.write('C1', 'Nome Fantasia')
    worksheet.write('D1', 'CNPJ')
    worksheet.write('E1', 'Website')
    worksheet.write('F1', 'Redes Sociais')
    worksheet.write('G1', 'País')
    worksheet.write('H1', 'Descrição (EN)')
    worksheet.write('I1', 'Descrição (PT)')
    worksheet.write('J1', 'Intereses - Está buscando | Looking For')
    worksheet.write('K1', 'Intereses - Status do projeto | Project Status')
    worksheet.write('L1', 'Intereses - Plataformas | Platforms')
    worksheet.write('M1', 'Intereses - Formato | Format')
    worksheet.write('N1', 'Intereses - Gênero | Genre')
    worksheet.write('O1', 'Intereses - Subgênero | Sub-genre')
    worksheet.write('P1', 'Público-Alvo')
    worksheet.write('Q1', 'Logo')
    worksheet.write('R1', 'Exibido no site?')
    worksheet.write('S1', 'Dt. Início Onboarding')
    worksheet.write('T1', 'Dt. Término Onboarding')
    worksheet.write('U1', 'Qtde. Projeto Recebidos')

    for i, row in df_players.iterrows():    
        worksheet.write('A'+str(i+2), row[0])
        worksheet.write('B'+str(i+2), row[1])
        worksheet.write('C'+str(i+2), row[2])
        worksheet.write('D'+str(i+2), row[3])
        worksheet.write('E'+str(i+2), row[4])
        worksheet.write('F'+str(i+2), row[5])
        worksheet.write('G'+str(i+2), row[6])
        worksheet.write('H'+str(i+2), row[7])
        worksheet.write('I'+str(i+2), row[8])
        worksheet.write('J'+str(i+2), row[9])
        worksheet.write('K'+str(i+2), row[10])
        worksheet.write('L'+str(i+2), row[11])
        worksheet.write('M'+str(i+2), row[12])
        worksheet.write('N'+str(i+2), row[13])
        worksheet.write('O'+str(i+2), row[14])
        worksheet.write('P'+str(i+2), row[15])
        worksheet.write('Q'+str(i+2), row[16])
        worksheet.write('R'+str(i+2), row[17])
        worksheet.write('S'+str(i+2), row[18])
        worksheet.write('T'+str(i+2), row[19])
        worksheet.write('U'+str(i+2), row[20])

    workbook.close()
    destinatariosEmail = settings.DESTINATARIOS
    send_mail(destinatariosEmail, file_path, file_name)


