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

    assuntoEmail = f'Relatório - Executivos Por Edição - '+edition

    mensagemEmail = f'<p>Prezados, segue em anexo o relatório de executivos por edição '+edition+'.</p>'
    Email.send_email(remetente, destinatariosEmail, assuntoEmail, mensagemEmail, file_path, file_name)


connection = pyodbc.connect('Driver={SQL Server};'
                    'Server=rio2c-sqlserver.cb5mokkxuw8r.us-east-1.rds.amazonaws.com;'
                    'Port=1433;'
                    'Database=MyRio2C_Prod;'
                    'uid=admin;'
                    'pwd=u-7JcerZR,otmC')

query_executivos = f'''
    SELECT 
	o.Name AS 'Player - Nome (admin)', 
	o.TradeName AS 'Player - Nome Fantasia',
	c.Badge AS 'Badge',
	c.FirstName AS 'Primeiro Nome',
	c.LastNames AS 'Sobrenomes',
	u.Email AS 'Email de Acesso',
	c.CellPhone AS 'Celular',
	c.PhoneNumber AS 'Telefone',
	c.PublicEmail AS 'Email de Contato',
	ac.OnboardingStartDate AS 'Dt. Início Onboarding',
	ac.OnboardingFinishDate AS 'Dt. Término Onboarding',
	ac.PlayerTermsAcceptanceDate AS 'Dt. Aceite do Termo',
	CASE
		WHEN c.ImageUploadDate IS NOT NULL THEN 'https://assets.my.rio2c.com/img/users/' + lower(c.uid) + '_thumbnail.png'
		ELSE NULL
	END as 'Foto'
FROM 
	dbo.Collaborators c
	INNER JOIN dbo.Users u ON u.Id = c.Id AND u.IsDeleted = 0
	INNER JOIN dbo.AttendeeCollaborators ac ON ac.CollaboratorId = c.Id AND ac.EditionId = '{editionId}' AND ac.IsDeleted = 0
	INNER JOIN dbo.AttendeeOrganizationCollaborators aoc ON aoc.AttendeeCollaboratorId = ac.Id AND aoc.IsDeleted = 0
	INNER JOIN dbo.AttendeeOrganizations ao ON ao.Id = aoc.AttendeeOrganizationId AND ao.IsDeleted = 0
	INNER JOIN dbo.Organizations o ON o.Id = ao.OrganizationId AND o.IsDeleted = 0
	INNER JOIN dbo.AttendeeCollaboratorTypes act ON act.AttendeeCollaboratorId = ac.Id AND act.CollaboratorTypeId = 200 AND act.IsDeleted = 0
WHERE 
	c.IsDeleted = 0		
ORDER BY
	o.Name,
	c.FirstName,
	c.LastNames

    '''
df_executivos = pd.read_sql_query(query_executivos, connection)

if df_executivos.empty:
    print('Não foram encontrados executivos para geração do arquivo')
else:
    file_name = 'relatorio-executivos-edicao-' + \
        edition+'_em_'+today.strftime("%d_%m_%Y")+'_as_'+timestr+'.xlsx'
    file_path = settings.DEFAULT_FILE_PATH

    workbook = xlsxwriter.Workbook(file_path + file_name)
    worksheet = workbook.add_worksheet()

    worksheet.write('A1', 'Player - Nome (admin)')
    worksheet.write('B1', 'Player - Nome Fantasia')
    worksheet.write('C1', 'Badge')
    worksheet.write('D1', 'Primeiro Nome')
    worksheet.write('E1', 'Sobrenomes')
    worksheet.write('F1', 'Email de Acesso')
    worksheet.write('G1', 'Celular')
    worksheet.write('H1', 'Telefone')
    worksheet.write('I1', 'Email de Contato')
    worksheet.write('J1', 'Dt. Início Onboarding')
    worksheet.write('K1', 'Dt. Término Onboarding')
    worksheet.write('L1', 'Dt. Aceite do Termo')
    worksheet.write('M1', 'Foto')

    for i, row in df_executivos.iterrows():    
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

    workbook.close()
    destinatariosEmail = settings.DESTINATARIOS
    send_mail(destinatariosEmail, file_path, file_name)
