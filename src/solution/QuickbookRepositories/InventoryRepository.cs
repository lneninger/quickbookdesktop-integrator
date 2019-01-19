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
        public const string QuantityOnHand = "QuantityOnHand";
        public const string AverageCost = "AverageCost";
        public const string SalesDescription = "SalesDesc";
        public const string SalesPrice = "SalesPrice";
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
                //MessageBox.Show(requestSet.ToXMLString());
                var query = buildInventoryItemQueryRq(new string[] { Properties.FullName, Properties.Name, Properties.IsActive, Properties.QuantityOnHand, Properties.AverageCost, Properties.SalesDescription, Properties.SalesPrice }, null);
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

                resultItem.FullName = itemInventory.FullName?.GetValue();
                resultItem.Name = itemInventory.Name?.GetValue();
                resultItem.SaleDescription = itemInventory.SalesDesc?.GetValue();
                resultItem.SalePrice = itemInventory.SalesPrice?.GetValue();
                resultItem.Cost = itemInventory.PurchaseCost?.GetValue();
                resultItem.Stock = itemInventory.QuantityOnHand?.GetValue();

                result.Add(resultItem);
            }
            return result;
        }
        
    }


}
