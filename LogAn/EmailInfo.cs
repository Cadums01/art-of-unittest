namespace LogAn
{
    public class EmailInfo
    {
        public string Body { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }


        public override bool Equals(object obj)
        {
            if (obj is EmailInfo that)
            {
                return Body == that.Body && To == that.To && Subject == that.Subject;
            }

            return false;
        }

        protected bool Equals(EmailInfo other)
        {
            return string.Equals(Body, other.Body) && string.Equals(To, other.To) && string.Equals(Subject, other.Subject);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Body != null ? Body.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (To != null ? To.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Subject != null ? Subject.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}