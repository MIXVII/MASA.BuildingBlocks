﻿namespace MASA.BuildingBlocks.Dispatcher.IntegrationEvents.Logs;
public class IntegrationEventLog
{
    public Guid Id { get; private set; }

    public Guid EventId { get; private set; }

    public string EventTypeName { get; private set; } = null!;

    [NotMapped]
    public string EventTypeShortName => EventTypeName.Split('.')?.Last()!;

    [NotMapped]
    public IIntegrationEvent Event { get; private set; } = null!;

    public IntegrationEventStates State { get; set; } = IntegrationEventStates.NotPublished;

    public int TimesSent { get; set; } = 0;

    public DateTime CreationTime { get; private set; } = DateTime.Now;

    public string Content { get; private set; } = null!;

    public Guid TransactionId { get; private set; } = Guid.Empty;

    private IntegrationEventLog()
    {
        Id = Guid.NewGuid();
    }

    public IntegrationEventLog(IIntegrationEvent @event, Guid transactionId) : this()
    {
        EventId = @event.Id;
        CreationTime = @event.CreationTime;
        EventTypeName = @event.GetType().FullName!;
        Content = System.Text.Json.JsonSerializer.Serialize((object)@event);
        State = IntegrationEventStates.NotPublished;
        TimesSent = 0;
        TransactionId = transactionId;
    }


    public IntegrationEventLog DeserializeJsonContent(Type type)
    {
        Event = (System.Text.Json.JsonSerializer.Deserialize(Content, type) as IIntegrationEvent)!;
        return this;
    }
}
