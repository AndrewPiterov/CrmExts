using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmExts.Extensions.Models
{
    /// <summary>
    /// Наименования договора contract -> insurance_type 
    /// </summary>
    public enum InsuranceType
    {
        Undefened = -1,

        /// <summary>
        /// Договор страхования (личное и имущественное страхование)
        /// </summary>
        DcLichnoeImushestvennoe = 686340000,

        /// <summary>
        /// Договор страхования (личное страхование)
        /// </summary>
        DcLichnoe = 686340001,

        /// <summary>
        /// Договор страхования (имущественное страхование)
        /// </summary>
        DcImushestvennoe = 686340002,

        /// <summary>
        /// Договор страхования ответственности заемщика (срок связан с достижением ООД (по плану) размера Порогового значения)
        /// </summary>
        DcOtvetstvennostiZaemshikaOOD = 686340003,

        /// <summary>
        /// Договор страхования ответственности заемщика (срок страхования равен сроку действия кредитного договора)
        /// </summary>
        DcOtvetstvennostiZaemshikaEqualKd = 686340004,
        /// <summary>
        /// Договор титульного страхования
        /// </summary>
        DogovorTitulnogoStrahovanya = 686340005,

        /// <summary>
        /// страхование имущества: квартиры
        /// </summary>
        ImuschesctvaKvartir = 686340006,

        /// <summary>
        /// страхование имущества: жилого дома с земельным участком
        /// </summary>
        HouseWithGarden = 686340007,

        /// <summary>
        /// страхование от несчастных случаев и болезней и страхование имущества: квартиры
        /// </summary>
        AccidentAndKvartira = 686340008,

        /// <summary>
        /// страхование от несчастных случаев и болезней и страхование имущества: жилого дома с земельным участком
        /// </summary>
        AccidentAndHouse = 686340009,
    }

    /// <summary>
    /// Тип договора
    /// </summary>
    public enum Contract_Type
    {
        /// <summary>
        /// неопределен
        /// </summary>
        Undefened = 0,

        /// <summary>
        /// Допсоглашение
        /// </summary>
        Dopnik = 686340001,

        /// <summary>
        /// Длговор
        /// </summary>
        Contract = 686340000
    }

    /// <summary>
    /// Статус закладной
    /// </summary>
    public enum LoanStatus
    {

        Undefened = 0,

        /// <summary>
        /// Активная 
        /// </summary>
        Active = 686340000,

        /// <summary>
        /// Дефолт
        /// </summary>
        Default = 686340001
    }

    /// <summary>
    /// Признак дефолта
    /// </summary>
    public enum LoanSign
    {
        Undefened = 0,

        /// <summary>
        /// АННДЕФ
        /// </summary>
        AnnDef = 686340000,

        /// <summary>
        /// СТРАХДЕФ
        /// </summary>
        StrahDef = 686340001,

        /// <summary>
        /// АННДЕФ/СТРАХДЕФ
        /// </summary>
        AnnDefStrahDef = 686340002,

        /// <summary>
        /// СТРАХДЕФ/АННДЕФ
        /// </summary>
        StrahDefAnnDef = 686340003,
    }

    /// <summary>
    /// Вид договора
    /// </summary>
    public enum ContractType
    {
        /// <summary>
        /// неопределен
        /// </summary>
        Undefened = 0,

        /// <summary>
        /// Кредитный договор
        /// </summary>
        CreditContract = 1,

        /// <summary>
        /// Договор займа
        /// </summary>
        ZaimContract = 2
    }

    /// <summary>
    /// Валюта кредита
    /// </summary>
    public enum Valuta
    {
        /// <summary>
        /// Не определен
        /// </summary>
        Undefened = 0,

        /// <summary>
        /// Рубли
        /// </summary>
        RUR = 1
    }


    /// <summary>
    /// Тип предмета ипотеки
    /// </summary>
    public enum MortagesType
    {
        /// <summary>
        /// квартира
        /// </summary>
        Flat = 0,

        /// <summary>
        /// дом
        /// </summary>
        House,

        /// <summary>
        /// таунхаус
        /// </summary>
        Tanhouse,

        /// <summary>
        /// земельный участок
        /// </summary>
        Land,

        /// <summary>
        /// Иное
        /// </summary>
        Other,

        /// <summary>
        /// комната
        /// </summary>
        Room,

        /// <summary>
        /// право аренды земельного участка
        /// </summary>
        PravoArendaZemUchastka,

        /// <summary>
        /// нежилое помещение
        /// </summary>
        Nonlive,

        Undefened
    }

    public enum StatusAccounting
    {

        Undefened = 0,

        /// <summary>
        /// Активная
        /// </summary>
        Active = 686340000,

        /// <summary>
        /// проданная
        /// </summary>
        Solded = 686340001,

        /// <summary>
        /// проданная/погашенная
        /// </summary>
        SoldedCanceled = 686340002,

        /// <summary>
        /// погашенная
        /// </summary>
        Canceled = 686340003
    }

    /// <summary>
    /// Цель кредита
    /// </summary>
    public enum CreditGoal
    {
        /// <summary>
        /// приобретение жилья
        /// </summary>
        PriobretenieZiliya = 0,

        /// <summary>
        /// долевое участие в строительстве
        /// </summary>
        DolevoeUchastieVStoyke,

        /// <summary>
        /// погашение ранее взятого кредита
        /// </summary>
        PogasheniePrevCredita,

        /// <summary>
        /// ремонт или иное неотделимое улучшение жилого помещения
        /// </summary>
        RemontOrOtherUluchshenieZilya,

        /// <summary>
        /// нецелевой
        /// </summary>
        NonCelevoy,

        /// <summary>
        /// приобретение нежилого помещения
        /// </summary>
        KupitNeziloePomeschenie,
        /// <summary>
        /// Иное
        /// </summary>
        Other,
        Undefened,

    }

    /// <summary>
    /// Вид субсидии
    /// </summary>
    public enum SubsidyType
    {
        /// <summary>
        /// Нет
        /// </summary>
        Non = 0,

        /// <summary>
        /// единовременная
        /// </summary>
        Edinovremennya,
        /// <summary>
        /// компенсация части процентной ставки
        /// </summary>
        CompensatciaChastiProcStavki,
        /// <summary>
        /// компенсация аннуитета
        /// </summary>
        CompensatciaAnnyieta,
        /// <summary>
        /// иное
        /// </summary>
        Other,
        Undefened,
    }


    /// <summary>
    /// тип процентной ставки
    /// </summary>
    public enum ProcStavkiType
    {
        /// <summary>
        /// Фиксированнная
        /// </summary>
        Fix = 0,
        /// <summary>
        /// Плавающая
        /// </summary>
        Fluent,
        Undefened
    }

    /// <summary>
    /// Период пересмотра ставки period_revision
    /// </summary>
    public enum PeriodRevisionType
    {
        EveryMonth = 1,
        EveryQuater,
        OnePerHalfYear,
        EveryYear,
        Undefened
    }

    /// <summary>
    /// Подтверждение доходов proof_income
    /// </summary>
    public enum ProofIncomeType
    {
        Offical = 0,
        NonOffical,
        Both,
        Undefened,
    }

    /// <summary>
    /// Тип использования приобретенной собственности  use_property
    /// </summary>
    public enum PropertyUseType
    {
        Live = 0,
        Rent,
        Other,
        Undefened
    }

    /// <summary>
    /// Наличие родственных связей kinship
    /// </summary>
    public enum KinshipType
    {
        Related = 0,
        NonRelated,
        OnlyOne,
        Undefened
    }

    /// <summary>
    /// Характеристика кредитной истории (при условии проверки в БКИ) characteristic_credit_history
    /// </summary>
    public enum CreditHistoryType
    {
        Undefened = 0,
        Positive,
        Negative,
    }

    /// <summary>
    /// Наименование продукта по стандартам АИЖК product_name_aigk
    /// </summary>
    public enum AigkProductName
    {
        NotStandart = 0,
        Standart,
        Newstroyka,
        FamilyCapital,
        VarStavka,
        NewbeScientist,
        Pereezd,
        ArendnoeZilue,
        VoennayaIpiteka,
        Other,
        Undefened
    }

    /// <summary>
    /// Модификация ипотечного кредита mortgage_loan_modification: 
    /// </summary>
    public enum MortageModification
    {
        StandartProduct = 1,
        MaterinskiyCapital,
        Undefened
    }

    /// <summary>
    /// для Округление аннуитета и для Округление ОД и % 
    /// </summary>
    public enum RoundingType
    {
        ToRubles = 0,
        ToKopeek,
        Undefened
    }

    /// <summary>
    /// Тип платежа
    /// </summary>
    public enum PaymentType
    {
        Undefened = 0,
        Annyitet = 1,
        Differencirovany = 2,
        Rastuschi = 3,
        Inoe = 4,
    }

    /// <summary>
    /// Периодичность платежа
    /// </summary>
    public enum PeriodPaymentType
    {
        undefened = 0,
        EveryMonth = 1,
        EveryQuoter = 2,
        Inoe = 3,
    }



    /// <summary>
    /// Вид деятельности заемщика
    /// </summary>
    public enum ActivityType
    {
        Undefened = -1,

        /// <summary>
        /// Гос служащий
        /// </summary>
        GosSlygashii = 0,

        /// <summary>
        /// наемный работник
        /// </summary>
        Nauemnik = 1,

        /// <summary>
        /// частный предприниматель
        /// </summary>
        ChastniyPredprinimatel = 2,

        /// <summary>
        /// пенсионер
        /// </summary>
        Pensioner = 3,

        /// <summary>
        /// военнослужащий
        /// </summary>
        Voenniy = 4,

        /// <summary>
        /// учащийся
        /// </summary>
        Uchashiysya = 5,

        /// <summary>
        /// отсутствует
        /// </summary>
        Nothing = 6,

    }

    /// <summary>
    /// Семейное положение 
    /// </summary>
    public enum MuritalStatus
    {
        Undefened,

        /// <summary>
        /// холост/не замужем/разведен(-а),
        /// </summary>
        Single = 0,

        /// <summary>
        /// женат/замужем
        /// </summary>
        Married = 1
    }

    /// <summary>
    /// Тип занятости заемщика
    /// </summary>
    public enum EmploymentType
    {
        Undefened = -1,

        /// <summary>
        /// 
        /// </summary>
        Full = 0,

        /// <summary>
        /// 
        /// </summary>
        Part = 1,

    }

    /// <summary>
    /// debiting_fact_payment.type
    /// </summary>
    public enum DebitingPaymentType
    {
        Undefened = 0,
        PogashenieOD = 686340000,
        DosrochnoePogashenieOD = 686340001,
        PogashenieprosrochiPoOD = 686340002,
        PogashenieProcentovZaPolzovanieCreditom = 686340003,
        PogashenieprosrochiPoProcentam = 686340004,
        PogasheniePeniPoOD = 686340005,
        PogashenieDonachislennihProcentov = 686340006,
        PogasheniePeniPoProcentam = 686340007,

    }

    /// <summary>
    /// Вид перерасчета
    /// </summary>
    public enum RecalcKind
    {
        Undefened = 0,

        /// <summary>
        /// Досрочное гашение 
        /// </summary>
        Dosrochnoe = 686340000,

        /// <summary>
        /// Полное гашение
        /// </summary>
        Full = 686340001,

        /// <summary>
        /// Аннуитет
        /// </summary>
        Annyitet = 686340002,
    }

    /// <summary>
    /// Тип перерасчета
    /// </summary>
    public enum RecalcType
    {
        Undefened = 0,

        /// <summary>
        /// Сокращение срока кредита 
        /// </summary>
        SokrSrokCredita = 686340000,

        /// <summary>
        /// Сокращение ежемесячного платежа
        /// </summary>
        SokrEveryMonthPayment = 686340001,

    }

    /// <summary>
    ///  "Вид просрочки" (delay_type)
    /// </summary>
    public enum DelayDebtType
    {
        Undefened = 0,

        /// <summary>
        /// Просрочка аннуитета
        /// </summary>
        Annyitet = 686340000,

        /// <summary>
        /// Просрочка пени
        /// </summary>
        Peny = 686340001

    }

    public enum PhoneCategory
    {
        Undefened = 0,

        Home = 686340000,

        MobileForSms = 686340001,

        MobileIndividual = 686340002,

        /// <summary>
        /// Сотовый телефон - Личный/для SMS
        /// </summary>
        MobileIndividualForSms = 686340003,

        MobileWork = 686340004,

        Work = 686340005,
    }

    /// <summary>
    /// OptionSet type для карточки synchronization_log 
    /// </summary>
    public enum SyncType
    {
        undefened = 0,

        Dogovor = 686340000,
        /// <summary>
        /// ZakritayaProsrochka
        /// </summary>
        ClosedAnnuitet = 686340001,

        /// <summary>
        /// Prosrochka
        /// </summary>
        Annuitet = 686340002,

        /// <summary>
        /// DogovorStrahovaniya
        /// </summary>
        Insurance = 686340003,

        /// <summary>
        /// Pereraschet
        /// </summary>
        Recalcs = 686340004,

        /// <summary>
        /// Platez
        /// </summary>
        Payments = 686340005,

        /// <summary>
        /// Grafik
        /// </summary>
        Grafik = 686340006,

        /// <summary>
        /// Prosrochka1
        /// </summary>
        Peny = 686340007,

        /// <summary>
        /// ZakritayaProsrochka1
        /// </summary>
        ClosedPeny = 686340008
    }

    /// <summary>
    /// Статус выполнения" (state) - "Успех" (686 340 000) или "Ошибка" (686 340 001) в зависимости от исхода.
    /// </summary>
    public enum SyncState
    {
        undefened = 0,
        Success = 686340000,
        Error = 686340001
    }




    ///// <summary>
    ///// Вид деятельности заемщика
    ///// </summary>
    //public enum ActivityType
    //{
    //    Undefened,

    //    /// <summary>
    //    /// Гос служащий
    //    /// </summary>
    //    GosSluga = 0,

    //    /// <summary>
    //    /// Наемный 
    //    /// </summary>
    //    Naemnik,

    //    /// <summary>
    //    /// Частный предприниматель
    //    /// </summary>
    //    Chastniypredprinimatel,

    //    /// <summary>
    //    /// Пенсионер
    //    /// </summary>
    //    Pensioner,

    //    /// <summary>
    //    /// Военнослужащий
    //    /// </summary>
    //    Voennyi,

    //    /// <summary>
    //    /// Учащийся
    //    /// </summary>
    //    Student,

    //    /// <summary>
    //    /// Отсутствует
    //    /// </summary>
    //    Non,
    //}

}
