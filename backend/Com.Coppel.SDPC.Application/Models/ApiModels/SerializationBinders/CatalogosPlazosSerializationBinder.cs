using Newtonsoft.Json.Serialization;

namespace Com.Coppel.SDPC.Application.ViewModels.ApiModels.SerializationBinders;

public class CatalogosPlazosSerializationBinder : ISerializationBinder
{
  private static readonly DefaultSerializationBinder Binder = new();

#nullable disable
  public void BindToName(Type serializedType, out string assemblyName, out string typeName)
  {
    Binder.BindToName(serializedType, out assemblyName, out typeName);
  }

  public Type BindToType(string assemblyName, string typeName)
  {
    return typeName.CompareTo("CtlCatalogoPlazo") != 0 ? null : Binder.BindToType(assemblyName, typeName);
  }
}
