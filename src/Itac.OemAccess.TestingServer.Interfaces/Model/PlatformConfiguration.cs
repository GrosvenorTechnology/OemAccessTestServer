
namespace Itac.OemAccess.TestingServer.Model
{
    public class PlatformPolledUri
    {
        public string Uri { get; set; }
        public int? Frequency { get; set; }
    }

    public sealed class QueueConfig : PlatformPolledUri
    {
        public int? BatchSize { get; set; }
    }

    public sealed class PlatformRoutingTarget : PlatformPolledUri
    {
        public string[] Filters { get; set; } = new string[0];
    }

    public sealed class Platform
    {
        public string ApplicationVersion { get; set; }
        public string FirmwareVersion { get; set; }
        public int ProtocolLevel { get; set; } = 1;
        public ServiceBaseUri[] Services { get; set; } = new ServiceBaseUri[0];
        public PlatformUris Uris { get; set; }
        public int DefaultPollFrequency { get; set; } = 60;
        public int DefaultQueueBatchSize { get; set; } = 100;
    }

    public sealed class PlatformUris
    {
        public PlatformPolledUri[] Heartbeat { get; set; } = new PlatformPolledUri[0];
        public string[] HardwareReport { get; set; }
        public PlatformPolledUri ApplicationConfig { get; set; }
        public QueueConfig StateQueue { get; set; }
        public QueueConfig CommandQueue { get; set; }
        public QueueConfig ChangeQueue { get; set; }
        public PlatformRoutingTarget[] EventSubmission { get; set; } = new PlatformRoutingTarget[0];
        public PlatformRoutingTarget[] StateSubmission { get; set; } = new PlatformRoutingTarget[0];
        public PlatformRoutingTarget[] CommandResponseSubmission { get; set; } = new PlatformRoutingTarget[0];

    }

    public sealed class PlatformConfiguration
    {
        public Platform Platform { get; set; }
    }
    public sealed class ServiceBaseUri
    {
        public string Name { get; set; }
        public string Uri { get; set; }
    }
}
