using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Routing.Template;
using Microsoft.OData.Edm;

namespace SynapseWebAPI.Provider
{
    public class ODataRoutingApplicationModelProvider : IApplicationModelProvider
    {
        public int Order => 90;
        public void OnProvidersExecuted(ApplicationModelProviderContext context)
        {
            IEdmModel model = EdmCoreModel.Instance; // just for place holder
            string prefix = string.Empty;
            foreach (var controllerModel in context.Result.Controllers)
            {
                // ProspectiveBuyersController
                if (controllerModel.ControllerName == "ProspectiveBuyers")
                {
                    ProcessProspectiveBuyersController(prefix, model, controllerModel);
                    continue;
                }

                // CustomersController
                if (controllerModel.ControllerName == "Customers")
                {
                    ProcessCustomersController(prefix, model, controllerModel);
                    continue;
                }

                // MetadataController
                if (controllerModel.ControllerName == "Metadata")
                {
                    ProcessMetadata(prefix, model, controllerModel);
                    continue;
                }
            }
        }

        public void OnProvidersExecuting(ApplicationModelProviderContext context)
        { }

        private static void ProcessProspectiveBuyersController(string prefix, IEdmModel model, ControllerModel controllerModel)
        {
            foreach (var actionModel in controllerModel.Actions)
            {
                // For simplicity, only check the parameter count
                if (actionModel.ActionName == "Get")
                {
                    if (actionModel.Parameters.Count <= 1)
                    {
                        ODataPathTemplate path = new(new EntitySetProspectiveBuyersSegment());
                        actionModel.AddSelector("get", prefix, model, path);
                    }
                    else
                    {
                        ODataPathTemplate path = new(
                             new EntitySetProspectiveBuyersSegment(),
                             new EntitySetWithKeySegment());
                        actionModel.AddSelector("get", prefix, model, path);
                    }
                }
            }
        }

        private static void ProcessCustomersController(string prefix, IEdmModel model, ControllerModel controllerModel)
        {
            foreach (var actionModel in controllerModel.Actions)
            {
                // For simplicity, only check the parameter count
                if (actionModel.ActionName == "Get")
                {
                    if (actionModel.Parameters.Count <= 1)
                    {
                        ODataPathTemplate path = new(new EntitySetCustomersSegment());
                        actionModel.AddSelector("get", prefix, model, path);
                    }
                    else
                    {
                        ODataPathTemplate path = new(
                             new EntitySetCustomersSegment(),
                             new EntitySetWithKeySegment());
                        actionModel.AddSelector("get", prefix, model, path);
                    }
                }
            }
        }

        private static void ProcessMetadata(string prefix, IEdmModel model, ControllerModel controllerModel)
        {
            foreach (var actionModel in controllerModel.Actions)
            {
                if (actionModel.ActionName == "GetMetadata")
                {
                    ODataPathTemplate path = new ODataPathTemplate(MetadataSegmentTemplate.Instance);
                    actionModel.AddSelector("get", prefix, model, path);
                }
                else if (actionModel.ActionName == "GetServiceDocument")
                {
                    ODataPathTemplate path = new ODataPathTemplate();
                    actionModel.AddSelector("get", prefix, model, path);
                }
            }
        }
    }
}
