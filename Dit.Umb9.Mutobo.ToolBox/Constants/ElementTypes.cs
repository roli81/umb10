using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dit.Umb9.Mutobo.ToolBox.Constants
{
    public static class ElementTypes
    {
        #region configuration

        public static class FooterConfiguration
        {
            public const string Alias = "footerConfiguration";

            public static class Fields
            {
                public const string NavigationArea = "navigationArea";
                public const string ContactArea = "contactArea";
                public const string Links = "footerLinks";
                public const string BlockLinks = "blockLinks";
                public const string PictureLinks = "pictureLinks";
                public const string CopyRight = "copyRight";
            }
        }

        public static class HeaderConfiguration {

            public const string Logo = "logo";
            public const string Link = "logoUrl";
        }

        public static class MailConfiguration
        {
            public const string Alias = "mailConfiguration";

            public static class Fields
            {
                public const string MailSubject = "mailSubject";
                public const string MailBody = "mailBody";
                public const string MailHeader = "mailHeader";
                public const string MailFooter = "mailFooter";
                public const string SenderMail = "senderMail";
                public const string ReceiverMail = "receiverMail";
            }
        }


        #endregion

        #region footer
        public static class FooterContact
        {
            public const string Headline = "headline";
            public const string AddressBlock = "addressBlock";
        }

        public static class FooterNavBlock
        {
            public const string StartNode = "startNode";
        }
        #endregion


        #region modules


        public static class Quote
        {
            public const string Alias = "quote";

            public static class Fields
            {
                public const string QuoteText = "quoteText";
                public const string SpellerName = "spellerName";
                public const string SpellerFunction = "spellerFunction";
            }
        }

        public static class PictureModule
        {
            public const string Alias = "pictureModule";

            public static class Fields
            {
                public const string Image = "image";
                public const string Height = "height";
                public const string Width = "width";
            }
        }

        public static class SliderComponent
        {
            public const string Alias = "sliderComponent";

            public static class Fields
            {
                public const string Slides = "slides";
                public const string Height = "height";
                public const string Width = "width";
                public const string Interval = "interval";
                public const string DisplayType = "displayType";
            }
        }

        public static class Teaser
        {
            public const string Alias = "teaser";

            public static class Fields
            {
                public const string Link = "link";
                public const string Images = "images";
                public const string UseArticleData = "useArticleData";
                public const string TeaserTitle = "teaserTitle";
                public const string TeaserText = "teaserText";
                public const string RenderAs = "renderAs";

            }

        }

        public static class RichTextComponent
        {
            public const string Alias = "richTextComponent";

            public static class Fields
            {
                public const string RichText = "richText";

            }
        }


        public static class Heading
        {
            public const string Alias = "heading";

            public static class Fields
            {
                public const string Text = "text";
                public const string RenderAs = "renderAs";
                public const string NavigationAnchor = "navigationAnchor";
            }
        }

        public static class VideoComponent
        {
            public const string Alias = "videoComponent";

            public static class Fields
            {
                public const string VideoFile = "videoFile";
                public const string Embedded = "embedded";
                public const string Text = "text";
                public const string Height = "height";
                public const string Width = "width";
            }
        }

        public static class DoubleSliderComponent
        {

            public const string Alias = "doubleSlider";

            public static class Fields
            {
                public const string Slides = "slides";
                public const string Height = "height";
                public const string Width = "width";
                public const string Interval = "interval";
                public const string DisplayType = "displayType";
            }
        }

        public static class TextImageSlide
        {
            public const string Alias = "textImageSlide";

            public static class Fields
            {
                public const string Link = "link";
                public const string Image = "image";
                public const string Text = "text";
                public const string Title = "title";
            }

        }


        public static class Accordeon
        {
            public const string Alias = "accordeon";
            public static class Fields
            {
                public const string Summary = "summary";
                public const string Details = "details";
            }
        }

        public static class CardContainer
        {
            public const string Alias = "cardContainer";

            public static class Fields
            {
                public const string Cards = "cards";
            }
        }

        public static class Card
        {
            public const string Alias = "card";

            public static class Fields
            {
                public const string DetailPageLink = "detailPageLink";
                public const string Image = "image";
            }
        }


        public static class PersonalCard
        {
            public const string Alias = "personalCard";

            public static class Fields
            {
                public const string Lastname = "lastname";
                public const string Firstname = "firstname";
                public const string Function = "function";
                public const string Description = "description";
                public const string IsMainPerson = "isMainPerson";
                public const string DisplayType = "displayType";
            }
        }

        public static class CallToActionButton
        {

            public const string Alias = "callToActionButton";

            public static class Fields
            {
                public const string Title = "title";
                public const string Text = "text";
                public const string Link = "link";
            }
        }


        public static class Flyer
        {
            public const string Alias = "flyer";

            public static class Fields
            {
                public const string Link = "link";
                public const string Color = "color";
                public const string Direction = "direction";
                public const string Width = "width";
                public const string Height = "height";
                public const string Rotation = "rotation";
                public const string Timer = "timer";
                public const string Position = "position";
                public const string FlyerTitle = "flyerTitle";
                public const string FlyerImage = "flyerImage";
                public const string FlyerTeaserText = "flyerTeaserText";
                public const string FlyerLink = "website";
                public const string MarginTop = "marginTop";
                public const string MarginSide = "marginSide";
                public const string TextHeight = "textHeight";
                public const string TextWidth = "textWidth";
            }
        }

        #endregion


        public static class Picture
        {
            public const string Alias = "pictureModule";

            public static class Fields
            {
                public const string Image = "image";
            }
        }


        public static class SearchResults
        {
            public static string Alias = "searchResults";

            public static class Fields
            {
                // BasePage
                public const string PageTitle = "pageTitle";
            }
        }


        public static class PictureLink
        {
            public const string Alias = "pictureLink";

            public static class Fields
            {
                public const string Image = "image";
                public const string Link = "link";
                public const string LogoLink = "logoLink";
                public const string Text = "text";
                public const string MaxHeight = "maxHeight";
                public const string PaddingTop = "paddingTop";
                public const string PaddingRight = "paddingRight";
                public const string PaddingBottom = "paddingBottom";
                public const string PaddingLeft = "paddingLeft";
            }
        }
    }
}
