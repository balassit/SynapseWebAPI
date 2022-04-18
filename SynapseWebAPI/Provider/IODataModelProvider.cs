using Microsoft.OData.Edm;

namespace SynapseWebAPI.Provider
{
    public interface IODataModelProvider
    {
        IEdmModel GetEdmModel(string apiVersion);
    }
}