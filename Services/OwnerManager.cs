using System;
using System.Data;
using System.Data.SqlClient;
using EaglePortal.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using EaglePortal.Models;

namespace EaglePortal.Services
{
    public class OwnerManager
    {
        private SqlConnection conn;

        private UtilityManager utilityManager;
        public OwnerManager(String connectionString)
        {
            conn = new SqlConnection();
            conn.ConnectionString = connectionString;
            conn.Open();
            utilityManager = new UtilityManager();

        }
        ~OwnerManager()
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
        public Dictionary<string, object> UpsertOwner(List<Owner> owners, string merchantId)
        {
            Dictionary<string, object> toReturn = new Dictionary<string, object>();

            #region sql
            foreach (var owner in owners)
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    bool isNew = owner.Id == "0";
                    string Query = string.Empty;
                    if (isNew)
                    {
                        Query = @"INSERT INTO [MerchantOwner] (ownerfirstname, ownermiddlename, ownerlastname ,  
                                    ownerbirthdate , owneremail, owneraddress,  ownercity , ownerstate, 
                                    ownerzipcode, ownerzipcodeplusfour, ownercitizenship, ownerpercent, 
                                    ownertitle , ownersocialsecurity , ownerhomephone , ownercellphone , 
                                    ownerbankruptcy , ownerbankruptcydate , ownerlicense,  
                                    ownerlicensestate , ownerlicenseexpiration ,
                                    MerchantID) Values(@ownerfirstname, @ownermiddlename, @ownerlastname ,  
                                    @ownerbirthdate , @owneremail, @owneraddress,  @ownercity , @ownerstate, 
                                    @ownerzipcode, @ownerzipcodeplusfour, @ownercitizenship, @ownerpercent, 
                                    @ownertitle , @ownersocialsecurity , @ownerhomephone , @ownercellphone , 
                                    @ownerbankruptcy , @ownerbankruptcydate , @ownerlicense,  
                                    @ownerlicensestate , @ownerlicenseexpiration ,
                                    @MerchantID) SELECT CAST(scope_identity() AS int)";
                    }

                    else
                    {
                        Query = @"UPDATE [MerchantOwner] SET ownerfirstname = @ownerfirstname, ownermiddlename = ownermiddlename, ownerlastname  = @ownerlastname ,  
                                    ownerbirthdate = @ownerbirthdate , owneremail = @owneremail, owneraddress = @owneraddress,  ownercity = @ownercity , ownerstate = @ownerstate, 
                                    ownerzipcode = @ownerzipcode, ownerzipcodeplusfour = @ownerzipcodeplusfour, ownercitizenship = @ownercitizenship, ownerpercent = @ownerpercent, 
                                    ownertitle = @ownertitle , ownersocialsecurity = @ownersocialsecurity , ownerhomephone = @ownerhomephone , ownercellphone = @ownercellphone , 
                                    ownerbankruptcy = @ownerbankruptcy , ownerbankruptcydate = @ownerbankruptcydate , ownerlicense = @ownerlicense,  
                                    ownerlicensestate = @ownerlicensestate , ownerlicenseexpiration = @ownerlicenseexpiration 
                                    WHERE Id = @Id";
                    }



                    command.CommandText = Query;
                    if (!isNew)
                        command.Parameters.AddWithValue("@Id", owner.Id);
                    command.Parameters.AddWithValue("@MerchantId", merchantId);
                    command.Parameters.AddWithValue("@ownerfirstname ", owner.ownerfirstname);
                    command.Parameters.AddWithValue("@ownermiddlename ", owner.ownermiddlename);
                    command.Parameters.AddWithValue("@ownerlastname ", owner.ownerlastname);
                    command.Parameters.AddWithValue("@ownerbirthdate ", owner.ownerbirthdate);
                    command.Parameters.AddWithValue("@owneremail ", owner.owneremail);
                    command.Parameters.AddWithValue("@owneraddress ", owner.owneraddress);
                    command.Parameters.AddWithValue("@ownercity ", owner.ownercity);
                    command.Parameters.AddWithValue("@ownerstate ", owner.ownerstate);
                    command.Parameters.AddWithValue("@ownerzipcode ", owner.ownerzipcode);
                    command.Parameters.AddWithValue("@ownerzipcodeplusfour ", owner.ownerzipcodeplusfour);
                    command.Parameters.AddWithValue("@ownercitizenship ", owner.ownercitizenship);
                    command.Parameters.AddWithValue("@ownerpercent ", owner.ownerpercent);
                    command.Parameters.AddWithValue("@ownertitle ", owner.ownertitle);
                    command.Parameters.AddWithValue("@ownersocialsecurity ", owner.ownersocialsecurity);
                    command.Parameters.AddWithValue("@ownerhomephone ", owner.ownerhomephone);
                    command.Parameters.AddWithValue("@ownercellphone ", owner.ownercellphone);
                    command.Parameters.AddWithValue("@ownerbankruptcy ", owner.ownerbankruptcy);
                    command.Parameters.AddWithValue("@ownerbankruptcydate ", owner.ownerbankruptcydate);
                    command.Parameters.AddWithValue("@ownerlicense ", owner.ownerlicense);
                    command.Parameters.AddWithValue("@ownerlicensestate ", owner.ownerlicensestate);
                    command.Parameters.AddWithValue("@ownerlicenseexpiration ", owner.ownerlicenseexpiration);
                    if (isNew)
                    {
                        owner.Id = command.ExecuteScalar().ToString();
                    }
                    else
                    {
                        command.ExecuteScalar();
                    }
                }

            }
            #endregion
            if (!string.IsNullOrEmpty(merchantId))
                toReturn.Add("Success", Convert.ToInt32(merchantId));
            else
                toReturn.Add("Success", 0);
            toReturn.Add("Owners", owners);

            return toReturn;
        }
    }
}
