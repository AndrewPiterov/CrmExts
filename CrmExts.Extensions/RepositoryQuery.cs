using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmExts.Extensions
{
    public class RepositoryQuery
    {
        private readonly IOrganizationService _service;
        private readonly string _entityname;
        private readonly List<ConditionExpression> _filter;
        private readonly List<string> _select;
        private readonly List<OrderBy> _orders;
        private bool _allattributes;

        public RepositoryQuery(IOrganizationService service, string entityname)
        {
            if (service == null) throw new ArgumentNullException("service");
            if (string.IsNullOrEmpty(entityname)) throw new ArgumentNullException("entityname");

            _service = service;
            _entityname = entityname;

            _filter = new List<ConditionExpression>();
            _select = new List<string>();
            _orders = new List<OrderBy>();
        }


        public RepositoryQuery Active(bool onlyActive = true)
        {
            _filter.Add(new ConditionExpression("statecode", ConditionOperator.Equal, 0));
            return this;
        }

        public RepositoryQuery Filter(ConditionExpression filter)
        {
            _filter.Add(filter);
            return this;
        }

        public RepositoryQuery Order(OrderBy order)
        {
            _orders.Add(order);
            return this;
        }

        public RepositoryQuery Select(bool allAttributes)
        {
            _allattributes = allAttributes;
            return this;
        }

        public RepositoryQuery Select(params string[] select)
        {
            foreach (var s in select)
                _select.Add(s);

            return this;
        }
        public Entity FirstOrDefault()
        {
            return _service.Get(_entityname, _allattributes, _orders, _filter, _select, 1, 1).FirstOrDefault();
        }

        public IEnumerable<Entity> Get()
        {
            return _service.Get(_entityname, _allattributes, _orders, _filter, _select);
        }

        public IEnumerable<Entity> GetPage(int page = 1, int count = 1000)
        {
            return _service.Get(_entityname, _allattributes, _orders, _filter, _select, page, count);
        }

        public IEnumerable<Entity> GetPage(out int totalCount, int page = 1, int count = 1000)
        {
            return _service.Get(out totalCount, _entityname, _allattributes, _orders, _filter, _select, page, count);
        }
    }


    public static class BaseQueryStack
    {
        public static QueryExpression UserSettings(params string[] attrs)
        {
            var q = new QueryExpression("usersettings")
            {
                Criteria = new FilterExpression
                {
                    Conditions =
                        {
                            new ConditionExpression("systemuserid", ConditionOperator.EqualUserId)
                        }
                }
            };

            if (attrs.Any())
            {
                q.ColumnSet = new ColumnSet(attrs);
            }
            else
            {
                q.ColumnSet = new ColumnSet(true);
            }

            return q;
        }

        public static QueryExpression CurrentUser
        {
            get
            {
                return new QueryExpression(EntityNames.Systemuser)
                {
                    ColumnSet = new ColumnSet(true),
                    Criteria = new FilterExpression
                    {
                        Conditions =
                        {
                            new ConditionExpression("systemuserid", ConditionOperator.EqualUserId)
                        }
                    }
                };

            }
        }

        public static QueryExpression GetSysParameter(bool selectAll = true)
        {
            return new QueryExpression
            {
                EntityName = "syst_parameters",
                ColumnSet = new ColumnSet(selectAll),
                Criteria =
                {
                    Conditions =
                        {
                            new ConditionExpression("statecode", ConditionOperator.Equal, 0)
                        }
                }
            };
        }

        public static QueryExpression GetSysParameter(params string[] select)
        {
            if (select == null)
            {
                throw new ArgumentNullException("select");
            }

            return new QueryExpression
            {
                EntityName = "syst_parameters",
                ColumnSet = select.Any() ? new ColumnSet(select) : new ColumnSet(true),
                Criteria =
                {
                    Conditions =
                        {
                            new ConditionExpression("statecode", ConditionOperator.Equal, 0)
                        }
                }
            };

        }

        public static QueryExpression GetLoanSchedulleRows(Guid loanId, params string[] attrs)
        {
            var q = new QueryExpression(EntityNames.SheduleRow)
            {
                ColumnSet = new ColumnSet(attrs),
                Criteria =
                {
                    Conditions =
                    {
                        new ConditionExpression("statecode", ConditionOperator.Equal, 0),
                        new ConditionExpression("ref_loan", ConditionOperator.Equal, loanId)
                    }
                }
            };

            return q;
        }
    }

    public static class ListExtensions
    {
        public static Dictionary<int, List<T>> ToPages<T>(this IEnumerable<T> list, int countPerPage)
        {
            var l = list.ToList();

            var pages = new Dictionary<int, List<T>>();

            bool has;
            var page = 1;

            do
            {
                var pageContent = GetPage(l, page, countPerPage);

                has = pageContent.Any();
                if (!has) continue;

                pages.Add(page, pageContent);
                page++;

            } while (has);

            return pages;
        }

        private static List<T> GetPage<T>(IEnumerable<T> list, int page, int perPage)
        {
            return list.Skip((page - 1) * perPage).Take(perPage).ToList();
        }
    }

    public static class EntityNames
    {
        public static string synchronization_log
        {
            get { return "synchronization_log"; }
        }


        public static string Account
        {
            get { return "account"; }
        }

        public static string Loan
        {
            get { return "loan"; }
        }

        public static string Insurance
        {
            get { return "insurance_contract"; }
        }

        public static string RecalcsSchedulle
        {
            get { return "recalc_schedule"; }
        }

        public static string Person
        {
            get { return "person"; }
        }

        public static string Phone
        {
            get { return "phone"; }
        }

        public static string Address
        {
            get { return "address"; }
        }

        public static string Document
        {
            get { return "document"; }
        }

        public static string SheduleRow
        {
            get { return "loan_schedule_row"; }
        }

        public static string FactPayment
        {
            get { return "fact_payment"; }
        }

        public static string EarlyPayment
        {
            get { return "early_payment"; }
        }

        public static string DelayDebt
        {
            get { return "delay_debt"; }
        }

        public static string InsuranceSchedulledString
        {
            get { return "insurance_schedule_string"; }
        }

        public static string DebetingFactPayment
        {
            get { return "debiting_fact_payment"; }
        }

        public static string SystemParam
        {
            get { return "syst_parameters"; }
        }

        public static string Sms { get { return "sent_sms"; } }
        public static string SchedulledTask { get { return "scheduled_task"; } }
        public static string Systemuser
        {
            get { return "systemuser"; }
        }
    }

}
