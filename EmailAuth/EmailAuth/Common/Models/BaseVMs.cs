using System.Net;

namespace EmailAuth.Common.Models
{
    /// <summary>
    /// Base view model class for API responses.
    /// </summary>
    public class BaseVM
    {
        /// <summary>
        /// Gets or sets the status code of the response.
        /// </summary>
        public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;

        /// <summary>
        /// Gets or sets the list of response messages.
        /// </summary>
        public List<ResponseMessage> Messages { get; set; } = new List<ResponseMessage>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseVM"/> class.
        /// </summary>
        public BaseVM() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseVM"/> class with specified status code and messages.
        /// </summary>
        /// <param name="code">The status code of the response.</param>
        /// <param name="messages">The response messages.</param>
        public BaseVM(HttpStatusCode code, params ResponseMessage[] messages)
        {
            Status = code;
            Messages = messages?.ToList() ?? new List<ResponseMessage>();
        }
    }

    /// <summary>
    /// Generic base view model class for API responses with data.
    /// </summary>
    /// <typeparam name="T">The type of data in the response.</typeparam>
    public class BaseVM<T> : BaseVM
    {
        /// <summary>
        /// Gets or sets the data of the response.
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseVM{T}"/> class.
        /// </summary>
        public BaseVM() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseVM{T}"/> class with specified status code and messages.
        /// </summary>
        /// <param name="code">The status code of the response.</param>
        /// <param name="messages">The response messages.</param>
        public BaseVM(HttpStatusCode code, params ResponseMessage[] messages) : base(code, messages) { }
    }

    /// <summary>
    /// Model representing a response message.
    /// </summary>
    public class ResponseMessage
    {
        /// <summary>
        /// Gets or sets the message text.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the description of the message.
        /// </summary>
        public string Description { get; set; }
    }
}
