namespace Tony.Listener.Subscribers;
internal class QueueAttribute : Attribute {
    public string QueueName { get; set; }
    public QueueAttribute( string queue_name ) {
        this.QueueName = queue_name;
    }
}
