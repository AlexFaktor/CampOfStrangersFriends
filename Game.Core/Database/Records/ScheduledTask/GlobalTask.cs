﻿using Game.Core.Resources.Enums.ScheduledTask;

namespace Game.Core.Database.Records.ScheduledTask;

public class GlobalTask
{
    public Guid Id { get; set; }
    public EGlobalTask Type { get; set; }
    public uint FrequencyInSeconds { get; set; }
    public uint CallsNeeded { get; set; }
    public DateTime LastExecutionTime { get; set; }
}
