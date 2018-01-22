using ATTRepos.Issues.DataAccess.IssuesJson;
using ATTRepos.Issues.DataAccess.RepoIssues.IssuesJson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ATTRepos.Issues.DataAccess.RepoIssues.Client
{
    public class RepoClient : IRepoClient
    {
        private List<AttIssueObject> _attRepoIssues = new List<AttIssueObject>();
        private List<IssueObject> _repoIssues;
        private List<Comment> _comments;
        public List<AttIssueObject> GetRepoIssues()
        {
            // ATT public repos - https://api.github.com/orgs/att/repos
            // Get Issues - https://api.github.com/repos/att/{RepoName}/issues
            // Get Comments - https://api.github.com/repos/att/{RepoName}/issues/{Issue#}/comments

            List<string> repoList = GetATTRepos();

            foreach (var repo in repoList)
            {
                string url = @"https://api.github.com/repos/att/" + repo + "/issues";
                string jsonComments = "";
                using (WebClient wc = new WebClient())
                {
                    wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    wc.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(
                        Encoding.ASCII.GetBytes("spoombady:502b885f946540059521ec2d3fa5238b69bff7ff")));
                    var json = wc.DownloadString(url);
                    _repoIssues = (List<IssueObject>)JsonConvert.DeserializeObject((json), typeof(List<IssueObject>));
                }

                for (int i = 0; i < _repoIssues.Count - 1; i++)
                {
                    if (_repoIssues[i].comments > 0)
                    {
                        List<string> comments = new List<string>();
                        AttIssueObject attIssueObj = new AttIssueObject();

                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                            wc.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(
                                Encoding.ASCII.GetBytes("spoombady:502b885f946540059521ec2d3fa5238b69bff7ff")));
                            jsonComments = wc.DownloadString(_repoIssues[i].comments_url);
                            _comments = (List<Comment>)JsonConvert.DeserializeObject((jsonComments), typeof(List<Comment>));

                            foreach (var comment in _comments)
                            {
                                comments.Add(comment.body);
                            }
                            attIssueObj.Comments = comments;
                            attIssueObj.Issue = _repoIssues[i];
                            _attRepoIssues.Add(attIssueObj);
                        }
                    }
                }
            }
            return _attRepoIssues;
        }

        private List<string> GetATTRepos()
        {
            List<RepoObject> repos;
            List<string> attRepoList = new List<string>();
            string url = @"https://api.github.com/orgs/att/repos";

            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                wc.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(
                    Encoding.ASCII.GetBytes("spoombady:502b885f946540059521ec2d3fa5238b69bff7ff")));
                var json = wc.DownloadString(url);
                repos = (List<RepoObject>)JsonConvert.DeserializeObject((json), typeof(List<RepoObject>));
            }

            foreach (var repo in repos)
            {
                attRepoList.Add(repo.name);
            }

            return attRepoList;
        }
    }
}
