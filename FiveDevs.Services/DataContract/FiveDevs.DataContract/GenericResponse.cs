using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveDevs.DataContract
{
    /// <summary>
    /// Generic Response
    /// </summary>
    public class GenericResponse
    {
        /// <summary>
        /// Gets or sets the name of the entity.
        /// </summary>
        /// <value>
        /// The name of the entity.
        /// </value>
        public string EntityName { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public dynamic Content { get; set; }
    }
}
