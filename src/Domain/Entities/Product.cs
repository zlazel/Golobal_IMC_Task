using Golobal_IMC_Task.Domain.Common;
using Golobal_IMC_Task.Domain.Enums;
using System;

namespace Golobal_IMC_Task.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
    }
}
