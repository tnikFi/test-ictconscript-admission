﻿namespace Domain.Common;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTimeOffset IsoTime { get; set; }
}