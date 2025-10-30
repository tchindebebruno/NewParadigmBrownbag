namespace AgentBased.Infrastructure.Constants
{
    public static class ConfigurationConstants
    {
        public const string AgentTelemetrySourceName = "agent-telemetry-source";
        public const string SubsmanagerMcpCommand = "subsmanager-mcp";
        public const string SubsmanagerMcpName = "SubsManager.Mcp";
        public const string PostgresConnectionStringName = "Postgres";
        public const string PostgresConnectionStringEnvVar = "POSTGRES_CONNECTION_STRINGS";
        public const string PostgresConnectionStringParamEnv = "ConnectionStrings__Postgres";
        public const string OpenAIKeyName = "OpenAI:ApiKey";
        public const string GTP4oMiniName = "gpt-4o-mini";
        public const string SubsmanagerMcpUrl = "SubsmanagerMcp:Url";
    }
}
