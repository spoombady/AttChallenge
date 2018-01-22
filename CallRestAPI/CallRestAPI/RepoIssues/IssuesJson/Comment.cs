using ATTRepos.Issues.DataAccess.IssuesJson;
using System;

namespace ATTRepos.Issues.DataAccess.RepoIssues.IssuesJson
{
    public class Comment
    {
        public string url { get; set; }
        public string html_url { get; set; }
        public string issue_url { get; set; }
        public int id { get; set; }
        public User user { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string author_association { get; set; }
        public string body { get; set; }
    }
}
