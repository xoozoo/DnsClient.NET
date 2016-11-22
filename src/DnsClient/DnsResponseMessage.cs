﻿using System;
using System.Collections.Generic;
using System.Linq;
using DnsClient.Protocol;

namespace DnsClient
{
    public class DnsResponseMessage
    {
        private IList<DnsResourceRecord> _additionals = new List<DnsResourceRecord>();
        private IList<DnsResourceRecord> _answers = new List<DnsResourceRecord>();
        private IList<DnsQuestion> _questions = new List<DnsQuestion>();
        private IList<DnsResourceRecord> _servers = new List<DnsResourceRecord>();

        /// <summary>
        /// Gets a list of additional records.
        /// </summary>
        public IReadOnlyCollection<DnsResourceRecord> Additionals => _additionals.ToArray();

        /// <summary>
        /// Gets a list of answer records.
        /// </summary>
        public IReadOnlyCollection<DnsResourceRecord> Answers => _answers.ToArray();

        /// <summary>
        /// Gets a list of authority records.
        /// </summary>
        public IReadOnlyCollection<DnsResourceRecord> Authorities => _servers.ToArray();

        /// <summary>
        /// Gets a list of all answers, addtional and authority records.
        /// </summary>
        public IEnumerable<DnsResourceRecord> AllRecords
        {
            get
            {
                return Answers.Concat(Additionals).Concat(Authorities);
            }
        }

        /// <summary>
        /// Gets the header of the response.
        /// </summary>
        public DnsResponseHeader Header { get; }

        /// <summary>
        /// A flag indicating if the header contains a response codde other than <see cref="DnsResponseCode.NoError"/>.
        /// </summary>
        public bool HasError => Header?.ResponseCode != DnsResponseCode.NoError;

        /// <summary>
        /// Returns a string value representing the error response code in case an error occured, otherwise empty.
        /// </summary>
        public string ErrorMessage => HasError ? DnsResponseCodeText.GetErrorText(Header.ResponseCode) : string.Empty;

        /// <summary>
        /// Gets the list of questions.
        /// </summary>
        public IReadOnlyCollection<DnsQuestion> Questions => _questions.ToArray();

        public DnsResponseMessage(DnsResponseHeader header)
        {
            this.Header = header;
        }

        public void AddAdditional(DnsResourceRecord record)
        {
            _additionals.Add(record);
        }

        public void AddAnswer(DnsResourceRecord record)
        {
            _answers.Add(record);
        }

        public void AddQuestion(DnsQuestion question)
        {
            _questions.Add(question);
        }

        public void AddServer(DnsResourceRecord record)
        {
            _servers.Add(record);
        }
    }
}