import base64
import os
from sendgrid.helpers.mail import *
from sendgrid import SendGridAPIClient
import settings

class Email:
    @staticmethod
    def send_email(remetentes, destinatarios, assunto, mensagem, diretorio_anexo=None, nome_arquivo=None):
        email = Mail(
            from_email=remetentes,
            subject=assunto,
            html_content=mensagem)

        personalization = Personalization()
        for destinatario in destinatarios:
            personalization.add_to(To(destinatario))
        email.add_personalization(personalization)

        if diretorio_anexo is not None and nome_arquivo is not None:
            Email.anexar_arquivo(email, diretorio_anexo, nome_arquivo)

        try:
            sg = SendGridAPIClient(api_key=settings.SENDGRID_API_KEY)
            sg.send(email)
            print(f'E-mail {assunto} enviado com sucesso')
        except Exception as e:
            print(f'O e-mail não foi enviado: {e}')

    @staticmethod
    def anexar_arquivo(email, diretorio_anexo, nome_arquivo):
        with open(diretorio_anexo + nome_arquivo, 'rb') as f:
            data = f.read()
            f.close()
        encoded_file = base64.b64encode(data).decode()

        attachedFile = Attachment(
            FileContent(encoded_file),
            FileName(nome_arquivo),
            FileType('application/vnd. ms-excel'),
            Disposition('attachment')
        )
        email.attachment = attachedFile
