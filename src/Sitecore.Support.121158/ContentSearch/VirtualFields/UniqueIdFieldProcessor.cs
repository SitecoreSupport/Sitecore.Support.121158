namespace Sitecore.Support.ContentSearch.VirtualFields
{
  using System.Collections.Generic;
  using Sitecore.ContentSearch;
  using Sitecore.ContentSearch.Linq.Common;
  using Sitecore.ContentSearch.Pipelines.ProcessFacets;
  [UsedImplicitly]
  public class UniqueIdFieldProcessor : Sitecore.ContentSearch.VirtualFields.UniqueIdFieldProcessor, IVirtualFieldProcessor, IFieldQueryTranslator
  {
    IDictionary<string, object> IFieldQueryTranslator.TranslateFieldResult(IDictionary<string, object> fields, FieldNameTranslator fieldNameTranslator)
    {
      var fieldValue = fields[fieldNameTranslator.GetIndexFieldName(BuiltinFields.UniqueId)];
      fields.Add(this.FieldName, fieldValue);
      return fields;
    }

    IDictionary<string, ICollection<KeyValuePair<string, int>>> IVirtualFieldProcessor.TranslateFacetResult(ProcessFacetsArgs args)
    {
      var facets = args.Facets;

      ICollection<KeyValuePair<string, int>> versionFacet;
      var fieldName = args.FieldNameTranslator.GetIndexFieldName(this.FieldName);
      if (facets.TryGetValue(fieldName, out versionFacet))
      {
        facets.Remove(fieldName);
        facets[BuiltinFields.UniqueId] = versionFacet;
      }

      return facets;
    }
  }
}