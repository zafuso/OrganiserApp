using Newtonsoft.Json;
using OrganiserApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganiserApp.Models
{
    public partial class Order
    {
        [JsonProperty(PropertyName = "uuid")]
        public string Uuid { get; set; }

        [JsonProperty(PropertyName = "batch_id")]
        public string BatchId { get; set; }

        [JsonProperty(PropertyName = "status")]
        public OrderStatusType Status { get; set; }

        [JsonProperty(PropertyName = "total_balance_incl_vat")]
        public decimal TotalBalanceInclVat { get; set; }

        [JsonProperty(PropertyName = "currency_id")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "is_guest_list_order")]
        public bool IsGuestListOrder { get; set; }

        [JsonProperty(PropertyName = "is_box_office_order")]
        public bool IsBoxOfficeOrder { get; set; }

        [JsonProperty(PropertyName = "is_issued")]
        public bool IsIssued { get; set; }

        [JsonProperty(PropertyName = "is_completed")]
        public bool IsCompleted { get; set; }

        [JsonProperty(PropertyName = "is_pending")]
        public bool IsPending { get; set; }

        [JsonProperty(PropertyName = "is_refunded")]
        public bool IsRefunded { get; set; }

        [JsonProperty(PropertyName = "is_canceled")]
        public bool IsCanceled { get; set; }

        [JsonProperty(PropertyName = "is_declined")]
        public bool IsDeclined { get; set; }

        [JsonProperty(PropertyName = "is_personalized")]
        public bool IsPersonalized { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "due_at")]
        public string DueAt { get; set; }

        [JsonProperty(PropertyName = "completed_at")]
        public string CompletedAt { get; set; }

        [JsonProperty(PropertyName = "email_send_at")]
        public string EmailSendAt { get; set; }

        [JsonProperty(PropertyName = "canceled_at")]
        public string CanceledAt { get; set; }

        [JsonProperty(PropertyName = "customer_data")]
        public CustomerData CustomerData { get; set; }

        [JsonProperty(PropertyName = "download_url")]
        public string DownloadUrl { get; set; }

        [JsonProperty(PropertyName = "ticket_types")]
        public List<OrderTicket> TicketTypes { get; set; }

        [JsonProperty(PropertyName = "service_fee")]
        public decimal ServiceFee { get; set; }

        [JsonProperty(PropertyName = "service_fee_order")]
        public decimal ServiceFeeOrder { get; set; }

        [JsonProperty(PropertyName = "transaction_fee")]
        public decimal TransactionFee { get; set; }

        [JsonProperty(PropertyName = "total_balance")]
        public decimal TotalBalance { get; set; }

        [JsonProperty(PropertyName = "payment_method_name")]
        public string PaymentMethodName { get; set; }

        [JsonProperty(PropertyName = "discount_balance")]
        public decimal DiscountBalance { get; set; }

        [JsonProperty(PropertyName = "discount_description")]
        public string DiscountDescription { get; set; }

        [JsonProperty(PropertyName = "time_slot")]
        public string TimeSlot { get; set; }

        [JsonProperty(PropertyName = "consents")]
        public List<Consent> Consent { get; set; }

        [JsonProperty(PropertyName = "is_refund_allowed")]
        public RefundAllowed IsRefundAllowed { get; set; }

        [JsonProperty(PropertyName = "is_refundable")]
        public bool IsRefundable { get; set; }
        public int Products { get; set; }
        public string FullName { get; set; }
        public string OrderStatus { get; set; }
        public string StatusIcon { get; set; }
        public Color StatusTextColor { get; set; }
        public DateTime Completed { get; set; }
    }

    public partial class OrderTicket
    {
        [JsonProperty(PropertyName = "uuid")]
        public string Uuid { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }

        [JsonProperty(PropertyName = "is_canceled")]
        public bool IsCanceled { get; set; }

        [JsonProperty(PropertyName = "ticket_type_price_incl_vat")]
        public decimal TicketTypePriceInclVat { get; set; }

        [JsonProperty(PropertyName = "total_balance_excl_service_fee")]
        public decimal TotalBalanceExclServiceFee { get; set; }

        [JsonProperty(PropertyName = "currency_id")]
        public string CurrencyId { get; set; }
    }

    public partial class RefundAllowed
    {
        [JsonProperty(PropertyName = "is_refundable")]
        public bool IsRefundable { get; set; }

        [JsonProperty(PropertyName = "external_code")]
        public string ExternalCode { get; set; }
    }
}
