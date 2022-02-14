using Elselam.UnityRouter.Domain;
using Elselam.UnityRouter.Extensions;
using Elselam.UnityRouter.History;
using System.Collections.Generic;
using System.Linq;

namespace Elselam.UnityRouter.Url
{
    public class UrlManager : IUrlManager
    {
        private const string HISTORY_KEY = "history";
        private readonly IUrlDomainProvider urlDomain;
        private readonly IHistory history;

        public UrlManager(IUrlDomainProvider urlDomain, IHistory history)
        {
            this.urlDomain = urlDomain;
            this.history = history;
        }

        public string BuildToString(string screenName, IDictionary<string, string> parameters)
        {
            var parametersString = string.Empty;
            if (!parameters.IsNullOrEmpty())
            {
                foreach (var parameter in parameters)
                    parametersString += $"{parameter.Key}={parameter.Value}&";
                parametersString = parametersString.Remove(parametersString.Length - 1);
            }

            return string.IsNullOrEmpty(parametersString) ?
                $"{urlDomain.Url}{screenName}" :
                $"{urlDomain.Url}{screenName}?{parametersString}";
        }

        public ScreenScheme BuildToScheme(string screenName, IDictionary<string, string> parameters)
        {
            var value = BuildToString(screenName, parameters);
            return Deserialize(value);
        }

        public ScreenScheme Deserialize(string url)
        {
            var uri = url;
            if (uri.Contains(urlDomain.Url))
                uri = url.Replace(urlDomain.Url, string.Empty);

            uri = ExtractHistoryInUrl(uri);
            if (!uri.Contains('?'))
                return new ScreenScheme(url, uri);
            var elements = uri.Split('?');
            var screenName = elements[0];
            var parameters = elements[1].Split('&');

            var paramsDictionary = new Dictionary<string, string>();
            foreach (var parameter in parameters)
            {
                var splitParam = parameter.Split('=');
                var key = splitParam[0];
                var value = splitParam[1];
                paramsDictionary[key] = value;
            }

            return new ScreenScheme(url, screenName, paramsDictionary);
        }


        //TODO: create keywords registration with specific actions
        private string ExtractHistoryInUrl(string url)
        {
            if (!url.Contains(HISTORY_KEY))
                return url;

            var originalUrl = url;
            var historyStartKeywordIndex = url.IndexOf(HISTORY_KEY);
            var historyStartContentIndex = historyStartKeywordIndex + HISTORY_KEY.Length + 1;
            int historyEndContentIndex = 0;

            for (var i = historyStartContentIndex; i < url.Length; i++)
            {
                if (url[i] == '&')
                {
                    historyEndContentIndex = i;
                    break;
                }
            }

            var links = url.Substring(historyStartContentIndex, historyEndContentIndex - historyStartContentIndex);
            var linksList = links.Split('-').ToList();
            AddToHistory(linksList);

            //remove '&' character from url
            var historyEndContentIndexWithArgSeparator = (historyEndContentIndex + 1);
            return originalUrl.Remove(historyStartKeywordIndex, historyEndContentIndexWithArgSeparator - historyStartKeywordIndex);
        }

        private void AddToHistory(IEnumerable<string> links)
        {
            foreach (var link in links)
            {
                history.Add(Deserialize(link));
            }
        }
    }
}