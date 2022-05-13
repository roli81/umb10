using Dit.Umb9.Mutobo.ToolBox.Models.PoCo;
using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dit.Umb9.Mutobo.ToolBox.Interfaces
{
    public interface IVideoComponent
    {
        string Anchor { get; }
        Video Video { get; }
        string Embedded { get; }
        string Text { get; }
        int? Width { get; }
        int? Height { get; }
        IHtmlContent RenderIFrame(int? width = null, int? height = null);
    }
}
