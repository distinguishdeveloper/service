using FiveDevs.DataAccess.Interface;
using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Reads the specified entity name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityName">Name of the entity.</param>
        /// <returns></returns>
        public async Task<T> Read<T>(string entityName)
        {
            return default(T);
        }
    }
}
