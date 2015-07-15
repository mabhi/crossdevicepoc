using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortableCode.Models;

namespace PortableCode.Services
{
    class POCWebservice
    {
        static readonly POCWebservice instance = new POCWebservice();
        /// <summary>
        /// Gets the instance of the Azure Web Service
        /// </summary>
        public static POCWebservice Instance
        {
            get
            {
                return instance;
            }
        }

        public Task
    }
}
