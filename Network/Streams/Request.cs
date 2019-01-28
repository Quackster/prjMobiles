using System;
using System.Collections.Generic;
using System.Text;

namespace Squirtle.Network.Streams
{
    class Request
    {
        private string messageHeader;
        private string messageData;

        /// <summary>
        /// Get the message header
        /// </summary>
        public string Header
        {
            get { return messageHeader; }
        }
        
        /// <summary>
        /// Get the message body
        /// </summary>
        public string Body
        {
            get { return messageData; }
        }

        /// <summary>
        /// Constructor for request class.
        /// </summary>
        /// <param name="messageHeader">the header requested</param>
        /// <param name="messageData">the data sent from client</param>
        public Request(string messageHeader, string messageData)
        {
            this.messageHeader = messageHeader;
            this.messageData = messageData;
        }
    }
}
