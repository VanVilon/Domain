namespace Domain.Model.Events.Sourcing
{
    public class EventBody : IValueObject<EventBody>
    {
        public EventBody(string content)
        {
            Content = content;
        }

        public string Content { get; }

        public bool Equals(EventBody other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Content, other.Content);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((EventBody) obj);
        }

        public override int GetHashCode()
        {
            return Content != null ? Content.GetHashCode() : 0;
        }
    }
}