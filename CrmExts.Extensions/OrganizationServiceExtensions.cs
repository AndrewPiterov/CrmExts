using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Client.Caching;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmExts.Extensions
{
    public class OpStat
    {
        public bool IsSuccess { get; set; }
        public int ProcessedEntitiesCount { get; set; }
    }

    public class OrderBy
    {
        public string AttributeName { get; set; }
        public OrderType OrderType { get; set; }
    }


    public static class OrganizationServiceExtensions
    {
        public static bool IsValidDate(this IOrganizationService service, DateTime date)
        {
            return !(date == default(DateTime) || date < new DateTime(1900, 1, 1));

        }
        public static IEnumerable<Entity> Get(
            this IOrganizationService service,
            string entityname,
            bool allattributes = false,
            IEnumerable<OrderBy> orders = null,
            IEnumerable<ConditionExpression> conditions = null,
            IEnumerable<string> attrs = null,
            int? page = null,
            int? count = null)
        {
            ObjectCacheManager.GetInstance().Clear();

            if (string.IsNullOrEmpty(entityname)) throw new ArgumentNullException("entityname");

            var query = new QueryExpression(entityname);

            if (attrs != null)
            {
                var a = attrs.ToArray();

                if (allattributes)
                {
                    query.ColumnSet = new ColumnSet(true);
                }
                else
                {
                    query.ColumnSet = a.Count() == 0 ? new ColumnSet(false) : new ColumnSet(a);
                }
            }

            if (conditions != null)
            {
                conditions.ToList().ForEach(x => { query.Criteria.AddCondition(x); });
            }

            if (orders != null)
            {
                orders.ToList().ForEach(x => { query.AddOrder(x.AttributeName, x.OrderType); });
            }

            List<Entity> ents;

            if (page != null && count != null)
            {
                query.PageInfo = new PagingInfo
                {
                    Count = count.Value,
                    PageNumber = page.Value,
                    PagingCookie = null,
                    ReturnTotalRecordCount = true
                };

                ents = service.GetPage(query);
            }
            else
            {
                ents = service.GetAllEntities(query);
            }

            return ents;
        }

        public static IEnumerable<Entity> Get(
           this IOrganizationService service,
            out int totalItemsCount,
           string entityname,
           bool allattributes = false,
           IEnumerable<OrderBy> orders = null,
           IEnumerable<ConditionExpression> conditions = null,
           IEnumerable<string> attrs = null,
           int? page = null,
           int? count = null)
        {

            ObjectCacheManager.GetInstance().Clear();

            if (string.IsNullOrEmpty(entityname)) throw new ArgumentNullException("entityname");

            var query = new QueryExpression(entityname);

            if (attrs != null)
            {
                var a = attrs.ToArray();

                if (allattributes)
                {
                    query.ColumnSet = new ColumnSet(true);
                }
                else
                {
                    query.ColumnSet = a.Count() == 0 ? new ColumnSet(false) : new ColumnSet(a);
                }
            }

            if (conditions != null)
            {
                conditions.ToList().ForEach(x => { query.Criteria.AddCondition(x); });
            }

            if (orders != null)
            {
                orders.ToList().ForEach(x => { query.AddOrder(x.AttributeName, x.OrderType); });
            }

            List<Entity> ents;

            totalItemsCount = 0;

            if (page != null && count != null)
            {
                query.PageInfo = new PagingInfo
                {
                    Count = count.Value,
                    PageNumber = page.Value,
                    PagingCookie = null,
                    ReturnTotalRecordCount = true
                };

                ents = service.GetPage(query, out totalItemsCount);
            }
            else
            {
                ents = service.GetAllEntities(query);
            }

            return ents;
        }


        public static RepositoryQuery Query(this IOrganizationService service, string entityname)
        {
            var rq = new RepositoryQuery(service, entityname);
            return rq;
        }

        public static RepositoryQuery All(this IOrganizationService service, string entityname)
        {
            var rq = new RepositoryQuery(service, entityname);
            return rq;
        }

        public static Entity FirstOrDefault(this IOrganizationService service, QueryExpression query, params string[] attributes)
        {
            if (attributes == null || !attributes.Any())
            {
                query.ColumnSet.AllColumns = true;
                return service.RetrieveMultiple(query).Entities.FirstOrDefault();
            }

            query.ColumnSet.AllColumns = false;
            query.ColumnSet.AddColumns(attributes);

            return service.RetrieveMultiple(query).Entities.FirstOrDefault();
        }

        public static Entity First(this IOrganizationService service, QueryBase query)
        {
            var coll = service.RetrieveMultiple(query);

            return coll.Entities.First();
        }

        public static Entity FirstOrDefault(this IOrganizationService service, QueryBase query)
        {
            var coll = service.RetrieveMultiple(query);

            return coll.Entities.FirstOrDefault();
        }

        public static Entity FirstOrDefault(this IOrganizationService service,
            string entityName,
            ConditionExpression filter,
            bool withAttrs = false)
        {
            var q = new QueryExpression(entityName)
            {
                ColumnSet = new ColumnSet(withAttrs),

                Criteria =
                {
                    Conditions =
                    {
                        filter
                    }
                }
            };

            var coll = service.RetrieveMultiple(q);

            return coll.Entities.FirstOrDefault();
        }

        public static List<Entity> GetEntities(this IOrganizationService service, QueryExpression query)
        {
            return service.RetrieveMultiple(query).Entities.ToList();
        }
        
        /// <summary>
        /// Активировать карточку
        /// </summary>
        /// <param name="organizationService"></param>
        /// <param name="record"></param>
        public static void ActivateRecord(this IOrganizationService organizationService, Entity record)
        {
            var cols = new ColumnSet("statecode", "statuscode");

            //Check if it is Inactive or not
            var entity = organizationService.Retrieve(record.LogicalName, record.Id, cols);

            if (entity != null && entity.GetAttributeValue<OptionSetValue>("statecode").Value != 0)
            {
                //StateCode = 0 and StatusCode = 1 for activating Account or Contact
                SetStateRequest setStateRequest = new SetStateRequest()
                {
                    EntityMoniker = new EntityReference
                    {
                        Id = record.Id,
                        LogicalName = record.LogicalName,
                    },

                    State = new OptionSetValue(0),
                    Status = new OptionSetValue(1)
                };
                organizationService.Execute(setStateRequest);
            }
        }

        /// <summary>
        /// Декактивировать карточку
        /// </summary>
        /// <param name="organizationService"></param>
        /// <param name="record"></param>
        public static void DeactivateRecord(this IOrganizationService organizationService, Entity record)
        {
            organizationService.DeactivateRecord(record.Id, record.LogicalName);
        }

        public static void DeactivateRecord(this IOrganizationService organizationService, Guid entityId,
            string entityName)
        {

            if (entityId == Guid.Empty) throw new ArgumentNullException("entityId");
            if (String.IsNullOrEmpty(entityName)) throw new ArgumentNullException("entityName");

            var cols = new ColumnSet("statecode", "statuscode");

            //Check if it is Active or not
            var entity = organizationService.Retrieve(entityName, entityId, cols);

            if (entity != null && entity.GetAttributeValue<OptionSetValue>("statecode").Value == 0)
            {
                //StateCode = 1 and StatusCode = 2 for deactivating Account or Contact
                SetStateRequest setStateRequest = new SetStateRequest()
                {
                    EntityMoniker = new EntityReference
                    {
                        Id = entityId,
                        LogicalName = entityName,
                    },

                    State = new OptionSetValue(1),
                    Status = new OptionSetValue(-1)
                };
                organizationService.Execute(setStateRequest);
            }
        }

        public static bool UpdateBatch(this IOrganizationService organizationService, List<Entity> batchEntities, int countEntitiesPerBatch = 100)
        {
            if (batchEntities == null)
            {
                throw new ArgumentNullException();
            }

            var pages = batchEntities.ToPages(countEntitiesPerBatch);

            foreach (var page in pages)
            {
                // Create an ExecuteMultipleRequest object.
                var multipleRequest = new ExecuteMultipleRequest()
                {
                    // Assign settings that define execution behavior: continue on error, return responses. 
                    Settings = new ExecuteMultipleSettings()
                    {
                        ContinueOnError = false,
                        ReturnResponses = true
                    },
                    // Create an empty organization request collection.
                    Requests = new OrganizationRequestCollection()
                };

                // Add a UpdateRequest for each entity to the request collection.
                foreach (var entity in page.Value)
                {
                    var updateRequest = new UpdateRequest { Target = entity };
                    multipleRequest.Requests.Add(updateRequest);
                }

                // Execute all the requests in the request collection using a single web method call.
                var multipleResponse = (ExecuteMultipleResponse)organizationService.Execute(multipleRequest);
            }

            return true;
        }

        public static void Delete(this IOrganizationService service, Entity entity)
        {
            service.Delete(entity.LogicalName, entity.Id);
        }
        
        /// <summary>
        /// Call this method for bulk delete
        /// </summary>
        /// <param name="service">Org Service</param>
        /// <param name="entityReferences">Collection of EntityReferences to Delete</param>
        public static void DeleteBatch(this IOrganizationService service, IEnumerable<EntityReference> entityReferences)
        {
            var pages = entityReferences.ToList()
                .ToPages(500);

            foreach (var page in pages)
            {
                // Create an ExecuteMultipleRequest object.
                var multipleRequest = new ExecuteMultipleRequest()
                {
                    // Assign settings that define execution behavior: continue on error, return responses. 
                    Settings = new ExecuteMultipleSettings()
                    {
                        ContinueOnError = false,
                        ReturnResponses = true
                    },
                    // Create an empty organization request collection.
                    Requests = new OrganizationRequestCollection()
                };

                // Add a DeleteRequest for each entity to the request collection.
                foreach (var entityRef in page.Value)
                {
                    var deleteRequest = new DeleteRequest { Target = entityRef };
                    multipleRequest.Requests.Add(deleteRequest);
                }

                // Execute all the requests in the request collection using a single web method call.
                var multipleResponse = (ExecuteMultipleResponse)service.Execute(multipleRequest);
            }
        }

        public static void DeleteBatch(this IOrganizationService service, string entityname, IEnumerable<Guid> ids)
        {
            service.DeleteBatch(ids.Select(x => new EntityReference(entityname, x)).ToList());
        }

        //public static OpStat ProcessAllEntities(this IOrganizationService service, IEnumerable<Entity> entities,
        //    Action<IEnumerable<Entity>> action)
        //{
        //    action(entities);
        //}

        public static bool DeleteBulkAsync(this IOrganizationService service, params QueryExpression[] set)
        {
            try
            {
                if (set == null || !set.Any())
                {
                    throw new ArgumentNullException("set");
                }

                // Create the bulk delete request.
                BulkDeleteRequest bulkDeleteRequest = new BulkDeleteRequest();

                // Set the request properties.
                bulkDeleteRequest.JobName = "Bulk Delete";

                // Querying activities
                bulkDeleteRequest.QuerySet = set;

                // Set the start time for the bulk delete.
                bulkDeleteRequest.StartDateTime = DateTime.Now;

                // Set the required recurrence pattern.
                bulkDeleteRequest.RecurrencePattern = String.Empty;

                var currentUserId = Guid.Empty;

                WhoAmIRequest request = new WhoAmIRequest();
                WhoAmIResponse response = (WhoAmIResponse)service.Execute(request);
                if (response != null)
                    currentUserId = response.UserId;

                // Set email activity properties.
                bulkDeleteRequest.SendEmailNotification = false;
                bulkDeleteRequest.ToRecipients = new[] { currentUserId };
                bulkDeleteRequest.CCRecipients = new Guid[] { };

                // Submit the bulk delete job.
                // NOTE: Because this is an asynchronous operation, the response will be immediate.
                var bulkDeleteResponse =
                    (BulkDeleteResponse)service.Execute(bulkDeleteRequest);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }



        public static OpStat ProcessAllEntities(
            this IOrganizationService service,
            QueryExpression query,
            Action<Entity> func)
        {
            var stat = new OpStat();

            var pagingInfo = new PagingInfo
            {
                Count = 5000,
                PageNumber = 1,
                PagingCookie = null,
                ReturnTotalRecordCount = true
            };

            query.PageInfo = pagingInfo;

            var count = 0;

            var res = true;

            while (true)
            {
                var result = service.RetrieveMultiple(query);

                count += result.Entities.Count;

                //res &= 

                foreach (var item in new ConcurrentBag<Entity>(result.Entities.ToList()))
                {
                    func(item);
                }

                if (result.MoreRecords)
                {
                    // retrieve next page
                    query.PageInfo.PageNumber++;

                    // set paging cookie
                    query.PageInfo.PagingCookie = result.PagingCookie;

                }
                else
                {
                    break;
                }
            }

            stat.IsSuccess = res;
            stat.ProcessedEntitiesCount = count;

            return stat;
        }

        public static OpStat ProcessAllEntities(this IOrganizationService service, QueryExpression query,
            Func<List<Entity>, bool> func)
        {
            var stat = new OpStat();

            var pagingInfo = new PagingInfo
            {
                Count = 5000,
                PageNumber = 1,
                PagingCookie = null,
                ReturnTotalRecordCount = true
            };

            query.PageInfo = pagingInfo;

            var count = 0;

            var res = true;

            while (true)
            {
                var result = service.RetrieveMultiple(query);

                count += result.Entities.Count;

                res &= func(result.Entities.ToList());

                if (result.MoreRecords)
                {
                    // retrieve next page
                    query.PageInfo.PageNumber++;

                    // set paging cookie
                    query.PageInfo.PagingCookie = result.PagingCookie;

                }
                else
                {
                    break;
                }
            }

            stat.IsSuccess = res;
            stat.ProcessedEntitiesCount = count;

            return stat;
        }

        public static List<Entity> GetPage(this IOrganizationService service, QueryExpression query)
        {
            var coll = new List<Entity>();

            if (query.PageInfo.Count > 5000)
            {
                throw new NotImplementedException("Paging");
            }

            var result = service.RetrieveMultiple(query);
            coll.AddRange(result.Entities);

            return coll;
        }

        public static List<Entity> GetPage(this IOrganizationService service, QueryExpression query, out int totalItemsCount)
        {
            var coll = new List<Entity>();

            if (query.PageInfo.Count > 5000)
            {
                throw new NotImplementedException("Paging");
            }

            var result = service.RetrieveMultiple(query);
            coll.AddRange(result.Entities);

            totalItemsCount = result.TotalRecordCount;

            return coll;
        }

        public static List<Entity> GetAllEntities(this IOrganizationService service, QueryExpression query)
        {
            var coll = new List<Entity>();

            if (query.PageInfo == null)
            {
                query.PageInfo = new PagingInfo
                {
                    Count = 5000,
                    PageNumber = 1,
                    PagingCookie = null,
                    ReturnTotalRecordCount = true
                };
            }

            while (true)
            {
                var result = service.RetrieveMultiple(query);
                coll.AddRange(result.Entities);

                if (result.MoreRecords)
                {
                    // retrieve next page
                    query.PageInfo.PageNumber++;
                    // set paging cookie
                    query.PageInfo.PagingCookie = result.PagingCookie;
                }
                else
                {
                    break;
                }
            }

            return coll;
        }



        #region Entity

        public static bool SetAttrvalue(this Entity entity, string attrName, object value)
        {
            if (entity.Id.Equals(Guid.Empty))
            {
                entity[attrName] = value;
                return true;
            }

            if (entity.Contains(attrName))
            {
                var myval = entity[attrName];

                if (myval != null && value == null)
                {
                    entity[attrName] = null;
                    return true;
                }

                if ((myval == null && value == null)
                    || (myval != null && myval.Equals(value))
                    || (myval is DateTime && IsEqualDates((DateTime)myval, (DateTime)value))
                    ||
                    (myval is EntityReference && value is EntityReference &&
                    (myval as EntityReference).Id == ((EntityReference)value).Id))
                {
                    entity.Attributes.Remove(attrName);
                    return false;
                }
            }
            else
            {
                // если нет значения, значит его нет и в црм!то есть он нулл
                if (value == null ||
                    ((value is Decimal) && (decimal)value == 0) ||
                    ((value is int) && (int)value == 0) ||
                    (value is DateTime && (DateTime)value == default(DateTime)) ||
                    string.IsNullOrEmpty(value.ToString()))
                {
                    return false;
                }
            }


            entity[attrName] = value;

            return true;
        }

        private static bool IsEqualDates(DateTime myval, DateTime value)
        {
            if (value == default(DateTime))
                return false;

            var local = ToLocalTime(myval);

            switch (value.Kind)
            {
                case DateTimeKind.Local:
                    return local == value;
                case DateTimeKind.Utc:
                    return myval == value;
                case DateTimeKind.Unspecified:
                    return myval.Date == value.Date;
            }

            return true;
        }

        private static DateTime ToLocalTime(DateTime utc)
        {
            if (utc.Kind != DateTimeKind.Utc)
                return utc;

            var loc = utc.ToLocalTime();
            return loc.Hour == 23 ? loc.AddHours(1) : loc;
        }

        public static void SetAttrvalue(this Entity entity, string attrName, object value, ref bool isUpdated)
        {
            isUpdated |= entity.SetAttrvalue(attrName, value);
        }

        public static void Remove(this Entity entity, string attrName)
        {
            if (!entity.Contains(attrName)) return;

            entity.Attributes.Remove(attrName);
        }


        public static Entity Clone(this Entity e, bool withAttrs = true)
        {
            var clone = new Entity(e.LogicalName) { Id = e.Id };

            if (!withAttrs) return clone;

            foreach (var a in e.Attributes)
                clone[a.Key] = a.Value;

            return clone;
        }

        public static Entity Clone(this Entity e, params string[] attrsToCopy)
        {
            var clone = new Entity(e.LogicalName) { Id = e.Id };

            foreach (var a in e.Attributes)
            {
                if (attrsToCopy.Contains(a.Key))
                {
                    clone[a.Key] = a.Value;
                }
            }

            return clone;
        }

        #endregion



        #region DateTime

        /// <summary>
        /// Retrieves the current users timezone code
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static int? RetrieveCurrentUsersSettings(this IOrganizationService service)
        {
            var currentUserSettings = service.FirstOrDefault(BaseQueryStack.UserSettings());

            //return time zone code
            return (int?)currentUserSettings.Attributes["timezonecode"];
        }

        /// <summary>
        /// Retrive the local time from the UTC time with IOrgService.
        /// </summary>
        public static DateTime RetrieveLocalTimeFromUtcTime(this IOrganizationService service, DateTime utcTime)
        {
            if (utcTime.Kind == DateTimeKind.Local)
                return utcTime; //throw new Exception("Вы хотели привести LocalTime to LocalTime");

            var retrieveCurrentUsersSettings = service.RetrieveCurrentUsersSettings();

            if (retrieveCurrentUsersSettings == null)
                throw new Exception("Не удалось определить настройки пользователя");

            var request = new LocalTimeFromUtcTimeRequest
            {
                TimeZoneCode = retrieveCurrentUsersSettings.Value,
                UtcTime = utcTime.ToUniversalTime()
            };

            var response = (LocalTimeFromUtcTimeResponse)service.Execute(request);

            var local = response.LocalTime;

            return local.Hour == 23 ? local.AddHours(1) : local;
        }

        /// <summary>
        /// Retrive the local time from the UTC time.
        /// </summary>
        public static DateTime RetrieveLocalTimeFromUtcTime2(this IOrganizationService service, DateTime utcTime)
        {
            if (utcTime.Kind == DateTimeKind.Local)
                return utcTime; //throw new Exception("Вы хотели привести LocalTime to LocalTime");

            var local = utcTime.ToLocalTime();

            return local.Hour == 23 ? local.AddHours(1) : local;
        }

        public static DateTime Locally(this DateTime? utcTime)
        {
            if (utcTime == null) return default(DateTime);

            return Locally(utcTime.Value);

            //var t = utcTime.Value;

            ////if (t.Kind == DateTimeKind.Local)
            ////    return t; //throw new Exception("Вы хотели привести LocalTime to LocalTime");

            //var local = t.ToLocalTime();

            //return local.Hour == 23 ? local.AddHours(1) : local;
        }

        public static DateTime Locally(this DateTime t)
        {
            if (t == default(DateTime)) return default(DateTime);


            //if (t.Kind == DateTimeKind.Local)
            //    return t; //throw new Exception("Вы хотели привести LocalTime to LocalTime");

            var local = t.ToLocalTime();

            return local.Hour == 23 ? local.AddHours(1) : local;
        }

        //public static DateTime Locally(this DateTime? date)
        //{
        //    return date == null ? default(DateTime) : RetrieveLocalTimeFromUtcTime2(date.Value);
        //}

        /// <summary>
        /// Retrive the UTC DateTime from Local Date time format.
        /// </summary>
        public static DateTime RetrieveUtcTimeFromLocalTime(IOrganizationService service, DateTime localTime)
        {
            if (localTime.Kind == DateTimeKind.Utc)
            {
                return localTime; // throw new Exception("Вы хотели привести UTC to UTC");
            }

            var retrieveCurrentUsersSettings = service.RetrieveCurrentUsersSettings();

            if (retrieveCurrentUsersSettings == null)
                throw new Exception("Не удалось определить настройки пользователя");

            var request = new UtcTimeFromLocalTimeRequest
            {
                TimeZoneCode = retrieveCurrentUsersSettings.Value,
                LocalTime = localTime
            };

            var response = (UtcTimeFromLocalTimeResponse)service.Execute(request);

            return response.UtcTime;
        }

        #endregion
    }
}
