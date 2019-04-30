using System;

namespace ITOps.UIComposition.Mvc
{
    public sealed class UICompositionSupportAttribute : Attribute
    {
        public UICompositionSupportAttribute(string baseNamespace)
        {
            this.BaseNamespace = baseNamespace;
        }

        public string BaseNamespace { get; private set; }
    }
}
