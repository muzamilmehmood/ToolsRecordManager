using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Record_Manager
{
    public class UsersModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public double PhoneNumber { get; set; }

        public string Location { get; set; }

        public string ToolName { get; set; }

        public string DOR { get; set; }

        public string DOE { get; set; }

        public string PaymentMode { get; set; }

        public int Amount { get; set; }

        public string Payment { get; set; }
    }
}
