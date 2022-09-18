using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EaglePortal.Models
{
    public class Location
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("displayid")]
        public string DisplayId { get; set; }
        [JsonProperty("customerid")]
        public string CustomerId { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("suite")]
        public string Suite { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("zipcode")]
        public string ZipCode { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("fax")]
        public string Fax { get; set; }
        [JsonProperty("emailaddress")]
        public string Emailaddress { get; set; }
        [JsonProperty("locationtype")]
        public string Locationtype { get; set; }
        [JsonProperty("locationtypeother")]
        public string Locationtypeother { get; set; }
        [JsonProperty("contactname")]
        public string Contactname { get; set; }
        [JsonProperty("bankaccountfordeposit")]
        public string BankAccountfordeposit { get; set; }
        [JsonProperty("bankaccountforwithdrawls")]
        public string BankAccountforwithdrawls { get; set; }
        [JsonProperty("bankname")]
        public string Bankname { get; set; }
        [JsonProperty("bankname2")]
        public string Bankname2 { get; set; }
        [JsonProperty("bankaddress")]
        public string Bankaddress { get; set; }
        [JsonProperty("bankcity")]
        public string Bankcity { get; set; }
        [JsonProperty("Bankcity2")]
        public string Bankcity2 { get; set; }
        [JsonProperty("bankstate")]
        public string Bankstate { get; set; }
        [JsonProperty("bankstate2")]
        public string Bankstate2 { get; set; }
        [JsonProperty("bankzipcode")]
        public string Bankzipcode { get; set; }
        [JsonProperty("bankzipcode2")]
        public string Bankzipcode2 { get; set; }
        [JsonProperty("bankcontactname")]
        public string Bankcontactname { get; set; }
        [JsonProperty("bankcontactname2")]
        public string Bankcontactname2 { get; set; }
        [JsonProperty("bankphone")]
        public string Bankphone { get; set; }
        [JsonProperty("bankphone2")]
        public string Bankphone2 { get; set; }
        [JsonProperty("bankfax")]
        public string Bankfax { get; set; }
        [JsonProperty("bankfax2")]
        public string Bankfax2 { get; set; }
        [JsonProperty("bankrouting")]
        public string Bankrouting { get; set; }
        [JsonProperty("bankrouting2")]
        public string Bankrouting2 { get; set; }
        [JsonProperty("bankaccount")]
        public string Bankaccount { get; set; }
        [JsonProperty("bankaccount2")]
        public string Bankaccount2 { get; set; }
        [JsonProperty("bankauthorizedsignor")]
        public string Bankauthorizedsignor { get; set; }
        [JsonProperty("bankauthorizedsignor2")]
        public string Bankauthorizedsignor2 { get; set; }
        [JsonProperty("maillocation")]
        public string Maillocation { get; set; }
        [JsonProperty("needterminal")]
        public string Needterminal { get; set; }
        [JsonProperty("terminalshippinglocation")]
        public string Terminalshippinglocation { get; set; }
        [JsonProperty("supplyingownterminal")]
        public string Supplyingownterminal { get; set; }
        [JsonProperty("terminalmanufacturer")]
        public string Terminalmanufacturer { get; set; }
        [JsonProperty("usepos")]
        public string Usepos { get; set; }
        [JsonProperty("posmanufacturer")]
        public string Posmanufacturer { get; set; }
        [JsonProperty("posmodel")]
        public string PosModel { get; set; }
        [JsonProperty("usecheckimager")]
        public string UseCheckImager { get; set; }
        [JsonProperty("checkimagermanufacturer")]
        public string CheckImagermanufacturer { get; set; }
        [JsonProperty("checkimagermodel")]
        public string CheckImagermodel { get; set; }
        [JsonProperty("amexmerchantid")]
        public string AmexMerchantId { get; set; }
        [JsonProperty("discovermerchantid")]
        public string DiscoverMerchantId { get; set; }
        [JsonProperty("processdiscover")]
        public string ProcessDiscover { get; set; }
        [JsonProperty("acceptdiscover")]
        public string AcceptDiscover { get; set; }
        [JsonProperty("percentswipe")]
        public string PercentSwipe { get; set; }
        [JsonProperty("percentkey")]
        public string PercentKey { get; set; }
        [JsonProperty("visamcmerchantid")]
        public string VisamcMerchantId { get; set; }
        [JsonProperty("pinpadmodel")]
        public string PinpadModel { get; set; }
        [JsonProperty("AcceptAmex")]
        public string acceptamex { get; set; }
        [JsonProperty("RequestedAmount")]
        public string requestedamount { get; set; }
        [JsonProperty("RequestedMinimum")]
        public string requestedminimum { get; set; }
        [JsonProperty("Intendeduse")]
        public string intendeduse { get; set; }
        [JsonProperty("Previousadvance")]
        public string Previousadvance { get; set; }
        [JsonProperty("Previouslender")]
        public string Previouslender { get; set; }
        [JsonProperty("previousactive")]
        public string Previousactive { get; set; }
        [JsonProperty("previousbalance")]
        public string Previousbalance { get; set; }
        [JsonProperty("ownershiptype")]
        public string Ownershiptype { get; set; }
        [JsonProperty("businessformationstate")]
        public string Businessformationstate { get; set; }
        [JsonProperty("visamcaccepted")]
        public string Visamcaccepted { get; set; }
        [JsonProperty("visamcprovider")]
        public string Visamcprovider { get; set; }
        [JsonProperty("sellingtype")]
        public string Sellingtype { get; set; }
        [JsonProperty("sellproductarea")]
        public string Sellproductarea { get; set; }
        [JsonProperty("goodservicetype")]
        public string Goodservicetype { get; set; }
        [JsonProperty("mcccode")]
        public string Mcccode { get; set; }
        [JsonProperty("averagesale")]
        public string Averagesale { get; set; }
        [JsonProperty("averagemcmonth")]
        public string Averagemcmonth { get; set; }
        [JsonProperty("averagemctkt")]
        public string Averagemctkt { get; set; }
        [JsonProperty("averagediscmonth")]
        public string Averagediscmonth { get; set; }
        [JsonProperty("averagedisctkt")]
        public string Averagedisctkt { get; set; }
        [JsonProperty("averagediscovermonth")]
        public string Averagediscovermonth { get; set; }
        [JsonProperty("averageamexmonth")]
        public string Averageamexmonth { get; set; }
        [JsonProperty("averagegrossmonth")]
        public string Averagegrossmonth { get; set; }
        [JsonProperty("visamcdiscmonthlyvolume")]
        public string Visamcdiscmonthlyvolume { get; set; }
        [JsonProperty("visamcdiscmaxmonthlyvolume")]
        public string Visamcdiscmaxmonthlyvolume { get; set; }
        [JsonProperty("visamcdiscminavgticket")]
        public string Visamcdiscminavgticket { get; set; }
        [JsonProperty("visamcdiscmaxavgticket")]
        public string Visamcdiscmaxavgticket { get; set; }
        [JsonProperty("visamcdiscannualvolume")]
        public string Visamcdiscannualvolume { get; set; }
        [JsonProperty("poscontactname")]
        public string Poscontactname { get; set; }
        [JsonProperty("poscontactnumber")]
        public string Poscontactnumber { get; set; }
        [JsonProperty("highestsale")]
        public string Highestsale { get; set; }
        [JsonProperty("creditvolume")]
        public string Creditvolume { get; set; }
        [JsonProperty("numberlocations")]
        public string Numberlocations { get; set; }
        [JsonProperty("dateopened")]
        public string Dateopened { get; set; }
        [JsonProperty("seasonal")]
        public string Seasonal { get; set; }
        [JsonProperty("rentcurrent")]
        public string Rentcurrent { get; set; }
        [JsonProperty("landlordname")]
        public string Landlordname { get; set; }
        [JsonProperty("landlordcontactname")]
        public string Landlordcontactname { get; set; }
        [JsonProperty("landlordphone")]
        public string Landlordphone { get; set; }
        [JsonProperty("lengthofownershipyear")]
        public string Lengthofownershipyear { get; set; }
        [JsonProperty("lengthofownershipmonth")]
        public string Lengthofownershipmonth { get; set; }
        [JsonProperty("seasonalmonths")]
        public string Seasonalmonths { get; set; }
        [JsonProperty("leaseexpirationdate")]
        public string Leaseexpirationdate { get; set; }
        [JsonProperty("monthlyrent")]
        public string Monthlyrent { get; set; }
        [JsonProperty("acceptinglength")]
        public string Acceptinglength { get; set; }
        [JsonProperty("currentprocessor")]
        public string Currentprocessor { get; set; }
        [JsonProperty("batchesmonthly")]
        public string Batchesmonthly { get; set; }
        [JsonProperty("isdefault")]
        public string Isdefault { get; set; }
        [JsonProperty("merchantid")]
        public string Merchantid { get; set; }
        [JsonProperty("check21plusmerchantid")]
        public string Check21plusmerchantid { get; set; }
        [JsonProperty("check21plusterminalid")]
        public string Check21plusterminalid { get; set; }
        [JsonProperty("achdebitmerchantid")]
        public string Achdebitmerchantid { get; set; }
        [JsonProperty("achdebitterminalid")]
        public string Achdebitterminalid { get; set; }
        [JsonProperty("checksbyphonemerchantid")]
        public string Checksbyphonemerchantid { get; set; }
        [JsonProperty("checksbyphoneterminalid")]
        public string Checksbyphoneterminalid { get; set; }
        [JsonProperty("checksbywebmerchantid")]
        public string Checksbywebmerchantid { get; set; }
        [JsonProperty("checksbywebterminalid")]
        public string Checksbywebterminalid { get; set; }
        [JsonProperty("checkcollectmerchantid")]
        public string Checkcollectmerchantid { get; set; }
        [JsonProperty("checkcollectterminalid")]
        public string Checkcollectterminalid { get; set; }
        [JsonProperty("ckpapermerchantid")]
        public string Ckpapermerchantid { get; set; }
        [JsonProperty("ckpaperterminalid")]
        public string Ckpaperterminalid { get; set; }
        [JsonProperty("ckposmerchantid")]
        public string Ckposmerchantid { get; set; }
        [JsonProperty("ckposterminalid")]
        public string Ckposterminalid { get; set; }
        [JsonProperty("voidflag")]
        public string Voidflag { get; set; }
        [JsonProperty("viewed")]
        public string viewed { get; set; }
        [JsonProperty("created_at")]
        public string created_at { get; set; }
        [JsonProperty("updated_at")]
        public string updated_at { get; set; }
        [JsonProperty("terminalmodel")]
        public string terminalmodel { get; set; }
        [JsonProperty("estimatedannualvolume")]
        public string estimatedannualvolume { get; set; }
        [JsonProperty("estimatedavgticket")]
        public string estimatedavgticket { get; set; }
        [JsonProperty("hasprinter")]
        public string hasprinter { get; set; }
        [JsonProperty("terminalmanufacturerother")]
        public string terminalmanufacturerother { get; set; }
        [JsonProperty("haspinpad")]
        public string haspinpad { get; set; }
        [JsonProperty("dialoutphone")]
        public string dialoutphone { get; set; }
        [JsonProperty("dialoutphoneother")]
        public string dialoutphoneother { get; set; }
        [JsonProperty("printer")]
        public string printer { get; set; }
        [JsonProperty("timeremaininglease")]
        public string timeremaininglease { get; set; }
        [JsonProperty("timeremainingleasemonth")]
        public string timeremainingleasemonth { get; set; }
        [JsonProperty("previousprocessorterminatedwhom")]
        public string previousprocessorterminatedwhom { get; set; }
        [JsonProperty("previousprocessorterminated")]
        public string previousprocessorterminated { get; set; }
        [JsonProperty("previousprocessorterminatedexplain")]
        public string previousprocessorterminatedexplain { get; set; }
        [JsonProperty("previousprocessorterminateddate")]
        public string previousprocessorterminateddate { get; set; }
        [JsonProperty("checkreader")]
        public string checkreader { get; set; }
        [JsonProperty("checkreadermodel")]
        public string checkreadermodel { get; set; }
        [JsonProperty("bankaccounttype")]
        public string bankaccounttype { get; set; }
        [JsonProperty("bankaccounttype2")]
        public string bankaccounttype2 { get; set; }
        [JsonProperty("isbusinessforsale")]
        public string isbusinessforsale { get; set; }
        [JsonProperty("havetaxliens")]
        public string havetaxliens { get; set; }
        [JsonProperty("merchanttype")]
        public string merchanttype { get; set; }
        [JsonProperty("merchanttypeother")]
        public string merchanttypeother { get; set; }
        [JsonProperty("b2bpercent")]
        public string b2bpercent { get; set; }
        [JsonProperty("b2cpercent")]
        public string b2cpercent { get; set; }
        [JsonProperty("daysuntildelivery_0_pct")]
        public string daysuntildelivery_0_pct { get; set; }
        [JsonProperty("daysuntildelivery_1to3_pct")]
        public string daysuntildelivery_1to3_pct { get; set; }
        [JsonProperty("daysuntildelivery_4to7_pct")]
        public string daysuntildelivery_4to7_pct { get; set; }
        [JsonProperty("daysuntildelivery_8to14_pct")]
        public string daysuntildelivery_8to14_pct { get; set; }
        [JsonProperty("daysuntildelivery_30plus_pct")]
        public string daysuntildelivery_30plus_pct { get; set; }
        [JsonProperty("daysuntildelivery_total_pct")]
        public string daysuntildelivery_total_pct { get; set; }
        [JsonProperty("returnpolicyother")]
        public string returnpolicyother { get; set; }
        [JsonProperty("returnpolicy")]
        public string returnpolicy { get; set; }
        [JsonProperty("mktingmethods")]
        public string mktingmethods { get; set; }
        [JsonProperty("haveretaillocation")]
        public string haveretaillocation { get; set; }
        [JsonProperty("retailswipedpct")]
        public string retailswipedpct { get; set; }
        [JsonProperty("retailkeyedpct")]
        public string retailkeyedpct { get; set; }
        [JsonProperty("internetpct")]
        public string internetpct { get; set; }
        [JsonProperty("mailorderpct")]
        public string mailorderpct { get; set; }
        [JsonProperty("totalpct")]
        public string totalpct { get; set; }
        [JsonProperty("settlementon")]
        public string settlementon { get; set; }
        [JsonProperty("settlementonother")]
        public string settlementonother { get; set; }
        [JsonProperty("recurringcharge")]
        public string recurringcharge { get; set; }
        [JsonProperty("recurringchargefrequency")]
        public string recurringchargefrequency { get; set; }
        [JsonProperty("recurringchargeother")]
        public string recurringchargeother { get; set; }
        [JsonProperty("previosyearchargeback")]
        public string previosyearchargeback { get; set; }
        [JsonProperty("chargebackamount")]
        public string chargebackamount { get; set; }
        [JsonProperty("cardpaymenton")]
        public string cardpaymenton { get; set; }
        [JsonProperty("cardpaymentonother")]
        public string cardpaymentonother { get; set; }
        [JsonProperty("depositrequired")]
        public string depositrequired { get; set; }
        [JsonProperty("depositrequiredamt")]
        public string depositrequiredamt { get; set; }
        [JsonProperty("debitcardsvcs")]
        public string debitcardsvcs { get; set; }
        [JsonProperty("ebtsignup")]
        public string ebtsignup { get; set; }
        [JsonProperty("ebtnum")]
        public string ebtnum { get; set; }
        [JsonProperty("wexsignup")]
        public string wexsignup { get; set; }
        [JsonProperty("cksvcssignup")]
        public string cksvcssignup { get; set; }
        [JsonProperty("giftcardsvcs")]
        public string giftcardsvcs { get; set; }
        [JsonProperty("leasesvcs")]
        public string leasesvcs { get; set; }
        [JsonProperty("mccreditonly")]
        public string mccreditonly { get; set; }
        [JsonProperty("mcnonpindebitonly")]
        public string mcnonpindebitonly { get; set; }
        [JsonProperty("visacreditonly")]
        public string visacreditonly { get; set; }
        [JsonProperty("visanonpindebitonly")]
        public string visanonpindebitonly { get; set; }
        [JsonProperty("disccreditonly")]
        public string disccreditonly { get; set; }
        [JsonProperty("discnonpindebitonly")]
        public string discnonpindebitonly { get; set; }
        [JsonProperty("amexsvcreq")]
        public string amexsvcreq { get; set; }
        [JsonProperty("amexrate")]
        public string amexrate { get; set; }
        [JsonProperty("amexmofee")]
        public string amexmofee { get; set; }
        [JsonProperty("grosspay")]
        public string grosspay { get; set; }
        [JsonProperty("axppayfreq")]
        public string axppayfreq { get; set; }
        [JsonProperty("amexmonvol")]
        public string amexmonvol { get; set; }
        [JsonProperty("amexavgtkt")]
        public string amexavgtkt { get; set; }
        [JsonProperty("termmfg")]
        public string termmfg { get; set; }
        [JsonProperty("termmfg2")]
        public string termmfg2 { get; set; }
        [JsonProperty("termmodel")]
        public string termmodel { get; set; }
        [JsonProperty("repro")]
        public string repro { get; set; }
        [JsonProperty("multimerch1")]
        public string multimerch1 { get; set; }
        [JsonProperty("autoclose")]
        public string autoclose { get; set; }
        [JsonProperty("autoclosetime")]
        public string autoclosetime { get; set; }
        [JsonProperty("autoclosetime2")]
        public string autoclosetime2 { get; set; }
        [JsonProperty("tip")]
        public string tip { get; set; }
        [JsonProperty("tip2")]
        public string tip2 { get; set; }
        [JsonProperty("usepmtgateway")]
        public string usepmtgateway { get; set; }
        [JsonProperty("pmtgateway")]
        public string pmtgateway { get; set; }
        [JsonProperty("pmtgateway2")]
        public string pmtgateway2 { get; set; }
        [JsonProperty("shopcart")]
        public string shopcart { get; set; }
        [JsonProperty("software")]
        public string software { get; set; }
        [JsonProperty("software2")]
        public string software2 { get; set; }
        [JsonProperty("softwarever")]
        public string softwarever { get; set; }
        [JsonProperty("softwarepcicompliant")]
        public string softwarepcicompliant { get; set; }
        [JsonProperty("multimerch2")]
        public string multimerch2 { get; set; }
        [JsonProperty("termsn")]
        public string termsn { get; set; }
        [JsonProperty("termsupplier")]
        public string termsupplier { get; set; }
        [JsonProperty("termsupplier2")]
        public string termsupplier2 { get; set; }
        [JsonProperty("termsupplier3")]
        public string termsupplier3 { get; set; }
        [JsonProperty("termsupplier4")]
        public string termsupplier4 { get; set; }
        [JsonProperty("ship")]
        public string ship { get; set; }
        [JsonProperty("ship1")]
        public string ship1 { get; set; }
        [JsonProperty("ship2")]
        public string ship2 { get; set; }
        [JsonProperty("ship3")]
        public string ship3 { get; set; }
        [JsonProperty("ship4")]
        public string ship4 { get; set; }
        [JsonProperty("processor")]
        public string processor { get; set; }
        [JsonProperty("processor1")]
        public string processor1 { get; set; }
        [JsonProperty("dwnload4")]
        public string dwnload4 { get; set; }
        [JsonProperty("dwnload5")]
        public string dwnload5 { get; set; }
        [JsonProperty("leasesvcs1")]
        public string leasesvcs1 { get; set; }
        [JsonProperty("leasesvcs3")]
        public string leasesvcs3 { get; set; }
        [JsonProperty("leasesvcs4")]
        public string leasesvcs4 { get; set; }
        [JsonProperty("leasesvcs5")]
        public string leasesvcs5 { get; set; }
        [JsonProperty("wireless")]
        public string wireless { get; set; }
        [JsonProperty("wireless1")]
        public string wireless1 { get; set; }
        [JsonProperty("wireless2")]
        public string wireless2 { get; set; }
        [JsonProperty("ethernet")]
        public string ethernet { get; set; }
        [JsonProperty("site1")]
        public string site1 { get; set; }
        [JsonProperty("site2")]
        public string site2 { get; set; }
        [JsonProperty("site3")]
        public string site3 { get; set; }
        [JsonProperty("site4")]
        public string site4 { get; set; }
        [JsonProperty("pmtgateway4")]
        public string pmtgateway4 { get; set; }
        [JsonProperty("dwnload6")]
        public string dwnload6 { get; set; }
        [JsonProperty("site2a")]
        public string site2a { get; set; }
        [JsonProperty("thirdprty")]
        public string thirdprty { get; set; }
        [JsonProperty("thirdprty1")]
        public string thirdprty1 { get; set; }
        [JsonProperty("thirdprty2")]
        public string thirdprty2 { get; set; }
        [JsonProperty("thirdprty3")]
        public string thirdprty3 { get; set; }
        [JsonProperty("thirdprty4")]
        public string thirdprty4 { get; set; }
        [JsonProperty("thirdprty5")]
        public string thirdprty5 { get; set; }
        [JsonProperty("motofull")]
        public string motofull { get; set; }
        [JsonProperty("motofullb")]
        public string motofullb { get; set; }
        [JsonProperty("motofullc")]
        public string motofullc { get; set; }
        [JsonProperty("motofulld")]
        public string motofulld { get; set; }
        [JsonProperty("site5")]
        public string site5 { get; set; }
        [JsonProperty("leasessvcs6")]
        public string leasessvcs6 { get; set; }
        [JsonProperty("leasesvcs6")]
        public string leasesvcs6 { get; set; }
        [JsonProperty("cfgallowdebitcashfbackfee")]
        public string cfgallowdebitcashfbackfee { get; set; }
        [JsonProperty("cfgmcp")]
        public string cfgmcp { get; set; }
        [JsonProperty("cfgdcc")]
        public string cfgdcc { get; set; }
        [JsonProperty("cfgdebitsurcharge")]
        public string cfgdebitsurcharge { get; set; }
        [JsonProperty("cfgdebitsurchargeamount")]
        public string cfgdebitsurchargeamount { get; set; }
        [JsonProperty("mobileequipmentbrand")]
        public string mobileequipmentbrand { get; set; }
        [JsonProperty("mobileequipmentmodel")]
        public string mobileequipmentmodel { get; set; }
        [JsonProperty("ecommercesslused")]
        public string ecommercesslused { get; set; }
        [JsonProperty("mobileequipmentbrand2")]
        public string mobileequipmentbrand2 { get; set; }
        [JsonProperty("mobileequipmentmodel2")]
        public string mobileequipmentmodel2 { get; set; }
        [JsonProperty("depositrequiredpct")]
        public string depositrequiredpct { get; set; }
        [JsonProperty("mktingmethodsother")]
        public string mktingmethodsother { get; set; }
        [JsonProperty("bankid")]
        public string bankid { get; set; }
        [JsonProperty("bankaccountholder")]
        public string bankaccountholder { get; set; }
        [JsonProperty("bankaccountholder2")]
        public string bankaccountholder2 { get; set; }
        [JsonProperty("purchaseqty1")]
        public string purchaseqty1 { get; set; }
        [JsonProperty("purchaseqty2")]
        public string purchaseqty2 { get; set; }
        [JsonProperty("purchaseqty3")]
        public string purchaseqty3 { get; set; }
        [JsonProperty("purchasedesc1")]
        public string purchasedesc1 { get; set; }
        [JsonProperty("purchasedesc2")]
        public string purchasedesc2 { get; set; }
        [JsonProperty("purchasedesc3")]
        public string purchasedesc3 { get; set; }
        [JsonProperty("storecreditcards")]
        public string storecreditcards { get; set; }
        [JsonProperty("paymentoverssl")]
        public string paymentoverssl { get; set; }
        [JsonProperty("merchantcertificatenumber")]
        public string merchantcertificatenumber { get; set; }
        [JsonProperty("certificateissuer")]
        public string certificateissuer { get; set; }
        [JsonProperty("certificateexpdate")]
        public string certificateexpdate { get; set; }
        [JsonProperty("certificatetype")]
        public string certificatetype { get; set; }
        [JsonProperty("paymentafterdelivery")]
        public string paymentafterdelivery { get; set; }
        [JsonProperty("whoperformsfulfillment")]
        public string whoperformsfulfillment { get; set; }
        [JsonProperty("whoperformsfulfillmentother")]
        public string whoperformsfulfillmentother { get; set; }
        [JsonProperty("fulfillmentname")]
        public string fulfillmentname { get; set; }
        [JsonProperty("fulfillmentcontact")]
        public string fulfillmentcontact { get; set; }
        [JsonProperty("fulfillmentaddress")]
        public string fulfillmentaddress { get; set; }
        [JsonProperty("fulfillmentcity")]
        public string fulfillmentcity { get; set; }
        [JsonProperty("fulfillmentstate")]
        public string fulfillmentstate { get; set; }
        [JsonProperty("fulfillmentzipcode")]
        public string fulfillmentzipcode { get; set; }
        [JsonProperty("fulfillmentphone")]
        public string fulfillmentphone { get; set; }
        [JsonProperty("deliverymethod")]
        public string deliverymethod { get; set; }
        [JsonProperty("mktnegativeresponseorauto")]
        public string mktnegativeresponseorauto { get; set; }
        [JsonProperty("mktnegresponseorautoother")]
        public string mktnegresponseorautoother { get; set; }
        [JsonProperty("fulfillmentpaymentbefore")]
        public string fulfillmentpaymentbefore { get; set; }
        [JsonProperty("fulfillmentpaymentbeforedays")]
        public string fulfillmentpaymentbeforedays { get; set; }
        [JsonProperty("productguarantees")]
        public string productguarantees { get; set; }
        [JsonProperty("debitsalesmonthlyvol")]
        public string debitsalesmonthlyvol { get; set; }
        [JsonProperty("debitsalesavg")]
        public string debitsalesavg { get; set; }
        [JsonProperty("cfgtipfunction")]
        public string cfgtipfunction { get; set; }
        [JsonProperty("cfgdebitautosettle")]
        public string cfgdebitautosettle { get; set; }
        [JsonProperty("cfgpasswordprotect")]
        public string cfgpasswordprotect { get; set; }
        [JsonProperty("cfgallowdebitcashback")]
        public string cfgallowdebitcashback { get; set; }
        [JsonProperty("cfgdebitautosettletime")]
        public string cfgdebitautosettletime { get; set; }
        [JsonProperty("cfgallowdebitcashfbackee")]
        public string cfgallowdebitcashfbackee { get; set; }
        [JsonProperty("averagevisamonth")]
        public string averagevisamonth { get; set; }
        [JsonProperty("averagevisamcmonth")]
        public string averagevisamcmonth { get; set; }
        [JsonProperty("averagesalevisa")]
        public string averagesalevisa { get; set; }
        [JsonProperty("averagesalevisamc")]
        public string averagesalevisamc { get; set; }
        [JsonProperty("amexannvol")]
        public string amexannvol { get; set; }
        [JsonProperty("needpinpad")]
        public string needpinpad { get; set; }
        [JsonProperty("needcheckreader")]
        public string needcheckreader { get; set; }
        [JsonProperty("supplyingpinpad")]
        public string supplyingpinpad { get; set; }
        [JsonProperty("supplyingcheckreader")]
        public string supplyingcheckreader { get; set; }
        [JsonProperty("existingterminalmodel")]
        public string existingterminalmodel { get; set; }
        [JsonProperty("existingterminalmanufacturer")]
        public string existingterminalmanufacturer { get; set; }
        [JsonProperty("existingcheckreadermanu")]
        public string existingcheckreadermanu { get; set; }
        [JsonProperty("existingcheckreadermodel")]
        public string existingcheckreadermodel { get; set; }
        [JsonProperty("existingpinpadmodel")]
        public string existingpinpadmodel { get; set; }
        [JsonProperty("existingpinpadmanu")]
        public string existingpinpadmanu { get; set; }
        [JsonProperty("totalpercent")]
        public string totalpercent { get; set; }
        [JsonProperty("timezone")]
        public string timezone { get; set; }
        [JsonProperty("equipmentpaymentby")]
        public string equipmentpaymentby { get; set; }
        [JsonProperty("equipment_type")]
        public string equipment_type { get; set; }
        [JsonProperty("equipment_type_lease")]
        public string equipment_type_lease { get; set; }
        [JsonProperty("equipment_type_rent")]
        public string equipment_type_rent { get; set; }
        [JsonProperty("equipment_rental_deposit")]
        public string equipment_rental_deposit { get; set; }
        [JsonProperty("zone")]
        public string zone { get; set; }
        [JsonProperty("immediatedelivery")]
        public string immediatedelivery { get; set; }
        [JsonProperty("sqft")]
        public string sqft { get; set; }
        [JsonProperty("refundpolicyexist")]
        public string refundpolicyexist { get; set; }
        [JsonProperty("daystosubmittransactions")]
        public string daystosubmittransactions { get; set; }
        [JsonProperty("owninventory")]
        public string owninventory { get; set; }
        [JsonProperty("inventorystoredatlocation")]
        public string inventorystoredatlocation { get; set; }
        [JsonProperty("inventorylocation")]
        public string inventorylocation { get; set; }
        [JsonProperty("warranteeguaranteeoffered")]
        public string warranteeguaranteeoffered { get; set; }
        [JsonProperty("warranteeguaranteetype")]
        public string warranteeguaranteetype { get; set; }
        [JsonProperty("refundtype")]
        public string refundtype { get; set; }
        [JsonProperty("refundother")]
        public string refundother { get; set; }
        [JsonProperty("whoenterscreditcardinfo")]
        public string whoenterscreditcardinfo { get; set; }
        [JsonProperty("whoenterscreditcardinfoother")]
        public string whoenterscreditcardinfoother { get; set; }
        [JsonProperty("pcinameversion")]
        public string pcinameversion { get; set; }
        [JsonProperty("pciversion")]
        public string pciversion { get; set; }
        [JsonProperty("merchantpcicompliant")]
        public string merchantpcicompliant { get; set; }
        [JsonProperty("namepcicompliant")]
        public string namepcicompliant { get; set; }
        [JsonProperty("terminalinsurance")]
        public string terminalinsurance { get; set; }
        [JsonProperty("fdglrelationshipcode")]
        public string fdglrelationshipcode { get; set; }
        [JsonProperty("billingoptions")]
        public string billingoptions { get; set; }
        [JsonProperty("newamex")]
        public string newamex { get; set; }
        [JsonProperty("daysuntildelivery_0")]
        public string daysuntildelivery_0 { get; set; }
        [JsonProperty("daysuntildelivery_1to3")]
        public string daysuntildelivery_1to3 { get; set; }
        [JsonProperty("daysuntildelivery_4to7")]
        public string daysuntildelivery_4to7 { get; set; }
        [JsonProperty("daysuntildelivery_8to14")]
        public string daysuntildelivery_8to14 { get; set; }
        [JsonProperty("daysuntildelivery_15to30")]
        public string daysuntildelivery_15to30 { get; set; }
        [JsonProperty("daysuntildelivery_30plus")]
        public string daysuntildelivery_30plus { get; set; }
        [JsonProperty("daysuntildelivery_15to30_pct")]
        public string daysuntildelivery_15to30_pct { get; set; }
        [JsonProperty("replacerefund")]
        public string replacerefund { get; set; }
        [JsonProperty("orderfulfillmentother")]
        public string orderfulfillmentother { get; set; }
        [JsonProperty("zipcodeplus4")]
        public string zipcodeplus4 { get; set; }
        [JsonProperty("fulfillmentzipcodeplus4")]
        public string fulfillmentzipcodeplus4 { get; set; }
        [JsonProperty("addresscivicnumber")]
        public string addresscivicnumber { get; set; }
        [JsonProperty("fulfillmentcivicnumber")]
        public string fulfillmentcivicnumber { get; set; }
        [JsonProperty("fulfillmentsuite")]
        public string fulfillmentsuite { get; set; }
        [JsonProperty("facility")]
        public string facility { get; set; }
        [JsonProperty("acceptallcard")]
        public string acceptallcard { get; set; }
        [JsonProperty("selectedcards")]
        public string selectedcards { get; set; }
        [JsonProperty("amountborrow")]
        public string amountborrow { get; set; }
        [JsonProperty("existcreditcardproc")]
        public string existcreditcardproc { get; set; }
        [JsonProperty("creditcardmerchantid")]
        public string creditcardmerchantid { get; set; }
        [JsonProperty("creditcardyear")]
        public string creditcardyear { get; set; }
        [JsonProperty("creditcardmonth")]
        public string creditcardmonth { get; set; }
        [JsonProperty("existcheckproc")]
        public string existcheckproc { get; set; }
        [JsonProperty("checkprocmerchantid")]
        public string checkprocmerchantid { get; set; }
        [JsonProperty("checkprocyear")]
        public string checkprocyear { get; set; }
        [JsonProperty("checkprocmonth")]
        public string checkprocmonth { get; set; }
        [JsonProperty("avgcheckmonthvol")]
        public string avgcheckmonthvol { get; set; }
        [JsonProperty("avgsalecreditcardamt")]
        public string avgsalecreditcardamt { get; set; }
        [JsonProperty("avgsalechekamt")]
        public string avgsalechekamt { get; set; }
        [JsonProperty("cks_account_type")]
        public string cks_account_type { get; set; }
        [JsonProperty("dba_address")]
        public string dba_address { get; set; }
        [JsonProperty("websiteurl")]
        public string websiteurl { get; set; }
        [JsonProperty("websiteencrypt")]
        public string websiteencrypt { get; set; }
        [JsonProperty("survey")]
        public string survey { get; set; }
        [JsonProperty("survey_found_merch")]
        public string survey_found_merch { get; set; }
        [JsonProperty("site_photo")]
        public string site_photo { get; set; }
        [JsonProperty("valid_id")]
        public string valid_id { get; set; }
        [JsonProperty("equip_capture")]
        public string equip_capture { get; set; }
        [JsonProperty("connectivity")]
        public string connectivity { get; set; }
        [JsonProperty("imprinter")]
        public string imprinter { get; set; }
        [JsonProperty("dial_prefix")]
        public string dial_prefix { get; set; }
        [JsonProperty("equip_provider")]
        public string equip_provider { get; set; }
        [JsonProperty("equip_download")]
        public string equip_download { get; set; }
        [JsonProperty("hasloyaltycard")]
        public string hasloyaltycard { get; set; }
        [JsonProperty("maxtktsize")]
        public string maxtktsize { get; set; }
        [JsonProperty("monthlyvolumerequired")]
        public string monthlyvolumerequired { get; set; }
        [JsonProperty("previouscardaccept")]
        public string previouscardaccept { get; set; }
        [JsonProperty("previousstatements")]
        public string previousstatements { get; set; }
        [JsonProperty("previousecommerce")]
        public string previousecommerce { get; set; }
        [JsonProperty("activelyconducting")]
        public string activelyconducting { get; set; }
        [JsonProperty("activelyconductingexplain")]
        public string activelyconductingexplain { get; set; }
        [JsonProperty("futuredeliverypct")]
        public string futuredeliverypct { get; set; }
        [JsonProperty("deliveryexplanation")]
        public string deliveryexplanation { get; set; }
        [JsonProperty("shippingmethod")]
        public string shippingmethod { get; set; }
        [JsonProperty("shipto")]
        public string shipto { get; set; }
        [JsonProperty("shippingaddress")]
        public string shippingaddress { get; set; }
        [JsonProperty("inventorysufficient")]
        public string inventorysufficient { get; set; }
        [JsonProperty("inventorysufficientexplain")]
        public string inventorysufficientexplain { get; set; }
        [JsonProperty("inventoryamount")]
        public string inventoryamount { get; set; }
        [JsonProperty("inventoryamountexplain")]
        public string inventoryamountexplain { get; set; }
        [JsonProperty("servicelistedonapp")]
        public string servicelistedonapp { get; set; }
        [JsonProperty("servicelistedonappexplain")]
        public string servicelistedonappexplain { get; set; }
        [JsonProperty("inspectedby")]
        public string inspectedby { get; set; }
        [JsonProperty("inboundcalls")]
        public string inboundcalls { get; set; }
        [JsonProperty("outboundcalls")]
        public string outboundcalls { get; set; }
        [JsonProperty("ach")]
        public string ach { get; set; }
        [JsonProperty("fein")]
        public string fein { get; set; }
        [JsonProperty("legalentity")]
        public string legalentity { get; set; }
        [JsonProperty("leadaccdistinction")]
        public string leadaccdistinction { get; set; }
        [JsonProperty("feetier")]
        public string feetier { get; set; }
        [JsonProperty("templateid")]
        public string templateid { get; set; }
        [JsonProperty("bankaccountuseforboth")]
        public string bankaccountuseforboth { get; set; }
        [JsonProperty("daysuntildeliveryvol1")]
        public string daysuntildeliveryvol1 { get; set; }
        [JsonProperty("daysuntildeliveryvol2")]
        public string daysuntildeliveryvol2 { get; set; }
        [JsonProperty("daysuntildeliveryvol3")]
        public string daysuntildeliveryvol3 { get; set; }
        [JsonProperty("daysuntildeliverydays1")]
        public string daysuntildeliverydays1 { get; set; }
        [JsonProperty("daysuntildeliverydays2")]
        public string daysuntildeliverydays2 { get; set; }
        [JsonProperty("pciemailaddress")]
        public string pciemailaddress { get; set; }
        [JsonProperty("daysuntildeliverydays3")]
        public string daysuntildeliverydays3 { get; set; }
        [JsonProperty("documents")]
        public List<Document> Documents { get; set; }
    }

}
