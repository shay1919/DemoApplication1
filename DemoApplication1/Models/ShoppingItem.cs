using Microsoft.AspNetCore.Routing.Constraints;
using System.ComponentModel.DataAnnotations;

namespace DemoApplication1.Models
{
    public class ShoppingItem
    {
        // Fields
        private int _id;
        private string _name;
        private double _weight;
        private double _price;
        private double _pricePerKg;

        // Properties
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [MaxLength(50, ErrorMessage = "Item name must not be longer than 50 characters.")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [Range(0, 100, ErrorMessage = "Item weight must be between 0 and 100kg.")]
        public double Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }
        
        [Range(0, 10000, ErrorMessage = "Item price must be between £0 and £10000.")]
        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public double PricePerKg
        {
            get { return _pricePerKg; }
            set { _pricePerKg = value; }
        }
    }
}
