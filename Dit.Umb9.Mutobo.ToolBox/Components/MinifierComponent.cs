using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.WebAssets;

namespace Dit.Umb9.Mutobo.ToolBox.Components
{
    public class MinifierComponent : IComponent
    {
        private readonly IRuntimeMinifier _runtimeMinifier;

        public MinifierComponent(IRuntimeMinifier runtimeMinifier) => _runtimeMinifier = runtimeMinifier;
        public void Initialize()
        {
            
            _runtimeMinifier.CreateCssBundle("inline-css-bundle",
                BundlingOptions.OptimizedNotComposite,
                new[] { "~/web-components-toolbox/src/css/initial.css", 
                    "~/web-components-toolbox/src/css/reset.css", 
                    "~/web-components-toolbox/src/css/colors.css", 
                    "~/web-components-toolbox/src/css/fonts.css", 
                    "~/web-components-toolbox/src/css/variables.css" });

        }

        public void Terminate()
        {

        }
    }
}
