using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmExts.Extensions.Models
{
    public class loan : Model
    {

        public loan()
        {

        }

        public loan(Entity entity) : base(entity)
        {

        }

        public DateTime? repayment_loan_date
        {
            get
            {
                if (!Entity.Contains("repayment_loan_date"))
                {
                    return null;
                }

                return Entity.GetAttributeValue<DateTime>("repayment_loan_date");
            }
            set { Entity.SetAttrvalue("repayment_loan_date", value); }
        }

        public override string LogicalName
        {
            get { return EntityNames.Loan; }
        }
    }
}
