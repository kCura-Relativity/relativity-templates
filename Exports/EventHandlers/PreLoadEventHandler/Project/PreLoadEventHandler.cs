﻿using System;
using System.Runtime.InteropServices;
using kCura.EventHandler;
using kCura.EventHandler.CustomAttributes;
using kCura.Relativity.Client;
using Relativity.API;
using Relativity.Services.Objects;

namespace $safeprojectname$
{
    [kCura.EventHandler.CustomAttributes.Description("Pre Load EventHandler")]
    [System.Runtime.InteropServices.Guid("$guid1$")]
    public class PreLoadEventHandler : kCura.EventHandler.PreLoadEventHandler
    {

        public override Response Execute()
        {
            //Construct a response object with default values.
            kCura.EventHandler.Response retVal = new kCura.EventHandler.Response();
            retVal.Success = true;
            retVal.Message = string.Empty;
            try
            {
                Int32 currentWorkspaceArtifactID = Helper.GetActiveCaseID();

                //The Object Manager is the newest and preferred way to interact with Relativity instead of the Relativity Services API(RSAPI). 
                //The RSAPI will be scheduled for depreciation after the Object Manager reaches feature party with it.
                using (IObjectManager objectManager = this.Helper.GetServicesManager().CreateProxy<IObjectManager>(ExecutionIdentity.System))
                {

                }

                //Setting up an RSAPI Client
                using (IRSAPIClient proxy =
                    Helper.GetServicesManager().CreateProxy<IRSAPIClient>(ExecutionIdentity.System))
                {
                    //Set the proxy to use the current workspace
                    proxy.APIOptions.WorkspaceID = currentWorkspaceArtifactID;
                    //Add code for working with RSAPIClient
                }

                Relativity.API.IDBContext workspaceContext = Helper.GetDBContext(currentWorkspaceArtifactID);

                //Get a dbContext for the EDDS database
                Relativity.API.IDBContext eddsDBContext = Helper.GetDBContext(-1);

                IAPILog logger = Helper.GetLoggerFactory().GetLogger();
                logger.LogVerbose("Log information throughout execution.");
            }
            catch (Exception ex)
            {
                //Change the response Success property to false to let the user know an error occurred
                retVal.Success = false;
                retVal.Message = ex.ToString();
            }

            return retVal;
        }

        /// <summary>
        ///     The RequiredFields property tells Relativity that your event handler needs to have access to specific fields that
        ///     you return in this collection property
        ///     regardless if they are on the current layout or not. These fields will be returned in the ActiveArtifact.Fields
        ///     collection just like other fields that are on
        ///     the current layout when the event handler is executed.
        /// </summary>
        public override FieldCollection RequiredFields
        {
            get
            {
                kCura.EventHandler.FieldCollection retVal = new kCura.EventHandler.FieldCollection();
                return retVal;
            }
        }
    }
}