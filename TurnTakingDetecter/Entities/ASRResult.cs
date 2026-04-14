namespace TurnTakingDetecter.Entities
{
    public class ASRResult(bool isFinal, string content)
    {
        public bool IsFinal { get; set; } = isFinal;
        public string Content { get; set; } = content;
    }
}
