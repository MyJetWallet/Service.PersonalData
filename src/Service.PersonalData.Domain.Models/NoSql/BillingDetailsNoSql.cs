using MyNoSqlServer.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.PersonalData.Domain.Models.NoSql
{
    public class BillingDetailsNoSql : MyNoSqlDbEntity
    {
        public const string TableName = "myjetwallet-personal-data-billing-details";

        public static string GeneratePartitionKey(string clientId) => clientId;
        public static string GenerateRowKey(string uid) => uid;

        public BillingDetails BillingDetails { get; set; }

        public static BillingDetailsNoSql Create(string clientId, string uid, BillingDetails billingDetails)
        {
            return new BillingDetailsNoSql()
            {
                PartitionKey = GeneratePartitionKey(clientId),
                RowKey = GenerateRowKey(uid),
                BillingDetails = billingDetails,
            };
        }
    }
}
