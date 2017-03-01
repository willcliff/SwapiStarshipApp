using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;

namespace SwapiStarshipApp.Entities
{
    /// <summary>
    /// Class Helper contains results and option to navigate to other pages.
    /// </summary>
    /// /// <typeparam name="T"><see cref="SwapiStarshipApp.Entities.BaseEntity" />Base entity.</typeparam>
    public class EntityResults<T> : BaseEntity where T : BaseEntity
    {
        /// <summary>
        /// Gets or sets the previous page.
        /// </summary>
        /// <value>The previous page.</value>
        public string previous
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the next page.
        /// </summary>
        /// <value>The next page.</value>
        public string next
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the next page number.
        /// </summary>
        /// <value>The previous page number.</value>
        public string previousPageNo
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the next page number.
        /// </summary>
        /// <value>The next page number.</value>
        public string nextPageNo
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the count of the results.
        /// </summary>
        /// <value>The count of the results.</value>
        public Int64 count
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the results downloaded from http://SWAPI.co/.
        /// </summary>
        /// <value>The results.</value>
        public List<T> results
        {
            get;
            set;
        }
    }
}

