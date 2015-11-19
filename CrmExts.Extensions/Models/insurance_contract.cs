using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmExts.Extensions.Models
{
    public class insurance_contract : Model
    {
        public insurance_contract()
            : base()
        {

        }

        public insurance_contract(Entity entity)
            : base(entity)
        {

        }

        public Guid? ref_parent_insurance_agreement
        {
            get
            {
                if (!Entity.Contains("ref_parent_insurance_agreement"))
                {
                    return null;
                }

                return Entity.GetAttributeValue<EntityReference>("ref_parent_insurance_agreement").Id;
            }
            set
            {
                if (!value.HasValue)
                {
                    return;
                }

                Entity.SetAttrvalue("ref_parent_insurance_agreement", new EntityReference(EntityNames.Insurance, value.Value));
            }
        }

        public Guid? ref_loan
        {
            get
            {
                if (!Entity.Contains("ref_loan"))
                {
                    return null;
                }

                return Entity.GetAttributeValue<EntityReference>("ref_loan").Id;
            }
            set
            {
                if (!value.HasValue)
                {
                    return;
                }

                Entity.SetAttrvalue("ref_loan", new EntityReference(EntityNames.Loan, value.Value));
            }
        }

        public override string LogicalName
        {
            get { return EntityNames.Insurance; }
        }


        public string contract_number
        {
            get
            {
                return Entity.Contains("contract_number") ? Entity.GetAttributeValue<string>("contract_number") : null;
            }
            set { Entity.SetAttrvalue("contract_number", value); }
        }



        /// <summary>
        /// Сумма страхового возмещения (имущество)
        /// </summary>
        public decimal? amount_insurance_compensation
        {
            get
            {
                return Entity.Contains("amount_insurance_compensation") ? Entity.GetAttributeValue<decimal>("amount_insurance_compensation") : (decimal?)null;
            }
            set { Entity.SetAttrvalue("amount_insurance_compensation", value); }
        }

        /// <summary>
        /// Просрочка по договору страхования (имущество) по оплате взноса
        /// </summary>
        public int? delay_number_days
        {
            get
            {
                return Entity.Contains("delay_number_days") ? Entity.GetAttributeValue<int>("delay_number_days") : (int?)null;
            }
            set { Entity.SetAttrvalue("delay_number_days", value); }
        }

        /// <summary>
        /// Рассрочка по страховым взносам (имущество)
        /// </summary>
        public bool? installment
        {
            get
            {
                return Entity.Contains("installment") ? Entity.GetAttributeValue<bool>("installment") : (bool?)null;
            }
            set { Entity.SetAttrvalue("installment", value); }
        }

        /// <summary>
        ///  Ссылка на карточку Организация (account). Поиск по полю name. ref_insurance_company
        /// </summary>
        public Guid? ref_insurance_company
        {
            get
            {
                if (!Entity.Contains("ref_insurance_company"))
                {
                    return null;
                }

                return Entity.GetAttributeValue<EntityReference>("ref_insurance_company").Id;
            }
            set
            {
                if (!value.HasValue)
                {
                    return;
                }

                Entity.SetAttrvalue("ref_insurance_company", new EntityReference(EntityNames.Loan, value.Value));
            }
        }

        /// <summary>
        /// Наименование договора
        /// </summary>
        public InsuranceType insurance_type
        {
            get
            {
                return Entity.Contains("insurance_type") ? (InsuranceType)Entity.GetAttributeValue<OptionSetValue>("insurance_type").Value : InsuranceType.Undefened;
            }
            set
            {
                if (value == InsuranceType.Undefened)
                {
                    return;
                }

                Entity.SetAttrvalue("insurance_type", new OptionSetValue((int)value));
            }
        }

        //public override string LogicalName()
        //{
        //    return "insurance_contract";
        //}

        //private Loan _loan;
        //public Loan Loan
        //{
        //    get { return _loan; }
        //    set
        //    {
        //        _loan = value;

        //        _loan.AddInsuranceContract(this);
        //    }
        //}

        // public string LoanNumber { get; set; }

        /// <summary>
        /// Дата заключения договора страхования (имущество)
        /// </summary>
        public DateTime? signing_date
        {
            get
            {
                if (!Entity.Contains("signing_date"))
                    return null;

                return Entity.GetAttributeValue<DateTime>("signing_date");
            }
            set { Entity.SetAttrvalue("signing_date", value); }
        }

        //private DateTime? signing_date_f;
        /// <summary>
        /// Начало действия договора страхования (имущество)
        /// </summary>
        public DateTime? effective_date
        {
            get
            {
                if (!Entity.Contains("effective_date"))
                    return null;

                return Entity.GetAttributeValue<DateTime>("effective_date");
            }
            set { Entity.SetAttrvalue("effective_date", value); }
        }

        //private DateTime? effective_date_f;
        /// <summary>
        /// Окончание действия договора страхования (имущество)
        /// </summary>
        public DateTime? expiration_date
        {
            get
            {
                if (!Entity.Contains("expiration_date"))
                    return null;

                return Entity.GetAttributeValue<DateTime>("expiration_date");
            }
            set { Entity.SetAttrvalue("expiration_date", value); }
        }

        public DateTime? last_payment_date
        {
            get
            {
                if (!Entity.Contains("last_payment_date"))
                    return null;

                return Entity.GetAttributeValue<DateTime>("last_payment_date");
            }
            set { Entity.SetAttrvalue("last_payment_date", value); }
        }




    }
}
