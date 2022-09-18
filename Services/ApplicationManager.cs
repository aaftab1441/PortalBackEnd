using System;
using System.Data;
using System.Data.SqlClient;
using EaglePortal.Utils;
using System.Collections.Generic;
using EaglePortal.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;

namespace EaglePortal.Services
{
    public class ApplicationManager
    {
        private SqlConnection conn;

        private UtilityManager utilityManager;
        public ApplicationManager(String connectionString)
        {
            conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            utilityManager = new UtilityManager();

        }
        ~ApplicationManager()
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
        public Dictionary<string, object> SaveApplication(dynamic application, FeeTemplate template, string base_API_Path)
        {
            #region
            SqlCommand addCommand = conn.CreateCommand();

            addCommand = conn.CreateCommand();
            string insertSQL = "INSERT INTO FeeTemplates (name, description, applicationtypeid, providerid, userid, equipmentid, pinpadid, " +
                               "checkreaderid, checkimagerid, possystemid, paymentgatewayid, createduserid, updateduserid, isdefault, " +
                               "agentdefault, iscustom, created_at, updated_at, pricetype, processorid) VALUES(@name, @description, " +
                               "@applicationtypeid, @providerid, @userid, @equipmentid, @pinpadid, @checkreaderid, @checkimagerid, " +
                               "@possystemid, @paymentgatewayid, @createduserid, @updateduserid, @isdefault, @agentdefault, @iscustom, " +
                               "@created_at, @updated_at, @pricetype, @processorid)SELECT CAST(scope_identity() AS int);";

            addCommand.CommandText = insertSQL;
            int appTypeId, providerTypeId, userTypeId, cretUseserId, UpdUserId, defaultTypeId, agentTypeId, customTypeId,
                priceTypeId, processorTypeId;
            DateTime createdDate, updatedDate;
            int.TryParse(template.ApplicationTypeId, out appTypeId);
            int.TryParse(template.ProviderId, out providerTypeId);
            int.TryParse(template.UserId, out userTypeId);
            int.TryParse(template.CreatedUserId, out cretUseserId);
            int.TryParse(template.UpdatedUserId, out UpdUserId);
            int.TryParse(template.IsDefault, out defaultTypeId);
            int.TryParse(template.AgentDefault, out agentTypeId);
            int.TryParse(template.IsCustom, out customTypeId);
            int.TryParse(template.PriceType, out priceTypeId);
            int.TryParse(template.ProcessorId, out processorTypeId);
            DateTime.TryParse(template.CreatedAt ?? DateTime.Now.ToString(), out createdDate);
            DateTime.TryParse(template.UpdatedAt ?? DateTime.Now.ToString(), out updatedDate);
            addCommand.Parameters.AddWithValue("@name", template.Name);
            addCommand.Parameters.AddWithValue("@description", template.Description);
            addCommand.Parameters.AddWithValue("@applicationtypeid", appTypeId);
            addCommand.Parameters.AddWithValue("@providerid", providerTypeId);
            addCommand.Parameters.AddWithValue("@userid", userTypeId);
            addCommand.Parameters.AddWithValue("@equipmentid", template.EquipmentId);
            addCommand.Parameters.AddWithValue("@pinpadid", template.PinpadId);
            addCommand.Parameters.AddWithValue("@checkreaderid", template.CheckReaderId);
            addCommand.Parameters.AddWithValue("@checkimagerid", template.CheckImagerId);
            addCommand.Parameters.AddWithValue("@possystemid", template.PosSystemId);
            addCommand.Parameters.AddWithValue("@paymentgatewayid", template.PaymentGatewayId);
            addCommand.Parameters.AddWithValue("@createduserid", cretUseserId);
            addCommand.Parameters.AddWithValue("@updateduserid", UpdUserId);
            addCommand.Parameters.AddWithValue("@isdefault", defaultTypeId);
            addCommand.Parameters.AddWithValue("@agentdefault", agentTypeId);
            addCommand.Parameters.AddWithValue("@iscustom", customTypeId);
            addCommand.Parameters.AddWithValue("@created_at", createdDate);
            addCommand.Parameters.AddWithValue("@updated_at", updatedDate);
            addCommand.Parameters.AddWithValue("@pricetype", priceTypeId);
            addCommand.Parameters.AddWithValue("@processorid", processorTypeId);

            template.Id = addCommand.ExecuteScalar().ToString();

            foreach (var category in template.Categories)
            {
                addCommand = conn.CreateCommand();

                insertSQL = "INSERT INTO FeeCategory (category, applicationtypeid, providerid, created_at, " +
                    "created_by, updated_at, updated_by)" +
                    " VALUES(@category, @applicationtypeid, @providerid, @created_at, @created_by, @updated_at, @updated_by)" +
                    "SELECT CAST(scope_identity() AS int);";

                addCommand.CommandText = insertSQL;

                addCommand.Parameters.AddWithValue("@category", category.Name);
                addCommand.Parameters.AddWithValue("@applicationtypeid", appTypeId);
                addCommand.Parameters.AddWithValue("@providerid", providerTypeId);
                addCommand.Parameters.AddWithValue("@created_at", createdDate);
                addCommand.Parameters.AddWithValue("@created_by", cretUseserId);
                addCommand.Parameters.AddWithValue("@updated_at", updatedDate);
                addCommand.Parameters.AddWithValue("@updated_by", UpdUserId);

                category.Id = addCommand.ExecuteScalar().ToString();

                foreach (var fee in category.Fees)
                {
                    addCommand = conn.CreateCommand();

                    insertSQL = "INSERT INTO Fees (parentid, applicationtypeid, templateid, [name], fieldtype, valuetype, defaultvalue, highvalue, " +
                        "lowvalue, columntodisplay, leftdisplayorder, rightdisplayorder, [description], [percent], haspercent, feetier, " +
                        "userid, isdefault, voidflag, created_at, updated_at, selectoptions, section) " +
                        " VALUES(@parentid, @applicationtypeid, @templateid, @name, @fieldtype, @valuetype, @defaultvalue, @highvalue, @lowvalue, " +
                        "@columntodisplay, @leftdisplayorder, @rightdisplayorder, @description, @percent, @haspercent, @feetier, " +
                        "@userid, @isdefault , @voidflag, @created_at, @updated_at, @selectoptions, @section)SELECT CAST(scope_identity() AS int);";

                    addCommand.CommandText = insertSQL;
                    int templateTypeId, parentTypeId, leftTypeId, rightTypeId, haspercentTypeId, feeTierTypeId, flagTypeId, sectionTypeId;
                    int.TryParse(fee.ApplicationTypeId, out appTypeId);
                    int.TryParse(fee.ParentId, out parentTypeId);
                    int.TryParse(fee.TemplateId, out templateTypeId);
                    int.TryParse(fee.LeftDisplayOrder, out leftTypeId);
                    int.TryParse(fee.RightDisplayOrder, out rightTypeId);
                    int.TryParse(fee.IsDefault, out defaultTypeId);
                    int.TryParse(fee.UserId, out userTypeId);
                    int.TryParse(fee.HasPercent, out haspercentTypeId);
                    int.TryParse(fee.FeeTier, out feeTierTypeId);
                    int.TryParse(fee.VoidFlag, out flagTypeId);
                    int.TryParse(fee.Section, out sectionTypeId);
                    DateTime.TryParse(fee.CreatedAt ?? DateTime.Now.ToString(), out createdDate);
                    DateTime.TryParse(fee.UpdatedAt ?? DateTime.Now.ToString(), out updatedDate);
                    addCommand.Parameters.AddWithValue("@parentid", parentTypeId);
                    addCommand.Parameters.AddWithValue("@applicationtypeid", appTypeId);
                    addCommand.Parameters.AddWithValue("@templateid", templateTypeId);
                    addCommand.Parameters.AddWithValue("@name", fee.Name);
                    addCommand.Parameters.AddWithValue("@fieldtype", fee.FieldType);
                    addCommand.Parameters.AddWithValue("@valuetype", fee.ValueType);
                    addCommand.Parameters.AddWithValue("@defaultvalue", fee.DefaultValue);
                    addCommand.Parameters.AddWithValue("@highvalue", fee.HighValue);
                    addCommand.Parameters.AddWithValue("@lowvalue", fee.LowValue);
                    addCommand.Parameters.AddWithValue("@columntodisplay", fee.ColumnToDisplay);
                    addCommand.Parameters.AddWithValue("@leftdisplayorder", leftTypeId);
                    addCommand.Parameters.AddWithValue("@rightdisplayorder", rightTypeId);
                    addCommand.Parameters.AddWithValue("@description", fee.Description);
                    addCommand.Parameters.AddWithValue("@percent", fee.Percent ?? "");
                    addCommand.Parameters.AddWithValue("@haspercent", haspercentTypeId);
                    addCommand.Parameters.AddWithValue("@feetier", feeTierTypeId);
                    addCommand.Parameters.AddWithValue("@userid", userTypeId);
                    addCommand.Parameters.AddWithValue("@isdefault", defaultTypeId);
                    addCommand.Parameters.AddWithValue("@voidflag", flagTypeId);
                    addCommand.Parameters.AddWithValue("@created_at", createdDate);
                    addCommand.Parameters.AddWithValue("@updated_at", updatedDate);
                    addCommand.Parameters.AddWithValue("@selectoptions", fee.SelectOptions ?? "");
                    addCommand.Parameters.AddWithValue("@section", sectionTypeId);

                    fee.Id = addCommand.ExecuteScalar().ToString();
                }
            }
            #endregion

            Dictionary<string, object> toReturn = new Dictionary<string, object>();
            #region sql
            insertSQL = "INSERT INTO Application (displayid, appid,applicationstatusid,applicationtypeid,isoid,lenderid,giftloyaltyid,lineofcreditid," +
                "checkaccountid,locationid,underwritingcomplete,invoiceid,applicationdate,statuschangedate,statuschangeby,newapplicationdate,paperworksentdate," +
                "paperworkreceiveddate,incompletepaperworkdate,outstandingesigdate,completedesigdate,submittedtovalidationdate,invalidationdate,submittedtopivitoldate," +
                "cancelleddate,liveaccountdate,validationrejecteddate,fundeddate,approveddate,paidoffdate,submittedtounderwritingdate,incompleteapplicationdate," +
                "voidflag,unlockfeesflag,created_at,updated_at,checkprocid,leadid) " +
                "VALUES(@displayid, @appid, @applicationstatusid, @applicationtypeid, @isoid, @lenderid, @giftloyaltyid, @lineofcreditid, @checkaccountid, @locationid, " +
                "@underwritingcomplete, @invoiceid, @applicationdate, @statuschangedate, @statuschangeby, @newapplicationdate, @paperworksentdate, " +
                "@paperworkreceiveddate, @incompletepaperworkdate, @outstandingesigdate, @completedesigdate, @submittedtovalidationdate, @invalidationdate, " +
                "@submittedtopivitoldate, @cancelleddate, @liveaccountdate, @validationrejecteddate, @fundeddate, @approveddate, @paidoffdate, " +
                "@submittedtounderwritingdate, @incompleteapplicationdate, @voidflag, @unlockfeesflag, @created_at, @updated_at, @checkprocid, @leadid) SELECT CAST(scope_identity() AS int);";

            addCommand = conn.CreateCommand();
            addCommand.CommandText = insertSQL;

            addCommand.Parameters.AddWithValue("@displayid", utilityManager.TryGetProperty(application, "displayid"));
            addCommand.Parameters.AddWithValue("@appid", utilityManager.TryGetProperty(application, "appid"));
            addCommand.Parameters.AddWithValue("@applicationstatusid", utilityManager.TryGetProperty(application, "applicationstatusid"));
            addCommand.Parameters.AddWithValue("@applicationtypeid", utilityManager.TryGetProperty(application, "applicationtypeid"));
            addCommand.Parameters.AddWithValue("@isoid", utilityManager.TryGetProperty(application, "isoid"));
            addCommand.Parameters.AddWithValue("@lenderid", utilityManager.TryGetProperty(application, "lenderid"));
            addCommand.Parameters.AddWithValue("@giftloyaltyid", utilityManager.TryGetProperty(application, "giftloyaltyid"));
            addCommand.Parameters.AddWithValue("@lineofcreditid", utilityManager.TryGetProperty(application, "lineofcreditid"));
            addCommand.Parameters.AddWithValue("@checkaccountid", utilityManager.TryGetProperty(application, "checkaccountid"));
            addCommand.Parameters.AddWithValue("@locationid", utilityManager.TryGetProperty(application, "locationid"));
            addCommand.Parameters.AddWithValue("@underwritingcomplete", utilityManager.TryGetProperty(application, "underwritingcomplete"));
            addCommand.Parameters.AddWithValue("@invoiceid", utilityManager.TryGetProperty(application, "invoiceid"));
            addCommand.Parameters.AddWithValue("@applicationdate", utilityManager.TryGetProperty(application, "applicationdate"));
            addCommand.Parameters.AddWithValue("@statuschangedate", utilityManager.TryGetProperty(application, "statuschangedate"));
            addCommand.Parameters.AddWithValue("@statuschangeby", utilityManager.TryGetProperty(application, "statuschangeby"));
            addCommand.Parameters.AddWithValue("@newapplicationdate", utilityManager.TryGetProperty(application, "newapplicationdate"));
            addCommand.Parameters.AddWithValue("@paperworksentdate", utilityManager.TryGetProperty(application, "paperworksentdate"));
            addCommand.Parameters.AddWithValue("@paperworkreceiveddate", utilityManager.TryGetProperty(application, "paperworkreceiveddate"));
            addCommand.Parameters.AddWithValue("@incompletepaperworkdate", utilityManager.TryGetProperty(application, "incompletepaperworkdate"));
            addCommand.Parameters.AddWithValue("@outstandingesigdate", utilityManager.TryGetProperty(application, "outstandingesigdate"));
            addCommand.Parameters.AddWithValue("@completedesigdate", utilityManager.TryGetProperty(application, "completedesigdate"));
            addCommand.Parameters.AddWithValue("@submittedtovalidationdate", utilityManager.TryGetProperty(application, "submittedtovalidationdate"));
            addCommand.Parameters.AddWithValue("@invalidationdate", utilityManager.TryGetProperty(application, "invalidationdate"));
            addCommand.Parameters.AddWithValue("@submittedtopivitoldate", utilityManager.TryGetProperty(application, "submittedtopivitoldate"));
            addCommand.Parameters.AddWithValue("@cancelleddate", utilityManager.TryGetProperty(application, "cancelleddate"));
            addCommand.Parameters.AddWithValue("@liveaccountdate", utilityManager.TryGetProperty(application, "liveaccountdate"));
            addCommand.Parameters.AddWithValue("@validationrejecteddate", utilityManager.TryGetProperty(application, "validationrejecteddate"));
            addCommand.Parameters.AddWithValue("@fundeddate", utilityManager.TryGetProperty(application, "fundeddate"));
            addCommand.Parameters.AddWithValue("@approveddate", utilityManager.TryGetProperty(application, "approveddate"));
            addCommand.Parameters.AddWithValue("@paidoffdate", utilityManager.TryGetProperty(application, "paidoffdate"));
            addCommand.Parameters.AddWithValue("@submittedtounderwritingdate", utilityManager.TryGetProperty(application, "submittedtounderwritingdate"));
            addCommand.Parameters.AddWithValue("@incompleteapplicationdate", utilityManager.TryGetProperty(application, "incompleteapplicationdate"));
            addCommand.Parameters.AddWithValue("@voidflag", utilityManager.TryGetProperty(application, "voidflag"));
            addCommand.Parameters.AddWithValue("@unlockfeesflag", utilityManager.TryGetProperty(application, "unlockfeesflag"));
            addCommand.Parameters.AddWithValue("@created_at", DateTime.Now);
            addCommand.Parameters.AddWithValue("@updated_at", DateTime.Now);
            addCommand.Parameters.AddWithValue("@checkprocid", utilityManager.TryGetProperty(application, "checkprocid"));
            addCommand.Parameters.AddWithValue("@leadid", utilityManager.TryGetProperty(application, "leadid"));
            int id = (int)addCommand.ExecuteScalar();
            #endregion

            PostProcessApplicationAsync(utilityManager.TryGetProperty(application, "appid"), utilityManager.TryGetProperty(application, "customerid"), utilityManager.TryGetProperty(application, "emailid"), base_API_Path);


            toReturn.Add("Success", id);

            return toReturn;
        }
        public Dictionary<string, object> GetApplication(int Id)
        {
            Dictionary<string, object> toReturn = new Dictionary<string, object>();
            SqlCommand selectCommand = conn.CreateCommand();
            #region sql
            string selectSQL = "SELECT * FROM Application WHERE Id = @Id";

            selectCommand.CommandText = selectSQL;
            selectCommand.Parameters.AddWithValue("@Id", Id);

            #endregion
            int ID = (int)selectCommand.ExecuteScalar();

            toReturn.Add("Success", ID);

            return toReturn;
        }
        public Dictionary<string, object> UpdateApplication(dynamic application)
        {
            Dictionary<string, object> toReturn = new Dictionary<string, object>();
            SqlCommand updateCommand = conn.CreateCommand();
            #region sql
            string updateSQL = "UPDATE Application SET displayid = @displayid, appid = @appid, applicationstatusid = @applicationstatusid, " +
                "applicationtypeid = @applicationtypeid,isoid = @isoid,lenderid = @lenderid,giftloyaltyid = @giftloyaltyid,lineofcreditid = @lineofcreditid," +
                "checkaccountid = @checkaccountid,locationid = @locationid,underwritingcomplete = @underwritingcomplete,invoiceid = @invoiceid," +
                "applicationdate = @applicationdate,statuschangedate = @statuschangedate,statuschangeby = @statuschangeby,newapplicationdate = @newapplicationdate," +
                "paperworksentdate = @paperworksentdate,paperworkreceiveddate = @paperworkreceiveddate,incompletepaperworkdate = @incompletepaperworkdate," +
                "outstandingesigdate = @outstandingesigdate,completedesigdate = @completedesigdate,submittedtovalidationdate = @submittedtovalidationdate," +
                "invalidationdate = @invalidationdate,submittedtopivitoldate = @submittedtopivitoldate,cancelleddate = @cancelleddate,liveaccountdate = @liveaccountdate," +
                "validationrejecteddate = @validationrejecteddate,fundeddate = @fundeddate,approveddate = @approveddate,paidoffdate = @paidoffdate," +
                "submittedtounderwritingdate = @submittedtounderwritingdate,incompleteapplicationdate = @incompleteapplicationdate,voidflag = @voidflag," +
                "unlockfeesflag = @unlockfeesflag,created_at = @created_at,updated_at = @updated_at,checkprocid = @checkprocid,leadid = @leadid WHERE ID =  @ID";

            updateCommand.CommandText = updateSQL;
            updateCommand.Parameters.AddWithValue("@Id", utilityManager.TryGetProperty(application, "Id"));
            updateCommand.Parameters.AddWithValue("@displayid", utilityManager.TryGetProperty(application, "displayid"));
            updateCommand.Parameters.AddWithValue("@appid", utilityManager.TryGetProperty(application, "appid"));
            updateCommand.Parameters.AddWithValue("@applicationstatusid", utilityManager.TryGetProperty(application, "applicationstatusid"));
            updateCommand.Parameters.AddWithValue("@applicationtypeid", utilityManager.TryGetProperty(application, "applicationtypeid"));
            updateCommand.Parameters.AddWithValue("@isoid", utilityManager.TryGetProperty(application, "isoid"));
            updateCommand.Parameters.AddWithValue("@lenderid", utilityManager.TryGetProperty(application, "lenderid"));
            updateCommand.Parameters.AddWithValue("@giftloyaltyid", utilityManager.TryGetProperty(application, "giftloyaltyid"));
            updateCommand.Parameters.AddWithValue("@lineofcreditid", utilityManager.TryGetProperty(application, "lineofcreditid"));
            updateCommand.Parameters.AddWithValue("@checkaccountid", utilityManager.TryGetProperty(application, "checkaccountid"));
            updateCommand.Parameters.AddWithValue("@locationid", utilityManager.TryGetProperty(application, "locationid"));
            updateCommand.Parameters.AddWithValue("@underwritingcomplete", utilityManager.TryGetProperty(application, "underwritingcomplete"));
            updateCommand.Parameters.AddWithValue("@invoiceid", utilityManager.TryGetProperty(application, "invoiceid"));
            updateCommand.Parameters.AddWithValue("@applicationdate", utilityManager.TryGetProperty(application, "applicationdate"));
            updateCommand.Parameters.AddWithValue("@statuschangedate", utilityManager.TryGetProperty(application, "statuschangedate"));
            updateCommand.Parameters.AddWithValue("@statuschangeby", utilityManager.TryGetProperty(application, "statuschangeby"));
            updateCommand.Parameters.AddWithValue("@newapplicationdate", utilityManager.TryGetProperty(application, "newapplicationdate"));
            updateCommand.Parameters.AddWithValue("@paperworksentdate", utilityManager.TryGetProperty(application, "paperworksentdate"));
            updateCommand.Parameters.AddWithValue("@paperworkreceiveddate", utilityManager.TryGetProperty(application, "paperworkreceiveddate"));
            updateCommand.Parameters.AddWithValue("@incompletepaperworkdate", utilityManager.TryGetProperty(application, "incompletepaperworkdate"));
            updateCommand.Parameters.AddWithValue("@outstandingesigdate", utilityManager.TryGetProperty(application, "outstandingesigdate"));
            updateCommand.Parameters.AddWithValue("@completedesigdate", utilityManager.TryGetProperty(application, "completedesigdate"));
            updateCommand.Parameters.AddWithValue("@submittedtovalidationdate", utilityManager.TryGetProperty(application, "submittedtovalidationdate"));
            updateCommand.Parameters.AddWithValue("@invalidationdate", utilityManager.TryGetProperty(application, "invalidationdate"));
            updateCommand.Parameters.AddWithValue("@submittedtopivitoldate", utilityManager.TryGetProperty(application, "submittedtopivitoldate"));
            updateCommand.Parameters.AddWithValue("@cancelleddate", utilityManager.TryGetProperty(application, "cancelleddate"));
            updateCommand.Parameters.AddWithValue("@liveaccountdate", utilityManager.TryGetProperty(application, "liveaccountdate"));
            updateCommand.Parameters.AddWithValue("@validationrejecteddate", utilityManager.TryGetProperty(application, "validationrejecteddate"));
            updateCommand.Parameters.AddWithValue("@fundeddate", utilityManager.TryGetProperty(application, "fundeddate"));
            updateCommand.Parameters.AddWithValue("@approveddate", utilityManager.TryGetProperty(application, "approveddate"));
            updateCommand.Parameters.AddWithValue("@paidoffdate", utilityManager.TryGetProperty(application, "paidoffdate"));
            updateCommand.Parameters.AddWithValue("@submittedtounderwritingdate", utilityManager.TryGetProperty(application, "submittedtounderwritingdate"));
            updateCommand.Parameters.AddWithValue("@incompleteapplicationdate", utilityManager.TryGetProperty(application, "incompleteapplicationdate"));
            updateCommand.Parameters.AddWithValue("@voidflag", utilityManager.TryGetProperty(application, "voidflag"));
            updateCommand.Parameters.AddWithValue("@unlockfeesflag", utilityManager.TryGetProperty(application, "unlockfeesflag"));
            updateCommand.Parameters.AddWithValue("@created_at", utilityManager.TryGetProperty(application, "created_at"));
            updateCommand.Parameters.AddWithValue("@updated_at", utilityManager.TryGetProperty(application, "updated_at"));
            updateCommand.Parameters.AddWithValue("@checkprocid", utilityManager.TryGetProperty(application, "checkprocid"));
            updateCommand.Parameters.AddWithValue("@leadid", utilityManager.TryGetProperty(application, "leadid"));

            #endregion
            int ID = (int)updateCommand.ExecuteScalar();

            toReturn.Add("Success", ID);

            return toReturn;
        }

        public void PostProcessApplicationAsync(int applicationid, int customerid, string email, string base_API_Path)
        {
            //Get API Token

            string url = base_API_Path + "esign";

            var model = new { email = email };
            try
            {
                HttpClient client = new HttpClient();
                var response = client.PostAsJsonAsync(url, model).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}