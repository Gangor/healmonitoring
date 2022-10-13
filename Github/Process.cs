using Github.Models;
using HealMonitoring.DB;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Github
{
    public class Process
    {
        const string baseUri = "https://api.github.com/";

        public List<Repository> Repositories = new List<Repository>();

        private Connexion Connexion;

        public Process(Connexion connexion)
        {
            Connexion = connexion;
        }

        #region Data fetching

        private void FetchRepositories()
        {
            var result = RequestFactory(Method.Get, $"users/{Connexion.User}/repos")
                                .ThrowIfError();

            Repositories = JsonSerializer.Deserialize<List<Repository>>(result.Content);

            foreach (var repository in Repositories)
            {
                GetCommit(repository);
            }
        }

        private void GetCommit(Repository repository)
        {
            var result = RequestFactory(Method.Get, $"repos/{repository.owner.login}/{repository.name}/commits")
                                .ThrowIfError();

            var commits = JsonSerializer.Deserialize<List<Commit>>(result.Content);

            foreach (var commit in commits)
            {
                // Merge request does not have author
                if (commit.author == null)
                    continue;

                // Skip non personnal commit
                if (!commit.author.login.Equals(Connexion.User, StringComparison.OrdinalIgnoreCase))
                    continue;

                repository.Commits.Add(commit);

                GetStats(repository, commit);
            }
        }

        private void GetStats(Repository repository, Commit commit)
        {
            var result = RequestFactory(Method.Get, $"repos/{repository.owner.login}/{repository.name}/commits/{commit.sha}")
                                .ThrowIfError();

            var tmp = JsonSerializer.Deserialize<Commit>(result.Content);

            commit.commit = tmp.commit;
            commit.files = tmp.files;
        }

        #endregion


        private List<Information> CalculateScore()
        {
            var informations = new List<Information>();

            foreach (var repository in Repositories)
            {
                foreach (var commit in repository.Commits)
                {
                    informations.Add(new Information
                    {
                        Date = DateTime.Parse(commit.commit.committer.date),
                        DataSources = nameof(Github),
                        Score = commit.GetScore(),
                        TypeDeDonnee = "commit",
                        ID_User = Connexion.ID_User
                    });
                }
            }

            return informations;
        }

        public List<Information> Execute()
        {
            FetchRepositories();
            return CalculateScore();
        }

        #region Helper

        public RestResponse RequestFactory(Method method, string action)
        {
            RestClient client = new RestClient(baseUri);
            RestRequest request = new RestRequest(baseUri + action, method);

            request.AddHeader("Authorization", $"Bearer {Connexion.Token}");

            return client.Execute(request);
        }

        #endregion
    }
}