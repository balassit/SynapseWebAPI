using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using SynapseWebAPI.Models;

namespace SynapseWebAPI.Provider
{
    public class ODataModelProvider : IODataModelProvider
    {
        private IDictionary<string, IEdmModel> _cached = new Dictionary<string, IEdmModel>();
        public IEdmModel GetEdmModel(string apiVersion)
        {
            if (_cached.TryGetValue(apiVersion, out var model))
            {
                return model;
            }

            model = BuildEdmModel(apiVersion);
            _cached[apiVersion] = model;
            return model;
        }

        private static IEdmModel BuildEdmModel(string version)
        {
            return version switch
            {
                "1.0" => BuildV1Model(),
                _ => throw new NotSupportedException($"The input version '{version}' is not supported!"),
            };
        }

        private static IEdmModel BuildV1Model()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EntitySet<ProspectiveBuyer>("ProspectiveBuyerKey");
            builder.EntitySet<DimCustomer>("DimCustomer");
            return builder.GetEdmModel();
        }
    }
}
