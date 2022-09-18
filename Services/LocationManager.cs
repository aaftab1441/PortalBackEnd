using System;
using System.Data;
using System.Data.SqlClient;
using EaglePortal.Utils;
using System.Collections.Generic;
using EaglePortal.Models;

namespace EaglePortal.Services
{
    public class LocationManager
    {
        private SqlConnection conn;

        private UtilityManager utilityManager;
        public LocationManager(String connectionString)
        {
            conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            utilityManager = new UtilityManager();

        }
        ~LocationManager()
        {
            try
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            catch (Exception e)
            {

            }
        }
        public void SaveLocation(Location location)
        {
            SqlCommand addCommand = conn.CreateCommand();
            #region sql
            string insertSQL = "INSERT INTO Location ([displayid], [customerid], [name],[address],[suite],[city],[state],[zipcode],[phone],[fax]," +
                "[emailaddress],[locationtype],[locationtypeother],[contactname],[bankaccountfordeposit],[bankaccountforwithdrawls],[bankname]," +
                "[bankname2],[bankaddress],[bankcity],[bankcity2],[bankstate],[bankstate2],[bankzipcode],[bankzipcode2],[bankcontactname]," +
                "[bankcontactname2],[bankphone],[bankphone2],[bankfax],[bankfax2],[bankrouting],[bankrouting2],[bankaccount],[bankaccount2]," +
                "[bankauthorizedsignor],[bankauthorizedsignor2],[maillocation],[needterminal],[terminalshippinglocation]," +
                "[terminalmanufacturer],[usepos],[posmanufacturer],[posmodel],[usecheckimager],[checkimagermanufacturer],[checkimagermodel]," +
                "[amexmerchantid],[discovermerchantid],[processdiscover],[acceptdiscover],[percentswipe],[percentkey],[visamcmerchantid]," +
                "[pinpadmodel],[acceptamex],[requestedamount],[requestedminimum],[intendeduse],[previousadvance],[previouslender],[previousactive]," +
                "[previousbalance],[ownershiptype],[businessformationstate],[visamcaccepted],[visamcprovider],[sellingtype],[sellproductarea],[goodservicetype]," +
                "[mcccode],[averagesale],[averagemcmonth],[averagemctkt],[averagediscmonth],[averagedisctkt],[averagediscovermonth],[averageamexmonth],[averagegrossmonth]," +
                "[visamcdiscmaxmonthlyvolume],[visamcdiscminavgticket],[visamcdiscmaxavgticket],[visamcdiscannualvolume],[poscontactname],[poscontactnumber]," +
                "[highestsale],[creditvolume],[numberlocations],[dateopened],[seasonal],[rentcurrent],[landlordname],[landlordcontactname],[landlordphone],[lengthofownershipyear]," +
                "[lengthofownershipmonth],[seasonalmonths],[leaseexpirationdate],[monthlyrent],[acceptinglength],[currentprocessor],[batchesmonthly],[isdefault],[merchantid]," +
                "[check21plusmerchantid],[check21plusterminalid],[achdebitmerchantid],[achdebitterminalid],[checksbyphonemerchantid],[checksbyphoneterminalid],[checksbywebmerchantid]," +
                "[checksbywebterminalid],[checkcollectmerchantid],[checkcollectterminalid],[ckpapermerchantid],[ckpaperterminalid],[ckposmerchantid],[ckposterminalid],[voidflag]," +
                "[viewed],[created_at],[updated_at],[terminalmodel],[estimatedannualvolume],[estimatedavgticket],[hasprinter],[terminalmanufacturerother],[haspinpad],[dialoutphone]," +
                "[dialoutphoneother],[printer],[timeremaininglease],[timeremainingleasemonth],[previousprocessorterminatedwhom],[previousprocessorterminated],[previousprocessorterminatedexplain]," +
                "[previousprocessorterminateddate],[checkreader],[checkreadermodel],[bankaccounttype],[bankaccounttype2],[isbusinessforsale],[havetaxliens],[merchanttype],[merchanttypeother]," +
                "[b2bpercent],[b2cpercent],[daysuntildelivery_0_pct],[daysuntildelivery_1to3_pct],[daysuntildelivery_4to7_pct],[daysuntildelivery_8to14_pct],[daysuntildelivery_30plus_pct]," +
                "[daysuntildelivery_total_pct],[returnpolicyother],[returnpolicy],[mktingmethods],[haveretaillocation],[retailswipedpct],[retailkeyedpct],[internetpct],[mailorderpct],[totalpct]," +
                "[settlementon],[settlementonother],[recurringcharge],[recurringchargefrequency],[recurringchargeother],[cardpaymenton]," +
                "[cardpaymentonother],[depositrequired],[depositrequiredamt],[debitcardsvcs],[ebtsignup],[ebtnum],[wexsignup],[cksvcssignup],[giftcardsvcs],[leasesvcs],[mccreditonly]," +
                "[mcnonpindebitonly],[visacreditonly],[visanonpindebitonly],[disccreditonly],[discnonpindebitonly],[amexsvcreq],[amexrate],[amexmofee],[grosspay],[axppayfreq],[amexmonvol]," +
                "[amexavgtkt],[termmfg],[termmfg2],[termmodel],[repro],[multimerch1],[autoclose],[autoclosetime],[autoclosetime2],[tip],[tip2],[usepmtgateway],[pmtgateway],[pmtgateway2]," +
                "[shopcart],[software],[softwarepcicompliant],[multimerch2],[termsn],[termsupplier],[termsupplier2],[termsupplier3],[termsupplier4],[ship],[ship1]," +
                "[ship2],[ship3],[ship4],[processor],[processor1],[dwnload4],[dwnload5],[leasesvcs1],[leasesvcs3],[leasesvcs4],[leasesvcs5],[wireless],[wireless1],[wireless2],[ethernet]," +
                "[site1],[site2],[site3],[site4],[pmtgateway4],[dwnload6],[site2a],[thirdprty],[thirdprty1],[thirdprty2],[thirdprty3],[thirdprty4],[thirdprty5],[motofull],[motofullb]," +
                "[motofullc],[motofulld],[site5],[leasessvcs6],[leasesvcs6],[cfgallowdebitcashfbackfee],[cfgmcp],[cfgdcc],[cfgdebitsurcharge],[cfgdebitsurchargeamount],[mobileequipmentbrand]," +
                "[mobileequipmentmodel],[ecommercesslused],[mobileequipmentbrand2],[mobileequipmentmodel2],[depositrequiredpct],[mktingmethodsother],[bankid],[bankaccountholder]," +
                "[bankaccountholder2],[purchaseqty1],[purchaseqty2],[purchaseqty3],[purchasedesc1],[purchasedesc2],[purchasedesc3],[storecreditcards],[paymentoverssl]," +
                "[merchantcertificatenumber],[certificateissuer],[certificateexpdate],[certificatetype],[paymentafterdelivery],[whoperformsfulfillment],[whoperformsfulfillmentother]," +
                "[fulfillmentname],[fulfillmentcontact],[fulfillmentaddress],[fulfillmentcity],[fulfillmentstate],[fulfillmentzipcode],[fulfillmentphone],[deliverymethod]," +
                "[mktnegativeresponseorauto],[mktnegresponseorautoother],[fulfillmentpaymentbefore],[fulfillmentpaymentbeforedays],[productguarantees],[debitsalesmonthlyvol]," +
                "[debitsalesavg],[cfgtipfunction],[cfgdebitautosettle],[cfgpasswordprotect],[cfgallowdebitcashback],[cfgdebitautosettletime],[cfgallowdebitcashfbackee],[averagevisamonth]," +
                "[averagevisamcmonth],[averagesalevisa],[averagesalevisamc],[amexannvol],[needpinpad],[needcheckreader],[supplyingcheckreader],[existingterminalmodel]," +
                "[existingterminalmanufacturer],[existingcheckreadermanu],[existingcheckreadermodel],[existingpinpadmodel],[totalpercent],[timezone],[equipmentpaymentby]," +
                "[equipment_type],[equipment_type_lease],[equipment_type_rent],[equipment_rental_deposit],[zone],[immediatedelivery],[sqft],[refundpolicyexist],[daystosubmittransactions]," +
                "[owninventory],[inventorystoredatlocation],[inventorylocation],[warranteeguaranteeoffered],[warranteeguaranteetype],[refundtype],[refundother],[whoenterscreditcardinfo]," +
                "[whoenterscreditcardinfoother],[pcinameversion],[pciversion],[merchantpcicompliant],[namepcicompliant],[terminalinsurance],[fdglrelationshipcode],[billingoptions],[newamex]," +
                "[daysuntildelivery_0],[daysuntildelivery_1to3],[daysuntildelivery_4to7],[daysuntildelivery_8to14],[daysuntildelivery_15to30],[daysuntildelivery_30plus]," +
                "[daysuntildelivery_15to30_pct],[replacerefund],[orderfulfillmentother],[zipcodeplus4],[fulfillmentzipcodeplus4],[addresscivicnumber],[fulfillmentcivicnumber]," +
                "[fulfillmentsuite],[facility],[acceptallcard],[selectedcards],[amountborrow],[existcreditcardproc],[creditcardmerchantid],[creditcardyear],[creditcardmonth]," +
                "[existcheckproc],[checkprocmerchantid],[checkprocyear],[checkprocmonth],[avgcheckmonthvol],[avgsalecreditcardamt],[avgsalechekamt],[cks_account_type],[dba_address]," +
                "[websiteurl], [websiteencrypt],[survey],[survey_found_merch], [site_photo],[valid_id],[equip_capture],[connectivity],[imprinter],[dial_prefix],[equip_provider],[equip_download],[hasloyaltycard]," +
                "[maxtktsize],[monthlyvolumerequired],[previouscardaccept]," +
                "[previousstatements],[previousecommerce],[activelyconducting],[activelyconductingexplain],[futuredeliverypct],[deliveryexplanation],[shippingmethod],[shipto]," +
                "[shippingaddress],[inventorysufficient],[inventorysufficientexplain],[inventoryamount],[inventoryamountexplain],[servicelistedonapp],[servicelistedonappexplain]," +
                "[inspectedby],[inboundcalls],[outboundcalls],[ach],[fein],[legalentity],[leadaccdistinction],[feetier],[templateid],[bankaccountuseforboth],[daysuntildeliveryvol1]," +
                "[daysuntildeliveryvol2],[daysuntildeliveryvol3],[daysuntildeliverydays1],[daysuntildeliverydays2],[daysuntildeliverydays3],[pciemailaddress])" +
                "VALUES(@displayid, @customerid, @name,@address,@suite,@city,@state,@zipcode,@phone,@fax," +
                "@emailaddress,@locationtype,@locationtypeother,@contactname,@bankaccountfordeposit,@bankaccountforwithdrawls,@bankname," +
                "@bankname2,@bankaddress,@bankcity,@bankcity2,@bankstate,@bankstate2,@bankzipcode,@bankzipcode2,@bankcontactname," +
                "@bankcontactname2,@bankphone,@bankphone2,@bankfax,@bankfax2,@bankrouting,@bankrouting2,@bankaccount,@bankaccount2," +
                "@bankauthorizedsignor,@bankauthorizedsignor2,@maillocation,@needterminal,@terminalshippinglocation," +
                "@terminalmanufacturer,@usepos,@posmanufacturer,@posmodel,@usecheckimager,@checkimagermanufacturer,@checkimagermodel," +
                "@amexmerchantid,@discovermerchantid,@processdiscover,@acceptdiscover,@percentswipe,@percentkey,@visamcmerchantid," +
                "@pinpadmodel,@acceptamex,@requestedamount,@requestedminimum,@intendeduse,@previousadvance,@previouslender,@previousactive," +
                "@previousbalance,@ownershiptype,@businessformationstate,@visamcaccepted,@visamcprovider,@sellingtype,@sellproductarea,@goodservicetype," +
                "@mcccode,@averagesale,@averagemcmonth,@averagemctkt,@averagediscmonth,@averagedisctkt,@averagediscovermonth,@averageamexmonth,@averagegrossmonth," +
                "@visamcdiscmaxmonthlyvolume,@visamcdiscminavgticket,@visamcdiscmaxavgticket,@visamcdiscannualvolume,@poscontactname,@poscontactnumber," +
                "@highestsale,@creditvolume,@numberlocations,@dateopened,@seasonal,@rentcurrent,@landlordname,@landlordcontactname,@landlordphone,@lengthofownershipyear," +
                "@lengthofownershipmonth,@seasonalmonths,@leaseexpirationdate,@monthlyrent,@acceptinglength,@currentprocessor,@batchesmonthly,@isdefault,@merchantid," +
                "@check21plusmerchantid,@check21plusterminalid,@achdebitmerchantid,@achdebitterminalid,@checksbyphonemerchantid,@checksbyphoneterminalid,@checksbywebmerchantid," +
                "@checksbywebterminalid,@checkcollectmerchantid,@checkcollectterminalid,@ckpapermerchantid,@ckpaperterminalid,@ckposmerchantid,@ckposterminalid,@voidflag," +
                "@viewed,@created_at,@updated_at,@terminalmodel,@estimatedannualvolume,@estimatedavgticket,@hasprinter,@terminalmanufacturerother,@haspinpad,@dialoutphone," +
                "@dialoutphoneother,@printer,@timeremaininglease,@timeremainingleasemonth,@previousprocessorterminatedwhom,@previousprocessorterminated,@previousprocessorterminatedexplain," +
                "@previousprocessorterminateddate,@checkreader,@checkreadermodel,@bankaccounttype,@bankaccounttype2,@isbusinessforsale,@havetaxliens,@merchanttype,@merchanttypeother," +
                "@b2bpercent,@b2cpercent,@daysuntildelivery_0_pct,@daysuntildelivery_1to3_pct,@daysuntildelivery_4to7_pct,@daysuntildelivery_8to14_pct,@daysuntildelivery_30plus_pct," +
                "@daysuntildelivery_total_pct,@returnpolicyother,@returnpolicy,@mktingmethods,@haveretaillocation,@retailswipedpct,@retailkeyedpct,@internetpct,@mailorderpct,@totalpct," +
                "@settlementon,@settlementonother,@recurringcharge,@recurringchargefrequency,@recurringchargeother,@cardpaymenton," +
                "@cardpaymentonother,@depositrequired,@depositrequiredamt,@debitcardsvcs,@ebtsignup,@ebtnum,@wexsignup,@cksvcssignup,@giftcardsvcs,@leasesvcs,@mccreditonly," +
                "@mcnonpindebitonly,@visacreditonly,@visanonpindebitonly,@disccreditonly,@discnonpindebitonly,@amexsvcreq,@amexrate,@amexmofee,@grosspay,@axppayfreq,@amexmonvol," +
                "@amexavgtkt,@termmfg,@termmfg2,@termmodel,@repro,@multimerch1,@autoclose,@autoclosetime,@autoclosetime2,@tip,@tip2,@usepmtgateway,@pmtgateway,@pmtgateway2," +
                "@shopcart,@software,@softwarepcicompliant,@multimerch2,@termsn,@termsupplier,@termsupplier2,@termsupplier3,@termsupplier4,@ship,@ship1," +
                "@ship2,@ship3,@ship4,@processor,@processor1,@dwnload4,@dwnload5,@leasesvcs1,@leasesvcs3,@leasesvcs4,@leasesvcs5,@wireless,@wireless1,@wireless2,@ethernet," +
                "@site1,@site2,@site3,@site4,@pmtgateway4,@dwnload6,@site2a,@thirdprty,@thirdprty1,@thirdprty2,@thirdprty3,@thirdprty4,@thirdprty5,@motofull,@motofullb," +
                "@motofullc,@motofulld,@site5,@leasessvcs6,@leasesvcs6,@cfgallowdebitcashfbackfee,@cfgmcp,@cfgdcc,@cfgdebitsurcharge,@cfgdebitsurchargeamount,@mobileequipmentbrand," +
                "@mobileequipmentmodel,@ecommercesslused,@mobileequipmentbrand2,@mobileequipmentmodel2,@depositrequiredpct,@mktingmethodsother,@bankid,@bankaccountholder," +
                "@bankaccountholder2,@purchaseqty1,@purchaseqty2,@purchaseqty3,@purchasedesc1,@purchasedesc2,@purchasedesc3,@storecreditcards,@paymentoverssl," +
                "@merchantcertificatenumber,@certificateissuer,@certificateexpdate,@certificatetype,@paymentafterdelivery,@whoperformsfulfillment,@whoperformsfulfillmentother," +
                "@fulfillmentname,@fulfillmentcontact,@fulfillmentaddress,@fulfillmentcity,@fulfillmentstate,@fulfillmentzipcode,@fulfillmentphone,@deliverymethod," +
                "@mktnegativeresponseorauto,@mktnegresponseorautoother,@fulfillmentpaymentbefore,@fulfillmentpaymentbeforedays,@productguarantees,@debitsalesmonthlyvol," +
                "@debitsalesavg,@cfgtipfunction,@cfgdebitautosettle,@cfgpasswordprotect,@cfgallowdebitcashback,@cfgdebitautosettletime,@cfgallowdebitcashfbackee,@averagevisamonth," +
                "@averagevisamcmonth,@averagesalevisa,@averagesalevisamc,@amexannvol,@needpinpad,@needcheckreader,@supplyingcheckreader,@existingterminalmodel," +
                "@existingterminalmanufacturer,@existingcheckreadermanu,@existingcheckreadermodel,@existingpinpadmodel,@totalpercent,@timezone,@equipmentpaymentby," +
                "@equipment_type,@equipment_type_lease,@equipment_type_rent,@equipment_rental_deposit,@zone,@immediatedelivery,@sqft,@refundpolicyexist,@daystosubmittransactions," +
                "@owninventory,@inventorystoredatlocation,@inventorylocation,@warranteeguaranteeoffered,@warranteeguaranteetype,@refundtype,@refundother,@whoenterscreditcardinfo," +
                "@whoenterscreditcardinfoother,@pcinameversion,@pciversion,@merchantpcicompliant,@namepcicompliant,@terminalinsurance,@fdglrelationshipcode,@billingoptions,@newamex," +
                "@daysuntildelivery_0,@daysuntildelivery_1to3,@daysuntildelivery_4to7,@daysuntildelivery_8to14,@daysuntildelivery_15to30,@daysuntildelivery_30plus," +
                "@daysuntildelivery_15to30_pct,@replacerefund,@orderfulfillmentother,@zipcodeplus4,@fulfillmentzipcodeplus4,@addresscivicnumber,@fulfillmentcivicnumber," +
                "@fulfillmentsuite,@facility,@acceptallcard,@selectedcards,@amountborrow,@existcreditcardproc,@creditcardmerchantid,@creditcardyear,@creditcardmonth," +
                "@existcheckproc,@checkprocmerchantid,@checkprocyear,@checkprocmonth,@avgcheckmonthvol,@avgsalecreditcardamt,@avgsalechekamt,@cks_account_type,@dba_address," +
                "@websiteurl, @websiteencrypt, @survey,@survey_found_merch, @site_photo,@valid_id,@equip_capture,@connectivity,@imprinter,@dial_prefix,@equip_provider,@equip_download,@hasloyaltycard," +
                "@maxtktsize,@monthlyvolumerequired,@previouscardaccept," +
                "@previousstatements,@previousecommerce,@activelyconducting,@activelyconductingexplain,@futuredeliverypct,@deliveryexplanation,@shippingmethod,@shipto," +
                "@shippingaddress,@inventorysufficient,@inventorysufficientexplain,@inventoryamount,@inventoryamountexplain,@servicelistedonapp,@servicelistedonappexplain," +
                "@inspectedby,@inboundcalls,@outboundcalls,@ach,@fein,@legalentity,@leadaccdistinction,@feetier,@templateid,@bankaccountuseforboth,@daysuntildeliveryvol1," +
                "@daysuntildeliveryvol2,@daysuntildeliveryvol3,@daysuntildeliverydays1,@daysuntildeliverydays2,@daysuntildeliverydays3,@pciemailaddress) SELECT CAST(scope_identity() AS int);";


            addCommand.CommandText = insertSQL;

            addCommand.Parameters.AddWithValue("@displayid", location.DisplayId == null ? DBNull.Value : location.DisplayId);
            addCommand.Parameters.AddWithValue("@customerid", location.CustomerId == null ? DBNull.Value : location.CustomerId);
            addCommand.Parameters.AddWithValue("@name", location.Name == null ? DBNull.Value : location.Name);
            addCommand.Parameters.AddWithValue("@address", location.Address == null ? DBNull.Value : location.Address);
            addCommand.Parameters.AddWithValue("@suite", location.Suite == null ? DBNull.Value : location.Suite);
            addCommand.Parameters.AddWithValue("@city", location.City == null ? DBNull.Value : location.City);
            addCommand.Parameters.AddWithValue("@state", location.State == null ? DBNull.Value : location.State);
            addCommand.Parameters.AddWithValue("@zipcode", location.ZipCode == null ? DBNull.Value : location.ZipCode);
            addCommand.Parameters.AddWithValue("@phone", location.Phone == null ? DBNull.Value : location.Phone);
            addCommand.Parameters.AddWithValue("@fax", location.Fax == null ? DBNull.Value : location.Fax);
            addCommand.Parameters.AddWithValue("@emailaddress", location.Emailaddress == null ? DBNull.Value : location.Emailaddress);
            addCommand.Parameters.AddWithValue("@locationtype", location.Locationtype == null ? DBNull.Value : location.Locationtype);
            addCommand.Parameters.AddWithValue("@locationtypeother", location.Locationtypeother == null ? DBNull.Value : location.Locationtypeother);
            addCommand.Parameters.AddWithValue("@contactname", location.Contactname == null ? DBNull.Value : location.Contactname);
            addCommand.Parameters.AddWithValue("@bankaccountfordeposit", location.BankAccountfordeposit == null ? DBNull.Value : location.BankAccountfordeposit);
            addCommand.Parameters.AddWithValue("@bankaccountforwithdrawls", location.BankAccountforwithdrawls == null ? DBNull.Value : location.BankAccountforwithdrawls);
            addCommand.Parameters.AddWithValue("@bankname", location.Bankname == null ? DBNull.Value : location.Bankname);
            addCommand.Parameters.AddWithValue("@bankname2", location.Bankname2 == null ? DBNull.Value : location.Bankname2);
            addCommand.Parameters.AddWithValue("@bankaddress", location.Bankaddress == null ? DBNull.Value : location.Bankaddress);
            addCommand.Parameters.AddWithValue("@bankcity", location.Bankcity == null ? DBNull.Value : location.Bankcity);
            addCommand.Parameters.AddWithValue("@bankcity2", location.Bankcity2 == null ? DBNull.Value : location.Bankcity2);
            addCommand.Parameters.AddWithValue("@bankstate", location.Bankstate == null ? DBNull.Value : location.Bankstate);
            addCommand.Parameters.AddWithValue("@bankstate2", location.Bankstate2 == null ? DBNull.Value : location.Bankstate2);
            addCommand.Parameters.AddWithValue("@bankzipcode", location.Bankzipcode == null ? DBNull.Value : location.Bankzipcode);
            addCommand.Parameters.AddWithValue("@bankzipcode2", location.Bankzipcode2 == null ? DBNull.Value : location.Bankzipcode2);
            addCommand.Parameters.AddWithValue("@bankcontactname", location.Bankcontactname == null ? DBNull.Value : location.Bankcontactname);
            addCommand.Parameters.AddWithValue("@bankcontactname2", location.Bankcontactname2 == null ? DBNull.Value : location.Bankcontactname2);
            addCommand.Parameters.AddWithValue("@bankphone", location.Bankphone == null ? DBNull.Value : location.Bankphone);
            addCommand.Parameters.AddWithValue("@bankphone2", location.Bankphone2 == null ? DBNull.Value : location.Bankphone2);
            addCommand.Parameters.AddWithValue("@bankfax", location.Bankfax == null ? DBNull.Value : location.Bankfax);
            addCommand.Parameters.AddWithValue("@bankfax2", location.Bankfax2 == null ? DBNull.Value : location.Bankfax2);
            addCommand.Parameters.AddWithValue("@bankrouting", location.Bankrouting == null ? DBNull.Value : location.Bankrouting);
            addCommand.Parameters.AddWithValue("@bankrouting2", location.Bankrouting2 == null ? DBNull.Value : location.Bankrouting2);
            addCommand.Parameters.AddWithValue("@bankaccount", location.Bankaccount == null ? DBNull.Value : location.Bankaccount);
            addCommand.Parameters.AddWithValue("@bankaccount2", location.Bankaccount2 == null ? DBNull.Value : location.Bankaccount2);
            addCommand.Parameters.AddWithValue("@bankauthorizedsignor", location.Bankauthorizedsignor == null ? DBNull.Value : location.Bankauthorizedsignor);
            addCommand.Parameters.AddWithValue("@bankauthorizedsignor2", location.Bankauthorizedsignor2 == null ? DBNull.Value : location.Bankauthorizedsignor2);
            addCommand.Parameters.AddWithValue("@maillocation", location.Maillocation == null ? DBNull.Value : location.Maillocation);
            addCommand.Parameters.AddWithValue("@needterminal", location.Needterminal == null ? DBNull.Value : location.Needterminal);
            addCommand.Parameters.AddWithValue("@terminalshippinglocation", location.Terminalshippinglocation == null ? DBNull.Value : location.Terminalshippinglocation);
            addCommand.Parameters.AddWithValue("@terminalmanufacturer", location.Terminalmanufacturer == null ? DBNull.Value : location.Terminalmanufacturer);
            addCommand.Parameters.AddWithValue("@usepos", location.Usepos == null ? DBNull.Value : location.Usepos);
            addCommand.Parameters.AddWithValue("@posmanufacturer", location.Posmanufacturer == null ? DBNull.Value : location.Posmanufacturer);
            addCommand.Parameters.AddWithValue("@posmodel", location.PosModel == null ? DBNull.Value : location.PosModel);
            addCommand.Parameters.AddWithValue("@usecheckimager", location.UseCheckImager == null ? DBNull.Value : location.UseCheckImager);
            addCommand.Parameters.AddWithValue("@checkimagermanufacturer", location.CheckImagermanufacturer == null ? DBNull.Value : location.CheckImagermanufacturer);
            addCommand.Parameters.AddWithValue("@checkimagermodel", location.CheckImagermodel == null ? DBNull.Value : location.CheckImagermodel);
            addCommand.Parameters.AddWithValue("@amexmerchantid", location.AmexMerchantId == null ? DBNull.Value : location.AmexMerchantId);
            addCommand.Parameters.AddWithValue("@discovermerchantid", location.DiscoverMerchantId == null ? DBNull.Value : location.DiscoverMerchantId);
            addCommand.Parameters.AddWithValue("@processdiscover", location.ProcessDiscover == null ? DBNull.Value : location.ProcessDiscover);
            addCommand.Parameters.AddWithValue("@acceptdiscover", location.AcceptDiscover == null ? DBNull.Value : location.AcceptDiscover);
            addCommand.Parameters.AddWithValue("@percentswipe", location.PercentSwipe == null ? DBNull.Value : location.PercentSwipe);
            addCommand.Parameters.AddWithValue("@percentkey", location.PercentKey == null ? DBNull.Value : location.PercentKey);
            addCommand.Parameters.AddWithValue("@visamcmerchantid", location.VisamcMerchantId == null ? DBNull.Value : location.VisamcMerchantId);
            addCommand.Parameters.AddWithValue("@pinpadmodel", location.PinpadModel == null ? DBNull.Value : location.PinpadModel);
            addCommand.Parameters.AddWithValue("@acceptamex", location.acceptamex == null ? DBNull.Value : location.acceptamex);
            addCommand.Parameters.AddWithValue("@requestedamount", location.requestedamount == null ? DBNull.Value : location.requestedamount);
            addCommand.Parameters.AddWithValue("@requestedminimum", location.requestedminimum == null ? DBNull.Value : location.requestedminimum);
            addCommand.Parameters.AddWithValue("@intendeduse", location.intendeduse == null ? DBNull.Value : location.intendeduse);
            addCommand.Parameters.AddWithValue("@previousadvance", location.Previousadvance == null ? DBNull.Value : location.Previousadvance);
            addCommand.Parameters.AddWithValue("@previouslender", location.Previouslender == null ? DBNull.Value : location.Previouslender);
            addCommand.Parameters.AddWithValue("@previousactive", location.Previousactive == null ? DBNull.Value : location.Previousactive);
            addCommand.Parameters.AddWithValue("@previousbalance", location.Previousbalance == null ? DBNull.Value : location.Previousbalance);
            addCommand.Parameters.AddWithValue("@ownershiptype", location.Ownershiptype == null ? DBNull.Value : location.Ownershiptype);
            addCommand.Parameters.AddWithValue("@businessformationstate", location.Businessformationstate == null ? DBNull.Value : location.Businessformationstate);
            addCommand.Parameters.AddWithValue("@visamcaccepted", location.Visamcaccepted == null ? DBNull.Value : location.Visamcaccepted);
            addCommand.Parameters.AddWithValue("@visamcprovider", location.Visamcprovider == null ? DBNull.Value : location.Visamcprovider);
            addCommand.Parameters.AddWithValue("@sellingtype", location.Sellingtype == null ? DBNull.Value : location.Sellingtype);
            addCommand.Parameters.AddWithValue("@sellproductarea", location.Sellproductarea == null ? DBNull.Value : location.Sellproductarea);
            addCommand.Parameters.AddWithValue("@goodservicetype", location.Goodservicetype == null ? DBNull.Value : location.Goodservicetype);
            addCommand.Parameters.AddWithValue("@mcccode", location.Mcccode == null ? DBNull.Value : location.Mcccode);
            addCommand.Parameters.AddWithValue("@averagesale", location.Averagesale == null ? DBNull.Value : location.Averagesale);
            addCommand.Parameters.AddWithValue("@averagemcmonth", location.Averagemcmonth == null ? DBNull.Value : location.Averagemcmonth);
            addCommand.Parameters.AddWithValue("@averagemctkt", location.Averagemctkt == null ? DBNull.Value : location.Averagemctkt);
            addCommand.Parameters.AddWithValue("@averagediscmonth", location.Averagediscmonth == null ? DBNull.Value : location.Averagediscmonth);
            addCommand.Parameters.AddWithValue("@averagedisctkt", location.Averagedisctkt == null ? DBNull.Value : location.Averagedisctkt);
            addCommand.Parameters.AddWithValue("@averagediscovermonth", location.Averagediscovermonth == null ? DBNull.Value : location.Averagediscovermonth);
            addCommand.Parameters.AddWithValue("@averageamexmonth", location.Averageamexmonth == null ? DBNull.Value : location.Averageamexmonth);
            addCommand.Parameters.AddWithValue("@averagegrossmonth", location.Averagegrossmonth == null ? DBNull.Value : location.Averagegrossmonth);
            addCommand.Parameters.AddWithValue("@visamcdiscmaxmonthlyvolume", location.Visamcdiscmaxmonthlyvolume == null ? DBNull.Value : location.Visamcdiscmaxmonthlyvolume);
            addCommand.Parameters.AddWithValue("@visamcdiscminavgticket", location.Visamcdiscminavgticket == null ? DBNull.Value : location.Visamcdiscminavgticket);
            addCommand.Parameters.AddWithValue("@visamcdiscmaxavgticket", location.Visamcdiscmaxavgticket == null ? DBNull.Value : location.Visamcdiscmaxavgticket);
            addCommand.Parameters.AddWithValue("@visamcdiscannualvolume", location.Visamcdiscannualvolume == null ? DBNull.Value : location.Visamcdiscannualvolume);
            addCommand.Parameters.AddWithValue("@poscontactname", location.Poscontactname == null ? DBNull.Value : location.Poscontactname);
            addCommand.Parameters.AddWithValue("@poscontactnumber", location.Poscontactnumber == null ? DBNull.Value : location.Poscontactnumber);
            addCommand.Parameters.AddWithValue("@highestsale", location.Highestsale == null ? DBNull.Value : location.Highestsale);
            addCommand.Parameters.AddWithValue("@creditvolume", location.Creditvolume == null ? DBNull.Value : location.Creditvolume);
            addCommand.Parameters.AddWithValue("@numberlocations", location.Numberlocations == null ? DBNull.Value : location.Numberlocations);
            addCommand.Parameters.AddWithValue("@dateopened", location.Dateopened == null ? DBNull.Value : location.Dateopened);
            addCommand.Parameters.AddWithValue("@seasonal", location.Seasonal == null ? DBNull.Value : location.Seasonal);
            addCommand.Parameters.AddWithValue("@rentcurrent", location.Rentcurrent == null ? DBNull.Value : location.Rentcurrent);
            addCommand.Parameters.AddWithValue("@landlordname", location.Landlordname == null ? DBNull.Value : location.Landlordname);
            addCommand.Parameters.AddWithValue("@landlordcontactname", location.Landlordcontactname == null ? DBNull.Value : location.Landlordcontactname);
            addCommand.Parameters.AddWithValue("@landlordphone", location.Landlordphone == null ? DBNull.Value : location.Landlordphone);
            addCommand.Parameters.AddWithValue("@lengthofownershipyear", location.Lengthofownershipyear == null ? DBNull.Value : location.Lengthofownershipyear);
            addCommand.Parameters.AddWithValue("@lengthofownershipmonth", location.Lengthofownershipmonth == null ? DBNull.Value : location.Lengthofownershipmonth);
            addCommand.Parameters.AddWithValue("@seasonalmonths", location.Seasonalmonths == null ? DBNull.Value : location.Seasonalmonths);
            addCommand.Parameters.AddWithValue("@leaseexpirationdate", location.Leaseexpirationdate == null ? DBNull.Value : location.Leaseexpirationdate);
            addCommand.Parameters.AddWithValue("@monthlyrent", location.Monthlyrent == null ? DBNull.Value : location.Monthlyrent);
            addCommand.Parameters.AddWithValue("@acceptinglength", location.Acceptinglength == null ? DBNull.Value : location.Acceptinglength);
            addCommand.Parameters.AddWithValue("@currentprocessor", location.Currentprocessor == null ? DBNull.Value : location.Currentprocessor);
            addCommand.Parameters.AddWithValue("@batchesmonthly", location.Batchesmonthly == null ? DBNull.Value : location.Batchesmonthly);
            addCommand.Parameters.AddWithValue("@isdefault", location.Isdefault == null ? DBNull.Value : location.Isdefault);
            addCommand.Parameters.AddWithValue("@merchantid", location.Merchantid == null ? DBNull.Value : location.Merchantid);
            addCommand.Parameters.AddWithValue("@check21plusmerchantid", location.Check21plusmerchantid == null ? DBNull.Value : location.Check21plusmerchantid);
            addCommand.Parameters.AddWithValue("@check21plusterminalid", location.Check21plusterminalid == null ? DBNull.Value : location.Check21plusterminalid);
            addCommand.Parameters.AddWithValue("@achdebitmerchantid", location.Achdebitmerchantid == null ? DBNull.Value : location.Achdebitmerchantid);
            addCommand.Parameters.AddWithValue("@achdebitterminalid", location.Achdebitterminalid == null ? DBNull.Value : location.Achdebitterminalid);
            addCommand.Parameters.AddWithValue("@checksbyphonemerchantid", location.Checksbyphonemerchantid == null ? DBNull.Value : location.Checksbyphonemerchantid);
            addCommand.Parameters.AddWithValue("@checksbyphoneterminalid", location.Checksbyphoneterminalid == null ? DBNull.Value : location.Checksbyphoneterminalid);
            addCommand.Parameters.AddWithValue("@checksbywebmerchantid", location.Checksbywebmerchantid == null ? DBNull.Value : location.Checksbywebmerchantid);
            addCommand.Parameters.AddWithValue("@checksbywebterminalid", location.Checksbywebterminalid == null ? DBNull.Value : location.Checksbywebterminalid);
            addCommand.Parameters.AddWithValue("@checkcollectmerchantid", location.Checkcollectmerchantid == null ? DBNull.Value : location.Checkcollectmerchantid);
            addCommand.Parameters.AddWithValue("@checkcollectterminalid", location.Checkcollectterminalid == null ? DBNull.Value : location.Checkcollectterminalid);
            addCommand.Parameters.AddWithValue("@ckpapermerchantid", location.Ckpapermerchantid == null ? DBNull.Value : location.Ckpapermerchantid);
            addCommand.Parameters.AddWithValue("@ckpaperterminalid", location.Ckpaperterminalid == null ? DBNull.Value : location.Ckpaperterminalid);
            addCommand.Parameters.AddWithValue("@ckposmerchantid", location.Ckposmerchantid == null ? DBNull.Value : location.Ckposmerchantid);
            addCommand.Parameters.AddWithValue("@ckposterminalid", location.Ckposterminalid == null ? DBNull.Value : location.Ckposterminalid);
            addCommand.Parameters.AddWithValue("@voidflag", location.Voidflag == null ? DBNull.Value : location.Voidflag);
            addCommand.Parameters.AddWithValue("@viewed", location.viewed == null ? DBNull.Value : location.viewed);
            addCommand.Parameters.AddWithValue("@created_at", location.created_at == null ? DBNull.Value : location.created_at);
            addCommand.Parameters.AddWithValue("@updated_at", location.updated_at == null ? DBNull.Value : location.updated_at);
            addCommand.Parameters.AddWithValue("@terminalmodel", location.terminalmodel == null ? DBNull.Value : location.terminalmodel);
            addCommand.Parameters.AddWithValue("@estimatedannualvolume", location.estimatedannualvolume == null ? DBNull.Value : location.estimatedannualvolume);
            addCommand.Parameters.AddWithValue("@estimatedavgticket", location.estimatedavgticket == null ? DBNull.Value : location.estimatedavgticket);
            addCommand.Parameters.AddWithValue("@hasprinter", location.hasprinter == null ? DBNull.Value : location.hasprinter);
            addCommand.Parameters.AddWithValue("@terminalmanufacturerother", location.terminalmanufacturerother == null ? DBNull.Value : location.terminalmanufacturerother);
            addCommand.Parameters.AddWithValue("@haspinpad", location.haspinpad == null ? DBNull.Value : location.haspinpad);
            addCommand.Parameters.AddWithValue("@dialoutphone", location.dialoutphone == null ? DBNull.Value : location.dialoutphone);
            addCommand.Parameters.AddWithValue("@dialoutphoneother", location.dialoutphoneother == null ? DBNull.Value : location.dialoutphoneother);
            addCommand.Parameters.AddWithValue("@printer", location.printer == null ? DBNull.Value : location.printer);
            addCommand.Parameters.AddWithValue("@timeremaininglease", location.timeremaininglease == null ? DBNull.Value : location.timeremaininglease);
            addCommand.Parameters.AddWithValue("@timeremainingleasemonth", location.timeremainingleasemonth == null ? DBNull.Value : location.timeremainingleasemonth);
            addCommand.Parameters.AddWithValue("@previousprocessorterminatedwhom", location.previousprocessorterminatedwhom == null ? DBNull.Value : location.previousprocessorterminatedwhom);
            addCommand.Parameters.AddWithValue("@previousprocessorterminated", location.previousprocessorterminated == null ? DBNull.Value : location.previousprocessorterminated);
            addCommand.Parameters.AddWithValue("@previousprocessorterminatedexplain", location.previousprocessorterminatedexplain == null ? DBNull.Value : location.previousprocessorterminatedexplain);
            addCommand.Parameters.AddWithValue("@previousprocessorterminateddate", location.previousprocessorterminateddate == null ? DBNull.Value : location.previousprocessorterminateddate);
            addCommand.Parameters.AddWithValue("@checkreader", location.checkreader == null ? DBNull.Value : location.checkreader);
            addCommand.Parameters.AddWithValue("@checkreadermodel", location.checkreadermodel == null ? DBNull.Value : location.checkreadermodel);
            addCommand.Parameters.AddWithValue("@bankaccounttype", location.bankaccounttype == null ? DBNull.Value : location.bankaccounttype);
            addCommand.Parameters.AddWithValue("@bankaccounttype2", location.bankaccounttype2 == null ? DBNull.Value : location.bankaccounttype2);
            addCommand.Parameters.AddWithValue("@isbusinessforsale", location.isbusinessforsale == null ? DBNull.Value : location.isbusinessforsale);
            addCommand.Parameters.AddWithValue("@havetaxliens", location.havetaxliens == null ? DBNull.Value : location.havetaxliens);
            addCommand.Parameters.AddWithValue("@merchanttype", location.merchanttype == null ? DBNull.Value : location.merchanttype);
            addCommand.Parameters.AddWithValue("@merchanttypeother", location.merchanttypeother == null ? DBNull.Value : location.merchanttypeother);
            addCommand.Parameters.AddWithValue("@b2bpercent", location.b2bpercent == null ? DBNull.Value : location.b2bpercent);
            addCommand.Parameters.AddWithValue("@b2cpercent", location.b2cpercent == null ? DBNull.Value : location.b2cpercent);
            addCommand.Parameters.AddWithValue("@daysuntildelivery_0_pct", location.daysuntildelivery_0_pct == null ? DBNull.Value : location.daysuntildelivery_0_pct);
            addCommand.Parameters.AddWithValue("@daysuntildelivery_1to3_pct", location.daysuntildelivery_1to3_pct == null ? DBNull.Value : location.daysuntildelivery_1to3_pct);
            addCommand.Parameters.AddWithValue("@daysuntildelivery_4to7_pct", location.daysuntildelivery_4to7_pct == null ? DBNull.Value : location.daysuntildelivery_4to7_pct);
            addCommand.Parameters.AddWithValue("@daysuntildelivery_8to14_pct", location.daysuntildelivery_8to14_pct == null ? DBNull.Value : location.daysuntildelivery_8to14_pct);
            addCommand.Parameters.AddWithValue("@daysuntildelivery_30plus_pct", location.daysuntildelivery_30plus_pct == null ? DBNull.Value : location.daysuntildelivery_30plus_pct);
            addCommand.Parameters.AddWithValue("@daysuntildelivery_total_pct", location.daysuntildelivery_total_pct == null ? DBNull.Value : location.daysuntildelivery_total_pct);
            addCommand.Parameters.AddWithValue("@returnpolicyother", location.returnpolicyother == null ? DBNull.Value : location.returnpolicyother);
            addCommand.Parameters.AddWithValue("@returnpolicy", location.returnpolicy == null ? DBNull.Value : location.returnpolicy);
            addCommand.Parameters.AddWithValue("@mktingmethods", location.mktingmethods == null ? DBNull.Value : location.mktingmethods);
            addCommand.Parameters.AddWithValue("@haveretaillocation", location.haveretaillocation == null ? DBNull.Value : location.haveretaillocation);
            addCommand.Parameters.AddWithValue("@retailswipedpct", location.retailswipedpct == null ? DBNull.Value : location.retailswipedpct);
            addCommand.Parameters.AddWithValue("@retailkeyedpct", location.retailkeyedpct == null ? DBNull.Value : location.retailkeyedpct);
            addCommand.Parameters.AddWithValue("@internetpct", location.internetpct == null ? DBNull.Value : location.internetpct);
            addCommand.Parameters.AddWithValue("@mailorderpct", location.mailorderpct == null ? DBNull.Value : location.mailorderpct);
            addCommand.Parameters.AddWithValue("@totalpct", location.totalpct == null ? DBNull.Value : location.totalpct);
            addCommand.Parameters.AddWithValue("@settlementon", location.settlementon == null ? DBNull.Value : location.settlementon);
            addCommand.Parameters.AddWithValue("@settlementonother", location.settlementonother == null ? DBNull.Value : location.settlementonother);
            addCommand.Parameters.AddWithValue("@recurringcharge", location.recurringcharge == null ? DBNull.Value : location.recurringcharge);
            addCommand.Parameters.AddWithValue("@recurringchargefrequency", location.recurringchargefrequency == null ? DBNull.Value : location.recurringchargefrequency);
            addCommand.Parameters.AddWithValue("@recurringchargeother", location.recurringchargeother == null ? DBNull.Value : location.recurringchargeother);
            addCommand.Parameters.AddWithValue("@cardpaymenton", location.cardpaymenton == null ? DBNull.Value : location.cardpaymenton);
            addCommand.Parameters.AddWithValue("@cardpaymentonother", location.cardpaymentonother == null ? DBNull.Value : location.cardpaymentonother);
            addCommand.Parameters.AddWithValue("@depositrequired", location.depositrequired == null ? DBNull.Value : location.depositrequired);
            addCommand.Parameters.AddWithValue("@depositrequiredamt", location.depositrequiredamt == null ? DBNull.Value : location.depositrequiredamt);
            addCommand.Parameters.AddWithValue("@debitcardsvcs", location.debitcardsvcs == null ? DBNull.Value : location.debitcardsvcs);
            addCommand.Parameters.AddWithValue("@ebtsignup", location.ebtsignup == null ? DBNull.Value : location.ebtsignup);
            addCommand.Parameters.AddWithValue("@ebtnum", location.ebtnum == null ? DBNull.Value : location.ebtnum);
            addCommand.Parameters.AddWithValue("@wexsignup", location.wexsignup == null ? DBNull.Value : location.wexsignup);
            addCommand.Parameters.AddWithValue("@cksvcssignup", location.cksvcssignup == null ? DBNull.Value : location.cksvcssignup);
            addCommand.Parameters.AddWithValue("@giftcardsvcs", location.giftcardsvcs == null ? DBNull.Value : location.giftcardsvcs);
            addCommand.Parameters.AddWithValue("@leasesvcs", location.leasesvcs == null ? DBNull.Value : location.leasesvcs);
            addCommand.Parameters.AddWithValue("@mccreditonly", location.mccreditonly == null ? DBNull.Value : location.mccreditonly);
            addCommand.Parameters.AddWithValue("@mcnonpindebitonly", location.mcnonpindebitonly == null ? DBNull.Value : location.mcnonpindebitonly);
            addCommand.Parameters.AddWithValue("@visacreditonly", location.visacreditonly == null ? DBNull.Value : location.visacreditonly);
            addCommand.Parameters.AddWithValue("@visanonpindebitonly", location.visanonpindebitonly == null ? DBNull.Value : location.visanonpindebitonly);
            addCommand.Parameters.AddWithValue("@disccreditonly", location.disccreditonly == null ? DBNull.Value : location.disccreditonly);
            addCommand.Parameters.AddWithValue("@discnonpindebitonly", location.discnonpindebitonly == null ? DBNull.Value : location.discnonpindebitonly);
            addCommand.Parameters.AddWithValue("@amexsvcreq", location.amexsvcreq == null ? DBNull.Value : location.amexsvcreq);
            addCommand.Parameters.AddWithValue("@amexrate", location.amexrate == null ? DBNull.Value : location.amexrate);
            addCommand.Parameters.AddWithValue("@amexmofee", location.amexmofee == null ? DBNull.Value : location.amexmofee);
            addCommand.Parameters.AddWithValue("@grosspay", location.grosspay == null ? DBNull.Value : location.grosspay);
            addCommand.Parameters.AddWithValue("@axppayfreq", location.axppayfreq == null ? DBNull.Value : location.axppayfreq);
            addCommand.Parameters.AddWithValue("@amexmonvol", location.amexmonvol == null ? DBNull.Value : location.amexmonvol);
            addCommand.Parameters.AddWithValue("@amexavgtkt", location.amexavgtkt == null ? DBNull.Value : location.amexavgtkt);
            addCommand.Parameters.AddWithValue("@termmfg", location.termmfg == null ? DBNull.Value : location.termmfg);
            addCommand.Parameters.AddWithValue("@termmfg2", location.termmfg2 == null ? DBNull.Value : location.termmfg2);
            addCommand.Parameters.AddWithValue("@termmodel", location.termmodel == null ? DBNull.Value : location.termmodel);
            addCommand.Parameters.AddWithValue("@repro", location.repro == null ? DBNull.Value : location.repro);
            addCommand.Parameters.AddWithValue("@multimerch1", location.multimerch1 == null ? DBNull.Value : location.multimerch1);
            addCommand.Parameters.AddWithValue("@autoclose", location.autoclose == null ? DBNull.Value : location.autoclose);
            addCommand.Parameters.AddWithValue("@autoclosetime", location.autoclosetime == null ? DBNull.Value : location.autoclosetime);
            addCommand.Parameters.AddWithValue("@autoclosetime2", location.autoclosetime2 == null ? DBNull.Value : location.autoclosetime2);
            addCommand.Parameters.AddWithValue("@tip", location.tip == null ? DBNull.Value : location.tip);
            addCommand.Parameters.AddWithValue("@tip2", location.tip2 == null ? DBNull.Value : location.tip2);
            addCommand.Parameters.AddWithValue("@usepmtgateway", location.usepmtgateway == null ? DBNull.Value : location.usepmtgateway);
            addCommand.Parameters.AddWithValue("@pmtgateway", location.pmtgateway == null ? DBNull.Value : location.pmtgateway);
            addCommand.Parameters.AddWithValue("@pmtgateway2", location.pmtgateway2 == null ? DBNull.Value : location.pmtgateway2);
            addCommand.Parameters.AddWithValue("@shopcart", location.shopcart == null ? DBNull.Value : location.shopcart);
            addCommand.Parameters.AddWithValue("@software", location.software == null ? DBNull.Value : location.software);
            addCommand.Parameters.AddWithValue("@softwarepcicompliant", location.softwarepcicompliant == null ? DBNull.Value : location.softwarepcicompliant);
            addCommand.Parameters.AddWithValue("@multimerch2", location.multimerch2 == null ? DBNull.Value : location.multimerch2);
            addCommand.Parameters.AddWithValue("@termsn", location.termsn == null ? DBNull.Value : location.termsn);
            addCommand.Parameters.AddWithValue("@termsupplier", location.termsupplier == null ? DBNull.Value : location.termsupplier);
            addCommand.Parameters.AddWithValue("@termsupplier2", location.termsupplier2 == null ? DBNull.Value : location.termsupplier2);
            addCommand.Parameters.AddWithValue("@termsupplier3", location.termsupplier3 == null ? DBNull.Value : location.termsupplier3);
            addCommand.Parameters.AddWithValue("@termsupplier4", location.termsupplier4 == null ? DBNull.Value : location.termsupplier4);
            addCommand.Parameters.AddWithValue("@ship", location.ship == null ? DBNull.Value : location.ship);
            addCommand.Parameters.AddWithValue("@ship1", location.ship1 == null ? DBNull.Value : location.ship1);
            addCommand.Parameters.AddWithValue("@ship2", location.ship2 == null ? DBNull.Value : location.ship2);
            addCommand.Parameters.AddWithValue("@ship3", location.ship3 == null ? DBNull.Value : location.ship3);
            addCommand.Parameters.AddWithValue("@ship4", location.ship4 == null ? DBNull.Value : location.ship4);
            addCommand.Parameters.AddWithValue("@processor", location.processor == null ? DBNull.Value : location.processor);
            addCommand.Parameters.AddWithValue("@processor1", location.processor1 == null ? DBNull.Value : location.processor1);
            addCommand.Parameters.AddWithValue("@dwnload4", location.dwnload4 == null ? DBNull.Value : location.dwnload4);
            addCommand.Parameters.AddWithValue("@dwnload5", location.dwnload5 == null ? DBNull.Value : location.dwnload5);
            addCommand.Parameters.AddWithValue("@leasesvcs1", location.leasesvcs1 == null ? DBNull.Value : location.leasesvcs1);
            addCommand.Parameters.AddWithValue("@leasesvcs3", location.leasesvcs3 == null ? DBNull.Value : location.leasesvcs3);
            addCommand.Parameters.AddWithValue("@leasesvcs4", location.leasesvcs4 == null ? DBNull.Value : location.leasesvcs4);
            addCommand.Parameters.AddWithValue("@leasesvcs5", location.leasesvcs5 == null ? DBNull.Value : location.leasesvcs5);
            addCommand.Parameters.AddWithValue("@wireless", location.wireless == null ? DBNull.Value : location.wireless);
            addCommand.Parameters.AddWithValue("@wireless1", location.wireless1 == null ? DBNull.Value : location.wireless1);
            addCommand.Parameters.AddWithValue("@wireless2", location.wireless2 == null ? DBNull.Value : location.wireless2);
            addCommand.Parameters.AddWithValue("@ethernet", location.ethernet == null ? DBNull.Value : location.ethernet);
            addCommand.Parameters.AddWithValue("@site1", location.site1 == null ? DBNull.Value : location.site1);
            addCommand.Parameters.AddWithValue("@site2", location.site2 == null ? DBNull.Value : location.site2);
            addCommand.Parameters.AddWithValue("@site3", location.site3 == null ? DBNull.Value : location.site3);
            addCommand.Parameters.AddWithValue("@site4", location.site4 == null ? DBNull.Value : location.site4);
            addCommand.Parameters.AddWithValue("@pmtgateway4", location.pmtgateway4 == null ? DBNull.Value : location.pmtgateway4);
            addCommand.Parameters.AddWithValue("@dwnload6", location.dwnload6 == null ? DBNull.Value : location.dwnload6);
            addCommand.Parameters.AddWithValue("@site2a", location.site2a == null ? DBNull.Value : location.site2a);
            addCommand.Parameters.AddWithValue("@thirdprty", location.thirdprty == null ? DBNull.Value : location.thirdprty);
            addCommand.Parameters.AddWithValue("@thirdprty1", location.thirdprty1 == null ? DBNull.Value : location.thirdprty1);
            addCommand.Parameters.AddWithValue("@thirdprty2", location.thirdprty2 == null ? DBNull.Value : location.thirdprty2);
            addCommand.Parameters.AddWithValue("@thirdprty3", location.thirdprty3 == null ? DBNull.Value : location.thirdprty3);
            addCommand.Parameters.AddWithValue("@thirdprty4", location.thirdprty4 == null ? DBNull.Value : location.thirdprty4);
            addCommand.Parameters.AddWithValue("@thirdprty5", location.thirdprty5 == null ? DBNull.Value : location.thirdprty5);
            addCommand.Parameters.AddWithValue("@motofull", location.motofull == null ? DBNull.Value : location.motofull);
            addCommand.Parameters.AddWithValue("@motofullb", location.motofullb == null ? DBNull.Value : location.motofullb);
            addCommand.Parameters.AddWithValue("@motofullc", location.motofullc == null ? DBNull.Value : location.motofullc);
            addCommand.Parameters.AddWithValue("@motofulld", location.motofulld == null ? DBNull.Value : location.motofulld);
            addCommand.Parameters.AddWithValue("@site5", location.site5 == null ? DBNull.Value : location.site5);
            addCommand.Parameters.AddWithValue("@leasessvcs6", location.leasessvcs6 == null ? DBNull.Value : location.leasessvcs6);
            addCommand.Parameters.AddWithValue("@leasesvcs6", location.leasesvcs6 == null ? DBNull.Value : location.leasesvcs6);
            addCommand.Parameters.AddWithValue("@cfgallowdebitcashfbackfee", location.cfgallowdebitcashfbackfee == null ? DBNull.Value : location.cfgallowdebitcashfbackfee);
            addCommand.Parameters.AddWithValue("@cfgmcp", location.cfgmcp == null ? DBNull.Value : location.cfgmcp);
            addCommand.Parameters.AddWithValue("@cfgdcc", location.cfgdcc == null ? DBNull.Value : location.cfgdcc);
            addCommand.Parameters.AddWithValue("@cfgdebitsurcharge", location.cfgdebitsurcharge == null ? DBNull.Value : location.cfgdebitsurcharge);
            addCommand.Parameters.AddWithValue("@cfgdebitsurchargeamount", location.cfgdebitsurchargeamount == null ? DBNull.Value : location.cfgdebitsurchargeamount);
            addCommand.Parameters.AddWithValue("@mobileequipmentbrand", location.mobileequipmentbrand == null ? DBNull.Value : location.mobileequipmentbrand);
            addCommand.Parameters.AddWithValue("@mobileequipmentmodel", location.mobileequipmentmodel == null ? DBNull.Value : location.mobileequipmentmodel);
            addCommand.Parameters.AddWithValue("@ecommercesslused", location.ecommercesslused == null ? DBNull.Value : location.ecommercesslused);
            addCommand.Parameters.AddWithValue("@mobileequipmentbrand2", location.mobileequipmentbrand2 == null ? DBNull.Value : location.mobileequipmentbrand2);
            addCommand.Parameters.AddWithValue("@mobileequipmentmodel2", location.mobileequipmentmodel2 == null ? DBNull.Value : location.mobileequipmentmodel2);
            addCommand.Parameters.AddWithValue("@depositrequiredpct", location.depositrequiredpct == null ? DBNull.Value : location.depositrequiredpct);
            addCommand.Parameters.AddWithValue("@mktingmethodsother", location.mktingmethodsother == null ? DBNull.Value : location.mktingmethodsother);
            addCommand.Parameters.AddWithValue("@bankid", location.bankid == null ? DBNull.Value : location.bankid);
            addCommand.Parameters.AddWithValue("@bankaccountholder", location.bankaccountholder == null ? DBNull.Value : location.bankaccountholder);
            addCommand.Parameters.AddWithValue("@bankaccountholder2", location.bankaccountholder2 == null ? DBNull.Value : location.bankaccountholder2);
            addCommand.Parameters.AddWithValue("@purchaseqty1", location.purchaseqty1 == null ? DBNull.Value : location.purchaseqty1);
            addCommand.Parameters.AddWithValue("@purchaseqty2", location.purchaseqty2 == null ? DBNull.Value : location.purchaseqty2);
            addCommand.Parameters.AddWithValue("@purchaseqty3", location.purchaseqty3 == null ? DBNull.Value : location.purchaseqty3);
            addCommand.Parameters.AddWithValue("@purchasedesc1", location.purchasedesc1 == null ? DBNull.Value : location.purchasedesc1);
            addCommand.Parameters.AddWithValue("@purchasedesc2", location.purchasedesc2 == null ? DBNull.Value : location.purchasedesc2);
            addCommand.Parameters.AddWithValue("@purchasedesc3", location.purchasedesc3 == null ? DBNull.Value : location.purchasedesc3);
            addCommand.Parameters.AddWithValue("@storecreditcards", location.storecreditcards == null ? DBNull.Value : location.storecreditcards);
            addCommand.Parameters.AddWithValue("@paymentoverssl", location.paymentoverssl == null ? DBNull.Value : location.paymentoverssl);
            addCommand.Parameters.AddWithValue("@merchantcertificatenumber", location.merchantcertificatenumber == null ? DBNull.Value : location.merchantcertificatenumber);
            addCommand.Parameters.AddWithValue("@certificateissuer", location.certificateissuer == null ? DBNull.Value : location.certificateissuer);
            addCommand.Parameters.AddWithValue("@certificateexpdate", location.certificateexpdate == null ? DBNull.Value : location.certificateexpdate);
            addCommand.Parameters.AddWithValue("@certificatetype", location.certificatetype == null ? DBNull.Value : location.certificatetype);
            addCommand.Parameters.AddWithValue("@paymentafterdelivery", location.paymentafterdelivery == null ? DBNull.Value : location.paymentafterdelivery);
            addCommand.Parameters.AddWithValue("@whoperformsfulfillment", location.whoperformsfulfillment == null ? DBNull.Value : location.whoperformsfulfillment);
            addCommand.Parameters.AddWithValue("@whoperformsfulfillmentother", location.whoperformsfulfillmentother == null ? DBNull.Value : location.whoperformsfulfillmentother);
            addCommand.Parameters.AddWithValue("@fulfillmentname", location.fulfillmentname == null ? DBNull.Value : location.fulfillmentname);
            addCommand.Parameters.AddWithValue("@fulfillmentcontact", location.fulfillmentcontact == null ? DBNull.Value : location.fulfillmentcontact);
            addCommand.Parameters.AddWithValue("@fulfillmentaddress", location.fulfillmentaddress == null ? DBNull.Value : location.fulfillmentaddress);
            addCommand.Parameters.AddWithValue("@fulfillmentcity", location.fulfillmentcity == null ? DBNull.Value : location.fulfillmentcity);
            addCommand.Parameters.AddWithValue("@fulfillmentstate", location.fulfillmentstate == null ? DBNull.Value : location.fulfillmentstate);
            addCommand.Parameters.AddWithValue("@fulfillmentzipcode", location.fulfillmentzipcode == null ? DBNull.Value : location.fulfillmentzipcode);
            addCommand.Parameters.AddWithValue("@fulfillmentphone", location.fulfillmentphone == null ? DBNull.Value : location.fulfillmentphone);
            addCommand.Parameters.AddWithValue("@deliverymethod", location.deliverymethod == null ? DBNull.Value : location.deliverymethod);
            addCommand.Parameters.AddWithValue("@mktnegativeresponseorauto", location.mktnegativeresponseorauto == null ? DBNull.Value : location.mktnegativeresponseorauto);
            addCommand.Parameters.AddWithValue("@mktnegresponseorautoother", location.mktnegresponseorautoother == null ? DBNull.Value : location.mktnegresponseorautoother);
            addCommand.Parameters.AddWithValue("@fulfillmentpaymentbefore", location.fulfillmentpaymentbefore == null ? DBNull.Value : location.fulfillmentpaymentbefore);
            addCommand.Parameters.AddWithValue("@fulfillmentpaymentbeforedays", location.fulfillmentpaymentbeforedays == null ? DBNull.Value : location.fulfillmentpaymentbeforedays);
            addCommand.Parameters.AddWithValue("@productguarantees", location.productguarantees == null ? DBNull.Value : location.productguarantees);
            addCommand.Parameters.AddWithValue("@debitsalesmonthlyvol", location.debitsalesmonthlyvol == null ? DBNull.Value : location.debitsalesmonthlyvol);
            addCommand.Parameters.AddWithValue("@debitsalesavg", location.debitsalesavg == null ? DBNull.Value : location.debitsalesavg);
            addCommand.Parameters.AddWithValue("@cfgtipfunction", location.cfgtipfunction == null ? DBNull.Value : location.cfgtipfunction);
            addCommand.Parameters.AddWithValue("@cfgdebitautosettle", location.cfgdebitautosettle == null ? DBNull.Value : location.cfgdebitautosettle);
            addCommand.Parameters.AddWithValue("@cfgpasswordprotect", location.cfgpasswordprotect == null ? DBNull.Value : location.cfgpasswordprotect);
            addCommand.Parameters.AddWithValue("@cfgallowdebitcashback", location.cfgallowdebitcashback == null ? DBNull.Value : location.cfgallowdebitcashback);
            addCommand.Parameters.AddWithValue("@cfgdebitautosettletime", location.cfgdebitautosettletime == null ? DBNull.Value : location.cfgdebitautosettletime);
            addCommand.Parameters.AddWithValue("@cfgallowdebitcashfbackee", location.cfgallowdebitcashfbackee == null ? DBNull.Value : location.cfgallowdebitcashfbackee);
            addCommand.Parameters.AddWithValue("@averagevisamonth", location.averagevisamonth == null ? DBNull.Value : location.averagevisamonth);
            addCommand.Parameters.AddWithValue("@averagevisamcmonth", location.averagevisamcmonth == null ? DBNull.Value : location.averagevisamcmonth);
            addCommand.Parameters.AddWithValue("@averagesalevisa", location.averagesalevisa == null ? DBNull.Value : location.averagesalevisa);
            addCommand.Parameters.AddWithValue("@averagesalevisamc", location.averagesalevisamc == null ? DBNull.Value : location.averagesalevisamc);
            addCommand.Parameters.AddWithValue("@amexannvol", location.amexannvol == null ? DBNull.Value : location.amexannvol);
            addCommand.Parameters.AddWithValue("@needpinpad", location.needpinpad == null ? DBNull.Value : location.needpinpad);
            addCommand.Parameters.AddWithValue("@needcheckreader", location.needcheckreader == null ? DBNull.Value : location.needcheckreader);
            addCommand.Parameters.AddWithValue("@supplyingcheckreader", location.supplyingcheckreader == null ? DBNull.Value : location.supplyingcheckreader);
            addCommand.Parameters.AddWithValue("@existingterminalmodel", location.existingterminalmodel == null ? DBNull.Value : location.existingterminalmodel);
            addCommand.Parameters.AddWithValue("@existingterminalmanufacturer", location.existingterminalmanufacturer == null ? DBNull.Value : location.existingterminalmanufacturer);
            addCommand.Parameters.AddWithValue("@existingcheckreadermanu", location.existingcheckreadermanu == null ? DBNull.Value : location.existingcheckreadermanu);
            addCommand.Parameters.AddWithValue("@existingcheckreadermodel", location.existingcheckreadermodel == null ? DBNull.Value : location.existingcheckreadermodel);
            addCommand.Parameters.AddWithValue("@existingpinpadmodel", location.existingpinpadmodel == null ? DBNull.Value : location.existingpinpadmodel);
            addCommand.Parameters.AddWithValue("@totalpercent", location.totalpercent == null ? DBNull.Value : location.totalpercent);
            addCommand.Parameters.AddWithValue("@timezone", location.timezone == null ? DBNull.Value : location.timezone);
            addCommand.Parameters.AddWithValue("@equipmentpaymentby", location.equipmentpaymentby == null ? DBNull.Value : location.equipmentpaymentby);
            addCommand.Parameters.AddWithValue("@equipment_type", location.equipment_type == null ? DBNull.Value : location.equipment_type);
            addCommand.Parameters.AddWithValue("@equipment_type_lease", location.equipment_type_lease == null ? DBNull.Value : location.equipment_type_lease);
            addCommand.Parameters.AddWithValue("@equipment_type_rent", location.equipment_type_rent == null ? DBNull.Value : location.equipment_type_rent);
            addCommand.Parameters.AddWithValue("@equipment_rental_deposit", location.equipment_rental_deposit == null ? DBNull.Value : location.equipment_rental_deposit);
            addCommand.Parameters.AddWithValue("@zone", location.zone == null ? DBNull.Value : location.zone);
            addCommand.Parameters.AddWithValue("@immediatedelivery", location.immediatedelivery == null ? DBNull.Value : location.immediatedelivery);
            addCommand.Parameters.AddWithValue("@sqft", location.sqft == null ? DBNull.Value : location.sqft);
            addCommand.Parameters.AddWithValue("@refundpolicyexist", location.refundpolicyexist == null ? DBNull.Value : location.refundpolicyexist);
            addCommand.Parameters.AddWithValue("@daystosubmittransactions", location.daystosubmittransactions == null ? DBNull.Value : location.daystosubmittransactions);
            addCommand.Parameters.AddWithValue("@owninventory", location.owninventory == null ? DBNull.Value : location.owninventory);
            addCommand.Parameters.AddWithValue("@inventorystoredatlocation", location.inventorystoredatlocation == null ? DBNull.Value : location.inventorystoredatlocation);
            addCommand.Parameters.AddWithValue("@inventorylocation", location.inventorylocation == null ? DBNull.Value : location.inventorylocation);
            addCommand.Parameters.AddWithValue("@warranteeguaranteeoffered", location.warranteeguaranteeoffered == null ? DBNull.Value : location.warranteeguaranteeoffered);
            addCommand.Parameters.AddWithValue("@warranteeguaranteetype", location.warranteeguaranteetype == null ? DBNull.Value : location.warranteeguaranteetype);
            addCommand.Parameters.AddWithValue("@refundtype", location.refundtype == null ? DBNull.Value : location.refundtype);
            addCommand.Parameters.AddWithValue("@refundother", location.refundother == null ? DBNull.Value : location.refundother);
            addCommand.Parameters.AddWithValue("@whoenterscreditcardinfo", location.whoenterscreditcardinfo == null ? DBNull.Value : location.whoenterscreditcardinfo);
            addCommand.Parameters.AddWithValue("@whoenterscreditcardinfoother", location.whoenterscreditcardinfoother == null ? DBNull.Value : location.whoenterscreditcardinfoother);
            addCommand.Parameters.AddWithValue("@pcinameversion", location.pcinameversion == null ? DBNull.Value : location.pcinameversion);
            addCommand.Parameters.AddWithValue("@pciversion", location.pciversion == null ? DBNull.Value : location.pciversion);
            addCommand.Parameters.AddWithValue("@merchantpcicompliant", location.merchantpcicompliant == null ? DBNull.Value : location.merchantpcicompliant);
            addCommand.Parameters.AddWithValue("@namepcicompliant", location.namepcicompliant == null ? DBNull.Value : location.namepcicompliant);
            addCommand.Parameters.AddWithValue("@terminalinsurance", location.terminalinsurance == null ? DBNull.Value : location.terminalinsurance);
            addCommand.Parameters.AddWithValue("@fdglrelationshipcode", location.fdglrelationshipcode == null ? DBNull.Value : location.fdglrelationshipcode);
            addCommand.Parameters.AddWithValue("@billingoptions", location.billingoptions == null ? DBNull.Value : location.billingoptions);
            addCommand.Parameters.AddWithValue("@newamex", location.newamex == null ? DBNull.Value : location.newamex);
            addCommand.Parameters.AddWithValue("@daysuntildelivery_0", location.daysuntildelivery_0 == null ? DBNull.Value : location.daysuntildelivery_0);
            addCommand.Parameters.AddWithValue("@daysuntildelivery_1to3", location.daysuntildelivery_1to3 == null ? DBNull.Value : location.daysuntildelivery_1to3);
            addCommand.Parameters.AddWithValue("@daysuntildelivery_4to7", location.daysuntildelivery_4to7 == null ? DBNull.Value : location.daysuntildelivery_4to7);
            addCommand.Parameters.AddWithValue("@daysuntildelivery_8to14", location.daysuntildelivery_8to14 == null ? DBNull.Value : location.daysuntildelivery_8to14);
            addCommand.Parameters.AddWithValue("@daysuntildelivery_15to30", location.daysuntildelivery_15to30 == null ? DBNull.Value : location.daysuntildelivery_15to30);
            addCommand.Parameters.AddWithValue("@daysuntildelivery_30plus", location.daysuntildelivery_30plus == null ? DBNull.Value : location.daysuntildelivery_30plus);
            addCommand.Parameters.AddWithValue("@daysuntildelivery_15to30_pct", location.daysuntildelivery_15to30_pct == null ? DBNull.Value : location.daysuntildelivery_15to30_pct);
            addCommand.Parameters.AddWithValue("@replacerefund", location.replacerefund == null ? DBNull.Value : location.replacerefund);
            addCommand.Parameters.AddWithValue("@orderfulfillmentother", location.orderfulfillmentother == null ? DBNull.Value : location.orderfulfillmentother);
            addCommand.Parameters.AddWithValue("@zipcodeplus4", location.zipcodeplus4 == null ? DBNull.Value : location.zipcodeplus4);
            addCommand.Parameters.AddWithValue("@fulfillmentzipcodeplus4", location.fulfillmentzipcodeplus4 == null ? DBNull.Value : location.fulfillmentzipcodeplus4);
            addCommand.Parameters.AddWithValue("@addresscivicnumber", location.addresscivicnumber == null ? DBNull.Value : location.addresscivicnumber);
            addCommand.Parameters.AddWithValue("@fulfillmentcivicnumber", location.fulfillmentcivicnumber == null ? DBNull.Value : location.fulfillmentcivicnumber);
            addCommand.Parameters.AddWithValue("@fulfillmentsuite", location.fulfillmentsuite == null ? DBNull.Value : location.fulfillmentsuite);
            addCommand.Parameters.AddWithValue("@facility", location.facility == null ? DBNull.Value : location.facility);
            addCommand.Parameters.AddWithValue("@acceptallcard", location.acceptallcard == null ? DBNull.Value : location.acceptallcard);
            addCommand.Parameters.AddWithValue("@selectedcards", location.selectedcards == null ? DBNull.Value : location.selectedcards);
            addCommand.Parameters.AddWithValue("@amountborrow", location.amountborrow == null ? DBNull.Value : location.amountborrow);
            addCommand.Parameters.AddWithValue("@existcreditcardproc", location.existcreditcardproc == null ? DBNull.Value : location.existcreditcardproc);
            addCommand.Parameters.AddWithValue("@creditcardmerchantid", location.creditcardmerchantid == null ? DBNull.Value : location.creditcardmerchantid);
            addCommand.Parameters.AddWithValue("@creditcardyear", location.creditcardyear == null ? DBNull.Value : location.creditcardyear);
            addCommand.Parameters.AddWithValue("@creditcardmonth", location.creditcardmonth == null ? DBNull.Value : location.creditcardmonth);
            addCommand.Parameters.AddWithValue("@existcheckproc", location.existcheckproc == null ? DBNull.Value : location.existcheckproc);
            addCommand.Parameters.AddWithValue("@checkprocmerchantid", location.checkprocmerchantid == null ? DBNull.Value : location.checkprocmerchantid);
            addCommand.Parameters.AddWithValue("@checkprocyear", location.checkprocyear == null ? DBNull.Value : location.checkprocyear);
            addCommand.Parameters.AddWithValue("@checkprocmonth", location.checkprocmonth == null ? DBNull.Value : location.checkprocmonth);
            addCommand.Parameters.AddWithValue("@avgcheckmonthvol", location.avgcheckmonthvol == null ? DBNull.Value : location.avgcheckmonthvol);
            addCommand.Parameters.AddWithValue("@avgsalecreditcardamt", location.avgsalecreditcardamt == null ? DBNull.Value : location.avgsalecreditcardamt);
            addCommand.Parameters.AddWithValue("@avgsalechekamt", location.avgsalechekamt == null ? DBNull.Value : location.avgsalechekamt);
            addCommand.Parameters.AddWithValue("@cks_account_type", location.cks_account_type == null ? DBNull.Value : location.cks_account_type);
            addCommand.Parameters.AddWithValue("@dba_address", location.dba_address == null ? DBNull.Value : location.dba_address);
            addCommand.Parameters.AddWithValue("@websiteurl", location.websiteurl == null ? DBNull.Value : location.websiteurl);
            addCommand.Parameters.AddWithValue("@websiteencrypt", location.websiteencrypt == null ? DBNull.Value : location.websiteencrypt);
            addCommand.Parameters.AddWithValue("@survey", location.survey == null ? DBNull.Value : location.survey);
            addCommand.Parameters.AddWithValue("@survey_found_merch", location.survey_found_merch == null ? DBNull.Value : location.survey_found_merch);
            addCommand.Parameters.AddWithValue("@site_photo", location.site_photo == null ? DBNull.Value : location.site_photo);
            addCommand.Parameters.AddWithValue("@valid_id", location.valid_id == null ? DBNull.Value : location.valid_id);
            addCommand.Parameters.AddWithValue("@equip_capture", location.equip_capture == null ? DBNull.Value : location.equip_capture);
            addCommand.Parameters.AddWithValue("@connectivity", location.connectivity == null ? DBNull.Value : location.connectivity);
            addCommand.Parameters.AddWithValue("@imprinter", location.imprinter == null ? DBNull.Value : location.imprinter);
            addCommand.Parameters.AddWithValue("@dial_prefix", location.dial_prefix == null ? DBNull.Value : location.dial_prefix);
            addCommand.Parameters.AddWithValue("@equip_provider", location.equip_provider == null ? DBNull.Value : location.equip_provider);
            addCommand.Parameters.AddWithValue("@equip_download", location.equip_download == null ? DBNull.Value : location.equip_download);
            addCommand.Parameters.AddWithValue("@hasloyaltycard", location.hasloyaltycard == null ? DBNull.Value : location.hasloyaltycard);
            addCommand.Parameters.AddWithValue("@maxtktsize", location.maxtktsize == null ? DBNull.Value : location.maxtktsize);
            addCommand.Parameters.AddWithValue("@monthlyvolumerequired", location.monthlyvolumerequired == null ? DBNull.Value : location.monthlyvolumerequired);
            addCommand.Parameters.AddWithValue("@previouscardaccept", location.previouscardaccept == null ? DBNull.Value : location.previouscardaccept);
            addCommand.Parameters.AddWithValue("@previousstatements", location.previousstatements == null ? DBNull.Value : location.previousstatements);
            addCommand.Parameters.AddWithValue("@previousecommerce", location.previousecommerce == null ? DBNull.Value : location.previousecommerce);
            addCommand.Parameters.AddWithValue("@activelyconducting", location.activelyconducting == null ? DBNull.Value : location.activelyconducting);
            addCommand.Parameters.AddWithValue("@activelyconductingexplain", location.activelyconductingexplain == null ? DBNull.Value : location.activelyconductingexplain);
            addCommand.Parameters.AddWithValue("@futuredeliverypct", location.futuredeliverypct == null ? DBNull.Value : location.futuredeliverypct);
            addCommand.Parameters.AddWithValue("@deliveryexplanation", location.deliveryexplanation == null ? DBNull.Value : location.deliveryexplanation);
            addCommand.Parameters.AddWithValue("@shippingmethod", location.shippingmethod == null ? DBNull.Value : location.shippingmethod);
            addCommand.Parameters.AddWithValue("@shipto", location.shipto == null ? DBNull.Value : location.shipto);
            addCommand.Parameters.AddWithValue("@shippingaddress", location.shippingaddress == null ? DBNull.Value : location.shippingaddress);
            addCommand.Parameters.AddWithValue("@inventorysufficient", location.inventorysufficient == null ? DBNull.Value : location.inventorysufficient);
            addCommand.Parameters.AddWithValue("@inventorysufficientexplain", location.inventorysufficientexplain == null ? DBNull.Value : location.inventorysufficientexplain);
            addCommand.Parameters.AddWithValue("@inventoryamount", location.inventoryamount == null ? DBNull.Value : location.inventoryamount);
            addCommand.Parameters.AddWithValue("@inventoryamountexplain", location.inventoryamountexplain == null ? DBNull.Value : location.inventoryamountexplain);
            addCommand.Parameters.AddWithValue("@servicelistedonapp", location.servicelistedonapp == null ? DBNull.Value : location.servicelistedonapp);
            addCommand.Parameters.AddWithValue("@servicelistedonappexplain", location.servicelistedonappexplain == null ? DBNull.Value : location.servicelistedonappexplain);
            addCommand.Parameters.AddWithValue("@inspectedby", location.inspectedby == null ? DBNull.Value : location.inspectedby);
            addCommand.Parameters.AddWithValue("@inboundcalls", location.inboundcalls == null ? DBNull.Value : location.inboundcalls);
            addCommand.Parameters.AddWithValue("@outboundcalls", location.outboundcalls == null ? DBNull.Value : location.outboundcalls);
            addCommand.Parameters.AddWithValue("@ach", location.ach == null ? DBNull.Value : location.ach);
            addCommand.Parameters.AddWithValue("@fein", location.fein == null ? DBNull.Value : location.fein);
            addCommand.Parameters.AddWithValue("@legalentity", location.legalentity == null ? DBNull.Value : location.legalentity);
            addCommand.Parameters.AddWithValue("@leadaccdistinction", location.leadaccdistinction == null ? DBNull.Value : location.leadaccdistinction);
            addCommand.Parameters.AddWithValue("@feetier", location.feetier == null ? DBNull.Value : location.feetier);
            addCommand.Parameters.AddWithValue("@templateid", location.templateid == null ? DBNull.Value : location.templateid);
            addCommand.Parameters.AddWithValue("@bankaccountuseforboth", location.bankaccountuseforboth == null ? DBNull.Value : location.bankaccountuseforboth);
            addCommand.Parameters.AddWithValue("@daysuntildeliveryvol1", location.daysuntildeliveryvol1 == null ? DBNull.Value : location.daysuntildeliveryvol1);
            addCommand.Parameters.AddWithValue("@daysuntildeliveryvol2", location.daysuntildeliveryvol2 == null ? DBNull.Value : location.daysuntildeliveryvol2);
            addCommand.Parameters.AddWithValue("@daysuntildeliveryvol3", location.daysuntildeliveryvol3 == null ? DBNull.Value : location.daysuntildeliveryvol3);
            addCommand.Parameters.AddWithValue("@daysuntildeliverydays1", location.daysuntildeliverydays1 == null ? DBNull.Value : location.daysuntildeliverydays1);
            addCommand.Parameters.AddWithValue("@daysuntildeliverydays2", location.daysuntildeliverydays2 == null ? DBNull.Value : location.daysuntildeliverydays2);
            addCommand.Parameters.AddWithValue("@daysuntildeliverydays3", location.daysuntildeliverydays3 == null ? DBNull.Value : location.daysuntildeliverydays3);
            addCommand.Parameters.AddWithValue("@pciemailaddress", location.pciemailaddress == null ? DBNull.Value : location.pciemailaddress);
            
            #endregion
            int id = (int)addCommand.ExecuteScalar();

            foreach (var document in location.Documents)
            {
                SaveLocationDocument(location.DisplayId, id, document.Name, document.DocumentType, location.Voidflag);
            }
        }
        public void SaveLocationDocument(string dispayId, int locationId, string document, string documentType, string voidFlag)
        {
            Dictionary<string, object> toReturn = new Dictionary<string, object>();
            SqlCommand addCommand = conn.CreateCommand();
            #region sql
            string insertSQL = "INSERT INTO LocationDocuments (displayid, LocationId, document, documentType, voidflag, created_at, updated_at) " +
                "VALUES(@displayid, @LocationId, @document, @documentType, @voidflag, @created_at, @updated_at) SELECT CAST(scope_identity() AS int);";


            addCommand.CommandText = insertSQL;

            addCommand.Parameters.AddWithValue("@displayid", dispayId ?? "");
            addCommand.Parameters.AddWithValue("@LocationId", locationId);
            addCommand.Parameters.AddWithValue("@document", document);
            addCommand.Parameters.AddWithValue("@documentType", documentType);
            addCommand.Parameters.AddWithValue("@voidflag", voidFlag ?? "");
            addCommand.Parameters.AddWithValue("@created_at", DateTime.Now);
            addCommand.Parameters.AddWithValue("@updated_at", DateTime.Now);
            #endregion
            addCommand.ExecuteScalar();
        }

        public Dictionary<string, object> GetLocation(int Id)
        {
            Dictionary<string, object> toReturn = new Dictionary<string, object>();
            SqlCommand selectCommand = conn.CreateCommand();
            #region sql
            string selectSQL = "SELECT * FROM Location WHERE Id = @Id";

            selectCommand.CommandText = selectSQL;
            selectCommand.Parameters.AddWithValue("@Id", Id);

            #endregion
            int ID = (int)selectCommand.ExecuteScalar();

            toReturn.Add("Success", ID);

            return toReturn;
        }
        public Dictionary<string, object> UpdateLocation(dynamic location)
        {
            Dictionary<string, object> toReturn = new Dictionary<string, object>();
            SqlCommand updateCommand = conn.CreateCommand();
            #region sql
            string updateSQL = "UPDATE Location SET displayid = @displayid, customerid = @customerid, name = @name, address = @address, suite = @suite, city = @city," +
                "state = @state, zipcode = @zipcode, phone = @phone, fax = @fax, emailaddress = @emailaddress, ocationtype = @locationtype, " +
                "locationtypeother = @locationtypeother, contactname = @contactname, bankaccountfordeposit = @bankaccountfordeposit, " +
                "bankaccountforwithdrawls = @bankaccountforwithdrawls, bankname = @bankname, bankname2 = @bankname2, bankaddress = @bankaddress, bankcity = @bankcity," +
                "bankcity2 = @bankcity2, bankstate = @bankstate, bankstate2 = @bankstate2, bankzipcode = @bankzipcode, bankzipcode2 = @bankzipcode2, " +
                "bankcontactname = @bankcontactname, bankcontactname2 = @bankcontactname2, bankphone = @bankphone, bankphone2 = @bankphone2, " +
                "bankfax = @bankfax, bankfax2 = @bankfax2, ankrouting = @bankrouting, bankrouting2 = @bankrouting2, bankaccount = @bankaccount, " +
                "bankaccount2 = @bankaccount2, ankauthorizedsignor = @bankauthorizedsignor, ankauthorizedsignor2 = @bankauthorizedsignor2, maillocation = @maillocation, " +
                "needterminal = @needterminal, terminalshippinglocation = @terminalshippinglocation, " +
                "erminalmanufacturer = @terminalmanufacturer, usepos = @usepos, posmanufacturer = @posmanufacturer, osmodel = @posmodel, " +
                "usecheckimager = @usecheckimager, checkimagermanufacturer = @checkimagermanufacturer, checkimagermodel = @checkimagermodel, " +
                "amexmerchantid = @amexmerchantid, discovermerchantid = @discovermerchantid, processdiscover = @processdiscover, acceptdiscover = @acceptdiscover, " +
                "percentswipe = @percentswipe, percentkey = @percentkey, visamcmerchantid = @visamcmerchantid, inpadmodel = @pinpadmodel, acceptamex = @acceptamex, " +
                "equestedamount = @requestedamount, requestedminimum = @requestedminimum, ntendeduse = @intendeduse, previousadvance = @previousadvance, " +
                "previouslender = @previouslender, previousactive = @previousactive, previousbalance = @previousbalance, ownershiptype = @ownershiptype, " +
                "businessformationstate = @businessformationstate, visamcaccepted = @visamcaccepted, visamcprovider = @visamcprovider, sellingtype = @sellingtype, " +
                "sellproductarea = @sellproductarea, goodservicetype = @goodservicetype, mcccode = @mcccode, averagesale = @averagesale, " +
                "averagemcmonth = @averagemcmonth, averagemctkt = @averagemctkt, averagediscmonth = @averagediscmonth, averagedisctkt = @averagedisctkt, " +
                "averagediscovermonth = @averagediscovermonth, averageamexmonth = @averageamexmonth, veragegrossmonth = @averagegrossmonth, " +
                "visamcdiscmaxmonthlyvolume = @visamcdiscmaxmonthlyvolume, " +
                "visamcdiscminavgticket = @visamcdiscminavgticket, visamcdiscmaxavgticket = @visamcdiscmaxavgticket, visamcdiscannualvolume = @visamcdiscannualvolume, " +
                "poscontactname = @poscontactname, poscontactnumber = @poscontactnumber, erchantid = @merchantid, check21plusmerchantid = @check21plusmerchantid, " +
                "check21plusterminalid = @check21plusterminalid, achdebitmerchantid = @achdebitmerchantid, achdebitterminalid = @achdebitterminalid, " +
                "checksbyphonemerchantid = @checksbyphonemerchantid, checksbyphoneterminalid = @checksbyphoneterminalid, checksbywebmerchantid = @checksbywebmerchantid, " +
                "checksbywebterminalid = @checksbywebterminalid, checkcollectmerchantid = @checkcollectmerchantid, checkcollectterminalid = @checkcollectterminalid, " +
                "ckpapermerchantid = @ckpapermerchantid, ckpaperterminalid = @ckpaperterminalid, ckposmerchantid = @ckposmerchantid, ckposterminalid = @ckposterminalid, " +
                "voidflag = @voidflag, viewed = @viewed, created_at = @created_at, updated_at = @updated_at, terminalmodel = @terminalmodel, " +
                "estimatedannualvolume = @estimatedannualvolume, estimatedavgticket = @estimatedavgticket, hasprinter = @hasprinter, " +
                "erminalmanufacturerother = @terminalmanufacturerother, haspinpad = @haspinpad, dialoutphone = @dialoutphone, dialoutphoneother = @dialoutphoneother, " +
                "printer = @printer, timeremaininglease = @timeremaininglease, imeremainingleasemonth = @timeremainingleasemonth, " +
                "previousprocessorterminatedwhom = @previousprocessorterminatedwhom, previousprocessorterminated = @previousprocessorterminated, " +
                "previousprocessorterminatedexplain = @previousprocessorterminatedexplain, previousprocessorterminateddate = @previousprocessorterminateddate, " +
                "checkreader = @checkreader, checkreadermodel = @checkreadermodel, bankaccounttype = @bankaccounttype, bankaccounttype2 = @bankaccounttype2, " +
                "isbusinessforsale = @isbusinessforsale, avetaxliens = @havetaxliens, merchanttype = @merchanttype, merchanttypeother = @merchanttypeother, " +
                "b2bpercent = @b2bpercent, b2cpercent = @b2cpercent, daysuntildelivery_0_pct = @daysuntildelivery_0_pct, " +
                "daysuntildelivery_1to3_pct = @daysuntildelivery_1to3_pct, daysuntildelivery_4to7_pct = @daysuntildelivery_4to7_pct, " +
                "daysuntildelivery_8to14_pct = @daysuntildelivery_8to14_pct, aysuntildelivery_30plus_pct = @daysuntildelivery_30plus_pct, " +
                "daysuntildelivery_total_pct = @daysuntildelivery_total_pct, returnpolicyother = @returnpolicyother, returnpolicy = @returnpolicy, " +
                "mktingmethods = @mktingmethods, haveretaillocation = @haveretaillocation, retailswipedpct = @retailswipedpct, retailkeyedpct = @retailkeyedpct, " +
                "internetpct = @internetpct, mailorderpct = @mailorderpct, totalpct = @totalpct, settlementon = @settlementon, settlementonother = @settlementonother, " +
                "recurringcharge = @recurringcharge, recurringchargefrequency = @recurringchargefrequency, recurringchargeother = @recurringchargeother, cardpaymenton = @cardpaymenton, " +
                "cardpaymentonother = @cardpaymentonother, depositrequired = @depositrequired, depositrequiredamt = @depositrequiredamt, " +
                "debitcardsvcs = @debitcardsvcs, ebtsignup = @ebtsignup, ebtnum = @ebtnum, wexsignup = @wexsignup, cksvcssignup = @cksvcssignup, " +
                "giftcardsvcs = @giftcardsvcs, leasesvcs = @leasesvcs, mccreditonly = @mccreditonly, mcnonpindebitonly = @mcnonpindebitonly, " +
                "visacreditonly = @visacreditonly, visanonpindebitonly = @visanonpindebitonly, disccreditonly = @disccreditonly, " +
                "discnonpindebitonly = @discnonpindebitonly, amexsvcreq = @amexsvcreq, amexrate = @amexrate, amexmofee = @amexmofee, " +
                "grosspay = @grosspay, axppayfreq = @axppayfreq, amexmonvol = @amexmonvol, amexavgtkt = @amexavgtkt, termmfg = @termmfg, " +
                "termmfg2 = @termmfg2, termmodel = @termmodel, repro = @repro, multimerch1 = @multimerch1, utoclose = @autoclose, " +
                "autoclosetime = @autoclosetime, autoclosetime2 = @autoclosetime2, tip = @tip, tip2 = @tip2, usepmtgateway = @usepmtgateway, " +
                "pmtgateway = @pmtgateway, pmtgateway2 = @pmtgateway2, shopcart = @shopcart, software = @software," +
                "softwarepcicompliant = @softwarepcicompliant, multimerch2 = @multimerch2, termsn = @termsn, " +
                "termsupplier = @termsupplier, termsupplier2 = @termsupplier2, termsupplier3 = @termsupplier3, termsupplier4 = @termsupplier4, " +
                "ship = @ship, ship1 = @ship1, ship2 = @ship2, ship3 = @ship3, ship4 = @ship4, rocessor = @processor, processor1 = @processor1, " +
                "dwnload4 = @dwnload4, dwnload5 = @dwnload5, leasesvcs1 = @leasesvcs1, leasesvcs3 = @leasesvcs3, leasesvcs4 = @leasesvcs4, " +
                "leasesvcs5 = @leasesvcs5, wireless = @wireless, ireless1 = @wireless1, ireless2 = @wireless2, ethernet = @ethernet, site1 = @site1, " +
                "site2 = @site2, site3 = @site3, site4 = @site4, pmtgateway4 = @pmtgateway4, dwnload6 = @dwnload6, site2a = @site2a, thirdprty = @thirdprty, " +
                "thirdprty1 = @thirdprty1, thirdprty2 = @thirdprty2, thirdprty3 = @thirdprty3, thirdprty4 = @thirdprty4, thirdprty5 = @thirdprty5, " +
                "motofull = @motofull, motofullb = @motofullb, motofullc = @motofullc, motofulld = @motofulld, site5 = @site5, leasessvcs6 = @leasessvcs6, " +
                "leasesvcs6 = @leasesvcs6, cfgallowdebitcashfbackfee = @cfgallowdebitcashfbackfee, cfgmcp = @cfgmcp, cfgdcc = @cfgdcc, " +
                "cfgdebitsurcharge = @cfgdebitsurcharge, cfgdebitsurchargeamount = @cfgdebitsurchargeamount, mobileequipmentbrand = @mobileequipmentbrand, " +
                "mobileequipmentmodel = @mobileequipmentmodel, ecommercesslused = @ecommercesslused, mobileequipmentbrand2 = @mobileequipmentbrand2, " +
                "mobileequipmentmodel2 = @mobileequipmentmodel2, depositrequiredpct = @depositrequiredpct, mktingmethodsother = @mktingmethodsother, " +
                "bankid = @bankid, bankaccountholder = @bankaccountholder, ankaccountholder2 = @bankaccountholder2, purchaseqty1 = @purchaseqty1, " +
                "purchaseqty2 = @purchaseqty2, purchaseqty3 = @purchaseqty3, purchasedesc1 = @purchasedesc1, purchasedesc2 = @purchasedesc2, " +
                "purchasedesc3 = @purchasedesc3, storecreditcards = @storecreditcards, paymentoverssl = @paymentoverssl, " +
                "merchantcertificatenumber = @merchantcertificatenumber, certificateissuer = @certificateissuer, certificateexpdate = @certificateexpdate, " +
                "certificatetype = @certificatetype, paymentafterdelivery = @paymentafterdelivery, whoperformsfulfillment = @whoperformsfulfillment, " +
                "whoperformsfulfillmentother = @whoperformsfulfillmentother, ulfillmentname = @fulfillmentname, fulfillmentcontact = @fulfillmentcontact, " +
                "fulfillmentaddress = @fulfillmentaddress, fulfillmentcity = @fulfillmentcity, fulfillmentstate = @fulfillmentstate, " +
                "fulfillmentzipcode = @fulfillmentzipcode, fulfillmentphone = @fulfillmentphone, deliverymethod = @deliverymethod, " +
                "mktnegativeresponseorauto = @mktnegativeresponseorauto, mktnegresponseorautoother = @mktnegresponseorautoother, " +
                "fulfillmentpaymentbefore = @fulfillmentpaymentbefore, fulfillmentpaymentbeforedays = @fulfillmentpaymentbeforedays, " +
                "productguarantees = @productguarantees, debitsalesmonthlyvol = @debitsalesmonthlyvol, debitsalesavg = @debitsalesavg, " +
                "cfgtipfunction = @cfgtipfunction, cfgdebitautosettle = @cfgdebitautosettle, cfgpasswordprotect = @cfgpasswordprotect, " +
                "cfgallowdebitcashback = @cfgallowdebitcashback, cfgdebitautosettletime = @cfgdebitautosettletime, " +
                "cfgallowdebitcashfbackee = @cfgallowdebitcashfbackee, averagevisamonth = @averagevisamonth, averagevisamcmonth = @averagevisamcmonth, " +
                "averagesalevisa = @averagesalevisa, averagesalevisamc = @averagesalevisamc, amexannvol = @amexannvol, needpinpad = @needpinpad, " +
                "needcheckreader = @needcheckreader, supplyingcheckreader = @supplyingcheckreader, " +
                "existingterminalmodel = @existingterminalmodel, existingterminalmanufacturer = @existingterminalmanufacturer, " +
                "existingcheckreadermanu = @existingcheckreadermanu, existingcheckreadermodel = @existingcheckreadermodel, " +
                "existingpinpadmodel = @existingpinpadmodel, totalpercent = @totalpercent, timezone = @timezone, " +
                "equipmentpaymentby = @equipmentpaymentby, equipment_type = @equipment_type, equipment_type_lease = @equipment_type_lease, " +
                "equipment_type_rent = @equipment_type_rent, equipment_rental_deposit = @equipment_rental_deposit, zone = @zone, " +
                "immediatedelivery = @immediatedelivery, sqft = @sqft, refundpolicyexist = @refundpolicyexist, daystosubmittransactions = @daystosubmittransactions, " +
                "owninventory = @owninventory, inventorystoredatlocation = @inventorystoredatlocation, inventorylocation = @inventorylocation, " +
                "warranteeguaranteeoffered = @warranteeguaranteeoffered, warranteeguaranteetype = @warranteeguaranteetype, refundtype = @refundtype, " +
                "refundother = @refundother, whoenterscreditcardinfo = @whoenterscreditcardinfo, whoenterscreditcardinfoother = @whoenterscreditcardinfoother, " +
                "pcinameversion = @pcinameversion, pciversion = @pciversion, merchantpcicompliant = @merchantpcicompliant, namepcicompliant = @namepcicompliant, " +
                "terminalinsurance = @terminalinsurance, fdglrelationshipcode = @fdglrelationshipcode, billingoptions = @billingoptions, newamex = @newamex, " +
                "daysuntildelivery_0 = @daysuntildelivery_0, daysuntildelivery_1to3 = @daysuntildelivery_1to3, daysuntildelivery_4to7 = @daysuntildelivery_4to7, " +
                "daysuntildelivery_8to14 = @daysuntildelivery_8to14, daysuntildelivery_15to30 = @daysuntildelivery_15to30, " +
                "daysuntildelivery_30plus = @daysuntildelivery_30plus, aysuntildelivery_15to30_pct = @daysuntildelivery_15to30_pct, replacerefund = @replacerefund, " +
                "orderfulfillmentother = @orderfulfillmentother, zipcodeplus4 = @zipcodeplus4, fulfillmentzipcodeplus4 = @fulfillmentzipcodeplus4, " +
                "addresscivicnumber = @addresscivicnumber, ulfillmentcivicnumber = @fulfillmentcivicnumber, fulfillmentsuite = @fulfillmentsuite, facility = @facility, " +
                "acceptallcard = @acceptallcard, selectedcards = @selectedcards, amountborrow = @amountborrow, xistcreditcardproc = @existcreditcardproc, " +
                "reditcardmerchantid = @creditcardmerchantid, creditcardyear = @creditcardyear, creditcardmonth = @creditcardmonth, existcheckproc = @existcheckproc, " +
                "checkprocmerchantid = @checkprocmerchantid, checkprocyear = @checkprocyear, checkprocmonth = @checkprocmonth, avgcheckmonthvol = @avgcheckmonthvol, " +
                "avgsalecreditcardamt = @avgsalecreditcardamt, vgsalechekamt = @avgsalechekamt, cks_account_type = @cks_account_type, dba_address = @dba_address, " +
                "websiteurl = @websiteurl, websiteencrypt = @websiteencrypt, survey = @survey, survey_found_merch = @survey_found_merch, site_photo = @site_photo, valid_id = @valid_id, " +
                "equip_capture = @equip_capture, connectivity = @connectivity, imprinter = @imprinter, dial_prefix = @dial_prefix, equip_provider = @equip_provider, " +
                "equip_download = @equip_download, hasloyaltycard = @hasloyaltycard, " +
                "maxtktsize = @maxtktsize, monthlyvolumerequired = @monthlyvolumerequired, previouscardaccept = @previouscardaccept, " +
                "previousstatements = @previousstatements, previousecommerce = @previousecommerce, activelyconducting = @activelyconducting, " +
                "activelyconductingexplain = @activelyconductingexplain, uturedeliverypct = @futuredeliverypct, deliveryexplanation = @deliveryexplanation, " +
                "shippingmethod = @shippingmethod, shipto = @shipto, shippingaddress = @shippingaddress, inventorysufficient = @inventorysufficient, " +
                "inventorysufficientexplain = @inventorysufficientexplain, inventoryamount = @inventoryamount, inventoryamountexplain = @inventoryamountexplain, " +
                "servicelistedonapp = @servicelistedonapp, servicelistedonappexplain = @servicelistedonappexplain, inspectedby = @inspectedby, " +
                "inboundcalls = @inboundcalls, outboundcalls = @outboundcalls, ach = @ach, fein = @fein, legalentity = @legalentity, " +
                "leadaccdistinction = @leadaccdistinction, feetier = @feetier, templateid = @templateid, bankaccountuseforboth = @bankaccountuseforboth, " +
                "daysuntildeliveryvol1 = @daysuntildeliveryvol1, daysuntildeliveryvol2 = @daysuntildeliveryvol2, daysuntildeliveryvol3 = @daysuntildeliveryvol3, " +
                "daysuntildeliverydays1 = @daysuntildeliverydays1, daysuntildeliverydays2 = @daysuntildeliverydays2, daysuntildeliverydays3 = @daysuntildeliverydays3, pciemailaddress = @pciemailaddress " +
                "WHERE ID =  @ID";


            updateCommand.CommandText = updateSQL;
            updateCommand.Parameters.AddWithValue("@ID", utilityManager.TryGetProperty(location, "Id"));
            updateCommand.Parameters.AddWithValue("@displayid", location.DisplayId == null ? DBNull.Value : location.DisplayId);
            updateCommand.Parameters.AddWithValue("@customerid", location.CustomerId == null ? DBNull.Value : location.CustomerId);
            updateCommand.Parameters.AddWithValue("@name", location.Name == null ? DBNull.Value : location.Name);
            updateCommand.Parameters.AddWithValue("@address", location.Address == null ? DBNull.Value : location.Address);
            updateCommand.Parameters.AddWithValue("@suite", location.Suite == null ? DBNull.Value : location.Suite);
            updateCommand.Parameters.AddWithValue("@city", location.City == null ? DBNull.Value : location.City);
            updateCommand.Parameters.AddWithValue("@state", location.State == null ? DBNull.Value : location.State);
            updateCommand.Parameters.AddWithValue("@zipcode", location.ZipCode == null ? DBNull.Value : location.ZipCode);
            updateCommand.Parameters.AddWithValue("@phone", location.Phone == null ? DBNull.Value : location.Phone);
            updateCommand.Parameters.AddWithValue("@fax", location.Fax == null ? DBNull.Value : location.Fax);
            updateCommand.Parameters.AddWithValue("@emailaddress", location.Emailaddress == null ? DBNull.Value : location.Emailaddress);
            updateCommand.Parameters.AddWithValue("@locationtype", location.Locationtype == null ? DBNull.Value : location.Locationtype);
            updateCommand.Parameters.AddWithValue("@locationtypeother", location.Locationtypeother == null ? DBNull.Value : location.Locationtypeother);
            updateCommand.Parameters.AddWithValue("@contactname", location.Contactname == null ? DBNull.Value : location.Contactname);
            updateCommand.Parameters.AddWithValue("@bankaccountfordeposit", location.BankAccountfordeposit == null ? DBNull.Value : location.BankAccountfordeposit);
            updateCommand.Parameters.AddWithValue("@bankaccountforwithdrawls", location.BankAccountforwithdrawls == null ? DBNull.Value : location.BankAccountforwithdrawls);
            updateCommand.Parameters.AddWithValue("@bankname", location.Bankname == null ? DBNull.Value : location.Bankname);
            updateCommand.Parameters.AddWithValue("@bankname2", location.Bankname2 == null ? DBNull.Value : location.Bankname2);
            updateCommand.Parameters.AddWithValue("@bankaddress", location.Bankaddress == null ? DBNull.Value : location.Bankaddress);
            updateCommand.Parameters.AddWithValue("@bankcity", location.Bankcity == null ? DBNull.Value : location.Bankcity);
            updateCommand.Parameters.AddWithValue("@bankcity2", location.Bankcity2 == null ? DBNull.Value : location.Bankcity2);
            updateCommand.Parameters.AddWithValue("@bankstate", location.Bankstate == null ? DBNull.Value : location.Bankstate);
            updateCommand.Parameters.AddWithValue("@bankstate2", location.Bankstate2 == null ? DBNull.Value : location.Bankstate2);
            updateCommand.Parameters.AddWithValue("@bankzipcode", location.Bankzipcode == null ? DBNull.Value : location.Bankzipcode);
            updateCommand.Parameters.AddWithValue("@bankzipcode2", location.Bankzipcode2 == null ? DBNull.Value : location.Bankzipcode2);
            updateCommand.Parameters.AddWithValue("@bankcontactname", location.Bankcontactname == null ? DBNull.Value : location.Bankcontactname);
            updateCommand.Parameters.AddWithValue("@bankcontactname2", location.Bankcontactname2 == null ? DBNull.Value : location.Bankcontactname2);
            updateCommand.Parameters.AddWithValue("@bankphone", location.Bankphone == null ? DBNull.Value : location.Bankphone);
            updateCommand.Parameters.AddWithValue("@bankphone2", location.Bankphone2 == null ? DBNull.Value : location.Bankphone2);
            updateCommand.Parameters.AddWithValue("@bankfax", location.Bankfax == null ? DBNull.Value : location.Bankfax);
            updateCommand.Parameters.AddWithValue("@bankfax2", location.Bankfax2 == null ? DBNull.Value : location.Bankfax2);
            updateCommand.Parameters.AddWithValue("@bankrouting", location.Bankrouting == null ? DBNull.Value : location.Bankrouting);
            updateCommand.Parameters.AddWithValue("@bankrouting2", location.Bankrouting2 == null ? DBNull.Value : location.Bankrouting2);
            updateCommand.Parameters.AddWithValue("@bankaccount", location.Bankaccount == null ? DBNull.Value : location.Bankaccount);
            updateCommand.Parameters.AddWithValue("@bankaccount2", location.Bankaccount2 == null ? DBNull.Value : location.Bankaccount2);
            updateCommand.Parameters.AddWithValue("@bankauthorizedsignor", location.Bankauthorizedsignor == null ? DBNull.Value : location.Bankauthorizedsignor);
            updateCommand.Parameters.AddWithValue("@bankauthorizedsignor2", location.Bankauthorizedsignor2 == null ? DBNull.Value : location.Bankauthorizedsignor2);
            updateCommand.Parameters.AddWithValue("@maillocation", location.Maillocation == null ? DBNull.Value : location.Maillocation);
            updateCommand.Parameters.AddWithValue("@needterminal", location.Needterminal == null ? DBNull.Value : location.Needterminal);
            updateCommand.Parameters.AddWithValue("@terminalshippinglocation", location.Terminalshippinglocation == null ? DBNull.Value : location.Terminalshippinglocation);
            updateCommand.Parameters.AddWithValue("@terminalmanufacturer", location.Terminalmanufacturer == null ? DBNull.Value : location.Terminalmanufacturer);
            updateCommand.Parameters.AddWithValue("@usepos", location.Usepos == null ? DBNull.Value : location.Usepos);
            updateCommand.Parameters.AddWithValue("@posmanufacturer", location.Posmanufacturer == null ? DBNull.Value : location.Posmanufacturer);
            updateCommand.Parameters.AddWithValue("@posmodel", location.PosModel == null ? DBNull.Value : location.PosModel);
            updateCommand.Parameters.AddWithValue("@usecheckimager", location.UseCheckImager == null ? DBNull.Value : location.UseCheckImager);
            updateCommand.Parameters.AddWithValue("@checkimagermanufacturer", location.CheckImagermanufacturer == null ? DBNull.Value : location.CheckImagermanufacturer);
            updateCommand.Parameters.AddWithValue("@checkimagermodel", location.CheckImagermodel == null ? DBNull.Value : location.CheckImagermodel);
            updateCommand.Parameters.AddWithValue("@amexmerchantid", location.AmexMerchantId == null ? DBNull.Value : location.AmexMerchantId);
            updateCommand.Parameters.AddWithValue("@discovermerchantid", location.DiscoverMerchantId == null ? DBNull.Value : location.DiscoverMerchantId);
            updateCommand.Parameters.AddWithValue("@processdiscover", location.ProcessDiscover == null ? DBNull.Value : location.ProcessDiscover);
            updateCommand.Parameters.AddWithValue("@acceptdiscover", location.AcceptDiscover == null ? DBNull.Value : location.AcceptDiscover);
            updateCommand.Parameters.AddWithValue("@percentswipe", location.PercentSwipe == null ? DBNull.Value : location.PercentSwipe);
            updateCommand.Parameters.AddWithValue("@percentkey", location.PercentKey == null ? DBNull.Value : location.PercentKey);
            updateCommand.Parameters.AddWithValue("@visamcmerchantid", location.VisamcMerchantId == null ? DBNull.Value : location.VisamcMerchantId);
            updateCommand.Parameters.AddWithValue("@pinpadmodel", location.PinpadModel == null ? DBNull.Value : location.PinpadModel);
            updateCommand.Parameters.AddWithValue("@acceptamex", location.acceptamex == null ? DBNull.Value : location.acceptamex);
            updateCommand.Parameters.AddWithValue("@requestedamount", location.requestedamount == null ? DBNull.Value : location.requestedamount);
            updateCommand.Parameters.AddWithValue("@requestedminimum", location.requestedminimum == null ? DBNull.Value : location.requestedminimum);
            updateCommand.Parameters.AddWithValue("@intendeduse", location.intendeduse == null ? DBNull.Value : location.intendeduse);
            updateCommand.Parameters.AddWithValue("@previousadvance", location.Previousadvance == null ? DBNull.Value : location.Previousadvance);
            updateCommand.Parameters.AddWithValue("@previouslender", location.Previouslender == null ? DBNull.Value : location.Previouslender);
            updateCommand.Parameters.AddWithValue("@previousactive", location.Previousactive == null ? DBNull.Value : location.Previousactive);
            updateCommand.Parameters.AddWithValue("@previousbalance", location.Previousbalance == null ? DBNull.Value : location.Previousbalance);
            updateCommand.Parameters.AddWithValue("@ownershiptype", location.Ownershiptype == null ? DBNull.Value : location.Ownershiptype);
            updateCommand.Parameters.AddWithValue("@businessformationstate", location.Businessformationstate == null ? DBNull.Value : location.Businessformationstate);
            updateCommand.Parameters.AddWithValue("@visamcaccepted", location.Visamcaccepted == null ? DBNull.Value : location.Visamcaccepted);
            updateCommand.Parameters.AddWithValue("@visamcprovider", location.Visamcprovider == null ? DBNull.Value : location.Visamcprovider);
            updateCommand.Parameters.AddWithValue("@sellingtype", location.Sellingtype == null ? DBNull.Value : location.Sellingtype);
            updateCommand.Parameters.AddWithValue("@sellproductarea", location.Sellproductarea == null ? DBNull.Value : location.Sellproductarea);
            updateCommand.Parameters.AddWithValue("@goodservicetype", location.Goodservicetype == null ? DBNull.Value : location.Goodservicetype);
            updateCommand.Parameters.AddWithValue("@mcccode", location.Mcccode == null ? DBNull.Value : location.Mcccode);
            updateCommand.Parameters.AddWithValue("@averagesale", location.Averagesale == null ? DBNull.Value : location.Averagesale);
            updateCommand.Parameters.AddWithValue("@averagemcmonth", location.Averagemcmonth == null ? DBNull.Value : location.Averagemcmonth);
            updateCommand.Parameters.AddWithValue("@averagemctkt", location.Averagemctkt == null ? DBNull.Value : location.Averagemctkt);
            updateCommand.Parameters.AddWithValue("@averagediscmonth", location.Averagediscmonth == null ? DBNull.Value : location.Averagediscmonth);
            updateCommand.Parameters.AddWithValue("@averagedisctkt", location.Averagedisctkt == null ? DBNull.Value : location.Averagedisctkt);
            updateCommand.Parameters.AddWithValue("@averagediscovermonth", location.Averagediscovermonth == null ? DBNull.Value : location.Averagediscovermonth);
            updateCommand.Parameters.AddWithValue("@averageamexmonth", location.Averageamexmonth == null ? DBNull.Value : location.Averageamexmonth);
            updateCommand.Parameters.AddWithValue("@averagegrossmonth", location.Averagegrossmonth == null ? DBNull.Value : location.Averagegrossmonth);
            updateCommand.Parameters.AddWithValue("@visamcdiscmaxmonthlyvolume", location.Visamcdiscmaxmonthlyvolume == null ? DBNull.Value : location.Visamcdiscmaxmonthlyvolume);
            updateCommand.Parameters.AddWithValue("@visamcdiscminavgticket", location.Visamcdiscminavgticket == null ? DBNull.Value : location.Visamcdiscminavgticket);
            updateCommand.Parameters.AddWithValue("@visamcdiscmaxavgticket", location.Visamcdiscmaxavgticket == null ? DBNull.Value : location.Visamcdiscmaxavgticket);
            updateCommand.Parameters.AddWithValue("@visamcdiscannualvolume", location.Visamcdiscannualvolume == null ? DBNull.Value : location.Visamcdiscannualvolume);
            updateCommand.Parameters.AddWithValue("@poscontactname", location.Poscontactname == null ? DBNull.Value : location.Poscontactname);
            updateCommand.Parameters.AddWithValue("@poscontactnumber", location.Poscontactnumber == null ? DBNull.Value : location.Poscontactnumber);
            updateCommand.Parameters.AddWithValue("@highestsale", location.Highestsale == null ? DBNull.Value : location.Highestsale);
            updateCommand.Parameters.AddWithValue("@creditvolume", location.Creditvolume == null ? DBNull.Value : location.Creditvolume);
            updateCommand.Parameters.AddWithValue("@numberlocations", location.Numberlocations == null ? DBNull.Value : location.Numberlocations);
            updateCommand.Parameters.AddWithValue("@dateopened", location.Dateopened == null ? DBNull.Value : location.Dateopened);
            updateCommand.Parameters.AddWithValue("@seasonal", location.Seasonal == null ? DBNull.Value : location.Seasonal);
            updateCommand.Parameters.AddWithValue("@rentcurrent", location.Rentcurrent == null ? DBNull.Value : location.Rentcurrent);
            updateCommand.Parameters.AddWithValue("@landlordname", location.Landlordname == null ? DBNull.Value : location.Landlordname);
            updateCommand.Parameters.AddWithValue("@landlordcontactname", location.Landlordcontactname == null ? DBNull.Value : location.Landlordcontactname);
            updateCommand.Parameters.AddWithValue("@landlordphone", location.Landlordphone == null ? DBNull.Value : location.Landlordphone);
            updateCommand.Parameters.AddWithValue("@lengthofownershipyear", location.Lengthofownershipyear == null ? DBNull.Value : location.Lengthofownershipyear);
            updateCommand.Parameters.AddWithValue("@lengthofownershipmonth", location.Lengthofownershipmonth == null ? DBNull.Value : location.Lengthofownershipmonth);
            updateCommand.Parameters.AddWithValue("@seasonalmonths", location.Seasonalmonths == null ? DBNull.Value : location.Seasonalmonths);
            updateCommand.Parameters.AddWithValue("@leaseexpirationdate", location.Leaseexpirationdate == null ? DBNull.Value : location.Leaseexpirationdate);
            updateCommand.Parameters.AddWithValue("@monthlyrent", location.Monthlyrent == null ? DBNull.Value : location.Monthlyrent);
            updateCommand.Parameters.AddWithValue("@acceptinglength", location.Acceptinglength == null ? DBNull.Value : location.Acceptinglength);
            updateCommand.Parameters.AddWithValue("@currentprocessor", location.Currentprocessor == null ? DBNull.Value : location.Currentprocessor);
            updateCommand.Parameters.AddWithValue("@batchesmonthly", location.Batchesmonthly == null ? DBNull.Value : location.Batchesmonthly);
            updateCommand.Parameters.AddWithValue("@isdefault", location.Isdefault == null ? DBNull.Value : location.Isdefault);
            updateCommand.Parameters.AddWithValue("@merchantid", location.Merchantid == null ? DBNull.Value : location.Merchantid);
            updateCommand.Parameters.AddWithValue("@check21plusmerchantid", location.Check21plusmerchantid == null ? DBNull.Value : location.Check21plusmerchantid);
            updateCommand.Parameters.AddWithValue("@check21plusterminalid", location.Check21plusterminalid == null ? DBNull.Value : location.Check21plusterminalid);
            updateCommand.Parameters.AddWithValue("@achdebitmerchantid", location.Achdebitmerchantid == null ? DBNull.Value : location.Achdebitmerchantid);
            updateCommand.Parameters.AddWithValue("@achdebitterminalid", location.Achdebitterminalid == null ? DBNull.Value : location.Achdebitterminalid);
            updateCommand.Parameters.AddWithValue("@checksbyphonemerchantid", location.Checksbyphonemerchantid == null ? DBNull.Value : location.Checksbyphonemerchantid);
            updateCommand.Parameters.AddWithValue("@checksbyphoneterminalid", location.Checksbyphoneterminalid == null ? DBNull.Value : location.Checksbyphoneterminalid);
            updateCommand.Parameters.AddWithValue("@checksbywebmerchantid", location.Checksbywebmerchantid == null ? DBNull.Value : location.Checksbywebmerchantid);
            updateCommand.Parameters.AddWithValue("@checksbywebterminalid", location.Checksbywebterminalid == null ? DBNull.Value : location.Checksbywebterminalid);
            updateCommand.Parameters.AddWithValue("@checkcollectmerchantid", location.Checkcollectmerchantid == null ? DBNull.Value : location.Checkcollectmerchantid);
            updateCommand.Parameters.AddWithValue("@checkcollectterminalid", location.Checkcollectterminalid == null ? DBNull.Value : location.Checkcollectterminalid);
            updateCommand.Parameters.AddWithValue("@ckpapermerchantid", location.Ckpapermerchantid == null ? DBNull.Value : location.Ckpapermerchantid);
            updateCommand.Parameters.AddWithValue("@ckpaperterminalid", location.Ckpaperterminalid == null ? DBNull.Value : location.Ckpaperterminalid);
            updateCommand.Parameters.AddWithValue("@ckposmerchantid", location.Ckposmerchantid == null ? DBNull.Value : location.Ckposmerchantid);
            updateCommand.Parameters.AddWithValue("@ckposterminalid", location.Ckposterminalid == null ? DBNull.Value : location.Ckposterminalid);
            updateCommand.Parameters.AddWithValue("@voidflag", location.Voidflag == null ? DBNull.Value : location.Voidflag);
            updateCommand.Parameters.AddWithValue("@viewed", location.viewed == null ? DBNull.Value : location.viewed);
            updateCommand.Parameters.AddWithValue("@created_at", location.created_at == null ? DBNull.Value : location.created_at);
            updateCommand.Parameters.AddWithValue("@updated_at", location.updated_at == null ? DBNull.Value : location.updated_at);
            updateCommand.Parameters.AddWithValue("@terminalmodel", location.terminalmodel == null ? DBNull.Value : location.terminalmodel);
            updateCommand.Parameters.AddWithValue("@estimatedannualvolume", location.estimatedannualvolume == null ? DBNull.Value : location.estimatedannualvolume);
            updateCommand.Parameters.AddWithValue("@estimatedavgticket", location.estimatedavgticket == null ? DBNull.Value : location.estimatedavgticket);
            updateCommand.Parameters.AddWithValue("@hasprinter", location.hasprinter == null ? DBNull.Value : location.hasprinter);
            updateCommand.Parameters.AddWithValue("@terminalmanufacturerother", location.terminalmanufacturerother == null ? DBNull.Value : location.terminalmanufacturerother);
            updateCommand.Parameters.AddWithValue("@haspinpad", location.haspinpad == null ? DBNull.Value : location.haspinpad);
            updateCommand.Parameters.AddWithValue("@dialoutphone", location.dialoutphone == null ? DBNull.Value : location.dialoutphone);
            updateCommand.Parameters.AddWithValue("@dialoutphoneother", location.dialoutphoneother == null ? DBNull.Value : location.dialoutphoneother);
            updateCommand.Parameters.AddWithValue("@printer", location.printer == null ? DBNull.Value : location.printer);
            updateCommand.Parameters.AddWithValue("@timeremaininglease", location.timeremaininglease == null ? DBNull.Value : location.timeremaininglease);
            updateCommand.Parameters.AddWithValue("@timeremainingleasemonth", location.timeremainingleasemonth == null ? DBNull.Value : location.timeremainingleasemonth);
            updateCommand.Parameters.AddWithValue("@previousprocessorterminatedwhom", location.previousprocessorterminatedwhom == null ? DBNull.Value : location.previousprocessorterminatedwhom);
            updateCommand.Parameters.AddWithValue("@previousprocessorterminated", location.previousprocessorterminated == null ? DBNull.Value : location.previousprocessorterminated);
            updateCommand.Parameters.AddWithValue("@previousprocessorterminatedexplain", location.previousprocessorterminatedexplain == null ? DBNull.Value : location.previousprocessorterminatedexplain);
            updateCommand.Parameters.AddWithValue("@previousprocessorterminateddate", location.previousprocessorterminateddate == null ? DBNull.Value : location.previousprocessorterminateddate);
            updateCommand.Parameters.AddWithValue("@checkreader", location.checkreader == null ? DBNull.Value : location.checkreader);
            updateCommand.Parameters.AddWithValue("@checkreadermodel", location.checkreadermodel == null ? DBNull.Value : location.checkreadermodel);
            updateCommand.Parameters.AddWithValue("@bankaccounttype", location.bankaccounttype == null ? DBNull.Value : location.bankaccounttype);
            updateCommand.Parameters.AddWithValue("@bankaccounttype2", location.bankaccounttype2 == null ? DBNull.Value : location.bankaccounttype2);
            updateCommand.Parameters.AddWithValue("@isbusinessforsale", location.isbusinessforsale == null ? DBNull.Value : location.isbusinessforsale);
            updateCommand.Parameters.AddWithValue("@havetaxliens", location.havetaxliens == null ? DBNull.Value : location.havetaxliens);
            updateCommand.Parameters.AddWithValue("@merchanttype", location.merchanttype == null ? DBNull.Value : location.merchanttype);
            updateCommand.Parameters.AddWithValue("@merchanttypeother", location.merchanttypeother == null ? DBNull.Value : location.merchanttypeother);
            updateCommand.Parameters.AddWithValue("@b2bpercent", location.b2bpercent == null ? DBNull.Value : location.b2bpercent);
            updateCommand.Parameters.AddWithValue("@b2cpercent", location.b2cpercent == null ? DBNull.Value : location.b2cpercent);
            updateCommand.Parameters.AddWithValue("@daysuntildelivery_0_pct", location.daysuntildelivery_0_pct == null ? DBNull.Value : location.daysuntildelivery_0_pct);
            updateCommand.Parameters.AddWithValue("@daysuntildelivery_1to3_pct", location.daysuntildelivery_1to3_pct == null ? DBNull.Value : location.daysuntildelivery_1to3_pct);
            updateCommand.Parameters.AddWithValue("@daysuntildelivery_4to7_pct", location.daysuntildelivery_4to7_pct == null ? DBNull.Value : location.daysuntildelivery_4to7_pct);
            updateCommand.Parameters.AddWithValue("@daysuntildelivery_8to14_pct", location.daysuntildelivery_8to14_pct == null ? DBNull.Value : location.daysuntildelivery_8to14_pct);
            updateCommand.Parameters.AddWithValue("@daysuntildelivery_30plus_pct", location.daysuntildelivery_30plus_pct == null ? DBNull.Value : location.daysuntildelivery_30plus_pct);
            updateCommand.Parameters.AddWithValue("@daysuntildelivery_total_pct", location.daysuntildelivery_total_pct == null ? DBNull.Value : location.daysuntildelivery_total_pct);
            updateCommand.Parameters.AddWithValue("@returnpolicyother", location.returnpolicyother == null ? DBNull.Value : location.returnpolicyother);
            updateCommand.Parameters.AddWithValue("@returnpolicy", location.returnpolicy == null ? DBNull.Value : location.returnpolicy);
            updateCommand.Parameters.AddWithValue("@mktingmethods", location.mktingmethods == null ? DBNull.Value : location.mktingmethods);
            updateCommand.Parameters.AddWithValue("@haveretaillocation", location.haveretaillocation == null ? DBNull.Value : location.haveretaillocation);
            updateCommand.Parameters.AddWithValue("@retailswipedpct", location.retailswipedpct == null ? DBNull.Value : location.retailswipedpct);
            updateCommand.Parameters.AddWithValue("@retailkeyedpct", location.retailkeyedpct == null ? DBNull.Value : location.retailkeyedpct);
            updateCommand.Parameters.AddWithValue("@internetpct", location.internetpct == null ? DBNull.Value : location.internetpct);
            updateCommand.Parameters.AddWithValue("@mailorderpct", location.mailorderpct == null ? DBNull.Value : location.mailorderpct);
            updateCommand.Parameters.AddWithValue("@totalpct", location.totalpct == null ? DBNull.Value : location.totalpct);
            updateCommand.Parameters.AddWithValue("@settlementon", location.settlementon == null ? DBNull.Value : location.settlementon);
            updateCommand.Parameters.AddWithValue("@settlementonother", location.settlementonother == null ? DBNull.Value : location.settlementonother);
            updateCommand.Parameters.AddWithValue("@recurringcharge", location.recurringcharge == null ? DBNull.Value : location.recurringcharge);
            updateCommand.Parameters.AddWithValue("@recurringchargefrequency", location.recurringchargefrequency == null ? DBNull.Value : location.recurringchargefrequency);
            updateCommand.Parameters.AddWithValue("@recurringchargeother", location.recurringchargeother == null ? DBNull.Value : location.recurringchargeother);
            updateCommand.Parameters.AddWithValue("@cardpaymenton", location.cardpaymenton == null ? DBNull.Value : location.cardpaymenton);
            updateCommand.Parameters.AddWithValue("@cardpaymentonother", location.cardpaymentonother == null ? DBNull.Value : location.cardpaymentonother);
            updateCommand.Parameters.AddWithValue("@depositrequired", location.depositrequired == null ? DBNull.Value : location.depositrequired);
            updateCommand.Parameters.AddWithValue("@depositrequiredamt", location.depositrequiredamt == null ? DBNull.Value : location.depositrequiredamt);
            updateCommand.Parameters.AddWithValue("@debitcardsvcs", location.debitcardsvcs == null ? DBNull.Value : location.debitcardsvcs);
            updateCommand.Parameters.AddWithValue("@ebtsignup", location.ebtsignup == null ? DBNull.Value : location.ebtsignup);
            updateCommand.Parameters.AddWithValue("@ebtnum", location.ebtnum == null ? DBNull.Value : location.ebtnum);
            updateCommand.Parameters.AddWithValue("@wexsignup", location.wexsignup == null ? DBNull.Value : location.wexsignup);
            updateCommand.Parameters.AddWithValue("@cksvcssignup", location.cksvcssignup == null ? DBNull.Value : location.cksvcssignup);
            updateCommand.Parameters.AddWithValue("@giftcardsvcs", location.giftcardsvcs == null ? DBNull.Value : location.giftcardsvcs);
            updateCommand.Parameters.AddWithValue("@leasesvcs", location.leasesvcs == null ? DBNull.Value : location.leasesvcs);
            updateCommand.Parameters.AddWithValue("@mccreditonly", location.mccreditonly == null ? DBNull.Value : location.mccreditonly);
            updateCommand.Parameters.AddWithValue("@mcnonpindebitonly", location.mcnonpindebitonly == null ? DBNull.Value : location.mcnonpindebitonly);
            updateCommand.Parameters.AddWithValue("@visacreditonly", location.visacreditonly == null ? DBNull.Value : location.visacreditonly);
            updateCommand.Parameters.AddWithValue("@visanonpindebitonly", location.visanonpindebitonly == null ? DBNull.Value : location.visanonpindebitonly);
            updateCommand.Parameters.AddWithValue("@disccreditonly", location.disccreditonly == null ? DBNull.Value : location.disccreditonly);
            updateCommand.Parameters.AddWithValue("@discnonpindebitonly", location.discnonpindebitonly == null ? DBNull.Value : location.discnonpindebitonly);
            updateCommand.Parameters.AddWithValue("@amexsvcreq", location.amexsvcreq == null ? DBNull.Value : location.amexsvcreq);
            updateCommand.Parameters.AddWithValue("@amexrate", location.amexrate == null ? DBNull.Value : location.amexrate);
            updateCommand.Parameters.AddWithValue("@amexmofee", location.amexmofee == null ? DBNull.Value : location.amexmofee);
            updateCommand.Parameters.AddWithValue("@grosspay", location.grosspay == null ? DBNull.Value : location.grosspay);
            updateCommand.Parameters.AddWithValue("@axppayfreq", location.axppayfreq == null ? DBNull.Value : location.axppayfreq);
            updateCommand.Parameters.AddWithValue("@amexmonvol", location.amexmonvol == null ? DBNull.Value : location.amexmonvol);
            updateCommand.Parameters.AddWithValue("@amexavgtkt", location.amexavgtkt == null ? DBNull.Value : location.amexavgtkt);
            updateCommand.Parameters.AddWithValue("@termmfg", location.termmfg == null ? DBNull.Value : location.termmfg);
            updateCommand.Parameters.AddWithValue("@termmfg2", location.termmfg2 == null ? DBNull.Value : location.termmfg2);
            updateCommand.Parameters.AddWithValue("@termmodel", location.termmodel == null ? DBNull.Value : location.termmodel);
            updateCommand.Parameters.AddWithValue("@repro", location.repro == null ? DBNull.Value : location.repro);
            updateCommand.Parameters.AddWithValue("@multimerch1", location.multimerch1 == null ? DBNull.Value : location.multimerch1);
            updateCommand.Parameters.AddWithValue("@autoclose", location.autoclose == null ? DBNull.Value : location.autoclose);
            updateCommand.Parameters.AddWithValue("@autoclosetime", location.autoclosetime == null ? DBNull.Value : location.autoclosetime);
            updateCommand.Parameters.AddWithValue("@autoclosetime2", location.autoclosetime2 == null ? DBNull.Value : location.autoclosetime2);
            updateCommand.Parameters.AddWithValue("@tip", location.tip == null ? DBNull.Value : location.tip);
            updateCommand.Parameters.AddWithValue("@tip2", location.tip2 == null ? DBNull.Value : location.tip2);
            updateCommand.Parameters.AddWithValue("@usepmtgateway", location.usepmtgateway == null ? DBNull.Value : location.usepmtgateway);
            updateCommand.Parameters.AddWithValue("@pmtgateway", location.pmtgateway == null ? DBNull.Value : location.pmtgateway);
            updateCommand.Parameters.AddWithValue("@pmtgateway2", location.pmtgateway2 == null ? DBNull.Value : location.pmtgateway2);
            updateCommand.Parameters.AddWithValue("@shopcart", location.shopcart == null ? DBNull.Value : location.shopcart);
            updateCommand.Parameters.AddWithValue("@software", location.software == null ? DBNull.Value : location.software);
            updateCommand.Parameters.AddWithValue("@softwarepcicompliant", location.softwarepcicompliant == null ? DBNull.Value : location.softwarepcicompliant);
            updateCommand.Parameters.AddWithValue("@multimerch2", location.multimerch2 == null ? DBNull.Value : location.multimerch2);
            updateCommand.Parameters.AddWithValue("@termsn", location.termsn == null ? DBNull.Value : location.termsn);
            updateCommand.Parameters.AddWithValue("@termsupplier", location.termsupplier == null ? DBNull.Value : location.termsupplier);
            updateCommand.Parameters.AddWithValue("@termsupplier2", location.termsupplier2 == null ? DBNull.Value : location.termsupplier2);
            updateCommand.Parameters.AddWithValue("@termsupplier3", location.termsupplier3 == null ? DBNull.Value : location.termsupplier3);
            updateCommand.Parameters.AddWithValue("@termsupplier4", location.termsupplier4 == null ? DBNull.Value : location.termsupplier4);
            updateCommand.Parameters.AddWithValue("@ship", location.ship == null ? DBNull.Value : location.ship);
            updateCommand.Parameters.AddWithValue("@ship1", location.ship1 == null ? DBNull.Value : location.ship1);
            updateCommand.Parameters.AddWithValue("@ship2", location.ship2 == null ? DBNull.Value : location.ship2);
            updateCommand.Parameters.AddWithValue("@ship3", location.ship3 == null ? DBNull.Value : location.ship3);
            updateCommand.Parameters.AddWithValue("@ship4", location.ship4 == null ? DBNull.Value : location.ship4);
            updateCommand.Parameters.AddWithValue("@processor", location.processor == null ? DBNull.Value : location.processor);
            updateCommand.Parameters.AddWithValue("@processor1", location.processor1 == null ? DBNull.Value : location.processor1);
            updateCommand.Parameters.AddWithValue("@dwnload4", location.dwnload4 == null ? DBNull.Value : location.dwnload4);
            updateCommand.Parameters.AddWithValue("@dwnload5", location.dwnload5 == null ? DBNull.Value : location.dwnload5);
            updateCommand.Parameters.AddWithValue("@leasesvcs1", location.leasesvcs1 == null ? DBNull.Value : location.leasesvcs1);
            updateCommand.Parameters.AddWithValue("@leasesvcs3", location.leasesvcs3 == null ? DBNull.Value : location.leasesvcs3);
            updateCommand.Parameters.AddWithValue("@leasesvcs4", location.leasesvcs4 == null ? DBNull.Value : location.leasesvcs4);
            updateCommand.Parameters.AddWithValue("@leasesvcs5", location.leasesvcs5 == null ? DBNull.Value : location.leasesvcs5);
            updateCommand.Parameters.AddWithValue("@wireless", location.wireless == null ? DBNull.Value : location.wireless);
            updateCommand.Parameters.AddWithValue("@wireless1", location.wireless1 == null ? DBNull.Value : location.wireless1);
            updateCommand.Parameters.AddWithValue("@wireless2", location.wireless2 == null ? DBNull.Value : location.wireless2);
            updateCommand.Parameters.AddWithValue("@ethernet", location.ethernet == null ? DBNull.Value : location.ethernet);
            updateCommand.Parameters.AddWithValue("@site1", location.site1 == null ? DBNull.Value : location.site1);
            updateCommand.Parameters.AddWithValue("@site2", location.site2 == null ? DBNull.Value : location.site2);
            updateCommand.Parameters.AddWithValue("@site3", location.site3 == null ? DBNull.Value : location.site3);
            updateCommand.Parameters.AddWithValue("@site4", location.site4 == null ? DBNull.Value : location.site4);
            updateCommand.Parameters.AddWithValue("@pmtgateway4", location.pmtgateway4 == null ? DBNull.Value : location.pmtgateway4);
            updateCommand.Parameters.AddWithValue("@dwnload6", location.dwnload6 == null ? DBNull.Value : location.dwnload6);
            updateCommand.Parameters.AddWithValue("@site2a", location.site2a == null ? DBNull.Value : location.site2a);
            updateCommand.Parameters.AddWithValue("@thirdprty", location.thirdprty == null ? DBNull.Value : location.thirdprty);
            updateCommand.Parameters.AddWithValue("@thirdprty1", location.thirdprty1 == null ? DBNull.Value : location.thirdprty1);
            updateCommand.Parameters.AddWithValue("@thirdprty2", location.thirdprty2 == null ? DBNull.Value : location.thirdprty2);
            updateCommand.Parameters.AddWithValue("@thirdprty3", location.thirdprty3 == null ? DBNull.Value : location.thirdprty3);
            updateCommand.Parameters.AddWithValue("@thirdprty4", location.thirdprty4 == null ? DBNull.Value : location.thirdprty4);
            updateCommand.Parameters.AddWithValue("@thirdprty5", location.thirdprty5 == null ? DBNull.Value : location.thirdprty5);
            updateCommand.Parameters.AddWithValue("@motofull", location.motofull == null ? DBNull.Value : location.motofull);
            updateCommand.Parameters.AddWithValue("@motofullb", location.motofullb == null ? DBNull.Value : location.motofullb);
            updateCommand.Parameters.AddWithValue("@motofullc", location.motofullc == null ? DBNull.Value : location.motofullc);
            updateCommand.Parameters.AddWithValue("@motofulld", location.motofulld == null ? DBNull.Value : location.motofulld);
            updateCommand.Parameters.AddWithValue("@site5", location.site5 == null ? DBNull.Value : location.site5);
            updateCommand.Parameters.AddWithValue("@leasessvcs6", location.leasessvcs6 == null ? DBNull.Value : location.leasessvcs6);
            updateCommand.Parameters.AddWithValue("@leasesvcs6", location.leasesvcs6 == null ? DBNull.Value : location.leasesvcs6);
            updateCommand.Parameters.AddWithValue("@cfgallowdebitcashfbackfee", location.cfgallowdebitcashfbackfee == null ? DBNull.Value : location.cfgallowdebitcashfbackfee);
            updateCommand.Parameters.AddWithValue("@cfgmcp", location.cfgmcp == null ? DBNull.Value : location.cfgmcp);
            updateCommand.Parameters.AddWithValue("@cfgdcc", location.cfgdcc == null ? DBNull.Value : location.cfgdcc);
            updateCommand.Parameters.AddWithValue("@cfgdebitsurcharge", location.cfgdebitsurcharge == null ? DBNull.Value : location.cfgdebitsurcharge);
            updateCommand.Parameters.AddWithValue("@cfgdebitsurchargeamount", location.cfgdebitsurchargeamount == null ? DBNull.Value : location.cfgdebitsurchargeamount);
            updateCommand.Parameters.AddWithValue("@mobileequipmentbrand", location.mobileequipmentbrand == null ? DBNull.Value : location.mobileequipmentbrand);
            updateCommand.Parameters.AddWithValue("@mobileequipmentmodel", location.mobileequipmentmodel == null ? DBNull.Value : location.mobileequipmentmodel);
            updateCommand.Parameters.AddWithValue("@ecommercesslused", location.ecommercesslused == null ? DBNull.Value : location.ecommercesslused);
            updateCommand.Parameters.AddWithValue("@mobileequipmentbrand2", location.mobileequipmentbrand2 == null ? DBNull.Value : location.mobileequipmentbrand2);
            updateCommand.Parameters.AddWithValue("@mobileequipmentmodel2", location.mobileequipmentmodel2 == null ? DBNull.Value : location.mobileequipmentmodel2);
            updateCommand.Parameters.AddWithValue("@depositrequiredpct", location.depositrequiredpct == null ? DBNull.Value : location.depositrequiredpct);
            updateCommand.Parameters.AddWithValue("@mktingmethodsother", location.mktingmethodsother == null ? DBNull.Value : location.mktingmethodsother);
            updateCommand.Parameters.AddWithValue("@bankid", location.bankid == null ? DBNull.Value : location.bankid);
            updateCommand.Parameters.AddWithValue("@bankaccountholder", location.bankaccountholder == null ? DBNull.Value : location.bankaccountholder);
            updateCommand.Parameters.AddWithValue("@bankaccountholder2", location.bankaccountholder2 == null ? DBNull.Value : location.bankaccountholder2);
            updateCommand.Parameters.AddWithValue("@purchaseqty1", location.purchaseqty1 == null ? DBNull.Value : location.purchaseqty1);
            updateCommand.Parameters.AddWithValue("@purchaseqty2", location.purchaseqty2 == null ? DBNull.Value : location.purchaseqty2);
            updateCommand.Parameters.AddWithValue("@purchaseqty3", location.purchaseqty3 == null ? DBNull.Value : location.purchaseqty3);
            updateCommand.Parameters.AddWithValue("@purchasedesc1", location.purchasedesc1 == null ? DBNull.Value : location.purchasedesc1);
            updateCommand.Parameters.AddWithValue("@purchasedesc2", location.purchasedesc2 == null ? DBNull.Value : location.purchasedesc2);
            updateCommand.Parameters.AddWithValue("@purchasedesc3", location.purchasedesc3 == null ? DBNull.Value : location.purchasedesc3);
            updateCommand.Parameters.AddWithValue("@storecreditcards", location.storecreditcards == null ? DBNull.Value : location.storecreditcards);
            updateCommand.Parameters.AddWithValue("@paymentoverssl", location.paymentoverssl == null ? DBNull.Value : location.paymentoverssl);
            updateCommand.Parameters.AddWithValue("@merchantcertificatenumber", location.merchantcertificatenumber == null ? DBNull.Value : location.merchantcertificatenumber);
            updateCommand.Parameters.AddWithValue("@certificateissuer", location.certificateissuer == null ? DBNull.Value : location.certificateissuer);
            updateCommand.Parameters.AddWithValue("@certificateexpdate", location.certificateexpdate == null ? DBNull.Value : location.certificateexpdate);
            updateCommand.Parameters.AddWithValue("@certificatetype", location.certificatetype == null ? DBNull.Value : location.certificatetype);
            updateCommand.Parameters.AddWithValue("@paymentafterdelivery", location.paymentafterdelivery == null ? DBNull.Value : location.paymentafterdelivery);
            updateCommand.Parameters.AddWithValue("@whoperformsfulfillment", location.whoperformsfulfillment == null ? DBNull.Value : location.whoperformsfulfillment);
            updateCommand.Parameters.AddWithValue("@whoperformsfulfillmentother", location.whoperformsfulfillmentother == null ? DBNull.Value : location.whoperformsfulfillmentother);
            updateCommand.Parameters.AddWithValue("@fulfillmentname", location.fulfillmentname == null ? DBNull.Value : location.fulfillmentname);
            updateCommand.Parameters.AddWithValue("@fulfillmentcontact", location.fulfillmentcontact == null ? DBNull.Value : location.fulfillmentcontact);
            updateCommand.Parameters.AddWithValue("@fulfillmentaddress", location.fulfillmentaddress == null ? DBNull.Value : location.fulfillmentaddress);
            updateCommand.Parameters.AddWithValue("@fulfillmentcity", location.fulfillmentcity == null ? DBNull.Value : location.fulfillmentcity);
            updateCommand.Parameters.AddWithValue("@fulfillmentstate", location.fulfillmentstate == null ? DBNull.Value : location.fulfillmentstate);
            updateCommand.Parameters.AddWithValue("@fulfillmentzipcode", location.fulfillmentzipcode == null ? DBNull.Value : location.fulfillmentzipcode);
            updateCommand.Parameters.AddWithValue("@fulfillmentphone", location.fulfillmentphone == null ? DBNull.Value : location.fulfillmentphone);
            updateCommand.Parameters.AddWithValue("@deliverymethod", location.deliverymethod == null ? DBNull.Value : location.deliverymethod);
            updateCommand.Parameters.AddWithValue("@mktnegativeresponseorauto", location.mktnegativeresponseorauto == null ? DBNull.Value : location.mktnegativeresponseorauto);
            updateCommand.Parameters.AddWithValue("@mktnegresponseorautoother", location.mktnegresponseorautoother == null ? DBNull.Value : location.mktnegresponseorautoother);
            updateCommand.Parameters.AddWithValue("@fulfillmentpaymentbefore", location.fulfillmentpaymentbefore == null ? DBNull.Value : location.fulfillmentpaymentbefore);
            updateCommand.Parameters.AddWithValue("@fulfillmentpaymentbeforedays", location.fulfillmentpaymentbeforedays == null ? DBNull.Value : location.fulfillmentpaymentbeforedays);
            updateCommand.Parameters.AddWithValue("@productguarantees", location.productguarantees == null ? DBNull.Value : location.productguarantees);
            updateCommand.Parameters.AddWithValue("@debitsalesmonthlyvol", location.debitsalesmonthlyvol == null ? DBNull.Value : location.debitsalesmonthlyvol);
            updateCommand.Parameters.AddWithValue("@debitsalesavg", location.debitsalesavg == null ? DBNull.Value : location.debitsalesavg);
            updateCommand.Parameters.AddWithValue("@cfgtipfunction", location.cfgtipfunction == null ? DBNull.Value : location.cfgtipfunction);
            updateCommand.Parameters.AddWithValue("@cfgdebitautosettle", location.cfgdebitautosettle == null ? DBNull.Value : location.cfgdebitautosettle);
            updateCommand.Parameters.AddWithValue("@cfgpasswordprotect", location.cfgpasswordprotect == null ? DBNull.Value : location.cfgpasswordprotect);
            updateCommand.Parameters.AddWithValue("@cfgallowdebitcashback", location.cfgallowdebitcashback == null ? DBNull.Value : location.cfgallowdebitcashback);
            updateCommand.Parameters.AddWithValue("@cfgdebitautosettletime", location.cfgdebitautosettletime == null ? DBNull.Value : location.cfgdebitautosettletime);
            updateCommand.Parameters.AddWithValue("@cfgallowdebitcashfbackee", location.cfgallowdebitcashfbackee == null ? DBNull.Value : location.cfgallowdebitcashfbackee);
            updateCommand.Parameters.AddWithValue("@averagevisamonth", location.averagevisamonth == null ? DBNull.Value : location.averagevisamonth);
            updateCommand.Parameters.AddWithValue("@averagevisamcmonth", location.averagevisamcmonth == null ? DBNull.Value : location.averagevisamcmonth);
            updateCommand.Parameters.AddWithValue("@averagesalevisa", location.averagesalevisa == null ? DBNull.Value : location.averagesalevisa);
            updateCommand.Parameters.AddWithValue("@averagesalevisamc", location.averagesalevisamc == null ? DBNull.Value : location.averagesalevisamc);
            updateCommand.Parameters.AddWithValue("@amexannvol", location.amexannvol == null ? DBNull.Value : location.amexannvol);
            updateCommand.Parameters.AddWithValue("@needpinpad", location.needpinpad == null ? DBNull.Value : location.needpinpad);
            updateCommand.Parameters.AddWithValue("@needcheckreader", location.needcheckreader == null ? DBNull.Value : location.needcheckreader);
            updateCommand.Parameters.AddWithValue("@supplyingcheckreader", location.supplyingcheckreader == null ? DBNull.Value : location.supplyingcheckreader);
            updateCommand.Parameters.AddWithValue("@existingterminalmodel", location.existingterminalmodel == null ? DBNull.Value : location.existingterminalmodel);
            updateCommand.Parameters.AddWithValue("@existingterminalmanufacturer", location.existingterminalmanufacturer == null ? DBNull.Value : location.existingterminalmanufacturer);
            updateCommand.Parameters.AddWithValue("@existingcheckreadermanu", location.existingcheckreadermanu == null ? DBNull.Value : location.existingcheckreadermanu);
            updateCommand.Parameters.AddWithValue("@existingcheckreadermodel", location.existingcheckreadermodel == null ? DBNull.Value : location.existingcheckreadermodel);
            updateCommand.Parameters.AddWithValue("@existingpinpadmodel", location.existingpinpadmodel == null ? DBNull.Value : location.existingpinpadmodel);
            updateCommand.Parameters.AddWithValue("@totalpercent", location.totalpercent == null ? DBNull.Value : location.totalpercent);
            updateCommand.Parameters.AddWithValue("@timezone", location.timezone == null ? DBNull.Value : location.timezone);
            updateCommand.Parameters.AddWithValue("@equipmentpaymentby", location.equipmentpaymentby == null ? DBNull.Value : location.equipmentpaymentby);
            updateCommand.Parameters.AddWithValue("@equipment_type", location.equipment_type == null ? DBNull.Value : location.equipment_type);
            updateCommand.Parameters.AddWithValue("@equipment_type_lease", location.equipment_type_lease == null ? DBNull.Value : location.equipment_type_lease);
            updateCommand.Parameters.AddWithValue("@equipment_type_rent", location.equipment_type_rent == null ? DBNull.Value : location.equipment_type_rent);
            updateCommand.Parameters.AddWithValue("@equipment_rental_deposit", location.equipment_rental_deposit == null ? DBNull.Value : location.equipment_rental_deposit);
            updateCommand.Parameters.AddWithValue("@zone", location.zone == null ? DBNull.Value : location.zone);
            updateCommand.Parameters.AddWithValue("@immediatedelivery", location.immediatedelivery == null ? DBNull.Value : location.immediatedelivery);
            updateCommand.Parameters.AddWithValue("@sqft", location.sqft == null ? DBNull.Value : location.sqft);
            updateCommand.Parameters.AddWithValue("@refundpolicyexist", location.refundpolicyexist == null ? DBNull.Value : location.refundpolicyexist);
            updateCommand.Parameters.AddWithValue("@daystosubmittransactions", location.daystosubmittransactions == null ? DBNull.Value : location.daystosubmittransactions);
            updateCommand.Parameters.AddWithValue("@owninventory", location.owninventory == null ? DBNull.Value : location.owninventory);
            updateCommand.Parameters.AddWithValue("@inventorystoredatlocation", location.inventorystoredatlocation == null ? DBNull.Value : location.inventorystoredatlocation);
            updateCommand.Parameters.AddWithValue("@inventorylocation", location.inventorylocation == null ? DBNull.Value : location.inventorylocation);
            updateCommand.Parameters.AddWithValue("@warranteeguaranteeoffered", location.warranteeguaranteeoffered == null ? DBNull.Value : location.warranteeguaranteeoffered);
            updateCommand.Parameters.AddWithValue("@warranteeguaranteetype", location.warranteeguaranteetype == null ? DBNull.Value : location.warranteeguaranteetype);
            updateCommand.Parameters.AddWithValue("@refundtype", location.refundtype == null ? DBNull.Value : location.refundtype);
            updateCommand.Parameters.AddWithValue("@refundother", location.refundother == null ? DBNull.Value : location.refundother);
            updateCommand.Parameters.AddWithValue("@whoenterscreditcardinfo", location.whoenterscreditcardinfo == null ? DBNull.Value : location.whoenterscreditcardinfo);
            updateCommand.Parameters.AddWithValue("@whoenterscreditcardinfoother", location.whoenterscreditcardinfoother == null ? DBNull.Value : location.whoenterscreditcardinfoother);
            updateCommand.Parameters.AddWithValue("@pcinameversion", location.pcinameversion == null ? DBNull.Value : location.pcinameversion);
            updateCommand.Parameters.AddWithValue("@pciversion", location.pciversion == null ? DBNull.Value : location.pciversion);
            updateCommand.Parameters.AddWithValue("@merchantpcicompliant", location.merchantpcicompliant == null ? DBNull.Value : location.merchantpcicompliant);
            updateCommand.Parameters.AddWithValue("@namepcicompliant", location.namepcicompliant == null ? DBNull.Value : location.namepcicompliant);
            updateCommand.Parameters.AddWithValue("@terminalinsurance", location.terminalinsurance == null ? DBNull.Value : location.terminalinsurance);
            updateCommand.Parameters.AddWithValue("@fdglrelationshipcode", location.fdglrelationshipcode == null ? DBNull.Value : location.fdglrelationshipcode);
            updateCommand.Parameters.AddWithValue("@billingoptions", location.billingoptions == null ? DBNull.Value : location.billingoptions);
            updateCommand.Parameters.AddWithValue("@newamex", location.newamex == null ? DBNull.Value : location.newamex);
            updateCommand.Parameters.AddWithValue("@daysuntildelivery_0", location.daysuntildelivery_0 == null ? DBNull.Value : location.daysuntildelivery_0);
            updateCommand.Parameters.AddWithValue("@daysuntildelivery_1to3", location.daysuntildelivery_1to3 == null ? DBNull.Value : location.daysuntildelivery_1to3);
            updateCommand.Parameters.AddWithValue("@daysuntildelivery_4to7", location.daysuntildelivery_4to7 == null ? DBNull.Value : location.daysuntildelivery_4to7);
            updateCommand.Parameters.AddWithValue("@daysuntildelivery_8to14", location.daysuntildelivery_8to14 == null ? DBNull.Value : location.daysuntildelivery_8to14);
            updateCommand.Parameters.AddWithValue("@daysuntildelivery_15to30", location.daysuntildelivery_15to30 == null ? DBNull.Value : location.daysuntildelivery_15to30);
            updateCommand.Parameters.AddWithValue("@daysuntildelivery_30plus", location.daysuntildelivery_30plus == null ? DBNull.Value : location.daysuntildelivery_30plus);
            updateCommand.Parameters.AddWithValue("@daysuntildelivery_15to30_pct", location.daysuntildelivery_15to30_pct == null ? DBNull.Value : location.daysuntildelivery_15to30_pct);
            updateCommand.Parameters.AddWithValue("@replacerefund", location.replacerefund == null ? DBNull.Value : location.replacerefund);
            updateCommand.Parameters.AddWithValue("@orderfulfillmentother", location.orderfulfillmentother == null ? DBNull.Value : location.orderfulfillmentother);
            updateCommand.Parameters.AddWithValue("@zipcodeplus4", location.zipcodeplus4 == null ? DBNull.Value : location.zipcodeplus4);
            updateCommand.Parameters.AddWithValue("@fulfillmentzipcodeplus4", location.fulfillmentzipcodeplus4 == null ? DBNull.Value : location.fulfillmentzipcodeplus4);
            updateCommand.Parameters.AddWithValue("@addresscivicnumber", location.addresscivicnumber == null ? DBNull.Value : location.addresscivicnumber);
            updateCommand.Parameters.AddWithValue("@fulfillmentcivicnumber", location.fulfillmentcivicnumber == null ? DBNull.Value : location.fulfillmentcivicnumber);
            updateCommand.Parameters.AddWithValue("@fulfillmentsuite", location.fulfillmentsuite == null ? DBNull.Value : location.fulfillmentsuite);
            updateCommand.Parameters.AddWithValue("@facility", location.facility == null ? DBNull.Value : location.facility);
            updateCommand.Parameters.AddWithValue("@acceptallcard", location.acceptallcard == null ? DBNull.Value : location.acceptallcard);
            updateCommand.Parameters.AddWithValue("@selectedcards", location.selectedcards == null ? DBNull.Value : location.selectedcards);
            updateCommand.Parameters.AddWithValue("@amountborrow", location.amountborrow == null ? DBNull.Value : location.amountborrow);
            updateCommand.Parameters.AddWithValue("@existcreditcardproc", location.existcreditcardproc == null ? DBNull.Value : location.existcreditcardproc);
            updateCommand.Parameters.AddWithValue("@creditcardmerchantid", location.creditcardmerchantid == null ? DBNull.Value : location.creditcardmerchantid);
            updateCommand.Parameters.AddWithValue("@creditcardyear", location.creditcardyear == null ? DBNull.Value : location.creditcardyear);
            updateCommand.Parameters.AddWithValue("@creditcardmonth", location.creditcardmonth == null ? DBNull.Value : location.creditcardmonth);
            updateCommand.Parameters.AddWithValue("@existcheckproc", location.existcheckproc == null ? DBNull.Value : location.existcheckproc);
            updateCommand.Parameters.AddWithValue("@checkprocmerchantid", location.checkprocmerchantid == null ? DBNull.Value : location.checkprocmerchantid);
            updateCommand.Parameters.AddWithValue("@checkprocyear", location.checkprocyear == null ? DBNull.Value : location.checkprocyear);
            updateCommand.Parameters.AddWithValue("@checkprocmonth", location.checkprocmonth == null ? DBNull.Value : location.checkprocmonth);
            updateCommand.Parameters.AddWithValue("@avgcheckmonthvol", location.avgcheckmonthvol == null ? DBNull.Value : location.avgcheckmonthvol);
            updateCommand.Parameters.AddWithValue("@avgsalecreditcardamt", location.avgsalecreditcardamt == null ? DBNull.Value : location.avgsalecreditcardamt);
            updateCommand.Parameters.AddWithValue("@avgsalechekamt", location.avgsalechekamt == null ? DBNull.Value : location.avgsalechekamt);
            updateCommand.Parameters.AddWithValue("@cks_account_type", location.cks_account_type == null ? DBNull.Value : location.cks_account_type);
            updateCommand.Parameters.AddWithValue("@dba_address", location.dba_address == null ? DBNull.Value : location.dba_address);
            updateCommand.Parameters.AddWithValue("@websiteurl", location.websiteurl == null ? DBNull.Value : location.websiteurl);
            updateCommand.Parameters.AddWithValue("@websiteencrypt", location.websiteencrypt == null ? DBNull.Value : location.websiteencrypt);
            updateCommand.Parameters.AddWithValue("@survey", location.survey == null ? DBNull.Value : location.survey);
            updateCommand.Parameters.AddWithValue("@survey_found_merch", location.survey_found_merch == null ? DBNull.Value : location.survey_found_merch);
            updateCommand.Parameters.AddWithValue("@site_photo", location.site_photo == null ? DBNull.Value : location.site_photo);
            updateCommand.Parameters.AddWithValue("@valid_id", location.valid_id == null ? DBNull.Value : location.valid_id);
            updateCommand.Parameters.AddWithValue("@equip_capture", location.equip_capture == null ? DBNull.Value : location.equip_capture);
            updateCommand.Parameters.AddWithValue("@connectivity", location.connectivity == null ? DBNull.Value : location.connectivity);
            updateCommand.Parameters.AddWithValue("@imprinter", location.imprinter == null ? DBNull.Value : location.imprinter);
            updateCommand.Parameters.AddWithValue("@dial_prefix", location.dial_prefix == null ? DBNull.Value : location.dial_prefix);
            updateCommand.Parameters.AddWithValue("@equip_provider", location.equip_provider == null ? DBNull.Value : location.equip_provider);
            updateCommand.Parameters.AddWithValue("@equip_download", location.equip_download == null ? DBNull.Value : location.equip_download);
            updateCommand.Parameters.AddWithValue("@hasloyaltycard", location.hasloyaltycard == null ? DBNull.Value : location.hasloyaltycard);
            updateCommand.Parameters.AddWithValue("@maxtktsize", location.maxtktsize == null ? DBNull.Value : location.maxtktsize);
            updateCommand.Parameters.AddWithValue("@monthlyvolumerequired", location.monthlyvolumerequired == null ? DBNull.Value : location.monthlyvolumerequired);
            updateCommand.Parameters.AddWithValue("@previouscardaccept", location.previouscardaccept == null ? DBNull.Value : location.previouscardaccept);
            updateCommand.Parameters.AddWithValue("@previousstatements", location.previousstatements == null ? DBNull.Value : location.previousstatements);
            updateCommand.Parameters.AddWithValue("@previousecommerce", location.previousecommerce == null ? DBNull.Value : location.previousecommerce);
            updateCommand.Parameters.AddWithValue("@activelyconducting", location.activelyconducting == null ? DBNull.Value : location.activelyconducting);
            updateCommand.Parameters.AddWithValue("@activelyconductingexplain", location.activelyconductingexplain == null ? DBNull.Value : location.activelyconductingexplain);
            updateCommand.Parameters.AddWithValue("@futuredeliverypct", location.futuredeliverypct == null ? DBNull.Value : location.futuredeliverypct);
            updateCommand.Parameters.AddWithValue("@deliveryexplanation", location.deliveryexplanation == null ? DBNull.Value : location.deliveryexplanation);
            updateCommand.Parameters.AddWithValue("@shippingmethod", location.shippingmethod == null ? DBNull.Value : location.shippingmethod);
            updateCommand.Parameters.AddWithValue("@shipto", location.shipto == null ? DBNull.Value : location.shipto);
            updateCommand.Parameters.AddWithValue("@shippingaddress", location.shippingaddress == null ? DBNull.Value : location.shippingaddress);
            updateCommand.Parameters.AddWithValue("@inventorysufficient", location.inventorysufficient == null ? DBNull.Value : location.inventorysufficient);
            updateCommand.Parameters.AddWithValue("@inventorysufficientexplain", location.inventorysufficientexplain == null ? DBNull.Value : location.inventorysufficientexplain);
            updateCommand.Parameters.AddWithValue("@inventoryamount", location.inventoryamount == null ? DBNull.Value : location.inventoryamount);
            updateCommand.Parameters.AddWithValue("@inventoryamountexplain", location.inventoryamountexplain == null ? DBNull.Value : location.inventoryamountexplain);
            updateCommand.Parameters.AddWithValue("@servicelistedonapp", location.servicelistedonapp == null ? DBNull.Value : location.servicelistedonapp);
            updateCommand.Parameters.AddWithValue("@servicelistedonappexplain", location.servicelistedonappexplain == null ? DBNull.Value : location.servicelistedonappexplain);
            updateCommand.Parameters.AddWithValue("@inspectedby", location.inspectedby == null ? DBNull.Value : location.inspectedby);
            updateCommand.Parameters.AddWithValue("@inboundcalls", location.inboundcalls == null ? DBNull.Value : location.inboundcalls);
            updateCommand.Parameters.AddWithValue("@outboundcalls", location.outboundcalls == null ? DBNull.Value : location.outboundcalls);
            updateCommand.Parameters.AddWithValue("@ach", location.ach == null ? DBNull.Value : location.ach);
            updateCommand.Parameters.AddWithValue("@fein", location.fein == null ? DBNull.Value : location.fein);
            updateCommand.Parameters.AddWithValue("@legalentity", location.legalentity == null ? DBNull.Value : location.legalentity);
            updateCommand.Parameters.AddWithValue("@leadaccdistinction", location.leadaccdistinction == null ? DBNull.Value : location.leadaccdistinction);
            updateCommand.Parameters.AddWithValue("@feetier", location.feetier == null ? DBNull.Value : location.feetier);
            updateCommand.Parameters.AddWithValue("@templateid", location.templateid == null ? DBNull.Value : location.templateid);
            updateCommand.Parameters.AddWithValue("@bankaccountuseforboth", location.bankaccountuseforboth == null ? DBNull.Value : location.bankaccountuseforboth);
            updateCommand.Parameters.AddWithValue("@daysuntildeliveryvol1", location.daysuntildeliveryvol1 == null ? DBNull.Value : location.daysuntildeliveryvol1);
            updateCommand.Parameters.AddWithValue("@daysuntildeliveryvol2", location.daysuntildeliveryvol2 == null ? DBNull.Value : location.daysuntildeliveryvol2);
            updateCommand.Parameters.AddWithValue("@daysuntildeliveryvol3", location.daysuntildeliveryvol3 == null ? DBNull.Value : location.daysuntildeliveryvol3);
            updateCommand.Parameters.AddWithValue("@daysuntildeliverydays1", location.daysuntildeliverydays1 == null ? DBNull.Value : location.daysuntildeliverydays1);
            updateCommand.Parameters.AddWithValue("@daysuntildeliverydays2", location.daysuntildeliverydays2 == null ? DBNull.Value : location.daysuntildeliverydays2);
            updateCommand.Parameters.AddWithValue("@daysuntildeliverydays3", location.daysuntildeliverydays3 == null ? DBNull.Value : location.daysuntildeliverydays3);
            updateCommand.Parameters.AddWithValue("@pciemailaddress", location.pciemailaddress == null ? DBNull.Value : location.pciemailaddress);

            #endregion
            int ID = (int)updateCommand.ExecuteScalar();

            toReturn.Add("Success", ID);

            return toReturn;
        }
    }
}