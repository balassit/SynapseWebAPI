using Microsoft.AspNetCore.OData.Routing;
using Microsoft.AspNetCore.OData.Routing.Template;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace SynapseWebAPI.Provider
{
    public class EntitySetCustomersSegment : ODataSegmentTemplate
    {
        public override IEnumerable<string> GetTemplates(ODataRouteOptions options)
        {
            yield return "/Customers";
            yield return "/Customers/$count";
        }

        public override bool TryTranslate(ODataTemplateTranslateContext context)
        {
            // Support case-insenstivie
            var edmEntitySet = context.Model.EntityContainer.EntitySets()
                .FirstOrDefault(e => string.Equals("DimCustomer", e.Name, StringComparison.OrdinalIgnoreCase));

            if (edmEntitySet != null)
            {
                bool countRequest = context.HttpContext.Request.Path.Value.EndsWith("/$count");

                EntitySetSegment segment = new EntitySetSegment(edmEntitySet);
                context.Segments.Add(segment);

                if (countRequest)
                {
                    context.Segments.Add(CountSegment.Instance);
                }

                return true;
            }

            return false;
        }
    }
}
