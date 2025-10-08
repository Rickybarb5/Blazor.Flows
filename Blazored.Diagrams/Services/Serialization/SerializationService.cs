using Newtonsoft.Json;
using Blazored.Diagrams.Diagrams; // Assuming IDiagram is here

namespace Blazored.Diagrams.Services.Serialization;

/// <inheritdoc />
public class SerializationService : ISerializationService
{
    private readonly JsonSerializerSettings _settings;

    public SerializationService()
    {
        _settings = CreateSettings();
    }
    
    /// <inheritdoc />
    public string ToJson<TDiagram>(TDiagram diagram)
        where TDiagram : IDiagram
    {
        return JsonConvert.SerializeObject(diagram, _settings);
    }

    /// <inheritdoc />
    public TDiagram FromJson<TDiagram>(string json)
        where TDiagram : IDiagram
    {
        return JsonConvert.DeserializeObject<TDiagram>(json, _settings)!;
    }

    //Todo: make this just public?
    public static JsonSerializerSettings CreateSettings()
    {
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All, 
            PreserveReferencesHandling = PreserveReferencesHandling.All,
            Formatting = Formatting.Indented,
        };

        return settings;
    }
}