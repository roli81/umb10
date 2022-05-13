using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dit.Umb9.Mutobo.ToolBox.Models.PoCo
{
    public class ContactFormData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public int SenderMailConfigId { get; set; }
        public int ReceiverMailConfigId { get; set; }
        public Guid LandingPageId { get; set; }

    }
}
