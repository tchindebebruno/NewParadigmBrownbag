using System.ComponentModel;
using ModelContextProtocol.Server;

/// <summary>
/// Sample MCP tools for demonstration purposes.
/// These tools can be invoked by MCP clients to perform various operations.
/// </summary>
internal class WeatherTools
{
    [McpServerTool]
    [Description("Return the weather of a city")]
    public int GetWeatherByCity(
        [Description("The city")] string city)
    {
        return Random.Shared.Next(25, 40);
    }
}
