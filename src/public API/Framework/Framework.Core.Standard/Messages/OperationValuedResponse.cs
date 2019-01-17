using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Storage.DataHolders.Messages
{
    /// <summary>
    /// Generic implementation for Operation Response Result Data Holder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Framework.Storage.DataHolders.Messages.OperationResponse" />
    public class OperationResponse<T> : OperationResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResponse{T}"/> class.
        /// </summary>
        public OperationResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResponse{T}"/> class.
        /// </summary>
        /// <param name="baseResponse">The base response.</param>
        public OperationResponse(OperationResponse baseResponse): this()
        {
            if (baseResponse != null)
            {
                this.Messages = baseResponse.Messages;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResponse{T}"/> class.
        /// </summary>
        /// <param name="bag">The bag.</param>
        /// <param name="baseResponse">The base response.</param>
        public OperationResponse(T bag, OperationResponse baseResponse): this(baseResponse)
        {
            this.Bag = bag;
        }

        /// <summary>
        /// The bag
        /// </summary>
        public T Bag;
    }
}
