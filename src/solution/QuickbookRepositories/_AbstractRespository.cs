using ApplicationLogic.Quickbooks;
using Framework.Logging.Log4Net;
using Interop.QBFC13;
using System;

namespace QuickbookRepositories
{
    public class AbstractRespository
    {
        protected LoggerCustom Logger = Framework.Logging.Log4Net.LoggerFactory.Create(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AbstractRespository(SessionManager sessionManager)
        {
            this.SessionManager = sessionManager;
        }

        public SessionManager SessionManager { get; }


        protected IMsgSetResponse processRequestFromQB(IMsgSetRequest requestSet)
        {
            try
            {
                //MessageBox.Show(requestSet.ToXMLString());
                IMsgSetResponse responseSet = this.SessionManager.doRequest(true, ref requestSet);
                //MessageBox.Show(responseSet.ToXMLString());
                return responseSet;
            }
            catch (Exception e)
            {
                this.Logger.Error(e);
                return null;
            }
        }

        protected IMsgSetRequest buildDataCountQuery(string request)
        {
            IMsgSetRequest requestMsgSet = this.SessionManager.getMsgSetRequest();
            requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;
            switch (request)
            {
                case "CustomerQueryRq":
                    ICustomerQuery custQuery = requestMsgSet.AppendCustomerQueryRq();
                    custQuery.metaData.SetValue(ENmetaData.mdMetaDataOnly);
                    break;
                case "ItemQueryRq":
                    IItemQuery itemQuery = requestMsgSet.AppendItemQueryRq();
                    itemQuery.metaData.SetValue(ENmetaData.mdMetaDataOnly);
                    break;
                case "TermsQueryRq":
                    ITermsQuery termsQuery = requestMsgSet.AppendTermsQueryRq();
                    termsQuery.metaData.SetValue(ENmetaData.mdMetaDataOnly);
                    break;
                case "SalesTaxCodeQueryRq":
                    ISalesTaxCodeQuery salesTaxQuery = requestMsgSet.AppendSalesTaxCodeQueryRq();
                    salesTaxQuery.metaData.SetValue(ENmetaData.mdMetaDataOnly);
                    break;
                case "CustomerMsgQueryRq":
                    ICustomerMsgQuery custMsgQuery = requestMsgSet.AppendCustomerMsgQueryRq();
                    custMsgQuery.metaData.SetValue(ENmetaData.mdMetaDataOnly);
                    break;
                default:
                    break;
            }
            return requestMsgSet;
        }

        protected int parseRsForCount(IMsgSetResponse responseMsgSet)
        {
            int ret = -1;
            try
            {
                IResponse response = responseMsgSet.ResponseList.GetAt(0);
                ret = response.retCount;
            }
            catch (Exception e)
            {
                this.Logger.Error("Error encountered ", e);
                ret = -1;
            }
            return ret;
        }
    }
}