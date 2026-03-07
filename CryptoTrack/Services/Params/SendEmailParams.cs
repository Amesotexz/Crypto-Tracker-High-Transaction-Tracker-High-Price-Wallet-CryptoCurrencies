class SendEmailParams
{
    public required string RecipientEmail { get; set; }
    public required string RecipientName { get; set; }
    public required string Subject { get; set; }
    public required string Body { get; set; }
}