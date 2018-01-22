using ATTRepos.Issues.DataAccess.IssuesJson;
using ATTRepos.Issues.DataAccess.RepoIssues.IssuesJson;
using System.Collections.Generic;

namespace ATTRepos.Issues.DataAccess.RepoIssues.Client
{
    public interface IRepoClient
    {
        List<AttIssueObject> GetRepoIssues();
    }
}
