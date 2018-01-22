using ATTRepos.Issues.DataAccess.IssuesJson;
using System.Collections.Generic;

namespace ATTRepos.Issues.DataAccess.RepoIssues.IssuesJson
{
    public class AttIssueObject
    {
        public IssueObject Issue { get; set; }
        public List<string> Comments { get; set; }
    }
}