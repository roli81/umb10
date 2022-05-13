using Dit.Umb9.Mutobo.ToolBox.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Dit.Umb9.Mutobo.ToolBox.Models.Config
{
    public class MailConfiguration : ContentModel
    {

        public string MailSubject => this.Content.HasValue(ElementTypes.MailConfiguration.Fields.MailSubject)
            ? this.Content.Value<string>(ElementTypes.MailConfiguration.Fields.MailSubject) : null;
        public string MailBody => this.Content.HasValue(ElementTypes.MailConfiguration.Fields.MailBody)
            ? this.Content.Value<string>(ElementTypes.MailConfiguration.Fields.MailBody) : null;
        public string MailHeader => this.Content.HasValue(ElementTypes.MailConfiguration.Fields.MailHeader)
            ? this.Content.Value<string>(ElementTypes.MailConfiguration.Fields.MailHeader) : null;
        public string MailFooter => this.Content.HasValue(ElementTypes.MailConfiguration.Fields.MailFooter)
            ? this.Content.Value<string>(ElementTypes.MailConfiguration.Fields.MailFooter) : null;
        public string SenderMail => this.Content.HasValue(ElementTypes.MailConfiguration.Fields.SenderMail)
            ? this.Content.Value<string>(ElementTypes.MailConfiguration.Fields.SenderMail) : null;
        public string ReceiverMail => this.Content.HasValue(ElementTypes.MailConfiguration.Fields.ReceiverMail)
            ? this.Content.Value<string>(ElementTypes.MailConfiguration.Fields.ReceiverMail) : null;




        public MailConfiguration(IPublishedContent content) : base(content)
        {


        }
    }
}
