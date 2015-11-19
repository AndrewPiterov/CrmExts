using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace CrmExts.Extensions.Test
{
    public abstract class CrmSuperTest
    {
        protected IOrganizationService Context;
        protected ConcurrentBag<Entity> EntitiesTodeelete;
        
        protected static IOrganizationService GetContext()
        {
            var orgUrl = new Uri(@"http://<url>:<port>/<organization>/XRMServices/2011/Organization.svc");
            var creds = new ClientCredentials();
            creds.Windows.ClientCredential = new NetworkCredential("<login>", "<pass>", "<domen>");

            IOrganizationService service;

            using (var proxy = new OrganizationServiceProxy(orgUrl, null, creds, null))
            {
                // To impersonate set the GUID of CRM user here
                // serviceProxy.CallerId = Guid.NewGuid();
                // ObjectCacheManager.GetInstance().Clear();

                proxy.ServiceConfiguration.CurrentServiceEndpoint.Behaviors.Add(new ProxyTypesBehavior());
                proxy.Timeout = new TimeSpan(0, 1, 0, 0);
                service = proxy;
            }

            return service;
        }

        protected void Init()
        {
            Context = GetContext();

            EntitiesTodeelete = new ConcurrentBag<Entity>();
        }

        protected void CleanUp()
        {

            Parallel.ForEach(EntitiesTodeelete, new ParallelOptions { MaxDegreeOfParallelism = 4 }, e =>
            {
                var ctx = GetContext();

                ctx.Delete(e.LogicalName, e.Id);
            });
            
            Context = null;
        }

        protected Entity RandomLoan(string entityName)
        {
            var loan = Context.FirstOrDefault(new QueryExpression(entityName));
            return loan;
        }
    }
}
