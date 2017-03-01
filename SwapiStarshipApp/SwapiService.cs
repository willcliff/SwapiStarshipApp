using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO;
using SwapiStarshipApp.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;

namespace SwapiStarshipApp
{
    /// <summary>
    /// Makes the requests, and retrieves the data from the Api..
    /// </summary>
    public class SwapiService
    {
        private enum HttpMethod
        {
            GET,
            POST
        }

        private string apiUrl = "http://swapi.co/api";
        private const string defaultPageNumber = "1";

        /// <summary>
        /// Create SwapiService object.
        /// </summary>
        public SwapiService()
        {
        }

        #region Private
        /// <summary>
        /// Makes the request to the Api.
        /// </summary>
        /// <param name="url">The address of the page</param>
        /// <param name="httpMethod">The method for the request</param>
        /// <returns>
        /// The result of the request.
        /// </returns>   
        private string Request(string url, HttpMethod httpMethod)
        {
            string result = string.Empty;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = httpMethod.ToString();

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream());
            result = reader.ReadToEnd();
            reader.Dispose();

            return result;
        }

        /// <summary>
        /// Creates string for page requested.
        /// </summary>
        /// <param name="dictionary">KeyValuePAir of the page and number</param>
        /// <returns>
        /// String for request.
        /// </returns> 
        private string SerializeDictionary(Dictionary<string, string> dictionary)
        {
            StringBuilder parameters = new StringBuilder();
            foreach (KeyValuePair<string, string> keyValuePair in dictionary)
            {
                parameters.Append(keyValuePair.Key + "=" + keyValuePair.Value + "&");
            }
            return parameters.Remove(parameters.Length - 1, 1).ToString();
        }

        /// <summary>
        /// Retrieves the <see cref="SwapiStarshipApp.Entities.Starship" /> for the gievn endpoint.
        /// </summary>
        /// <param name="endpoint">The address of the endpoint.</param>
        /// <param name="parameters">The address of the endpoint.</param>
        /// <returns>
        /// Returns Entity results.
        /// </returns> 
        private EntityResults<T> GetMultiple<T>(string endpoint, Dictionary<string, string> parameters) where T : BaseEntity
        {
            string serializedParameters = "";
            if (parameters != null)
            {
                serializedParameters = "?" + SerializeDictionary(parameters);
            }
            string json = Request(string.Format("{0}{1}{2}", apiUrl, endpoint, serializedParameters), HttpMethod.GET);
            EntityResults<T> swapiResponse = JsonConvert.DeserializeObject<EntityResults<T>>(json);
            return swapiResponse;
        }

        /// <summary>
        /// Retrieves the next page number
        /// </summary>
        /// <param name="dataWithQuery">The address of the next/previous page of entities</param>
        /// <returns>
        /// Returns the next/previous page number
        /// </returns>      
        private NameValueCollection GetQueryParameters(string dataWithQuery)
        {
            NameValueCollection result = new NameValueCollection();
            string[] parts = dataWithQuery.Split('?');
            if (parts.Length > 0)
            {
                string QueryParameter = parts.Length > 1 ? parts[1] : parts[0];
                if (!string.IsNullOrEmpty(QueryParameter))
                {
                    string[] p = QueryParameter.Split('&');
                    foreach (string s in p)
                    {
                        if (s.IndexOf('=') > -1)
                        {
                            string[] temp = s.Split('=');
                            result.Add(temp[0], temp[1]);
                        }
                        else
                        {
                            result.Add(s, string.Empty);
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Retrieves the next page number
        /// </summary>
        /// <param name="entityName">The name of the entity being retrieved</param>
        /// <param name="pageNumber">The number of the page being retrieved. Initially set to default.</param>
        /// <returns>
        /// Returns the next/previous page number
        /// </returns> 
        public EntityResults<T> GetAllPaginated<T>(string entityName, string pageNumber = defaultPageNumber) where T : BaseEntity
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("page", pageNumber);

            EntityResults<T> result = GetMultiple<T>(entityName, parameters);

            result.nextPageNo = String.IsNullOrEmpty(result.next) ? null : GetQueryParameters(result.next)["page"];
            result.previousPageNo = String.IsNullOrEmpty(result.previous) ? null : GetQueryParameters(result.previous)["page"];

            return result;
        }

        #endregion

        #region Public   

        /// <summary>
        /// Get all the starship entites on the given page
        /// </summary>
        public EntityResults<Starship> GetAllStarships(string pageNumber = defaultPageNumber)
        {
            EntityResults<Starship> result = GetAllPaginated<Starship>("/starships/", pageNumber);
            return result;
        }

        /// <summary>
        /// Retrieves the page with results <see cref="SwapiStarshipApp.Entities.EntityResults{T}" />.
        /// </summary>
        /// <returns>
        /// Returns a list of all <see cref="SwapiStarshipApp.Entities.Starship" /> objects.
        /// </returns>
        public List<Starship> GetStarships()
        {           
            List<Starship> allStarships = new List<Starship>();
            var starships = GetAllStarships();

            while (allStarships.Count < starships.count)
            {
                foreach (Starship starship in starships.results)
                {
                    allStarships.Add(starship);
                }
                if (allStarships.Count == starships.count)
                {
                    break;
                }
                starships = GetAllStarships(starships.nextPageNo);
            }
            return allStarships;
        }

        #endregion
    }
}

