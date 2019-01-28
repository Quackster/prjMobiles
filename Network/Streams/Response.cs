using System;
using System.Collections.Generic;
using System.Text;

namespace Squirtle.Network.Streams
{
    public class Response
    {
        private string _header;
        private bool _finalised;

        private StringBuilder _buffer;

        /// <summary>
        /// Get the message header
        /// </summary>
        public string Header
        {
            get { return _header; }
        }
        
        /// <summary>
        /// Get the message buffer
        /// </summary>
        public StringBuilder Buffer
        {
            get { return _buffer; }
        }

        /// <summary>
        /// Get the message body
        /// </summary>
        public string Body
        {
            get { return _buffer.ToString(); }
        }

        /// <summary>
        /// Constructor for request class.
        /// </summary>
        /// <param name="messageHeader">the header requested</param>
        /// <param name="messageData">the data sent from client</param>
        public Response(string Header)
        {
            _header = Header;

            _buffer = new StringBuilder();
            _buffer.Append("#");
            _buffer.Append(_header);
        }

        /// <summary>
        /// Append a raw object to the response
        /// </summary>
        /// <param name="data">the data to send</param>
        public void Append(object data)
        {
            string value = data.ToString();
            value = value.Replace("#", "*");

            _buffer.Append(value);
        }

        /// <summary>
        /// Get the finalised message
        /// </summary>
        /// <returns>the created message</returns>
        public string GetMessage()
        {
            if (!_finalised)
            {
                _finalised = true;
                _buffer.Append("#");
                _buffer.Append("#");
            }

            return _buffer.ToString();
        }
    }
}
