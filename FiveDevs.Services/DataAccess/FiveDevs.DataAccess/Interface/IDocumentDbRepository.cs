using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveDevs.DataAccess.Interface
{
    /// <summary>
    /// IDocument Db Repository
    /// </summary>
    public interface IDocumentDbRepository
    {
        /// <summary>
        /// Reads the specified entity name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityName">Name of the entity.</param>
        /// <returns></returns>
        Task<T> Read<T>(string entityName);
    }
}
