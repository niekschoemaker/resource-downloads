using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceDownloads.Models
{
    public class TebexPayment
    {
        public virtual Payment Payment { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual List<Coupon> Coupons { get; set; }
        public virtual List<GiftCard> GiftCards { get; set; }
        public virtual List<Package> Packages { get; set; }
    }

    public class Payment
    {
        [JsonProperty("txn_id")]
        public string TxnId { get; set; }
        public long Timestamp { get; set; }
        public string Date { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public string Gateway { get; set; }
        public string Status { get; set; }
    }

    public class Customer
    {
        public string Email { get; set; }
        public string Country { get; set; }
        public string Ign { get; set; }
        public string Uuid { get; set; }
        public string Ip { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class Coupon
    {
        public string Code { get; set; }
        public string Type { get; set; }
        [JsonProperty("discount_type")]
        public string DiscountType { get; set; }
        [JsonProperty("discount_percentage")]
        public int DiscountPercentage { get; set; }
        [JsonProperty("discount_amount")]
        public double DiscountAmount { get; set; }
    }

    public class GiftCard
    {
        [JsonProperty("gift_card")]
        public long GiftCardId { get; set; }
        public string Amount { get; set; }
    }

    public class Package
    {
        [JsonProperty("package_id")]
        public int PackageId { get; set; }
        public string Name { get; set; }
        [JsonProperty("purchase_data")]
        public virtual PurchaseData PurchaseData { get; set; }
        public virtual List<Variable> Variables { get; set; }
    }

    public class PurchaseData
    {
        public int Payment { get; set; }
        public int Package { get; set; }
        public int Quanity { get; set; }
        public int Expire { get; set; }
        public string Server { get; set; }
        public double Price { get; set; }
        public string Notified { get; set; }
        [JsonProperty("base_price")]
        public double BasePrice { get; set; }
    }

    public class Variable
    {
        public string Identifier { get; set; }
        public string Option { get; set; }
    }
}
