using System;
using System.Collections.Generic;

namespace Lib.DbController.Models;

public partial class Event
{
    public long EventId { get; set; }

    public string? EventType { get; set; }

    public DateTime? EventTime { get; set; }

    public long? CategoryId { get; set; }

    public string? CategoryCode { get; set; }

    public string? Brand { get; set; }

    public decimal? Price { get; set; }

    public int? UserId { get; set; }

    public Guid? UserSession { get; set; }

    public int? ProductId { get; set; }
}
