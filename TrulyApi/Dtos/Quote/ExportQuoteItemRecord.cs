﻿namespace TrulyApi.Dtos.Quote
{
    public class ExportQuoteItemRecord
    {
        public string? Title { get; init; }
        public string? Note { get; init; }
        public int Rating { get; init; }
        public string? Comments { get; init; }
        public string? Source { get; init; }
        public string? Author { get; init; }
        public string? ReferenceUrl { get; init; }
        public string? Tags { get; init; }
    }
}
