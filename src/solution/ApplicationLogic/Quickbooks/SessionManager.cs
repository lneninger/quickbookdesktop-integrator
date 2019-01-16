using ApplicationLogic.AppConfiguration;
using Framework.Logging.Log4Net;
using Interop.QBFC13;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic.Quickbooks
{
    public class SessionManager: IDisposable
    {
        static LoggerCustom Logger = Framework.Logging.Log4Net.LoggerFactory.Create(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Interop.QBFC13.QBSessionManager _qbSessionManager { get; set; }
        public AppConfig AppConfig { get; }

        private bool hasBeenDisposed = false;                       // For object status
        private bool _connOpen = false;                            // Maintains state of the connection
        private bool _sessionOpen = false;                         // Maintains state of the session
        private double _qbsdkVersion = 0.0;                         // most recent SDK version supported by QB instance
        private short _qbSDKMajorVer = 0;                           // generic location to store the major version
        private short _qbSDKMinorVer = 0;                           // generic locaiton to store the minor version
        private string _appId = "";                                 // storage location for the application ID
        private string _appName = "";                               // storage location for the application name
        private string _qbFile = "";                                // storage location for the qbFile entry 
        private ENConnectionType _connType;                         // SDK connection type
        private ENOpenMode _openMode = Defaults.SESSION_MODE;       // Mode used during request set creation
        private ENEdition _edition = Defaults.EDITION;              // QuickBooks edition
        private IMsgSetResponse _queryResponse = null;              // private storage for the query response
        private ISubscriptionMsgSetResponse _querySubResp = null;   // private storage for the subscription response

        public SessionManager(AppConfig appConfig)
        {
            this.AppConfig = appConfig;
            this.Initialize();
        }

        private void Initialize()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Used by the framework
        /// </summary>
        /// <param name="disposeObjs"></param>
        private void Dispose(bool disposeObjs)
        {
            // Only dispose of the object once!
            if (!hasBeenDisposed)
            {
                Logger.Info("SessionManager.Dispose: Disposing of the SessionManager object");

                if (disposeObjs)
                {
                    closeConnection();
                    _qbSessionManager = null;
                }
            }

            hasBeenDisposed = true;
        }

        /// <summary>
        /// Used by the framework
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }

        // ************************************************************************
        // ************************************************************************
        // ***               Open and Close Connection Methods                  ***
        // ************************************************************************
        // ************************************************************************

        /// <summary>
        /// Opens a connection to the backend Quickbooks instance.
        /// </summary>
        public void openConnection()
        {
            openConnection(true, this.AppConfig.QuickbooksApplicationID, this.AppConfig.QuickbooksApplicationName, _connType);
        }

        /// <summary>
        /// Opens a connection to the backend Quickbooks instance.
        /// </summary>
        /// <param name="connType">Type type of connection to open with the backend QuickBooks instance</param>
        public void openConnection(ENConnectionType connType)
        {
            openConnection(true, this.AppConfig.QuickbooksApplicationID, this.AppConfig.QuickbooksApplicationName, connType);
        }

        /// <summary>
        /// Opens a connection to the backend Quickbooks instance.  Allows the developer to
        /// redefine the appId and appName, if desired (these can be defined when the object
        /// is created).
        /// </summary>
        /// <param name="logError">When true, exceptions are logged</param>
        /// <param name="sAppId">The ID of the integrated application (assigned by Intuit)</param>
        /// <param name="appName">The name of the integrated application</param>
        /// <param name="connType">Type type of connection to open with the backend QuickBooks instance</param>
        private void openConnection(bool logError, string appId, string appName, ENConnectionType connType)
        {
            try
            {
                // Close the connection if it was previously open...
                closeConnection(false);

                // Open the connection and save the pertinent information
                _qbSessionManager.OpenConnection2(appId, appName, connType);
                _appId = appId;
                _appName = appName;
                _connType = connType;
                _queryResponse = null;
                _sessionOpen = false;
                _connOpen = true;

                // Now obtain the version information for the backend Quickbooks instance

            }
            catch (Exception e)
            {
                if (logError)
                    Logger.Fatal("SessionManager.OpenConnection", e);

                throw e;
            }
        }

        /// <summary>
        /// Closes the connection to the backend QuickBooks instance
        /// </summary>
        public void closeConnection()
        {
            closeConnection(true);
        }

        /// <summary>
        /// Closes the connection to the backend QuickBooks instance
        /// </summary>
        /// <param name="logError">When true, exceptions are logged</param>
        private void closeConnection(bool logError)
        {
            try
            {
                endSession(false);

                if (_connOpen)
                {
                    _qbSessionManager.CloseConnection();
                    _queryResponse = null;
                    _connOpen = false;
                }
            }
            catch (Exception e)
            {
                if (logError)
                    Logger.Fatal("SessionManager.CloseConnection", e);

                throw e;
            }
        }


        // ************************************************************************
        // ************************************************************************
        // ***                 Open and Close Session Methods                   ***
        // ************************************************************************
        // ************************************************************************

        /// <summary>
        /// Starts a new session, if required.  This method assumes that the QuickBooks is currently
        /// running and that the application will open a connection with the currently
        /// open company file.
        /// </summary>
        /// <returns>The QBSessionManager object</returns>
        public QBSessionManager beginSession()
        {
            return beginSession(true, Defaults.QBFILE, Defaults.SESSION_MODE);
        }

        /// <summary>
        /// Starts a new session, if required.  This method assumes that the QuickBooks is currently
        /// running and that the application will open a connection with the currently
        /// open company file.
        /// </summary>
        /// <param name="qbFile">Path to the company file to open. Mandatory in unattended mode</param>
        /// <returns>The QBSessionManager object</returns>
        public QBSessionManager beginSession(string qbFile)
        {
            return beginSession(true, qbFile, Defaults.SESSION_MODE);
        }

        /// <summary>
        /// Starts a new session, if required.  This method assumes that the QuickBooks is currently
        /// running and that the application will open a connection with the currently
        /// open company file.
        /// </summary>
        /// <param name="qbFile">Path to the company file to open. Mandatory in unattended mode</param>
        /// <returns>The QBSessionManager object</returns>
        public QBSessionManager beginSession(ENOpenMode openMode)
        {
            return beginSession(true, Defaults.QBFILE, openMode);
        }

        /// <summary>
        /// Starts a new session, if required.  This method assumes that the QuickBooks is currently
        /// running and that the application will open a connection with the currently
        /// open company file.
        /// </summary>
        /// <param name="qbFile">Path to the company file to open. Mandatory in unattended mode</param>
        /// <param name="openMode">the mode to use when starting the session</param>
        /// <returns>The QBSessionManager object</returns>
        public QBSessionManager beginSession(string qbFile, ENOpenMode openMode)
        {
            return beginSession(true, qbFile, openMode);
        }

        /// <summary>
        /// Starts a new session, if required.
        /// </summary>
        /// <param name="logError">When true, exceptions are logged</param>
        /// <param name="qbFile">Path to the company file to open. Mandatory in unattended mode</param>
        /// <param name="openMode">the mode to use when starting the session</param>
        /// <returns>The QBSessionManager object</returns>
        private QBSessionManager beginSession(bool logError, string qbFile, ENOpenMode openMode)
        {
            try
            {
                if (!_connOpen)
                    openConnection(false, this.AppConfig.QuickbooksApplicationID, this.AppConfig.QuickbooksApplicationName, _connType);
                ;

                // If a session is already open, do not create another one
                if (!_sessionOpen)
                {
                    _qbSessionManager.BeginSession(qbFile, openMode);
                    _queryResponse = null;
                    _sessionOpen = true;
                }
            }
            catch (Exception e)
            {
                _sessionOpen = false;

                if (logError)
                {
                    Logger.Fatal("SessionManager.beginSession", e);
                }
                throw e;
            }

            return _qbSessionManager;
        }

        /// <summary>
        /// Ends the session, if one exists.
        /// </summary>
        public void endSession()
        {
            endSession(true);
        }

        /// <summary>
        /// Ends the session, if one exists.
        /// </summary>
        /// <param name="bDispErrMsg">When true, exceptions are logged</param>
        private void endSession(bool logError)
        {
            try
            {
                if (_sessionOpen)
                {
                    _qbSessionManager.EndSession();
                    _queryResponse = null;
                    _sessionOpen = false;
                }
            }
            catch (Exception e)
            {
                if (logError)
                    Logger.Fatal("SessionManager.endSession", e);

                throw e;
            }
        }

        // ************************************************************************
        // ************************************************************************
        // ***                       Request and Responses                      ***
        // ************************************************************************
        // ************************************************************************

        /// <summary>
        /// Returns a new request message set object. If a session has already been started, it is
        /// reused.  If you wish to utilize a new session, you must explicitly end the session prior
        /// to calling this method.
        /// </summary>
        /// <returns>The message set object into which requests can be added</returns>
        public IMsgSetRequest getMsgSetRequest()
        {
            IMsgSetRequest rset = getMsgSetRequest(true);
            return rset;
        }

        /// <summary>
        /// Returns a new request message set object. If a session has already been started, it is
        /// reused.  If you wish to utilize a new session, you must explicitly end the session prior
        /// to calling this method.
        /// </summary>
        /// <param name="logError">When true, exceptions are logged</param>
        /// <returns>The message set object into which requests can be added</returns>
        private IMsgSetRequest getMsgSetRequest(bool logError)
        {
            try
            {
                beginSession(false, _qbFile, _openMode);

                if (!_sessionOpen)
                    return null;

                IMsgSetRequest rset = _qbSessionManager.CreateMsgSetRequest(QBEdition.getEdition(_edition), _qbSDKMajorVer, _qbSDKMinorVer);

                if (rset == null)
                {
                    if (logError)
                        Logger.Fatal("SessionManager.getMsgSetRequest: Unable to create a message set");
                        //logger.logCritical("SessionManager.getMsgSetRequest", "Unable to create a message set");

                    throw new QBException("Unable to create a MessageSet");
                }

                return rset;
            }
            catch (Exception e)
            {
                if (logError)
                {
                    Logger.Fatal("SessionManager.getMsgSetRequest", e);
                    //logger.logCritical("SessionManager.getMsgSetRequest", e.Message);
                }
                throw e;
            }
        }

        // ************************************************************************
        // ************************************************************************
        // ***                     Version Checking Method                      ***
        // ************************************************************************
        // ************************************************************************

        /// <summary>
        /// Returns the most recent version supported by the QuickBooks instance to which you are
        /// connecting.  Generally speaking, you always want to use the latest version of the SDK
        /// supported by the instance.
        /// </summary>
        /// <returns>the latest version supported by the QuickBooks instance</returns>
        private void getBackendSdkVersion()
        {
            // The version of the request is hardcoded to 1.0; this is permissible because the
            // very first version of the SDK supported the HostQuery request.  Note that the
            // return of beginSession does not need to be checked because an Exception will be
            // thrown if the object cannot be obtained.
            IMsgSetRequest msgset = beginSession(false, "", ENOpenMode.omDontCare).
                   CreateMsgSetRequest(QBEdition.getEdition(Defaults.EDITION), 1, 0);

            if (msgset == null)
                throw new Exception("Unable to create the Session's message set request object");

            // Append the HostQuery request to the message set.  This particular request does not
            // require any additional parameters; therefore, it can be sent to Quickbooks right now.
            msgset.AppendHostQueryRq();
            //logRequestSet(ref msgset, ENLogLevel.VERBOSE, false);
            Logger.Info(msgset.ToXMLString());
            IMsgSetResponse queryResponse = _qbSessionManager.DoRequests(msgset);
            Logger.Info(queryResponse.ToXMLString());
            //logResponseSet(ref queryResponse, ENLogLevel.VERBOSE, false);

            // The response list contains only one response, which corresponds to 
            // our single HostQuery request; therefore, we can easily hardcode this 
            // particular response
            if (queryResponse.ResponseList == null || queryResponse.ResponseList.GetAt(0) == null)
            {
                Logger.Fatal("SessionManager.getBackendSdkVersion: Detected a null while trying to obtain the response object");
                throw new Exception("Detected a null while trying to obtain the response object");
            }

            checkResponseStatus(ref msgset, ref queryResponse);
            IResponse response = queryResponse.ResponseList.GetAt(0);

            // Typecast the response's detail to IHostRet, which correlates with the return value for
            // the original request.
            if (response == null || response.Detail == null)
            {
                Logger.Fatal($"SessionManager.getBackendSdkVersion: Detected a null while attempting to obtain the IHostRet or its Detail");
                throw new Exception("Detected a null while attempting to obtain the IHostRet or its Detail");
            }

            IHostRet HostResponse = (IHostRet)response.Detail;

            if (HostResponse == null)
            {
                Logger.Fatal("SessionManager.getBackendSdkVersion: Detected a null while attempting to obtain the HostResponse object");
                throw new Exception("Detected a problem trying to obtain the HostResponse object");
            }

            IBSTRList supportedVersions = (IBSTRList)HostResponse.SupportedQBXMLVersionList;

            // Perform a little variable initialization prior to obtaining the most recent SDK version
            // supported by the backend QuickBooks instance.
            int i;
            double vers;
            _qbsdkVersion = 0.0;
            string svers = null;

            // Iterate through each of the versions supported by the remote 
            // QuickBooks instance and return the most recent.
            for (i = 0; i <= supportedVersions.Count - 1; i++)
            {
                svers = supportedVersions.GetAt(i);
                vers = Convert.ToDouble(svers);
                if (vers > _qbsdkVersion)
                    _qbsdkVersion = vers;
            }

            // At this point, _qbsdkVersion has the value of the most recent SDK version supported by the
            // backend instance.  Unfortunately, many of the signatures for methods we want to use require
            // that this double be split apart into two separate integers.  The represent the major (left of
            // the decimal point) and minor (right of the decimal point) values.  Perform this extraction 
            // now.
            _qbSDKMajorVer = (short)_qbsdkVersion;
            string minor = (_qbsdkVersion - _qbSDKMajorVer).ToString();

            if (minor.Length > 1)
                _qbSDKMinorVer = short.Parse(minor.Substring(2));
            else
                _qbSDKMinorVer = 0;

            endSession(false);
        }


        /// <summary>
        /// Executes the requests that have been previously added to the passed request message set object
        /// </summary>
        /// <param name="requestSet">The request message set object containing the requests</param>
        /// <returns>False if the request was successful, true otherwise</returns>
        public bool doRequests(ref IMsgSetRequest requestSet)
        {
            return doRequests(true, ref requestSet);
        }


        /// <summary>
        /// Executes the requests that have been previously added to the passed request message set object
        /// </summary>
        /// <param name="logError">When true, exceptions are logged</param>
        /// <param name="requestSet">The request message set object containing the requests</param>
        /// <returns>False if the request was successful, true otherwise</returns>
        private bool doRequests(bool logError, ref IMsgSetRequest requestSet)
        {
            try
            {
                Logger.Debug(requestSet.ToXMLString());
                //logRequestSet(ref requestSet, ENLogLevel.VERBOSE, false);

                // Execute the query...
                _queryResponse = _qbSessionManager.DoRequests(requestSet);

                // Check the results of the query... return false if bad
                return checkResponseStatus(ref requestSet, ref _queryResponse);
            }
            catch (Exception e)
            {
                if (logError)
                {
                    Logger.Fatal("SessionManager.doRequests", e);
                    //logger.logCritical("SessionManager.doRequests", e.Message);
                }
                throw e;
            }
        }


        /// <summary>
        /// Executes the requests that have been previously added to the passed request message set object
        /// </summary>
        /// <param name="logError">When true, exceptions are logged</param>
        /// <param name="requestSet">The request message set object containing the requests</param>
        /// <returns>IResponseMsgSet if successful, null if error</returns>
        public IMsgSetResponse doRequest(bool logError, ref IMsgSetRequest requestSet)
        {
            try
            {
                //logRequestSet(ref requestSet, ENLogLevel.VERBOSE, false);
                Logger.Info(requestSet.ToXMLString());


                // Execute the query...
                _queryResponse = _qbSessionManager.DoRequests(requestSet);

                // Check the results of the query... return null if bad
                if (checkResponseStatus(ref requestSet, ref _queryResponse))
                {
                    return null;
                }
                else
                {
                    return _queryResponse;
                }

            }
            catch (Exception e)
            {
                if (logError)
                {
                    Logger.Fatal("SessionManager.doRequests", e);
                    //logger.logCritical("SessionManager.doRequests", e.Message);
                }
                throw e;
            }
        }


        /// <summary>
        /// Returns an object containing all of the responses provided by Quickbooks as a result of
        /// processing the last request.
        /// </summary>
        /// <returns>An object containing responses to the last request</returns>
        public IResponseList getResponseList()
        {
            return getResponseList(true);
        }

        /// <summary>
        /// Returns an object containing all of the responses provided by Quickbooks as a result of
        /// processing the last request.
        /// </summary>
        /// <param name="logError">When true, exceptions are logged</param>
        /// <returns>An object containing responses to the last request</returns>
        private IResponseList getResponseList(bool logError)
        {
            if (_queryResponse == null)
            {
                if (logError)
                {
                    Logger.Fatal($@"SessionManager.getResponseList: Unable to obtain the IMsgSetResponse as it is null");
                    //logger.logCritical("SessionManager.getResponseList", "Unable to obtain the IMsgSetResponse as it is null");
                }
                throw new QBException("Null IMsgSetResponse detected!");
            }

            return _queryResponse.ResponseList;
        }

        /// <summary>
        /// Returns a count representing the number of responses within the response object
        /// </summary>
        /// <returns>The number of responses returned by Quickbooks as a result of the last request processed</returns>
        public int getResponseCount()
        {
            return getResponseCount(true);
        }

        /// <summary>
        /// Returns a count representing the number of responses within the response object
        /// </summary>
        /// <param name="logError">When true, exceptions are logged</param>
        /// <returns>The number of responses returned by Quickbooks as a result of the last request processed</returns>
        private int getResponseCount(bool logError)
        {
            try
            {
                return getResponseList(false).Count;
            }
            catch (Exception e)
            {
                if (logError)
                {
                    Logger.Fatal("SessionManager.getResponseCount", e);
                    //logger.logCritical("SessionManager.getResponseCount", e.Message);
                }
                throw e;
            }
        }

        /// <summary>
        /// Returns the desired response within the response list.  The passed value identifies the
        /// response to return as a zero based array index.  A Response index typically correlates with 
        /// the order that its request was added to the request set.  Responses must be typecast to the
        /// appropriate object type before they can be used.
        /// </summary>
        /// <param name="index">The index of the desired response object</param>
        /// <returns>A response object that must be typecast to be used</returns>
        public IQBBase getResponse(int index)
        {
            return getResponse(true, index);
        }

        /// <summary>
        /// Returns the desired response within the response list.  The passed value identifies the
        /// response to return as a zero based array index.  A Response index typically correlates with 
        /// the order that its request was added to the request set.  Responses must be typecast to the
        /// appropriate object type before they can be used.
        /// </summary>
        /// <param name="logError">When true, exceptions are logged</param>
        /// <param name="index">The index of the desired response object</param>
        /// <returns>A response object that must be typecast to be used</returns>
        private IQBBase getResponse(bool logError, int index)
        {
            try
            {
                if (index >= getResponseCount(false))
                {
                    if (logError)
                    {
                        Logger.Fatal($"SessionManager.getResponse{Environment.NewLine}You are requesting an object that does not exist: " + index);
                        //logger.logCritical("SessionManager.getResponse", "You are requesting an object that does not exist: " + index);
                    }
                    throw new Exception("You are requesting an object that does not exist: " + index);
                }

                // Obtain the results of the report.  In this sample, only one request was made in the 
                // original set.  Therefore, we can pull the request data directly from index 0.
                if (_queryResponse.ResponseList == null || _queryResponse.ResponseList.GetAt(index) == null)
                {
                    if (logError)
                    {
                        Logger.Fatal($"SessionManager.getResponse{Environment.NewLine}Detected a problem trying to obtain the response object");
                        //logger.logCritical("SessionManager.getResponse", "Detected a problem trying to obtain the response object");
                    }
                    throw new Exception("Detected a problem trying to obtain the response object");
                }

                return getResponseList(false).GetAt(index).Detail;
            }
            catch (Exception e)
            {
                if (logError)
                {
                    Logger.Fatal("SessionManager.getResponse", e);
                    //logger.logCritical("SessionManager.getResponse", e.Message);
                }
                throw e;
            }
        }


        // ************************************************************************
        // ************************************************************************
        // ***                Subscription Request and Responses                ***
        // ************************************************************************
        // ************************************************************************

        /// <summary>
        /// Returns a new subscription request message set object.
        /// </summary>
        /// <returns>The message set object into which requests can be added</returns>
        public ISubscriptionMsgSetRequest getSubscriptionMsgSetRequest()
        {
            ISubscriptionMsgSetRequest rset = getSubscriptionMsgSetRequest(true);
            return rset;
        }

        /// <summary>
        /// Returns a new subscription request message set object.
        /// </summary>
        /// <param name="logError">When true, exceptions are logged</param>
        /// <returns>The subscription message set object into which requests can be added</returns>
        private ISubscriptionMsgSetRequest getSubscriptionMsgSetRequest(bool logError)
        {
            try
            {
                openConnection();
                ISubscriptionMsgSetRequest rset = _qbSessionManager.CreateSubscriptionMsgSetRequest(_qbSDKMajorVer, _qbSDKMinorVer);
                //ISubscriptionMsgSetRequest rset = _sessionMgr.CreateSubscriptionMsgSetRequest(_qbSDKMajorVer, _qbSDKMinorVer);

                if (rset == null)
                {
                    if (logError)
                        Logger.Fatal($"SessionManager.getSubscriptionMsgSetRequest{Environment.NewLine}Unable to create a message set");

                    throw new QBException("Unable to create a SubscriptionMessageSet");
                }

                return rset;
            }
            catch (Exception e)
            {
                if (logError)
                {
                    Logger.Fatal("SessionManager.getSubscriptionMsgSetRequest", e);
                    //logger.logCritical("SessionManager.getSubscriptionMsgSetRequest", e.Message);
                }
                throw e;
            }
        }

        /// <summary>
        /// Executes the subscription requests that have been previously added to the passed 
        /// subscription request message set object
        /// </summary>
        /// <param name="requestSet">The subscription request message set object containing the requests</param>
        /// <returns>False if the subscription request was successful, true otherwise</returns>
        public bool doSubscriptionRequests(ref ISubscriptionMsgSetRequest requestSet)
        {
            return doSubscriptionRequests(true, ref requestSet);
        }

        /// <summary>
        /// Executes the requests that have been previously added to the passed request message set object
        /// </summary>
        /// <param name="logError">When true, exceptions are logged</param>
        /// <param name="requestSet">The request message set object containing the requests</param>
        /// <returns>False if the subscription request was successful, true otherwise</returns>
        private bool doSubscriptionRequests(bool logError, ref ISubscriptionMsgSetRequest requestSet)
        {
            try
            {
                Logger.Info(requestSet.ToXMLString());
                //logRequestSet(ref requestSet, ENLogLevel.VERBOSE, false);

                // Execute the query...
                _querySubResp = _qbSessionManager.DoSubscriptionRequests(requestSet);

                // Check the results of the query... return false if bad
                return checkResponseStatus(ref requestSet, ref _querySubResp);
            }
            catch (Exception e)
            {
                if (logError)
                {
                    Logger.Fatal("SessionManager.doSubscriptionRequests", e);
                    //logger.logCritical("SessionManager.doSubscriptionRequests", e.Message);
                }
                throw e;
            }
        }

        /// <summary>
        /// Returns an object containing all of the subscription responses provided by Quickbooks 
        /// as a result of processing the last subscription request.
        /// </summary>
        /// <returns>An object containing subscription responses to the last request</returns>
        public IResponseList getSubscriptionResponseList()
        {
            return getSubscriptionResponseList(true);
        }

        /// <summary>
        /// Returns an object containing all of the subscription responses provided by Quickbooks 
        /// as a result of processing the last subscription request.
        /// </summary>
        /// <param name="logError">When true, exceptions are logged</param>
        /// <returns>An object containing subscription responses to the last request</returns>
        private IResponseList getSubscriptionResponseList(bool logError)
        {
            try
            {
                if (_querySubResp == null)
                {
                    if (logError)
                    {
                        Logger.Fatal($"SessionManager.getSubscriptionResponseList{Environment.NewLine}Unable to obtain the ISubscriptionMsgSetResponse as it is null");
                        //logger.logCritical("SessionManager.getSubscriptionResponseList", "Unable to obtain the ISubscriptionMsgSetResponse as it is null");
                    }
                    throw new QBException("Null ISubscriptionMsgSetResponse detected!");
                }

                return _querySubResp.ResponseList;
            }
            catch (Exception e)
            {
                if (logError)
                {
                    Logger.Fatal("SessionManager.getSubscriptionResponseList", e);
                    //logger.logCritical("SessionManager.getSubscriptionResponseList", e.Message);
                }
                throw e;
            }
        }

        /// <summary>
        /// Returns a count representing the number of subscription responses within the subscription 
        /// response object
        /// </summary>
        /// <returns>The number of subscription responses returned by Quickbooks as a result of the last request processed</returns>
        public int getSubscriptionResponseCount()
        {
            return getSubscriptionResponseCount(true);
        }

        /// <summary>
        /// Returns a count representing the number of subscription responses within the subscription 
        /// response object
        /// </summary>
        /// <param name="logError">When true, exceptions are logged</param>
        /// <returns>The number of subscription responses returned by Quickbooks as a result of the last request processed</returns>
        private int getSubscriptionResponseCount(bool logError)
        {
            try
            {
                return getSubscriptionResponseList(false).Count;
            }
            catch (Exception e)
            {
                if (logError)
                {
                    Logger.Fatal("SessionManager.getSubscriptionResponseCount", e);
                    //logger.logCritical("SessionManager.getSubscriptionResponseCount", e.Message);
                }
                throw e;
            }
        }

        /// <summary>
        /// Returns the desired subscription response within the response list.  The passed value identifies the
        /// response to return as a zero based array index.  A Response index typically correlates with 
        /// the order that its request was added to the request set.  Responses must be typecast to the
        /// appropriate object type before they can be used.
        /// </summary>
        /// <param name="index">The index of the desired subscription response object</param>
        /// <returns>A subscription response object that must be typecast to be used</returns>
        public IQBBase getSubscriptionResponse(int index)
        {
            return getSubscriptionResponse(true, index);
        }

        /// <summary>
        /// Returns the desired subscription response within the response list.  The passed value identifies the
        /// response to return as a zero based array index.  A Response index typically correlates with 
        /// the order that its request was added to the request set.  Responses must be typecast to the
        /// appropriate object type before they can be used.
        /// </summary>
        /// <param name="logError">When true, exceptions are logged</param>
        /// <param name="index">The index of the desired subscription response object</param>
        /// <returns>A subscription response object that must be typecast to be used</returns>
        private IQBBase getSubscriptionResponse(bool logError, int index)
        {
            try
            {
                if (index >= getSubscriptionResponseCount(false))
                {
                    if (logError)
                    {
                        Logger.Fatal($"SessionManager.getSubscriptionResponse{Environment.NewLine}You are requesting an object that does not exist: " + index);
                        //logger.logCritical("SessionManager.getSubscriptionResponse", "You are requesting an object that does not exist: " + index);
                    }
                    throw new Exception("You are requesting an object that does not exist: " + index);
                }

                // Obtain the results of the report.  In this sample, only one request was made in the 
                // original set.  Therefore, we can pull the request data directly from index 0.
                if (_querySubResp.ResponseList == null || _querySubResp.ResponseList.GetAt(index) == null)
                {
                    if (logError)
                    {
                        Logger.Fatal($"SessionManager.getSubscriptionResponse{Environment.NewLine}Detected a problem trying to obtain the subscription response object");
                        //logger.logCritical("SessionManager.getSubscriptionResponse", "Detected a problem trying to obtain the subscription response object");
                    }
                    throw new Exception("Detected a problem trying to obtain the subscription response object");
                }

                return getSubscriptionResponseList(false).GetAt(index).Detail;
            }
            catch (Exception e)
            {
                if (logError)
                {
                    Logger.Fatal("SessionManager.getSubscriptionResponse", e);
                    //logger.logCritical("SessionManager.getSubscriptionResponse", e.Message);
                }
                throw e;
            }
        }

        // ************************************************************************
        // ************************************************************************
        // ***                  Subscription Helper Methods                     ***
        // ************************************************************************
        // ************************************************************************

        /// <summary>
        /// Checks the response and logs accordingly.
        /// </summary>
        /// <param name="requestSet">The initial request</param>
        /// <param name="responseSet">The response</param>
        /// <returns>False if the request was successful, true otherwise</returns>
        private bool checkResponseStatus(ref ISubscriptionMsgSetRequest requestSet, ref ISubscriptionMsgSetResponse responseSet)
        {
            bool bErrorDetected = false;
            // Log the Response Set
            Logger.Info(responseSet.ToXMLString());
            //logResponseSet(ref responseSet, ENLogLevel.VERBOSE, false);

            try
            {
                // Determine the number of responses that have been returned and log
                // this information
                IResponseList responseList = responseSet.ResponseList;
                int numResp = responseList.Count;
                Logger.Info("Number of responses in the set: " + numResp);
                //logger.logInfo("Number of responses in the set: " + numResp);

                for (int i = 0; i < numResp; ++i)
                {
                    IResponse response = responseList.GetAt(i);
                    if (response == null)
                    {
                        // There is a problem as this index *should* have a response!
                        Logger.Fatal($"SessionManager.checkResponseStatus(Subscription){Environment.NewLine} Error: Unable to find Response Index " + i + " of " + numResp);
                        Logger.Fatal($"SessionManager.checkResponseStatus(Subscription){Environment.NewLine} {responseSet.ToXMLString()}");
                        //logger.logCritical("SessionManager.checkResponseStatus(Subscription)", "Error: Unable to find Response Index " + i + " of " + numResp);
                        //logger.logCritical("SessionManager.checkResponseStatus(Subscription)", responseSet.ToXMLString());
                        throw new QBNoResponseException(i, "No response object was found for index: " + i);
                    }

                    if (response.StatusCode == 0)
                        continue;

                    // An error was detected... Determine the severity of the error...
                    if (response.StatusSeverity.ToUpper() == "INFO")
                    {
                        Logger.Info(responseSet.ToXMLString());
                        //logResponseSet(ref responseSet, ENLogLevel.VERBOSE, true);
                    }
                    else if (response.StatusSeverity.ToUpper() == "WARNING")
                    {
                        // Log the request and response if currently in logging mode...
                        Logger.Info(requestSet.ToXMLString());
                        Logger.Info(responseSet.ToXMLString());

                        Logger.Info("Warning detected for index: " + i +
                                       "\n\tCode: " + response.StatusCode +
                                       "\n\tMessage: " + response.StatusMessage
                                       );
                        //logRequestSet(ref requestSet, ENLogLevel.VERBOSE, true);
                        //logResponseSet(ref responseSet, ENLogLevel.VERBOSE, true);

                        //logger.logInfo("Warning detected for index: " + i +
                        //               "\n\tCode: " + response.StatusCode +
                        //               "\n\tMessage: " + response.StatusMessage
                        //               );
                    }
                    else if (response.StatusSeverity.ToUpper() == "ERROR")
                    {
                        bErrorDetected = true;

                        Logger.Error(requestSet.ToXMLString());
                        Logger.Error(responseSet.ToXMLString());

                        Logger.Error("Error detected for index: " + i +
                                        "\n\tCode: " + response.StatusCode +
                                        "\n\tMessage: " + response.StatusMessage + "\n"
                                        );

                        //logRequestSet(ref requestSet, ENLogLevel.ERROR, true);
                        //logResponseSet(ref responseSet, ENLogLevel.ERROR, true);

                        //logger.logError("Error detected for index: " + i +
                        //                "\n\tCode: " + response.StatusCode +
                        //                "\n\tMessage: " + response.StatusMessage + "\n"
                        //                );
                    }
                    else
                    {
                        bErrorDetected = true;

                        Logger.Fatal(requestSet.ToXMLString());
                        Logger.Fatal(responseSet.ToXMLString());
                        Logger.Fatal("Unknown StatusSeverity, " + response.StatusSeverity + " for index: " + i +
                                           "\n\tCode: " + response.StatusCode +
                                           "\n\tMessage: " + response.StatusMessage
                                          );
                        Logger.Fatal($"SessionManager.checkResponseStatus(Subscription){Environment.NewLine}Unknown StatusSeverity, " + response.StatusSeverity + " for index: " + i);
                        //logRequestSet(ref requestSet, ENLogLevel.CRITICAL, true);
                        //logResponseSet(ref responseSet, ENLogLevel.CRITICAL, true);
                        //logger.logCritical("Unknown StatusSeverity, " + response.StatusSeverity + " for index: " + i +
                        //                   "\n\tCode: " + response.StatusCode +
                        //                   "\n\tMessage: " + response.StatusMessage
                        //                  );
                        //logger.logCritical("SessionManager.checkResponseStatus(Subscription)", "Unknown StatusSeverity, " + response.StatusSeverity + " for index: " + i);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Fatal("SessionManager.checkResponseStatus(Subscription)", e);
                //logger.logCritical("SessionManager.checkResponseStatus(Subscription)", e.Message);
                throw e;
            }

            return bErrorDetected;
        }



        // ************************************************************************
        // ************************************************************************
        // ***                         Property access                          ***
        // ************************************************************************
        // ************************************************************************

        /// <summary>
        /// Allows the developer to determine if a connection is currently open. 
        /// This property may only be read.
        /// </summary>
        public bool IsConnectionOpen
        {
            get
            {
                return _connOpen;
            }
        }

        // ************************************************************************
        // ************************************************************************
        // ***                     Normal Helper Methods                        ***
        // ************************************************************************
        // ************************************************************************

        // ************************************************************************
        // ************************************************************************
        // ***                  Subscription Helper Methods                     ***
        // ************************************************************************
        // ************************************************************************




        /// <summary>
        /// Checks the response and logs accordingly.
        /// </summary>
        /// <param name="requestSet">The initial request (by reference)</param>
        /// <param name="responseSet">The response (by reference)</param>
        /// <returns>False if the request was successful, true otherwise</returns>
        private bool checkResponseStatus(ref IMsgSetRequest requestSet, ref IMsgSetResponse responseSet)
        {
            bool bErrorDetected = false;
            // Log the Response Set
            Logger.Info(responseSet.ToXMLString());
            //logResponseSet(ref responseSet, ENLogLevel.VERBOSE, false);

            try
            {
                // Determine the number of responses that have been returned and log
                // this information
                IResponseList responseList = responseSet.ResponseList;
                int numResp = responseList.Count;
                Logger.Info(@"Number of responses in the set: " + numResp);
                //logger.logInfo("Number of responses in the set: " + numResp);

                for (int i = 0; i < numResp; ++i)
                {
                    IResponse response = responseList.GetAt(i);
                    if (response == null)
                    {
                        // There is a problem as this index *should* have a resonse!
                        Logger.Fatal($"SessionManager.checkResponseStatus(Message){Environment.NewLine}Error: Unable to find Response Index " + i + " of " + numResp);
                        Logger.Fatal($"SessionManager.checkResponseStatus(Message){Environment.NewLine}{responseSet.ToXMLString()}");
                        //logger.logCritical("SessionManager.checkResponseStatus(Message)", "Error: Unable to find Response Index " + i + " of " + numResp);
                        //logger.logCritical("SessionManager.checkResponseStatus(Message)", responseSet.ToXMLString());
                        throw new QBNoResponseException(i, "No response object was found for index: " + i);
                    }

                    if (response.StatusCode == 0)
                        continue;

                    // An error was detected... Determine the severity of the error...
                    if (response.StatusSeverity.ToUpper() == "INFO")
                    {
                        Logger.Info(responseSet.ToXMLString());
                        //logResponseSet(ref responseSet, ENLogLevel.VERBOSE, true);
                    }
                    else if (response.StatusSeverity.ToUpper() == "WARNING")
                    {
                        Logger.Info(requestSet.ToXMLString());
                        Logger.Info(responseSet.ToXMLString());
                        //logRequestSet(ref requestSet, ENLogLevel.VERBOSE, true);
                        //logResponseSet(ref responseSet, ENLogLevel.VERBOSE, true);

                        Logger.Info("Warning detected for index: " + i +
                                           "\n\tCode: " + response.StatusCode +
                                           "\n\tMessage: " + response.StatusMessage
                                           );
                        //logger.logInfo("Warning detected for index: " + i +
                        //               "\n\tCode: " + response.StatusCode +
                        //               "\n\tMessage: " + response.StatusMessage
                        //               );
                    }
                    else if (response.StatusSeverity.ToUpper() == "ERROR")
                    {
                        bErrorDetected = true;

                        Logger.Error(requestSet.ToXMLString());
                        Logger.Error(responseSet.ToXMLString());
                        //logRequestSet(ref requestSet, ENLogLevel.ERROR, true);
                        //logResponseSet(ref responseSet, ENLogLevel.ERROR, true);

                        Logger.Error("Error detected for index: " + i +
                                        "\n\tCode: " + response.StatusCode +
                                        "\n\tMessage: " + response.StatusMessage + "\n"
                                        );
                    }
                    else
                    {
                        bErrorDetected = true;

                        // Log the request and response if currently in logging mode...
                        Logger.Fatal(requestSet.ToXMLString());
                        Logger.Fatal(responseSet.ToXMLString());
                        //logRequestSet(ref requestSet, ENLogLevel.CRITICAL, true);
                        //logResponseSet(ref responseSet, ENLogLevel.CRITICAL, true);
                        Logger.Fatal("Unknown StatusSeverity, " + response.StatusSeverity + " for index: " + i +
                                           "\n\tCode: " + response.StatusCode +
                                           "\n\tMessage: " + response.StatusMessage
                                          );
                        Logger.Fatal($"SessionManager.checkResponseStatus(Message): Unknown StatusSeverity, " + response.StatusSeverity + " for index: " + i);
                        //logger.logCritical("Unknown StatusSeverity, " + response.StatusSeverity + " for index: " + i +
                        //                   "\n\tCode: " + response.StatusCode +
                        //                   "\n\tMessage: " + response.StatusMessage
                        //                  );
                        //logger.logCritical("SessionManager.checkResponseStatus(Message)", "Unknown StatusSeverity, " + response.StatusSeverity + " for index: " + i);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Fatal("SessionManager.checkResponseStatus(Message)", e);
                //logger.logCritical("SessionManager.checkResponseStatus(Message)", e.Message);
                throw e;
            }

            return bErrorDetected;
        }

    }
}
