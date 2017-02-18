using SharpStore.Data;
using SharpStore.Data.Models;
using SimpleHttpServer.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace SharpStore.Services
{
    public class KnivesService
    {
        private SharpStoreContext context;

        public KnivesService()
        {
            this.context = Data.Data.Context;
        }
        public IList<Knive> GetAllKnivesFromUrl(string url)
        {            
            int indexOfQ = url.IndexOf('?');
            if (indexOfQ == -1)
            {
                return this.context.Knives.ToList();
            }
            var variables = QueryStringParser.Parse(url.Substring(indexOfQ + 1));
            var knifeName = variables["knife-name"];
            return this.context.Knives.Where(k => k.Name.Contains(knifeName)).ToList();
        }
    }
}
