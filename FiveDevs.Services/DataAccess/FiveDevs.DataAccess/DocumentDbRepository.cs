using FiveDevs.DataAccess.Interface;
using FiveDevs.DataContract;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveDevs.DataAccess
{
    /// <summary>
    /// Document Db Repository impelments IDocumentDbRepository
    /// </summary>
    public class DocumentDbRepository : IDocumentDbRepository
    {
        #region Properties

        /// <summary>
        /// The API key
        /// </summary>
        private string ApiKey = ConfigurationManager.AppSettings["DocDB-ApiKey"]; //"mmdubOth+HRk6VAkmILvfTIn2wKg0BwzkZvcqdNi7SJnmC/Rn7WtvX7Vfp8vyySHwVs6bl2F98yGd6/CL0H9Jw==");

        /// <summary>
        /// The endpoint URL
        /// </summary>
        private string EndpointUrl = ConfigurationManager.AppSettings["DocDB-EndpointUrl"]; //"https://devdoc.documents.azure.com:443/");

        #endregion

        #region Constructor

                /// <summary>
                /// Initializes a new instance of the <see cref="DocumentDbRepository"/> class.
                /// </summary>
                public DocumentDbRepository()
                {
                }

        #endregion

        #region IDocumentDbRepository Implementation

        /// <summary>
        /// Reads the specified entity name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityName">Name of the entity.</param>
        /// <returns></returns>
        public async Task<GenericResponse> Read<T>(string entityName)
        {
            return await GetDocuments(entityName);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the documents.
        /// </summary>
        /// <param name="entityName">Name of the entity.</param>
        /// <returns></returns>
        private async Task<GenericResponse> GetDocuments(string entityName)
        {
            var response = new GenericResponse();
            using (var client = new DocumentClient(new Uri(EndpointUrl), ApiKey))
            {
                DocumentCollection documentCollection = await GetDocumentCollection(client, entityName);
                    
                FeedOptions feeds = new FeedOptions() { MaxItemCount = 2 };
                    
                var query = client.CreateDocumentQuery<GenericResponse>(new Uri(documentCollection.DocumentsLink), feeds).AsQueryable();
                
                var feedResponse = await query.ExecuteNextAsync<GenericResponse>();

                response.Content = feedResponse.OrderBy(f => f.CreatedDate).ToList();
            };
            return response;        
        }
        
        /// <summary>
        /// Gets the document collection.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="entityName">Name of the entity.</param>
        /// <returns></returns>
        private async Task<DocumentCollection> GetDocumentCollection(DocumentClient client, string entityName)
        {
            Database database = client.CreateDatabaseQuery().AsEnumerable().FirstOrDefault(db => db.Id == entityName);

            DocumentCollection documentCollection = client.CreateDocumentCollectionQuery(new Uri(database.CollectionsLink)).AsEnumerable().FirstOrDefault(c => c.Id == entityName);
                       
            if (documentCollection != null)
            {
                documentCollection = await client.CreateDocumentCollectionAsync(database.CollectionsLink, new DocumentCollection
                    {
                        Id = entityName
                    });
            }
            return documentCollection;

        }

        #endregion
    }
}
