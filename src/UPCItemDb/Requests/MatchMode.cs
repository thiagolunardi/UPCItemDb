namespace UPCItemDb.Requests
{
    public enum MatchMode
    {
        /// <summary>
        /// Find best matches if no match for the whole phrase
        /// </summary>
        Default = 0,

        /// <summary>
        /// Strict mode, match all search keywords
        /// </summary>
        Phrase = 1
    }
}