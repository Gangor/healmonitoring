using HealMonitoring.DB;
using RestSharp;
using Saleforce.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Saleforce
{
    public class Process
    {
        const string baseUri = "http://localhost:5036/";

        public List<Conges> Conges = new List<Conges>();

        private Connexion Connexion;

        public Process(Connexion connexion)
        {
            Connexion = connexion;
        }

        #region Data fetching

        public void FetchingConges()
        {
            var result = RequestFactory(Method.Get, $"api/saleforce/conges/{Connexion.ID_User}")
                                .ThrowIfError();

            Conges = JsonSerializer.Deserialize<List<Conges>>(result.Content);
        }

        #endregion

        private List<Information> CalculateScore()
        {
            var informations = new List<Information>();

            foreach (var conge in Conges)
            {
                informations.Add(new Information
                {
                    Date = conge.StartDate,
                    DataSources = nameof(Saleforce),
                    Score = conge.GetScore(),
                    TypeDeDonnee = "conges",
                    ID_User = Connexion.ID_User
                });
            }

            return informations;
        }

        public List<Information> Execute()
        {
            FetchingConges();
            return CalculateScore();
        }

        #region Helper

        public RestResponse RequestFactory(Method method, string action)
        {
            RestClient client = new RestClient(baseUri);
            RestRequest request = new RestRequest(baseUri + action, method);

            return client.Execute(request);
        }

        #endregion
    }
}