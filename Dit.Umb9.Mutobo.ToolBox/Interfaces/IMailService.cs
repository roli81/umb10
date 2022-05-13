using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dit.Umb9.Mutobo.ToolBox.Interfaces
{
    public interface IMailService
    {
        void SendConfirmationMail(ContactFormData model);
        void SendContactMail(ContactFormData model);
    }
}
