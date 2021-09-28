using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Generic;
using System.Linq;

namespace BuscarCep.Services.Api.Configurations
{
    public class RouteConvention : IApplicationModelConvention
    {
        private readonly AttributeRouteModel _centralPrefix;

        public RouteConvention(IRouteTemplateProvider routeTemplateProvider)
        {
            _centralPrefix = new AttributeRouteModel(routeTemplateProvider);
        }

        public void Apply(ApplicationModel application)
        {
            foreach (ControllerModel controller in application.Controllers)
            {
                IEnumerable<SelectorModel> _matchedSelectors =
                    controller.Selectors.Where(selector => selector.AttributeRouteModel != null);

                if (_matchedSelectors.Any())
                    foreach (SelectorModel selector in _matchedSelectors)
                        selector.AttributeRouteModel =
                            AttributeRouteModel.CombineAttributeRouteModel(_centralPrefix, selector.AttributeRouteModel);

                IEnumerable<SelectorModel> _unmatchedSelectors =
                    controller.Selectors.Where(selector => selector.AttributeRouteModel == null);

                if (_unmatchedSelectors.Any())
                    foreach (SelectorModel selector in _unmatchedSelectors)
                        selector.AttributeRouteModel = _centralPrefix;
            }
        }
    }
}
