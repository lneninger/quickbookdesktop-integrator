using ApplicationLogic.Commands.QuickbooksIntegrator.GetAccountById.Models;
using ApplicationLogic.Commands.QuickbooksIntegrator.GetAccountByIds.Models;
using ApplicationLogic.Commands.QuickbooksIntegrator.GetInventoryItems.Models;
using ApplicationLogic.Interfaces.Repositories.Database;
using ApplicationLogic.Interfaces.Repositories.Quickbooks;
using ApplicationLogic.Quickbooks;
using DatabaseSchema;
using Interop.QBFC13;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickbookRepositories
{
    public class Properties
    {
        public const string FullName = "FullName";
        public const string Name = "Name";
        public const string IsActive = "IsActive";
        public const string IncomeAccountRef = "IncomeAccountRef";
        public const string AssetAccountRef = "AssetAccountRef";
        
        public const string QuantityOnHand = "QuantityOnHand";
        public const string AverageCost = "AverageCost";
        public const string SalesDescription = "SalesDesc";
        public const string SalesPrice = "SalesPrice";
        public const string ListID = "ListID";

    }


    public class InventoryRepository : AbstractRespository, IInventoryItemRepository
    {
        public InventoryRepository(SessionManager sessionManager) : base(sessionManager)
        {

        }
        public IEnumerable<GetInventoryItemsOutputIventoryItemDTO> GetAll()
        {
            string request = "ItemInventoryQueryRq";
            //connectToQB();
            int count = getCount(request);
            try
            {
                Logger.Info($"Parsing response with {count} Inventory Items");
                //MessageBox.Show(requestSet.ToXMLString());
                var query = buildInventoryItemQueryRq(new string[] { Properties.ListID, Properties.FullName, Properties.Name, Properties.IsActive, Properties.QuantityOnHand, Properties.AverageCost, Properties.SalesDescription, Properties.SalesPrice, Properties.IncomeAccountRef, Properties.AssetAccountRef }, null);
                IMsgSetResponse responseSet = this.SessionManager.doRequest(true, ref query);
                //MessageBox.Show(responseSet.ToXMLString());
                var result = parseInventoryItemQueryRs(responseSet, count, 1);
                return result;
            }
            catch (Exception e)
            {
                this.Logger.Error(e);
                return null;
            }
            finally
            {
                this.SessionManager.closeConnection();
            }
        }

        public IEnumerable<GetAccountByIdsOutputDTO> GetAccountById(IEnumerable<string> ids)
        {
            string requestName = "AccountQueryRq";
            //connectToQB();
            int count = getCount(requestName);
            try
            {
                //MessageBox.Show(requestSet.ToXMLString());
                IAccountQuery query = null;
                var request = buildAccountQueryRq(new string[] { Properties.ListID, Properties.FullName, Properties.Name, Properties.IsActive }, out query);

                foreach (var id in ids)
                {
                    query.ORAccountListQuery.ListIDList.Add(id);
                }

                IMsgSetResponse responseSet = this.SessionManager.doRequest(true, ref request);
                //MessageBox.Show(responseSet.ToXMLString());
                var result = parseAccountByIdsQueryRs(responseSet, ids.Count(), 1);
                return result.ToList();
            }
            catch (Exception e)
            {
                this.Logger.Error(e);
                return null;
            }
            finally
            {
                this.SessionManager.closeConnection();
            }
        }

        public GetAccountByIdOutputDTO GetAccountById(string id)
        {
            string requestName = "AccountQueryRq";
            //connectToQB();
            int count = getCount(requestName);
            try
            {
                //MessageBox.Show(requestSet.ToXMLString());
                IAccountQuery query = null;
                var request = buildAccountQueryRq(new string[] { Properties.ListID, Properties.FullName, Properties.Name, Properties.IsActive }, out query);

                query.ORAccountListQuery.ListIDList.Add(id);

                IMsgSetResponse responseSet = this.SessionManager.doRequest(true, ref request);
                //MessageBox.Show(responseSet.ToXMLString());
                var result = parseAccountQueryRs(responseSet, count, 1);
                return result;
            }
            catch (Exception e)
            {
                this.Logger.Error(e);
                return null;
            }
            finally
            {
                this.SessionManager.closeConnection();
            }
        }

        private IMsgSetRequest buildInventoryItemQueryRq(string[] includeRetElement, string fullName)
        {
            IMsgSetRequest requestMsgSet = this.SessionManager.getMsgSetRequest();
            requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;
            IItemInventoryQuery itemQuery = requestMsgSet.AppendItemInventoryQueryRq();

            for (int x = 0; x < includeRetElement.Length; x++)
            {
                itemQuery.IncludeRetElementList.Add(includeRetElement[x]);
            }
            return requestMsgSet;
        }

        private IMsgSetRequest buildAccountQueryRq(string[] includeRetElement, out IAccountQuery query)
        {
            IMsgSetRequest requestMsgSet = this.SessionManager.getMsgSetRequest();
            requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;
            query = requestMsgSet.AppendAccountQueryRq();

            for (int x = 0; x < includeRetElement.Length; x++)
            {
                query.IncludeRetElementList.Add(includeRetElement[x]);
            }
            return requestMsgSet;
        }

        private int getCount(string request)
        {
            IMsgSetResponse responseMsgSet = processRequestFromQB(buildDataCountQuery(request));
            int count = parseRsForCount(responseMsgSet);
            return count;
        }


        private IEnumerable<GetInventoryItemsOutputIventoryItemDTO> parseInventoryItemQueryRs(IMsgSetResponse responseMsgSet, int countOfRows, int arraySize)
        {
            var result = new List<GetInventoryItemsOutputIventoryItemDTO>();
            IResponseList responseList = responseMsgSet.ResponseList;
            if (responseList == null)
            {
                return result;
            }

            IResponse response = responseList.GetAt(0);

            ENResponseType responseType = (ENResponseType)response.Type.GetValue();
            IItemInventoryRetList list = null;
            int statusCode = response.StatusCode;
            if (statusCode == 0)
            {
                if (response.Detail == null)
                {
                    return null;
                }
                if (responseType == ENResponseType.rtItemInventoryQueryRs)
                {
                    list = (IItemInventoryRetList)response.Detail;
                }
                else
                {
                    return null;
                }
            }

            GetInventoryItemsOutputIventoryItemDTO resultItem = null;
            for (int i = 0; i < countOfRows; i++)
            {
                IItemInventoryRet itemInventory = list.GetAt(i);
                resultItem = new GetInventoryItemsOutputIventoryItemDTO();

                resultItem.Id = itemInventory.ListID?.GetValue();
                resultItem.FullName = itemInventory.FullName?.GetValue();
                resultItem.Name = itemInventory.Name?.GetValue();
                resultItem.SaleDescription = itemInventory.SalesDesc?.GetValue();
                resultItem.SalePrice = itemInventory.SalesPrice?.GetValue();
                resultItem.Cost = itemInventory.PurchaseCost?.GetValue();
                resultItem.Stock = itemInventory.QuantityOnHand?.GetValue();
                resultItem.IncomeAccountId = itemInventory.IncomeAccountRef?.ListID?.GetValue();
                resultItem.AssetAccountId = itemInventory.AssetAccountRef?.ListID?.GetValue();

                Logger.Info($"Parsed item {resultItem.FullName}");

                result.Add(resultItem);
            }
            return result;
        }



        private GetAccountByIdOutputDTO parseAccountQueryRs(IMsgSetResponse responseMsgSet, int countOfRows, int arraySize)
        {
            //var result = new List<GetInventoryItemsOutputIventoryItemDTO>();
            IResponseList responseList = responseMsgSet.ResponseList;
            if (responseList == null)
            {
                return null;
            }

            IResponse response = responseList.GetAt(0);

            ENResponseType responseType = (ENResponseType)response.Type.GetValue();
            IAccountRetList list = null;
            int statusCode = response.StatusCode;
            if (statusCode == 0)
            {
                if (response.Detail == null)
                {
                    return null;
                }
                if (responseType == ENResponseType.rtItemInventoryQueryRs)
                {
                    list = (IAccountRetList)response.Detail;
                }
                else
                {
                    return null;
                }
            }

            IAccountRet itemInventory = list.GetAt(0);
            GetAccountByIdOutputDTO result = new GetAccountByIdOutputDTO();

            result.ListID = itemInventory.ListID?.GetValue();
            result.FullName = itemInventory.FullName?.GetValue();
            result.Name = itemInventory.Name?.GetValue();
            result.IsActive = itemInventory.IsActive?.GetValue();

            return result;
        }


        private IEnumerable<GetAccountByIdsOutputDTO> parseAccountByIdsQueryRs(IMsgSetResponse responseMsgSet, int countOfRows, int arraySize)
        {
            var result = new List<GetAccountByIdsOutputDTO>();
            IResponseList responseList = responseMsgSet.ResponseList;
            if (responseList == null)
            {
                return result;
            }

            IResponse response = responseList.GetAt(0);

            ENResponseType responseType = (ENResponseType)response.Type.GetValue();
            IAccountRetList list = null;
            int statusCode = response.StatusCode;
            if (statusCode == 0)
            {
                if (response.Detail == null)
                {
                    return null;
                }
                if (responseType == ENResponseType.rtAccountQueryRs)
                {
                    list = (IAccountRetList)response.Detail;
                }
                else
                {
                    return null;
                }
            }

            GetAccountByIdsOutputDTO resultItem = null;
            try
            {
                for (int i = 0; i < countOfRows; i++)
                {
                    IAccountRet itemInventory = list.GetAt(i);
                    resultItem = new GetAccountByIdsOutputDTO();

                    resultItem.Id = itemInventory.ListID?.GetValue();
                    resultItem.FullName = itemInventory.FullName?.GetValue();
                    resultItem.Name = itemInventory.Name?.GetValue();
                    resultItem.IsActive = itemInventory.IsActive?.GetValue();

                    result.Add(resultItem);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }
    }


}
