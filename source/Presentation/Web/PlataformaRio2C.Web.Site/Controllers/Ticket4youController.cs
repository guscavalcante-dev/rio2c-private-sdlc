// ***********************************************************************
// Assembly         : PlataformaRio2C.Web.Site
// Author           : Rafael Dantas Ruiz
// Created          : 06-28-2019
//
// Last Modified By : Rafael Dantas Ruiz
// Last Modified On : 08-07-2019
// ***********************************************************************
// <copyright file="Ticket4youController.cs" company="Softo">
//     Copyright (c) Softo. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using MediatR;
using PlataformaRio2C.Infra.CrossCutting.Identity.Service;
using PlataformaRio2C.Web.Site.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PlataformaRio2C.Web.Site.Controllers
{
    /// <summary>Ticket4youController</summary>
    public class Ticket4youController : BaseController
    {
        private string urlJson = "http://rio2c.ticketforyou.com.br/Web/Api/Controller/APIExterna/dados_comprador.php";
        public TicketJsonResponse UserTicket { get; set; }

        /// <summary>Initializes a new instance of the <see cref="Ticket4youController"/> class.</summary>
        /// <param name="commandBus">The command bus.</param>
        /// <param name="identityController">The identity controller.</param>
        public Ticket4youController(IMediator commandBus, IdentityAutenticationService identityController)
            : base(commandBus, identityController)
        {
        }

        public IEnumerable<TicketJsonResponse> SearchAll()
        {
            var json = new WebClient().DownloadString(urlJson);

            List<TicketJsonResponse> teste = new List<TicketJsonResponse>();

            dynamic jsonList = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            foreach (dynamic jsonOne in jsonList)
            {
                TicketJsonResponse response = new TicketJsonResponse();
                IngressoTicket IngressoResponse = new IngressoTicket();
                List<CredencialTicket> CredencialResponse = new List<CredencialTicket>();

                response.nome_pessoa = jsonOne.nome_pessoa;
                response.sobrenome_pessoa = jsonOne.sobrenome_pessoa;
                response.email = jsonOne.email;
                response.senha = jsonOne.senha;
                response.status_pedido = jsonOne.status_pedido;
                response.tipo_compra = jsonOne.tipo_compra;

                //var credenciados = jsonOne.credenciados;

                //if (credenciados.First.Count > 0)
                //{
                //    var teste2r = false;
                //    foreach (CredencialTicket credenciado in credenciados)
                //    {
                //        CredencialTicket CredencialOne = new CredencialTicket(credenciado);

                //        CredencialOne.email_credencial = credenciado.email_credencial;
                //        CredencialOne.nome_credencial = credenciado.nome_credencial;
                //        CredencialOne.empresa_credencial = credenciado.empresa_credencial;

                //        CredencialResponse.Add(CredencialOne);
                //    }
                //}

                response.Credencial = CredencialResponse;

                //response.data_venda = jsonOne.data_venda;
                //response.ingresso = new List<IngressoTicket>();

                //foreach (dynamic ingressoOne in jsonOne.ingresso)
                //{
                //    string cupom = (ingressoOne.cupom == null ? "" : ingressoOne.cupom);
                //    IngressoResponse.nome_ingresso = ingressoOne.nome_ingresso;
                //    IngressoResponse.qtd_ingresso = ingressoOne.qtd_ingresso;
                //    IngressoResponse.cupom = cupom;

                //    response.ingresso.Add(IngressoResponse);
                //}



                teste.Add(response);
            }

            return teste;
        }

        public TicketJsonResponse SearchOne(string email)
        {
            List<TicketJsonResponse> teste = SearchAll().ToList();
            TicketJsonResponse busca = new TicketJsonResponse();

            foreach (TicketJsonResponse testeOne in teste)
            {
                if (testeOne.email == email)
                {
                    busca = testeOne;
                }
                /*
                else
                {
                    if (testeOne.Credencial != null)
                    {
                        var search = testeOne.Credencial.FirstOrDefault(u => u.email_credencial == email);

                        if (search != null)
                        {
                            busca = testeOne;
                        }
                    }
                }
                */

            }

            if (busca != null)
            {
                UserTicket = busca;
            }

            return busca;
        }
    }
}