/*
 * Used sources
 * https://stackoverflow.com/questions/58841974/need-to-hide-a-designer-only-property-from-propertygrid-for-a-net-winforms-cont
 * https://www.vbforums.com/showthread.php?861883-RESOLVED-How-can-I-hide-form-s-DoubleBuffered-property-without-make-it-nonfunctional
 */

using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

internal partial class FilterService<T> : ITypeDescriptorFilterService
{
    private string[] PropertiesToRemove;

    public FilterService(ITypeDescriptorFilterService baseService, params string[] propertiesToRemove)
    {
        BaseService = baseService;
        PropertiesToRemove = propertiesToRemove;
    }

    public ITypeDescriptorFilterService BaseService { get; }

    public bool FilterAttributes(IComponent component, IDictionary attributes)
    {
        return BaseService.FilterAttributes(component, attributes);
    }

    public bool FilterEvents(IComponent component, IDictionary events)
    {
        return BaseService.FilterEvents(component, events);
    }

    public bool FilterProperties(IComponent component, IDictionary properties)
    {
        bool ret = BaseService.FilterProperties(component, properties);

        if (component is T && !(properties.IsFixedSize | properties.IsReadOnly))
        {
            foreach (string propName in PropertiesToRemove)
                properties.Remove(propName);
        }

        return ret;
    }
}