using Dit.Umb9.Mutobo.ToolBox.Constants;
using Dit.Umb9.Mutobo.ToolBox.Enum;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Dit.Umb9.Mutobo.ToolBox.Modules
{
    public class PersonalCard : Card
    {
        public string Lastname => this.HasValue(ElementTypes.PersonalCard.Fields.Lastname)
    ? this.Value<string>(ElementTypes.PersonalCard.Fields.Lastname)
    : null;
        public string Firstname => this.HasValue(ElementTypes.PersonalCard.Fields.Firstname)
            ? this.Value<string>(ElementTypes.PersonalCard.Fields.Firstname)
            : null;
        public string Function => this.HasValue(ElementTypes.PersonalCard.Fields.Function)
            ? this.Value<string>(ElementTypes.PersonalCard.Fields.Function)
            : null;

        public string Description => this.HasValue(ElementTypes.PersonalCard.Fields.Description)
            ? this.Value<string>(ElementTypes.PersonalCard.Fields.Description)
            : null;


        public EPersonalCardDisplayType DisplayType => this.HasValue(ElementTypes.PersonalCard.Fields.DisplayType) && !string.IsNullOrEmpty(this.Value<string>(ElementTypes.PersonalCard.Fields.DisplayType)?.Trim())
            ? (EPersonalCardDisplayType)System.Enum.Parse(typeof(EPersonalCardDisplayType),
                this.Value<string>(ElementTypes.PersonalCard.Fields.DisplayType)) : EPersonalCardDisplayType.Compact;

        public bool IsMainPerson => this.Value<bool>(ElementTypes.PersonalCard.Fields.IsMainPerson);

        public PersonalCard(IPublishedElement content, PublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {



        }
    }
}
