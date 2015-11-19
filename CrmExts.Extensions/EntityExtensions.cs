using CrmExts.Extensions.Models;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmExts.Extensions
{
    public static class EntityExtensions
    {
        public static TModel ToModel<TModel>(this Entity entity) where TModel : Model, new()
        {
            try
            {
                if (entity.LogicalName.Equals(EntityNames.Insurance))
                {
                    return new insurance_contract(entity) as TModel;
                }

                if (entity.LogicalName.Equals(EntityNames.Loan))
                {
                    return new loan(entity) as TModel;
                }

                // etc.

                throw new Exception(string.Format("Could not convert entity {0} to model", entity.LogicalName));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
