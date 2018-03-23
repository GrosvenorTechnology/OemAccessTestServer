using System;

namespace Itac.OemAccess.TestingServer.Model
{
    public class ApplicationConfiguration
    {
        public ApplicationController Controller { get; set; }
    }

    public abstract class ApplicationEntity
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }

    public sealed class ApplicationController : ApplicationEntity
    {
        public string TimeZone { get; set; } = "Etc/UTC";
        public ApplicationReader[] Readers { get; set; }
        public ApplicationPortal[] Portals { get; set; }
        public ApplicationArea[] Areas { get; set; }
        public ApplicationInput[] Inputs { get; set; }
        public ApplicationOutput[] Outputs { get; set; }
        public ApplicationReaderCommands[] ReaderCommands { get; set; }
    }

    public abstract class ApplicationCommandModeEntity : ApplicationEntity
    {
        public string OperationalMode { get; set; }
    }

    public abstract class ApplicationHardwareEntity : ApplicationCommandModeEntity
    {
        public string Address { get; set; }
    }

    public sealed class ApplicationReader : ApplicationHardwareEntity
    {
        public string TokenFormatType { get; set; } = "RDSATEONPRO";
        public string ValidLedType { get; set; } = "activeHigh";
        public string BeeperType { get; set; } = "activeHigh";
        public string ReaderTamperType { get; set; } = "unsupervised";

        public TimeSpan EnterPinPeriod { get; set; } = TimeSpan.FromSeconds(20);
        public int DigitsForPin { get; set; } = 4;

        public TimeSpan ValidReadLedPeriod { get; set; } = TimeSpan.FromSeconds(3);
        public TimeSpan ValidReadBeeperPeriod { get; set; } = TimeSpan.FromMilliseconds(200);
        public TimeSpan InvalidReadBeeperPeriod { get; set; } = TimeSpan.FromSeconds(3);
        public string InvalidReadBeeperMode { get; set; } = "urgentPulse";

        public string[] ChangeModePermissions { get; set; }

        public ApplicationReader()
        {
            OperationalMode = "tokenOnly";
            Type = "Hardware.Reader";
        }
    }

    public sealed class ApplicationPortal : ApplicationHardwareEntity
    {
        public class PortalDirection
        {
            public string[] Readers { get; set; }
            public string[] AccessPermissions { get; set; }
            public string[] EscortedPermissions { get; set; }
            public string[] EscortPermissions { get; set; }
            public string[] Areas { get; set; }
        }

        public bool UsingExitSwitch { get; set; } = false;

        public string LockType { get; set; } = "energiseToLock";
        public bool UseAuxOutputForLock { get; set; } = false;
        public string RelockMode { get; set; } = "onPortalOpen";
        public string LockDetectionType { get; set; } = "disable";

        public string SensorType { get; set; } = "disabled";
        public string SwitchType { get; set; } = "unsupervised";
        public string BreakGlassType { get; set; } = "disabled";

        public TimeSpan NormalUnlockPeriod { get; set; } = TimeSpan.FromSeconds(5);
        public TimeSpan ExtendedUnlockPeriod { get; set; } = TimeSpan.FromSeconds(10);
        public TimeSpan NormalMinimumOpenPeriod { get; set; } = TimeSpan.FromSeconds(2);
        public TimeSpan ExtendedMinimumOpenPeriod { get; set; } = TimeSpan.FromSeconds(4);
        public TimeSpan NormalOpenTooLongPeriod { get; set; } = TimeSpan.FromSeconds(30);
        public TimeSpan ExtendedOpenTooLongPeriod { get; set; } = TimeSpan.FromSeconds(60);
        public string ForcedSounderMode { get; set; } = "urgentPulse";
        public TimeSpan ForcedSounderPeriod { get; set; } = TimeSpan.FromSeconds(20);
        public string OpenTooLongSounderMode { get; set; } = "nonUrgentPulse";
        public TimeSpan OpenTooLongSouderPeriod { get; set; } = TimeSpan.FromSeconds(10);

        public int LockCurrentLimit { get; set; } = 4000;
        public int LockCurrentMinimum { get; set; } = 20;
        public int LockCurrentMaximum { get; set; } = 3000;

        public PortalDirection Entry { get; set; }
        public PortalDirection Exit { get; set; }

        public string PortalInterlock { get; set; }

        public string[] SingleUnlockPermissions { get; set; }
        public string[] ChangeModePermissions { get; set; }

        public ApplicationPortal()
        {
            OperationalMode = "normal";
            Type = "AccessControl.Portal";
        }
    }

    public sealed class ApplicationArea : ApplicationCommandModeEntity
    {
        public string OfflineMode { get; set; } = "unenforced";
        public bool EnforceOccupancyLimits { get; set; } = true;
        public int MaximumOccupancy { get; set; } = 0;
        public int MinimumOccupancy { get; set; } = 0;

        public string[] ChangeModePermissions { get; set; }
        public string[] SetPersonStatePermissions { get; set; }
        public string[] MakeAllUnknownPermissions { get; set; }
        public string[] MakeAllOutPermissions { get; set; }

        public ApplicationArea()
        {
            Type = "AccessControl.Area";
            OperationalMode = "enforced";
        }
    }

    public sealed class ApplicationInput : ApplicationHardwareEntity
    {
        public bool NormallyOpen { get; set; } = true;
        public string InputType { get; set; } = "unsupervised";
        public string InputOperationType { get; set; } = "normal";
        public TimeSpan PirInhibit { get; set; } = TimeSpan.FromSeconds(30);
        public TimeSpan PirFault { get; set; } = TimeSpan.FromSeconds(60);

        public string[] ChangeModePermissions { get; set; }

        public ApplicationInput()
        {
            Type = "Hardware.Input";
            OperationalMode = "enabled";
        }
    }
    public sealed class ApplicationOutput : ApplicationHardwareEntity
    {
        public TimeSpan PulseLength { get; set; } = TimeSpan.FromSeconds(1);
        public string DefaultOutputMode { get; set; } = "constant";
        public string OutputStateType { get; set; } = "activeHigh";
        public string[] CommandPermissions { get; set; }

        public string[] ChangeModePermissions { get; set; }

        public ApplicationOutput()
        {
            Type = "Hardware.Output";
            OperationalMode = "normal";
        }
    }

    public sealed class ApplicationReaderCommands : ApplicationHardwareEntity
    {
        public string ActionChar { get; set; } = "#";
        public bool Anonymous { get; set; }
        public string[] Readers { get; set; }
        public string[] Commands { get; set; }

        public string[] ChangeModePermissions { get; set; }

        public ApplicationReaderCommands()
        {
            Type = "AccessControl.ReaderCommand";
            OperationalMode = "normal";
        }
    }
}
