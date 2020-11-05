using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HW6.Models.ViewModels
{
    public class ItemDetailsViewModel
    {
        public ItemDetailsViewModel(StockItem item)
        {
            //ItemID = item.StockItemID;
            ItemName = item.StockItemName;
            ItemSize = item.Size;
            RecRetailPrice = item.RecommendedRetailPrice;
            WeightPerUnit = item.TypicalWeightPerUnit;
            ItemLeadTime = item.LeadTimeDays;
            ValidSince = item.ValidFrom;
            //Using Jobject
            string OrigonalString = item.CustomFields;
            JObject JParse = JObject.Parse(OrigonalString);
            Origin = (string)JParse["CountryOfManufacture"];
            OrigonalString = item.Tags;
            ItemTags = item.Tags;
            ItemPhoto = item.Photo;

            //Supplier Profile
            Company = item.Supplier.SupplierName;
            Phone = item.Supplier.PhoneNumber;
            Fax = item.Supplier.FaxNumber;
            Web = item.Supplier.WebsiteURL;
            ContactName = item.Supplier.Person2.FullName;

            //Sale History Summary
            Orders = item.InvoiceLines.Count();
            GrossSale = item.InvoiceLines.Select(p => p.ExtendedPrice).Sum();
            GrossProfit = item.InvoiceLines.Select(p => p.LineProfit).Sum();
            //Top Customers
            CustomerName  = item.InvoiceLines.GroupBy(i => i.Invoice.Customer.CustomerName)
                .Select(g => new { CustomerName = g.Key, QuantityByCustomer = g.Sum(i => i.Quantity)})
                .OrderByDescending(i => i.QuantityByCustomer).Take(10).ToString();
        }

        //Stock Item
        public string ItemName { get; private set; }

        [StringLength(20)]
        public string ItemSize { get; private set; }
        public decimal? RecRetailPrice { get; set; }
        public decimal WeightPerUnit { get; set; }
        public int ItemLeadTime { get; private set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/yyyy}")]
        public DateTime ValidSince { get; private set; }
        public string Origin { get; private set; }
        public string ItemTags { get; private set; }
        public byte[] ItemPhoto { get; private set; }

        //Supplier Profile
        public string Company { get; private set; }
        public string Phone { get; private set; }
        public string Fax { get; private set; }
        [Url]
        public string Web { get; private set; }
        public string ContactName { get; private set; }

        //Sale History Summary
        public int Orders { get; set; }
        public decimal? GrossSale { get; set; }
        public decimal? GrossProfit { get; set; }

        //Top Customers
        
        public string CustomerName { get; set; }
        public int QuantityByCustomer { get; set; }

        







    }
}