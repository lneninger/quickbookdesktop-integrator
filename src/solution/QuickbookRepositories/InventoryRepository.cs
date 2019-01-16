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
    public class InventoryRepository : AbstractRespository, IInventoryRepository
    {
        public InventoryRepository(SessionManager sessionManager): base(sessionManager)
        {

        }
        public void Request()
        {
            string request = "ItemInventoryQueryRq";
            //connectToQB();
            int count = getCount(request);
            try
            {
                //MessageBox.Show(requestSet.ToXMLString());
                var query = buildInventoryItemQueryRq(new string[] { }, null);
                IMsgSetResponse responseSet = this.SessionManager.doRequest(true, ref query);
                //MessageBox.Show(responseSet.ToXMLString());
                var result = parseInventoryItemQueryRs(responseSet, count, 1);
            }
            catch (Exception e)
            {
                this.Logger.Error(e);
                //return null;
            }
            finally {
                //disconnectFromQB();
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


        private string[,] parseInventoryItemQueryRs(IMsgSetResponse responseMsgSet, int countOfRows, int arraySize)
        {
            /*
              <?xml version="1.0" ?> 
            - <QBXML>
            - <QBXMLMsgsRs>
            - <ItemQueryRs requestID="2" statusCode="0" statusSeverity="Info" statusMessage="Status OK">
            - <ItemServiceRet>
                <ListID>20000-933272655</ListID> 
                <TimeCreated>1999-07-29T11:24:15-08:00</TimeCreated> 
                <TimeModified>2007-12-15T11:32:53-08:00</TimeModified> 
                <EditSequence>1197747173</EditSequence> 
                <Name>Installation</Name> 
                <FullName>Installation</FullName> 
                <IsActive>true</IsActive> 
                <Sublevel>0</Sublevel> 
            - 	<SalesTaxCodeRef>
                    <ListID>20000-999022286</ListID> 
                    <FullName>Non</FullName> 
                </SalesTaxCodeRef>
            - 	<SalesOrPurchase>
                    <Desc>Installation labor</Desc> 
                    <Price>35.00</Price> 
            - 		<AccountRef>
                        <ListID>190000-933270541</ListID> 
                        <FullName>Construction Income:Labor Income</FullName> 
                    </AccountRef>
                </SalesOrPurchase>
              </ItemServiceRet>
              </ItemQueryRs>
              </QBXMLMsgsRs>
              </QBXML>
            */

            string[,] retVal = new string[countOfRows, arraySize];
            IResponse response = responseMsgSet.ResponseList.GetAt(0);
            int statusCode = response.StatusCode;
            if (statusCode == 0)
            {
                if (response.Detail == null)
                {
                    return null;
                }
                ENResponseType responseType = (ENResponseType)response.Type.GetValue();
                IORItemRetList OR = null;
                if (responseType == ENResponseType.rtItemQueryRs)
                {
                    OR = response.Detail as IORItemRetList;
                }
                else
                {
                    return null;
                }
                for (int i = 0; i < countOfRows; i++)
                {
                    if (OR.GetAt(i) == null) break;
                    if (OR.GetAt(i).ItemServiceRet != null)
                    {
                        string fullName = null, desc = null;
                        double price = 0.0;

                        if (OR.GetAt(i).ItemServiceRet.FullName != null)
                        {
                            fullName = OR.GetAt(i).ItemServiceRet.FullName.GetValue();
                            populateRetVal(ref retVal, i, 0, "fullName", fullName);
                        }
                        if (OR.GetAt(i).ItemServiceRet.ORSalesPurchase != null)
                        {
                            if (OR.GetAt(i).ItemServiceRet.ORSalesPurchase.SalesOrPurchase != null)
                            {
                                //Get value of Desc
                                if (OR.GetAt(i).ItemServiceRet.ORSalesPurchase.SalesOrPurchase.Desc != null)
                                {
                                    desc = (string)OR.GetAt(i).ItemServiceRet.ORSalesPurchase.SalesOrPurchase.Desc.GetValue();
                                    populateRetVal(ref retVal, i, 0, "desc", desc);
                                }
                                if (OR.GetAt(i).ItemServiceRet.ORSalesPurchase.SalesOrPurchase.ORPrice != null)
                                {
                                    if (OR.GetAt(i).ItemServiceRet.ORSalesPurchase.SalesOrPurchase.ORPrice.Price != null)
                                    {
                                        //Get value of Price
                                        price = (double)OR.GetAt(i).ItemServiceRet.ORSalesPurchase.SalesOrPurchase.ORPrice.Price.GetValue();
                                        populateRetVal(ref retVal, i, 1, "price", price.ToString());
                                    }
                                }
                            }
                        }
                    }

                    if (OR.GetAt(i).ItemNonInventoryRet != null)
                    {
                        string fullName = null, desc = null;
                        double price = 0.0;

                        if (OR.GetAt(i).ItemNonInventoryRet.FullName != null)
                        {
                            fullName = OR.GetAt(i).ItemNonInventoryRet.FullName.GetValue();
                            populateRetVal(ref retVal, i, 0, "fullName", fullName);
                        }
                        if (OR.GetAt(i).ItemNonInventoryRet.ORSalesPurchase != null)
                        {
                            if (OR.GetAt(i).ItemNonInventoryRet.ORSalesPurchase.SalesOrPurchase != null)
                            {
                                //Get value of Desc
                                if (OR.GetAt(i).ItemNonInventoryRet.ORSalesPurchase.SalesOrPurchase.Desc != null)
                                {
                                    desc = (string)OR.GetAt(i).ItemNonInventoryRet.ORSalesPurchase.SalesOrPurchase.Desc.GetValue();
                                    populateRetVal(ref retVal, i, 0, "desc", desc);
                                }
                                if (OR.GetAt(i).ItemNonInventoryRet.ORSalesPurchase.SalesOrPurchase.ORPrice != null)
                                {
                                    if (OR.GetAt(i).ItemNonInventoryRet.ORSalesPurchase.SalesOrPurchase.ORPrice.Price != null)
                                    {
                                        //Get value of Price
                                        price = (double)OR.GetAt(i).ItemNonInventoryRet.ORSalesPurchase.SalesOrPurchase.ORPrice.Price.GetValue();
                                        populateRetVal(ref retVal, i, 1, "price", price.ToString());
                                    }
                                }
                            }
                        }
                    }
                    if (OR.GetAt(i).ItemOtherChargeRet != null)
                    {
                        string fullName = null, desc = null;
                        double price = 0.0;

                        if (OR.GetAt(i).ItemOtherChargeRet.FullName != null)
                        {
                            fullName = OR.GetAt(i).ItemOtherChargeRet.FullName.GetValue();
                            populateRetVal(ref retVal, i, 0, "fullName", fullName);
                        }
                        if (OR.GetAt(i).ItemOtherChargeRet.ORSalesPurchase != null)
                        {
                            if (OR.GetAt(i).ItemOtherChargeRet.ORSalesPurchase.SalesOrPurchase != null)
                            {
                                //Get value of Desc
                                if (OR.GetAt(i).ItemOtherChargeRet.ORSalesPurchase.SalesOrPurchase.Desc != null)
                                {
                                    desc = (string)OR.GetAt(i).ItemOtherChargeRet.ORSalesPurchase.SalesOrPurchase.Desc.GetValue();
                                    populateRetVal(ref retVal, i, 0, "desc", desc);
                                }
                                if (OR.GetAt(i).ItemOtherChargeRet.ORSalesPurchase.SalesOrPurchase.ORPrice != null)
                                {
                                    if (OR.GetAt(i).ItemOtherChargeRet.ORSalesPurchase.SalesOrPurchase.ORPrice.Price != null)
                                    {
                                        //Get value of Price
                                        price = (double)OR.GetAt(i).ItemOtherChargeRet.ORSalesPurchase.SalesOrPurchase.ORPrice.Price.GetValue();
                                        populateRetVal(ref retVal, i, 1, "price", price.ToString());
                                    }
                                }
                            }
                        }
                    }
                    if (OR.GetAt(i).ItemInventoryRet != null)
                    {
                        string fullName = null, desc = null;
                        double price = 0.0;

                        if (OR.GetAt(i).ItemInventoryRet.FullName != null)
                        {
                            fullName = OR.GetAt(i).ItemInventoryRet.FullName.GetValue();
                            populateRetVal(ref retVal, i, 0, "fullName", fullName);
                        }
                        if (OR.GetAt(i).ItemInventoryRet.SalesDesc != null)
                        {
                            //Get value of Desc
                            desc = (string)OR.GetAt(i).ItemInventoryRet.SalesDesc.GetValue();
                            populateRetVal(ref retVal, i, 0, "desc", desc);
                        }
                        if (OR.GetAt(i).ItemInventoryRet.SalesPrice != null)
                        {
                            //Get value of Price
                            price = (double)OR.GetAt(i).ItemInventoryRet.SalesPrice.GetValue();
                            populateRetVal(ref retVal, i, 1, "price", price.ToString());
                        }
                    }
                    if (OR.GetAt(i).ItemInventoryAssemblyRet != null)
                    {
                        string fullName = null, desc = null;
                        double price = 0.0;

                        if (OR.GetAt(i).ItemInventoryAssemblyRet.FullName != null)
                        {
                            fullName = OR.GetAt(i).ItemInventoryAssemblyRet.FullName.GetValue();
                            populateRetVal(ref retVal, i, 0, "fullName", fullName);
                        }
                        if (OR.GetAt(i).ItemInventoryAssemblyRet.SalesDesc != null)
                        {
                            //Get value of Desc
                            desc = (string)OR.GetAt(i).ItemInventoryAssemblyRet.SalesDesc.GetValue();
                            populateRetVal(ref retVal, i, 0, "desc", desc);
                        }
                        if (OR.GetAt(i).ItemInventoryAssemblyRet.SalesPrice != null)
                        {
                            //Get value of Price
                            price = (double)OR.GetAt(i).ItemInventoryAssemblyRet.SalesPrice.GetValue();
                            populateRetVal(ref retVal, i, 1, "price", price.ToString());
                        }
                    }
                }
            }
            return retVal;
        }

        private void populateRetVal(ref string[,] retVal, int row, int col, string fieldName, string fieldValue)
        {
            switch (fieldName)
            {
                case "fullName":
                case "desc":
                    if (fieldValue != null)
                    {
                        retVal[row, col] = fieldValue;
                    }
                    break;
                case "price":
                    double price = Convert.ToDouble(fieldValue);
                    if (price != 0.0)
                    {
                        //if (exchangeRate != 1.0)
                        //{
                        //    price = price / exchangeRate;
                        //}
                        retVal[row, col] = price.ToString("N2");
                    }
                    break;
            }
        }
    }

  
}
